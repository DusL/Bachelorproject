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

    /// <summary>
    /// This is a support class for CTLModel. It can generate the appropriate classes for
    /// CTLModel from the classes it receives from XMLTaskParser. The class is domain-specific,
    /// this implementation will generate data in the context of cognitive load for
    /// railroad-operators.
    /// </summary>
    public class PRLDomain
    {
        /// <summary>
        /// Generates a CTLEvent instance from a ParsedEvent instance
        /// </summary>
        /// <param name="parsedEvent">The ParsedEvent to generate from</param>
        /// <returns>The generated CTLEvent</returns>
        public CTLEvent generateEvent(ParsedEvent parsedEvent)
        {
            CTLEvent ctlEvent = null;

            List<string> validTypes = new List<string>();
            validTypes.Add("GESTRANDE_TREIN");
            validTypes.Add("GESTOORDE_WISSEL");
            validTypes.Add("VERTRAAGDE_TREIN");

            if (parsedEvent != null && parsedEvent.type != null && parsedEvent.identifier != null)
            {
                if (validTypes.Contains(parsedEvent.type))
                {
                    ctlEvent = new CTLEvent(parsedEvent.identifier, parsedEvent.type);
                }
            }

            return ctlEvent;
        }

        /// <summary>
        /// Generates a CTLTask instance from a ParsedTask instance
        /// </summary>
        /// <param name="parsedTask">The ParsedTask to generate from</param>
        /// <returns>The generated CTLTask</returns>
        public CTLTask generateTask(ParsedTask parsedTask)
        {
            CTLTask ctlTask = null;

            if (parsedTask != null && parsedTask.type != null && parsedTask.identifier != null)
            {
                if (parsedTask.type.Equals("ARI_UIT"))
                {
                    ctlTask = new CTLTask(parsedTask.identifier, "ARI_UIT");
                    ctlTask.description = "ARI uitschakelen voor geselecteerde planregels";
                    ctlTask.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("ARI_IN"))
                {
                    ctlTask = new CTLTask(parsedTask.identifier, "ARI_IN");
                    ctlTask.description = "ARI inschakelen voor geselecteerde planregels";
                    ctlTask.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("VIND_TREIN"))
                {
                    ctlTask = new CTLTask(parsedTask.identifier, "VIND_TREIN");
                    ctlTask.description = "Vind planregel met specifiek treinnummer";
                    ctlTask.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("SELECTEER_REGEL"))
                {
                    ctlTask = new CTLTask(parsedTask.identifier, "SELECTEER_REGEL");
                    ctlTask.description = "Selecteer planregel";
                    ctlTask.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("DESELECTEER_REGEL"))
                {
                    ctlTask = new CTLTask(parsedTask.identifier, "DESELECTEER_REGEL");
                    ctlTask.description = "De-selecteer planregels";
                    ctlTask.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("REGEL_IN_MUTATIESCHERM"))
                {
                    ctlTask = new CTLTask(parsedTask.identifier, "REGEL_IN_MUTATIESCHERM");
                    ctlTask.description = "Planregel in mutatiescherm plaatsen";
                    ctlTask.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("MUTEER_REGEL"))
                {
                    ctlTask = new CTLTask(parsedTask.identifier, "MUTEER_REGEL");
                    ctlTask.description = "Planregel muteren";
                    ctlTask.moValue = 0;
                    ctlTask.lipValue = 0;
                    ctlTask.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("REGEL_TERUG"))
                {
                    ctlTask = new CTLTask(parsedTask.identifier, "REGEL_TERUG");
                    ctlTask.description = "Planregel terug plaatsen";
                    ctlTask.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("HAND_VERWERK_REGEL"))
                {
                    ctlTask = new CTLTask(parsedTask.identifier, "HAND_VERWERK_REGEL");
                    ctlTask.description = "planregel handmatig verwerken";
                    ctlTask.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("VERWERK_VERT_REGELS"))
                {
                    ctlTask = new CTLTask(parsedTask.identifier, "VERWERK_VERT_REGELS");
                    ctlTask.description = "Vertraging verwerken voor geselecteerde planregels";
                    ctlTask.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("KWIT_VERT_REGELS"))
                {
                    ctlTask = new CTLTask(parsedTask.identifier, "KWIT_VERT_REGELS");
                    ctlTask.description = "Vertraging kwiteren voor geselecteerde planregels";
                    ctlTask.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("VERWERK_VERT_TREIN"))
                {
                    ctlTask = new CTLTask(parsedTask.identifier, "VERWERK_VERT_TREIN");
                    ctlTask.description = "Vertraging verwerken voor trein";
                    ctlTask.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("VERWERK_VERT_REGELS"))
                {
                    ctlTask = new CTLTask(parsedTask.identifier, "VERWERK_VERT_REGELS");
                    ctlTask.description = "Vertraging verwerken voor geselecteerde planregels";
                    ctlTask.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("LASTGEVING"))
                {
                    ctlTask = new CTLTask(parsedTask.identifier, "LASTGEVING");
                    ctlTask.description = "Lastgeving";
                    ctlTask.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("HERROEP_SEIN"))
                {
                    ctlTask = new CTLTask(parsedTask.identifier, "HERROEP_SEIN");
                    ctlTask.description = "Sein herroepen";
                    ctlTask.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
                else if (parsedTask.type.Equals("COMMUNICATIE"))
                {
                    ctlTask = new CTLTask(parsedTask.identifier, "COMMUNICATIE");
                    ctlTask.description = "Communicatie met andere processleiders en externe partijen";
                    ctlTask.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
                }
            }

            return ctlTask;
        }

        /// <summary>
        /// Create a list of CTLEvents from a list of ParsedEvents
        /// </summary>
        /// <param name="parsedTasks">The list to generate from</param>
        /// <param name="sessionTime">The current session time</param>
        /// <returns>A list of CTLEvents</returns>
        public List<CTLEvent> generateEvents(List<ParsedEvent> parsedEvents, TimeSpan sessionTime)
        {
            // Add all CTLEvent objects to a list
            List<CTLEvent> events = new List<CTLEvent>();

            foreach (ParsedEvent parsedEvent in parsedEvents)
            {
                CTLEvent ctlEvent = generateEvent(parsedEvent);
                ctlEvent.startTime = sessionTime;
                events.Add(ctlEvent);
            }

            return events;
        }

        /// <summary>
        /// Create a list of CTLTasks from a list of ParsedTasks
        /// </summary>
        /// <param name="parsedTasks">The list to generate from</param>
        /// <param name="sessionTime">The current session time</param>
        /// <returns>A list of CTLTasks</returns>
        public List<CTLTask> generateTasks(List<ParsedTask> parsedTasks, TimeSpan sessionTime)
        {
            // Add all CTLTask objects to a list
            List<CTLTask> tasks = new List<CTLTask>();

            foreach (ParsedTask parsedTask in parsedTasks)
            {
                CTLTask task = generateTask(parsedTask);
                task.startTime = sessionTime;
                tasks.Add(task);
            }

            return tasks;
        }
    }
}
