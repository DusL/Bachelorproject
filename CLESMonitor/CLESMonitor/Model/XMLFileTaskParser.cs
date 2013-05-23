using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using System.Threading;
using System.Reflection;
using CLESMonitor.Controller;

//using System.IO.Packaging;

namespace CLESMonitor.Model
{
    public class XMLFileTaskParser
    {
        private XmlNodeList seconds;
        /// <summary>
        /// 1 scenario is 1 file
        /// 
        /// </summary>
        public XMLFileTaskParser(String path)
        {

            XmlDocument xmlDoc = new XmlDocument(); //* create an xml document object.
            xmlDoc.Load(path); //* load the XML document from the specified file

            // Haal iedere scenarion in de file binnen.
            XmlNodeList scenario = xmlDoc.GetElementsByTagName("scenario");
            //Console.WriteLine(scenario[0].Name);
           // Console.WriteLine(second[0].Name);

            //Haal iedere sconde die in het scenario gedefinieerd is binnen
            seconds = xmlDoc.GetElementsByTagName("second");
            
        }

        public XmlNodeList getActionsForSecond(int currentSecond)
        {
            return this.seconds[currentSecond].ChildNodes;
        }
        /// <summary>
        /// Bepaald welke events voorkomen in een XmlNodeList. 
        /// Hiermee kan bepaald worden met welke events iets aan de hand is 
        /// in een bepaalde seconde.
        /// </summary>
        /// <param name="actions"></param>
        /// <returns>Een XmlNode array van events</returns>
        public XmlNode[] getEvents(XmlNodeList actions)
        {
            XmlNode[] events = new XmlNode[0];
            foreach (XmlNode node in actions)
            {
                if (node.GetType().Equals("event"))
                {
                    events[events.Count()] = node;
                }
            }
            return events;
        }
        /// <summary>
        /// Bepaald welke tasks voorkomen in een XmlNodeList.
        /// Hiermee kan bepaald worden in welke seconde een task stopt en start.
        /// </summary>
        /// <param name="actions"></param>
        /// <returns>Een XmlNode array met taken</returns>
        public XmlNode[] getTasks(XmlNodeList actions)
        {
            XmlNode[] tasks = new XmlNode[0];
            foreach (XmlNode node in actions)
            {
                if (node.GetType().Equals("task"))
                {
                    tasks[tasks.Count()] = node;
                }
            }
            return tasks;
        }
        /// <summary>
        /// Bepaald bij welk event een task hoort.
        /// </summary>
        /// <param name="task"></param>
        /// <returns>Een string met een event name</returns>
        public string getPartOfEvent(XmlNode task) 
        {
           return task.Attributes["partOfEvent"].Value;
        }

        /// <summary>
        /// Bepaald van een XmlNode task de identifier in de vorm van een string
        /// </summary>
        /// <param name="task"></param>
        /// <returns>string identifier</returns>
        public string taskIdentifier(XmlNode task)
        {
            XmlNodeList childNodes = task.ChildNodes;
            string identifier = ""; 
            foreach (XmlNode node in childNodes)
            {
                if (node.GetType().Equals("name"))
                {
                    identifier = node.Value;
                }
            }
            return identifier;
        }
    }
}
