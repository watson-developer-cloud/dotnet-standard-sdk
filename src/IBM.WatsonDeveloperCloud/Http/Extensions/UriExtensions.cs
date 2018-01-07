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
                let value = argument.Value != null ?
                                argument.Value is bool ? 
                                    WebUtility.UrlEncode(argument.Value.ToString().ToLower()) : 
                                        WebUtility.UrlEncode(argument.Value.ToString()) : 
                                            string.Empty
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