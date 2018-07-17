using System.Collections.Generic;

namespace SerkoTestAPI.Models
{
    public interface IRuleManager
    {
        IEnumerable<RuleResult> RunRules(Node rootNode);
    }

    public class RuleManager : IRuleManager
    {
        private readonly IEnumerable<IRule> _rules;

        public RuleManager(IEnumerable<IRule> rules)
        {
            _rules = rules;
        }

        public IEnumerable<RuleResult> RunRules(Node rootNode)
        {
            foreach (IRule rule in _rules)
            {
                yield return rule.RunRule(ref rootNode);
            }
        }
    }
}