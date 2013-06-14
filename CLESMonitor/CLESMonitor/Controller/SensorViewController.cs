using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using CLESMonitor.Model.ES;
using CLESMonitor.View;

namespace CLESMonitor.Controller
{
    /// <summary>
    /// The controller of the form containing twe graphs representing the input data of the sensors
    /// </summary>
    public class SensorViewController
    {
        private const double TIME_WINDOW = 1; //in minutes       
        Timer sensorTimer;

        private TimeSpan currentSessionTime;
        private DateTime startTime;

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

        // Outlets
        Chart HRChart;
        Chart GSRChart;

        // Needed to update the graphs on this thread
        public delegate void UpdateDelegate();

        // Sets the Form of the controller
        public SensorViewForm View { get; private set; }


        /// <summary>
        /// Constructor: Creates the Form, sets the inputsensors and contains the setup of both charts
        /// </summary>
        /// <param name="hrSensor">The hart rate input that is set manualy or through a sensor</param>
        /// <param name="gsrSensor">The GSR input that is set manualy or through a sensor</param>
        public SensorViewController(HRSensor hrSensor, GSRSensor gsrSensor)
        {
            View = new SensorViewForm(this);
            startTime = DateTime.Now;

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
            newSeries.XValueType = ChartValueType.Double;
            HRChart.Series.Add(newSeries);

            // Setup of chart2
            GSRChart.Series.Clear();
            Series newSeries2 = new Series("Series1");
            newSeries2.ChartType = SeriesChartType.Spline;
            newSeries2.BorderWidth = 2;
            newSeries2.Color = Color.Blue;
            newSeries2.XValueType = ChartValueType.Double;
            GSRChart.Series.Add(newSeries2);
        }

        /// <summary>
        /// When the form has been fully loaded for the first time after the controller is created, the Timer is created
        /// </summary>
        public void shown()
        {
            TimerCallback timerCallback = sensorTimerCallback;
            sensorTimer = new Timer(timerCallback, null, 0, 1000);
        }

        /// <summary>
        /// Invokes the updateChart methods every second (callback time = 1 sec.)
        /// </summary>
        /// <param name="stateInfo">The current TimerCallback</param>
        private void sensorTimerCallback(Object stateInfo)
        {
            if (sensorTimer != null)
            {
                currentSessionTime = DateTime.Now - startTime;

                HRChart.Invoke(new UpdateDelegate(UpdateHRChart));
                GSRChart.Invoke(new UpdateDelegate(UpdateGSRChart));
            }
            
        }

        /// <summary>
        /// Retrieves the current hr data from the sensor and updates the corresponding chart
        /// </summary>
        private void UpdateHRChart()
        {
            double newDataPoint = hrSensor.sensorValue;
            this.UpdateChartData(HRChart, newDataPoint, currentSessionTime); //DateTime.Now
        }

        /// <summary>
        /// Retrieves the current gsr data from the sensor and updates the corresponding chart
        /// </summary>
        private void UpdateGSRChart()
        {
            double newDataPoint2 = gsrSensor.sensorValue;
            this.UpdateChartData(GSRChart, newDataPoint2, currentSessionTime);//DateTime.Now
        }

        /// <summary>
        /// Pre: graph contains a series named "Series1"
        /// Adjusts "Series1" 
        /// </summary>
        /// <param name="chart">The chart that needs updating</param>
        /// <param name="newDataPoint">The new point that needs to be added to the chart</param>
        private void UpdateChartData(Chart chart, double newDataPoint, TimeSpan currentSessionTime)
        {
            // Update chart
            Series series = chart.Series["Series1"];
            int point = series.Points.AddXY(Math.Floor(currentSessionTime.TotalSeconds), newDataPoint);
            int pointsCounter = series.Points.Count;

            chart.ChartAreas[0].AxisX.Minimum = series.Points[0].XValue;
            chart.ChartAreas[0].AxisX.Maximum = series.Points[0].XValue + ((double)(60) * (TIME_WINDOW));
            chart.Invalidate(); //redraw

            // Remove old datapoints
            double removeBefore = Math.Floor(currentSessionTime.TotalSeconds - ((60) * (TIME_WINDOW)));
            while (series.Points[0].XValue < removeBefore)
            {
                series.Points.RemoveAt(0);
            }
        }

        /// <summary>
        /// When the SensorViewForm closses, the TimerCallback needs to be disposed
        /// </summary>
        public void formClosing()
        {
            //Console.WriteLine("Disposing of the sensorTimer");
            sensorTimer.Dispose();
        }

       
    }
}
