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
    public partial class SensorViewForm : Form
    {
        private SensorViewController _controller;

        public SensorViewForm(SensorViewController controller)
        {
            _controller = controller;
            InitializeComponent();
        }

        private void ESChart_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void SensorViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _controller.formClosing();
        }

        private void SensorViewForm_Shown(object sender, EventArgs e)
        {
            _controller.shown();
        }


    }
}
