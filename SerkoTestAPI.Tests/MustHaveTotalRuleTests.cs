using NUnit.Framework;
using SerkoTestAPI.Models;

namespace SerkoTestAPI.Tests
{
    [TestFixture]
    public class MustHaveTotalRuleTests
    {
        [Test]
        public void RuleFailsIfTotalIsMissingFromNodeStructure()
        {
            var mustHaveTotalRule = new MustHaveTotalRule();
            var rootNode = new Node();

            var ruleResult = mustHaveTotalRule.RunRule(ref rootNode);

            Assert.AreEqual(false, ruleResult.RulePassed);
        }

        [Test]
        public void RuleFailsPassesIfTotalIsPresent()
        {
            var mustHaveTotalRule = new MustHaveTotalRule();
            var rootNode = new Node(){Children = { new Node(){Name = TagNames.Total}}};

            var ruleResult = mustHaveTotalRule.RunRule(ref rootNode);

            Assert.AreEqual(true, ruleResult.RulePassed);
        }
    }
}
