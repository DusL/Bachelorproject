using CLESMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLESMonitor.Model.CL
{
    public abstract class CTLDomain
    {
        /// <summary>
        /// Generates an event from an element read from an input source
        /// </summary>
        /// <param name="InputElement">An input element representing an event</param>
        /// <returns>A CTLEvent</returns>
        public abstract CTLEvent generateEvent(InputElement InputElement);

        /// <summary>
        /// Generates an event from an element read from an input source
        /// </summary>
        /// <param name="InputElement">An input element representing a task</param>
        /// <returns>A CTLTask</returns>
        public abstract CTLTask generateTask(InputElement InputElement);
    }
}
