/**
* (C) Copyright IBM Corp. 2018, 2022.
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

/**
* IBM OpenAPI SDK Code Generator Version: 3.53.0-9710cac3-20220713-193508
*/
 
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using IBM.Cloud.SDK.Core.Authentication;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Service;
using IBM.Watson.Assistant.v2.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using Environment = IBM.Watson.Assistant.v2.Model.Environment;

namespace IBM.Watson.Assistant.v2
{
    public partial class AssistantService : IBMService, IAssistantService
    {
        const string defaultServiceName = "assistant";
        private const string defaultServiceUrl = "https://api.us-south.assistant.watson.cloud.ibm.com";
        public string Version { get; set; }

        public AssistantService(string version) : this(version, defaultServiceName, ConfigBasedAuthenticatorFactory.GetAuthenticator(defaultServiceName)) { }
        public AssistantService(string version, IAuthenticator authenticator) : this(version, defaultServiceName, authenticator) {}
        public AssistantService(string version, string serviceName) : this(version, serviceName, ConfigBasedAuthenticatorFactory.GetAuthenticator(serviceName)) { }
        public AssistantService(IClient httpClient) : base(defaultServiceName, httpClient) { }

        public AssistantService(string version, string serviceName, IAuthenticator authenticator) : base(serviceName, authenticator)
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
        /// Create a session.
        ///
        /// Create a new session. A session is used to send user input to a skill and receive responses. It also
        /// maintains the state of the conversation. A session persists until it is deleted, or until it times out
        /// because of inactivity. (For more information, see the
        /// [documentation](https://cloud.ibm.com/docs/assistant?topic=assistant-assistant-settings).
        /// </summary>
        /// <param name="assistantId">Unique identifier of the assistant. To find the assistant ID in the Watson
        /// Assistant user interface, open the assistant settings and click **API Details**. For information about
        /// creating assistants, see the
        /// [documentation](https://cloud.ibm.com/docs/assistant?topic=assistant-assistant-add#assistant-add-task).
        ///
        /// **Note:** Currently, the v2 API does not support creating assistants.</param>
        /// <returns><see cref="SessionResponse" />SessionResponse</returns>
        public DetailedResponse<SessionResponse> CreateSession(string assistantId)
        {
            if (string.IsNullOrEmpty(assistantId))
            {
                throw new ArgumentNullException("`assistantId` is required for `CreateSession`");
            }
            else
            {
                assistantId = Uri.EscapeDataString(assistantId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<SessionResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/assistants/{assistantId}/sessions");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v2", "CreateSession"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<SessionResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<SessionResponse>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete session.
        ///
        /// Deletes a session explicitly before it times out. (For more information about the session inactivity
        /// timeout, see the [documentation](https://cloud.ibm.com/docs/assistant?topic=assistant-assistant-settings)).
        /// </summary>
        /// <param name="assistantId">Unique identifier of the assistant. To find the assistant ID in the Watson
        /// Assistant user interface, open the assistant settings and click **API Details**. For information about
        /// creating assistants, see the
        /// [documentation](https://cloud.ibm.com/docs/assistant?topic=assistant-assistant-add#assistant-add-task).
        ///
        /// **Note:** Currently, the v2 API does not support creating assistants.</param>
        /// <param name="sessionId">Unique identifier of the session.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteSession(string assistantId, string sessionId)
        {
            if (string.IsNullOrEmpty(assistantId))
            {
                throw new ArgumentNullException("`assistantId` is required for `DeleteSession`");
            }
            else
            {
                assistantId = Uri.EscapeDataString(assistantId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(sessionId))
            {
                throw new ArgumentNullException("`sessionId` is required for `DeleteSession`");
            }
            else
            {
                sessionId = Uri.EscapeDataString(sessionId);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v2/assistants/{assistantId}/sessions/{sessionId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v2", "DeleteSession"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<object>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<object>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Send user input to assistant (stateful).
        ///
        /// Send user input to an assistant and receive a response, with conversation state (including context data)
        /// stored by Watson Assistant for the duration of the session.
        /// </summary>
        /// <param name="assistantId">Unique identifier of the assistant. To find the assistant ID in the Watson
        /// Assistant user interface, open the assistant settings and click **API Details**. For information about
        /// creating assistants, see the
        /// [documentation](https://cloud.ibm.com/docs/assistant?topic=assistant-assistant-add#assistant-add-task).
        ///
        /// **Note:** Currently, the v2 API does not support creating assistants.</param>
        /// <param name="sessionId">Unique identifier of the session.</param>
        /// <param name="input">An input object that includes the input text. (optional)</param>
        /// <param name="context">Context data for the conversation. You can use this property to set or modify context
        /// variables, which can also be accessed by dialog nodes. The context is stored by the assistant on a
        /// per-session basis.
        ///
        /// **Note:** The total size of the context data stored for a stateful session cannot exceed 100KB.
        /// (optional)</param>
        /// <param name="userId">A string value that identifies the user who is interacting with the assistant. The
        /// client must provide a unique identifier for each individual end user who accesses the application. For
        /// user-based plans, this user ID is used to identify unique users for billing purposes. This string cannot
        /// contain carriage return, newline, or tab characters. If no value is specified in the input, **user_id** is
        /// automatically set to the value of **context.global.session_id**.
        ///
        /// **Note:** This property is the same as the **user_id** property in the global system context. If **user_id**
        /// is specified in both locations, the value specified at the root is used. (optional)</param>
        /// <returns><see cref="MessageResponse" />MessageResponse</returns>
        public DetailedResponse<MessageResponse> Message(string assistantId, string sessionId, MessageInput input = null, MessageContext context = null, string userId = null)
        {
            if (string.IsNullOrEmpty(assistantId))
            {
                throw new ArgumentNullException("`assistantId` is required for `Message`");
            }
            else
            {
                assistantId = Uri.EscapeDataString(assistantId);
            }
            if (string.IsNullOrEmpty(sessionId))
            {
                throw new ArgumentNullException("`sessionId` is required for `Message`");
            }
            else
            {
                sessionId = Uri.EscapeDataString(sessionId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<MessageResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/assistants/{assistantId}/sessions/{sessionId}/message");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (input != null)
                {
                    bodyObject["input"] = JToken.FromObject(input);
                }
                if (context != null)
                {
                    bodyObject["context"] = JToken.FromObject(context);
                }
                if (!string.IsNullOrEmpty(userId))
                {
                    bodyObject["user_id"] = userId;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v2", "Message"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<MessageResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<MessageResponse>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Send user input to assistant (stateless).
        ///
        /// Send user input to an assistant and receive a response, with conversation state (including context data)
        /// managed by your application.
        /// </summary>
        /// <param name="assistantId">Unique identifier of the assistant. To find the assistant ID in the Watson
        /// Assistant user interface, open the assistant settings and click **API Details**. For information about
        /// creating assistants, see the
        /// [documentation](https://cloud.ibm.com/docs/assistant?topic=assistant-assistant-add#assistant-add-task).
        ///
        /// **Note:** Currently, the v2 API does not support creating assistants.</param>
        /// <param name="input">An input object that includes the input text. (optional)</param>
        /// <param name="context">Context data for the conversation. You can use this property to set or modify context
        /// variables, which can also be accessed by dialog nodes. The context is not stored by the assistant. To
        /// maintain session state, include the context from the previous response.
        ///
        /// **Note:** The total size of the context data for a stateless session cannot exceed 250KB. (optional)</param>
        /// <param name="userId">A string value that identifies the user who is interacting with the assistant. The
        /// client must provide a unique identifier for each individual end user who accesses the application. For
        /// user-based plans, this user ID is used to identify unique users for billing purposes. This string cannot
        /// contain carriage return, newline, or tab characters. If no value is specified in the input, **user_id** is
        /// automatically set to the value of **context.global.session_id**.
        ///
        /// **Note:** This property is the same as the **user_id** property in the global system context. If **user_id**
        /// is specified in both locations in a message request, the value specified at the root is used.
        /// (optional)</param>
        /// <returns><see cref="MessageResponseStateless" />MessageResponseStateless</returns>
        public DetailedResponse<MessageResponseStateless> MessageStateless(string assistantId, MessageInputStateless input = null, MessageContextStateless context = null, string userId = null)
        {
            if (string.IsNullOrEmpty(assistantId))
            {
                throw new ArgumentNullException("`assistantId` is required for `MessageStateless`");
            }
            else
            {
                assistantId = Uri.EscapeDataString(assistantId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<MessageResponseStateless> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/assistants/{assistantId}/message");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (input != null)
                {
                    bodyObject["input"] = JToken.FromObject(input);
                }
                if (context != null)
                {
                    bodyObject["context"] = JToken.FromObject(context);
                }
                if (!string.IsNullOrEmpty(userId))
                {
                    bodyObject["user_id"] = userId;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v2", "MessageStateless"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<MessageResponseStateless>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<MessageResponseStateless>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Identify intents and entities in multiple user utterances.
        ///
        /// Send multiple user inputs to a dialog skill in a single request and receive information about the intents
        /// and entities recognized in each input. This method is useful for testing and comparing the performance of
        /// different skills or skill versions.
        ///
        /// This method is available only with Enterprise with Data Isolation plans.
        /// </summary>
        /// <param name="skillId">Unique identifier of the skill. To find the skill ID in the Watson Assistant user
        /// interface, open the skill settings and click **API Details**.</param>
        /// <param name="input">An array of input utterances to classify.</param>
        /// <returns><see cref="BulkClassifyResponse" />BulkClassifyResponse</returns>
        public DetailedResponse<BulkClassifyResponse> BulkClassify(string skillId, List<BulkClassifyUtterance> input)
        {
            if (string.IsNullOrEmpty(skillId))
            {
                throw new ArgumentNullException("`skillId` is required for `BulkClassify`");
            }
            else
            {
                skillId = Uri.EscapeDataString(skillId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (input == null)
            {
                throw new ArgumentNullException("`input` is required for `BulkClassify`");
            }
            DetailedResponse<BulkClassifyResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/skills/{skillId}/workspace/bulk_classify");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (input != null && input.Count > 0)
                {
                    bodyObject["input"] = JToken.FromObject(input);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v2", "BulkClassify"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<BulkClassifyResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<BulkClassifyResponse>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// List log events for an assistant.
        ///
        /// List the events from the log of an assistant.
        ///
        /// This method requires Manager access, and is available only with Enterprise plans.
        ///
        /// **Note:** If you use the **cursor** parameter to retrieve results one page at a time, subsequent requests
        /// must be no more than 5 minutes apart. Any returned value for the **cursor** parameter becomes invalid after
        /// 5 minutes. For more information about using pagination, see [Pagination](#pagination).
        /// </summary>
        /// <param name="assistantId">Unique identifier of the assistant. To find the assistant ID in the Watson
        /// Assistant user interface, open the assistant settings and click **API Details**. For information about
        /// creating assistants, see the
        /// [documentation](https://cloud.ibm.com/docs/assistant?topic=assistant-assistant-add#assistant-add-task).
        ///
        /// **Note:** Currently, the v2 API does not support creating assistants.</param>
        /// <param name="sort">How to sort the returned log events. You can sort by **request_timestamp**. To reverse
        /// the sort order, prefix the parameter value with a minus sign (`-`). (optional)</param>
        /// <param name="filter">A cacheable parameter that limits the results to those matching the specified filter.
        /// For more information, see the
        /// [documentation](https://cloud.ibm.com/docs/assistant?topic=assistant-filter-reference#filter-reference).
        /// (optional)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <returns><see cref="LogCollection" />LogCollection</returns>
        public DetailedResponse<LogCollection> ListLogs(string assistantId, string sort = null, string filter = null, long? pageLimit = null, string cursor = null)
        {
            if (string.IsNullOrEmpty(assistantId))
            {
                throw new ArgumentNullException("`assistantId` is required for `ListLogs`");
            }
            else
            {
                assistantId = Uri.EscapeDataString(assistantId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<LogCollection> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/assistants/{assistantId}/logs");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    restRequest.WithArgument("sort", sort);
                }
                if (!string.IsNullOrEmpty(filter))
                {
                    restRequest.WithArgument("filter", filter);
                }
                if (pageLimit != null)
                {
                    restRequest.WithArgument("page_limit", pageLimit);
                }
                if (!string.IsNullOrEmpty(cursor))
                {
                    restRequest.WithArgument("cursor", cursor);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v2", "ListLogs"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<LogCollection>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<LogCollection>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Delete labeled data.
        ///
        /// Deletes all data associated with a specified customer ID. The method has no effect if no data is associated
        /// with the customer ID.
        ///
        /// You associate a customer ID with data by passing the `X-Watson-Metadata` header with a request that passes
        /// data. For more information about personal data and customer IDs, see [Information
        /// security](https://cloud.ibm.com/docs/assistant?topic=assistant-information-security#information-security).
        ///
        /// **Note:** This operation is intended only for deleting data associated with a single specific customer, not
        /// for deleting data associated with multiple customers or for any other purpose. For more information, see
        /// [Labeling and deleting data in Watson
        /// Assistant](https://cloud.ibm.com/docs/assistant?topic=assistant-information-security#information-security-gdpr-wa).
        /// </summary>
        /// <param name="customerId">The customer ID for which all data is to be deleted.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteUserData(string customerId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(customerId))
            {
                throw new ArgumentNullException("`customerId` is required for `DeleteUserData`");
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v2/user_data");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (!string.IsNullOrEmpty(customerId))
                {
                    restRequest.WithArgument("customer_id", customerId);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v2", "DeleteUserData"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<object>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<object>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// List environments.
        ///
        /// List the environments associated with an assistant.
        /// </summary>
        /// <param name="assistantId">Unique identifier of the assistant. To find the assistant ID in the Watson
        /// Assistant user interface, open the assistant settings and click **API Details**. For information about
        /// creating assistants, see the
        /// [documentation](https://cloud.ibm.com/docs/assistant?topic=assistant-assistant-add#assistant-add-task).
        ///
        /// **Note:** Currently, the v2 API does not support creating assistants.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records that satisfy the
        /// request, regardless of the page limit. If this parameter is `true`, the `pagination` object in the response
        /// includes the `total` property. (optional, default to false)</param>
        /// <param name="sort">The attribute by which returned environments will be sorted. To reverse the sort order,
        /// prefix the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="EnvironmentCollection" />EnvironmentCollection</returns>
        public DetailedResponse<EnvironmentCollection> ListEnvironments(string assistantId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(assistantId))
            {
                throw new ArgumentNullException("`assistantId` is required for `ListEnvironments`");
            }
            else
            {
                assistantId = Uri.EscapeDataString(assistantId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<EnvironmentCollection> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/assistants/{assistantId}/environments");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (pageLimit != null)
                {
                    restRequest.WithArgument("page_limit", pageLimit);
                }
                if (includeCount != null)
                {
                    restRequest.WithArgument("include_count", includeCount);
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    restRequest.WithArgument("sort", sort);
                }
                if (!string.IsNullOrEmpty(cursor))
                {
                    restRequest.WithArgument("cursor", cursor);
                }
                if (includeAudit != null)
                {
                    restRequest.WithArgument("include_audit", includeAudit);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v2", "ListEnvironments"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<EnvironmentCollection>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<EnvironmentCollection>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for ListEnvironments.
        /// </summary>
        public class ListEnvironmentsEnums
        {
            /// <summary>
            /// The attribute by which returned environments will be sorted. To reverse the sort order, prefix the value
            /// with a minus sign (`-`).
            /// </summary>
            public class SortValue
            {
                /// <summary>
                /// Constant NAME for name
                /// </summary>
                public const string NAME = "name";
                /// <summary>
                /// Constant UPDATED for updated
                /// </summary>
                public const string UPDATED = "updated";
                
            }
        }

        /// <summary>
        /// Get environment.
        ///
        /// Get information about an environment. For more information about environments, see
        /// [Environments](https://cloud.ibm.com/docs/watson-assistant?topic=watson-assistant-publish-overview#environments).
        /// </summary>
        /// <param name="assistantId">Unique identifier of the assistant. To find the assistant ID in the Watson
        /// Assistant user interface, open the assistant settings and click **API Details**. For information about
        /// creating assistants, see the
        /// [documentation](https://cloud.ibm.com/docs/assistant?topic=assistant-assistant-add#assistant-add-task).
        ///
        /// **Note:** Currently, the v2 API does not support creating assistants.</param>
        /// <param name="environmentId">Unique identifier of the environment. To find the environment ID in the Watson
        /// Assistant user interface, open the environment settings and click **API Details**. **Note:** Currently, the
        /// API does not support creating environments.</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Environment" />Environment</returns>
        public DetailedResponse<Environment> GetEnvironment(string assistantId, string environmentId, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(assistantId))
            {
                throw new ArgumentNullException("`assistantId` is required for `GetEnvironment`");
            }
            else
            {
                assistantId = Uri.EscapeDataString(assistantId);
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `GetEnvironment`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<Environment> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/assistants/{assistantId}/environments/{environmentId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (includeAudit != null)
                {
                    restRequest.WithArgument("include_audit", includeAudit);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v2", "GetEnvironment"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Environment>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Environment>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// List releases.
        ///
        /// List the releases associated with an assistant. (In the Watson Assistant user interface, a release is called
        /// a *version*.).
        /// </summary>
        /// <param name="assistantId">Unique identifier of the assistant. To find the assistant ID in the Watson
        /// Assistant user interface, open the assistant settings and click **API Details**. For information about
        /// creating assistants, see the
        /// [documentation](https://cloud.ibm.com/docs/assistant?topic=assistant-assistant-add#assistant-add-task).
        ///
        /// **Note:** Currently, the v2 API does not support creating assistants.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records that satisfy the
        /// request, regardless of the page limit. If this parameter is `true`, the `pagination` object in the response
        /// includes the `total` property. (optional, default to false)</param>
        /// <param name="sort">The attribute by which returned workspaces will be sorted. To reverse the sort order,
        /// prefix the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="ReleaseCollection" />ReleaseCollection</returns>
        public DetailedResponse<ReleaseCollection> ListReleases(string assistantId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(assistantId))
            {
                throw new ArgumentNullException("`assistantId` is required for `ListReleases`");
            }
            else
            {
                assistantId = Uri.EscapeDataString(assistantId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<ReleaseCollection> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/assistants/{assistantId}/releases");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (pageLimit != null)
                {
                    restRequest.WithArgument("page_limit", pageLimit);
                }
                if (includeCount != null)
                {
                    restRequest.WithArgument("include_count", includeCount);
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    restRequest.WithArgument("sort", sort);
                }
                if (!string.IsNullOrEmpty(cursor))
                {
                    restRequest.WithArgument("cursor", cursor);
                }
                if (includeAudit != null)
                {
                    restRequest.WithArgument("include_audit", includeAudit);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v2", "ListReleases"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ReleaseCollection>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ReleaseCollection>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for ListReleases.
        /// </summary>
        public class ListReleasesEnums
        {
            /// <summary>
            /// The attribute by which returned workspaces will be sorted. To reverse the sort order, prefix the value
            /// with a minus sign (`-`).
            /// </summary>
            public class SortValue
            {
                /// <summary>
                /// Constant NAME for name
                /// </summary>
                public const string NAME = "name";
                /// <summary>
                /// Constant UPDATED for updated
                /// </summary>
                public const string UPDATED = "updated";
                
            }
        }

        /// <summary>
        /// Get release.
        ///
        /// Get information about a release.
        ///
        /// Release data is not available until publishing of the release completes. If publishing is still in progress,
        /// you can continue to poll by calling the same request again and checking the value of the **status**
        /// property. When processing has completed, the request returns the release data.
        /// </summary>
        /// <param name="assistantId">Unique identifier of the assistant. To find the assistant ID in the Watson
        /// Assistant user interface, open the assistant settings and click **API Details**. For information about
        /// creating assistants, see the
        /// [documentation](https://cloud.ibm.com/docs/assistant?topic=assistant-assistant-add#assistant-add-task).
        ///
        /// **Note:** Currently, the v2 API does not support creating assistants.</param>
        /// <param name="release">Unique identifier of the release.</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Release" />Release</returns>
        public DetailedResponse<Release> GetRelease(string assistantId, string release, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(assistantId))
            {
                throw new ArgumentNullException("`assistantId` is required for `GetRelease`");
            }
            else
            {
                assistantId = Uri.EscapeDataString(assistantId);
            }
            if (string.IsNullOrEmpty(release))
            {
                throw new ArgumentNullException("`release` is required for `GetRelease`");
            }
            else
            {
                release = Uri.EscapeDataString(release);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<Release> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/assistants/{assistantId}/releases/{release}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (includeAudit != null)
                {
                    restRequest.WithArgument("include_audit", includeAudit);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v2", "GetRelease"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Release>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Release>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Deploy release.
        ///
        /// Update the environment with the content of the release. All snapshots saved as part of the release become
        /// active in the environment.
        /// </summary>
        /// <param name="assistantId">Unique identifier of the assistant. To find the assistant ID in the Watson
        /// Assistant user interface, open the assistant settings and click **API Details**. For information about
        /// creating assistants, see the
        /// [documentation](https://cloud.ibm.com/docs/assistant?topic=assistant-assistant-add#assistant-add-task).
        ///
        /// **Note:** Currently, the v2 API does not support creating assistants.</param>
        /// <param name="release">Unique identifier of the release.</param>
        /// <param name="environmentId">The environment ID of the environment where the release is to be
        /// deployed.</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Environment" />Environment</returns>
        public DetailedResponse<Environment> DeployRelease(string assistantId, string release, string environmentId, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(assistantId))
            {
                throw new ArgumentNullException("`assistantId` is required for `DeployRelease`");
            }
            else
            {
                assistantId = Uri.EscapeDataString(assistantId);
            }
            if (string.IsNullOrEmpty(release))
            {
                throw new ArgumentNullException("`release` is required for `DeployRelease`");
            }
            else
            {
                release = Uri.EscapeDataString(release);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `DeployRelease`");
            }
            DetailedResponse<Environment> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/assistants/{assistantId}/releases/{release}/deploy");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (includeAudit != null)
                {
                    restRequest.WithArgument("include_audit", includeAudit);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(environmentId))
                {
                    bodyObject["environment_id"] = environmentId;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v2", "DeployRelease"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Environment>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Environment>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
