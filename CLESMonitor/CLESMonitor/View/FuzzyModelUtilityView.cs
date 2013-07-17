using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CLESMonitor.Controller;

namespace CLESMonitor.View
{
    public partial class FuzzyModelUtilityView : Form
    {
        public delegate void EventHandler();
        public delegate void EventHandlerWithArgs(object sender, EventArgs e);
        
        public event EventHandler calibrateClickedHandler;
        public event EventHandler sensorButtonClickedHandler;
        public event EventHandler formClosingHandler;
        public event EventHandler HRSensorComboBoxSelectedIndexChangedHandler;

        public event EventHandlerWithArgs HRValueChangeByButtonHandler;
        public event EventHandlerWithArgs HRTrackbarScrollHandler;
        public event EventHandlerWithArgs HRSensorTypeChangedHandler;

        public event EventHandlerWithArgs GSRValueChangeByButtonHandler;
        public event EventHandlerWithArgs GSRTrackbarScrollHandler;

        /// <summary>
        /// The Constructor method.
        /// </summary>
        /// <param name="controller">A viewController for this view.</param>
        public FuzzyModelUtilityView(FuzzyModelUtilityVC controller)
        {
            InitializeComponent();
        }

        private void hrPlusButton_Click(object sender, EventArgs e)
        {
            if (HRValueChangeByButtonHandler != null)
            {
                HRValueChangeByButtonHandler(sender, e);
            }
        }

        private void hrMinusButton_Click(object sender, EventArgs e)
        {
            if (HRValueChangeByButtonHandler != null)
            {
                HRValueChangeByButtonHandler(sender, e);
            }
        }

        private void hrTrackbar_Scroll(object sender, EventArgs e)
        {
            if (HRTrackbarScrollHandler != null)
            {
                HRTrackbarScrollHandler(sender, e);
            }
        }

        private void hrSensorTypeRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (HRSensorTypeChangedHandler != null)
            {
                HRSensorTypeChangedHandler(sender, e);
            }
        }

        private void hrSensorTypeRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (HRSensorTypeChangedHandler != null)
            {
                HRSensorTypeChangedHandler(sender, e);
            }
        }

        private void gsrPlusButton_Click(object sender, EventArgs e)
        {
            if (GSRValueChangeByButtonHandler != null)
            {
                GSRValueChangeByButtonHandler(sender, e);
            }
        }

        private void gsrMinusButton_Click(object sender, EventArgs e)
        {
           if (GSRValueChangeByButtonHandler != null)
           {
               GSRValueChangeByButtonHandler(sender, e);
           }
        }

        private void gsrTrackBar_Scroll(object sender, EventArgs e)
        {
            if (GSRTrackbarScrollHandler != null)
            {
                GSRTrackbarScrollHandler(sender, e);
            }
        }

        private void calibrateButton_Click(object sender, EventArgs e)
        {
            if (calibrateClickedHandler != null)
            {
                calibrateClickedHandler();
            }
        }

        private void sensorButton_Click(object sender, EventArgs e)
        {
            if (sensorButtonClickedHandler != null)
            {
                sensorButtonClickedHandler();
            }
        }

        private void HRSensorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HRSensorComboBoxSelectedIndexChangedHandler != null)
            {
                HRSensorComboBoxSelectedIndexChangedHandler();
            }
        }

        private void FuzzyModelUtilityView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (formClosingHandler != null)
            {
                formClosingHandler();
            }
        }


    }
}
