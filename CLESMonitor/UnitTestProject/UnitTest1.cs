using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace UnitTest
{

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            NUnit.Framework.Assert.AreEqual(2, 2);
            NUnit.Framework.Assert.AreEqual(3, 2);
        }
    }
}
