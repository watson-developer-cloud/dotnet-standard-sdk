/**
* Copyright 2017 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

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