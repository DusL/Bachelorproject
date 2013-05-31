﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLESMonitor.Model
{
    public class CTLEvent
    {
        private string identifier;
        public string type;
        public TimeSpan startTime { get; set; }
        public TimeSpan endTime { get; set; }

        public CTLEvent(string _identifier, string _type)
        {
            identifier = _identifier;
            type = _type;
        }

        public string getIdentifier()
        {
            return identifier;
        }

        /// <summary>
        /// ToString method
        /// </summary>
        /// <returns>A string-representation of the CTLEvent object</returns>
        public override string ToString()
        {
            return String.Format("Event: Identifier = {0}, Type = {1}, startTime = {2}, endTime = {3}", identifier, type, startTime.TotalSeconds, endTime.TotalSeconds);
        }
    }
}
