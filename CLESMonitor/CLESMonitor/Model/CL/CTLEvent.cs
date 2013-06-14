using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLESMonitor.Model.CL
{
    public class CTLEvent
    {
        public string identifier {get; private set;}
        public string name { get; private set; }
        public TimeSpan startTime { get; set; }
        public TimeSpan endTime { get; set; }
        public double moValue { get; set; }
        public int lipValue { get; set; }

        public CTLEvent(string _identifier, string _name, double _moValue, int _lipValue)
        {
            identifier = _identifier;
            name = _name;
            moValue = _moValue;
            lipValue = _lipValue;
        }


        /// <summary>
        /// ToString method
        /// </summary>
        /// <returns>A string-representation of the CTLEvent object</returns>
        public override string ToString()
        {
            return String.Format("Event: Identifier={0}, Type={1}, startTime={2}, endTime={3}, moValue={4}, lipValue={5}", identifier, name, startTime.TotalSeconds, endTime.TotalSeconds, moValue, lipValue);
        }
    }
}
