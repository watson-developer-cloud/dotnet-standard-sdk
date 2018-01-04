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

using IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Model;

namespace IBM.WatsonDeveloperCloud.PersonalityInsights.v3
{
    public interface IPersonalityInsightsService
    {
        /// <summary>
        /// Generates a personality profile based on input text. Derives personality insights for up to 20 MB of input content written by an author, though the service requires much less text to produce an accurate profile; for more information, see [Guidelines for providing sufficient input](https://console.bluemix.net/docs/services/personality-insights/input.html#sufficient). Accepts input in Arabic, English, Japanese, or Spanish and produces output in one of eleven languages. Provide plain text, HTML, or JSON content, and receive results in JSON or CSV format.
        /// </summary>
        /// <param name="contentType">The content type of the request: plain text (the default), HTML, or JSON. Per the JSON specification, the default character encoding for JSON content is effectively always UTF-8; per the HTTP specification, the default encoding for plain text and HTML is ISO-8859-1 (effectively, the ASCII character set). When specifying a content type of plain text or HTML, include the `charset` parameter to indicate the character encoding of the input text, for example, `Content-Type: text/plain;charset=utf-8`.</param>
        /// <param name="accept">The desired content type of the response: JSON (the default) or CSV. CSV output includes a fixed number of columns and optional headers.</param>
        /// <param name="body">A maximum of 20 MB of content to analyze, though the service requires much less text; for more information, see [Guidelines for providing sufficient input](https://console.bluemix.net/docs/services/personality-insights/input.html#sufficient). A JSON request must conform to the `ContentListContainer` model.</param>
        /// <param name="contentLanguage">The language of the input text for the request: Arabic, English, Spanish, or Japanese. Regional variants are treated as their parent language; for example, `en-US` is interpreted as `en`. The effect of the `Content-Language` header depends on the `Content-Type` header. When `Content-Type` is `text/plain` or `text/html`, `Content-Language` is the only way to specify the language. When `Content-Type` is `application/json`, `Content-Language` overrides a language specified with the `language` parameter of a `ContentItem` object, and content items that specify a different language are ignored; omit this header to base the language on the specification of the content items. You can specify any combination of languages for `Content-Language` and `Accept-Language`. (optional, default to en)</param>
        /// <param name="acceptLanguage">The desired language of the response. For two-character arguments, regional variants are treated as their parent language; for example, `en-US` is interpreted as `en`. You can specify any combination of languages for the input and response content. (optional, default to en)</param>
        /// <param name="rawScores">If `true`, a raw score in addition to a normalized percentile is returned for each characteristic; raw scores are not compared with a sample population. If `false` (the default), only normalized percentiles are returned. (optional, default to false)</param>
        /// <param name="csvHeaders">If `true`, column labels are returned with a CSV response; if `false` (the default), they are not. Applies only when the `Accept` header is set to `text/csv`. (optional, default to false)</param>
        /// <param name="consumptionPreferences">If `true`, information about consumption preferences is returned with the results; if `false` (the default), the response does not include the information. (optional, default to false)</param>
        /// <returns><see cref="Profile" />Profile</returns>
        Profile Profile(string contentType, string accept, ContentListContainer body, string contentLanguage = null, string acceptLanguage = null, bool? rawScores = null, bool? csvHeaders = null, bool? consumptionPreferences = null);
    }
}
