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
           _controller.startTrending_Click(null, null);
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            _controller.stopButtonClicked();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            _controller.pauseButtonClicked();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
       
    }
}
