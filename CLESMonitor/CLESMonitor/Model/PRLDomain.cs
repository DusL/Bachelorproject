using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLESMonitor.Model
{
    class PRLDomain
    {
        public Array GenerateTasks()
        {
            CTLTask[] taskArray = new CTLTask[2];

            CTLTask task = new CTLTask("ARI uitschakelen voor geselecteerde planregels");
            task.moValue = 0;
            task.lipValue = 0;
            task.informationDomain = InformationDomain.InformationDomainUnknown;
            taskArray[0] = task;

            task = new CTLTask("ARI inschakelen voor geselecteerde planregels");
            task.moValue = 0;
            task.lipValue = 0;
            task.informationDomain = InformationDomain.InformationDomainUnknown;
            taskArray[0] = task;

            return taskArray;
        }
    }
}
