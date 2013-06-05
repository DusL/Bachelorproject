using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;

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
        /// <summary>
        /// Default value
        /// </summary>
        Unknown,
        Event,
        Task,
    }

    public enum ActionType
    {
        /// <summary>
        /// Default value
        /// </summary>
        Unknown,
        EventStarted,
        EventStopped,
        TaskStarted,
        TaskStopped
    }

    /// <summary>
    /// This is a support class for CTLModel. It can parse simulated scenario's in
    /// XML format.
    /// </summary>
    public class XMLFileTaskParser
    {
        private XmlNodeList seconds;
        private bool endOfFileReached;

        /// <summary>
        /// Load a TextReader into the parser. The data from this TextReader needs to be
        /// in the correct XML format.
        /// </summary>
        /// <param name="textReader">The TextReader to load.</param>
        public void loadTextReader(TextReader textReader)
        {
            // Load the data from the textReader
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(textReader); 

            // Retrieve every scenario from file.
            XmlNodeList scenario = xmlDoc.GetElementsByTagName("scenario");

            // Retrieve every second defiened in the scenario
            seconds = xmlDoc.GetElementsByTagName("second");

            endOfFileReached = false;
        }

        /// <summary>
        /// Returns all actions that have occurred on a given second.
        /// </summary>
        /// <param name="currentSecond">The second</param>
        /// <param name="action">The kind (event, task) of actions to be returned</param>
        /// <returns>A list of XmlNode objects</returns>
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

        /// <summary>
        /// Returns all events that have started in the second represented by timeSpan
        /// </summary>
        /// <param name="timeSpan">The timeSpan</param>
        /// <returns>A list of ParsedEvent objects</returns>
        public List<ParsedEvent> eventsStarted(TimeSpan timeSpan)
        {
            return eventsForTime(timeSpan, ActionType.EventStarted);
        }

        /// <summary>
        /// Returns all events that have stopped in the second represented by timeSpan
        /// </summary>
        /// <param name="timeSpan">The timeSpan</param>
        /// <returns>A list of ParsedEvent objects</returns>
        public List<ParsedEvent> eventsStopped(TimeSpan timeSpan)
        {
            return eventsForTime(timeSpan, ActionType.EventStopped);
        }

        /// <summary>
        /// Returns all events in the second represented by timeSpan.
        /// </summary>
        /// <param name="timeSpan">The timeSpan</param>
        /// <param name="actionType">The type of actions to be returned</param>
        /// <returns>A list of ParsedEvent objects</returns>
        public List<ParsedEvent> eventsForTime(TimeSpan timeSpan, ActionType actionType)
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
        /// Returns all tasks that have started in the second represented by timeSpan
        /// </summary>
        /// <param name="timeSpan">The timeSpan</param>
        /// <returns>A list of ParsedTask objects</returns>
        public List<ParsedTask> tasksStarted(TimeSpan timeSpan)
        {
            return tasksForTime(timeSpan, ActionType.TaskStarted);
        }

        /// <summary>
        /// Returns all tasks that have stopped in the second represented by timeSpan
        /// </summary>
        /// <param name="timeSpan">The timeSpan</param>
        /// <returns>A list of ParsedTask objects</returns>
        public List<ParsedTask> tasksStopped(TimeSpan timeSpan)
        {
            return tasksForTime(timeSpan, ActionType.TaskStopped);
        }

        /// <summary>
        /// Returns all tasks in the second represented by timeSpan.
        /// </summary>
        /// <param name="timeSpan">The timeSpan</param>
        /// <param name="actionType">The type of actions to be returned</param>
        /// <returns>A list of ParsedTask objects</returns>
        public List<ParsedTask> tasksForTime(TimeSpan timeSpan, ActionType actionType)
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
    }
}
