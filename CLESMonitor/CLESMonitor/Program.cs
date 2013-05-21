using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CLESMonitor.Model;
using CLESMonitor.Controller;

namespace CLESMonitor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var controller = new ViewController();
            controller.clModel = new CTLModel();
            controller.esModel = new FuzzyModel();
            
            Application.Run(controller.View);
        }
    }
}
