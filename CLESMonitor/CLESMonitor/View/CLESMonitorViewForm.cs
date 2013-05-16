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
        public CLESMonitorViewForm()
        {
            //_controller = controller;
            InitializeComponent();
        }

        private void CLChart_Click(object sender, EventArgs e)
        {

        }
    }
}
