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
using System.Threading;
using System.Globalization;

namespace CLESMonitor.View
{
    public partial class MainView : Form
    {
        private MainViewController controller;

        public MainView(MainViewController controller)
        {
            // TODO: Meertalig maken??
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("nl-NL");
            this.controller = controller;
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            controller.startButtonClicked(null, null);
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            controller.stopButtonClicked();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.startButtonClicked(null, null);
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.stopButtonClicked();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.quit();
        }

        private void CLESMonitorViewForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.F4)
            {
                controller.quit();
            }
            
        }

    }
}
