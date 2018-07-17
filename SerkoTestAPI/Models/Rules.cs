namespace SerkoTestAPI.Models
{
    public interface IRule
    {
        RuleResult RunRule(ref Node rootNode);
    }

    public class MustHaveTotalRule : IRule
    {
        public RuleResult RunRule(ref Node rootNode)
        {
            var node = rootNode.GetNodeByName(TagNames.Total);
            if (node == null) return new RuleResult(false, ErrorMessages.TotalTagIsMissing);
            return new RuleResult(true);
        }
    }

    public class AddDefaultCostCentreRule : IRule
    {
        public RuleResult RunRule(ref Node rootNode)
        {
            var node = rootNode.GetNodeByName(TagNames.CostCentre);
            if (node != null) return new RuleResult(true);

            var expenseNode = rootNode.GetNodeByName(TagNames.Expense);
            if (expenseNode == null)
            {
                expenseNode = new Node
                {
                    Name = TagNames.Expense
                };

                rootNode.Children.Add(expenseNode);
            }

            expenseNode.Children.Add(new Node {Name = TagNames.CostCentre, Value = TagValues.Unknown});

            return new RuleResult(true);
        }
    }
}