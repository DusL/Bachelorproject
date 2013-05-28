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

            XMLFileTaskParser parser = new XMLFileTaskParser();
            CTLModel ctlModel = new CTLModel(parser);

            HRSensor hrSensor = new HRSensor();
            GSRSensor gsrSensor = new GSRSensor();
            FuzzyModel fuzzyModel = new FuzzyModel(hrSensor, gsrSensor);

            var controller = new ViewController(ctlModel,fuzzyModel);

            controller.hrSensor = hrSensor;
            controller.gsrSensor = gsrSensor;

            Application.Run(controller.View);
        }
    }
}
