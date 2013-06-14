using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLESMonitor.View;
using CLESMonitor.Model.ES;
using System.Windows.Forms;

namespace CLESMonitor.Controller
{
    public class FuzzyModelUtilityViewController
    {
        public FuzzyModelUtilityView View { get; private set; }

        HRSensor hrSensor;
        GSRSensor gsrSensor;

        // Outlets
        private TrackBar hrTrackbar;
        private Label hrValueLabel;
        private Button hrPlusButton;
        private Button hrMinusButton;
        private TrackBar gsrTrackbar;
        private Label gsrValueLabel;

        public FuzzyModelUtilityViewController(HRSensor hrSensor, GSRSensor gsrSensor)
        {
            this.View = new FuzzyModelUtilityView(this);
            this.hrSensor = hrSensor;
            this.gsrSensor = gsrSensor;

            setupOutlets();

            hrValueLabel.Text = hrTrackbar.Value.ToString();
            gsrValueLabel.Text = gsrTrackbar.Value.ToString();
        }

        private void setupOutlets()
        {
            hrTrackbar = View.hrTrackbar;
            hrValueLabel = View.hrValueLabel;
            hrPlusButton = View.hrPlusButton;
            hrMinusButton = View.hrMinusButton;
            gsrTrackbar = View.gsrTrackBar;
            gsrValueLabel = View.gsrValueLabel;
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
                    hrSensor.type = HRSensorType.ManualInput;
                    hrTrackbar.Enabled = true;
                    hrMinusButton.Enabled = true;
                    hrPlusButton.Enabled = true;
                }
                else if (radioButton.Name.Equals("hrSensorTypeRadioButton2"))
                {
                    hrSensor.type = HRSensorType.BluetoothZephyr;
                    hrTrackbar.Enabled = false;
                    hrMinusButton.Enabled = false;
                    hrPlusButton.Enabled = false;
                }
            }
        }

        // FIXME: bounds checking!

        public void increaseHRValueInManualContext()
        {
            hrTrackbar.Value = hrTrackbar.Value + 10;
            hrValueLabel.Text = hrTrackbar.Value.ToString();

            // Pass along simulated sensor-data
            if (hrSensor.type == HRSensorType.ManualInput)
            {
                hrSensor.sensorValue = hrTrackbar.Value;
            }
        }

        // FIXME: bounds checking!

        public void decreaseHRValueInManualContext()
        {
            hrTrackbar.Value = hrTrackbar.Value - 10;
            hrValueLabel.Text = hrTrackbar.Value.ToString();

            // Pass along simulated sensor-data
            if (hrSensor.type == HRSensorType.ManualInput)
            {
                hrSensor.sensorValue = hrTrackbar.Value;
            }
        }

        public void HRValueChangedInManualContext(object sender)
        {
            TrackBar trackBar = (TrackBar)sender;
            hrValueLabel.Text = trackBar.Value.ToString();

            // Pass along simulated sensor-data
            if (hrSensor.type == HRSensorType.ManualInput)
            {
                hrSensor.sensorValue = hrTrackbar.Value;
            }
        }

        // FIXME: bounds checking!

        public void increaseGSRValueInManualContext()
        {
            gsrTrackbar.Value = gsrTrackbar.Value + 10;
            gsrValueLabel.Text = gsrTrackbar.Value.ToString();

            // Pass along simulated sensor-data
            if (gsrSensor.type == GSRSensorType.ManualInput)
            {
                gsrSensor.sensorValue = gsrTrackbar.Value;
            }
        }

        // FIXME: bounds checking!

        public void decreaseGSRValueInManualContext()
        {
            gsrTrackbar.Value = gsrTrackbar.Value - 10;
            gsrValueLabel.Text = gsrTrackbar.Value.ToString();

            // Pass along simulated sensor-data
            if (gsrSensor.type == GSRSensorType.ManualInput)
            {
                gsrSensor.sensorValue = gsrTrackbar.Value;
            }
        }

        public void GSRValueChangedInManualContext(object sender)
        {
            TrackBar trackBar = (TrackBar)sender;
            gsrValueLabel.Text = trackBar.Value.ToString();

            // Pass along simulated sensor-data
            if (gsrSensor.type == GSRSensorType.ManualInput)
            {
                gsrSensor.sensorValue = gsrTrackbar.Value;
            }
        }
    }
}
