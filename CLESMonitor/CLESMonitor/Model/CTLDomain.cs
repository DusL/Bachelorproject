using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CLESMonitor;

namespace CLESMonitor.Model
{
    public abstract class CTLDomain
    {
        public abstract CTLEvent generateEvent(InputElement InputElement);

        public abstract CTLTask generateTask(InputElement InputElement);

    }
}
