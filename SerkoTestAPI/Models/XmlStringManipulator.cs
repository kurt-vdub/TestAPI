using System;

namespace SerkoTestAPI.Models
{
    public class XmlStringManipulator
    {
        public string StripOpeningAndClosingTag(string tagName, string value)
        {
            value = value.Substring(value.IndexOf($"<{tagName}>", StringComparison.Ordinal) + tagName.Length + 2);
            value = value.Substring(0, value.IndexOf($"</{tagName}>", StringComparison.Ordinal));
            return value;
        }
    }
}