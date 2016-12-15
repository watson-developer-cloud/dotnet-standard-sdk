using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using IBM.WatsonDeveloperCloud.Http.Filters;

namespace IBM.WatsonDeveloperCloud.Http
{
    public class WatsonHttpClient : IClient
    {
        private bool IsDisposed;

        public List<IHttpFilter> Filters { get; private set; }

        public HttpClient BaseClient { get; private set; }

        public MediaTypeFormatterCollection Formatters { get; protected set; }

        public WatsonHttpClient(string baseUri)
        {
            this.Filters = new List<IHttpFilter> { new ErrorFilter() };
            if (baseUri != null)
                this.BaseClient.BaseAddress = new Uri(baseUri);

            this.Formatters = new MediaTypeFormatterCollection();
        }

        public WatsonHttpClient(string baseUri, string userName, string password)
        {
            this.BaseClient = new HttpClient();

            this.Filters = new List<IHttpFilter> { new ErrorFilter() };

            if (baseUri != null)
                this.BaseClient.BaseAddress = new Uri(baseUri);

            this.Formatters = new MediaTypeFormatterCollection();

            this.WithAuthentication(userName, password);
        }

        public WatsonHttpClient(string baseUri, string userName, string password, HttpClient client)
        {
            this.BaseClient = client;
            this.Filters = new List<IHttpFilter> { new ErrorFilter() };
            if (baseUri != null)
                this.BaseClient.BaseAddress = new Uri(baseUri);
            this.Formatters = new MediaTypeFormatterCollection();

            this.WithAuthentication(userName, password);
        }

        public IClient WithAuthentication(string userName, string password)
        {
            string auth = string.Format("{0}:{1}", userName, password);
            string auth64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(auth));

            this.BaseClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth64);

            return this;
        }

        public IRequest DeleteAsync(string resource)
        {
            return this.SendAsync(HttpMethod.Delete, resource);
        }

        public IRequest GetAsync(string resource)
        {
            return this.SendAsync(HttpMethod.Get, resource);
        }

        public IRequest PostAsync(string resource)
        {
            return this.SendAsync(HttpMethod.Post, resource);
        }

        public IRequest PostAsync<TBody>(string resource, TBody body)
        {
            return this.PostAsync(resource).WithBody(body);
        }

        public IRequest PutAsync(string resource)
        {
            return this.SendAsync(HttpMethod.Put, resource);
        }

        public IRequest PutAsync<TBody>(string resource, TBody body)
        {
            return this.PutAsync(resource).WithBody(body);
        }

        public virtual IRequest SendAsync(HttpMethod method, string resource)
        {
            this.AssertNotDisposed();

            Uri uri = new Uri(this.BaseClient.BaseAddress, resource);
            HttpRequestMessage message = HttpFactory.GetRequestMessage(method, uri, this.Formatters);
            return this.SendAsync(message);
        }

        public virtual IRequest SendAsync(HttpRequestMessage message)
        {
            this.AssertNotDisposed();
            return new Request(message, this.Formatters, request => this.BaseClient.SendAsync(request.Message), this.Filters.ToArray());
        }

        public virtual void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void AssertNotDisposed()
        {
            if (this.IsDisposed)
                throw new ObjectDisposedException(nameof(WatsonHttpClient));
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (this.IsDisposed)
                return;

            if (isDisposing)
                this.BaseClient.Dispose();

            this.IsDisposed = true;
        }

        ~WatsonHttpClient()
        {
            Dispose(false);
        }
    }
}