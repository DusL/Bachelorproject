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

            // TODO: kunnen deze sensoren (nu) niet gewoon in fuzzymodel worden geinstantieerd?
            HRSensor hrSensor = new HRSensor();
            GSRSensor gsrSensor = new GSRSensor();
            FuzzyModel fuzzyModel = new FuzzyModel(hrSensor, gsrSensor);

            // Main viewcontroller setup
            var controller = new CLESMonitorViewController(ctlModel,fuzzyModel);

            // Cognitive Load setup
            CTLModelUtilityViewController ctlUtilityVC = new CTLModelUtilityViewController(ctlModel, parser);
            controller.clUtilityView = ctlUtilityVC.View;

            // Emotional State setup
            FuzzyModelUtilityViewController esUtilityVC = new FuzzyModelUtilityViewController(fuzzyModel);
            controller.esUtilityView = esUtilityVC.View;

            Application.Run(controller.View);
        }
    }
}
