using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CLESMonitor.Model.CL;
using CLESMonitor.Model.ES;
using CLESMonitor.View;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

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

    public class CLESMonitorViewController
    {
        private const double TIME_WINDOW = 0.5; //in minutes
        private const int LOOP_SLEEP_INTERVAL = 1000; //in milliseconds

        private CLModel clModel;
        private ESModel esModel;
        private Thread updateChartDataThread;
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
                _hrSensor.type = HRSensorType.ManualInput;
            }
        }
        private GSRSensor _gsrSensor;
        public GSRSensor gsrSensor
        {
            get { return _gsrSensor; }
            set
            {
                _gsrSensor = value;
                _gsrSensor.type = GSRSensorType.ManualInput;
            }
        }

        public XMLParser parser;

        public delegate void UpdateDelegate();

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
        OpenFileDialog openFileDialog;
        TableLayoutPanel tableLayoutPanel2;

        /// <summary>The View this Controller manages</summary>
        public CLESMonitorViewForm View { get; set; }
        public Form clUtilityView { get; set; }
        private Form _esUtilityView;
        public Form esUtilityView 
        {
            get { return _esUtilityView; }            
            set
            {
                _esUtilityView = value;
                _esUtilityView.TopLevel = false;
                _esUtilityView.Visible = true;
                _esUtilityView.Dock = DockStyle.Fill;
                tableLayoutPanel2.Controls.Add(_esUtilityView);
            }
        }

        /// <summary>
        /// The constructor method.
        /// </summary>
        /// <param name="clModel">The CL model to use.</param>
        /// <param name="esModel">The ES model to use.</param>
        public CLESMonitorViewController(CLModel clModel, ESModel esModel)
        {
            View = new CLESMonitorViewForm(this);
            this.clModel = clModel;
            this.esModel = esModel;

            // Set outlets 
            this.setupOutlets();

            // Setup of chart1
            CLChart.Series.Clear();
            Series newSeries = new Series("Series1");
            newSeries.ChartType = SeriesChartType.FastLine;
            newSeries.BorderWidth = 2;
            newSeries.Color = Color.OrangeRed;
            newSeries.XValueType = ChartValueType.Double;
            CLChart.Series.Add(newSeries);

            // Setup of chart2
            ESChart.Series.Clear();
            Series newSeries2 = new Series("Series1");
            newSeries2.ChartType = SeriesChartType.Line;
            newSeries2.BorderWidth = 2;
            newSeries2.Color = Color.Blue;
            newSeries2.XValueType = ChartValueType.Double;
            ESChart.Series.Add(newSeries2);

            // Create a thread for the real-time graph - not yet starting
            ThreadStart updateChartDataThreadStart = new ThreadStart(UpdateChartDataLoop);
            updateChartDataThread = new Thread(updateChartDataThreadStart);
            // A background thread will automatically stop before the program closes
            updateChartDataThread.IsBackground = true;

            // Set timer initially to 0 seconds elapsed seconden verstreken 
            emptyTimer = DateTime.Now - DateTime.Now;
            sessionTimeBox.Text = emptyTimer.ToString();

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
            sessionTimeBox = this.View.sessionTimeBox;
            startButton = this.View.startButton;
            stopButton = this.View.stopButton;
            pauseButton = this.View.pauseButton;
            openFileDialog = this.View.openFileDialog1;
            tableLayoutPanel2 = this.View.tableLayoutPanel2;
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
                    CLChart.Invoke(new UpdateDelegate(UpdateCLChartData));
                    ESChart.Invoke(new UpdateDelegate(UpdateESChartData));
                }
                richTextBox1.Invoke(new UpdateDelegate(UpdateConsole));
                sessionTimeBox.Invoke(new UpdateDelegate(UpdateSessionTime));

                // TODO: vervangen met een timer
                Thread.Sleep(LOOP_SLEEP_INTERVAL);
            }
        }
        /// <summary>
        /// Keeps the session time up-to-date
        /// </summary>
        private void UpdateSessionTime()
        {
            currentSessionTime =  DateTime.Now - startTime;
            sessionTimeBox.Text = currentSessionTime.ToString(@"%h\:mm\:ss");

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
            double newDataPoint = clModel.calculateModelValue();

            // Update the graph and TextBox
            this.UpdateChartData(CLChart, newDataPoint, currentSessionTime);
            clTextBox.Text = newDataPoint.ToString();
        }
        /// <summary>
        ///Adjusts the ES graph
        /// </summary>
        private void UpdateESChartData()
        {
            // Calculate the most recent de nieuwste waarde
            double newDataPoint = this.esModel.calculateModelValue();

            // Update the graph and TextBox
            this.UpdateChartData(ESChart, newDataPoint, currentSessionTime);
            esTextBox.Text = newDataPoint.ToString();
        }

        /// <summary>
        /// Pre: graph contains a series named "Series1"
        /// Adjusts "Series1" 
        /// </summary>
        /// <param name="chart"></param>
        /// <param name="newDataPoint"></param>
        private void UpdateChartData(Chart chart, double newDataPoint, TimeSpan currentSessionTime)
        {
            // Update chart
            Series series = chart.Series["Series1"];
            series.Points.AddXY(Math.Floor(currentSessionTime.TotalSeconds), newDataPoint);
            chart.ChartAreas[0].AxisX.Minimum = series.Points[0].XValue;
            chart.ChartAreas[0].AxisX.Maximum = series.Points[0].XValue + ((double)(60) * (TIME_WINDOW));
            chart.Invalidate(); //redraw

            // Remove old datapoints
            double removeBefore = Math.Floor(currentSessionTime.TotalSeconds- ((double)(60) * (TIME_WINDOW)));
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
                clModel.startSession();
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
            if (this.currentState == ViewControllerState.Started)
            {
                clModel.stopSession();
                esModel.stopSession();
            }

            updateChartDataThread.Suspend();
            stopButton.Enabled = false;
            pauseButton.Enabled = false;
            startButton.Enabled = true;

            this.writeStringToConsole("ViewController State = Stopped");
            this.currentState = ViewControllerState.Stopped;
        }

        /// <summary>
        /// Action method when the openScenarioFileButton is clicked.
        /// </summary>
        public void openScenarioFileDialog()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(File.Open(openFileDialog.FileName, FileMode.Open));
                parser.loadTextReader(streamReader);
                writeStringToConsole("Gekozen file: " + openFileDialog.FileName);
                startButton.Enabled = true;
            }            
        }
    }
}
