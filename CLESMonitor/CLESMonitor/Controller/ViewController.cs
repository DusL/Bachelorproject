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
        private const double TIME_WINDOW = 0.5; //in minutes
        private const int LOOP_SLEEP_INTERVAL = 1000; //in milliseconds

        private CLModel clModel;
        private ESModel esModel;
        private Thread updateChartDataThread;
        private Random random = new Random();
        private DateTime startTime;
        private TimeSpan emptyTimer;
        private TimeSpan currentSessionTime;

        // References to sensors for manual input
        private HRSensor _hrSensor;
        public HRSensor hrSensor
        {
            get { return _hrSensor; }
            set
            {
                _hrSensor = value;
                _hrSensor.sensorType = HRSensorType.ManualInput;
            }
        }
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
        Button hrMinusButton;
        Button hrPlusButton;
        // Sensors
        TrackBar hrTrackbar;
        Label hrValueLabel;
        TrackBar gsrTrackbar;
        Label gsrValueLabel;
       
        /// <summary>
        /// The View this Controller manages
        /// </summary>
        public CLESMonitorViewForm View { get; private set; }

        public ViewController(CLModel clModel, ESModel esModel)
        {
            View = new CLESMonitorViewForm(this);
            this.clModel = clModel;
            this.esModel = esModel;

            // Set outlets 
            this.setupOutlets();

            // Setup of chart1
            CLChart.Series.Clear();
            Series newSeries = new Series("Series1");
            newSeries.ChartType = SeriesChartType.Spline;
            newSeries.BorderWidth = 2;
            newSeries.Color = Color.OrangeRed;
            newSeries.XValueType = ChartValueType.DateTime;
            CLChart.Series.Add(newSeries);

            // Setup of chart2
            ESChart.Series.Clear();
            Series newSeries2 = new Series("Series1");
            newSeries2.ChartType = SeriesChartType.Spline;
            newSeries2.BorderWidth = 2;
            newSeries2.Color = Color.Blue;
            newSeries2.XValueType = ChartValueType.DateTime;
            ESChart.Series.Add(newSeries2);

            //Create a thread for the real-time graph - not yet starting
            ThreadStart updateChartDataThreadStart = new ThreadStart(UpdateChartDataLoop);
            updateChartDataThread = new Thread(updateChartDataThreadStart);
            // A background thread will automatically stop before the program closes
            updateChartDataThread.IsBackground = true;

            // Appoint delegates
            updateCLChartDataDelegate += new UpdateChartDataDelegate(UpdateCLChartData);
            updateESChartDataDelegate += new UpdateChartDataDelegate(UpdateESChartData);
            updateConsoleDelegate += new UpdateConsoleDelegate(UpdateConsole);
            updateSessionTimeDelegate += new UpdateSessionTimeDelegate(UpdateSessionTime);
            
            // Set timer initially to 0 seconds elapsed seconden verstreken 
            emptyTimer = DateTime.Now - DateTime.Now;
            sessionTimeBox.Text = emptyTimer.ToString();

            hrValueLabel.Text = hrTrackbar.Value.ToString();
            gsrValueLabel.Text = gsrTrackbar.Value.ToString();

            this.writeStringToConsole("ViewController State = Stopped");
            this.currentState = ViewControllerState.Stopped;
        }

        /// <summary>
        /// Sets the controller outlets
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
            hrMinusButton = this.View.hrMinusButton;
            hrPlusButton = this.View.hrPlusButton;
        }

        /// <summary>
        /// The loop that updates everything each second
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
        /// Keeps the session time up-to-date
        /// </summary>
        private void UpdateSessionTime()
        {
            currentSessionTime =  DateTime.Now - startTime;
            sessionTimeBox.Text = currentSessionTime.ToString();
        }

        ///<summary>
        /// Writes console messages (as part of the runloop) 
        ///</summary>
        private void UpdateConsole()
        {
        }

        /// <summary>
        /// Writes a string to the console in the GUI
        /// </summary>
        /// <param name="stringToWrite"></param>
        private void writeStringToConsole(String stringToWrite)
        {
            richTextBox1.Select(0, 0);
            richTextBox1.SelectedText = " " + stringToWrite + "\n";
        }

        /// <summary>
        /// Adjusts the CL-graph
        /// </summary>
        private void UpdateCLChartData()
        {
           
            // Calculate the most recent value
            double newDataPoint = this.clModel.calculateModelValue(currentSessionTime);

            // Update the graph and TextBox
            this.UpdateChartData(CLChart, newDataPoint, DateTime.Now);
            clTextBox.Text = newDataPoint.ToString();
        }
        /// <summary>
        ///Adjusts the ES graph
        /// </summary>
        private void UpdateESChartData()
        {
            // Pass along simulated sensor-data
            if (hrSensor.sensorType == HRSensorType.ManualInput) {
                hrSensor.sensorValue = hrTrackbar.Value;
            }
            gsrSensor.sensorValue = gsrTrackbar.Value;

            // Calculate the most recent de nieuwste waarde
            double newDataPoint = this.esModel.calculateModelValue();

            // Update the graph and TextBox
            this.UpdateChartData(ESChart, newDataPoint, DateTime.Now);
            esTextBox.Text = newDataPoint.ToString();
        }

        /// <summary>
        /// Pre: graph contains a series named "Series1"
        /// Adjusts "Series1" 
        /// </summary>
        /// <param name="chart"></param>
        /// <param name="newDataPoint"></param>
        private void UpdateChartData(Chart chart, double newDataPoint, DateTime timeStamp)
        {
            // Update chart
            Series series = chart.Series["Series1"];
            series.Points.AddXY(timeStamp.ToOADate(), newDataPoint);
            chart.ChartAreas[0].AxisX.Minimum = series.Points[0].XValue;
            chart.ChartAreas[0].AxisX.Maximum = DateTime.FromOADate(series.Points[0].XValue).AddMinutes(TIME_WINDOW).ToOADate();
            chart.Invalidate(); //redraw

            // Remove old datapoints
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

                // Check if thread is already active
                if (updateChartDataThread.IsAlive) {
                    updateChartDataThread.Resume();
                }
                else {
                    startTime = DateTime.Now;
                    updateChartDataThread.Start();
                }
            }

            // Adjust buttons
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
                clModel.setPathForParser(openFileDialog.FileName);
                writeStringToConsole("Gekozen file: " + openFileDialog.FileName);
                startButton.Enabled = true;
            }            
        }

        /// <summary>
        /// Action when the heart rate sensor type is being changed
        /// </summary>
        /// <param name="sender">Radio buttons for the available types</param>
        public void hrSensorTypeChanged(object sender)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                //TODO: is dit vies?
                if (radioButton.Name.Equals("hrSensorTypeRadioButton1"))
                {
                    hrSensor.sensorType = HRSensorType.ManualInput;
                    hrTrackbar.Enabled = true;
                    hrMinusButton.Enabled = true;
                    hrPlusButton.Enabled = true;
                }
                else if (radioButton.Name.Equals("hrSensorTypeRadioButton2"))
                {
                    hrSensor.sensorType = HRSensorType.BluetoothZephyr;
                    hrTrackbar.Enabled = false;
                    hrMinusButton.Enabled = false;
                    hrPlusButton.Enabled = false;
                }
            }
        }
    }
}
