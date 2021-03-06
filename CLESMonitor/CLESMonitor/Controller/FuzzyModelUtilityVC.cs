﻿using CLESMonitor.Model.ES;
using CLESMonitor.View;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        System.Windows.Forms.Timer timer; //TODO: dit vervangen met System.Threading.Timer
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

            // Setup the combobox
            string[] portNames = SerialPort.GetPortNames();
            View.HRSensorComboBox.Items.AddRange(portNames);
            View.HRSensorComboBox.SelectedIndex = 0;


            subscribe();

            

            hrValueLabel.Text = View.hrTrackBar.Value.ToString();
            gsrValueLabel.Text = View.gsrTrackBar.Value.ToString();

            // Set the default sensor types
            fuzzyModel.hrSensor.type = HRSensor.Type.ManualInput;
            fuzzyModel.gsrSensor.type = GSRSensor.Type.ManualInput;

            // Makes sure that from the start a value is present (not being 0).
            fuzzyModel.hrSensor.sensorValue = View.hrTrackBar.Value;
            fuzzyModel.gsrSensor.sensorValue = View.gsrTrackBar.Value;

            sensorController = null;

            TimerCallback timerCallback = sensorTimerCallback;
            sensorTimer = new Timer(timerCallback, null, 1000, 1000);

            this.currentState = State.Uncalibrated;
        }

        // 
        private void subscribe()
        {
            View.calibrateClickedHandler += new FuzzyModelUtilityView.EventHandler(calibrateButtonClicked);
            View.sensorButtonClickedHandler += new FuzzyModelUtilityView.EventHandler(sensorButtonClicked);
            View.formClosingHandler += new FuzzyModelUtilityView.EventHandler(closeForm);
            View.HRSensorComboBoxSelectedIndexChangedHandler += new FuzzyModelUtilityView.EventHandler(HRSensorComboBoxChanged);

            View.HRValueChangeByButtonHandler += new FuzzyModelUtilityView.EventHandlerWithArgs(HRValueChangeByButton);
            View.HRTrackbarScrollHandler += new FuzzyModelUtilityView.EventHandlerWithArgs(HRValueChangedInManualContext);
            View.HRSensorTypeChangedHandler += new FuzzyModelUtilityView.EventHandlerWithArgs(HRSensorTypeChanged);

            View.GSRValueChangeByButtonHandler += new FuzzyModelUtilityView.EventHandlerWithArgs(GSRValueChangeByButton);
            View.GSRTrackbarScrollHandler += new FuzzyModelUtilityView.EventHandlerWithArgs(GSRValueChangedInManualContext);
        }

        /// <summary>
        /// Always show the current states.
        /// </summary>
        private void sensorTimerCallback(Object stateInfo)
        {
            try
            {
                View.Invoke((Action)(() =>
                {
                    try
                    {
                        View.hrValueLabel.Text = fuzzyModel.hrSensor.sensorValue.ToString();
                        View.hrMeanLabel.Text = Math.Round(fuzzyModel.HRMean).ToString();
                        View.hrSDLabel.Text = Math.Round(fuzzyModel.HRsd).ToString();
                        View.hrMinLabel.Text = Math.Round(fuzzyModel.HRMin).ToString();
                        View.hrMaxLabel.Text = Math.Round(fuzzyModel.HRMax).ToString();

                        View.gsrValueLabel.Text = fuzzyModel.gsrSensor.sensorValue.ToString();
                        View.gsrMeanLabel.Text = Math.Round(fuzzyModel.GSRMean).ToString();
                        View.gsrSDLabel.Text = Math.Round(fuzzyModel.GSRsd).ToString();
                        View.gsrMinLabel.Text = Math.Round(fuzzyModel.GSRMin).ToString();
                        View.gsrMaxLabel.Text = Math.Round(fuzzyModel.GSRMax).ToString();
                    }
                    catch (ObjectDisposedException exception) { Console.WriteLine(exception.ToString()); }
                }));
            }
            catch (ObjectDisposedException exception) { Console.WriteLine(exception.ToString()); }
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
                timeSpanCounter = new TimeSpan(0, 10, 0);
                timer = new System.Windows.Forms.Timer();
                timer.Tick += new EventHandler(timer_Tick);
                timer.Interval = 1000; // 1 second
                timer.Start();
                calibrateButton.Text = timeSpanCounter.ToString(@"%h\:mm\:ss");

                // Pass any manual input values for the first time,
                // they are passed on change.
                if (fuzzyModel.hrSensor.type == HRSensor.Type.ManualInput)
                {
                    fuzzyModel.hrSensor.sensorValue = View.hrTrackBar.Value;
                }
                if (fuzzyModel.gsrSensor.type == GSRSensor.Type.ManualInput)
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
                calibrateButton.Text = "Opnieuw kalibreren";

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
            timeSpanCounter = timeSpanCounter - new TimeSpan(0, 0, 1);

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
        public void HRSensorTypeChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            if (radioButton.Checked)
            {
                if (radioButton == View.hrSensorTypeRadioButton1)
                {
                    HRSensor hrSensor = new HRSensor(HRSensor.Type.ManualInput);
                    hrSensor.sensorValue = View.hrTrackBar.Value;
                    fuzzyModel.reloadWithHRSensor(hrSensor);

                    View.hrTrackBar.Enabled = true;
                    hrMinusButton.Enabled = true;
                    hrPlusButton.Enabled = true;
                    View.HRSensorComboBox.Enabled = false;
                }
                else if (radioButton == View.hrSensorTypeRadioButton2)
                {
                    HRSensor hrSensor = new HRSensor(HRSensor.Type.BluetoothZephyr);
                    if (View.HRSensorComboBox.SelectedIndex != 0)
                    {
                        hrSensor.serialPortName = View.HRSensorComboBox.Text;
                    }
                    fuzzyModel.reloadWithHRSensor(hrSensor);

                    View.hrTrackBar.Enabled = false;
                    hrMinusButton.Enabled = false;
                    hrPlusButton.Enabled = false;
                    View.HRSensorComboBox.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Action method for when the value for the dropdown box with available
        /// serialports for the bluetooth heart rate monitor is changed.
        /// </summary>
        public void HRSensorComboBoxChanged()
        {
            // Index 0 contains the filler text
            if (View.HRSensorComboBox.SelectedIndex != 0)
            {
                HRSensor hrSensor = new HRSensor(HRSensor.Type.BluetoothZephyr);
                hrSensor.serialPortName = View.HRSensorComboBox.Text;
                fuzzyModel.reloadWithHRSensor(hrSensor); //replace the sensor in FuzzyModel
            }
        }

        /// <summary>
        /// Event method for a change in HRvalue by pressing the plus or minus button
        /// </summary>
        public void HRValueChangeByButton(object sender, EventArgs e)
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
            if (fuzzyModel.hrSensor.type == HRSensor.Type.ManualInput)
            {
                fuzzyModel.hrSensor.sensorValue = View.hrTrackBar.Value;
            }
        }

        public void HRValueChangedInManualContext(object sender, EventArgs e)
        {
            TrackBar trackBar = (TrackBar)sender;
            hrValueLabel.Text = trackBar.Value.ToString();

            // Pass along simulated sensor-data
            if (fuzzyModel.hrSensor.type == HRSensor.Type.ManualInput)
            {
                fuzzyModel.hrSensor.sensorValue = View.hrTrackBar.Value;
            }
        }

        /// <summary>
        /// Event method for a change in HRvalue by pressing the plus or minus button
        /// </summary>
        public void GSRValueChangeByButton(object sender, EventArgs e)
        {
            Button buttonPressed = (Button)sender;
            if (buttonPressed.Equals(View.gsrPlusButton))
            {
                if (View.gsrTrackBar.Maximum > View.gsrTrackBar.Value)
                {
                    View.gsrTrackBar.Value = View.gsrTrackBar.Value + 1;
                }
            }
            else if (buttonPressed.Equals(View.gsrMinusButton))
            {
                if (View.gsrTrackBar.Minimum < View.gsrTrackBar.Value)
                {
                    View.gsrTrackBar.Value = View.gsrTrackBar.Value - 1;
                }
            }

            View.gsrValueLabel.Text = View.gsrTrackBar.Value.ToString();
           
            // Pass along simulated sensor-data
            if (fuzzyModel.gsrSensor.type == GSRSensor.Type.ManualInput)
            {
                fuzzyModel.gsrSensor.sensorValue = View.gsrTrackBar.Value;
            }
        }

        public void GSRValueChangedInManualContext(object sender, EventArgs e)
        {
            TrackBar trackBar = (TrackBar)sender;
            gsrValueLabel.Text = trackBar.Value.ToString();

            // Pass along simulated sensor-data
            if (fuzzyModel.gsrSensor.type == GSRSensor.Type.ManualInput)
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
