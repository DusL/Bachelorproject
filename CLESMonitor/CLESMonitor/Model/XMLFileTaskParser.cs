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
        private ArrayList taskActionsOccured;
        private XmlNodeList seconds;
        /// <summary>
        /// 1 scenario is 1 file
        /// 
        /// </summary>
        public XMLFileTaskParser(String path)
        {
            // Laad het gewenste Xml document in via het gespecificeerde pad.
            XmlDocument xmlDoc = new XmlDocument(); 
            xmlDoc.Load(path); 

            // Haal iedere scenarion in de file binnen.
            XmlNodeList scenario = xmlDoc.GetElementsByTagName("scenario");

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
                if (node.Name.Equals("event"))
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
        public ArrayList getTasks(XmlNodeList actions)
        {
            ArrayList tasks = new ArrayList();
            foreach (XmlNode node in actions)
            {
                if (node.Name.Equals("task"))
                {   
                    Console.WriteLine("taakjes");
                    //Array.Resize(ref tasks, tasks.Length + 1);
                    tasks.Add(node);
                }
            }
            return tasks;
        }
        public void findTasks(TimeSpan timeSpan)
        {
            XmlNodeList actions = getActionsForSecond((int)Math.Floor(timeSpan.TotalSeconds));
            taskActionsOccured = getTasks(actions);
            Console.WriteLine(taskActionsOccured.Count);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Een arraylist met strings, de identifiers van tasks</returns>
        public ArrayList tasksBegan()
        {
            ArrayList tasksBegan = new ArrayList();
            foreach (XmlNode node in taskActionsOccured)
            {
                XmlNodeList children = node.ChildNodes;
                foreach (XmlNode c in node.ChildNodes)
                {
                    Console.WriteLine(c.Name + " " + c.InnerText);
                    if (c.Name.Equals("action") & c.InnerText.Equals("started"))
                    {
                        tasksBegan.Add(node.InnerText);
                    }
                }
            }

            return tasksBegan;     
        }

        public ArrayList tasksEnded()
        {
           ArrayList tasksEnded = new ArrayList();
            foreach (XmlNode node in taskActionsOccured)
            {
                XmlNodeList children = node.ChildNodes;
                foreach (XmlNode c in node.ChildNodes)
                {
                    if (c.Name.Equals("action") & c.InnerText.Equals("stopped"))
                    {
                        Console.WriteLine("Hoppa");
                        
                        tasksEnded.Add(node.InnerText);
                    }
                }
            }

            return tasksEnded;
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
                if (node.Name.Equals("name"))
                {
                    identifier = node.InnerText;
                }
            }
            return identifier;
        }
    }
}
