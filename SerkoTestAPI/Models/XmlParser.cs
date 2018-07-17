using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using Newtonsoft.Json;

namespace SerkoTestAPI.Models
{
    public interface IXmlParser
    {
        /// <summary>
        /// Method to traverse data and build up a noded data structure.
        /// </summary>
        /// <param name="nodeName">The name of the current node.</param>
        /// <param name="nodeValue">The value of the current node.</param>
        Node BuildNodeStructure(string nodeName, string nodeValue);
    }

    public class XmlParser : IXmlParser
    {
        private readonly XmlStringManipulator _manipulator;

        public XmlParser(XmlStringManipulator manipulator)
        {
            _manipulator = manipulator;
        }

        /// <summary>
        /// Method to traverse data and build up a noded data structure.
        /// </summary>
        /// <param name="nodeName">The name of the current node.</param>
        /// <param name="nodeValue">The value of the current node.</param>
        /// <returns></returns>
        public Node BuildNodeStructure(string nodeName, string nodeValue)
        {
            var node = new Node {Name = nodeName};

            int index = 0;
            while (index < nodeValue.Length)
            {
                nodeValue = nodeValue.Substring(index);
                var tagMatch = Regex.Match(nodeValue, RegexPatterns.AnyTag);

                //if no tags found, set value and break
                if (!tagMatch.Success)
                {
                    //if greater than 0, we have already found a tag on this level, and any extra text is not required
                    if(index == 0) node.Value = nodeValue;
                    break;
                }

                var tagMatchValue = tagMatch.Groups[2].Value;

                //if it is an end tag, throw error
                if (tagMatch.Groups[1].Value.Equals("/"))
                    throw new XmlParseException(string.Format(ErrorMessages.OpeningTagNotFound, tagMatchValue));

                var closingTag = Regex.Match(nodeValue, $"</{tagMatchValue}>");

                if (!closingTag.Success)
                    throw new XmlParseException(string.Format(ErrorMessages.ClosingTagNotFound, tagMatchValue));

                var strippedValue = _manipulator.StripOpeningAndClosingTag(tagMatchValue, nodeValue);

                //recursion
                node.Children.Add(BuildNodeStructure(tagMatchValue, strippedValue));

                index = closingTag.Index + closingTag.Value.Length;
            }

            return node;
        }
    }
}