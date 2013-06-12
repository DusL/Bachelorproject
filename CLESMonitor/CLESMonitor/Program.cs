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

            XMLParser parser = new XMLParser();
            PRLDomain prlDomain = new PRLDomain();
            CTLModel ctlModel = new CTLModel(parser, prlDomain);

            HRSensor hrSensor = new HRSensor();
            GSRSensor gsrSensor = new GSRSensor();
            FuzzyModel fuzzyModel = new FuzzyModel(hrSensor, gsrSensor);

            var controller = new CLESMonitorViewController(ctlModel,fuzzyModel);
            controller.parser = parser;
            controller.hrSensor = hrSensor;
            controller.gsrSensor = gsrSensor;

            Application.Run(controller.View);
        }
    }
}
