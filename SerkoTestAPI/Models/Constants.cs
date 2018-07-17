namespace SerkoTestAPI.Models
{
    /// <summary>
    /// Application error messages. In a real scenario, I would be moving these into resource files.
    /// </summary>
    public static class ErrorMessages
    {
        public const string ClosingTagNotFound = @"Closing tag for [{0}] not found.";
        public const string OpeningTagNotFound = @"Closing tag [{0}] found without opening tag.";
        public const string UnknownParseError = "Something went wrong, please check the data is structured correctly.";
        public static string TotalTagIsMissing = "[total] field is not present in the data. Message rejected.";
        public static string PostDataNotPresent = "No data provided, or data not sent correctly. Please pass in data as a single string.";
    }

    public static class RegexPatterns
    {
        public const string OpeningTag = @"<([a-zA-Z_][a-zA-Z0-9\-_\.]*)>";
        public const string ClosingTag = @"<\/([a-zA-Z_][a-zA-Z0-9\-_\.]*)>";
        public const string AnyTag = @"<(\/?)([a-zA-Z_][a-zA-Z0-9\-_\.]*)>";
    }

    public static class TagNames
    {
        public const string Root = "root";
        public const string Expense = "expense";
        public const string CostCentre = "cost_centre";
        public const string Total = "total";
        public const string PaymentMethod = "payment_method";
        public const string Vendor = "vendor";
        public const string Description = "description";
        public const string Date = "date";
    }

    public static class TagValues
    {
        public const string Unknown = "UNKNOWN";

    }
}