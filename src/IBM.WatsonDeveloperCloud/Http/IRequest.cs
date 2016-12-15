using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace IBM.WatsonDeveloperCloud.Http
{
    public interface IRequest : IResponse
    {
        HttpRequestMessage Message { get; }

        IRequest WithBody<T>(T body, MediaTypeHeaderValue contentType = null);

        IRequest WithBody<T>(T body, MediaTypeFormatter formatter, string mediaType = null);

        IRequest WithBodyContent(HttpContent body);

        IRequest WithHeader(string key, string value);

        IRequest WithArgument(string key, object value);

        IRequest WithArguments(object arguments);

        IRequest WithCustom(Action<HttpRequestMessage> request);

        TaskAwaiter<IResponse> GetAwaiter();
    }
}