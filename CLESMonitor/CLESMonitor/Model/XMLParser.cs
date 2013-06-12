using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml;

namespace CLESMonitor.Model
{
    /// <summary>
    /// This is a support class for CTLModel. It can parse simulated scenario's in
    /// XML format.
    /// </summary>
    public class XMLParser : CTLInputSource
    {
        // A XmlNodeList of all <second> nodes in the file
        private XmlNodeList secondNodeList;
        // The secondNodeList[index] that XMLParser will process in the next updateTimerCallback
        private int secondIndex;
        // A internal timer that will periodically call updateTimerCallback
        private Timer updateTimer;

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

            // Retrieve every second defined in the scenario
            secondNodeList = xmlDoc.GetElementsByTagName("second");

            secondIndex = 0;
        }

        #region Abstract CTLInputSource implementation

        /// <summary>
        /// Start receiving calls on the delegate.
        /// </summary>
        public override void startReceivingInput()
        {
            updateTimer = new Timer(updateTimerCallback, null, 0, 1000);
        }

        /// <summary>
        /// Stop receiving calls on the delegate.
        /// </summary>
        public override void stopReceivingInput()
        {
            updateTimer.Dispose();
        }

        #endregion

        /// <summary>
        /// Timer callback method. When called, the parser will process one second
        /// from the XML scenario and call the relevant methods on its delegate.
        /// </summary>
        /// <param name="stateInfo">The state info, unused in this method</param>
        public void updateTimerCallback(Object stateInfo)
        {
            List<InputElement> inputElements = elementsForSecond(secondIndex);

            // Check whether the delegate is set and there are elements in the second-node
            if (this.delegateObject != null && inputElements.Count > 0)
            {
                foreach (InputElement inputElement in inputElements)
                {
                    Console.WriteLine(secondIndex + ": Found -> " + inputElement);

                    // A starting event 
                    if (inputElement.type == InputElement.Type.Event && inputElement.action == InputElement.Action.Started)
                    {
                        delegateObject.eventHasStarted(inputElement);
                    }
                    // A stopping event 
                    else if (inputElement.type == InputElement.Type.Event && inputElement.action == InputElement.Action.Stopped)
                    {
                        delegateObject.eventHasStopped(inputElement);
                    }
                    // A starting task
                    else if (inputElement.type == InputElement.Type.Task && inputElement.action == InputElement.Action.Started)
                    {
                        delegateObject.taskHasStarted(inputElement);
                    }
                    // A stopping task
                    else if (inputElement.type == InputElement.Type.Task && inputElement.action == InputElement.Action.Stopped)
                    {
                        delegateObject.taskHasStopped(inputElement);
                    }
                }
            }

            // Add one second to advance to the next second-node
            secondIndex++;
        }

        /// <summary>
        /// Returns a list containing all the elements that have occured in the 
        /// second represented by timeSpan. 
        /// </summary>
        /// <param name="timeSpan">The time span</param>
        /// <returns>The list of elements in the same order as they occur in the xml-file</returns>
        public List<InputElement> elementsForSecond(int second)
        {
            List<InputElement> elements = new List<InputElement>();

            // Only positive natural seconds are accepted.
            if (second >= 0)
            {
                // Collect all the nodes (events, tasks) from the given second in one list
                List<XmlNode> childNodeList = new List<XmlNode>();
                if (second < secondNodeList.Count)
                {
                    foreach (XmlNode node in this.secondNodeList[second].ChildNodes)
                    {
                        childNodeList.Add(node);
                    }
                }

                // Convert the collected nodes into a list of InputElements
                foreach (XmlNode node in childNodeList)
                {
                    // Determine the InputElement type
                    InputElement.Type elementType = InputElement.Type.Unknown;
                    string secondaryIdentifier = null;
                    if (node.Name.Equals("task"))
                    {
                        elementType = InputElement.Type.Task;
                        secondaryIdentifier = node.Attributes["eventID"].Value;
                    }
                    else if (node.Name.Equals("event"))
                    {
                        elementType = InputElement.Type.Event;
                    }

                    // Determine the InputElement action
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

                    // Determine the remaining values
                    string identifier = node.Attributes["id"].Value;
                    string name = node.FirstChild.InnerText;

                    // Create and add the InputElement
                    InputElement returnElement = new InputElement(identifier, name, elementType, elementAction);
                    returnElement.secondaryIndentifier = secondaryIdentifier;
                    elements.Add(returnElement);
                }
            }

            return elements;
        }
    }
}
