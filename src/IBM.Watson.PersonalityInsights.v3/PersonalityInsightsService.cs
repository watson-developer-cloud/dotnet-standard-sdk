/**
* (C) Copyright IBM Corp. 2018, 2020.
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

using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using IBM.Cloud.SDK.Core.Authentication;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Service;
using IBM.Watson.PersonalityInsights.v3.Model;
using Newtonsoft.Json;
using System;

namespace IBM.Watson.PersonalityInsights.v3
{
    public partial class PersonalityInsightsService : IBMService, IPersonalityInsightsService
    {
        const string defaultServiceName = "personality_insights";
        private const string defaultServiceUrl = "https://gateway.watsonplatform.net/personality-insights/api";
        public string Version { get; set; }

        public PersonalityInsightsService(string version) : this(version, defaultServiceName, ConfigBasedAuthenticatorFactory.GetAuthenticator(defaultServiceName)) { }
        public PersonalityInsightsService(string version, IAuthenticator authenticator) : this(version, defaultServiceName, authenticator) {}
        public PersonalityInsightsService(string version, string serviceName) : this(version, serviceName, ConfigBasedAuthenticatorFactory.GetAuthenticator(serviceName)) { }
        public PersonalityInsightsService(IClient httpClient) : base(defaultServiceName, httpClient) { }

        public PersonalityInsightsService(string version, string serviceName, IAuthenticator authenticator) : base(serviceName, authenticator)
        {
            if (string.IsNullOrEmpty(version))
            {
                throw new ArgumentNullException("`version` is required");
            }
            Version = version;

            if (string.IsNullOrEmpty(ServiceUrl))
            {
                SetServiceUrl(defaultServiceUrl);
            }
        }

        /// <summary>
        /// Get profile.
        ///
        /// Generates a personality profile for the author of the input text. The service accepts a maximum of 20 MB of
        /// input content, but it requires much less text to produce an accurate profile. The service can analyze text
        /// in Arabic, English, Japanese, Korean, or Spanish. It can return its results in a variety of languages.
        ///
        /// **See also:**
        /// * [Requesting a
        /// profile](https://cloud.ibm.com/docs/services/personality-insights?topic=personality-insights-input#input)
        /// * [Providing sufficient
        /// input](https://cloud.ibm.com/docs/services/personality-insights?topic=personality-insights-input#sufficient)
        ///
        ///
        /// ### Content types
        ///
        ///  You can provide input content as plain text (`text/plain`), HTML (`text/html`), or JSON
        /// (`application/json`) by specifying the **Content-Type** parameter. The default is `text/plain`.
        /// * Per the JSON specification, the default character encoding for JSON content is effectively always UTF-8.
        /// * Per the HTTP specification, the default encoding for plain text and HTML is ISO-8859-1 (effectively, the
        /// ASCII character set).
        ///
        /// When specifying a content type of plain text or HTML, include the `charset` parameter to indicate the
        /// character encoding of the input text; for example, `Content-Type: text/plain;charset=utf-8`.
        ///
        /// **See also:** [Specifying request and response
        /// formats](https://cloud.ibm.com/docs/services/personality-insights?topic=personality-insights-input#formats)
        ///
        /// ### Accept types
        ///
        ///  You must request a response as JSON (`application/json`) or comma-separated values (`text/csv`) by
        /// specifying the **Accept** parameter. CSV output includes a fixed number of columns. Set the **csv_headers**
        /// parameter to `true` to request optional column headers for CSV output.
        ///
        /// **See also:**
        /// * [Understanding a JSON
        /// profile](https://cloud.ibm.com/docs/services/personality-insights?topic=personality-insights-output#output)
        /// * [Understanding a CSV
        /// profile](https://cloud.ibm.com/docs/services/personality-insights?topic=personality-insights-outputCSV#outputCSV).
        /// </summary>
        /// <param name="content">A maximum of 20 MB of content to analyze, though the service requires much less text;
        /// for more information, see [Providing sufficient
        /// input](https://cloud.ibm.com/docs/services/personality-insights?topic=personality-insights-input#sufficient).
        /// For JSON input, provide an object of type `Content`.</param>
        /// <param name="contentType">The type of the input. For more information, see **Content types** in the method
        /// description. (optional, default to text/plain)</param>
        /// <param name="contentLanguage">The language of the input text for the request: Arabic, English, Japanese,
        /// Korean, or Spanish. Regional variants are treated as their parent language; for example, `en-US` is
        /// interpreted as `en`.
        ///
        /// The effect of the **Content-Language** parameter depends on the **Content-Type** parameter. When
        /// **Content-Type** is `text/plain` or `text/html`, **Content-Language** is the only way to specify the
        /// language. When **Content-Type** is `application/json`, **Content-Language** overrides a language specified
        /// with the `language` parameter of a `ContentItem` object, and content items that specify a different language
        /// are ignored; omit this parameter to base the language on the specification of the content items. You can
        /// specify any combination of languages for **Content-Language** and **Accept-Language**. (optional, default to
        /// en)</param>
        /// <param name="acceptLanguage">The desired language of the response. For two-character arguments, regional
        /// variants are treated as their parent language; for example, `en-US` is interpreted as `en`. You can specify
        /// any combination of languages for the input and response content. (optional, default to en)</param>
        /// <param name="rawScores">Indicates whether a raw score in addition to a normalized percentile is returned for
        /// each characteristic; raw scores are not compared with a sample population. By default, only normalized
        /// percentiles are returned. (optional, default to false)</param>
        /// <param name="csvHeaders">Indicates whether column labels are returned with a CSV response. By default, no
        /// column labels are returned. Applies only when the response type is CSV (`text/csv`). (optional, default to
        /// false)</param>
        /// <param name="consumptionPreferences">Indicates whether consumption preferences are returned with the
        /// results. By default, no consumption preferences are returned. (optional, default to false)</param>
        /// <returns><see cref="Profile" />Profile</returns>
        public DetailedResponse<Profile> Profile(Content content, string contentType = null, string contentLanguage = null, string acceptLanguage = null, bool? rawScores = null, bool? csvHeaders = null, bool? consumptionPreferences = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (content == null)
            {
                throw new ArgumentNullException("`content` is required for `Profile`");
            }
            DetailedResponse<Profile> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v3/profile");

                restRequest.WithHeader("Accept", "application/json");

                if (!string.IsNullOrEmpty(contentType))
                {
                    restRequest.WithHeader("Content-Type", contentType);
                }

                if (!string.IsNullOrEmpty(contentLanguage))
                {
                    restRequest.WithHeader("Content-Language", contentLanguage);
                }

                if (!string.IsNullOrEmpty(acceptLanguage))
                {
                    restRequest.WithHeader("Accept-Language", acceptLanguage);
                }
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (rawScores != null)
                {
                    restRequest.WithArgument("raw_scores", rawScores);
                }
                if (csvHeaders != null)
                {
                    restRequest.WithArgument("csv_headers", csvHeaders);
                }
                if (consumptionPreferences != null)
                {
                    restRequest.WithArgument("consumption_preferences", consumptionPreferences);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders(ServiceName, "v3", "Profile"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Profile>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Profile>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for Profile.
        /// </summary>
        public class ProfileEnums
        {
            /// <summary>
            /// The type of the input. For more information, see **Content types** in the method description.
            /// </summary>
            public class ContentTypeValue
            {
                /// <summary>
                /// Constant APPLICATION_JSON for application/json
                /// </summary>
                public const string APPLICATION_JSON = "application/json";
                /// <summary>
                /// Constant TEXT_HTML for text/html
                /// </summary>
                public const string TEXT_HTML = "text/html";
                /// <summary>
                /// Constant TEXT_PLAIN for text/plain
                /// </summary>
                public const string TEXT_PLAIN = "text/plain";
                
            }
            /// <summary>
            /// The language of the input text for the request: Arabic, English, Japanese, Korean, or Spanish. Regional
            /// variants are treated as their parent language; for example, `en-US` is interpreted as `en`.
            ///
            /// The effect of the **Content-Language** parameter depends on the **Content-Type** parameter. When
            /// **Content-Type** is `text/plain` or `text/html`, **Content-Language** is the only way to specify the
            /// language. When **Content-Type** is `application/json`, **Content-Language** overrides a language
            /// specified with the `language` parameter of a `ContentItem` object, and content items that specify a
            /// different language are ignored; omit this parameter to base the language on the specification of the
            /// content items. You can specify any combination of languages for **Content-Language** and
            /// **Accept-Language**.
            /// </summary>
            public class ContentLanguageValue
            {
                /// <summary>
                /// Constant AR for ar
                /// </summary>
                public const string AR = "ar";
                /// <summary>
                /// Constant EN for en
                /// </summary>
                public const string EN = "en";
                /// <summary>
                /// Constant ES for es
                /// </summary>
                public const string ES = "es";
                /// <summary>
                /// Constant JA for ja
                /// </summary>
                public const string JA = "ja";
                /// <summary>
                /// Constant KO for ko
                /// </summary>
                public const string KO = "ko";
                
            }
            /// <summary>
            /// The desired language of the response. For two-character arguments, regional variants are treated as
            /// their parent language; for example, `en-US` is interpreted as `en`. You can specify any combination of
            /// languages for the input and response content.
            /// </summary>
            public class AcceptLanguageValue
            {
                /// <summary>
                /// Constant AR for ar
                /// </summary>
                public const string AR = "ar";
                /// <summary>
                /// Constant DE for de
                /// </summary>
                public const string DE = "de";
                /// <summary>
                /// Constant EN for en
                /// </summary>
                public const string EN = "en";
                /// <summary>
                /// Constant ES for es
                /// </summary>
                public const string ES = "es";
                /// <summary>
                /// Constant FR for fr
                /// </summary>
                public const string FR = "fr";
                /// <summary>
                /// Constant IT for it
                /// </summary>
                public const string IT = "it";
                /// <summary>
                /// Constant JA for ja
                /// </summary>
                public const string JA = "ja";
                /// <summary>
                /// Constant KO for ko
                /// </summary>
                public const string KO = "ko";
                /// <summary>
                /// Constant PT_BR for pt-br
                /// </summary>
                public const string PT_BR = "pt-br";
                /// <summary>
                /// Constant ZH_CN for zh-cn
                /// </summary>
                public const string ZH_CN = "zh-cn";
                /// <summary>
                /// Constant ZH_TW for zh-tw
                /// </summary>
                public const string ZH_TW = "zh-tw";
                
            }
        }

        /// <summary>
        /// The type of the input. For more information, see **Content types** in the method description.
        /// </summary>
        public class ProfileContentTypeEnumValue
        {
            /// <summary>
            /// Constant APPLICATION_JSON for application/json
            /// </summary>
            public const string APPLICATION_JSON = "application/json";
            /// <summary>
            /// Constant TEXT_HTML for text/html
            /// </summary>
            public const string TEXT_HTML = "text/html";
            /// <summary>
            /// Constant TEXT_PLAIN for text/plain
            /// </summary>
            public const string TEXT_PLAIN = "text/plain";
            
        }
        /// <summary>
        /// The language of the input text for the request: Arabic, English, Japanese, Korean, or Spanish. Regional
        /// variants are treated as their parent language; for example, `en-US` is interpreted as `en`.
        ///
        /// The effect of the **Content-Language** parameter depends on the **Content-Type** parameter. When
        /// **Content-Type** is `text/plain` or `text/html`, **Content-Language** is the only way to specify the
        /// language. When **Content-Type** is `application/json`, **Content-Language** overrides a language specified
        /// with the `language` parameter of a `ContentItem` object, and content items that specify a different language
        /// are ignored; omit this parameter to base the language on the specification of the content items. You can
        /// specify any combination of languages for **Content-Language** and **Accept-Language**.
        /// </summary>
        public class ProfileContentLanguageEnumValue
        {
            /// <summary>
            /// Constant AR for ar
            /// </summary>
            public const string AR = "ar";
            /// <summary>
            /// Constant EN for en
            /// </summary>
            public const string EN = "en";
            /// <summary>
            /// Constant ES for es
            /// </summary>
            public const string ES = "es";
            /// <summary>
            /// Constant JA for ja
            /// </summary>
            public const string JA = "ja";
            /// <summary>
            /// Constant KO for ko
            /// </summary>
            public const string KO = "ko";
            
        }
        /// <summary>
        /// The desired language of the response. For two-character arguments, regional variants are treated as their
        /// parent language; for example, `en-US` is interpreted as `en`. You can specify any combination of languages
        /// for the input and response content.
        /// </summary>
        public class ProfileAcceptLanguageEnumValue
        {
            /// <summary>
            /// Constant AR for ar
            /// </summary>
            public const string AR = "ar";
            /// <summary>
            /// Constant DE for de
            /// </summary>
            public const string DE = "de";
            /// <summary>
            /// Constant EN for en
            /// </summary>
            public const string EN = "en";
            /// <summary>
            /// Constant ES for es
            /// </summary>
            public const string ES = "es";
            /// <summary>
            /// Constant FR for fr
            /// </summary>
            public const string FR = "fr";
            /// <summary>
            /// Constant IT for it
            /// </summary>
            public const string IT = "it";
            /// <summary>
            /// Constant JA for ja
            /// </summary>
            public const string JA = "ja";
            /// <summary>
            /// Constant KO for ko
            /// </summary>
            public const string KO = "ko";
            /// <summary>
            /// Constant PT_BR for pt-br
            /// </summary>
            public const string PT_BR = "pt-br";
            /// <summary>
            /// Constant ZH_CN for zh-cn
            /// </summary>
            public const string ZH_CN = "zh-cn";
            /// <summary>
            /// Constant ZH_TW for zh-tw
            /// </summary>
            public const string ZH_TW = "zh-tw";
            
        }
        /// <summary>
        /// Get profile as csv.
        ///
        /// Generates a personality profile for the author of the input text. The service accepts a maximum of 20 MB of
        /// input content, but it requires much less text to produce an accurate profile. The service can analyze text
        /// in Arabic, English, Japanese, Korean, or Spanish. It can return its results in a variety of languages.
        ///
        /// **See also:**
        /// * [Requesting a
        /// profile](https://cloud.ibm.com/docs/services/personality-insights?topic=personality-insights-input#input)
        /// * [Providing sufficient
        /// input](https://cloud.ibm.com/docs/services/personality-insights?topic=personality-insights-input#sufficient)
        ///
        ///
        /// ### Content types
        ///
        ///  You can provide input content as plain text (`text/plain`), HTML (`text/html`), or JSON
        /// (`application/json`) by specifying the **Content-Type** parameter. The default is `text/plain`.
        /// * Per the JSON specification, the default character encoding for JSON content is effectively always UTF-8.
        /// * Per the HTTP specification, the default encoding for plain text and HTML is ISO-8859-1 (effectively, the
        /// ASCII character set).
        ///
        /// When specifying a content type of plain text or HTML, include the `charset` parameter to indicate the
        /// character encoding of the input text; for example, `Content-Type: text/plain;charset=utf-8`.
        ///
        /// **See also:** [Specifying request and response
        /// formats](https://cloud.ibm.com/docs/services/personality-insights?topic=personality-insights-input#formats)
        ///
        /// ### Accept types
        ///
        ///  You must request a response as JSON (`application/json`) or comma-separated values (`text/csv`) by
        /// specifying the **Accept** parameter. CSV output includes a fixed number of columns. Set the **csv_headers**
        /// parameter to `true` to request optional column headers for CSV output.
        ///
        /// **See also:**
        /// * [Understanding a JSON
        /// profile](https://cloud.ibm.com/docs/services/personality-insights?topic=personality-insights-output#output)
        /// * [Understanding a CSV
        /// profile](https://cloud.ibm.com/docs/services/personality-insights?topic=personality-insights-outputCSV#outputCSV).
        /// </summary>
        /// <param name="content">A maximum of 20 MB of content to analyze, though the service requires much less text;
        /// for more information, see [Providing sufficient
        /// input](https://cloud.ibm.com/docs/services/personality-insights?topic=personality-insights-input#sufficient).
        /// For JSON input, provide an object of type `Content`.</param>
        /// <param name="contentType">The type of the input. For more information, see **Content types** in the method
        /// description. (optional, default to text/plain)</param>
        /// <param name="contentLanguage">The language of the input text for the request: Arabic, English, Japanese,
        /// Korean, or Spanish. Regional variants are treated as their parent language; for example, `en-US` is
        /// interpreted as `en`.
        ///
        /// The effect of the **Content-Language** parameter depends on the **Content-Type** parameter. When
        /// **Content-Type** is `text/plain` or `text/html`, **Content-Language** is the only way to specify the
        /// language. When **Content-Type** is `application/json`, **Content-Language** overrides a language specified
        /// with the `language` parameter of a `ContentItem` object, and content items that specify a different language
        /// are ignored; omit this parameter to base the language on the specification of the content items. You can
        /// specify any combination of languages for **Content-Language** and **Accept-Language**. (optional, default to
        /// en)</param>
        /// <param name="acceptLanguage">The desired language of the response. For two-character arguments, regional
        /// variants are treated as their parent language; for example, `en-US` is interpreted as `en`. You can specify
        /// any combination of languages for the input and response content. (optional, default to en)</param>
        /// <param name="rawScores">Indicates whether a raw score in addition to a normalized percentile is returned for
        /// each characteristic; raw scores are not compared with a sample population. By default, only normalized
        /// percentiles are returned. (optional, default to false)</param>
        /// <param name="csvHeaders">Indicates whether column labels are returned with a CSV response. By default, no
        /// column labels are returned. Applies only when the response type is CSV (`text/csv`). (optional, default to
        /// false)</param>
        /// <param name="consumptionPreferences">Indicates whether consumption preferences are returned with the
        /// results. By default, no consumption preferences are returned. (optional, default to false)</param>
        /// <returns><see cref="System.IO.MemoryStream" />System.IO.MemoryStream</returns>
        public DetailedResponse<System.IO.MemoryStream> ProfileAsCsv(Content content, string contentType = null, string contentLanguage = null, string acceptLanguage = null, bool? rawScores = null, bool? csvHeaders = null, bool? consumptionPreferences = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (content == null)
            {
                throw new ArgumentNullException("`content` is required for `ProfileAsCsv`");
            }
            DetailedResponse<System.IO.MemoryStream> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v3/profile");

                restRequest.WithHeader("Accept", "text/csv");

                if (!string.IsNullOrEmpty(contentType))
                {
                    restRequest.WithHeader("Content-Type", contentType);
                }

                if (!string.IsNullOrEmpty(contentLanguage))
                {
                    restRequest.WithHeader("Content-Language", contentLanguage);
                }

                if (!string.IsNullOrEmpty(acceptLanguage))
                {
                    restRequest.WithHeader("Accept-Language", acceptLanguage);
                }
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (rawScores != null)
                {
                    restRequest.WithArgument("raw_scores", rawScores);
                }
                if (csvHeaders != null)
                {
                    restRequest.WithArgument("csv_headers", csvHeaders);
                }
                if (consumptionPreferences != null)
                {
                    restRequest.WithArgument("consumption_preferences", consumptionPreferences);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders(ServiceName, "v3", "ProfileAsCsv"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = new DetailedResponse<System.IO.MemoryStream>();
                result.Result = new System.IO.MemoryStream(restRequest.AsByteArray().Result);
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for ProfileAsCsv.
        /// </summary>
        public class ProfileAsCsvEnums
        {
            /// <summary>
            /// The type of the input. For more information, see **Content types** in the method description.
            /// </summary>
            public class ContentTypeValue
            {
                /// <summary>
                /// Constant APPLICATION_JSON for application/json
                /// </summary>
                public const string APPLICATION_JSON = "application/json";
                /// <summary>
                /// Constant TEXT_HTML for text/html
                /// </summary>
                public const string TEXT_HTML = "text/html";
                /// <summary>
                /// Constant TEXT_PLAIN for text/plain
                /// </summary>
                public const string TEXT_PLAIN = "text/plain";
                
            }
            /// <summary>
            /// The language of the input text for the request: Arabic, English, Japanese, Korean, or Spanish. Regional
            /// variants are treated as their parent language; for example, `en-US` is interpreted as `en`.
            ///
            /// The effect of the **Content-Language** parameter depends on the **Content-Type** parameter. When
            /// **Content-Type** is `text/plain` or `text/html`, **Content-Language** is the only way to specify the
            /// language. When **Content-Type** is `application/json`, **Content-Language** overrides a language
            /// specified with the `language` parameter of a `ContentItem` object, and content items that specify a
            /// different language are ignored; omit this parameter to base the language on the specification of the
            /// content items. You can specify any combination of languages for **Content-Language** and
            /// **Accept-Language**.
            /// </summary>
            public class ContentLanguageValue
            {
                /// <summary>
                /// Constant AR for ar
                /// </summary>
                public const string AR = "ar";
                /// <summary>
                /// Constant EN for en
                /// </summary>
                public const string EN = "en";
                /// <summary>
                /// Constant ES for es
                /// </summary>
                public const string ES = "es";
                /// <summary>
                /// Constant JA for ja
                /// </summary>
                public const string JA = "ja";
                /// <summary>
                /// Constant KO for ko
                /// </summary>
                public const string KO = "ko";
                
            }
            /// <summary>
            /// The desired language of the response. For two-character arguments, regional variants are treated as
            /// their parent language; for example, `en-US` is interpreted as `en`. You can specify any combination of
            /// languages for the input and response content.
            /// </summary>
            public class AcceptLanguageValue
            {
                /// <summary>
                /// Constant AR for ar
                /// </summary>
                public const string AR = "ar";
                /// <summary>
                /// Constant DE for de
                /// </summary>
                public const string DE = "de";
                /// <summary>
                /// Constant EN for en
                /// </summary>
                public const string EN = "en";
                /// <summary>
                /// Constant ES for es
                /// </summary>
                public const string ES = "es";
                /// <summary>
                /// Constant FR for fr
                /// </summary>
                public const string FR = "fr";
                /// <summary>
                /// Constant IT for it
                /// </summary>
                public const string IT = "it";
                /// <summary>
                /// Constant JA for ja
                /// </summary>
                public const string JA = "ja";
                /// <summary>
                /// Constant KO for ko
                /// </summary>
                public const string KO = "ko";
                /// <summary>
                /// Constant PT_BR for pt-br
                /// </summary>
                public const string PT_BR = "pt-br";
                /// <summary>
                /// Constant ZH_CN for zh-cn
                /// </summary>
                public const string ZH_CN = "zh-cn";
                /// <summary>
                /// Constant ZH_TW for zh-tw
                /// </summary>
                public const string ZH_TW = "zh-tw";
                
            }
        }
        /// <summary>
        /// The type of the input. For more information, see **Content types** in the method description.
        /// </summary>
        public class ProfileAsCsvContentTypeEnumValue
        {
            /// <summary>
            /// Constant APPLICATION_JSON for application/json
            /// </summary>
            public const string APPLICATION_JSON = "application/json";
            /// <summary>
            /// Constant TEXT_HTML for text/html
            /// </summary>
            public const string TEXT_HTML = "text/html";
            /// <summary>
            /// Constant TEXT_PLAIN for text/plain
            /// </summary>
            public const string TEXT_PLAIN = "text/plain";
            
        }
        /// <summary>
        /// The language of the input text for the request: Arabic, English, Japanese, Korean, or Spanish. Regional
        /// variants are treated as their parent language; for example, `en-US` is interpreted as `en`.
        ///
        /// The effect of the **Content-Language** parameter depends on the **Content-Type** parameter. When
        /// **Content-Type** is `text/plain` or `text/html`, **Content-Language** is the only way to specify the
        /// language. When **Content-Type** is `application/json`, **Content-Language** overrides a language specified
        /// with the `language` parameter of a `ContentItem` object, and content items that specify a different language
        /// are ignored; omit this parameter to base the language on the specification of the content items. You can
        /// specify any combination of languages for **Content-Language** and **Accept-Language**.
        /// </summary>
        public class ProfileAsCsvContentLanguageEnumValue
        {
            /// <summary>
            /// Constant AR for ar
            /// </summary>
            public const string AR = "ar";
            /// <summary>
            /// Constant EN for en
            /// </summary>
            public const string EN = "en";
            /// <summary>
            /// Constant ES for es
            /// </summary>
            public const string ES = "es";
            /// <summary>
            /// Constant JA for ja
            /// </summary>
            public const string JA = "ja";
            /// <summary>
            /// Constant KO for ko
            /// </summary>
            public const string KO = "ko";
            
        }
        /// <summary>
        /// The desired language of the response. For two-character arguments, regional variants are treated as their
        /// parent language; for example, `en-US` is interpreted as `en`. You can specify any combination of languages
        /// for the input and response content.
        /// </summary>
        public class ProfileAsCsvAcceptLanguageEnumValue
        {
            /// <summary>
            /// Constant AR for ar
            /// </summary>
            public const string AR = "ar";
            /// <summary>
            /// Constant DE for de
            /// </summary>
            public const string DE = "de";
            /// <summary>
            /// Constant EN for en
            /// </summary>
            public const string EN = "en";
            /// <summary>
            /// Constant ES for es
            /// </summary>
            public const string ES = "es";
            /// <summary>
            /// Constant FR for fr
            /// </summary>
            public const string FR = "fr";
            /// <summary>
            /// Constant IT for it
            /// </summary>
            public const string IT = "it";
            /// <summary>
            /// Constant JA for ja
            /// </summary>
            public const string JA = "ja";
            /// <summary>
            /// Constant KO for ko
            /// </summary>
            public const string KO = "ko";
            /// <summary>
            /// Constant PT_BR for pt-br
            /// </summary>
            public const string PT_BR = "pt-br";
            /// <summary>
            /// Constant ZH_CN for zh-cn
            /// </summary>
            public const string ZH_CN = "zh-cn";
            /// <summary>
            /// Constant ZH_TW for zh-tw
            /// </summary>
            public const string ZH_TW = "zh-tw";
            
        }
    }
}
