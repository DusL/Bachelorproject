using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            
            ///<summary>
            ///Hiermee wordt in het tekst veld telkens bovenaan een regel toegevoegd. 
            ///</summary>
            for (int i = 0; i<=10; i++)                    
            {
                //TODO: Dit moet nog naar de controller
                controller.View.richTextBox1.Select(0, 0);
                controller.View.richTextBox1.SelectedText = i + " Deze bla staat nu boven aan" + "\n";
            }

            Application.Run(controller.View);
        }
    }
}
