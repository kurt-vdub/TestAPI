using NUnit.Framework;
using SerkoTestAPI.Models;

namespace SerkoTestAPI.Tests.Models
{
    [TestFixture]
    public class AddDefaultCostCentreRuleTests
    {
        [Test]
        public void RunRuleAddsCostCenterNodeIfNotExists()
        {
            var costCentreRule = new AddDefaultCostCentreRule();

            var data = new Node
            {
                Children = {new Node {Name = TagNames.Expense}}
            };

            costCentreRule.RunRule(ref data);

            Assert.AreEqual(1, data.Children.Count);
            Assert.AreEqual(1, data.Children[0].Children.Count);
            Assert.AreEqual(TagNames.CostCentre, data.Children[0].Children[0].Name);
            Assert.AreEqual(TagValues.Unknown, data.Children[0].Children[0].Value);
        }

        [Test]
        public void RunRuleAddsExpenseNodeIfNotExists()
        {
            var costCentreRule = new AddDefaultCostCentreRule();

            var data = new Node();

            costCentreRule.RunRule(ref data);

            Assert.AreEqual(1, data.Children.Count);
            Assert.AreEqual(1, data.Children[0].Children.Count);
            Assert.AreEqual(TagNames.Expense, data.Children[0].Name);
            Assert.AreEqual(TagNames.CostCentre, data.Children[0].Children[0].Name);
            Assert.AreEqual(TagValues.Unknown, data.Children[0].Children[0].Value);
        }

        [Test]
        public void RunRuleDoesNotAlterCostCenterIfNodeExists()
        {
            var costCentreRule = new AddDefaultCostCentreRule();

            string expectedValue = "cost centre 1";
            var data = new Node
            {
                Children =
                {
                    new Node {Name = TagNames.Expense, Children = {new Node {Name = TagNames.CostCentre, Value = expectedValue}}}
                }
            };

            costCentreRule.RunRule(ref data);

            Assert.AreEqual(1, data.Children.Count);
            Assert.AreEqual(1, data.Children[0].Children.Count);
            Assert.AreEqual(TagNames.CostCentre, data.Children[0].Children[0].Name);
            Assert.AreEqual(expectedValue, data.Children[0].Children[0].Value);
        }
    }
}