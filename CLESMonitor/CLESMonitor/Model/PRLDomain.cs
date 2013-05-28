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
        /// <summary>
        /// Gets the task by means of its string identifier
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
       public CTLTask getTaskByIdentifier(string identifier)
        {
             CTLTask task;

            if (identifier.Equals("ARI_UIT"))
            {
                task = new CTLTask("ARI_UIT");
                task.description = "ARI uitschakelen voor geselecteerde planregels";
                task.moValue = 0;
                task.lipValue = 0;
                task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            }
            else if (identifier.Equals("ARI_IN"))
            {
                task = new CTLTask("ARI_IN");
                task.description = "ARI inschakelen voor geselecteerde planregels";
                task.moValue = 0;
                task.lipValue = 0;
                task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            }
            else if (identifier.Equals("VIND_TREIN"))
            {
                task = new CTLTask("VIND_TREIN");
                task.description = "Vind planregel met specifiek treinnummer";
                task.moValue = 0;
                task.lipValue = 0;
                task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            }
            else if (identifier.Equals("SELECTEER_REGEL"))
            {
                task = new CTLTask("SELECTEER_REGEL");
                task.description = "Selecteer planregel";
                task.moValue = 0;
                task.lipValue = 0;
                task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            }
            else if (identifier.Equals("DESELECTEER_REGEL"))
            {
                task = new CTLTask("DESELECTEER_REGEL");
                task.description = "De-selecteer planregels";
                task.moValue = 0;
                task.lipValue = 0;
                task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            }
            else if (identifier.Equals("REGEL_IN_MUTATIESCHERM"))
            {
                task = new CTLTask("REGEL_IN_MUTATIESCHERM");
                task.description = "Planregel in mutatiescherm plaatsen";
                task.moValue = 0;
                task.lipValue = 0;
                task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            }
            else if (identifier.Equals("MUTEER_REGEL"))
            {
                task = new CTLTask("MUTEER_REGEL");
                task.description = "Planregel muteren";
                task.moValue = 0;
                task.lipValue = 0;
                task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            }
            else if (identifier.Equals("REGEL_TERUG"))
            {
                task = new CTLTask("REGEL_TERUG");
                task.description = "Planregel terug plaatsen";
                task.moValue = 0;
                task.lipValue = 0;
                task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            }
            else if (identifier.Equals("HAND_VERWERK_REGEL"))
            {
                task = new CTLTask("HAND_VERWERK_REGEL");
                task.description = "planregel handmatig verwerken";
                task.moValue = 0;
                task.lipValue = 0;
                task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            }
            else if (identifier.Equals("VERWERK_VERT_REGELS"))
            {
                task = new CTLTask("VERWERK_VERT_REGELS");
                task.description = "Vertraging verwerken voor geselecteerde planregels";
                task.moValue = 0;
                task.lipValue = 0;
                task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            }
            else if (identifier.Equals("KWIT_VERT_REGELS"))
            {
                task = new CTLTask("KWIT_VERT_REGELS");
                task.description = "Vertraging kwiteren voor geselecteerde planregels";
                task.moValue = 0;
                task.lipValue = 0;
                task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            }
            else if (identifier.Equals("VERWERK_VERT_TREIN"))
            {
                task = new CTLTask("VERWERK_VERT_TREIN");
                task.description = "Vertraging verwerken voor trein";
                task.moValue = 0;
                task.lipValue = 0;
                task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            }
            else if (identifier.Equals("VERWERK_VERT_REGELS"))
            {
                task = new CTLTask("VERWERK_VERT_REGELS");
                task.description = "Vertraging verwerken voor geselecteerde planregels";
                task.moValue = 0;
                task.lipValue = 0;
                task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            }
            else if (identifier.Equals("LASTGEVING"))
            {
                task = new CTLTask("LASTGEVING");
                task.description = "Lastgeving";
                task.moValue = 0;
                task.lipValue = 0;
                task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            }
            else if (identifier.Equals("HERROEP_SEIN"))
            {
                task = new CTLTask("HERROEP_SEIN");
                task.description = "Sein herroepen";
                task.moValue = 0;
                task.lipValue = 0;
                task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            }
            else if (identifier.Equals("COMMUNICATIE"))
            {
                task = new CTLTask("COMMUNICATIE");
                task.description = "Communicatie met andere processleiders en externe partijen";
                task.moValue = 0;
                task.lipValue = 0;
                task.informationDomains = new InformationDomain[] { InformationDomain.InformationDomainUnknown };
            }
            else
            {
                throw new Exception("Er is een identifier gevonden die niet bestaat. Controleer het XML bestand en probeer opnieuw");
                //task = null;
            }
            return task;
        }       
    }
}
