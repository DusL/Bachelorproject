using CLESMonitor.Model.CL;
using Moq;
using NUnit.Framework;
using System;

namespace UnitTest.Model.CL
{
    [TestFixture]
    public class CTLInputSourceTest
    {
        #region InputElement

        [Test]
        public void inputElementEquals_Properties()
        {
            InputElement inputElement1 =
                new InputElement("1", "FOO", InputElement.Type.Event, InputElement.Action.Started);
            InputElement inputElement2;

            // Equal objects
            inputElement2 = new InputElement("1", "FOO", InputElement.Type.Event, InputElement.Action.Started);
            Assert.IsTrue(inputElement1.Equals(inputElement2));
            // Different identifier
            inputElement2 = new InputElement("2", "FOO", InputElement.Type.Event, InputElement.Action.Started);
            Assert.IsFalse(inputElement1.Equals(inputElement2));
            // Different name
            inputElement2 = new InputElement("1", "BAR", InputElement.Type.Event, InputElement.Action.Started);
            Assert.IsFalse(inputElement1.Equals(inputElement2));
            // Different type
            inputElement2 = new InputElement("1", "FOO", InputElement.Type.Task, InputElement.Action.Started);
            Assert.IsFalse(inputElement1.Equals(inputElement2));
            // Different action
            inputElement2 = new InputElement("1", "FOO", InputElement.Type.Event, InputElement.Action.Stopped);
            Assert.IsFalse(inputElement1.Equals(inputElement2));
        }

        [Test]
        public void inputElementEquals_Logic()
        {
            InputElement inputElement1 =
                new InputElement("1", "FOO", InputElement.Type.Event, InputElement.Action.Started);
            InputElement inputElement2 =
                new InputElement("2", "FOO", InputElement.Type.Event, InputElement.Action.Started);

            // x.equals(x) should be true
            Assert.IsTrue(inputElement1.Equals(inputElement1));
            // x.equals(y) should be the same as y.equals(x)
            Assert.AreEqual(inputElement1.Equals(inputElement2), inputElement2.Equals(inputElement1));
            // x.equals(null) should be false
            Assert.IsFalse(inputElement1.Equals(null));
        }

        #endregion
    }
}
