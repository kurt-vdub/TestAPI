using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SerkoTestAPI.Models;

namespace SerkoTestAPI.Controllers
{
    public class DataController : ApiController
    {
        private readonly DataService _dataService;
        private readonly JsonResponseBuilder _jsonResponseBuilder;

        public DataController()
        {
            //in a larger application, I would use Unity to bootstrap these objects
            _dataService = new DataService(new XmlParser(new XmlStringManipulator()),
                new RuleManager(new List<IRule> {new MustHaveTotalRule(), new AddDefaultCostCentreRule()}));
            _jsonResponseBuilder = new JsonResponseBuilder();
        }

        public DataController(DataService dataService, JsonResponseBuilder jsonResponseBuilder)
        {
            _dataService = dataService;
            _jsonResponseBuilder = jsonResponseBuilder;
        }

        public HttpResponseMessage Post([FromBody] string value)
        {
            if (string.IsNullOrEmpty(value))
                return ErrorResponse(ErrorMessages.PostDataNotPresent);

            var receivedData = _dataService.ParseReceivedData(value);

            if (!receivedData.ParseSuccessful)
                return ErrorResponse(receivedData.ErrorMessages);

            return _jsonResponseBuilder.BuildResponse(Request.CreateResponse(HttpStatusCode.OK), receivedData.Data.Children);
        }

        private HttpResponseMessage ErrorResponse(params string[] error)
        {
            return Request.CreateResponse(HttpStatusCode.BadRequest, new {Errors = error});
        }
    }
}