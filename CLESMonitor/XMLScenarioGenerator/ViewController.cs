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

            // Add the (hardcoded) data to be generated here
            addScenario1d();
        }

        /// <summary>
        /// Een (korte, 1 min bijvoorbeeld) vertraagde trein, zonder problemen voor een andere trein.
        /// Deze andere trein zou bijvoorbeeld 3 min later het spoor van de eerste trein moeten kruisen.
        /// </summary>
        public void addScenario1a()
        {
            scenarioLength = 80;
            Event event1 = new Event("VERTRAAGDE_TREIN_OK", 10, 60);
            elements.Add(event1);

            elements.Add(new Task("SELECTEER_REGEL", 15, 3, event1));
            elements.Add(new Task("KWIT_VERT_REGELS", 20, 5, event1));
            elements.Add(new Task("VERWERK_VERT_REGELS", 45, 5, event1));
            elements.Add(new Task("DESELECTEER_REGEL", 52, 3, event1));
        }

        /// <summary>
        /// Een korte (3 min bijvoorbeeld) vertraagde trein, wat problemen oplevert voor een andere trein.
        /// Deze andere trein zou direct na de eerste het spoor moeten kruisen. (sein)
        /// </summary>
        public void addScenario1b()
        {
            scenarioLength = 140;
            Event event1 = new Event("VERTRAAGDE_TREIN_PROBLEEM", 10, 120);
            elements.Add(event1);

            elements.Add(new Task("SELECTEER_REGEL", 15, 3, event1));
            elements.Add(new Task("KWIT_VERT_REGELS", 20, 5, event1));

            elements.Add(new Task("ARI_UIT", 45, 3, event1));
            elements.Add(new Task("HERROEP_SEIN", 50, 20, event1));
            elements.Add(new Task("HAND_VERWERK_REGEL", 70, 20, event1));
            elements.Add(new Task("ARI_IN", 95, 3, event1));
            
            elements.Add(new Task("VERWERK_VERT_REGELS", 100, 5, event1));
            elements.Add(new Task("DESELECTEER_REGEL", 107, 3, event1));
        }

        /// <summary>
        /// Multitask-scenario, waarop 1a en 1b tegelijkertijd plaatsvinden.
        /// </summary>
        public void addScenario1c()
        {
            addScenario1a();
            addScenario1b();
            scenarioLength = 140;
        }

        /// <summary>
        /// Multitask-scenario, waarop 1a en 1b tegelijkertijd plaatsvinden.
        /// </summary>
        public void addScenario1d()
        {
            addScenario1a();
            addScenario1b();
            addScenario1b();
            scenarioLength = 140;
        }

        /// <summary>
        /// Een gestoorde wissel. Met twee treinen, waarvan de voorste niet over de (geklemde) wissel in de correcte richting kan,
        /// en een tweede die hierachter voor een sein staat en wel over de wissel kan.
        /// </summary>
        public void addScenario2()
        {
            scenarioLength = 440;
            Event event1 = new Event("GESTOORDE_WISSEL", 0, 420);
            elements.Add(event1);

            // Voor de trein die niet over de gestoorde wissel kan
            elements.Add(new Task("SELECTEER_REGEL", 2, 5, event1));
            elements.Add(new Task("VIND_TREIN", 5, 15, event1));
            elements.Add(new Task("ARI_UIT", 20, 3, event1));
            elements.Add(new Task("HERROEP_SEIN", 35, 20, event1));
            elements.Add(new Task("DESELECTEER_REGEL", 58, 3, event1));
            // Voor de trein aankomend achter de eerste 
            elements.Add(new Task("SELECTEER_REGEL", 62, 3, event1));
            elements.Add(new Task("VIND_TREIN", 65, 15, event1));
            elements.Add(new Task("ARI_UIT", 80, 3, event1));
            elements.Add(new Task("HERROEP_SEIN", 85, 20, event1));
            elements.Add(new Task("DESELECTEER_REGEL", 113, 3, event1));

            elements.Add(new Task("SELECTEER_REGEL", 115, 3, event1));
            elements.Add(new Task("HAND_VERWERK_REGEL", 125, 35, event1));
            elements.Add(new Task("VERWERK_VERT_REGELS", 170, 5, event1));
            elements.Add(new Task("ARI_IN", 178, 3, event1));
            elements.Add(new Task("DESELECTEER_REGEL", 184, 3, event1));

            elements.Add(new Task("SELECTEER_REGEL", 190, 3, event1));
            elements.Add(new Task("HAND_VERWERK_REGEL", 200, 35, event1));
            elements.Add(new Task("VERWERK_VERT_REGELS", 245, 5, event1));
            elements.Add(new Task("LASTGEVING", 310, 45, event1)); //over geklemde wissel
            elements.Add(new Task("ARI_IN", 365, 3, event1));
            elements.Add(new Task("DESELECTEER_REGEL", 370, 3, event1));
        }

        public void addScenario3()
        {
            scenarioLength = 610;

            Event event1 = new Event("GESTRANDE_TREIN", 5, 600);
            elements.Add(event1);

            // Gesprek met machinist kapotte trein
            elements.Add(new Task("COMMUNICATIE", 10, 90, event1));
            // Gesprek met VKL
            elements.Add(new Task("COMMUNICATIE", 110, 90, event1));

            // Rijweginstelling kapotte trein verhinderen
            elements.Add(new Task("SELECTEER_REGEL", 200, 3, event1));
            elements.Add(new Task("VIND_TREIN", 205, 15, event1));
            elements.Add(new Task("ARI_UIT", 220, 3, event1));
            elements.Add(new Task("DESELECTEER_REGEL", 223, 2, event1));

            // Sein herroepen bij de wissel
            elements.Add(new Task("HERROEP_SEIN", 230, 20, event1));

            // Rijweginstelling trein verhinderen
            elements.Add(new Task("SELECTEER_REGEL", 260, 3, event1));
            elements.Add(new Task("VIND_TREIN", 265, 20, event1));
            elements.Add(new Task("ARI_UIT", 285, 3, event1));

            elements.Add(new Task("HAND_VERWERK_REGEL", 290, 60, event1));
            elements.Add(new Task("ARI_IN", 350, 3, event1));
            elements.Add(new Task("DESELECTEER_REGEL", 355, 3, event1));

            // Communicatie monteur
            elements.Add(new Task("COMMUNICATIE", 380, 60, event1));

            // Rijweginstelling trein herstellen
            elements.Add(new Task("SELECTEER_REGEL", 440, 3, event1));
            elements.Add(new Task("VIND_TREIN", 445, 20, event1));
            elements.Add(new Task("HAND_VERWERK_REGEL", 470, 60, event1));
            elements.Add(new Task("ARI_IN", 530, 3, event1));
            elements.Add(new Task("DESELECTEER_REGEL", 535, 3, event1));
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
