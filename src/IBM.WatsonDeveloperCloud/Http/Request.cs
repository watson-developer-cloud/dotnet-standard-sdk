using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using IBM.WatsonDeveloperCloud.Http.Extensions;
using IBM.WatsonDeveloperCloud.Http.Filters;

namespace IBM.WatsonDeveloperCloud.Http
{
    public sealed class Request : IRequest
    {
        private readonly IHttpFilter[] Filters;

        private readonly Lazy<Task<HttpResponseMessage>> Dispatch;

        public HttpRequestMessage Message { get; }

        public MediaTypeFormatterCollection Formatters { get; }

        public Request(HttpRequestMessage message, MediaTypeFormatterCollection formatters, Func<IRequest, Task<HttpResponseMessage>> dispatcher, IHttpFilter[] filters)
        {
            this.Message = message;
            this.Formatters = formatters;
            this.Dispatch = new Lazy<Task<HttpResponseMessage>>(() => dispatcher(this));
            this.Filters = filters;
        }
        
        public IRequest WithBody<T>(T body, MediaTypeHeaderValue contentType = null)
        {
            MediaTypeFormatter formatter = HttpFactory.GetFormatter(this.Formatters, contentType);
            string mediaType = contentType != null ? contentType.MediaType : null;
            return this.WithBody(body, formatter, mediaType);
        }

        public IRequest WithBody<T>(T body, MediaTypeFormatter formatter, string mediaType = null)
        {
            return this.WithBodyContent(new ObjectContent<T>(body, formatter, mediaType));
        }

        public IRequest WithBodyContent(HttpContent body)
        {
            this.Message.Content = body;
            return this;
        }

        public IRequest WithHeader(string key, string value)
        {
            this.Message.Headers.Add(key, value);
            return this;
        }

        public IRequest WithArgument(string key, object value)
        {
            this.Message.RequestUri = this.Message.RequestUri.WithArguments(new KeyValuePair<string, object>(key, value));
            return this;
        }

        public IRequest WithArguments(object arguments)
        {
            this.Message.RequestUri = this.Message.RequestUri.WithArguments(this.GetArguments(arguments).ToArray());
            return this;
        }

        public IRequest WithCustom(Action<HttpRequestMessage> request)
        {
            request(this.Message);
            return this;
        }

        public TaskAwaiter<IResponse> GetAwaiter()
        {
            Func<Task<IResponse>> waiter = async () =>
            {
                await this.AsMessage();
                return this;
            };
            return waiter().GetAwaiter();
        }

        public async Task<HttpResponseMessage> AsMessage()
        {
            return await this.GetResponse(this.Dispatch.Value).ConfigureAwait(false);
        }

        public async Task<T> As<T>()
        {
            HttpResponseMessage message = await this.AsMessage().ConfigureAwait(false);
            return await message.Content.ReadAsAsync<T>(this.Formatters).ConfigureAwait(false);
        }

        public Task<List<T>> AsList<T>()
        {
            return this.As<List<T>>();
        }

        public async Task<byte[]> AsByteArray()
        {
            HttpResponseMessage message = await this.AsMessage().ConfigureAwait(false);
            return await message.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
        }

        public async Task<string> AsString()
        {
            HttpResponseMessage message = await this.AsMessage().ConfigureAwait(false);
            return await message.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        public async Task<Stream> AsStream()
        {
            HttpResponseMessage message = await this.AsMessage().ConfigureAwait(false);
            Stream stream = await message.Content.ReadAsStreamAsync().ConfigureAwait(false);
            stream.Position = 0;
            return stream;
        }

        private async Task<HttpResponseMessage> GetResponse(Task<HttpResponseMessage> request)
        {
            foreach (IHttpFilter filter in this.Filters)
                filter.OnRequest(this, this.Message);
            HttpResponseMessage response = await request.ConfigureAwait(false);
            foreach (IHttpFilter filter in this.Filters)
                filter.OnResponse(this, response);
            return response;
        }

        private IDictionary<string, object> GetArguments(object arguments)
        {
            // null
            if (arguments == null)
                return new Dictionary<string, object>();

            // generic dictionary
            if (arguments is IDictionary<string, object>)
                return (IDictionary<string, object>)arguments;

            // dictionary
            if (arguments is IDictionary)
            {
                IDictionary<string, object> dict = new Dictionary<string, object>();
                IDictionary argDict = (IDictionary)arguments;
                foreach (var key in argDict.Keys)
                    dict.Add(key.ToString(), argDict[key]);
                return dict;
            }

            // object
            return arguments.GetType()
                .GetRuntimeProperties()
                .Where(p => p.CanRead)
                .ToDictionary(p => p.Name, p => p.GetValue(arguments));
        }
    }
}