using System.Net.Http;
using IBM.WatsonDeveloperCloud.Http.Exceptions;

namespace IBM.WatsonDeveloperCloud.Http.Filters
{
    public class ErrorFilter : IHttpFilter
    {
        public void OnRequest(IRequest request, HttpRequestMessage requestMessage) { }

        public void OnResponse(IResponse response, HttpResponseMessage responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                ServiceResponseException exception =
                    new ServiceResponseException(response, responseMessage, $"The API query failed with status code {responseMessage.StatusCode}: {responseMessage.ReasonPhrase}");

                exception.Error = responseMessage.Content.ReadAsAsync<Error>().Result;

                throw exception;
            }
                
        }
    }
}