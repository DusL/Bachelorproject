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
        private ArrayList taskActionsOccured; //Een lijst van taken waar in een seconde iets mee gebeurt is.
        private XmlNodeList seconds;
        private bool endOfFileReached;
        /// <summary>
        /// 1 scenario is 1 file
        /// 
        /// </summary>
        public XMLFileTaskParser()
        {

        }

        /// <summary>
        /// Laad het gekozen Xml-bestand in en slaat direct een lijst van alle seconden op.
        /// </summary>
        /// <param name="path"></param>
        public void readPath(String path)
        {
            // Laad het gewenste Xml document in via het gespecificeerde pad.
            XmlDocument xmlDoc = new XmlDocument(); 
            xmlDoc.Load(path); 

            // Haal iedere scenarion in de file binnen.
            XmlNodeList scenario = xmlDoc.GetElementsByTagName("scenario");

            //Haal iedere sconde die in het scenario gedefinieerd is binnen
            seconds = xmlDoc.GetElementsByTagName("second");

            endOfFileReached = false;
        }
        /// <summary>
        /// Haalt voor de huidige seconde alle acties (alle childnodes) op.
        /// </summary>
        /// <param name="currentSecond"></param>
        /// <returns>Een ArrayList die alle tasks en events bevat die in deze seconde gestart of gestopt zijn.</returns>
        private ArrayList getActionsForSecond(int currentSecond)
        {
            
            ArrayList newList = new ArrayList();
            if (currentSecond < seconds.Count )
            {
                foreach (XmlNode node in this.seconds[currentSecond].ChildNodes)
                {
                    newList.Add(node);
                }
            }
            else 
            {
                endOfFileReached = true;
            }
            
            return newList; 
        }
        /// <summary>
        /// Bepaald welke events voorkomen in een XmlNodeList. 
        /// Hiermee kan bepaald worden met welke events iets aan de hand is 
        /// in een bepaalde seconde.
        /// </summary>
        /// <param name="actions"></param>
        /// <returns>Een XmlNode array van events</returns>
        private ArrayList getEvents(XmlNodeList actions)
        {
            ArrayList events = new ArrayList();
            foreach (XmlNode node in actions)
            {
                if (node.Name.Equals("event"))
                {
                    
                    events.Add(node);
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
        private ArrayList getTasks(ArrayList actions)
        {
            ArrayList tasks = new ArrayList();
            foreach (XmlNode node in actions)
            {
                if (node.Name.Equals("task"))
                {   
                    Console.WriteLine("taakjes");
                    tasks.Add(node);
                }
            }
            return tasks;
        }
        /// <summary>
        /// Haalt alle actions binnen die op een bepaalde seconden voorkomen 
        /// en filert alle taken eruit en zet deze in taskActionsOccured.
        /// </summary>
        /// <param name="timeSpan"></param>
        private void findTasks(TimeSpan timeSpan)
        {
            ArrayList actions = getActionsForSecond((int)Math.Floor(timeSpan.TotalSeconds));
            taskActionsOccured = getTasks(actions);
           
        }
        /// <summary>
        /// Voor iedere taak in taskActionsOccured wordt de child opgezocht waarind de actie gedefinieerd staat.
        /// Wanneer deze actie "started" is wordt deze aan de ArrayList toegevoegd.        
        /// </summary>
        /// <returns>Een arraylist met strings, de identifiers van tasks</returns>
        public List<string> tasksBegan(TimeSpan time)
        {
            findTasks(time);
            List<string> tasksBegan = new List<string>();
            foreach (XmlNode node in taskActionsOccured)
            {
                XmlNodeList children = node.ChildNodes;
                foreach (XmlNode c in node.ChildNodes)
                {
                    Console.WriteLine(c.Name + " " + c.InnerText);
                    if (c.Name.Equals("action") & c.InnerText.Equals("started"))
                    {
                        tasksBegan.Add(node.FirstChild.InnerText);//c.InnerText);

                    }
                }
            }
            Console.WriteLine(taskActionsOccured.Count);

            return tasksBegan;     
        }
        /// <summary>
        /// Voor iedere taak in taskActionsOccured wordt de child opgezocht waarind de actie gedefinieerd staat.
        /// Wanneer deze actie "stopped" is wordt deze aan de ArrayList toegevoegd.
        /// </summary>
        /// <param name="time"></param>
        /// <returns>Een ArrayList met alle taken die deze seconde geeindigd zijn</returns>
        public List<string> tasksEnded(TimeSpan time)
        {
           findTasks(time);
           List<string> tasksEnded = new List<string>();
            foreach (XmlNode node in taskActionsOccured)
            {
                XmlNodeList children = node.ChildNodes;
                foreach (XmlNode c in node.ChildNodes)
                {
                    if (c.Name.Equals("action") & c.InnerText.Equals("stopped"))
                    {
                        Console.WriteLine("Hoppa");
                        
                        tasksEnded.Add(c.InnerText);
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
   
    }
}
