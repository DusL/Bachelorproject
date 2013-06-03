using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace XMLScenarioGenerator
{
    /// <summary>
    /// An Element represents two XML-nodes to be generated,
    /// one when the element starts and one when it stops
    /// </summary>
    public class Element
    {
        public string name;
        public int startSecond;
        public int duration;
        public string identifier;
        private static int counter = 1;

        public Element(string _name, int _startSecond, int _duration)
        {
            name = _name;
            identifier = null;
            startSecond = _startSecond;
            duration = _duration;
            identifier = counter.ToString();
            counter++;
        }
    }

    /// <summary>
    /// Represents an event
    /// </summary>
    public class Event : Element
    {
        public Event(string _name, int _startSecond, int _duration)
            : base(_name, _startSecond, _duration)
        { 
        }
    }

    /// <summary>
    /// Represents a task, it has a pointer to the event it belongs to
    /// </summary>
    public class Task : Element
    {
        public string eventIdentifier;

        public Task(string _name, int _startSecond, int _duration, Event _event) 
            : base(_name, _startSecond, _duration)
        {
            eventIdentifier = _event.identifier;
        }
    }

    /// <summary>
    /// ViewController class
    /// </summary>
    public class ViewController
    {
        List<Element> elements;
        int scenarioLength;

        private Form1 _view;
        public Form1 view
        {
            get
            {
                return _view;
            }
        }

        // Outlets
        SaveFileDialog saveFileDialog;

        /// <summary>
        /// Constructor
        /// </summary>
        public ViewController()
        {
            elements = new List<Element>();
            _view = new Form1(this);

            // Set outlets
            saveFileDialog = this.view.saveFileDialog;

            // Add the (hardcoded) data to be generated here //
            scenarioLength = 60;
            Event event1 = new Event("EVENT_IDENTIFIER_1", 5, 30);
            elements.Add(event1);
            elements.Add(new Task("TASK_IDENTIFIER_1", 15, 2, event1));
            elements.Add(new Task("TASK_IDENTIFIER_2", 20, 3, event1));
            Event event2 = new Event("EVENT_IDENTIFIER_2", 30, 6);
            elements.Add(event2);
            elements.Add(new Task("TASK_IDENTIFIER_3", 31, 2, event2));
            // Add the (hardcoded) data to be generated here //
        }

        /// <summary>
        /// This methods is called when the button in the GUI is pressed
        /// and will generate and save the XML file using the (hardcoded) data.
        /// </summary>
        public void generateXMLFile(String savePath)
        {
            XmlTextWriter writer = new XmlTextWriter(savePath, null);
            writer.Formatting = Formatting.Indented;

            writer.WriteStartElement("scenario");

            // For each second in the scenario
            for (int i=1; i <= scenarioLength; i++)
            {
                writer.WriteStartElement("second", null);
                writer.WriteAttributeString("id", Convert.ToString(i));

                foreach (Element element in elements)
                {
                    // Check whether an element starts now
                    if (element.startSecond == i)
                    {
                        if (element.GetType() == typeof(Event)) {
                            writer.WriteStartElement("event", null);
                            writer.WriteAttributeString("id", element.identifier);
                        }
                        if (element.GetType() == typeof(Task)) {
                            Task task = (Task)element;
                            writer.WriteStartElement("task", null);
                            writer.WriteAttributeString("id", element.identifier);
                            writer.WriteAttributeString("eventID", task.eventIdentifier);
                        }
                        writer.WriteStartElement("name", null);
                        writer.WriteString(element.name);
                        writer.WriteEndElement();
                        writer.WriteStartElement("action", null);
                        writer.WriteString("started");
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }

                    // Check whether an element ends now
                    if (element.startSecond + element.duration == i)
                    {
                        if (element.GetType() == typeof(Event)) {
                            writer.WriteStartElement("event", null);
                            writer.WriteAttributeString("id", element.identifier);
                        }
                        if (element.GetType() == typeof(Task)) {
                            Task task = (Task)element;
                            writer.WriteStartElement("task", null);
                            writer.WriteAttributeString("id", element.identifier);
                            writer.WriteAttributeString("eventID", task.eventIdentifier);
                        }
                        
                        writer.WriteStartElement("name", null);
                        writer.WriteString(element.name);
                        writer.WriteEndElement();
                        writer.WriteStartElement("action", null);
                        writer.WriteString("stopped");
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }
                }

                writer.WriteEndElement(); //end of "second"
            }

            writer.WriteEndElement(); //end of "scenario"

            writer.Close();
        }

        /// <summary>
        /// This action is called when the button in the GUI is pressed
        /// </summary>
        public void openScenarioFileDialog()
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Console.Write("Gekozen file: " + saveFileDialog.FileName);
                this.generateXMLFile(saveFileDialog.FileName);
            }
        }
    }
}
