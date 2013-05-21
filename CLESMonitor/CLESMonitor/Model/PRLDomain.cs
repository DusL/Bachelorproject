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

            CTLTask task = new CTLTask("ARI_UIT");
            task.description = "ARI uitschakelen voor geselecteerde planregels";
            task.moValue = 0;
            task.lipValue = 0;
            task.domains =  new InformationDomain[]{InformationDomain.InformationDomainUnknown};
            taskArray[0] = task;

            task = new CTLTask("ARI_IN");
            task.description = "ARI inschakelen voor geselecteerde planregels";
            task.moValue = 0;
            task.lipValue = 0;
            task.domains = new InformationDomain[] {InformationDomain.InformationDomainUnknown};
            taskArray[0] = task;

            task = new CTLTask("VIND_TREIN");
            task.description = "Vind planregel met specifiek treinnummer";
            task.moValue = 0;
            task.lipValue = 0;
            task.domains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            taskArray[0] = task;

            task = new CTLTask("SELECTEER_REGEL");
            task.description = "Selecteer planregel";
            task.moValue = 0;
            task.lipValue = 0;
            task.domains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            taskArray[0] = task;

            task = new CTLTask("REGEL_IN_MUTATIESCHERM");
            task.description = "Planregel in mutatiescherm plaatsen";
            task.moValue = 0;
            task.lipValue = 0;
            task.domains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            taskArray[0] = task;

            task = new CTLTask("MUTEER_REGEL");
            task.description = "Planregel muteren";
            task.moValue = 0;
            task.lipValue = 0;
            task.domains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            taskArray[0] = task;

            task = new CTLTask("REGEL_TERUG");
            task.description = "Planregel terug plaatsen";
            task.moValue = 0;
            task.lipValue = 0;
            task.domains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            taskArray[0] = task;

            task = new CTLTask("HAND_VERWERK_REGEL");
            task.description = "planregel handmatig verwerken";
            task.moValue = 0;
            task.lipValue = 0;
            task.domains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            taskArray[0] = task;

            task = new CTLTask("VERWERK_VERT_REGELS");
            task.description = "Vertraging verwerken voor geselecteerde planregels";
            task.moValue = 0;
            task.lipValue = 0;
            task.domains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            taskArray[0] = task;

            task = new CTLTask("KWIT_VERT_REGELS");
            task.description = "Vertraging kwiteren voor geselecteerde planregels";
            task.moValue = 0;
            task.lipValue = 0;
            task.domains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            taskArray[0] = task;

            task = new CTLTask("VERWERK_VERT_TREIN");
            task.description = "Vertraging verwerken voor trein";
            task.moValue = 0;
            task.lipValue = 0;
            task.domains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            taskArray[0] = task;

            task = new CTLTask("VERWERK_VERT_REGELS");
            task.description = "Vertraging verwerken voor geselecteerde planregels";
            task.moValue = 0;
            task.lipValue = 0;
            task.domains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            taskArray[0] = task;

            task = new CTLTask("LASTGEVING");
            task.description = "Lastgeving";
            task.moValue = 0;
            task.lipValue = 0;
            task.domains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            taskArray[0] = task;

            task = new CTLTask("HERROEP_SEIN");
            task.description = "Sein herroepen";
            task.moValue = 0;
            task.lipValue = 0;
            task.domains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            taskArray[0] = task;

            task = new CTLTask("COMMUNICATIE");
            task.description = "Communicatie met andere processleiders en externe partijen";
            task.moValue = 0;
            task.lipValue = 0;
            task.domains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
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
