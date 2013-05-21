using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLESMonitor.Model
{
    //TODO: onderstaande is domein-specifiek, verkeerde plek!
    public enum InformationDomain
    {
        InformationDomainUnknown,
        InformationDomainUsingInterface,
        InformationDomainExternalContact
    }
    public class PRLDomain
    {
        CTLTask[] taskArray;// = new CTLTask[7];
        public Array GenerateTasks()
        {
            taskArray = new CTLTask[2];

            CTLTask task = new CTLTask("ARI uitschakelen voor geselecteerde planregels");
            task.moValue = 0;
            task.lipValue = 0;
            task.domains =  new InformationDomain[]{InformationDomain.InformationDomainUnknown};
            taskArray[0] = task;

            task = new CTLTask("ARI inschakelen voor geselecteerde planregels");
            task.moValue = 0;
            task.lipValue = 0;
            task.domains = new InformationDomain[] {InformationDomain.InformationDomainUnknown};
            taskArray[0] = task;

            return taskArray;
        }

        public void addNewTask(CTLTask task)
        { 
            Array.Resize(ref taskArray, taskArray.Length +1);
            taskArray[taskArray.Length] = task;
        }
        
    }
}
