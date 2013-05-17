using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLESMonitor.Model;
using CLESMonitor.View;

namespace CLESMonitor.Controller
{
    class ViewController
    {
        public CLModel clModel;
        public ESModel esModel;
        private CLESMonitorViewForm _view;

        public CLESMonitorViewForm View
        {
            get
            {
                return _view;
            }
        }

        public ViewController()
        {
            _view = new CLESMonitorViewForm();    
        }   




    }
}
