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
    public class MainViewController
    {
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
            View = new MainView(this);
            this.clModel = clModel;
            this.esModel = esModel;

            // Set timer initially to 0 seconds elapsed seconden verstreken 
            emptyTimer = DateTime.Now - DateTime.Now;
            View.sessionTimeLabel.Text = emptyTimer.ToString();
        }

        private void updateCallback(Object stateInfo)
        {
            if (!View.IsDisposed)
            {
                View.Invoke((Action)(() =>
                {
                    if (!View.IsDisposed)
                    {
                        // Update the CL-graph and TextBox
                        double newCLDataPoint = clModel.calculateModelValue();
                        //UpdateChartData(View.clesChart.Series["Series2"],View.clesChart.ChartAreas[1], newCLDataPoint, currentSessionTime);
                        UpdateChartData(View.clesChart.Series["clSeries"], View.clesChart.ChartAreas[0], newCLDataPoint, currentSessionTime);
                        // Update the ES-graph and TextBox
                        double newESDataPoint = esModel.calculateModelValue();
                        this.UpdateChartData(View.clesChart.Series["esSeries"], View.clesChart.ChartAreas[0], newESDataPoint, currentSessionTime);

                    // Keep the session time up-to-date
                    currentSessionTime = DateTime.Now - startTime;
                    View.sessionTimeLabel.Text = currentSessionTime.ToString(@"%h\:mm\:ss");

                    compareValues(newCLDataPoint, newESDataPoint);
                }
            }));
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
            string[] textToPrint = {"LET OP: De cognitieve belasting is veel hoger dan de emoitionele toestand",
                                       "LET OP: De emoitionele toestand is veel hoger dan de cognitieve belasting",
                                       "De emotionele toestand wijkt aanzienlijk af van de gemeten cognitieve belasting",
                                       "De cognitieve belasting wijkt aanzienlijk af van de gemeten emotionele toestand",
                                       "De gemeten waarde voor de cognitieve belasting en emotionele toestand komen overeen"};
            
            string previousText = View.clesRichTextBox.Lines[0];

            View.clesRichTextBox.SelectionStart = 0;
            View.clesRichTextBox.SelectionLength = 0;
            
            if (CL - ES >= 3 && !previousText.Equals(textToPrint[0]))
            {
                View.clesRichTextBox.SelectionColor = Color.DarkRed;
                View.clesRichTextBox.SelectedText = "\n" + textToPrint[0];
            }

            else if (ES - CL >= 3 && !previousText.Equals(textToPrint[1]))
            {
                View.clesRichTextBox.SelectionColor = Color.DarkBlue;
                View.clesRichTextBox.SelectedText = "\n" + textToPrint[1];
            }
            else if (ES - CL == 2 && !previousText.Equals(textToPrint[2]))
            {
                View.clesRichTextBox.SelectionColor = Color.Blue;
                View.clesRichTextBox.SelectedText = "\n" + textToPrint[2];
            }

            else if (CL - ES == 2 && !previousText.Equals(textToPrint[3]))
            {
                View.clesRichTextBox.SelectionColor = Color.Red;
                View.clesRichTextBox.SelectedText = "\n" + textToPrint[3];
            }

            else if (!previousText.Equals(textToPrint[4]))
            {
                View.clesRichTextBox.SelectionColor = Color.Black;
                View.clesRichTextBox.SelectedText = "\n" + textToPrint[4];
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

        /// <summary>
        /// This method is called by the view when the quit-shortcut is triggered.
        /// </summary>
        internal void quit()
        {
            View.Dispose();
        }
    }
}
