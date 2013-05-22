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

//using System.IO.Packaging;

namespace CLESMonitor.Model
{
    public class XMLFileTaskParser
    {
        /// <summary>
        /// 1 scenario is 1 file
        /// 
        /// </summary>
        public XMLFileTaskParser()
        {
            ViewController controller = new ViewController();

            XmlDocument xmlDoc = new XmlDocument(); //* create an xml document object.
            xmlDoc.Load(@"D:\vvandertas\Dropbox\Bachelorproject\XMLFile1.xml"); //* load the XML document from the specified file

            // Haal iedere scenarion in de file binnen.
            XmlNodeList scenario = xmlDoc.GetElementsByTagName("scenario");
            Console.WriteLine(scenario[0].Name);
           // Console.WriteLine(second[0].Name);

            //Haal iedere sconde die in het scenario gedefinieerd is binnen
            XmlNodeList second = xmlDoc.GetElementsByTagName("second");
            XmlNodeList tasksSecond1 = second[0].ChildNodes;
            //XmlNode task1 = 
            XmlNode firstNode = second.Item(1);
        }

        
    }
}
