/**
* (C) Copyright IBM Corp. 2018, 2021.
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
* IBM OpenAPI SDK Code Generator Version: 3.38.0-07189efd-20210827-205025
*/
 
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using IBM.Cloud.SDK.Core.Authentication;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Service;
using IBM.Watson.Assistant.v1.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace IBM.Watson.Assistant.v1
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
        /// Get response to user input.
        ///
        /// Send user input to a workspace and receive a response.
        ///
        /// **Important:** This method has been superseded by the new v2 runtime API. The v2 API offers significant
        /// advantages, including ease of deployment, automatic state management, versioning, and search capabilities.
        /// For more information, see the
        /// [documentation](https://cloud.ibm.com/docs/assistant?topic=assistant-api-overview).
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="input">An input object that includes the input text. (optional)</param>
        /// <param name="intents">Intents to use when evaluating the user input. Include intents from the previous
        /// response to continue using those intents rather than trying to recognize intents in the new input.
        /// (optional)</param>
        /// <param name="entities">Entities to use when evaluating the message. Include entities from the previous
        /// response to continue using those entities rather than detecting entities in the new input.
        /// (optional)</param>
        /// <param name="alternateIntents">Whether to return more than one intent. A value of `true` indicates that all
        /// matching intents are returned. (optional, default to false)</param>
        /// <param name="context">State information for the conversation. To maintain state, include the context from
        /// the previous response. (optional)</param>
        /// <param name="output">An output object that includes the response to the user, the dialog nodes that were
        /// triggered, and messages from the log. (optional)</param>
        /// <param name="userId">A string value that identifies the user who is interacting with the workspace. The
        /// client must provide a unique identifier for each individual end user who accesses the application. For
        /// user-based plans, this user ID is used to identify unique users for billing purposes. This string cannot
        /// contain carriage return, newline, or tab characters. If no value is specified in the input, **user_id** is
        /// automatically set to the value of **context.conversation_id**.
        ///
        /// **Note:** This property is the same as the **user_id** property in the context metadata. If **user_id** is
        /// specified in both locations in a message request, the value specified at the root is used.
        /// (optional)</param>
        /// <param name="nodesVisitedDetails">Whether to include additional diagnostic information about the dialog
        /// nodes that were visited during processing of the message. (optional, default to false)</param>
        /// <returns><see cref="MessageResponse" />MessageResponse</returns>
        public DetailedResponse<MessageResponse> Message(string workspaceId, MessageInput input = null, List<RuntimeIntent> intents = null, List<RuntimeEntity> entities = null, bool? alternateIntents = null, Context context = null, OutputData output = null, bool? nodesVisitedDetails = null, string userId = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `Message`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
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

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/message");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (nodesVisitedDetails != null)
                {
                    restRequest.WithArgument("nodes_visited_details", nodesVisitedDetails);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (input != null)
                {
                    bodyObject["input"] = JToken.FromObject(input);
                }
                if (intents != null && intents.Count > 0)
                {
                    bodyObject["intents"] = JToken.FromObject(intents);
                }
                if (entities != null && entities.Count > 0)
                {
                    bodyObject["entities"] = JToken.FromObject(entities);
                }
                if (alternateIntents != null)
                {
                    bodyObject["alternate_intents"] = JToken.FromObject(alternateIntents);
                }
                if (context != null)
                {
                    bodyObject["context"] = JToken.FromObject(context);
                }
                if (output != null)
                {
                    bodyObject["output"] = JToken.FromObject(output);
                }
                if (!string.IsNullOrEmpty(userId))
                {
                    bodyObject["user_id"] = userId;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "Message"));
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
        /// Identify intents and entities in multiple user utterances.
        ///
        /// Send multiple user inputs to a workspace in a single request and receive information about the intents and
        /// entities recognized in each input. This method is useful for testing and comparing the performance of
        /// different workspaces.
        ///
        /// This method is available only with Enterprise with Data Isolation plans.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="input">An array of input utterances to classify. (optional)</param>
        /// <returns><see cref="BulkClassifyResponse" />BulkClassifyResponse</returns>
        public DetailedResponse<BulkClassifyResponse> BulkClassify(string workspaceId, List<BulkClassifyUtterance> input = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `BulkClassify`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<BulkClassifyResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/bulk_classify");

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

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "BulkClassify"));
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
        /// List workspaces.
        ///
        /// List the workspaces associated with a Watson Assistant service instance.
        /// </summary>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records that satisfy the
        /// request, regardless of the page limit. If this parameter is `true`, the `pagination` object in the response
        /// includes the `total` property. (optional, default to false)</param>
        /// <param name="sort">The attribute by which returned workspaces will be sorted. To reverse the sort order,
        /// prefix the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="WorkspaceCollection" />WorkspaceCollection</returns>
        public DetailedResponse<WorkspaceCollection> ListWorkspaces(long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<WorkspaceCollection> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces");

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

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "ListWorkspaces"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<WorkspaceCollection>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<WorkspaceCollection>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for ListWorkspaces.
        /// </summary>
        public class ListWorkspacesEnums
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
        /// Create workspace.
        ///
        /// Create a workspace based on component objects. You must provide workspace components defining the content of
        /// the new workspace.
        /// </summary>
        /// <param name="name">The name of the workspace. This string cannot contain carriage return, newline, or tab
        /// characters. (optional)</param>
        /// <param name="description">The description of the workspace. This string cannot contain carriage return,
        /// newline, or tab characters. (optional)</param>
        /// <param name="language">The language of the workspace. (optional)</param>
        /// <param name="dialogNodes">An array of objects describing the dialog nodes in the workspace.
        /// (optional)</param>
        /// <param name="counterexamples">An array of objects defining input examples that have been marked as
        /// irrelevant input. (optional)</param>
        /// <param name="metadata">Any metadata related to the workspace. (optional)</param>
        /// <param name="learningOptOut">Whether training data from the workspace (including artifacts such as intents
        /// and entities) can be used by IBM for general service improvements. `true` indicates that workspace training
        /// data is not to be used. (optional, default to false)</param>
        /// <param name="systemSettings">Global settings for the workspace. (optional)</param>
        /// <param name="webhooks"> (optional)</param>
        /// <param name="intents">An array of objects defining the intents for the workspace. (optional)</param>
        /// <param name="entities">An array of objects describing the entities for the workspace. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Workspace" />Workspace</returns>
        public DetailedResponse<Workspace> CreateWorkspace(string name = null, string description = null, string language = null, List<DialogNode> dialogNodes = null, List<Counterexample> counterexamples = null, Dictionary<string, object> metadata = null, bool? learningOptOut = null, WorkspaceSystemSettings systemSettings = null, List<Webhook> webhooks = null, List<CreateIntent> intents = null, List<CreateEntity> entities = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<Workspace> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces");

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
                if (!string.IsNullOrEmpty(name))
                {
                    bodyObject["name"] = name;
                }
                if (!string.IsNullOrEmpty(description))
                {
                    bodyObject["description"] = description;
                }
                if (!string.IsNullOrEmpty(language))
                {
                    bodyObject["language"] = language;
                }
                if (dialogNodes != null && dialogNodes.Count > 0)
                {
                    bodyObject["dialog_nodes"] = JToken.FromObject(dialogNodes);
                }
                if (counterexamples != null && counterexamples.Count > 0)
                {
                    bodyObject["counterexamples"] = JToken.FromObject(counterexamples);
                }
                if (metadata != null)
                {
                    bodyObject["metadata"] = JToken.FromObject(metadata);
                }
                if (learningOptOut != null)
                {
                    bodyObject["learning_opt_out"] = JToken.FromObject(learningOptOut);
                }
                if (systemSettings != null)
                {
                    bodyObject["system_settings"] = JToken.FromObject(systemSettings);
                }
                if (webhooks != null && webhooks.Count > 0)
                {
                    bodyObject["webhooks"] = JToken.FromObject(webhooks);
                }
                if (intents != null && intents.Count > 0)
                {
                    bodyObject["intents"] = JToken.FromObject(intents);
                }
                if (entities != null && entities.Count > 0)
                {
                    bodyObject["entities"] = JToken.FromObject(entities);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "CreateWorkspace"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Workspace>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Workspace>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get information about a workspace.
        ///
        /// Get information about a workspace, optionally including all workspace content.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the
        /// returned data includes only information about the element itself. If **export**=`true`, all content,
        /// including subelements, is included. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <param name="sort">Indicates how the returned workspace data will be sorted. This parameter is valid only if
        /// **export**=`true`. Specify `sort=stable` to sort all workspace objects by unique identifier, in ascending
        /// alphabetical order. (optional)</param>
        /// <returns><see cref="Workspace" />Workspace</returns>
        public DetailedResponse<Workspace> GetWorkspace(string workspaceId, bool? export = null, bool? includeAudit = null, string sort = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `GetWorkspace`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<Workspace> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (export != null)
                {
                    restRequest.WithArgument("export", export);
                }
                if (includeAudit != null)
                {
                    restRequest.WithArgument("include_audit", includeAudit);
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    restRequest.WithArgument("sort", sort);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "GetWorkspace"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Workspace>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Workspace>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for GetWorkspace.
        /// </summary>
        public class GetWorkspaceEnums
        {
            /// <summary>
            /// Indicates how the returned workspace data will be sorted. This parameter is valid only if
            /// **export**=`true`. Specify `sort=stable` to sort all workspace objects by unique identifier, in
            /// ascending alphabetical order.
            /// </summary>
            public class SortValue
            {
                /// <summary>
                /// Constant STABLE for stable
                /// </summary>
                public const string STABLE = "stable";
                
            }
        }

        /// <summary>
        /// Update workspace.
        ///
        /// Update an existing workspace with new or modified data. You must provide component objects defining the
        /// content of the updated workspace.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="name">The name of the workspace. This string cannot contain carriage return, newline, or tab
        /// characters. (optional)</param>
        /// <param name="description">The description of the workspace. This string cannot contain carriage return,
        /// newline, or tab characters. (optional)</param>
        /// <param name="language">The language of the workspace. (optional)</param>
        /// <param name="dialogNodes">An array of objects describing the dialog nodes in the workspace.
        /// (optional)</param>
        /// <param name="counterexamples">An array of objects defining input examples that have been marked as
        /// irrelevant input. (optional)</param>
        /// <param name="metadata">Any metadata related to the workspace. (optional)</param>
        /// <param name="learningOptOut">Whether training data from the workspace (including artifacts such as intents
        /// and entities) can be used by IBM for general service improvements. `true` indicates that workspace training
        /// data is not to be used. (optional, default to false)</param>
        /// <param name="systemSettings">Global settings for the workspace. (optional)</param>
        /// <param name="webhooks"> (optional)</param>
        /// <param name="intents">An array of objects defining the intents for the workspace. (optional)</param>
        /// <param name="entities">An array of objects describing the entities for the workspace. (optional)</param>
        /// <param name="append">Whether the new data is to be appended to the existing data in the object. If
        /// **append**=`false`, elements included in the new data completely replace the corresponding existing
        /// elements, including all subelements. For example, if the new data for a workspace includes **entities** and
        /// **append**=`false`, all existing entities in the workspace are discarded and replaced with the new entities.
        ///
        /// If **append**=`true`, existing elements are preserved, and the new elements are added. If any elements in
        /// the new data collide with existing elements, the update request fails. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Workspace" />Workspace</returns>
        public DetailedResponse<Workspace> UpdateWorkspace(string workspaceId, string name = null, string description = null, string language = null, List<DialogNode> dialogNodes = null, List<Counterexample> counterexamples = null, Dictionary<string, object> metadata = null, bool? learningOptOut = null, WorkspaceSystemSettings systemSettings = null, List<Webhook> webhooks = null, List<CreateIntent> intents = null, List<CreateEntity> entities = null, bool? append = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `UpdateWorkspace`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<Workspace> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (append != null)
                {
                    restRequest.WithArgument("append", append);
                }
                if (includeAudit != null)
                {
                    restRequest.WithArgument("include_audit", includeAudit);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(name))
                {
                    bodyObject["name"] = name;
                }
                if (!string.IsNullOrEmpty(description))
                {
                    bodyObject["description"] = description;
                }
                if (!string.IsNullOrEmpty(language))
                {
                    bodyObject["language"] = language;
                }
                if (dialogNodes != null && dialogNodes.Count > 0)
                {
                    bodyObject["dialog_nodes"] = JToken.FromObject(dialogNodes);
                }
                if (counterexamples != null && counterexamples.Count > 0)
                {
                    bodyObject["counterexamples"] = JToken.FromObject(counterexamples);
                }
                if (metadata != null)
                {
                    bodyObject["metadata"] = JToken.FromObject(metadata);
                }
                if (learningOptOut != null)
                {
                    bodyObject["learning_opt_out"] = JToken.FromObject(learningOptOut);
                }
                if (systemSettings != null)
                {
                    bodyObject["system_settings"] = JToken.FromObject(systemSettings);
                }
                if (webhooks != null && webhooks.Count > 0)
                {
                    bodyObject["webhooks"] = JToken.FromObject(webhooks);
                }
                if (intents != null && intents.Count > 0)
                {
                    bodyObject["intents"] = JToken.FromObject(intents);
                }
                if (entities != null && entities.Count > 0)
                {
                    bodyObject["entities"] = JToken.FromObject(entities);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "UpdateWorkspace"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Workspace>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Workspace>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete workspace.
        ///
        /// Delete a workspace from the service instance.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteWorkspace(string workspaceId)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `DeleteWorkspace`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "DeleteWorkspace"));
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
        /// List intents.
        ///
        /// List the intents for a workspace.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the
        /// returned data includes only information about the element itself. If **export**=`true`, all content,
        /// including subelements, is included. (optional, default to false)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records that satisfy the
        /// request, regardless of the page limit. If this parameter is `true`, the `pagination` object in the response
        /// includes the `total` property. (optional, default to false)</param>
        /// <param name="sort">The attribute by which returned intents will be sorted. To reverse the sort order, prefix
        /// the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="IntentCollection" />IntentCollection</returns>
        public DetailedResponse<IntentCollection> ListIntents(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `ListIntents`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<IntentCollection> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (export != null)
                {
                    restRequest.WithArgument("export", export);
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

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "ListIntents"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<IntentCollection>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<IntentCollection>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for ListIntents.
        /// </summary>
        public class ListIntentsEnums
        {
            /// <summary>
            /// The attribute by which returned intents will be sorted. To reverse the sort order, prefix the value with
            /// a minus sign (`-`).
            /// </summary>
            public class SortValue
            {
                /// <summary>
                /// Constant INTENT for intent
                /// </summary>
                public const string INTENT = "intent";
                /// <summary>
                /// Constant UPDATED for updated
                /// </summary>
                public const string UPDATED = "updated";
                
            }
        }

        /// <summary>
        /// Create intent.
        ///
        /// Create a new intent.
        ///
        /// If you want to create multiple intents with a single API call, consider using the **[Update
        /// workspace](#update-workspace)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The name of the intent. This string must conform to the following restrictions:
        /// - It can contain only Unicode alphanumeric, underscore, hyphen, and dot characters.
        /// - It cannot begin with the reserved prefix `sys-`.</param>
        /// <param name="description">The description of the intent. This string cannot contain carriage return,
        /// newline, or tab characters. (optional)</param>
        /// <param name="examples">An array of user input examples for the intent. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Intent" />Intent</returns>
        public DetailedResponse<Intent> CreateIntent(string workspaceId, string intent, string description = null, List<Example> examples = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `CreateIntent`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(intent))
            {
                throw new ArgumentNullException("`intent` is required for `CreateIntent`");
            }
            DetailedResponse<Intent> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents");

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
                if (!string.IsNullOrEmpty(intent))
                {
                    bodyObject["intent"] = intent;
                }
                if (!string.IsNullOrEmpty(description))
                {
                    bodyObject["description"] = description;
                }
                if (examples != null && examples.Count > 0)
                {
                    bodyObject["examples"] = JToken.FromObject(examples);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "CreateIntent"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Intent>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Intent>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get intent.
        ///
        /// Get information about an intent, optionally including all intent content.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the
        /// returned data includes only information about the element itself. If **export**=`true`, all content,
        /// including subelements, is included. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Intent" />Intent</returns>
        public DetailedResponse<Intent> GetIntent(string workspaceId, string intent, bool? export = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `GetIntent`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(intent))
            {
                throw new ArgumentNullException("`intent` is required for `GetIntent`");
            }
            else
            {
                intent = Uri.EscapeDataString(intent);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<Intent> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (export != null)
                {
                    restRequest.WithArgument("export", export);
                }
                if (includeAudit != null)
                {
                    restRequest.WithArgument("include_audit", includeAudit);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "GetIntent"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Intent>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Intent>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update intent.
        ///
        /// Update an existing intent with new or modified data. You must provide component objects defining the content
        /// of the updated intent.
        ///
        /// If you want to update multiple intents with a single API call, consider using the **[Update
        /// workspace](#update-workspace)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="newIntent">The name of the intent. This string must conform to the following restrictions:
        /// - It can contain only Unicode alphanumeric, underscore, hyphen, and dot characters.
        /// - It cannot begin with the reserved prefix `sys-`. (optional)</param>
        /// <param name="newDescription">The description of the intent. This string cannot contain carriage return,
        /// newline, or tab characters. (optional)</param>
        /// <param name="newExamples">An array of user input examples for the intent. (optional)</param>
        /// <param name="append">Whether the new data is to be appended to the existing data in the object. If
        /// **append**=`false`, elements included in the new data completely replace the corresponding existing
        /// elements, including all subelements. For example, if the new data for the intent includes **examples** and
        /// **append**=`false`, all existing examples for the intent are discarded and replaced with the new examples.
        ///
        /// If **append**=`true`, existing elements are preserved, and the new elements are added. If any elements in
        /// the new data collide with existing elements, the update request fails. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Intent" />Intent</returns>
        public DetailedResponse<Intent> UpdateIntent(string workspaceId, string intent, string newIntent = null, string newDescription = null, List<Example> newExamples = null, bool? append = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `UpdateIntent`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(intent))
            {
                throw new ArgumentNullException("`intent` is required for `UpdateIntent`");
            }
            else
            {
                intent = Uri.EscapeDataString(intent);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<Intent> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (append != null)
                {
                    restRequest.WithArgument("append", append);
                }
                if (includeAudit != null)
                {
                    restRequest.WithArgument("include_audit", includeAudit);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(newIntent))
                {
                    bodyObject["intent"] = newIntent;
                }
                if (!string.IsNullOrEmpty(newDescription))
                {
                    bodyObject["description"] = newDescription;
                }
                if (newExamples != null && newExamples.Count > 0)
                {
                    bodyObject["examples"] = JToken.FromObject(newExamples);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "UpdateIntent"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Intent>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Intent>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete intent.
        ///
        /// Delete an intent from a workspace.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteIntent(string workspaceId, string intent)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `DeleteIntent`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(intent))
            {
                throw new ArgumentNullException("`intent` is required for `DeleteIntent`");
            }
            else
            {
                intent = Uri.EscapeDataString(intent);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "DeleteIntent"));
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
        /// List user input examples.
        ///
        /// List the user input examples for an intent, optionally including contextual entity mentions.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records that satisfy the
        /// request, regardless of the page limit. If this parameter is `true`, the `pagination` object in the response
        /// includes the `total` property. (optional, default to false)</param>
        /// <param name="sort">The attribute by which returned examples will be sorted. To reverse the sort order,
        /// prefix the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="ExampleCollection" />ExampleCollection</returns>
        public DetailedResponse<ExampleCollection> ListExamples(string workspaceId, string intent, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `ListExamples`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(intent))
            {
                throw new ArgumentNullException("`intent` is required for `ListExamples`");
            }
            else
            {
                intent = Uri.EscapeDataString(intent);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<ExampleCollection> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples");

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

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "ListExamples"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ExampleCollection>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ExampleCollection>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for ListExamples.
        /// </summary>
        public class ListExamplesEnums
        {
            /// <summary>
            /// The attribute by which returned examples will be sorted. To reverse the sort order, prefix the value
            /// with a minus sign (`-`).
            /// </summary>
            public class SortValue
            {
                /// <summary>
                /// Constant TEXT for text
                /// </summary>
                public const string TEXT = "text";
                /// <summary>
                /// Constant UPDATED for updated
                /// </summary>
                public const string UPDATED = "updated";
                
            }
        }

        /// <summary>
        /// Create user input example.
        ///
        /// Add a new user input example to an intent.
        ///
        /// If you want to add multiple examples with a single API call, consider using the **[Update
        /// intent](#update-intent)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="text">The text of a user input example. This string must conform to the following restrictions:
        /// - It cannot contain carriage return, newline, or tab characters.
        /// - It cannot consist of only whitespace characters.</param>
        /// <param name="mentions">An array of contextual entity mentions. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Example" />Example</returns>
        public DetailedResponse<Example> CreateExample(string workspaceId, string intent, string text, List<Mention> mentions = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `CreateExample`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(intent))
            {
                throw new ArgumentNullException("`intent` is required for `CreateExample`");
            }
            else
            {
                intent = Uri.EscapeDataString(intent);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("`text` is required for `CreateExample`");
            }
            DetailedResponse<Example> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples");

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
                if (!string.IsNullOrEmpty(text))
                {
                    bodyObject["text"] = text;
                }
                if (mentions != null && mentions.Count > 0)
                {
                    bodyObject["mentions"] = JToken.FromObject(mentions);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "CreateExample"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Example>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Example>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get user input example.
        ///
        /// Get information about a user input example.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="text">The text of the user input example.</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Example" />Example</returns>
        public DetailedResponse<Example> GetExample(string workspaceId, string intent, string text, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `GetExample`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(intent))
            {
                throw new ArgumentNullException("`intent` is required for `GetExample`");
            }
            else
            {
                intent = Uri.EscapeDataString(intent);
            }
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("`text` is required for `GetExample`");
            }
            else
            {
                text = Uri.EscapeDataString(text);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<Example> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples/{text}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (includeAudit != null)
                {
                    restRequest.WithArgument("include_audit", includeAudit);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "GetExample"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Example>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Example>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update user input example.
        ///
        /// Update the text of a user input example.
        ///
        /// If you want to update multiple examples with a single API call, consider using the **[Update
        /// intent](#update-intent)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="text">The text of the user input example.</param>
        /// <param name="newText">The text of the user input example. This string must conform to the following
        /// restrictions:
        /// - It cannot contain carriage return, newline, or tab characters.
        /// - It cannot consist of only whitespace characters. (optional)</param>
        /// <param name="newMentions">An array of contextual entity mentions. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Example" />Example</returns>
        public DetailedResponse<Example> UpdateExample(string workspaceId, string intent, string text, string newText = null, List<Mention> newMentions = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `UpdateExample`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(intent))
            {
                throw new ArgumentNullException("`intent` is required for `UpdateExample`");
            }
            else
            {
                intent = Uri.EscapeDataString(intent);
            }
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("`text` is required for `UpdateExample`");
            }
            else
            {
                text = Uri.EscapeDataString(text);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<Example> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples/{text}");

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
                if (!string.IsNullOrEmpty(newText))
                {
                    bodyObject["text"] = newText;
                }
                if (newMentions != null && newMentions.Count > 0)
                {
                    bodyObject["mentions"] = JToken.FromObject(newMentions);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "UpdateExample"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Example>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Example>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete user input example.
        ///
        /// Delete a user input example from an intent.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="text">The text of the user input example.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteExample(string workspaceId, string intent, string text)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `DeleteExample`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(intent))
            {
                throw new ArgumentNullException("`intent` is required for `DeleteExample`");
            }
            else
            {
                intent = Uri.EscapeDataString(intent);
            }
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("`text` is required for `DeleteExample`");
            }
            else
            {
                text = Uri.EscapeDataString(text);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples/{text}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "DeleteExample"));
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
        /// List counterexamples.
        ///
        /// List the counterexamples for a workspace. Counterexamples are examples that have been marked as irrelevant
        /// input.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records that satisfy the
        /// request, regardless of the page limit. If this parameter is `true`, the `pagination` object in the response
        /// includes the `total` property. (optional, default to false)</param>
        /// <param name="sort">The attribute by which returned counterexamples will be sorted. To reverse the sort
        /// order, prefix the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="CounterexampleCollection" />CounterexampleCollection</returns>
        public DetailedResponse<CounterexampleCollection> ListCounterexamples(string workspaceId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `ListCounterexamples`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<CounterexampleCollection> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples");

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

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "ListCounterexamples"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<CounterexampleCollection>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<CounterexampleCollection>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for ListCounterexamples.
        /// </summary>
        public class ListCounterexamplesEnums
        {
            /// <summary>
            /// The attribute by which returned counterexamples will be sorted. To reverse the sort order, prefix the
            /// value with a minus sign (`-`).
            /// </summary>
            public class SortValue
            {
                /// <summary>
                /// Constant TEXT for text
                /// </summary>
                public const string TEXT = "text";
                /// <summary>
                /// Constant UPDATED for updated
                /// </summary>
                public const string UPDATED = "updated";
                
            }
        }

        /// <summary>
        /// Create counterexample.
        ///
        /// Add a new counterexample to a workspace. Counterexamples are examples that have been marked as irrelevant
        /// input.
        ///
        /// If you want to add multiple counterexamples with a single API call, consider using the **[Update
        /// workspace](#update-workspace)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="text">The text of a user input marked as irrelevant input. This string must conform to the
        /// following restrictions:
        /// - It cannot contain carriage return, newline, or tab characters.
        /// - It cannot consist of only whitespace characters.</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Counterexample" />Counterexample</returns>
        public DetailedResponse<Counterexample> CreateCounterexample(string workspaceId, string text, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `CreateCounterexample`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("`text` is required for `CreateCounterexample`");
            }
            DetailedResponse<Counterexample> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples");

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
                if (!string.IsNullOrEmpty(text))
                {
                    bodyObject["text"] = text;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "CreateCounterexample"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Counterexample>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Counterexample>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get counterexample.
        ///
        /// Get information about a counterexample. Counterexamples are examples that have been marked as irrelevant
        /// input.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="text">The text of a user input counterexample (for example, `What are you wearing?`).</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Counterexample" />Counterexample</returns>
        public DetailedResponse<Counterexample> GetCounterexample(string workspaceId, string text, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `GetCounterexample`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("`text` is required for `GetCounterexample`");
            }
            else
            {
                text = Uri.EscapeDataString(text);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<Counterexample> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples/{text}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (includeAudit != null)
                {
                    restRequest.WithArgument("include_audit", includeAudit);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "GetCounterexample"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Counterexample>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Counterexample>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update counterexample.
        ///
        /// Update the text of a counterexample. Counterexamples are examples that have been marked as irrelevant input.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="text">The text of a user input counterexample (for example, `What are you wearing?`).</param>
        /// <param name="newText">The text of a user input marked as irrelevant input. This string must conform to the
        /// following restrictions:
        /// - It cannot contain carriage return, newline, or tab characters.
        /// - It cannot consist of only whitespace characters. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Counterexample" />Counterexample</returns>
        public DetailedResponse<Counterexample> UpdateCounterexample(string workspaceId, string text, string newText = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `UpdateCounterexample`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("`text` is required for `UpdateCounterexample`");
            }
            else
            {
                text = Uri.EscapeDataString(text);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<Counterexample> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples/{text}");

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
                if (!string.IsNullOrEmpty(newText))
                {
                    bodyObject["text"] = newText;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "UpdateCounterexample"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Counterexample>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Counterexample>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete counterexample.
        ///
        /// Delete a counterexample from a workspace. Counterexamples are examples that have been marked as irrelevant
        /// input.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="text">The text of a user input counterexample (for example, `What are you wearing?`).</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteCounterexample(string workspaceId, string text)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `DeleteCounterexample`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("`text` is required for `DeleteCounterexample`");
            }
            else
            {
                text = Uri.EscapeDataString(text);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples/{text}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "DeleteCounterexample"));
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
        /// List entities.
        ///
        /// List the entities for a workspace.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the
        /// returned data includes only information about the element itself. If **export**=`true`, all content,
        /// including subelements, is included. (optional, default to false)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records that satisfy the
        /// request, regardless of the page limit. If this parameter is `true`, the `pagination` object in the response
        /// includes the `total` property. (optional, default to false)</param>
        /// <param name="sort">The attribute by which returned entities will be sorted. To reverse the sort order,
        /// prefix the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="EntityCollection" />EntityCollection</returns>
        public DetailedResponse<EntityCollection> ListEntities(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `ListEntities`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<EntityCollection> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (export != null)
                {
                    restRequest.WithArgument("export", export);
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

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "ListEntities"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<EntityCollection>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<EntityCollection>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for ListEntities.
        /// </summary>
        public class ListEntitiesEnums
        {
            /// <summary>
            /// The attribute by which returned entities will be sorted. To reverse the sort order, prefix the value
            /// with a minus sign (`-`).
            /// </summary>
            public class SortValue
            {
                /// <summary>
                /// Constant ENTITY for entity
                /// </summary>
                public const string ENTITY = "entity";
                /// <summary>
                /// Constant UPDATED for updated
                /// </summary>
                public const string UPDATED = "updated";
                
            }
        }

        /// <summary>
        /// Create entity.
        ///
        /// Create a new entity, or enable a system entity.
        ///
        /// If you want to create multiple entities with a single API call, consider using the **[Update
        /// workspace](#update-workspace)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity. This string must conform to the following restrictions:
        /// - It can contain only Unicode alphanumeric, underscore, and hyphen characters.
        /// - If you specify an entity name beginning with the reserved prefix `sys-`, it must be the name of a system
        /// entity that you want to enable. (Any entity content specified with the request is ignored.).</param>
        /// <param name="description">The description of the entity. This string cannot contain carriage return,
        /// newline, or tab characters. (optional)</param>
        /// <param name="metadata">Any metadata related to the entity. (optional)</param>
        /// <param name="fuzzyMatch">Whether to use fuzzy matching for the entity. (optional)</param>
        /// <param name="values">An array of objects describing the entity values. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Entity" />Entity</returns>
        public DetailedResponse<Entity> CreateEntity(string workspaceId, string entity, string description = null, Dictionary<string, object> metadata = null, bool? fuzzyMatch = null, List<CreateValue> values = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `CreateEntity`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(entity))
            {
                throw new ArgumentNullException("`entity` is required for `CreateEntity`");
            }
            DetailedResponse<Entity> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities");

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
                if (!string.IsNullOrEmpty(entity))
                {
                    bodyObject["entity"] = entity;
                }
                if (!string.IsNullOrEmpty(description))
                {
                    bodyObject["description"] = description;
                }
                if (metadata != null)
                {
                    bodyObject["metadata"] = JToken.FromObject(metadata);
                }
                if (fuzzyMatch != null)
                {
                    bodyObject["fuzzy_match"] = JToken.FromObject(fuzzyMatch);
                }
                if (values != null && values.Count > 0)
                {
                    bodyObject["values"] = JToken.FromObject(values);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "CreateEntity"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Entity>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Entity>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get entity.
        ///
        /// Get information about an entity, optionally including all entity content.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the
        /// returned data includes only information about the element itself. If **export**=`true`, all content,
        /// including subelements, is included. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Entity" />Entity</returns>
        public DetailedResponse<Entity> GetEntity(string workspaceId, string entity, bool? export = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `GetEntity`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(entity))
            {
                throw new ArgumentNullException("`entity` is required for `GetEntity`");
            }
            else
            {
                entity = Uri.EscapeDataString(entity);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<Entity> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (export != null)
                {
                    restRequest.WithArgument("export", export);
                }
                if (includeAudit != null)
                {
                    restRequest.WithArgument("include_audit", includeAudit);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "GetEntity"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Entity>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Entity>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update entity.
        ///
        /// Update an existing entity with new or modified data. You must provide component objects defining the content
        /// of the updated entity.
        ///
        /// If you want to update multiple entities with a single API call, consider using the **[Update
        /// workspace](#update-workspace)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="newEntity">The name of the entity. This string must conform to the following restrictions:
        /// - It can contain only Unicode alphanumeric, underscore, and hyphen characters.
        /// - It cannot begin with the reserved prefix `sys-`. (optional)</param>
        /// <param name="newDescription">The description of the entity. This string cannot contain carriage return,
        /// newline, or tab characters. (optional)</param>
        /// <param name="newMetadata">Any metadata related to the entity. (optional)</param>
        /// <param name="newFuzzyMatch">Whether to use fuzzy matching for the entity. (optional)</param>
        /// <param name="newValues">An array of objects describing the entity values. (optional)</param>
        /// <param name="append">Whether the new data is to be appended to the existing data in the entity. If
        /// **append**=`false`, elements included in the new data completely replace the corresponding existing
        /// elements, including all subelements. For example, if the new data for the entity includes **values** and
        /// **append**=`false`, all existing values for the entity are discarded and replaced with the new values.
        ///
        /// If **append**=`true`, existing elements are preserved, and the new elements are added. If any elements in
        /// the new data collide with existing elements, the update request fails. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Entity" />Entity</returns>
        public DetailedResponse<Entity> UpdateEntity(string workspaceId, string entity, string newEntity = null, string newDescription = null, Dictionary<string, object> newMetadata = null, bool? newFuzzyMatch = null, List<CreateValue> newValues = null, bool? append = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `UpdateEntity`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(entity))
            {
                throw new ArgumentNullException("`entity` is required for `UpdateEntity`");
            }
            else
            {
                entity = Uri.EscapeDataString(entity);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<Entity> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (append != null)
                {
                    restRequest.WithArgument("append", append);
                }
                if (includeAudit != null)
                {
                    restRequest.WithArgument("include_audit", includeAudit);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(newEntity))
                {
                    bodyObject["entity"] = newEntity;
                }
                if (!string.IsNullOrEmpty(newDescription))
                {
                    bodyObject["description"] = newDescription;
                }
                if (newMetadata != null)
                {
                    bodyObject["metadata"] = JToken.FromObject(newMetadata);
                }
                if (newFuzzyMatch != null)
                {
                    bodyObject["fuzzy_match"] = JToken.FromObject(newFuzzyMatch);
                }
                if (newValues != null && newValues.Count > 0)
                {
                    bodyObject["values"] = JToken.FromObject(newValues);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "UpdateEntity"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Entity>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Entity>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete entity.
        ///
        /// Delete an entity from a workspace, or disable a system entity.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteEntity(string workspaceId, string entity)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `DeleteEntity`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(entity))
            {
                throw new ArgumentNullException("`entity` is required for `DeleteEntity`");
            }
            else
            {
                entity = Uri.EscapeDataString(entity);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "DeleteEntity"));
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
        /// List entity mentions.
        ///
        /// List mentions for a contextual entity. An entity mention is an occurrence of a contextual entity in the
        /// context of an intent user input example.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the
        /// returned data includes only information about the element itself. If **export**=`true`, all content,
        /// including subelements, is included. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="EntityMentionCollection" />EntityMentionCollection</returns>
        public DetailedResponse<EntityMentionCollection> ListMentions(string workspaceId, string entity, bool? export = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `ListMentions`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(entity))
            {
                throw new ArgumentNullException("`entity` is required for `ListMentions`");
            }
            else
            {
                entity = Uri.EscapeDataString(entity);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<EntityMentionCollection> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/mentions");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (export != null)
                {
                    restRequest.WithArgument("export", export);
                }
                if (includeAudit != null)
                {
                    restRequest.WithArgument("include_audit", includeAudit);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "ListMentions"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<EntityMentionCollection>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<EntityMentionCollection>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// List entity values.
        ///
        /// List the values for an entity.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the
        /// returned data includes only information about the element itself. If **export**=`true`, all content,
        /// including subelements, is included. (optional, default to false)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records that satisfy the
        /// request, regardless of the page limit. If this parameter is `true`, the `pagination` object in the response
        /// includes the `total` property. (optional, default to false)</param>
        /// <param name="sort">The attribute by which returned entity values will be sorted. To reverse the sort order,
        /// prefix the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="ValueCollection" />ValueCollection</returns>
        public DetailedResponse<ValueCollection> ListValues(string workspaceId, string entity, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `ListValues`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(entity))
            {
                throw new ArgumentNullException("`entity` is required for `ListValues`");
            }
            else
            {
                entity = Uri.EscapeDataString(entity);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<ValueCollection> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (export != null)
                {
                    restRequest.WithArgument("export", export);
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

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "ListValues"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ValueCollection>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ValueCollection>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for ListValues.
        /// </summary>
        public class ListValuesEnums
        {
            /// <summary>
            /// The attribute by which returned entity values will be sorted. To reverse the sort order, prefix the
            /// value with a minus sign (`-`).
            /// </summary>
            public class SortValue
            {
                /// <summary>
                /// Constant VALUE for value
                /// </summary>
                public const string VALUE = "value";
                /// <summary>
                /// Constant UPDATED for updated
                /// </summary>
                public const string UPDATED = "updated";
                
            }
        }

        /// <summary>
        /// Create entity value.
        ///
        /// Create a new value for an entity.
        ///
        /// If you want to create multiple entity values with a single API call, consider using the **[Update
        /// entity](#update-entity)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value. This string must conform to the following restrictions:
        /// - It cannot contain carriage return, newline, or tab characters.
        /// - It cannot consist of only whitespace characters.</param>
        /// <param name="metadata">Any metadata related to the entity value. (optional)</param>
        /// <param name="type">Specifies the type of entity value. (optional, default to synonyms)</param>
        /// <param name="synonyms">An array of synonyms for the entity value. A value can specify either synonyms or
        /// patterns (depending on the value type), but not both. A synonym must conform to the following resrictions:
        /// - It cannot contain carriage return, newline, or tab characters.
        /// - It cannot consist of only whitespace characters. (optional)</param>
        /// <param name="patterns">An array of patterns for the entity value. A value can specify either synonyms or
        /// patterns (depending on the value type), but not both. A pattern is a regular expression; for more
        /// information about how to specify a pattern, see the
        /// [documentation](https://cloud.ibm.com/docs/assistant?topic=assistant-entities#entities-create-dictionary-based).
        /// (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Value" />Value</returns>
        public DetailedResponse<Value> CreateValue(string workspaceId, string entity, string value, Dictionary<string, object> metadata = null, string type = null, List<string> synonyms = null, List<string> patterns = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `CreateValue`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(entity))
            {
                throw new ArgumentNullException("`entity` is required for `CreateValue`");
            }
            else
            {
                entity = Uri.EscapeDataString(entity);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("`value` is required for `CreateValue`");
            }
            DetailedResponse<Value> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values");

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
                if (!string.IsNullOrEmpty(value))
                {
                    bodyObject["value"] = value;
                }
                if (metadata != null)
                {
                    bodyObject["metadata"] = JToken.FromObject(metadata);
                }
                if (!string.IsNullOrEmpty(type))
                {
                    bodyObject["type"] = type;
                }
                if (synonyms != null && synonyms.Count > 0)
                {
                    bodyObject["synonyms"] = JToken.FromObject(synonyms);
                }
                if (patterns != null && patterns.Count > 0)
                {
                    bodyObject["patterns"] = JToken.FromObject(patterns);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "CreateValue"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Value>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Value>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get entity value.
        ///
        /// Get information about an entity value.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the
        /// returned data includes only information about the element itself. If **export**=`true`, all content,
        /// including subelements, is included. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Value" />Value</returns>
        public DetailedResponse<Value> GetValue(string workspaceId, string entity, string value, bool? export = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `GetValue`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(entity))
            {
                throw new ArgumentNullException("`entity` is required for `GetValue`");
            }
            else
            {
                entity = Uri.EscapeDataString(entity);
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("`value` is required for `GetValue`");
            }
            else
            {
                value = Uri.EscapeDataString(value);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<Value> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (export != null)
                {
                    restRequest.WithArgument("export", export);
                }
                if (includeAudit != null)
                {
                    restRequest.WithArgument("include_audit", includeAudit);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "GetValue"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Value>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Value>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update entity value.
        ///
        /// Update an existing entity value with new or modified data. You must provide component objects defining the
        /// content of the updated entity value.
        ///
        /// If you want to update multiple entity values with a single API call, consider using the **[Update
        /// entity](#update-entity)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="newValue">The text of the entity value. This string must conform to the following restrictions:
        /// - It cannot contain carriage return, newline, or tab characters.
        /// - It cannot consist of only whitespace characters. (optional)</param>
        /// <param name="newMetadata">Any metadata related to the entity value. (optional)</param>
        /// <param name="newType">Specifies the type of entity value. (optional, default to synonyms)</param>
        /// <param name="newSynonyms">An array of synonyms for the entity value. A value can specify either synonyms or
        /// patterns (depending on the value type), but not both. A synonym must conform to the following resrictions:
        /// - It cannot contain carriage return, newline, or tab characters.
        /// - It cannot consist of only whitespace characters. (optional)</param>
        /// <param name="newPatterns">An array of patterns for the entity value. A value can specify either synonyms or
        /// patterns (depending on the value type), but not both. A pattern is a regular expression; for more
        /// information about how to specify a pattern, see the
        /// [documentation](https://cloud.ibm.com/docs/assistant?topic=assistant-entities#entities-create-dictionary-based).
        /// (optional)</param>
        /// <param name="append">Whether the new data is to be appended to the existing data in the entity value. If
        /// **append**=`false`, elements included in the new data completely replace the corresponding existing
        /// elements, including all subelements. For example, if the new data for the entity value includes **synonyms**
        /// and **append**=`false`, all existing synonyms for the entity value are discarded and replaced with the new
        /// synonyms.
        ///
        /// If **append**=`true`, existing elements are preserved, and the new elements are added. If any elements in
        /// the new data collide with existing elements, the update request fails. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Value" />Value</returns>
        public DetailedResponse<Value> UpdateValue(string workspaceId, string entity, string value, string newValue = null, Dictionary<string, object> newMetadata = null, string newType = null, List<string> newSynonyms = null, List<string> newPatterns = null, bool? append = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `UpdateValue`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(entity))
            {
                throw new ArgumentNullException("`entity` is required for `UpdateValue`");
            }
            else
            {
                entity = Uri.EscapeDataString(entity);
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("`value` is required for `UpdateValue`");
            }
            else
            {
                value = Uri.EscapeDataString(value);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<Value> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (append != null)
                {
                    restRequest.WithArgument("append", append);
                }
                if (includeAudit != null)
                {
                    restRequest.WithArgument("include_audit", includeAudit);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(newValue))
                {
                    bodyObject["value"] = newValue;
                }
                if (newMetadata != null)
                {
                    bodyObject["metadata"] = JToken.FromObject(newMetadata);
                }
                if (!string.IsNullOrEmpty(newType))
                {
                    bodyObject["type"] = newType;
                }
                if (newSynonyms != null && newSynonyms.Count > 0)
                {
                    bodyObject["synonyms"] = JToken.FromObject(newSynonyms);
                }
                if (newPatterns != null && newPatterns.Count > 0)
                {
                    bodyObject["patterns"] = JToken.FromObject(newPatterns);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "UpdateValue"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Value>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Value>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete entity value.
        ///
        /// Delete a value from an entity.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteValue(string workspaceId, string entity, string value)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `DeleteValue`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(entity))
            {
                throw new ArgumentNullException("`entity` is required for `DeleteValue`");
            }
            else
            {
                entity = Uri.EscapeDataString(entity);
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("`value` is required for `DeleteValue`");
            }
            else
            {
                value = Uri.EscapeDataString(value);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "DeleteValue"));
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
        /// List entity value synonyms.
        ///
        /// List the synonyms for an entity value.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records that satisfy the
        /// request, regardless of the page limit. If this parameter is `true`, the `pagination` object in the response
        /// includes the `total` property. (optional, default to false)</param>
        /// <param name="sort">The attribute by which returned entity value synonyms will be sorted. To reverse the sort
        /// order, prefix the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="SynonymCollection" />SynonymCollection</returns>
        public DetailedResponse<SynonymCollection> ListSynonyms(string workspaceId, string entity, string value, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `ListSynonyms`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(entity))
            {
                throw new ArgumentNullException("`entity` is required for `ListSynonyms`");
            }
            else
            {
                entity = Uri.EscapeDataString(entity);
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("`value` is required for `ListSynonyms`");
            }
            else
            {
                value = Uri.EscapeDataString(value);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<SynonymCollection> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms");

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

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "ListSynonyms"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<SynonymCollection>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<SynonymCollection>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for ListSynonyms.
        /// </summary>
        public class ListSynonymsEnums
        {
            /// <summary>
            /// The attribute by which returned entity value synonyms will be sorted. To reverse the sort order, prefix
            /// the value with a minus sign (`-`).
            /// </summary>
            public class SortValue
            {
                /// <summary>
                /// Constant SYNONYM for synonym
                /// </summary>
                public const string SYNONYM = "synonym";
                /// <summary>
                /// Constant UPDATED for updated
                /// </summary>
                public const string UPDATED = "updated";
                
            }
        }

        /// <summary>
        /// Create entity value synonym.
        ///
        /// Add a new synonym to an entity value.
        ///
        /// If you want to create multiple synonyms with a single API call, consider using the **[Update
        /// entity](#update-entity)** or **[Update entity value](#update-entity-value)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="synonym">The text of the synonym. This string must conform to the following restrictions:
        /// - It cannot contain carriage return, newline, or tab characters.
        /// - It cannot consist of only whitespace characters.</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Synonym" />Synonym</returns>
        public DetailedResponse<Synonym> CreateSynonym(string workspaceId, string entity, string value, string synonym, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `CreateSynonym`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(entity))
            {
                throw new ArgumentNullException("`entity` is required for `CreateSynonym`");
            }
            else
            {
                entity = Uri.EscapeDataString(entity);
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("`value` is required for `CreateSynonym`");
            }
            else
            {
                value = Uri.EscapeDataString(value);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(synonym))
            {
                throw new ArgumentNullException("`synonym` is required for `CreateSynonym`");
            }
            DetailedResponse<Synonym> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms");

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
                if (!string.IsNullOrEmpty(synonym))
                {
                    bodyObject["synonym"] = synonym;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "CreateSynonym"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Synonym>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Synonym>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get entity value synonym.
        ///
        /// Get information about a synonym of an entity value.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="synonym">The text of the synonym.</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Synonym" />Synonym</returns>
        public DetailedResponse<Synonym> GetSynonym(string workspaceId, string entity, string value, string synonym, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `GetSynonym`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(entity))
            {
                throw new ArgumentNullException("`entity` is required for `GetSynonym`");
            }
            else
            {
                entity = Uri.EscapeDataString(entity);
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("`value` is required for `GetSynonym`");
            }
            else
            {
                value = Uri.EscapeDataString(value);
            }
            if (string.IsNullOrEmpty(synonym))
            {
                throw new ArgumentNullException("`synonym` is required for `GetSynonym`");
            }
            else
            {
                synonym = Uri.EscapeDataString(synonym);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<Synonym> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms/{synonym}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (includeAudit != null)
                {
                    restRequest.WithArgument("include_audit", includeAudit);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "GetSynonym"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Synonym>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Synonym>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update entity value synonym.
        ///
        /// Update an existing entity value synonym with new text.
        ///
        /// If you want to update multiple synonyms with a single API call, consider using the **[Update
        /// entity](#update-entity)** or **[Update entity value](#update-entity-value)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="synonym">The text of the synonym.</param>
        /// <param name="newSynonym">The text of the synonym. This string must conform to the following restrictions:
        /// - It cannot contain carriage return, newline, or tab characters.
        /// - It cannot consist of only whitespace characters. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Synonym" />Synonym</returns>
        public DetailedResponse<Synonym> UpdateSynonym(string workspaceId, string entity, string value, string synonym, string newSynonym = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `UpdateSynonym`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(entity))
            {
                throw new ArgumentNullException("`entity` is required for `UpdateSynonym`");
            }
            else
            {
                entity = Uri.EscapeDataString(entity);
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("`value` is required for `UpdateSynonym`");
            }
            else
            {
                value = Uri.EscapeDataString(value);
            }
            if (string.IsNullOrEmpty(synonym))
            {
                throw new ArgumentNullException("`synonym` is required for `UpdateSynonym`");
            }
            else
            {
                synonym = Uri.EscapeDataString(synonym);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<Synonym> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms/{synonym}");

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
                if (!string.IsNullOrEmpty(newSynonym))
                {
                    bodyObject["synonym"] = newSynonym;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "UpdateSynonym"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Synonym>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Synonym>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete entity value synonym.
        ///
        /// Delete a synonym from an entity value.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="synonym">The text of the synonym.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteSynonym(string workspaceId, string entity, string value, string synonym)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `DeleteSynonym`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(entity))
            {
                throw new ArgumentNullException("`entity` is required for `DeleteSynonym`");
            }
            else
            {
                entity = Uri.EscapeDataString(entity);
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("`value` is required for `DeleteSynonym`");
            }
            else
            {
                value = Uri.EscapeDataString(value);
            }
            if (string.IsNullOrEmpty(synonym))
            {
                throw new ArgumentNullException("`synonym` is required for `DeleteSynonym`");
            }
            else
            {
                synonym = Uri.EscapeDataString(synonym);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms/{synonym}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "DeleteSynonym"));
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
        /// List dialog nodes.
        ///
        /// List the dialog nodes for a workspace.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records that satisfy the
        /// request, regardless of the page limit. If this parameter is `true`, the `pagination` object in the response
        /// includes the `total` property. (optional, default to false)</param>
        /// <param name="sort">The attribute by which returned dialog nodes will be sorted. To reverse the sort order,
        /// prefix the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="DialogNodeCollection" />DialogNodeCollection</returns>
        public DetailedResponse<DialogNodeCollection> ListDialogNodes(string workspaceId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `ListDialogNodes`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<DialogNodeCollection> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/dialog_nodes");

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

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "ListDialogNodes"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DialogNodeCollection>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DialogNodeCollection>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for ListDialogNodes.
        /// </summary>
        public class ListDialogNodesEnums
        {
            /// <summary>
            /// The attribute by which returned dialog nodes will be sorted. To reverse the sort order, prefix the value
            /// with a minus sign (`-`).
            /// </summary>
            public class SortValue
            {
                /// <summary>
                /// Constant DIALOG_NODE for dialog_node
                /// </summary>
                public const string DIALOG_NODE = "dialog_node";
                /// <summary>
                /// Constant UPDATED for updated
                /// </summary>
                public const string UPDATED = "updated";
                
            }
        }

        /// <summary>
        /// Create dialog node.
        ///
        /// Create a new dialog node.
        ///
        /// If you want to create multiple dialog nodes with a single API call, consider using the **[Update
        /// workspace](#update-workspace)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="dialogNode">The unique ID of the dialog node. This is an internal identifier used to refer to
        /// the dialog node from other dialog nodes and in the diagnostic information included with message responses.
        ///
        /// This string can contain only Unicode alphanumeric, space, underscore, hyphen, and dot characters.</param>
        /// <param name="description">The description of the dialog node. This string cannot contain carriage return,
        /// newline, or tab characters. (optional)</param>
        /// <param name="conditions">The condition that will trigger the dialog node. This string cannot contain
        /// carriage return, newline, or tab characters. (optional)</param>
        /// <param name="parent">The unique ID of the parent dialog node. This property is omitted if the dialog node
        /// has no parent. (optional)</param>
        /// <param name="previousSibling">The unique ID of the previous sibling dialog node. This property is omitted if
        /// the dialog node has no previous sibling. (optional)</param>
        /// <param name="output">The output of the dialog node. For more information about how to specify dialog node
        /// output, see the
        /// [documentation](https://cloud.ibm.com/docs/assistant?topic=assistant-dialog-overview#dialog-overview-responses).
        /// (optional)</param>
        /// <param name="context">The context for the dialog node. (optional)</param>
        /// <param name="metadata">The metadata for the dialog node. (optional)</param>
        /// <param name="nextStep">The next step to execute following this dialog node. (optional)</param>
        /// <param name="title">A human-readable name for the dialog node. If the node is included in disambiguation,
        /// this title is used to populate the **label** property of the corresponding suggestion in the `suggestion`
        /// response type (unless it is overridden by the **user_label** property). The title is also used to populate
        /// the **topic** property in the `connect_to_agent` response type.
        ///
        /// This string can contain only Unicode alphanumeric, space, underscore, hyphen, and dot characters.
        /// (optional)</param>
        /// <param name="type">How the dialog node is processed. (optional)</param>
        /// <param name="eventName">How an `event_handler` node is processed. (optional)</param>
        /// <param name="variable">The location in the dialog context where output is stored. (optional)</param>
        /// <param name="actions">An array of objects describing any actions to be invoked by the dialog node.
        /// (optional)</param>
        /// <param name="digressIn">Whether this top-level dialog node can be digressed into. (optional)</param>
        /// <param name="digressOut">Whether this dialog node can be returned to after a digression. (optional)</param>
        /// <param name="digressOutSlots">Whether the user can digress to top-level nodes while filling out slots.
        /// (optional)</param>
        /// <param name="userLabel">A label that can be displayed externally to describe the purpose of the node to
        /// users. If set, this label is used to identify the node in disambiguation responses (overriding the value of
        /// the **title** property). (optional)</param>
        /// <param name="disambiguationOptOut">Whether the dialog node should be excluded from disambiguation
        /// suggestions. Valid only when **type**=`standard` or `frame`. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="DialogNode" />DialogNode</returns>
        public DetailedResponse<DialogNode> CreateDialogNode(string workspaceId, string dialogNode, string description = null, string conditions = null, string parent = null, string previousSibling = null, DialogNodeOutput output = null, DialogNodeContext context = null, Dictionary<string, object> metadata = null, DialogNodeNextStep nextStep = null, string title = null, string type = null, string eventName = null, string variable = null, List<DialogNodeAction> actions = null, string digressIn = null, string digressOut = null, string digressOutSlots = null, string userLabel = null, bool? disambiguationOptOut = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `CreateDialogNode`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(dialogNode))
            {
                throw new ArgumentNullException("`dialogNode` is required for `CreateDialogNode`");
            }
            DetailedResponse<DialogNode> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/dialog_nodes");

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
                if (!string.IsNullOrEmpty(dialogNode))
                {
                    bodyObject["dialog_node"] = dialogNode;
                }
                if (!string.IsNullOrEmpty(description))
                {
                    bodyObject["description"] = description;
                }
                if (!string.IsNullOrEmpty(conditions))
                {
                    bodyObject["conditions"] = conditions;
                }
                if (!string.IsNullOrEmpty(parent))
                {
                    bodyObject["parent"] = parent;
                }
                if (!string.IsNullOrEmpty(previousSibling))
                {
                    bodyObject["previous_sibling"] = previousSibling;
                }
                if (output != null)
                {
                    bodyObject["output"] = JToken.FromObject(output);
                }
                if (context != null)
                {
                    bodyObject["context"] = JToken.FromObject(context);
                }
                if (metadata != null)
                {
                    bodyObject["metadata"] = JToken.FromObject(metadata);
                }
                if (nextStep != null)
                {
                    bodyObject["next_step"] = JToken.FromObject(nextStep);
                }
                if (!string.IsNullOrEmpty(title))
                {
                    bodyObject["title"] = title;
                }
                if (!string.IsNullOrEmpty(type))
                {
                    bodyObject["type"] = type;
                }
                if (!string.IsNullOrEmpty(eventName))
                {
                    bodyObject["event_name"] = eventName;
                }
                if (!string.IsNullOrEmpty(variable))
                {
                    bodyObject["variable"] = variable;
                }
                if (actions != null && actions.Count > 0)
                {
                    bodyObject["actions"] = JToken.FromObject(actions);
                }
                if (!string.IsNullOrEmpty(digressIn))
                {
                    bodyObject["digress_in"] = digressIn;
                }
                if (!string.IsNullOrEmpty(digressOut))
                {
                    bodyObject["digress_out"] = digressOut;
                }
                if (!string.IsNullOrEmpty(digressOutSlots))
                {
                    bodyObject["digress_out_slots"] = digressOutSlots;
                }
                if (!string.IsNullOrEmpty(userLabel))
                {
                    bodyObject["user_label"] = userLabel;
                }
                if (disambiguationOptOut != null)
                {
                    bodyObject["disambiguation_opt_out"] = JToken.FromObject(disambiguationOptOut);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "CreateDialogNode"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DialogNode>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DialogNode>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get dialog node.
        ///
        /// Get information about a dialog node.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="dialogNode">The dialog node ID (for example, `node_1_1479323581900`).</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="DialogNode" />DialogNode</returns>
        public DetailedResponse<DialogNode> GetDialogNode(string workspaceId, string dialogNode, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `GetDialogNode`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(dialogNode))
            {
                throw new ArgumentNullException("`dialogNode` is required for `GetDialogNode`");
            }
            else
            {
                dialogNode = Uri.EscapeDataString(dialogNode);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<DialogNode> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/dialog_nodes/{dialogNode}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (includeAudit != null)
                {
                    restRequest.WithArgument("include_audit", includeAudit);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "GetDialogNode"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DialogNode>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DialogNode>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update dialog node.
        ///
        /// Update an existing dialog node with new or modified data.
        ///
        /// If you want to update multiple dialog nodes with a single API call, consider using the **[Update
        /// workspace](#update-workspace)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="dialogNode">The dialog node ID (for example, `node_1_1479323581900`).</param>
        /// <param name="newDialogNode">The unique ID of the dialog node. This is an internal identifier used to refer
        /// to the dialog node from other dialog nodes and in the diagnostic information included with message
        /// responses.
        ///
        /// This string can contain only Unicode alphanumeric, space, underscore, hyphen, and dot characters.
        /// (optional)</param>
        /// <param name="newDescription">The description of the dialog node. This string cannot contain carriage return,
        /// newline, or tab characters. (optional)</param>
        /// <param name="newConditions">The condition that will trigger the dialog node. This string cannot contain
        /// carriage return, newline, or tab characters. (optional)</param>
        /// <param name="newParent">The unique ID of the parent dialog node. This property is omitted if the dialog node
        /// has no parent. (optional)</param>
        /// <param name="newPreviousSibling">The unique ID of the previous sibling dialog node. This property is omitted
        /// if the dialog node has no previous sibling. (optional)</param>
        /// <param name="newOutput">The output of the dialog node. For more information about how to specify dialog node
        /// output, see the
        /// [documentation](https://cloud.ibm.com/docs/assistant?topic=assistant-dialog-overview#dialog-overview-responses).
        /// (optional)</param>
        /// <param name="newContext">The context for the dialog node. (optional)</param>
        /// <param name="newMetadata">The metadata for the dialog node. (optional)</param>
        /// <param name="newNextStep">The next step to execute following this dialog node. (optional)</param>
        /// <param name="newTitle">A human-readable name for the dialog node. If the node is included in disambiguation,
        /// this title is used to populate the **label** property of the corresponding suggestion in the `suggestion`
        /// response type (unless it is overridden by the **user_label** property). The title is also used to populate
        /// the **topic** property in the `connect_to_agent` response type.
        ///
        /// This string can contain only Unicode alphanumeric, space, underscore, hyphen, and dot characters.
        /// (optional)</param>
        /// <param name="newType">How the dialog node is processed. (optional)</param>
        /// <param name="newEventName">How an `event_handler` node is processed. (optional)</param>
        /// <param name="newVariable">The location in the dialog context where output is stored. (optional)</param>
        /// <param name="newActions">An array of objects describing any actions to be invoked by the dialog node.
        /// (optional)</param>
        /// <param name="newDigressIn">Whether this top-level dialog node can be digressed into. (optional)</param>
        /// <param name="newDigressOut">Whether this dialog node can be returned to after a digression.
        /// (optional)</param>
        /// <param name="newDigressOutSlots">Whether the user can digress to top-level nodes while filling out slots.
        /// (optional)</param>
        /// <param name="newUserLabel">A label that can be displayed externally to describe the purpose of the node to
        /// users. If set, this label is used to identify the node in disambiguation responses (overriding the value of
        /// the **title** property). (optional)</param>
        /// <param name="newDisambiguationOptOut">Whether the dialog node should be excluded from disambiguation
        /// suggestions. Valid only when **type**=`standard` or `frame`. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="DialogNode" />DialogNode</returns>
        public DetailedResponse<DialogNode> UpdateDialogNode(string workspaceId, string dialogNode, string newDialogNode = null, string newDescription = null, string newConditions = null, string newParent = null, string newPreviousSibling = null, DialogNodeOutput newOutput = null, DialogNodeContext newContext = null, Dictionary<string, object> newMetadata = null, DialogNodeNextStep newNextStep = null, string newTitle = null, string newType = null, string newEventName = null, string newVariable = null, List<DialogNodeAction> newActions = null, string newDigressIn = null, string newDigressOut = null, string newDigressOutSlots = null, string newUserLabel = null, bool? newDisambiguationOptOut = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `UpdateDialogNode`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(dialogNode))
            {
                throw new ArgumentNullException("`dialogNode` is required for `UpdateDialogNode`");
            }
            else
            {
                dialogNode = Uri.EscapeDataString(dialogNode);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<DialogNode> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/dialog_nodes/{dialogNode}");

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
                if (!string.IsNullOrEmpty(newDialogNode))
                {
                    bodyObject["dialog_node"] = newDialogNode;
                }
                if (!string.IsNullOrEmpty(newDescription))
                {
                    bodyObject["description"] = newDescription;
                }
                if (!string.IsNullOrEmpty(newConditions))
                {
                    bodyObject["conditions"] = newConditions;
                }
                if (!string.IsNullOrEmpty(newParent))
                {
                    bodyObject["parent"] = newParent;
                }
                if (!string.IsNullOrEmpty(newPreviousSibling))
                {
                    bodyObject["previous_sibling"] = newPreviousSibling;
                }
                if (newOutput != null)
                {
                    bodyObject["output"] = JToken.FromObject(newOutput);
                }
                if (newContext != null)
                {
                    bodyObject["context"] = JToken.FromObject(newContext);
                }
                if (newMetadata != null)
                {
                    bodyObject["metadata"] = JToken.FromObject(newMetadata);
                }
                if (newNextStep != null)
                {
                    bodyObject["next_step"] = JToken.FromObject(newNextStep);
                }
                if (!string.IsNullOrEmpty(newTitle))
                {
                    bodyObject["title"] = newTitle;
                }
                if (!string.IsNullOrEmpty(newType))
                {
                    bodyObject["type"] = newType;
                }
                if (!string.IsNullOrEmpty(newEventName))
                {
                    bodyObject["event_name"] = newEventName;
                }
                if (!string.IsNullOrEmpty(newVariable))
                {
                    bodyObject["variable"] = newVariable;
                }
                if (newActions != null && newActions.Count > 0)
                {
                    bodyObject["actions"] = JToken.FromObject(newActions);
                }
                if (!string.IsNullOrEmpty(newDigressIn))
                {
                    bodyObject["digress_in"] = newDigressIn;
                }
                if (!string.IsNullOrEmpty(newDigressOut))
                {
                    bodyObject["digress_out"] = newDigressOut;
                }
                if (!string.IsNullOrEmpty(newDigressOutSlots))
                {
                    bodyObject["digress_out_slots"] = newDigressOutSlots;
                }
                if (!string.IsNullOrEmpty(newUserLabel))
                {
                    bodyObject["user_label"] = newUserLabel;
                }
                if (newDisambiguationOptOut != null)
                {
                    bodyObject["disambiguation_opt_out"] = JToken.FromObject(newDisambiguationOptOut);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "UpdateDialogNode"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DialogNode>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DialogNode>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete dialog node.
        ///
        /// Delete a dialog node from a workspace.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="dialogNode">The dialog node ID (for example, `node_1_1479323581900`).</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteDialogNode(string workspaceId, string dialogNode)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `DeleteDialogNode`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
            }
            if (string.IsNullOrEmpty(dialogNode))
            {
                throw new ArgumentNullException("`dialogNode` is required for `DeleteDialogNode`");
            }
            else
            {
                dialogNode = Uri.EscapeDataString(dialogNode);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/dialog_nodes/{dialogNode}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "DeleteDialogNode"));
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
        /// List log events in a workspace.
        ///
        /// List the events from the log of a specific workspace.
        ///
        /// This method requires Manager access.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="sort">How to sort the returned log events. You can sort by **request_timestamp**. To reverse
        /// the sort order, prefix the parameter value with a minus sign (`-`). (optional)</param>
        /// <param name="filter">A cacheable parameter that limits the results to those matching the specified filter.
        /// For more information, see the
        /// [documentation](https://cloud.ibm.com/docs/assistant?topic=assistant-filter-reference#filter-reference).
        /// (optional)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <returns><see cref="LogCollection" />LogCollection</returns>
        public DetailedResponse<LogCollection> ListLogs(string workspaceId, string sort = null, string filter = null, long? pageLimit = null, string cursor = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
            {
                throw new ArgumentNullException("`workspaceId` is required for `ListLogs`");
            }
            else
            {
                workspaceId = Uri.EscapeDataString(workspaceId);
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

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/logs");

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

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "ListLogs"));
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
        /// List log events in all workspaces.
        ///
        /// List the events from the logs of all workspaces in the service instance.
        /// </summary>
        /// <param name="filter">A cacheable parameter that limits the results to those matching the specified filter.
        /// You must specify a filter query that includes a value for `language`, as well as a value for
        /// `request.context.system.assistant_id`, `workspace_id`, or `request.context.metadata.deployment`. These
        /// required filters must be specified using the exact match (`::`) operator. For more information, see the
        /// [documentation](https://cloud.ibm.com/docs/assistant?topic=assistant-filter-reference#filter-reference).</param>
        /// <param name="sort">How to sort the returned log events. You can sort by **request_timestamp**. To reverse
        /// the sort order, prefix the parameter value with a minus sign (`-`). (optional)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <returns><see cref="LogCollection" />LogCollection</returns>
        public DetailedResponse<LogCollection> ListAllLogs(string filter, string sort = null, long? pageLimit = null, string cursor = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(filter))
            {
                throw new ArgumentNullException("`filter` is required for `ListAllLogs`");
            }
            DetailedResponse<LogCollection> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/logs");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (!string.IsNullOrEmpty(filter))
                {
                    restRequest.WithArgument("filter", filter);
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    restRequest.WithArgument("sort", sort);
                }
                if (pageLimit != null)
                {
                    restRequest.WithArgument("page_limit", pageLimit);
                }
                if (!string.IsNullOrEmpty(cursor))
                {
                    restRequest.WithArgument("cursor", cursor);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "ListAllLogs"));
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

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/user_data");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (!string.IsNullOrEmpty(customerId))
                {
                    restRequest.WithArgument("customer_id", customerId);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("conversation", "v1", "DeleteUserData"));
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
    }
}
