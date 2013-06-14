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

        /// <summary>
        /// The Element constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="startSecond"></param>
        /// <param name="duration"></param>
        public Element(string name, int startSecond, int duration)
        {
            this.name = name;
            identifier = null;
            this.startSecond = startSecond;
            this.duration = duration;
            identifier = counter.ToString();
            counter++;
        }
    }

    /// <summary>
    /// Represents an event
    /// </summary>
    public class Event : Element
    {
        public Event(string name, int startSecond, int duration)
            : base(name, startSecond, duration)
        { 
        }
    }

    /// <summary>
    /// Represents a task, it has a pointer to the event it belongs to
    /// </summary>
    public class Task : Element
    {
        public string eventIdentifier;

        public Task(string name, int startSecond, int duration, Event eventObject) 
            : base(name, startSecond, duration)
        {
            eventIdentifier = eventObject.identifier;
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
            /*
            scenarioLength = 60;
            Event event1 = new Event("EVENT_IDENTIFIER_1", 5, 30);
            elements.Add(event1);
            elements.Add(new Task("TASK_IDENTIFIER_1", 15, 2, event1));
            elements.Add(new Task("TASK_IDENTIFIER_2", 20, 3, event1));
            Event event2 = new Event("EVENT_IDENTIFIER_2", 30, 6);
            elements.Add(event2);
            elements.Add(new Task("TASK_IDENTIFIER_3", 31, 2, event2));
            */
            addScenario1();
            // Add the (hardcoded) data to be generated here //
        }

        public void addScenario1()
        {
            scenarioLength = 160;

            Event event1 = new Event("GESTRANDE_TREIN", 5, 150);
            elements.Add(event1);

            // Rijweginstelling trein verhinderen
            elements.Add(new Task("SELECTEER_REGEL", 10, 1, event1));
            elements.Add(new Task("VIND_TREIN", 11, 5, event1));
            elements.Add(new Task("ARI_UIT", 16, 1, event1));
            elements.Add(new Task("DESELECTEER_REGEL", 17, 3, event1));

            // Rijweginstelling trein verhinderen
            elements.Add(new Task("SELECTEER_REGEL", 25, 1, event1));
            elements.Add(new Task("VIND_TREIN", 26, 5, event1));
            elements.Add(new Task("ARI_UIT", 31, 1, event1));
            elements.Add(new Task("DESELECTEER_REGEL", 32, 3, event1));

            // Communicatie VKL, monteur
            elements.Add(new Task("COMMUNICATIE", 40, 30, event1));
            elements.Add(new Task("COMMUNICATIE", 80, 30, event1));

            // Rijweginstelling trein herstellen
            elements.Add(new Task("SELECTEER_REGEL", 120, 1, event1));
            elements.Add(new Task("VIND_TREIN", 121, 5, event1));
            elements.Add(new Task("ARI_IN", 126, 1, event1));
            elements.Add(new Task("DESELECTEER_REGEL", 127, 3, event1));

            // Muteer rijweg
            elements.Add(new Task("SELECTEER_REGEL", 135, 2, event1));
            elements.Add(new Task("REGEL_IN_MUTATIESCHERM", 137, 2, event1));
            elements.Add(new Task("MUTEER_REGEL", 139, 11, event1));
            elements.Add(new Task("REGEL_TERUG", 150, 2, event1));

            Event event2 = new Event("VERTRAAGDE_TREIN", 20, 20);
            elements.Add(event2);

            elements.Add(new Task("SELECTEER_REGEL", 25, 1, event2));
            elements.Add(new Task("KWIT_VERT_REGELS", 26, 2, event2));
            elements.Add(new Task("VERWERK_VERT_REGELS", 38, 2, event2));
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
