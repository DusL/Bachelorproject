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
    public partial class SensorView : Form
    {
        public delegate void EventHandler();
        public event EventHandler formClosingHandler;
        public event EventHandler sensorViewFormShownHandler;

        public SensorView(SensorViewController controller)
        {
            InitializeComponent();
        }


        private void SensorViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (formClosingHandler != null)
            {
                formClosingHandler();
            }
        }

        private void SensorViewForm_Shown(object sender, EventArgs e)
        {
            if (sensorViewFormShownHandler != null)
            {
                sensorViewFormShownHandler();
            }
        }


    }
}
