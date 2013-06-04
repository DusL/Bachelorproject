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
    public class ParsedEvent
    {
        public string identifier { get; private set; }
        public string type { get; private set; }

        public ParsedEvent(string _identifier, string _type)
        {
            identifier = _identifier;
            type = _type;
        }

    }

    public class ParsedTask
    {
        public string identifier {get; private set;}
        public string type { get; private set; }

        public ParsedTask(string _identifier, string _type)
        {
            identifier = _identifier;
            type = _type;
        }

    }

    public enum Action
    {
        Unknown,
        Event,
        Task,
    }

    public enum ActionType
    {
        Unknown,
        EventStarted,
        EventStopped,
        TaskStarted,
        TaskStopped
    }

    public class XMLFileTaskParser
    {
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
        private List<XmlNode> getActionsForSecond(int currentSecond, Action action)
        {            
            List<XmlNode> actionList = new List<XmlNode>();
            if (currentSecond < seconds.Count)
            {
                foreach (XmlNode node in this.seconds[currentSecond].ChildNodes)
                {
                    if (action == Action.Event && node.Name.Equals("event"))
                    {
                        actionList.Add(node);
                    }
                    else if (action == Action.Task && node.Name.Equals("task"))
                    {
                        actionList.Add(node);
                    }
                }
            }
            else 
            {
                endOfFileReached = true;
            }

            return actionList; 
        }

        public List<ParsedEvent> eventsStarted(TimeSpan timeSpan)
        {
            return eventsForTime(timeSpan, ActionType.EventStarted);
        }

        public List<ParsedEvent> eventsStopped(TimeSpan timeSpan)
        {
            return eventsForTime(timeSpan, ActionType.EventStopped);
        }

        private List<ParsedEvent> eventsForTime(TimeSpan timeSpan, ActionType actionType)
        {
            int timeInSeconds = (int)Math.Floor(timeSpan.TotalSeconds);
            List<ParsedEvent> events = new List<ParsedEvent>();

            if (timeInSeconds >= 0)
            {
                List<XmlNode> eventNodeList = getActionsForSecond(timeInSeconds, Action.Event);

                foreach (XmlNode node in eventNodeList) //<event>
                {
                    foreach (XmlNode c in node.ChildNodes)
                    {
                        if (actionType == ActionType.EventStarted && c.Name.Equals("action") && c.InnerText.Equals("started"))
                        {
                            events.Add(new ParsedEvent(node.Attributes["id"].Value, node.FirstChild.InnerText));
                        }
                        else if (actionType == ActionType.EventStopped && c.Name.Equals("action") && c.InnerText.Equals("stopped"))
                        {
                            events.Add(new ParsedEvent(node.Attributes["id"].Value, node.FirstChild.InnerText));
                        }
                    }
                }
            }

            return events;
        }

        /// <summary>
        /// Retrieve the childnodes of each task in taskActionOccured, find the one defining the current action.
        /// When this action equals "started", add the task to the list.        
        /// </summary>
        /// <returns>A list containing the string identifiers of tasks that have started this second</returns>
        public List<ParsedTask> tasksStarted(TimeSpan timeSpan)
        {
            return tasksForTime(timeSpan, ActionType.TaskStarted);
        }

        /// <summary>
        /// Retrieve the childnodes of each task in taskActionOccured, find the one defining the current action.
        /// When this action equals "stopped", add the task to the list.
        /// </summary>
        /// <param name="time"></param>
        /// <returns>A list containing the string identifiers of tasks that have ended this second</returns>
        public List<ParsedTask> tasksStopped(TimeSpan timeSpan)
        {
            return tasksForTime(timeSpan, ActionType.TaskStopped);
        }

        private List<ParsedTask> tasksForTime(TimeSpan timeSpan, ActionType actionType)
        {
            int timeInSeconds = (int)Math.Floor(timeSpan.TotalSeconds);
            List<ParsedTask> tasks = new List<ParsedTask>();

            if (timeInSeconds >= 0)
            {
                List<XmlNode> taskNodeList = getActionsForSecond(timeInSeconds, Action.Task);

                foreach (XmlNode node in taskNodeList) //<task>
                {
                    foreach (XmlNode c in node.ChildNodes)
                    {
                        if (actionType == ActionType.TaskStarted && c.Name.Equals("action") && c.InnerText.Equals("started"))
                        {
                            tasks.Add(new ParsedTask(node.Attributes["id"].Value, node.FirstChild.InnerText));
                        }
                        else if (actionType == ActionType.TaskStopped && c.Name.Equals("action") && c.InnerText.Equals("stopped"))
                        {
                            tasks.Add(new ParsedTask(node.Attributes["id"].Value, node.FirstChild.InnerText));
                        }
                    }
                }
            }
            return tasks;
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
