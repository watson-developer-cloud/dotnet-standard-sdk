using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using IBM.WatsonDeveloperCloud.Http.Filters;

namespace IBM.WatsonDeveloperCloud.Http
{
    public interface IClient : IDisposable
    {
        HttpClient BaseClient { get; }

        MediaTypeFormatterCollection Formatters { get; }

        List<IHttpFilter> Filters { get; }

        IClient WithAuthentication(string userName, string password);

        IRequest DeleteAsync(string resource);

        IRequest GetAsync(string resource);

        IRequest PostAsync(string resource);

        IRequest PostAsync<TBody>(string resource, TBody body);

        IRequest PutAsync(string resource);

        IRequest PutAsync<TBody>(string resource, TBody body);

        IRequest SendAsync(HttpMethod method, string resource);

        IRequest SendAsync(HttpRequestMessage message);
    }
}