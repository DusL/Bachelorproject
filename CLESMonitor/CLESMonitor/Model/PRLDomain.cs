using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace CLESMonitor.Model
{    
    /// <summary>
    /// This enum is merely used to improve the readability. All domains are represented by integers,
    /// all we need to know if the domains are different or not. The specific type of domain is irrelevent.
    /// </summary>
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
    public class PRLDomain : CTLDomain
    {
        
        /// <summary>
        /// Generates a CTLEvent instance from a InputElement instance
        /// </summary>
        /// <param name="inputElement">The InputElement to generate from</param>
        /// <returns>The generated CTLEvent</returns>
        public override CTLEvent generateEvent(InputElement inputElement)
        {
            CTLEvent ctlEvent = null;

            // The possible event types with their corresponding mo and lip values
            Tuple<string, double, int>[] validValues = //new Tuple<string, double, int>();
            {Tuple.Create("GESTRANDE_TREIN", 0.6, 2),
             Tuple.Create("GESTOORDE_WISSEL", 0.1, 1),
             Tuple.Create("VERTRAAGDE_TREIN", 0.8, 3)};

            if (inputElement != null && inputElement.identifier != null && inputElement.name != null)
            {
                foreach(var values in validValues)
                {
                    if (values.Item1.Equals(inputElement.name))
                    {
                        ctlEvent = new CTLEvent(inputElement.identifier, inputElement.name, values.Item2, values.Item3);
                    }
                }
            }

            return ctlEvent;
        }

        /// <summary>
        /// Generates a CTLTask instance from a InputElement instance
        /// </summary>
        /// <param name="inputElement">The InputElement to generate from</param>
        /// <returns>The generated CTLTask</returns>
        public override CTLTask generateTask(InputElement inputElement)
        {
            CTLTask ctlTask = null;

            if (inputElement != null && inputElement.identifier != null && inputElement.name != null)
            {
                //Console.WriteLine(inputElement.name);
                if (inputElement.name.Equals("ARI_UIT"))
                {
                    ctlTask = new CTLTask(inputElement.identifier, "ARI_UIT", inputElement.secondaryIndentifier);
                    ctlTask.description = "ARI uitschakelen voor geselecteerde planregels";
                    ctlTask.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface });
                }
                else if (inputElement.name.Equals("ARI_IN"))
                {
                    ctlTask = new CTLTask(inputElement.identifier, "ARI_IN", inputElement.secondaryIndentifier);
                    ctlTask.description = "ARI inschakelen voor geselecteerde planregels";
                    ctlTask.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface });
                }
                else if (inputElement.name.Equals("VIND_TREIN"))
                {
                    ctlTask = new CTLTask(inputElement.identifier, "VIND_TREIN", inputElement.secondaryIndentifier);
                    ctlTask.description = "Vind planregel met specifiek treinnummer";
                    ctlTask.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface });
                }
                else if (inputElement.name.Equals("SELECTEER_REGEL"))
                {
                    ctlTask = new CTLTask(inputElement.identifier, "SELECTEER_REGEL", inputElement.secondaryIndentifier);
                    ctlTask.description = "Selecteer planregel";
                    ctlTask.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface });
                }
                else if (inputElement.name.Equals("DESELECTEER_REGEL"))
                {
                    ctlTask = new CTLTask(inputElement.identifier, "DESELECTEER_REGEL", inputElement.secondaryIndentifier);
                    ctlTask.description = "De-selecteer planregels";
                    ctlTask.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface });
                }
                else if (inputElement.name.Equals("REGEL_IN_MUTATIESCHERM"))
                {
                    ctlTask = new CTLTask(inputElement.identifier, "REGEL_IN_MUTATIESCHERM", inputElement.secondaryIndentifier);
                    ctlTask.description = "Planregel in mutatiescherm plaatsen";
                    ctlTask.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface });
                }
                else if (inputElement.name.Equals("MUTEER_REGEL"))
                {
                    ctlTask = new CTLTask(inputElement.identifier, "MUTEER_REGEL", inputElement.secondaryIndentifier);
                    ctlTask.description = "Planregel muteren";
                    ctlTask.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface });
                }
                else if (inputElement.name.Equals("REGEL_TERUG"))
                {
                    ctlTask = new CTLTask(inputElement.identifier, "REGEL_TERUG", inputElement.secondaryIndentifier);
                    ctlTask.description = "Planregel terug plaatsen";
                    ctlTask.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface });
                }
                else if (inputElement.name.Equals("HAND_VERWERK_REGEL"))
                {
                    ctlTask = new CTLTask(inputElement.identifier, "HAND_VERWERK_REGEL", inputElement.secondaryIndentifier);
                    ctlTask.description = "planregel handmatig verwerken";
                    ctlTask.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface });
                }
                else if (inputElement.name.Equals("VERWERK_VERT_REGELS"))
                {
                    ctlTask = new CTLTask(inputElement.identifier, "VERWERK_VERT_REGELS", inputElement.secondaryIndentifier);
                    ctlTask.description = "Vertraging verwerken voor geselecteerde planregels";
                    ctlTask.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface });
                }
                else if (inputElement.name.Equals("KWIT_VERT_REGELS"))
                {
                    ctlTask = new CTLTask(inputElement.identifier, "KWIT_VERT_REGELS", inputElement.secondaryIndentifier);
                    ctlTask.description = "Vertraging kwiteren voor geselecteerde planregels";
                    ctlTask.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface });
                }
                else if (inputElement.name.Equals("VERWERK_VERT_TREIN"))
                {
                    ctlTask = new CTLTask(inputElement.identifier, "VERWERK_VERT_TREIN", inputElement.secondaryIndentifier);
                    ctlTask.description = "Vertraging verwerken voor trein";
                    ctlTask.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface });
                }
                else if (inputElement.name.Equals("VERWERK_VERT_REGELS"))
                {
                    ctlTask = new CTLTask(inputElement.identifier, "VERWERK_VERT_REGELS", inputElement.secondaryIndentifier);
                    ctlTask.description = "Vertraging verwerken voor geselecteerde planregels";
                    ctlTask.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface });
                }
                else if (inputElement.name.Equals("LASTGEVING"))
                {
                    ctlTask = new CTLTask(inputElement.identifier, "LASTGEVING", inputElement.secondaryIndentifier);
                    ctlTask.description = "Lastgeving";
                    ctlTask.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainExternalContact });
                }
                else if (inputElement.name.Equals("HERROEP_SEIN"))
                {
                    ctlTask = new CTLTask(inputElement.identifier, "HERROEP_SEIN", inputElement.secondaryIndentifier);
                    ctlTask.description = "Sein herroepen";
                    ctlTask.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface });
                }
                else if (inputElement.name.Equals("COMMUNICATIE"))
                {
                    ctlTask = new CTLTask(inputElement.identifier, "COMMUNICATIE", inputElement.secondaryIndentifier);
                    ctlTask.description = "Communicatie met andere processleiders en externe partijen";
                    ctlTask.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainExternalContact });
                }

            }
            return ctlTask;
        }


        /// <summary>
        /// Create a list of CTLEvents from a list of InputElements
        /// </summary>
        /// <param name="InputElements">The list to generate from</param>
        /// <param name="sessionTime">The current session time</param>
        /// <returns>A list of CTLEvents</returns>
        public override List<CTLEvent> generateEvents(List<InputElement> InputElements, TimeSpan sessionTime)
        {
            // Add all CTLEvent objects to a list
            List<CTLEvent> events = new List<CTLEvent>();

            foreach (InputElement InputElement in InputElements)
            {
                CTLEvent ctlEvent = generateEvent(InputElement);
                if (ctlEvent != null)
                {
                    ctlEvent.startTime = sessionTime;
                    events.Add(ctlEvent);
                }
                else 
                {
                    //TODO: Hier iets van een log bericht zodat de gebruiker ook weet dat er iets mis is?
                }
                
                
            }

            return events;
        }

        /// <summary>
        /// Create a list of CTLTasks from a list of InputElements
        /// </summary>
        /// <param name="InputElements">The list to generate from</param>
        /// <param name="sessionTime">The current session time</param>
        /// <returns>A list of CTLTasks</returns>
        public override List<CTLTask> generateTasks(List<InputElement> InputElements, TimeSpan sessionTime)
        {
            // Add all CTLTask objects to a list
            List<CTLTask> tasks = new List<CTLTask>();

            foreach (InputElement InputElement in InputElements)
            {
                CTLTask task = generateTask(InputElement);
                if (task != null)
                {
                    task.startTime = sessionTime;
                    tasks.Add(task);
                }
                else
                {
                    //TODO: Hier iets van een log bericht zodat de gebruiker ook weet dat er iets mis is?
                }

            }

            return tasks;
        }
    }
}
