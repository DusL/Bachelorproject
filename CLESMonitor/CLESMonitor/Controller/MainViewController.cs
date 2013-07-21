using CLESMonitor.Model.CL;
using CLESMonitor.Model.ES;
using CLESMonitor.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Timer = System.Threading.Timer;

namespace CLESMonitor.Controller
{
    /// <summary>
    /// CLESMonitorViewController is the main viewcontroller for the CLESMonitor application.
    /// </summary>
    public class MainViewController : CLModelDelegate, ESModelDelegate
    {
        public enum State
        {
            Unkown,
            HighCL,
            MidHighCL,
            HighES,
            MidHighES,
            Normal
        }

        private const double TIME_WINDOW = 0.5; //in minutes
        private const int LOOP_SLEEP_INTERVAL = 1000; //in milliseconds

        private CLModel clModel;
        private ESModel esModel;
        private DateTime startTime;
        private TimeSpan emptyTimer;
        private TimeSpan currentSessionTime;
        private Timer updateTimer;

        /// <summary>The View this Controller manages</summary>
        public MainView View { get; set; }
        public State previousState { get; private set; }
        
        /// <summary>A utility view for the CL aspect of the monitor</summary>
        public Form clUtilityView
        {
            get { return _clUtilityView; }
            set
            {
                _clUtilityView = value;
                _clUtilityView.TopLevel = false;
                _clUtilityView.Visible = true;
                _clUtilityView.Dock = DockStyle.Fill;
                View.tableLayoutPanel2.Controls.Add(_clUtilityView);
            }
        }
        private Form _clUtilityView; //backing field

        /// <summary>A utility view for the ES aspect of the monitor</summary>
        public Form esUtilityView 
        {
            get { return _esUtilityView; }            
            set
            {
                _esUtilityView = value;
                _esUtilityView.TopLevel = false;
                _esUtilityView.Visible = true;
                _esUtilityView.Dock = DockStyle.Fill;
                View.tableLayoutPanel2.Controls.Add(_esUtilityView);
            }
        }
        private Form _esUtilityView; //backing field

        /// <summary>
        /// The constructor method.
        /// </summary>
        /// <param name="clModel">The CL model to use.</param>
        /// <param name="esModel">The ES model to use.</param>
        public MainViewController(CLModel clModel, ESModel esModel)
        {
            this.View = new MainView();
            this.View.startButtonClickedHandler += new MainView.EventHandler(startButtonClicked);
            this.View.stopButtonClickedHandler += new MainView.EventHandler(stopButtonClicked);
            this.View.startToolStripMenuItemClickedHandler += new MainView.EventHandler(startButtonClicked);
            this.View.stopToolStripMenuItemClickedHandler += new MainView.EventHandler(stopButtonClicked);
            this.View.quitToolStripMenuItemClickedHandler += new MainView.EventHandler(quit);

            this.clModel = clModel;
            clModel.delegateObject = this;
            this.esModel = esModel;
            esModel.delegateObject = this;

            // Set timer initially to 0 seconds elapsed seconden verstreken 
            emptyTimer = DateTime.Now - DateTime.Now;
            View.sessionTimeLabel.Text = emptyTimer.ToString();
        }

        #region CLModelDelegate, ESModelDelegate implementation

        /// <summary>
        /// Displays a log message in the UI.
        /// </summary>
        /// <param name="message">The message to display</param>
        public void displayLogMessage(string message)
        {
            displayLogMessage(message, Color.Black);
        }

        /// <summary>
        /// Displays a log message in the UI with the given message color.
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <param name="messageColor">The color to use</param>
        public void displayLogMessage(string message, Color messageColor)
        {
            // Voer uit op de UI thread
            View.Invoke((Action)(() =>
            {
                // Move the 'pointer' to the top of the text box
                View.clesRichTextBox.SelectionStart = 0;
                View.clesRichTextBox.SelectionLength = 0;

                // Add a new line of text
                View.clesRichTextBox.SelectedText = currentSessionTime.ToString(@"%h\:mm\:ss") + ": ";
                View.clesRichTextBox.SelectionColor = messageColor;
                View.clesRichTextBox.SelectedText += message + "\n\n";
            }));
        }

        #endregion

        private void updateCallback(Object stateInfo)
        {
            try
            {
                View.Invoke((Action)(() =>
                {
                    try
                    {
                        // Update the CL-graph and TextBox
                        double newCLDataPoint = clModel.calculateModelValue();
                        UpdateChartData(View.clesChart.Series["clSeries"], View.clesChart.ChartAreas[0], newCLDataPoint, currentSessionTime);
                        // Update the ES-graph and TextBox
                        double newESDataPoint = esModel.calculateModelValue();
                        this.UpdateChartData(View.clesChart.Series["esSeries"], View.clesChart.ChartAreas[0], newESDataPoint, currentSessionTime);

                        // Keep the session time up-to-date
                        currentSessionTime = DateTime.Now - startTime;
                        View.sessionTimeLabel.Text = currentSessionTime.ToString(@"%h\:mm\:ss");

                        compareValues(newCLDataPoint, newESDataPoint);
                    }
                    catch (ObjectDisposedException exception) { Console.WriteLine(exception.ToString()); }
                }));
            }
            catch (ObjectDisposedException exception) { Console.WriteLine(exception.ToString()); }
        }

        /// <summary>
        /// Pre: graph contains a series named "Series1"
        /// Adjusts "Series1" 
        /// </summary>
        /// <param name="chart">The chart in question</param>
        /// <param name="newDataPoint"></param>
        private void UpdateChartData(Series series, ChartArea chartArea, double newDataPoint, TimeSpan currentSessionTime)
        {
            // Update chart
            double now = Math.Floor(currentSessionTime.TotalSeconds);
            series.Points.AddY(newDataPoint);
         
            if (chartArea.AxisX.Maximum >= chartArea.AxisX.ScaleView.Size)
            {
                chartArea.AxisX.ScaleView.Scroll(chartArea.AxisX.Maximum );
            }
         
            View.clesChart.Invalidate(); //redraw
        }

        /// <summary>
        /// Compares the new CL and ES values and reports the situation
        /// </summary>
        /// <param name="CL">The most recently calculated CL-value</param>
        /// <param name="ES">The most recently calculated ES-value</param>
        private void compareValues(double CL, double ES)
        {
            // Check the relative difference in classification between the CL and ES states.
            switch ((int)(CL - ES))
            {
                case 3:
                    if (!previousState.Equals(State.HighCL))
                    {
                        previousState = State.HighCL;
                        displayLogMessage("Cognitieve Belasting is extreem hoog tegenover de Emotionele Toestand", Color.Red);
                    }
                    break;
                case 2:
                    if (!previousState.Equals(State.MidHighCL))
                    {
                        previousState = State.MidHighCL;
                        displayLogMessage("Cognitieve Belasting is aanzienlijk hoger dan de Emotionele Toestand", Color.DarkOrange);
                    }
                    break;
                case -3:
                    if (!previousState.Equals(State.HighES))
                    {
                        previousState = State.HighES;
                        displayLogMessage("Emotionele Toestand is extreem hoog tegenover de Cognitieve Belasting", Color.Blue);
                    }
                    break;
                case -2:
                    if (!previousState.Equals(State.MidHighES))
                    {
                        previousState = State.MidHighES;
                        displayLogMessage("Emotionele Toestand is aanzienlijk hoger dan de Cognitieve Belasting", Color.Indigo);
                    }
                    break;
                default:
                    if (!previousState.Equals(State.Normal))
                    {
                        previousState = State.Normal;
                        displayLogMessage("Cognitieve Belasting en Emotionele Toestand komen overeen", Color.Black);
                    }
                    break;
            }
        }

        /// <summary>
        /// The actionmethode for selecting the startfunction in the menustrip
        /// </summary>
        public void startButtonClicked()
        {
            // Cleanup / reset values
            startTime = DateTime.Now;
            currentSessionTime = emptyTimer;
            View.clesChart.Series[0].Points.Clear();
            View.clesChart.Series[1].Points.Clear();
            View.clesChart.ChartAreas[0].AxisX.ScaleView.Scroll(0.0); // Reset scrollbar to the begining of the chart

            //TODO: Legen van de lijst wanneer opnieuw wordt gestart

            // Start the update timer
            updateTimer = new Timer(updateCallback, null, 0, 1000);

            // Pass the message to the models
            clModel.startSession();
            esModel.startSession();

        }
        
        public void stopButtonClicked()
        {
            clModel.stopSession();
            esModel.stopSession();

            // Stop the update timer
            updateTimer.Dispose();
        }

        public void keysArePressed(object sender, EventArgs e)
        {
            KeyEventArgs keyEventArgs = (KeyEventArgs)e;
            if (keyEventArgs.Alt && keyEventArgs.KeyCode == Keys.F4)
            {
                quit();
            }
        }

        /// <summary>
        /// This method is called by the view when the quit-shortcut is triggered.
        /// </summary>
        internal void quit()
        {
            View.Dispose();
        }
    }
}
