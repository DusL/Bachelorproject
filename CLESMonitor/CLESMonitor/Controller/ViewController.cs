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
    class ViewController
    {
        private const double TIME_WINDOW = 0.5; //in minuten
        private const int LOOP_SLEEP_INTERVAL = 1000; //in milliseconden

        private CLModel clModel;
        private ESModel esModel;
        private CLESMonitorViewForm _view;
        private Thread updateChartDataThread;
        private Random random = new Random();

        public delegate void UpdateChartDataDelegate();
        public UpdateChartDataDelegate updateCLChartDataDelegate;
        public UpdateChartDataDelegate updateESChartDataDelegate;
        public delegate void UpdateConsoleDelegate();
        public UpdateConsoleDelegate updateConsoleDelegate;

        // Outlets
        Chart CLChart;
        Chart ESChart;
        TextBox clTextBox;
        TextBox esTextBox;
        RichTextBox richTextBox1;

        public CLESMonitorViewForm View
        {
            get
            {
                return _view;
            }
        }

        public ViewController()
        {
            _view = new CLESMonitorViewForm();

            // Stel outlets in
            CLChart = this.View.CLChart;
            ESChart = this.View.ESChart;
            clTextBox = this.View.clTextBox;
            esTextBox = this.View.esTextBox;
            richTextBox1 = this.View.richTextBox1;

            // Creëer een thread voor de real-time grafiek - nog niet starten
            ThreadStart updateChartDataThreadStart = new ThreadStart(UpdateChartDataLoop);
            updateChartDataThread = new Thread(updateChartDataThreadStart);

            // Wijs delegates toe
            updateCLChartDataDelegate += new UpdateChartDataDelegate(UpdateCLChartData);
            updateESChartDataDelegate += new UpdateChartDataDelegate(UpdateESChartData);
            updateConsoleDelegate += new UpdateConsoleDelegate(UpdateConsole);

            startTrending_Click(null, null);
        }

        private void UpdateChartDataLoop()
        {
            while (true)
            {
                CLChart.Invoke(updateCLChartDataDelegate);
                ESChart.Invoke(updateESChartDataDelegate);
                richTextBox1.Invoke(updateConsoleDelegate);

                Thread.Sleep(LOOP_SLEEP_INTERVAL);
            }
        }

        ///<summary>
        ///Hiermee wordt in het tekst veld telkens bovenaan een regel toegevoegd. 
        ///</summary>
        private void UpdateConsole()
        {
            richTextBox1.Select(0, 0);
            richTextBox1.SelectedText = " Deze bla staat nu boven aan" + "\n";
        }

        public void UpdateCLChartData()
        {
            DateTime timeStamp = DateTime.Now;
            Series series = CLChart.Series["Series1"];

            double newDataValue = AddNewPoint(timeStamp, series);

            // Update chart
            CLChart.ChartAreas[0].AxisX.Minimum = series.Points[0].XValue;
            CLChart.ChartAreas[0].AxisX.Maximum = DateTime.FromOADate(series.Points[0].XValue).AddMinutes(TIME_WINDOW).ToOADate();
            CLChart.Invalidate(); //redraw

            // Update TextBox
            clTextBox.Text = newDataValue.ToString();
        }

        public void UpdateESChartData()
        {
            DateTime timeStamp = DateTime.Now;
            Series series = ESChart.Series["Series1"];

            double newDataValue = AddNewPoint(timeStamp, series);

            // Update chart
            ESChart.ChartAreas[0].AxisX.Minimum = series.Points[0].XValue;
            ESChart.ChartAreas[0].AxisX.Maximum = DateTime.FromOADate(series.Points[0].XValue).AddMinutes(TIME_WINDOW).ToOADate();
            ESChart.Invalidate(); //redraw

            // Update TextBox
            esTextBox.Text = newDataValue.ToString();
        }

        /// The AddNewPoint function is called for each series in the chart when
        /// new points need to be added.  The new point will be placed at specified
        /// X axis (Date/Time) position with a random Y value
        public double AddNewPoint(DateTime timeStamp, System.Windows.Forms.DataVisualization.Charting.Series ptSeries)
        {
            // Add new data point to its series.
            double newPoint = random.Next(5, 15);
            ptSeries.Points.AddXY(timeStamp.ToOADate(), newPoint);

            // Remove all points from the source series older than the time window
            double removeBefore = timeStamp.AddSeconds((double)(60) * (-TIME_WINDOW)).ToOADate();
            while (ptSeries.Points[0].XValue < removeBefore)
            {
                ptSeries.Points.RemoveAt(0);
            }

            return newPoint;
        }

        private void startTrending_Click(object sender, System.EventArgs e)
        {
            // Predefine the viewing area of the chart
            DateTime minValue = DateTime.Now;
            DateTime maxValue = minValue.AddSeconds(TIME_WINDOW*60);

            CLChart.ChartAreas[0].AxisX.Minimum = minValue.ToOADate();
            CLChart.ChartAreas[0].AxisX.Maximum = maxValue.ToOADate();
            ESChart.ChartAreas[0].AxisX.Minimum = minValue.ToOADate();
            ESChart.ChartAreas[0].AxisX.Maximum = maxValue.ToOADate();

            // Setup van chart1
            CLChart.Series.Clear();
            Series newSeries = new Series("Series1");
            newSeries.ChartType = SeriesChartType.Line;
            newSeries.BorderWidth = 2;
            newSeries.Color = Color.OrangeRed;
            newSeries.XValueType = ChartValueType.DateTime;
            CLChart.Series.Add(newSeries);

            // Setup van chart2
            ESChart.Series.Clear();
            Series newSeries2 = new Series("Series1");
            newSeries2.ChartType = SeriesChartType.Line;
            newSeries2.BorderWidth = 2;
            newSeries2.Color = Color.Blue;
            newSeries2.XValueType = ChartValueType.DateTime;
            ESChart.Series.Add(newSeries2);

            // Start de thread
            updateChartDataThread.Start();
        }
    }
}
