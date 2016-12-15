using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace IBM.WatsonDeveloperCloud.Http.Extensions
{
    internal static class UriExtensions
    {
        public static Uri WithArguments(this Uri uri, params KeyValuePair<string, object>[] arguments)
        {
            string queryString = string.Join("&",
                from argument in arguments
                let key = WebUtility.UrlEncode(argument.Key)
                let value = argument.Value != null ? WebUtility.UrlEncode(argument.Value.ToString()) : string.Empty
                select key + "=" + value
            );
            return new Uri(
                uri
                + (string.IsNullOrWhiteSpace(uri.Query) ? "?" : "&")
                + queryString
            );
        }
    }
}