using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLESMonitor.View;
using CLESMonitor.Model.ES;
using System.Windows.Forms;
using System.Threading;


using Timer = System.Threading.Timer;

namespace CLESMonitor.Controller
{
    public class FuzzyModelUtilityVC
    {
        /// <summary>ViewController states</summary>
        public enum State
        {
            Unknown,
            Uncalibrated,
            Calibrating,
            Calibrated
        }

        private const int LOOP_SLEEP_INTERVAL = 1000; //in milliseconds
        /// <summary>The view this viewcontroller manages</summary>
        public FuzzyModelUtilityView View { get; private set; }
        /// <summary>The current state of this viewcontroller</summary>
        public State currentState { get; private set; }
        /// <summary>The FuzzyModel that this utility-viewcontroller interacts with</summary>
        private FuzzyModel fuzzyModel;


        // TODO: waarom is dit een Forms timer?
        System.Windows.Forms.Timer timer;
        private TimeSpan timeSpanCounter;
        private SensorViewController sensorController;
        private Timer sensorTimer;

        // Outlets
        private Label hrValueLabel;
        private Button hrPlusButton;
        private Button hrMinusButton;
        private Label gsrValueLabel;
        private Button calibrateButton;


        /// <summary>
        /// The constructor method.
        /// </summary>
        /// <param name="fuzzyModel">A instance of the FuzzyModel class</param>
        public FuzzyModelUtilityVC(FuzzyModel fuzzyModel)
        {
            this.View = new FuzzyModelUtilityView(this);
            this.fuzzyModel = fuzzyModel;

            setupOutlets();

            hrValueLabel.Text = View.hrTrackBar.Value.ToString();
            gsrValueLabel.Text = View.gsrTrackBar.Value.ToString();

            // Set the default sensor types
            fuzzyModel.hrSensor.type = HRSensorType.ManualInput;
            fuzzyModel.gsrSensor.type = GSRSensorType.ManualInput;

            // Makes sure that from the start a vlue is present (not being 0).
            fuzzyModel.hrSensor.sensorValue = View.hrTrackBar.Value;
            fuzzyModel.gsrSensor.sensorValue = View.gsrTrackBar.Value;

            sensorController = null;

            TimerCallback timerCallback = sensorTimerCallback;
            sensorTimer = new Timer(timerCallback, null, 1000, 1000);

            this.currentState = State.Uncalibrated;
        }

        /// <summary>
        /// Always show the current states.
        /// </summary>
        private void sensorTimerCallback(Object stateInfo)
        {
            if (sensorTimer != null)
            {
                //TODO: Dit gaat nog dood

                View.Invoke((Action)(() =>
                {
                    View.hrLevelLabel.Text = fuzzyModel.hrLevel.ToString();
                    View.hrMeanLabel.Text = Math.Round(fuzzyModel.HRMean).ToString();
                    View.hrSDLabel.Text = Math.Round(fuzzyModel.HRsd).ToString();
                    View.hrMinLabel.Text = Math.Round(fuzzyModel.HRMin).ToString();
                    View.hrMaxLabel.Text = Math.Round(fuzzyModel.HRMax).ToString();
                }));

                View.Invoke((Action)(() =>
                {
                    View.gsrLevelLabel.Text = fuzzyModel.gsrLevel.ToString();
                    View.gsrMeanLabel.Text = Math.Round(fuzzyModel.GSRMean).ToString();
                    View.gsrSDLabel.Text = Math.Round(fuzzyModel.GSRsd).ToString();
                    View.gsrMinLabel.Text = Math.Round(fuzzyModel.GSRMin).ToString();
                    View.gsrMaxLabel.Text = Math.Round(fuzzyModel.GSRMax).ToString();
                }));
            }
        }

        private void setupOutlets()
        {
            hrValueLabel = View.hrValueLabel;
            hrPlusButton = View.hrPlusButton;
            hrMinusButton = View.hrMinusButton;
            gsrValueLabel = View.gsrValueLabel;
            calibrateButton = View.calibrateButton;
        }

        /// <summary>
        /// Action method when the calibrate button is clicked.
        /// </summary>
        public void calibrateButtonClicked()
        {
            // When the button is first pressed, show the sensorForm and start calabration.
            if (currentState == State.Calibrated)
            {
                // The method does not proceed as long as this messagebox is open.
                var result = MessageBox.Show("Weet u zeker dat u nog maal wilt kalibreren?", "Kalibratie", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    currentState = State.Uncalibrated;
                }
                
            }
            if (currentState == State.Uncalibrated)
            {
                // Show the form
                if (sensorController == null || sensorController.View.IsDisposed)
                {
                    sensorController = new SensorViewController(fuzzyModel.hrSensor, fuzzyModel.gsrSensor);
                }
                
                sensorController.View.Show(); 

                // Setup the countdown calibration timer 
                timeSpanCounter = new TimeSpan(0, 5, 0);
                timer = new System.Windows.Forms.Timer();
                timer.Tick += new EventHandler(timer_Tick);
                timer.Interval = 1000; // 1 second
                timer.Start();
                calibrateButton.Text = timeSpanCounter.ToString(@"%h\:mm\:ss");

                // Pass any manual input values for the first time,
                // they are passed on change.
                if (fuzzyModel.hrSensor.type == HRSensorType.ManualInput)
                {
                    fuzzyModel.hrSensor.sensorValue = View.hrTrackBar.Value;
                }
                if (fuzzyModel.gsrSensor.type == GSRSensorType.ManualInput)
                {
                    fuzzyModel.gsrSensor.sensorValue = View.gsrTrackBar.Value;
                }

                 // Start calibrating
                fuzzyModel.startCalibration();
                currentState = State.Calibrating;
                Console.WriteLine("Calibratie gestart");
            }
            // When the button is pressed for a second time, the calibration is stopped
            else if (currentState == State.Calibrating)
            {
                // Stop the countdown and set the form back to the original state
                timer.Stop();
                calibrateButton.Text = "Opnieuw calibreren";

                fuzzyModel.stopCalibration();
                currentState = State.Calibrated;
                Console.WriteLine("Calibratie gestopt");
            }
            
        }

        /// <summary>
        /// Reduces the counter by 1 second, each second. When the counter is 0, stop calibrating and stop the timer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            timeSpanCounter.Subtract(new TimeSpan(0, 0, 1));
            if (timeSpanCounter.TotalSeconds == 0)
            {
                timer.Stop();
                fuzzyModel.stopCalibration();
            }

            calibrateButton.Text = timeSpanCounter.ToString(@"%h\:mm\:ss");
        }

        /// <summary>
        /// Action when the heart rate sensor type is being changed
        /// </summary>
        public void hrSensorTypeChanged()
        {
            if (View.hrSensorTypeRadioButton1.Checked)
            {
                fuzzyModel.hrSensor.type = HRSensorType.ManualInput;
                View.hrTrackBar.Enabled = true;
                hrMinusButton.Enabled = true;
                hrPlusButton.Enabled = true;
            }

            if (View.hrSensorTypeRadioButton2.Checked)
            {
                fuzzyModel.hrSensor.type = HRSensorType.BluetoothZephyr;
                View.hrTrackBar.Enabled = false;
                hrMinusButton.Enabled = false;
                hrPlusButton.Enabled = false;
            }
        }

        /// <summary>
        /// Event method for a change in HRvalue by pressing the plus or minus button
        /// </summary>
        public void HRValueChangeByButton(object sender)
        {
            int changeValue = 1;
            Button buttonPressed = (Button)sender;
            if (buttonPressed.Equals(View.hrPlusButton))
            {
                if (View.hrTrackBar.Maximum > View.hrTrackBar.Value)
                {
                    View.hrTrackBar.Value = View.hrTrackBar.Value + changeValue;
                    View.hrValueLabel.Text = View.hrTrackBar.Value.ToString();
                }
            }
            else if (buttonPressed.Equals(hrMinusButton))
            {
                if (View.hrTrackBar.Minimum < View.hrTrackBar.Value)
                {
                    View.hrTrackBar.Value = View.hrTrackBar.Value - changeValue;
                    View.hrValueLabel.Text = View.hrTrackBar.Value.ToString();
                }
            }
            // Pass along simulated sensor-data
            if (fuzzyModel.hrSensor.type == HRSensorType.ManualInput)
            {
                fuzzyModel.hrSensor.sensorValue = View.hrTrackBar.Value;
            }
        }


        public void HRValueChangedInManualContext(object sender)
        {
            TrackBar trackBar = (TrackBar)sender;
            hrValueLabel.Text = trackBar.Value.ToString();

            // Pass along simulated sensor-data
            if (fuzzyModel.hrSensor.type == HRSensorType.ManualInput)
            {
                fuzzyModel.hrSensor.sensorValue = View.hrTrackBar.Value;
            }
        }
        /// <summary>
        /// Event method for a change in HRvalue by pressing the plus or minus button
        /// </summary>
        public void GSRValueChangeByButton(object sender)
        {
            Button buttonPressed = (Button)sender;
            if (buttonPressed.Equals(View.gsrPlusButton))
            {
                if (View.gsrTrackBar.Maximum > View.gsrTrackBar.Value)
                {
                    View.gsrTrackBar.Value = View.gsrTrackBar.Value + 1;
                    View.gsrValueLabel.Text = View.gsrTrackBar.Value.ToString();
                }
            }
            else if (buttonPressed.Equals(View.gsrMinusButton))
            {
                if (View.gsrTrackBar.Minimum < View.gsrTrackBar.Value)
                {
                    View.gsrTrackBar.Value = View.gsrTrackBar.Value - 1;
                    gsrValueLabel.Text = View.gsrTrackBar.Value.ToString();
                }
            }
           
            // Pass along simulated sensor-data
            if (fuzzyModel.gsrSensor.type == GSRSensorType.ManualInput)
            {
                fuzzyModel.gsrSensor.sensorValue = View.gsrTrackBar.Value;
            }
        }


        public void GSRValueChangedInManualContext(object sender)
        {
            TrackBar trackBar = (TrackBar)sender;
            gsrValueLabel.Text = trackBar.Value.ToString();

            // Pass along simulated sensor-data
            if (fuzzyModel.gsrSensor.type == GSRSensorType.ManualInput)
            {
                fuzzyModel.gsrSensor.sensorValue = View.gsrTrackBar.Value;
            }
        }

        /// <summary>
        /// Action Method: When the sensorButton is clicked, a SensorView is created and the from is shown
        /// </summary>
        public void sensorButtonClicked()
        {
            if (sensorController == null || sensorController.View.IsDisposed)
            {
                sensorController = new SensorViewController(fuzzyModel.hrSensor, fuzzyModel.gsrSensor);

                sensorController.View.Show();
            }
            
        }

        internal void closeForm()
        {
           View.Dispose();
        }
    }
}
