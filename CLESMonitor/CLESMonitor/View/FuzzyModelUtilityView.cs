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
        private FuzzyModelUtilityViewController controller;

        public FuzzyModelUtilityView(FuzzyModelUtilityViewController controller)
        {
            InitializeComponent();
            this.controller = controller;
        }

        private void hrPlusButton_Click(object sender, EventArgs e)
        {
            controller.increaseHRValueInManualContext();
        }

        private void hrMinusButton_Click(object sender, EventArgs e)
        {
            controller.decreaseHRValueInManualContext();
        }

        private void hrTrackbar_Scroll(object sender, EventArgs e)
        {
            controller.HRValueChangedInManualContext(sender);
        }

        private void hrSensorTypeRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            controller.hrSensorTypeChanged(sender);
        }

        private void hrSensorTypeRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            controller.hrSensorTypeChanged(sender);
        }

        private void gsrPlusButton_Click(object sender, EventArgs e)
        {
            controller.increaseGSRValueInManualContext();
        }

        private void gsrMinusButton_Click(object sender, EventArgs e)
        {
            controller.decreaseGSRValueInManualContext();
        }

        private void gsrTrackBar_Scroll(object sender, EventArgs e)
        {
            controller.GSRValueChangedInManualContext(sender);
        }
    }
}
