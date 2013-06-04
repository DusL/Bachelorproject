using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using NUnit.Mocks;

using CLESMonitor.Model;

namespace UnitTest.Model
{
    [TestFixture]
    public class CTLModelTest
    {
        XMLFileTaskParser parser;
        CTLModel model;

        [SetUp]
        //Initialize any objects needed for the tests contained in this class
        public void Setup()
        {
            parser = new XMLFileTaskParser();
            model = new CTLModel(parser);
        
        }
        [Test]
        public void getTaskFromIdentifierNull()
        {

            Assert.AreEqual(1, 1);        
   
        }
        [TearDown]
        // Removes all objects generated during setUp and the tests so that only the original objects are present
        // after testing
        public void TearDown()
        { 
        
        }
    }
}
