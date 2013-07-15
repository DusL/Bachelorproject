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
        public delegate void EventHandler();
        public delegate void EventHandlerWithArguments(object sender, EventArgs e);
        public event EventHandler startButtonClickedHandler;
        public event EventHandler stopButtonClickedHandler;
        public event EventHandler clearListButtonClickedHandler;
        public event EventHandler startToolStripMenuItemClickedHandler;
        public event EventHandler stopToolStripMenuItemClickedHandler;
        public event EventHandler quitToolStripMenuItemClickedHandler;
        public event EventHandlerWithArguments formKeyDownHandler;

        /// <summary>
        /// Constructor method.
        /// </summary>
        public MainView()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (startButtonClickedHandler != null)
            {
                startButtonClickedHandler();
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            if (stopButtonClickedHandler != null)
            {
                stopButtonClickedHandler();
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (startToolStripMenuItemClickedHandler != null)
            {
                startToolStripMenuItemClickedHandler();
            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (stopToolStripMenuItemClickedHandler != null)
            {
                stopToolStripMenuItemClickedHandler();
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (quitToolStripMenuItemClickedHandler != null)
            {
                quitToolStripMenuItemClickedHandler();
            }
        }

        private void CLESMonitorViewForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (formKeyDownHandler != null)
            {
                formKeyDownHandler(sender, e);
            }
        }
    }
}
