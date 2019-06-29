/**
* (C) Copyright IBM Corp. 2018, 2019.
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
using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Service;
using IBM.Cloud.SDK.Core.Util;
using IBM.Watson.PersonalityInsights.v3.Model;
using Newtonsoft.Json;
using System;

namespace IBM.Watson.PersonalityInsights.v3
{
    public partial class PersonalityInsightsService : IBMService, IPersonalityInsightsService
    {
        new const string SERVICE_NAME = "personality_insights";
        const string URL = "https://gateway.watsonplatform.net/personality-insights/api";
        public new string DefaultEndpoint = "https://gateway.watsonplatform.net/personality-insights/api";
        private string _versionDate;
        public string VersionDate
        {
            get { return _versionDate; }
            set { _versionDate = value; }
        }

        public PersonalityInsightsService() : base(SERVICE_NAME) { }
        
        [Obsolete("Please use PersonalityInsightsService(string versionDate, IAuthenticatorConfig config) instead")]
        public PersonalityInsightsService(string userName, string password, string versionDate) : base(SERVICE_NAME, URL)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);
            if (string.IsNullOrEmpty(versionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            VersionDate = versionDate;
        }
        
        [Obsolete("Please use PersonalityInsightsService(string versionDate, IAuthenticatorConfig config) instead")]
        public PersonalityInsightsService(TokenOptions options, string versionDate) : base(SERVICE_NAME, URL)
        {
            if (string.IsNullOrEmpty(options.IamApiKey) && string.IsNullOrEmpty(options.IamAccessToken))
                throw new ArgumentNullException(nameof(options.IamAccessToken) + ", " + nameof(options.IamApiKey));
            if (string.IsNullOrEmpty(versionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            VersionDate = versionDate;

            if (!string.IsNullOrEmpty(options.ServiceUrl))
            {
                this.Endpoint = options.ServiceUrl;
            }
            else
            {
                options.ServiceUrl = this.Endpoint;
            }

            IamConfig iamConfig = null;
            if (!string.IsNullOrEmpty(options.IamAccessToken))
            {
                iamConfig = new IamConfig(
                    userManagedAccessToken: options.IamAccessToken
                    );
            }
            else
            {
                iamConfig = new IamConfig(
                    apikey: options.IamApiKey,
                    iamUrl: options.IamUrl
                    );
            }

            SetAuthenticator(iamConfig);
        }

        public PersonalityInsightsService(IClient httpClient) : base(SERVICE_NAME, URL)
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
            SkipAuthentication = true;
        }

        public PersonalityInsightsService(string versionDate, IAuthenticatorConfig config) : base(SERVICE_NAME, config)
        {
            VersionDate = versionDate;
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
        /// description.
        ///
        /// Default: `text/plain`. (optional)</param>
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
            if (content == null)
            {
                throw new ArgumentNullException("`content` is required for `Profile`");
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<Profile> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v3/profile");

                restRequest.WithArgument("version", VersionDate);
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

                restRequest.WithHeaders(Common.GetSdkHeaders("personality_insights", "v3", "Profile"));
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
        /// description.
        ///
        /// Default: `text/plain`. (optional)</param>
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
            if (content == null)
            {
                throw new ArgumentNullException("`content` is required for `ProfileAsCsv`");
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<System.IO.MemoryStream> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v3/profile");

                restRequest.WithArgument("version", VersionDate);
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

                restRequest.WithHeaders(Common.GetSdkHeaders("personality_insights", "v3", "ProfileAsCsv"));
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
    }
}
