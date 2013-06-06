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
    public partial class CLESMonitorViewForm : Form
    {
        private ViewController _controller;

        public CLESMonitorViewForm(ViewController controller)
        {
            _controller = controller;
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            _controller.startButtonClicked(null, null);
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            _controller.stopButtonClicked();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            _controller.pauseButtonClicked();
        }

        private void hrTrackBar_Scroll(object sender, EventArgs e)
        {
            _controller.HRValueChangedInManualContext(sender);
        }

        private void hrMinusButton_Click(object sender, EventArgs e)
        {
            _controller.decreaseHRValueInManualContext();
        }

        private void hrPlusButton_Click(object sender, EventArgs e)
        {
            _controller.increaseHRValueInManualContext();
        }

        private void gsrTrackBar_Scroll(object sender, EventArgs e)
        {
            _controller.GSRValueChangedInManualContext(sender);
        }

        private void gsrMinusButton_Click(object sender, EventArgs e)
        {
            _controller.decreaseGSRValueInManualContext();
        }

        private void gsrPlusButton_Click(object sender, EventArgs e)
        {
            _controller.increaseGSRValueInManualContext();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _controller.openScenarioFileDialog();
        }

        private void hrSensorTypeRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            _controller.hrSensorTypeChanged(sender);
        }

        private void hrSensorTypeRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            _controller.hrSensorTypeChanged(sender);
        }

        private void calibrateButton_Click(object sender, EventArgs e)
        {
            _controller.calibrateButtonClicked();
        }
    }
}
