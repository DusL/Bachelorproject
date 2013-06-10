using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Threading;

namespace CLESMonitor.Model
{
    /// <summary>
    /// This is a support class for CTLModel. It can parse simulated scenario's in
    /// XML format.
    /// </summary>
    public class XMLFileTaskParser : CTLInputSource
    {
        private XmlNodeList seconds;
        private Timer updateTimer;
        private TimeSpan timeSpan;

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

            timeSpan = new TimeSpan(0, 0, 0);
        }

        /// <summary>
        /// Start receiving calls on the delegate.
        /// </summary>
        public override void startReceivingInput()
        {
            updateTimer = new Timer(updateTimerCallback, null, 0, 1000);
        }

        /// <summary>
        /// Timer callback method.
        /// </summary>
        /// <param name="stateInfo">The state info</param>
        private void updateTimerCallback(Object stateInfo)
        {
            timeSpan = timeSpan + new TimeSpan(0, 0, 1); //add one second

            List<InputElement> elementsForSecond = elementsForTime(timeSpan);
            Console.WriteLine(elementsForSecond.Count);
            if (this.delegateObject != null)
            {
                if (elementsForSecond.Count > 0)
                {
                    foreach (InputElement inputElement in elementsForSecond)
                    {
                        Console.WriteLine(inputElement.type + " " + inputElement.action);
                        if (inputElement.type == InputElement.Type.Event && inputElement.action == InputElement.Action.Started)
                        {
                            delegateObject.eventHasStarted(inputElement);
                        }
                        else if (inputElement.type == InputElement.Type.Event && inputElement.action == InputElement.Action.Stopped)
                        {
                            delegateObject.eventHasStopped(inputElement);
                        }
                        else if (inputElement.type == InputElement.Type.Task && inputElement.action == InputElement.Action.Started)
                        {
                            delegateObject.taskHasStarted(inputElement);
                        }
                        else if (inputElement.type == InputElement.Type.Task && inputElement.action == InputElement.Action.Stopped)
                        {
                            delegateObject.taskHasStopped(inputElement);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Stop receiving calls on the delegate.
        /// </summary>
        public override void stopReceivingInput()
        {
            timeSpan = new TimeSpan(0, 0, 0);
            updateTimer.Dispose();
        }

        /// <summary>
        /// Returns a list containing all the elements that have occured in the 
        /// second represented by timeSpan.
        /// </summary>
        /// <param name="timeSpan">The time span</param>
        /// <returns>The list of elements</returns>
        public List<InputElement> elementsForTime(TimeSpan timeSpan)
        {
            int timeInSeconds = (int)Math.Floor(timeSpan.TotalSeconds);
            List<InputElement> actions = new List<InputElement>();
            string secondaryIdentifier = null;

            if (timeInSeconds >= 0)
            {
                List<XmlNode> actionNodeList = new List<XmlNode>();
                if (timeInSeconds < seconds.Count)
                {
                    foreach (XmlNode node in this.seconds[timeInSeconds].ChildNodes)
                    {
                        actionNodeList.Add(node);
                    }
                }

                foreach (XmlNode node in actionNodeList) //<event> of <task>
                {
                    InputElement.Type elementType = InputElement.Type.Unknown;
                    if (node.Name.Equals("task"))
                    {
                        elementType = InputElement.Type.Task;
                        secondaryIdentifier = node.Attributes["eventID"].Value;
                    }
                    else if (node.Name.Equals("event"))
                    {
                        elementType = InputElement.Type.Event;
                    }
                    else
                        Console.WriteLine(node.Name);

                    InputElement.Action elementAction = InputElement.Action.Unknown;
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (childNode.InnerText.Equals("started"))
                        {
                            elementAction = InputElement.Action.Started;
                        }
                        else if (childNode.InnerText.Equals("stopped"))
                        {
                            elementAction = InputElement.Action.Stopped;
                        }
                    }
                   
                    string identifier = node.Attributes["id"].Value;
                    string name = node.FirstChild.InnerText;
                    InputElement returnElement = new InputElement(identifier, name, elementType, elementAction);
                    returnElement.secondaryIndentifier = secondaryIdentifier;

                    actions.Add(returnElement);
                }
            }
            return actions;
        }
    }
}
