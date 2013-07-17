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
    public partial class CTLModelUtilityView : Form
    {
        public delegate void EventHandler();
        public event EventHandler openScenarioFileButtonClickedHandler;
        public event EventHandler CTLModelUtilityViewShownHandler;
        public event EventHandler clearListButtonClickedHandler;

        /// <summary>
        /// Constructor method.
        /// </summary>
        public CTLModelUtilityView()
        {
            InitializeComponent();
        }

        private void CTLModelUtilityView_Shown(object sender, EventArgs e)
        {
            if (CTLModelUtilityViewShownHandler != null)
            {
                CTLModelUtilityViewShownHandler();
            }
        }

        private void openScenarioFileButton_Click(object sender, EventArgs e)
        {
            if (openScenarioFileButtonClickedHandler != null)
            {
                openScenarioFileButtonClickedHandler();
            }
        }

        private void clearListButton_Click(object sender, EventArgs e)
        {
            if (clearListButtonClickedHandler != null)
            {
                clearListButtonClickedHandler();
            }
        }
    }
}
