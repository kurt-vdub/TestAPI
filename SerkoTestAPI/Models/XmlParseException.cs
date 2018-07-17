using System;

namespace SerkoTestAPI.Models
{
    public class XmlParseException : Exception
    {
        public XmlParseException(string message) : base(message)
        {
        }
    }
}