using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace CLESMonitor.Model
{    
    public enum InformationDomain
    {
        InformationDomainUnknown,
        InformationDomainUsingInterface,
        InformationDomainExternalContact
    }

    public class PRLDomain
    {
        public CTLEvent generateEvent(ParsedEvent parsedEvent)
        {
            CTLEvent ctlEvent = null;

            if(parsedEvent.type != null && parsedEvent.identifier != null)
            {
                if (parsedEvent.type.Equals("GESTRANDE_TREIN"))
                {
                    ctlEvent = new CTLEvent(parsedEvent.identifier, parsedEvent.type);
                }
                else if (parsedEvent.type.Equals("GESTOORDE_WISSEL"))
                {
                    ctlEvent = new CTLEvent(parsedEvent.identifier, parsedEvent.type);
                }
                else if (parsedEvent.type.Equals("VERTRAAGDE_TREIN"))
                {
                    ctlEvent = new CTLEvent(parsedEvent.identifier, parsedEvent.type);
                }
            }

            return ctlEvent;
        }

        /// <summary>
        /// Gets the task by means of its string identifier
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public CTLTask generateTask(ParsedTask parsedTask)
        {
            CTLTask task = null;
            if (parsedTask.type != null && parsedTask.identifier != null)
            {


                if (parsedTask.type.Equals("ARI_UIT"))
                {
                    task = new CTLTask(parsedTask.identifier, "ARI_UIT");
                    task.description = "ARI uitschakelen voor geselecteerde planregels";
                    task.moValue = 0;
                    task.lipValue = 0;
                    task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("ARI_IN"))
                {
                    task = new CTLTask(parsedTask.identifier, "ARI_IN");
                    task.description = "ARI inschakelen voor geselecteerde planregels";
                    task.moValue = 0;
                    task.lipValue = 0;
                    task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("VIND_TREIN"))
                {
                    task = new CTLTask(parsedTask.identifier, "VIND_TREIN");
                    task.description = "Vind planregel met specifiek treinnummer";
                    task.moValue = 0;
                    task.lipValue = 0;
                    task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("SELECTEER_REGEL"))
                {
                    task = new CTLTask(parsedTask.identifier, "SELECTEER_REGEL");
                    task.description = "Selecteer planregel";
                    task.moValue = 0;
                    task.lipValue = 0;
                    task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("DESELECTEER_REGEL"))
                {
                    task = new CTLTask(parsedTask.identifier, "DESELECTEER_REGEL");
                    task.description = "De-selecteer planregels";
                    task.moValue = 0;
                    task.lipValue = 0;
                    task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("REGEL_IN_MUTATIESCHERM"))
                {
                    task = new CTLTask(parsedTask.identifier, "REGEL_IN_MUTATIESCHERM");
                    task.description = "Planregel in mutatiescherm plaatsen";
                    task.moValue = 0;
                    task.lipValue = 0;
                    task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("MUTEER_REGEL"))
                {
                    task = new CTLTask(parsedTask.identifier, "MUTEER_REGEL");
                    task.description = "Planregel muteren";
                    task.moValue = 0;
                    task.lipValue = 0;
                    task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("REGEL_TERUG"))
                {
                    task = new CTLTask(parsedTask.identifier, "REGEL_TERUG");
                    task.description = "Planregel terug plaatsen";
                    task.moValue = 0;
                    task.lipValue = 0;
                    task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("HAND_VERWERK_REGEL"))
                {
                    task = new CTLTask(parsedTask.identifier, "HAND_VERWERK_REGEL");
                    task.description = "planregel handmatig verwerken";
                    task.moValue = 0;
                    task.lipValue = 0;
                    task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("VERWERK_VERT_REGELS"))
                {
                    task = new CTLTask(parsedTask.identifier, "VERWERK_VERT_REGELS");
                    task.description = "Vertraging verwerken voor geselecteerde planregels";
                    task.moValue = 0;
                    task.lipValue = 0;
                    task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("KWIT_VERT_REGELS"))
                {
                    task = new CTLTask(parsedTask.identifier, "KWIT_VERT_REGELS");
                    task.description = "Vertraging kwiteren voor geselecteerde planregels";
                    task.moValue = 0;
                    task.lipValue = 0;
                    task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("VERWERK_VERT_TREIN"))
                {
                    task = new CTLTask(parsedTask.identifier, "VERWERK_VERT_TREIN");
                    task.description = "Vertraging verwerken voor trein";
                    task.moValue = 0;
                    task.lipValue = 0;
                    task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("VERWERK_VERT_REGELS"))
                {
                    task = new CTLTask(parsedTask.identifier, "VERWERK_VERT_REGELS");
                    task.description = "Vertraging verwerken voor geselecteerde planregels";
                    task.moValue = 0;
                    task.lipValue = 0;
                    task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("LASTGEVING"))
                {
                    task = new CTLTask(parsedTask.identifier, "LASTGEVING");
                    task.description = "Lastgeving";
                    task.moValue = 0;
                    task.lipValue = 0;
                    task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("HERROEP_SEIN"))
                {
                    task = new CTLTask(parsedTask.identifier, "HERROEP_SEIN");
                    task.description = "Sein herroepen";
                    task.moValue = 0;
                    task.lipValue = 0;
                    task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("COMMUNICATIE"))
                {
                    task = new CTLTask(parsedTask.identifier, "COMMUNICATIE");
                    task.description = "Communicatie met andere processleiders en externe partijen";
                    task.moValue = 0;
                    task.lipValue = 0;
                    task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
            }
            return task;
        }       
    }
}
