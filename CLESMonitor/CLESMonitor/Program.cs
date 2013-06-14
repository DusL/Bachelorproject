using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CLESMonitor.Model.CL;
using CLESMonitor.Model.ES;
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

            FuzzyModelUtilityViewController esUtilityVC = new FuzzyModelUtilityViewController(hrSensor, gsrSensor);
            controller.esUtilityView = esUtilityVC.View;

            Application.Run(controller.View);
        }
    }
}
