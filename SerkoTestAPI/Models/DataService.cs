using System;
using System.Diagnostics;
using System.Linq;

namespace SerkoTestAPI.Models
{
    public class DataService
    {
        private readonly IXmlParser _xmlParser;
        private readonly IRuleManager _ruleManager;

        public DataService(IXmlParser xmlParser, IRuleManager ruleManager)
        {
            _xmlParser = xmlParser;
            _ruleManager = ruleManager;
        }

        public ParseDataResponse ParseReceivedData(string data)
        {
            var response = new ParseDataResponse();

            try
            {
                Node rootNode = _xmlParser.BuildNodeStructure(TagNames.Root, data);
                response.Data = rootNode;

                var ruleResults = _ruleManager.RunRules(rootNode).ToList();

                if (ruleResults.All(r => r.RulePassed))
                {
                    response.ParseSuccessful = true;
                    return response;
                }

                response.ErrorMessages = ruleResults.Where(r => !string.IsNullOrEmpty(r.ErrorMessage))
                    .Select(r => r.ErrorMessage).ToArray();
            }
            catch (XmlParseException e)
            {
                response.ErrorMessages = new[] {e.Message};
            }
            catch (Exception e)
            {
                //todo would log exception here
                Debug.WriteLine(e.ToString());
                response.ErrorMessages = new[] {ErrorMessages.UnknownParseError};
            }

            return response;
        }
    }
}