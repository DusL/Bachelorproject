using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CLESMonitor.Model;
using CLESMonitor.View;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using System.Drawing;

namespace CLESMonitor.Controller
{
    enum ViewControllerState
    {
        Unknown,
        Started,
        Paused,
        Stopped,
        Calibrating
    }

    public class ViewController
    {
        private const double TIME_WINDOW = 0.5; //in minuten
        private const int LOOP_SLEEP_INTERVAL = 1000; //in milliseconden

        private CLModel clModel;
        private ESModel esModel;
       // public XMLFileTaskParser parser;
        private CLESMonitorViewForm _view;
        private Thread updateChartDataThread;
        private Random random = new Random();
        private DateTime startTime;
        private TimeSpan emptyTimer;
        private TimeSpan currentSessionTime;

        // Verwijzing naar sensoren voor handmatige input
        public HRSensor hrSensor;
        public GSRSensor gsrSensor;

        public delegate void UpdateChartDataDelegate();
        public UpdateChartDataDelegate updateCLChartDataDelegate;
        public UpdateChartDataDelegate updateESChartDataDelegate;
        public delegate void UpdateConsoleDelegate();
        public UpdateConsoleDelegate updateConsoleDelegate;
        public delegate void UpdateSessionTimeDelegate();
        public UpdateSessionTimeDelegate updateSessionTimeDelegate;

        private ViewControllerState currentState;

        // Outlets
        Chart CLChart;
        Chart ESChart;
        TextBox clTextBox;
        TextBox esTextBox;
        TextBox sessionTimeBox;
        RichTextBox richTextBox1;
        Button startButton;
        Button stopButton;
        Button pauseButton;
        Button calibrateButton;
        OpenFileDialog openFileDialog;
        // Sensoren
        TrackBar hrTrackbar;
        Label hrValueLabel;
        TrackBar gsrTrackbar;
        Label gsrValueLabel;
       
        public CLESMonitorViewForm View
        {
            get
            {
                return _view;
            }
        }

        public ViewController(CLModel clModel, ESModel esModel)
        {
            _view = new CLESMonitorViewForm(this);
            this.clModel = clModel;
            this.esModel = esModel;

            // Stel outlets in
            this.setupOutlets();

            // Setup van chart1
            CLChart.Series.Clear();
            Series newSeries = new Series("Series1");
            newSeries.ChartType = SeriesChartType.Spline;
            newSeries.BorderWidth = 2;
            newSeries.Color = Color.OrangeRed;
            newSeries.XValueType = ChartValueType.DateTime;
            CLChart.Series.Add(newSeries);

            // Setup van chart2
            ESChart.Series.Clear();
            Series newSeries2 = new Series("Series1");
            newSeries2.ChartType = SeriesChartType.Spline;
            newSeries2.BorderWidth = 2;
            newSeries2.Color = Color.Blue;
            newSeries2.XValueType = ChartValueType.DateTime;
            ESChart.Series.Add(newSeries2);

            // Creëer een thread voor de real-time grafiek - nog niet starten
            ThreadStart updateChartDataThreadStart = new ThreadStart(UpdateChartDataLoop);
            updateChartDataThread = new Thread(updateChartDataThreadStart);
            // Een background thread zal automatisch stoppen voordat het programma sluit
            updateChartDataThread.IsBackground = true;

            // Wijs delegates toe
            updateCLChartDataDelegate += new UpdateChartDataDelegate(UpdateCLChartData);
            updateESChartDataDelegate += new UpdateChartDataDelegate(UpdateESChartData);
            updateConsoleDelegate += new UpdateConsoleDelegate(UpdateConsole);
            updateSessionTimeDelegate += new UpdateSessionTimeDelegate(UpdateSessionTime);
            
            // Stelt de timer initeel in op 0 seconden verstreken 
            emptyTimer = DateTime.Now - DateTime.Now;
            sessionTimeBox.Text = emptyTimer.ToString();

            hrValueLabel.Text = hrTrackbar.Value.ToString();
            gsrValueLabel.Text = gsrTrackbar.Value.ToString();

            this.writeStringToConsole("ViewController State = Stopped");
            this.currentState = ViewControllerState.Stopped;
        }

        /// <summary>
        /// Stelt de outlets van de controller in
        /// </summary>
        private void setupOutlets()
        {
            CLChart = this.View.CLChart;
            ESChart = this.View.ESChart;
            clTextBox = this.View.clTextBox;
            esTextBox = this.View.esTextBox;
            richTextBox1 = this.View.richTextBox1;
            sessionTimeBox = this.View.sesionTimeBox;
            startButton = this.View.startButton;
            stopButton = this.View.stopButton;
            calibrateButton = this.View.calibrateButton;
            pauseButton = this.View.pauseButton;
            hrTrackbar = this.View.hrTrackBar;
            hrValueLabel = this.View.hrValueLabel;
            gsrTrackbar = this.View.gsrTrackBar;
            gsrValueLabel = this.View.gsrValueLabel;
            openFileDialog = this.View.openFileDialog1;
        }

        /// <summary>
        /// De loop waarmee iedere seconde alles geupdate wordt
        /// </summary>
        private void UpdateChartDataLoop()
        {
            while (true)
            {
                // TODO: het updaten van de chart wordt hier gepauzeerd; je wilt
                // eigenlijk dat er tijdelijk geen metingen/calculaties meer plaatsvinden!
                if (this.currentState == ViewControllerState.Started)
                {
                    CLChart.Invoke(updateCLChartDataDelegate);
                    ESChart.Invoke(updateESChartDataDelegate);
                }
                richTextBox1.Invoke(updateConsoleDelegate);
                sessionTimeBox.Invoke(updateSessionTimeDelegate);

                Thread.Sleep(LOOP_SLEEP_INTERVAL);
            }
        }
        /// <summary>
        /// Hiermee wordt de tijdsduur van de sessie up-to-date gehouden
        /// </summary>
        private void UpdateSessionTime()
        {
            currentSessionTime =  DateTime.Now - startTime;
            sessionTimeBox.Text = currentSessionTime.ToString();
        }

        ///<summary>
        /// Schrijft als onderdeel van de loop console-berichten. 
        ///</summary>
        private void UpdateConsole()
        {
        }

        /// <summary>
        /// Schrijft een string naar de console op het scherm
        /// </summary>
        /// <param name="stringToWrite"></param>
        private void writeStringToConsole(String stringToWrite)
        {
            richTextBox1.Select(0, 0);
            richTextBox1.SelectedText = " " + stringToWrite + "\n";
        }

        /// <summary>
        /// Hiermee wordt de CL-grafiek bijgewerkt
        /// </summary>
        public void UpdateCLChartData()
        {
           
            // Bereken de nieuwste waarde
            
            double newDataPoint = this.clModel.calculateModelValue(currentSessionTime);

            // Update de grafiek en TextBox
            this.UpdateChartData(CLChart, newDataPoint, DateTime.Now);
            clTextBox.Text = newDataPoint.ToString();
        }
        /// <summary>
        /// Hiermee wordt de ES grafiek bijgewerkt
        /// </summary>
        public void UpdateESChartData()
        {
            // Gesimuleerde sensor-data doorgeven
            //hrSensor.sensorValue = hrTrackbar.Value;
            gsrSensor.sensorValue = gsrTrackbar.Value;

            // Bereken de nieuwste waarde
            double newDataPoint = this.esModel.calculateModelValue();

            // Update de grafiek en TextBox
            this.UpdateChartData(ESChart, newDataPoint, DateTime.Now);
            esTextBox.Text = newDataPoint.ToString();
        }

        /// <summary>
        /// Pre: grafiek bevat een series genaamd "Series1"
        /// Werkt "Series1" van een grafiek bij
        /// </summary>
        /// <param name="chart"></param>
        /// <param name="newDataPoint"></param>
        private void UpdateChartData(Chart chart, double newDataPoint, DateTime timeStamp)
        {
            // Update de chart
            Series series = chart.Series["Series1"];
            series.Points.AddXY(timeStamp.ToOADate(), newDataPoint);
            chart.ChartAreas[0].AxisX.Minimum = series.Points[0].XValue;
            chart.ChartAreas[0].AxisX.Maximum = DateTime.FromOADate(series.Points[0].XValue).AddMinutes(TIME_WINDOW).ToOADate();
            chart.Invalidate(); //redraw

            // Verwijder oude datapunten
            double removeBefore = timeStamp.AddSeconds((double)(60) * (-TIME_WINDOW)).ToOADate();
            while (series.Points[0].XValue < removeBefore)
            {
                series.Points.RemoveAt(0);
            }
        }

        public void startButtonClicked(object sender, System.EventArgs e)
        {
            // Predefine the viewing area of the chart
            DateTime minValue = DateTime.Now;
            DateTime maxValue = minValue.AddSeconds(TIME_WINDOW*60);
            
            CLChart.ChartAreas[0].AxisX.Minimum = minValue.ToOADate();
            CLChart.ChartAreas[0].AxisX.Maximum = maxValue.ToOADate();
            ESChart.ChartAreas[0].AxisX.Minimum = minValue.ToOADate();
            ESChart.ChartAreas[0].AxisX.Maximum = maxValue.ToOADate();

            if (this.currentState == ViewControllerState.Stopped)
            {
                esModel.startSession();

                // Controleer of de thread al actief is
                if (updateChartDataThread.IsAlive) {
                    updateChartDataThread.Resume();
                }
                else {
                    startTime = DateTime.Now;
                    updateChartDataThread.Start();
                }
            }

            // Pas knoppen aan
            startButton.Enabled = false;
            pauseButton.Enabled = true;
            stopButton.Enabled = true;
            calibrateButton.Enabled = false;

            this.writeStringToConsole("ViewController State = Started");
            this.currentState = ViewControllerState.Started;
        }
        public void pauseButtonClicked()
        {
            pauseButton.Enabled = false;
            startButton.Enabled = true;

            this.writeStringToConsole("ViewController State = Pauzed");
            this.currentState = ViewControllerState.Paused;
        }
       
        public void stopButtonClicked()
        {
            updateChartDataThread.Suspend();
            stopButton.Enabled = false;
            View.calibrateButton.Enabled = false;
            pauseButton.Enabled = false;
            startButton.Enabled = true;
           // updateChartDataThread.Interrupt();
           // updateChartDataThread.

            this.writeStringToConsole("ViewController State = Stopped");
            this.currentState = ViewControllerState.Stopped;
        }

        public void HRValueChangedInManualContext(object sender)
        {
            TrackBar trackBar = (TrackBar)sender;
            int trackBarValue = trackBar.Value;
            hrValueLabel.Text = trackBarValue.ToString();
        }

        public void increaseHRValueInManualContext()
        {
            hrTrackbar.Value = hrTrackbar.Value + 10;
            hrValueLabel.Text = hrTrackbar.Value.ToString();
        }

        public void decreaseHRValueInManualContext()
        {
            hrTrackbar.Value = hrTrackbar.Value - 10;
            hrValueLabel.Text = hrTrackbar.Value.ToString();
        }

        public void GSRValueChangedInManualContext(object sender)
        {
            TrackBar trackBar = (TrackBar)sender;
            int trackBarValue = trackBar.Value;
            gsrValueLabel.Text = trackBarValue.ToString();
        }

        public void increaseGSRValueInManualContext()
        {
            gsrTrackbar.Value = gsrTrackbar.Value + 10;
            gsrValueLabel.Text = gsrTrackbar.Value.ToString();
        }

        public void decreaseGSRValueInManualContext()
        {
            gsrTrackbar.Value = gsrTrackbar.Value - 10;
            gsrValueLabel.Text = gsrTrackbar.Value.ToString();
        }

        public void resetTimer() 
        {
           sessionTimeBox.Text = emptyTimer.ToString();
        }

        public void openScenarioFileDialog()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //parser.readPath(openFileDialog.FileName);
                clModel.setPathForParser(openFileDialog.FileName);
                writeStringToConsole("Gekozen file: " + openFileDialog.FileName);
            }            
        }
    }
}
