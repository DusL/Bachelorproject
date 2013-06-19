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
            View.Invoke((Action)(() =>
            {
                // Update the CL-graph and TextBox
                double newCLDataPoint = clModel.calculateModelValue();
                UpdateChartData(View.CLChart, newCLDataPoint, currentSessionTime);
                View.clTextBox.Text = newCLDataPoint.ToString();

                // Update the ES-graph and TextBox
                double newESDataPoint = this.esModel.calculateModelValue();
                this.UpdateChartData(View.ESChart, newESDataPoint, currentSessionTime);
                View.esTextBox.Text = newESDataPoint.ToString();

                // Keep the session time up-to-date
                currentSessionTime = DateTime.Now - startTime;
                View.sessionTimeLabel.Text = currentSessionTime.ToString(@"%h\:mm\:ss");
            }));
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
            // Cleanup / reset values
            startTime = DateTime.Now;
            currentSessionTime = emptyTimer;
            View.CLChart.Series[0].Points.Clear();
            View.ESChart.Series[0].Points.Clear();

            // Start the update timer
            updateTimer = new Timer(updateCallback, null, 0, 1000);

            // Pass the message to the models
            clModel.startSession();
            esModel.startSession();

            // Adjust buttons
            View.startButton.Enabled = false;
            View.stopButton.Enabled = true;
        }
        
        public void stopButtonClicked()
        {
            clModel.stopSession();
            esModel.stopSession();

            // Stop the update timer
            updateTimer.Dispose();

            View.stopButton.Enabled = false;
            View.startButton.Enabled = true;
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
