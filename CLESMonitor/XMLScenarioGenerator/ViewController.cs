using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace XMLScenarioGenerator
{
    public struct Element
    {
        public ElementType type;
        public string name;
        public int startSecond;
        public int duration;

        public Element(ElementType _type, string _name, int _startSecond, int _duration)
        {
            type = _type;
            name = _name;
            startSecond = _startSecond;
            duration = _duration;
        }
    }

    public enum ElementType
    {
        Unknown,
        Event,
        Task
    }

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

        public ViewController()
        {
            elements = new List<Element>();
            _view = new Form1(this);

            // Add the (hardcoded) data to be generated here
            scenarioLength = 35;
            elements.Add(new Element(ElementType.Event, "EVENT_IDENTIFIER_1", 2, 30));
            elements.Add(new Element(ElementType.Task, "TASK_IDENTIFIER_2", 15, 5));
            elements.Add(new Element(ElementType.Task, "TASK_IDENTIFIER_3", 20, 0));
        }

        /// <summary>
        /// This methods is called when the button in the GUI is pressed
        /// and will generate and save the XML file.
        /// </summary>
        public void generateXMLFile()
        {
            XmlTextWriter writer = new XmlTextWriter("GeneratedScenario.xml", null);
            writer.Formatting = Formatting.Indented;

            writer.WriteStartElement("scenario");

            // For each second in the generated scenario
            for (int i=1; i <= scenarioLength; i++)
            {
                writer.WriteStartElement("second", null);
                writer.WriteAttributeString("id", Convert.ToString(i));

                foreach (Element element in elements)
                {
                    // Check whether an element starts now
                    if (element.startSecond == i)
                    {
                        if (element.type == ElementType.Event) {
                            writer.WriteStartElement("event", null);
                        }
                        else if (element.type == ElementType.Task) {
                            writer.WriteStartElement("task", null);
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
                        if (element.type == ElementType.Event)
                        {
                            writer.WriteStartElement("event", null);
                        }
                        else if (element.type == ElementType.Task)
                        {
                            writer.WriteStartElement("task", null);
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

                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.Close();
        }
    }
}
