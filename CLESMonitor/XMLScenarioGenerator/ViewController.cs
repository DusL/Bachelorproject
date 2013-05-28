using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace XMLScenarioGenerator
{
    public class ViewController
    {
        private Form1 _view;
        public Form1 view
        {
            get
            {
                return _view;
            }
        }

        // Outlets
        TextBox textBox;

        public ViewController()
        {
            _view = new Form1(this);

            textBox = _view.textBox1;
        }

        public void generateXMLFile()
        {
            Console.WriteLine(textBox.Text);

            XDocument xmlDocument = new XDocument(
                new XElement("Root",
                    new XElement("Child", "content")
                )
            );
            
            xmlDocument.Save("testje.xml");
        }
    }
}
