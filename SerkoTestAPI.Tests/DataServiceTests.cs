using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using SerkoTestAPI.Models;

namespace SerkoTestAPI.Tests
{
    [TestFixture]
    public class DataServiceTests
    {
        private DataService _dataService;
        private Mock<IXmlParser> _xmlParserMock;
        private Mock<IRuleManager> _ruleManagerMock;

        [SetUp]
        public void SetUp()
        {
            _xmlParserMock = new Mock<IXmlParser>();
            _ruleManagerMock = new Mock<IRuleManager>();
            _dataService = new DataService(_xmlParserMock.Object, _ruleManagerMock.Object);
        }

        [Test]
        public void ParseReceivedDataReturnsSuccessResultIfRulesPass()
        {
            _ruleManagerMock.Setup(m => m.RunRules(It.IsAny<Node>())).Returns(new List<RuleResult>());

            ParseDataResponse response = _dataService.ParseReceivedData("data");

            Assert.AreEqual(true, response.ParseSuccessful);
        }

        [Test]
        public void ParseReceivedDataReturnsFailureResultIfAnyRulesFail()
        {
            var errorMessage = "error message";
            _ruleManagerMock.Setup(m => m.RunRules(It.IsAny<Node>()))
                .Returns(new List<RuleResult> {new RuleResult(false, errorMessage), new RuleResult(true)});

            ParseDataResponse response = _dataService.ParseReceivedData("data");

            Assert.AreEqual(false, response.ParseSuccessful);
            Assert.True(response.ErrorMessages.Contains(errorMessage));
        }

        [Test]
        public void ParseReceivedDataReturnsReturnsFailureIfUnknownExceptionIsThrown()
        {
            _ruleManagerMock.Setup(m => m.RunRules(It.IsAny<Node>())).Throws<Exception>();

            ParseDataResponse response = _dataService.ParseReceivedData("data");

            Assert.AreEqual(false, response.ParseSuccessful);
            Assert.True(response.ErrorMessages.Contains(ErrorMessages.UnknownParseError));
        }

        [Test]
        public void ParseReceivedDataReturnsReturnsFailureIfParseExceptionIsThrown()
        {
            var xmlParseException = new XmlParseException("the parse error");
            _xmlParserMock.Setup(m => m.BuildNodeStructure(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(xmlParseException);

            ParseDataResponse response = _dataService.ParseReceivedData("data");

            Assert.AreEqual(false, response.ParseSuccessful);
            Assert.True(response.ErrorMessages.Contains(xmlParseException.Message));
        }

        [Test]
        public void ParseReceivedDataReturnsNodedDataStructureIfRulesPass()
        {
            Node node = new Node {Name = "test"};
            _xmlParserMock.Setup(m => m.BuildNodeStructure(It.IsAny<string>(), It.IsAny<string>())).Returns(node);
            _ruleManagerMock.Setup(m => m.RunRules(It.IsAny<Node>()))
                .Returns(new List<RuleResult> {new RuleResult(true)});

            ParseDataResponse response = _dataService.ParseReceivedData("data");

            Assert.AreEqual(true, response.ParseSuccessful);
            Assert.AreEqual(node, response.Data);
        }
    }
}
