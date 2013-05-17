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
    

    public class CTLTask
    {
        public double moValue; //mental occupancy
        public int lipValue; //level of information processing
        public InformationDomain informationDomain;
        public double duration; //in seconden

        private string name;

        /// <summary>
        /// Constructor methode
        /// </summary>
        /// <param name="_name"></param>
        public CTLTask(string _name)
        {
            name = _name;
        }

        /// <summary>
        /// ToString methode
        /// </summary>
        /// <returns>Een string-representatie van het CTLTask object</returns>
        public string toString()
        {
            return String.Format("Name = {0}", name);
        }

        public int getLip()
        {
            return this.lipValue;
        }

        public double getMO()
        {
            return this.moValue;
        }
        public InformationDomain getInfoDomain()
        {
            return this.informationDomain;
        }
        public double getDuration()
        {
            return this.duration;
        }

    }
}
