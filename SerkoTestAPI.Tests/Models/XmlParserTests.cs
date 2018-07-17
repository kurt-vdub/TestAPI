using System;
using NUnit.Framework;
using SerkoTestAPI.Models;

namespace SerkoTestAPI.Tests.Models
{
    [TestFixture]
    public class XmlParserTests
    {
        private XmlParser _xmlParser;

        [SetUp]
        public void SetUp()
        {
            _xmlParser = new XmlParser(new XmlStringManipulator());
        }
     
        [Test]
        public void BuildNodesCanBuildFlatNodeStructure()
        {
            string testData = "<data>hello</data>" +
                              "<data1>world</data1>";

            Node node = _xmlParser.BuildNodeStructure(TagNames.Root, testData);

            Assert.AreEqual(2, node.Children.Count);
            Assert.AreEqual("data", node.Children[0].Name);
            Assert.AreEqual("hello", node.Children[0].Value);
            Assert.AreEqual("data1", node.Children[1].Name);
            Assert.AreEqual("world", node.Children[1].Value);
        }

        [Test]
        public void BuildNodesCanBuildNestedNodeStructure()
        {
            string testData = "<data>" +
                              "<innerdata>great</innerdata>" +
                              "</data>" +
                              "<data1>world</data1>";

            Node node = _xmlParser.BuildNodeStructure(TagNames.Root, testData);

            Assert.AreEqual(2, node.Children.Count);
            Assert.AreEqual("data", node.Children[0].Name);
            Assert.IsNull(node.Children[0].Value);
            Assert.AreEqual("innerdata", node.Children[0].Children[0].Name);
            Assert.AreEqual("great", node.Children[0].Children[0].Value);
            Assert.AreEqual("data1", node.Children[1].Name);
            Assert.AreEqual("world", node.Children[1].Value);
        }

        [Test]
        public void BuildNodesThrowsCorrectExceptionIfClosingTagIsMissing()
        {
            string testData = "<data>hello" +
                              "<opentag>" +
                              "</data>" +
                              "<data1>world</data1>";

            Exception e = Assert.Throws<XmlParseException>(() => _xmlParser.BuildNodeStructure(TagNames.Root, testData));

            Assert.AreEqual(string.Format(ErrorMessages.ClosingTagNotFound, "opentag"), e.Message);
        }

        [Test]
        public void BuildNodesThrowsCorrectExceptionIfOpeningTagIsMissing()
        {
            string testData = "<data>hello" +
                              "</data>" +
                              "</closing>" +
                              "<data1>world</data1>";

            Exception e = Assert.Throws<XmlParseException>(() => _xmlParser.BuildNodeStructure(TagNames.Root, testData));

            Assert.AreEqual(string.Format(ErrorMessages.OpeningTagNotFound, "closing"), e.Message);
        }

        [Test]
        public void BuildNodesIgnoresTagTextIfThereIsANestedTag()
        {
            string testData = "<singletag>hello</singletag>" +
                              "<tagparent>text to ignore" +
                              "<innertag>universe</innertag>" +
                              "more text to ignore" +
                              "</tagparent>";

            Node node = _xmlParser.BuildNodeStructure(TagNames.Root, testData);

            Assert.AreEqual("tagparent", node.Children[1].Name);
            Assert.IsNull(node.Children[1].Value);
        }

        [Test]
        public void BuildNodesThrowsClosingTagNotFoundExceptionIfParentTagClosesBeforeChild()
        {
            string testData = "<tag1>" +
                              "tag 1 value" +
                              "<tag2>" +
                              "tag 2 value" +
                              "</tag1>" +
                              "</tag2";

            Exception e = Assert.Throws<XmlParseException>(() => _xmlParser.BuildNodeStructure(TagNames.Root, testData));

            Assert.AreEqual(string.Format(ErrorMessages.ClosingTagNotFound, "tag2"), e.Message);
        }

        [Test]
        public void BuildNodesCanParseTestDataProvided()
        {
            Node node = _xmlParser.BuildNodeStructure(TagNames.Root, TestData.TestValue);

            Assert.AreEqual(4, node.Children.Count);

            Assert.AreEqual("expense", node.Children[0].Name);
            Assert.IsNull(node.Children[0].Value);

            Assert.AreEqual("vendor", node.Children[1].Name);
            Assert.AreEqual("Viaduct Steakhouse", node.Children[1].Value);

            Assert.AreEqual("description", node.Children[2].Name);
            Assert.AreEqual("development team’s project end celebration dinner", node.Children[2].Value);

            Assert.AreEqual("date", node.Children[3].Name);
            Assert.AreEqual("Tuesday 27 April 2017", node.Children[3].Value);

            Assert.AreEqual("cost_centre", node.Children[0].Children[0].Name);
            Assert.AreEqual("DEV002", node.Children[0].Children[0].Value);
            Assert.AreEqual("total", node.Children[0].Children[1].Name);
            Assert.AreEqual("890.55", node.Children[0].Children[1].Value);
            Assert.AreEqual("payment_method", node.Children[0].Children[2].Name);
            Assert.AreEqual("personal card", node.Children[0].Children[2].Value);
        }
    }
}
