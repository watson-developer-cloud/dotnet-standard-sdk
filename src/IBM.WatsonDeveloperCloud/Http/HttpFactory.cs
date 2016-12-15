using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace IBM.WatsonDeveloperCloud.Http
{
    internal static class HttpFactory
    {
        public static MediaTypeFormatter GetFormatter(MediaTypeFormatterCollection formatters, MediaTypeHeaderValue contentType = null)
        {
            if (!formatters.Any())
                throw new InvalidOperationException("No MediaTypeFormatters are available on the fluent client.");

            MediaTypeFormatter formatter = contentType != null
                ? formatters.FirstOrDefault(f => f.SupportedMediaTypes.Any(m => m.MediaType == contentType.MediaType))
                : formatters.FirstOrDefault();
            if (formatter == null)
                throw new InvalidOperationException(String.Format("No MediaTypeFormatters are available on the fluent client for the '{0}' content-type.", contentType));

            return formatter;
        }

        public static HttpRequestMessage GetRequestMessage(HttpMethod method, Uri resource, MediaTypeFormatterCollection formatters)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, resource);

            // add default headers
            request.Headers.Add("accept", formatters.SelectMany(p => p.SupportedMediaTypes).Select(p => p.MediaType));

            return request;
        }
    }
}