using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMLScenarioGenerator
{
    public partial class Form1 : Form
    {
        private ViewController _controller;

        public Form1(ViewController controller)
        {
            InitializeComponent();
            _controller = controller;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _controller.openScenarioFileDialog();
        }
    }
}
