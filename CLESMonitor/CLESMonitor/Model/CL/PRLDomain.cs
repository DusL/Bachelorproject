using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CLESMonitor.Model.CL
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
        #region Abstract CTLDomain implementation

        /// <summary>
        /// Generates a CTLEvent instance from a InputElement instance
        /// </summary>
        /// <param name="inputElement">The InputElement to generate from</param>
        /// <returns>The generated CTLEvent</returns>
        public override CTLEvent generateEvent(InputElement inputElement)
        {
            CTLEvent ctlEvent = null;

            // The possible event types with their corresponding mo and lip values
            Tuple<string, double, int>[] validValues =
            {Tuple.Create("GESTRANDE_TREIN", 0.6, 2),
             Tuple.Create("GESTOORDE_WISSEL", 0.8, 3),
             Tuple.Create("VERTRAAGDE_TREIN_OK", 0.2, 1),
             Tuple.Create("VERTRAAGDE_TREIN_PROBLEEM", 0.3, 1)};

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

            List<Tuple<string, string, List<int>>> availableTaskData = PRLDomain.availableTaskData();

            foreach (Tuple<string, string, List<int>> taskData in availableTaskData)
            {
                if (taskData.Item1.Equals(inputElement.name))
                {
                    ctlTask = new CTLTask(inputElement.identifier, inputElement.name, inputElement.secondaryIndentifier);
                    ctlTask.description = taskData.Item2;
                    ctlTask.informationDomains = taskData.Item3;
                    break;
                }
            }

            return ctlTask;
        }

        private static List<Tuple<string, string, List<int>>> availableTaskData()
        {
            List<Tuple<string, string, List<int>>> availableTaskData = new List<Tuple<string, string, List<int>>>();

            availableTaskData.Add(Tuple.Create(
                "ARI_UIT",
                "ARI uitschakelen voor geselecteerde planregels",
                new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface })
                ));
            availableTaskData.Add(Tuple.Create(
                "ARI_IN",
                "ARI inschakelen voor geselecteerde planregels",
                new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface })
                ));
            availableTaskData.Add(Tuple.Create(
                "VIND_TREIN",
                "Vind planregel met specifiek treinnummer",
                new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface })
                ));
            availableTaskData.Add(Tuple.Create(
                "SELECTEER_REGEL",
                "Selecteer planregel",
                new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface })
                ));
            availableTaskData.Add(Tuple.Create(
                "DESELECTEER_REGEL",
                "De-selecteer planregels",
                new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface })
                ));
            availableTaskData.Add(Tuple.Create(
                "REGEL_IN_MUTATIESCHERM",
                "Planregel in mutatiescherm plaatsen",
                new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface })
                ));
            availableTaskData.Add(Tuple.Create(
                "MUTEER_REGEL",
                "Planregel muteren",
                new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface })
                ));
            availableTaskData.Add(Tuple.Create(
                "REGEL_TERUG",
                "Planregel terug plaatsen",
                new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface })
                ));
            availableTaskData.Add(Tuple.Create(
                "HAND_VERWERK_REGEL",
                "Planregel handmatig verwerken",
                new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface })
                ));
            availableTaskData.Add(Tuple.Create(
                "VERWERK_VERT_REGELS",
                "Vertraging verwerken voor geselecteerde planregels",
                new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface })
                ));
            availableTaskData.Add(Tuple.Create(
                "KWIT_VERT_REGELS",
                "Vertraging kwiteren voor geselecteerde planregels",
                new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface })
                ));
            availableTaskData.Add(Tuple.Create(
                "VERWERK_VERT_TREIN",
                "Vertraging verwerken voor trein",
                new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface })
                ));
            availableTaskData.Add(Tuple.Create(
                "LASTGEVING",
                "Lastgeving afgeven",
                new List<int>(new int[] { (int)InformationDomain.InformationDomainExternalContact })
                ));
            availableTaskData.Add(Tuple.Create(
                "HERROEP_SEIN",
                "Sein herroepen",
                new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface })
                ));
            availableTaskData.Add(Tuple.Create(
                "COMMUNICATIE",
                "Communicatie met andere processleiders en externe partijen",
                new List<int>(new int[] { (int)InformationDomain.InformationDomainExternalContact })
                ));

            return availableTaskData;
        }

        #endregion
    }
}
