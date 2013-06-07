using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using CLESMonitor.Model;
using CLESMonitor.View;

namespace CLESMonitor.Controller
{
    public class SensorViewController
    {
        private const double TIME_WINDOW = 1; //in minutes       
        Timer sensorTimer;

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

        // Outlets
        Chart HRChart;
        Chart GSRChart;

        public delegate void UpdateDelegate();

        // Sets the Form of the controller
        public SensorViewForm View { get; private set; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hrSensor"></param>
        /// <param name="gsrSensor"></param>
        public SensorViewController(HRSensor hrSensor, GSRSensor gsrSensor)
        {
            View = new SensorViewForm(this);

            this.hrSensor = hrSensor;
            this.gsrSensor = gsrSensor;

            HRChart = this.View.HRChart;
            GSRChart = this.View.GSRChart;

            // Setup of chart1
            HRChart.Series.Clear();
            Series newSeries = new Series("Series1");
            newSeries.ChartType = SeriesChartType.Spline;
            newSeries.BorderWidth = 2;
            newSeries.Color = Color.OrangeRed;
            newSeries.XValueType = ChartValueType.DateTime;
            HRChart.Series.Add(newSeries);

            // Setup of chart2
            GSRChart.Series.Clear();
            Series newSeries2 = new Series("Series1");
            newSeries2.ChartType = SeriesChartType.Spline;
            newSeries2.BorderWidth = 2;
            newSeries2.Color = Color.Blue;
            newSeries2.XValueType = ChartValueType.DateTime;
            GSRChart.Series.Add(newSeries2);
        }

        // 
        public void stopRunLoop() 
        {
            sensorTimer.Dispose();
        }

        private void sensorTimerCallback(Object stateInfo)
        {         
            HRChart.Invoke(new UpdateDelegate(UpdateHRChart));
            GSRChart.Invoke(new UpdateDelegate(UpdateGSRChart));
        }

        private void UpdateHRChart()
        {
            double newDataPoint = hrSensor.sensorValue;
            this.UpdateChartData(HRChart, newDataPoint, DateTime.Now);
        }

        private void UpdateGSRChart()
        {
            double newDataPoint2 = gsrSensor.sensorValue;
            this.UpdateChartData(GSRChart, newDataPoint2, DateTime.Now);
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

        public void formClosing()
        {
            this.stopRunLoop();
        }

        public void shown()
        {
            TimerCallback timerCallback = sensorTimerCallback;
            sensorTimer = new Timer(timerCallback, null, 0, 1000);
        }
    }
}
