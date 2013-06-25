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
        private FuzzyModelUtilityVC controller;

        /// <summary>
        /// The Constructor method.
        /// </summary>
        /// <param name="controller">A viewController for this view.</param>
        public FuzzyModelUtilityView(FuzzyModelUtilityVC controller)
        {
            InitializeComponent();
            this.controller = controller;
        }

        private void hrPlusButton_Click(object sender, EventArgs e)
        {
            controller.HRValueChangeByButton(sender);
        }

        private void hrMinusButton_Click(object sender, EventArgs e)
        {
            controller.HRValueChangeByButton(sender);
        }

        private void hrTrackbar_Scroll(object sender, EventArgs e)
        {
            controller.HRValueChangedInManualContext(sender);
        }

        private void hrSensorTypeRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            controller.hrSensorTypeChanged();
        }

        private void hrSensorTypeRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            controller.hrSensorTypeChanged();
        }

        private void gsrPlusButton_Click(object sender, EventArgs e)
        {
            controller.GSRValueChangeByButton(sender);
        }

        private void gsrMinusButton_Click(object sender, EventArgs e)
        {
            controller.GSRValueChangeByButton(sender);
        }

        private void gsrTrackBar_Scroll(object sender, EventArgs e)
        {
            controller.GSRValueChangedInManualContext(sender);
        }

        private void calibrateButton_Click(object sender, EventArgs e)
        {
            controller.calibrateButtonClicked();
        }

        private void sensorButton_Click(object sender, EventArgs e)
        {
            controller.sensorButtonClicked();
        }

        private void FuzzyModelUtilityView_FormClosing(object sender, FormClosingEventArgs e)
        {
            controller.closeForm();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.hrSensorComboBoxChanged();
        }


    }
}
