namespace SerkoTestAPI.Models
{
    public class RuleResult
    {
        public RuleResult(bool rulePassed, string errorMessage = null)
        {
            RulePassed = rulePassed;
            ErrorMessage = errorMessage;
        }

        public bool RulePassed { get; }
        public string ErrorMessage { get; }
    }
}