using NUnit.Framework;
using SerkoTestAPI.Models;

namespace SerkoTestAPI.Tests
{
    [TestFixture]
    public class XmlStringManipulatorTests
    {
        private XmlStringManipulator _xmlStringManipulator;

        [SetUp]
        public void SetUp()
        {
            _xmlStringManipulator = new XmlStringManipulator();
        }

        [Test]
        public void StripXmlRemovesOpeningAndClosingTags()
        {
            string tag = "<qwerty>Hello there</qwerty>";
            var result = _xmlStringManipulator.StripOpeningAndClosingTag("qwerty", tag);

            Assert.AreEqual("Hello there", result);
        }

        [Test]
        public void StripXmlRemovesRandomTextInFrontOfOtherTag()
        {
            string tag = "random<qwerty>Hello there</qwerty>";
            var result = _xmlStringManipulator.StripOpeningAndClosingTag("qwerty", tag);

            Assert.AreEqual("Hello there", result);
        }

        [Test]
        public void StripXmlRemovesRandomTextAtEndOfOtherTag()
        {
            string tag = "random<qwerty>Hello there</qwerty>segdfdhdfh";
            var result = _xmlStringManipulator.StripOpeningAndClosingTag("qwerty", tag);

            Assert.AreEqual("Hello there", result);
        }
    }
}
