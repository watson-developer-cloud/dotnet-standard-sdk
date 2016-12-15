using System;
using System.Net;
using System.Net.Http;

namespace IBM.WatsonDeveloperCloud.Http.Exceptions
{
    public class ServiceResponseException : Exception
    {
        public Error Error { get; set; }

        public HttpStatusCode Status { get; protected set; }

        public IResponse Response { get; protected set; }

        public HttpResponseMessage ResponseMessage { get; protected set; }

        public ServiceResponseException(IResponse response, HttpResponseMessage responseMessage, string message, Exception innerException = null)
            : base(message, innerException)
        {
            this.Response = response;
            this.ResponseMessage = responseMessage;
            this.Status = responseMessage.StatusCode;
        }
    }
}