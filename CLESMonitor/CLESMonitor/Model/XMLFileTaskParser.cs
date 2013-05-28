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


namespace CLESMonitor.Model
{
    public class XMLFileTaskParser
    {
        private List<XmlNode> taskActionsOccured; //A list of tasks with which something happens during a specific second.
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
        /// Load the chosen Xml-file and create a list containing every second in the file
        /// </summary>
        /// <param name="path"></param>
        public void readPath(String path)
        {
            //Load the Xml file through teh specified path.
            XmlDocument xmlDoc = new XmlDocument(); 
            xmlDoc.Load(path); 

            //Retrieve every scenario from file.
            XmlNodeList scenario = xmlDoc.GetElementsByTagName("scenario");

            //Retrieve every second defiened in the scenario
            seconds = xmlDoc.GetElementsByTagName("second");

            endOfFileReached = false;
        }
        /// <summary>
        /// Get every action (childnode) of the current second.
        /// </summary>
        /// <param name="currentSecond"></param>
        /// <returns>Een ArrayList die alle tasks en events bevat die in deze seconde gestart of gestopt zijn.</returns>
        private List<XmlNode> getActionsForSecond(int currentSecond)
        {
            
            List<XmlNode> newList = new List<XmlNode>();
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
        /// Determine which events are present in a list of XmlNodes.
        /// </summary>
        /// <param name="actions"></param>
        /// <returns>A lsit containing XmlNode representations of events</returns>
        private List<XmlNode> getEvents(XmlNodeList actions)
        {
            List<XmlNode> events = new List<XmlNode>();
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
        /// Determine which tasks are present in a list of XmlNodes
        /// </summary>
        /// <param name="actions"></param>
        /// <returns>A List containing tasks represented by XmlNodes</returns>
        private List<XmlNode> getTasks(List<XmlNode> actions)
        {
            List<XmlNode> tasks = new List<XmlNode>();
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
        /// Retrieves all actions occuring during a specific second and adds all tasks to taskActionsOccurd
        /// </summary>
        /// <param name="timeSpan"></param>
        private void findTasks(TimeSpan timeSpan)
        {
            List<XmlNode> actions = getActionsForSecond((int)Math.Floor(timeSpan.TotalSeconds));
            taskActionsOccured = getTasks(actions);
           
        }
        /// <summary>
        /// Retrieve the childnodes of each task in taskActionOccured, find the one defining the current action.
        /// When this action equals "started", add the task to the list.        
        /// </summary>
        /// <returns>A list containing the string identifiers of tasks that have started this second</returns>
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
        /// Retrieve the childnodes of each task in taskActionOccured, find the one defining the current action.
        /// When this action equals "stopped", add the task to the list.
        /// </summary>
        /// <param name="time"></param>
        /// <returns>A list containing the string identifiers of tasks that have ended this second</returns>
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
        /// Determine the event a task belongs to.
        /// </summary>
        /// <param name="task"></param>
        /// <returns>A string: an event name</returns>
        public string getPartOfEvent(XmlNode task) 
        {
           return task.Attributes["partOfEvent"].Value;
        }
   
    }
}
