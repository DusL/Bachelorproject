using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CLESMonitor;

namespace CLESMonitor.Model
{
    //public abstract enum InformationDomain { };
    

    public abstract class CTLDomain
    {
        

        public abstract CTLEvent generateEvent(ParsedEvent parsedEvent);

        public abstract CTLTask generateTask(ParsedTask parsedTask);

        public abstract List<CTLEvent> generateEvents(List<ParsedEvent> parsedEvents, TimeSpan sessionTime);

        public abstract List<CTLTask> generateTasks(List<ParsedTask> parsedTasks, TimeSpan sessionTime);
    }
}
