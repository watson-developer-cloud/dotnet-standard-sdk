/**
* Copyright 2018, 2019 IBM Corp. All Rights Reserved.
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
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Service;
using IBM.Cloud.SDK.Core.Util;
using IBM.Watson.Assistant.v1.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace IBM.Watson.Assistant.v1
{
    public partial class AssistantService : IBMService, IAssistantService
    {
        new const string SERVICE_NAME = "assistant";
        const string URL = "https://gateway.watsonplatform.net/assistant/api";
        private string _versionDate;
        public string VersionDate
        {
            get { return _versionDate; }
            set { _versionDate = value; }
        }

        public AssistantService() : base(SERVICE_NAME) { }
        
        public AssistantService(string userName, string password, string versionDate) : base(SERVICE_NAME, URL)
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
        
        public AssistantService(TokenOptions options, string versionDate) : base(SERVICE_NAME, URL)
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

            _tokenManager = new TokenManager(options);
        }

        public AssistantService(IClient httpClient) : base(SERVICE_NAME, URL)
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        /// <summary>
        /// Get response to user input.
        ///
        /// Send user input to a workspace and receive a response.
        ///
        /// There is no rate limit for this operation.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="request">The message to be sent. This includes the user's input, along with optional intents,
        /// entities, and context from the last response. (optional)</param>
        /// <param name="nodesVisitedDetails">Whether to include additional diagnostic information about the dialog
        /// nodes that were visited during processing of the message. (optional, default to false)</param>
        /// <returns><see cref="MessageResponse" />MessageResponse</returns>
        public DetailedResponse<MessageResponse> Message(string workspaceId, JObject input = null, List<JObject> intents = null, List<JObject> entities = null, bool? alternateIntents = null, JObject context = null, JObject output = null, bool? nodesVisitedDetails = null)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `Message`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<MessageResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/message");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (nodesVisitedDetails != null)
                    restRequest.WithArgument("nodes_visited_details", nodesVisitedDetails);
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (input != null)
                    bodyObject["input"] = JToken.FromObject(input);
                if (intents != null && intents.Count > 0)
                    bodyObject["intents"] = JToken.FromObject(intents);
                if (entities != null && entities.Count > 0)
                    bodyObject["entities"] = JToken.FromObject(entities);
                if (alternateIntents != null)
                    bodyObject["alternate_intents"] = JToken.FromObject(alternateIntents);
                if (context != null)
                    bodyObject["context"] = JToken.FromObject(context);
                if (output != null)
                    bodyObject["output"] = JToken.FromObject(output);
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "Message"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<MessageResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<MessageResponse>();
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
        ///
        /// This operation is limited to 500 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional,
        /// default to false)</param>
        /// <param name="sort">The attribute by which returned workspaces will be sorted. To reverse the sort order,
        /// prefix the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="WorkspaceCollection" />WorkspaceCollection</returns>
        public DetailedResponse<WorkspaceCollection> ListWorkspaces(long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<WorkspaceCollection> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (pageLimit != null)
                    restRequest.WithArgument("page_limit", pageLimit);
                if (includeCount != null)
                    restRequest.WithArgument("include_count", includeCount);
                if (!string.IsNullOrEmpty(sort))
                    restRequest.WithArgument("sort", sort);
                if (!string.IsNullOrEmpty(cursor))
                    restRequest.WithArgument("cursor", cursor);
                if (includeAudit != null)
                    restRequest.WithArgument("include_audit", includeAudit);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "ListWorkspaces"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<WorkspaceCollection>().Result;
                if (result == null)
                    result = new DetailedResponse<WorkspaceCollection>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Create workspace.
        ///
        /// Create a workspace based on component objects. You must provide workspace components defining the content of
        /// the new workspace.
        ///
        /// This operation is limited to 30 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="properties">The content of the new workspace.
        ///
        /// The maximum size for this data is 50MB. If you need to import a larger workspace, consider importing the
        /// workspace without intents and entities and then adding them separately. (optional)</param>
        /// <returns><see cref="Workspace" />Workspace</returns>
        public DetailedResponse<Workspace> CreateWorkspace(string name = null, string description = null, string language = null, Dictionary<string, object> metadata = null, bool? learningOptOut = null, WorkspaceSystemSettings systemSettings = null, List<CreateIntent> intents = null, List<CreateEntity> entities = null, List<DialogNode> dialogNodes = null, List<Counterexample> counterexamples = null)
        {

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Workspace> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(name))
                    bodyObject["name"] = name;
                if (!string.IsNullOrEmpty(description))
                    bodyObject["description"] = description;
                if (!string.IsNullOrEmpty(language))
                    bodyObject["language"] = language;
                if (metadata != null)
                    bodyObject["metadata"] = JToken.FromObject(metadata);
                if (learningOptOut != null)
                    bodyObject["learning_opt_out"] = JToken.FromObject(learningOptOut);
                if (systemSettings != null)
                    bodyObject["system_settings"] = JToken.FromObject(systemSettings);
                if (intents != null && intents.Count > 0)
                    bodyObject["intents"] = JToken.FromObject(intents);
                if (entities != null && entities.Count > 0)
                    bodyObject["entities"] = JToken.FromObject(entities);
                if (dialogNodes != null && dialogNodes.Count > 0)
                    bodyObject["dialog_nodes"] = JToken.FromObject(dialogNodes);
                if (counterexamples != null && counterexamples.Count > 0)
                    bodyObject["counterexamples"] = JToken.FromObject(counterexamples);
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "CreateWorkspace"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Workspace>().Result;
                if (result == null)
                    result = new DetailedResponse<Workspace>();
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
        ///
        /// With **export**=`false`, this operation is limited to 6000 requests per 5 minutes. With **export**=`true`,
        /// the limit is 20 requests per 30 minutes. For more information, see **Rate limiting**.
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
            throw new ArgumentNullException("`workspaceId` is required for `GetWorkspace`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Workspace> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (export != null)
                    restRequest.WithArgument("export", export);
                if (includeAudit != null)
                    restRequest.WithArgument("include_audit", includeAudit);
                if (!string.IsNullOrEmpty(sort))
                    restRequest.WithArgument("sort", sort);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "GetWorkspace"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Workspace>().Result;
                if (result == null)
                    result = new DetailedResponse<Workspace>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update workspace.
        ///
        /// Update an existing workspace with new or modified data. You must provide component objects defining the
        /// content of the updated workspace.
        ///
        /// This operation is limited to 30 request per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="properties">Valid data defining the new and updated workspace content.
        ///
        /// The maximum size for this data is 50MB. If you need to import a larger amount of workspace data, consider
        /// importing components such as intents and entities using separate operations. (optional)</param>
        /// <param name="append">Whether the new data is to be appended to the existing data in the workspace. If
        /// **append**=`false`, elements included in the new data completely replace the corresponding existing
        /// elements, including all subelements. For example, if the new data includes **entities** and
        /// **append**=`false`, all existing entities in the workspace are discarded and replaced with the new entities.
        ///
        /// If **append**=`true`, existing elements are preserved, and the new elements are added. If any elements in
        /// the new data collide with existing elements, the update request fails. (optional, default to false)</param>
        /// <returns><see cref="Workspace" />Workspace</returns>
        public DetailedResponse<Workspace> UpdateWorkspace(string workspaceId, string name = null, string description = null, string language = null, Dictionary<string, object> metadata = null, bool? learningOptOut = null, WorkspaceSystemSettings systemSettings = null, List<CreateIntent> intents = null, List<CreateEntity> entities = null, List<DialogNode> dialogNodes = null, List<Counterexample> counterexamples = null, bool? append = null)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `UpdateWorkspace`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Workspace> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (append != null)
                    restRequest.WithArgument("append", append);
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(name))
                    bodyObject["name"] = name;
                if (!string.IsNullOrEmpty(description))
                    bodyObject["description"] = description;
                if (!string.IsNullOrEmpty(language))
                    bodyObject["language"] = language;
                if (metadata != null)
                    bodyObject["metadata"] = JToken.FromObject(metadata);
                if (learningOptOut != null)
                    bodyObject["learning_opt_out"] = JToken.FromObject(learningOptOut);
                if (systemSettings != null)
                    bodyObject["system_settings"] = JToken.FromObject(systemSettings);
                if (intents != null && intents.Count > 0)
                    bodyObject["intents"] = JToken.FromObject(intents);
                if (entities != null && entities.Count > 0)
                    bodyObject["entities"] = JToken.FromObject(entities);
                if (dialogNodes != null && dialogNodes.Count > 0)
                    bodyObject["dialog_nodes"] = JToken.FromObject(dialogNodes);
                if (counterexamples != null && counterexamples.Count > 0)
                    bodyObject["counterexamples"] = JToken.FromObject(counterexamples);
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "UpdateWorkspace"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Workspace>().Result;
                if (result == null)
                    result = new DetailedResponse<Workspace>();
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
        ///
        /// This operation is limited to 30 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteWorkspace(string workspaceId)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `DeleteWorkspace`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "DeleteWorkspace"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<object>().Result;
                if (result == null)
                    result = new DetailedResponse<object>();
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
        ///
        /// With **export**=`false`, this operation is limited to 2000 requests per 30 minutes. With **export**=`true`,
        /// the limit is 400 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the
        /// returned data includes only information about the element itself. If **export**=`true`, all content,
        /// including subelements, is included. (optional, default to false)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional,
        /// default to false)</param>
        /// <param name="sort">The attribute by which returned intents will be sorted. To reverse the sort order, prefix
        /// the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="IntentCollection" />IntentCollection</returns>
        public DetailedResponse<IntentCollection> ListIntents(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `ListIntents`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<IntentCollection> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (export != null)
                    restRequest.WithArgument("export", export);
                if (pageLimit != null)
                    restRequest.WithArgument("page_limit", pageLimit);
                if (includeCount != null)
                    restRequest.WithArgument("include_count", includeCount);
                if (!string.IsNullOrEmpty(sort))
                    restRequest.WithArgument("sort", sort);
                if (!string.IsNullOrEmpty(cursor))
                    restRequest.WithArgument("cursor", cursor);
                if (includeAudit != null)
                    restRequest.WithArgument("include_audit", includeAudit);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "ListIntents"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<IntentCollection>().Result;
                if (result == null)
                    result = new DetailedResponse<IntentCollection>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Create intent.
        ///
        /// Create a new intent.
        ///
        /// If you want to create multiple intents with a single API call, consider using the **[Update
        /// workspace](#update-workspace)** method instead.
        ///
        /// This operation is limited to 2000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="body">The content of the new intent.</param>
        /// <returns><see cref="Intent" />Intent</returns>
        public DetailedResponse<Intent> CreateIntent(string workspaceId, string intent, string description = null, List<Example> examples = null)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `CreateIntent`");
        if (string.IsNullOrEmpty(intent))
            throw new ArgumentNullException("`intent` is required for `CreateIntent`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Intent> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(intent))
                    bodyObject["intent"] = intent;
                if (!string.IsNullOrEmpty(description))
                    bodyObject["description"] = description;
                if (examples != null && examples.Count > 0)
                    bodyObject["examples"] = JToken.FromObject(examples);
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "CreateIntent"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Intent>().Result;
                if (result == null)
                    result = new DetailedResponse<Intent>();
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
        ///
        /// With **export**=`false`, this operation is limited to 6000 requests per 5 minutes. With **export**=`true`,
        /// the limit is 400 requests per 30 minutes. For more information, see **Rate limiting**.
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
            throw new ArgumentNullException("`workspaceId` is required for `GetIntent`");
        if (string.IsNullOrEmpty(intent))
            throw new ArgumentNullException("`intent` is required for `GetIntent`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Intent> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (export != null)
                    restRequest.WithArgument("export", export);
                if (includeAudit != null)
                    restRequest.WithArgument("include_audit", includeAudit);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "GetIntent"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Intent>().Result;
                if (result == null)
                    result = new DetailedResponse<Intent>();
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
        ///
        /// This operation is limited to 2000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="body">The updated content of the intent.
        ///
        /// Any elements included in the new data will completely replace the equivalent existing elements, including
        /// all subelements. (Previously existing subelements are not retained unless they are also included in the new
        /// data.) For example, if you update the user input examples for an intent, the previously existing examples
        /// are discarded and replaced with the new examples specified in the update.</param>
        /// <returns><see cref="Intent" />Intent</returns>
        public DetailedResponse<Intent> UpdateIntent(string workspaceId, string intent, string newIntent = null, string newDescription = null, List<Example> newExamples = null)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `UpdateIntent`");
        if (string.IsNullOrEmpty(intent))
            throw new ArgumentNullException("`intent` is required for `UpdateIntent`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Intent> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(newIntent))
                    bodyObject["intent"] = newIntent;
                if (!string.IsNullOrEmpty(newDescription))
                    bodyObject["description"] = newDescription;
                if (newExamples != null && newExamples.Count > 0)
                    bodyObject["examples"] = JToken.FromObject(newExamples);
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "UpdateIntent"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Intent>().Result;
                if (result == null)
                    result = new DetailedResponse<Intent>();
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
        ///
        /// This operation is limited to 2000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteIntent(string workspaceId, string intent)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `DeleteIntent`");
        if (string.IsNullOrEmpty(intent))
            throw new ArgumentNullException("`intent` is required for `DeleteIntent`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "DeleteIntent"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<object>().Result;
                if (result == null)
                    result = new DetailedResponse<object>();
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
        ///
        /// This operation is limited to 2500 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional,
        /// default to false)</param>
        /// <param name="sort">The attribute by which returned examples will be sorted. To reverse the sort order,
        /// prefix the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="ExampleCollection" />ExampleCollection</returns>
        public DetailedResponse<ExampleCollection> ListExamples(string workspaceId, string intent, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `ListExamples`");
        if (string.IsNullOrEmpty(intent))
            throw new ArgumentNullException("`intent` is required for `ListExamples`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<ExampleCollection> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (pageLimit != null)
                    restRequest.WithArgument("page_limit", pageLimit);
                if (includeCount != null)
                    restRequest.WithArgument("include_count", includeCount);
                if (!string.IsNullOrEmpty(sort))
                    restRequest.WithArgument("sort", sort);
                if (!string.IsNullOrEmpty(cursor))
                    restRequest.WithArgument("cursor", cursor);
                if (includeAudit != null)
                    restRequest.WithArgument("include_audit", includeAudit);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "ListExamples"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<ExampleCollection>().Result;
                if (result == null)
                    result = new DetailedResponse<ExampleCollection>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Create user input example.
        ///
        /// Add a new user input example to an intent.
        ///
        /// If you want to add multiple exaples with a single API call, consider using the **[Update
        /// intent](#update-intent)** method instead.
        ///
        /// This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="body">The content of the new user input example.</param>
        /// <returns><see cref="Example" />Example</returns>
        public DetailedResponse<Example> CreateExample(string workspaceId, string intent, string text, List<Mention> mentions = null)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `CreateExample`");
        if (string.IsNullOrEmpty(intent))
            throw new ArgumentNullException("`intent` is required for `CreateExample`");
        if (string.IsNullOrEmpty(text))
            throw new ArgumentNullException("`text` is required for `CreateExample`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Example> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(text))
                    bodyObject["text"] = text;
                if (mentions != null && mentions.Count > 0)
                    bodyObject["mentions"] = JToken.FromObject(mentions);
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "CreateExample"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Example>().Result;
                if (result == null)
                    result = new DetailedResponse<Example>();
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
        ///
        /// This operation is limited to 6000 requests per 5 minutes. For more information, see **Rate limiting**.
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
            throw new ArgumentNullException("`workspaceId` is required for `GetExample`");
        if (string.IsNullOrEmpty(intent))
            throw new ArgumentNullException("`intent` is required for `GetExample`");
        if (string.IsNullOrEmpty(text))
            throw new ArgumentNullException("`text` is required for `GetExample`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Example> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples/{text}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (includeAudit != null)
                    restRequest.WithArgument("include_audit", includeAudit);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "GetExample"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Example>().Result;
                if (result == null)
                    result = new DetailedResponse<Example>();
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
        ///
        /// This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="text">The text of the user input example.</param>
        /// <param name="body">The new text of the user input example.</param>
        /// <returns><see cref="Example" />Example</returns>
        public DetailedResponse<Example> UpdateExample(string workspaceId, string intent, string text, string newText = null, List<Mention> newMentions = null)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `UpdateExample`");
        if (string.IsNullOrEmpty(intent))
            throw new ArgumentNullException("`intent` is required for `UpdateExample`");
        if (string.IsNullOrEmpty(text))
            throw new ArgumentNullException("`text` is required for `UpdateExample`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Example> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples/{text}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(newText))
                    bodyObject["text"] = newText;
                if (newMentions != null && newMentions.Count > 0)
                    bodyObject["mentions"] = JToken.FromObject(newMentions);
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "UpdateExample"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Example>().Result;
                if (result == null)
                    result = new DetailedResponse<Example>();
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
        ///
        /// This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="text">The text of the user input example.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteExample(string workspaceId, string intent, string text)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `DeleteExample`");
        if (string.IsNullOrEmpty(intent))
            throw new ArgumentNullException("`intent` is required for `DeleteExample`");
        if (string.IsNullOrEmpty(text))
            throw new ArgumentNullException("`text` is required for `DeleteExample`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples/{text}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "DeleteExample"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<object>().Result;
                if (result == null)
                    result = new DetailedResponse<object>();
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
        ///
        /// This operation is limited to 2500 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional,
        /// default to false)</param>
        /// <param name="sort">The attribute by which returned counterexamples will be sorted. To reverse the sort
        /// order, prefix the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="CounterexampleCollection" />CounterexampleCollection</returns>
        public DetailedResponse<CounterexampleCollection> ListCounterexamples(string workspaceId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `ListCounterexamples`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<CounterexampleCollection> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (pageLimit != null)
                    restRequest.WithArgument("page_limit", pageLimit);
                if (includeCount != null)
                    restRequest.WithArgument("include_count", includeCount);
                if (!string.IsNullOrEmpty(sort))
                    restRequest.WithArgument("sort", sort);
                if (!string.IsNullOrEmpty(cursor))
                    restRequest.WithArgument("cursor", cursor);
                if (includeAudit != null)
                    restRequest.WithArgument("include_audit", includeAudit);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "ListCounterexamples"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<CounterexampleCollection>().Result;
                if (result == null)
                    result = new DetailedResponse<CounterexampleCollection>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Create counterexample.
        ///
        /// Add a new counterexample to a workspace. Counterexamples are examples that have been marked as irrelevant
        /// input.
        ///
        /// If you want to add multiple counterexamples with a single API call, consider using the **[Update
        /// workspace](#update-workspace)** method instead.
        ///
        /// This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="body">The content of the new counterexample.</param>
        /// <returns><see cref="Counterexample" />Counterexample</returns>
        public DetailedResponse<Counterexample> CreateCounterexample(string workspaceId, string text)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `CreateCounterexample`");
        if (string.IsNullOrEmpty(text))
            throw new ArgumentNullException("`text` is required for `CreateCounterexample`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Counterexample> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(text))
                    bodyObject["text"] = text;
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "CreateCounterexample"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Counterexample>().Result;
                if (result == null)
                    result = new DetailedResponse<Counterexample>();
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
        ///
        /// This operation is limited to 6000 requests per 5 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="text">The text of a user input counterexample (for example, `What are you wearing?`).</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Counterexample" />Counterexample</returns>
        public DetailedResponse<Counterexample> GetCounterexample(string workspaceId, string text, bool? includeAudit = null)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `GetCounterexample`");
        if (string.IsNullOrEmpty(text))
            throw new ArgumentNullException("`text` is required for `GetCounterexample`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Counterexample> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples/{text}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (includeAudit != null)
                    restRequest.WithArgument("include_audit", includeAudit);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "GetCounterexample"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Counterexample>().Result;
                if (result == null)
                    result = new DetailedResponse<Counterexample>();
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
        ///
        /// If you want to update multiple counterexamples with a single API call, consider using the **[Update
        /// workspace](#update-workspace)** method instead.
        ///
        /// This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="text">The text of a user input counterexample (for example, `What are you wearing?`).</param>
        /// <param name="body">The text of the counterexample.</param>
        /// <returns><see cref="Counterexample" />Counterexample</returns>
        public DetailedResponse<Counterexample> UpdateCounterexample(string workspaceId, string text, string newText = null)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `UpdateCounterexample`");
        if (string.IsNullOrEmpty(text))
            throw new ArgumentNullException("`text` is required for `UpdateCounterexample`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Counterexample> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples/{text}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(newText))
                    bodyObject["text"] = newText;
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "UpdateCounterexample"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Counterexample>().Result;
                if (result == null)
                    result = new DetailedResponse<Counterexample>();
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
        ///
        /// This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="text">The text of a user input counterexample (for example, `What are you wearing?`).</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteCounterexample(string workspaceId, string text)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `DeleteCounterexample`");
        if (string.IsNullOrEmpty(text))
            throw new ArgumentNullException("`text` is required for `DeleteCounterexample`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples/{text}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "DeleteCounterexample"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<object>().Result;
                if (result == null)
                    result = new DetailedResponse<object>();
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
        ///
        /// With **export**=`false`, this operation is limited to 1000 requests per 30 minutes. With **export**=`true`,
        /// the limit is 200 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the
        /// returned data includes only information about the element itself. If **export**=`true`, all content,
        /// including subelements, is included. (optional, default to false)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional,
        /// default to false)</param>
        /// <param name="sort">The attribute by which returned entities will be sorted. To reverse the sort order,
        /// prefix the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="EntityCollection" />EntityCollection</returns>
        public DetailedResponse<EntityCollection> ListEntities(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `ListEntities`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<EntityCollection> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (export != null)
                    restRequest.WithArgument("export", export);
                if (pageLimit != null)
                    restRequest.WithArgument("page_limit", pageLimit);
                if (includeCount != null)
                    restRequest.WithArgument("include_count", includeCount);
                if (!string.IsNullOrEmpty(sort))
                    restRequest.WithArgument("sort", sort);
                if (!string.IsNullOrEmpty(cursor))
                    restRequest.WithArgument("cursor", cursor);
                if (includeAudit != null)
                    restRequest.WithArgument("include_audit", includeAudit);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "ListEntities"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<EntityCollection>().Result;
                if (result == null)
                    result = new DetailedResponse<EntityCollection>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Create entity.
        ///
        /// Create a new entity, or enable a system entity.
        ///
        /// If you want to create multiple entities with a single API call, consider using the **[Update
        /// workspace](#update-workspace)** method instead.
        ///
        /// This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="properties">The content of the new entity.</param>
        /// <returns><see cref="Entity" />Entity</returns>
        public DetailedResponse<Entity> CreateEntity(string workspaceId, string entity, string description = null, Dictionary<string, object> metadata = null, bool? fuzzyMatch = null, List<CreateValue> values = null)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `CreateEntity`");
        if (string.IsNullOrEmpty(entity))
            throw new ArgumentNullException("`entity` is required for `CreateEntity`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Entity> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(entity))
                    bodyObject["entity"] = entity;
                if (!string.IsNullOrEmpty(description))
                    bodyObject["description"] = description;
                if (metadata != null)
                    bodyObject["metadata"] = JToken.FromObject(metadata);
                if (fuzzyMatch != null)
                    bodyObject["fuzzy_match"] = JToken.FromObject(fuzzyMatch);
                if (values != null && values.Count > 0)
                    bodyObject["values"] = JToken.FromObject(values);
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "CreateEntity"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Entity>().Result;
                if (result == null)
                    result = new DetailedResponse<Entity>();
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
        ///
        /// With **export**=`false`, this operation is limited to 6000 requests per 5 minutes. With **export**=`true`,
        /// the limit is 200 requests per 30 minutes. For more information, see **Rate limiting**.
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
            throw new ArgumentNullException("`workspaceId` is required for `GetEntity`");
        if (string.IsNullOrEmpty(entity))
            throw new ArgumentNullException("`entity` is required for `GetEntity`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Entity> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (export != null)
                    restRequest.WithArgument("export", export);
                if (includeAudit != null)
                    restRequest.WithArgument("include_audit", includeAudit);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "GetEntity"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Entity>().Result;
                if (result == null)
                    result = new DetailedResponse<Entity>();
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
        ///
        /// This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="properties">The updated content of the entity. Any elements included in the new data will
        /// completely replace the equivalent existing elements, including all subelements. (Previously existing
        /// subelements are not retained unless they are also included in the new data.) For example, if you update the
        /// values for an entity, the previously existing values are discarded and replaced with the new values
        /// specified in the update.</param>
        /// <returns><see cref="Entity" />Entity</returns>
        public DetailedResponse<Entity> UpdateEntity(string workspaceId, string entity, string newEntity = null, string newDescription = null, Dictionary<string, object> newMetadata = null, bool? newFuzzyMatch = null, List<CreateValue> newValues = null)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `UpdateEntity`");
        if (string.IsNullOrEmpty(entity))
            throw new ArgumentNullException("`entity` is required for `UpdateEntity`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Entity> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(newEntity))
                    bodyObject["entity"] = newEntity;
                if (!string.IsNullOrEmpty(newDescription))
                    bodyObject["description"] = newDescription;
                if (newMetadata != null)
                    bodyObject["metadata"] = JToken.FromObject(newMetadata);
                if (newFuzzyMatch != null)
                    bodyObject["fuzzy_match"] = JToken.FromObject(newFuzzyMatch);
                if (newValues != null && newValues.Count > 0)
                    bodyObject["values"] = JToken.FromObject(newValues);
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "UpdateEntity"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Entity>().Result;
                if (result == null)
                    result = new DetailedResponse<Entity>();
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
        ///
        /// This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteEntity(string workspaceId, string entity)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `DeleteEntity`");
        if (string.IsNullOrEmpty(entity))
            throw new ArgumentNullException("`entity` is required for `DeleteEntity`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "DeleteEntity"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<object>().Result;
                if (result == null)
                    result = new DetailedResponse<object>();
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
        ///
        /// This operation is limited to 200 requests per 30 minutes. For more information, see **Rate limiting**.
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
            throw new ArgumentNullException("`workspaceId` is required for `ListMentions`");
        if (string.IsNullOrEmpty(entity))
            throw new ArgumentNullException("`entity` is required for `ListMentions`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<EntityMentionCollection> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/mentions");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (export != null)
                    restRequest.WithArgument("export", export);
                if (includeAudit != null)
                    restRequest.WithArgument("include_audit", includeAudit);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "ListMentions"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<EntityMentionCollection>().Result;
                if (result == null)
                    result = new DetailedResponse<EntityMentionCollection>();
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
        ///
        /// This operation is limited to 2500 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the
        /// returned data includes only information about the element itself. If **export**=`true`, all content,
        /// including subelements, is included. (optional, default to false)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional,
        /// default to false)</param>
        /// <param name="sort">The attribute by which returned entity values will be sorted. To reverse the sort order,
        /// prefix the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="ValueCollection" />ValueCollection</returns>
        public DetailedResponse<ValueCollection> ListValues(string workspaceId, string entity, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `ListValues`");
        if (string.IsNullOrEmpty(entity))
            throw new ArgumentNullException("`entity` is required for `ListValues`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<ValueCollection> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (export != null)
                    restRequest.WithArgument("export", export);
                if (pageLimit != null)
                    restRequest.WithArgument("page_limit", pageLimit);
                if (includeCount != null)
                    restRequest.WithArgument("include_count", includeCount);
                if (!string.IsNullOrEmpty(sort))
                    restRequest.WithArgument("sort", sort);
                if (!string.IsNullOrEmpty(cursor))
                    restRequest.WithArgument("cursor", cursor);
                if (includeAudit != null)
                    restRequest.WithArgument("include_audit", includeAudit);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "ListValues"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<ValueCollection>().Result;
                if (result == null)
                    result = new DetailedResponse<ValueCollection>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Create entity value.
        ///
        /// Create a new value for an entity.
        ///
        /// If you want to create multiple entity values with a single API call, consider using the **[Update
        /// entity](#update-entity)** method instead.
        ///
        /// This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="properties">The new entity value.</param>
        /// <returns><see cref="Value" />Value</returns>
        public DetailedResponse<Value> CreateValue(string workspaceId, string entity, string value, Dictionary<string, object> metadata = null, string valueType = null, List<string> synonyms = null, List<string> patterns = null)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `CreateValue`");
        if (string.IsNullOrEmpty(entity))
            throw new ArgumentNullException("`entity` is required for `CreateValue`");
        if (string.IsNullOrEmpty(value))
            throw new ArgumentNullException("`value` is required for `CreateValue`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Value> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(value))
                    bodyObject["value"] = value;
                if (metadata != null)
                    bodyObject["metadata"] = JToken.FromObject(metadata);
                if (!string.IsNullOrEmpty(valueType))
                    bodyObject["type"] = valueType;
                if (synonyms != null && synonyms.Count > 0)
                    bodyObject["synonyms"] = JToken.FromObject(synonyms);
                if (patterns != null && patterns.Count > 0)
                    bodyObject["patterns"] = JToken.FromObject(patterns);
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "CreateValue"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Value>().Result;
                if (result == null)
                    result = new DetailedResponse<Value>();
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
        ///
        /// This operation is limited to 6000 requests per 5 minutes. For more information, see **Rate limiting**.
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
            throw new ArgumentNullException("`workspaceId` is required for `GetValue`");
        if (string.IsNullOrEmpty(entity))
            throw new ArgumentNullException("`entity` is required for `GetValue`");
        if (string.IsNullOrEmpty(value))
            throw new ArgumentNullException("`value` is required for `GetValue`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Value> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (export != null)
                    restRequest.WithArgument("export", export);
                if (includeAudit != null)
                    restRequest.WithArgument("include_audit", includeAudit);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "GetValue"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Value>().Result;
                if (result == null)
                    result = new DetailedResponse<Value>();
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
        ///
        /// This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="properties">The updated content of the entity value.
        ///
        /// Any elements included in the new data will completely replace the equivalent existing elements, including
        /// all subelements. (Previously existing subelements are not retained unless they are also included in the new
        /// data.) For example, if you update the synonyms for an entity value, the previously existing synonyms are
        /// discarded and replaced with the new synonyms specified in the update.</param>
        /// <returns><see cref="Value" />Value</returns>
        public DetailedResponse<Value> UpdateValue(string workspaceId, string entity, string value, string newValue = null, Dictionary<string, object> newMetadata = null, string newValueType = null, List<string> newSynonyms = null, List<string> newPatterns = null)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `UpdateValue`");
        if (string.IsNullOrEmpty(entity))
            throw new ArgumentNullException("`entity` is required for `UpdateValue`");
        if (string.IsNullOrEmpty(value))
            throw new ArgumentNullException("`value` is required for `UpdateValue`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Value> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(newValue))
                    bodyObject["value"] = newValue;
                if (newMetadata != null)
                    bodyObject["metadata"] = JToken.FromObject(newMetadata);
                if (!string.IsNullOrEmpty(newValueType))
                    bodyObject["type"] = newValueType;
                if (newSynonyms != null && newSynonyms.Count > 0)
                    bodyObject["synonyms"] = JToken.FromObject(newSynonyms);
                if (newPatterns != null && newPatterns.Count > 0)
                    bodyObject["patterns"] = JToken.FromObject(newPatterns);
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "UpdateValue"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Value>().Result;
                if (result == null)
                    result = new DetailedResponse<Value>();
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
        ///
        /// This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteValue(string workspaceId, string entity, string value)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `DeleteValue`");
        if (string.IsNullOrEmpty(entity))
            throw new ArgumentNullException("`entity` is required for `DeleteValue`");
        if (string.IsNullOrEmpty(value))
            throw new ArgumentNullException("`value` is required for `DeleteValue`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "DeleteValue"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<object>().Result;
                if (result == null)
                    result = new DetailedResponse<object>();
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
        ///
        /// This operation is limited to 2500 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional,
        /// default to false)</param>
        /// <param name="sort">The attribute by which returned entity value synonyms will be sorted. To reverse the sort
        /// order, prefix the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="SynonymCollection" />SynonymCollection</returns>
        public DetailedResponse<SynonymCollection> ListSynonyms(string workspaceId, string entity, string value, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `ListSynonyms`");
        if (string.IsNullOrEmpty(entity))
            throw new ArgumentNullException("`entity` is required for `ListSynonyms`");
        if (string.IsNullOrEmpty(value))
            throw new ArgumentNullException("`value` is required for `ListSynonyms`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<SynonymCollection> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (pageLimit != null)
                    restRequest.WithArgument("page_limit", pageLimit);
                if (includeCount != null)
                    restRequest.WithArgument("include_count", includeCount);
                if (!string.IsNullOrEmpty(sort))
                    restRequest.WithArgument("sort", sort);
                if (!string.IsNullOrEmpty(cursor))
                    restRequest.WithArgument("cursor", cursor);
                if (includeAudit != null)
                    restRequest.WithArgument("include_audit", includeAudit);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "ListSynonyms"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<SynonymCollection>().Result;
                if (result == null)
                    result = new DetailedResponse<SynonymCollection>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Create entity value synonym.
        ///
        /// Add a new synonym to an entity value.
        ///
        /// If you want to create multiple synonyms with a single API call, consider using the **[Update
        /// entity](#update-entity)** or **[Update entity value](#update-entity-value)** method instead.
        ///
        /// This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="body">The new synonym.</param>
        /// <returns><see cref="Synonym" />Synonym</returns>
        public DetailedResponse<Synonym> CreateSynonym(string workspaceId, string entity, string value, string synonym)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `CreateSynonym`");
        if (string.IsNullOrEmpty(entity))
            throw new ArgumentNullException("`entity` is required for `CreateSynonym`");
        if (string.IsNullOrEmpty(value))
            throw new ArgumentNullException("`value` is required for `CreateSynonym`");
        if (string.IsNullOrEmpty(synonym))
            throw new ArgumentNullException("`synonym` is required for `CreateSynonym`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Synonym> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(synonym))
                    bodyObject["synonym"] = synonym;
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "CreateSynonym"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Synonym>().Result;
                if (result == null)
                    result = new DetailedResponse<Synonym>();
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
        ///
        /// This operation is limited to 6000 requests per 5 minutes. For more information, see **Rate limiting**.
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
            throw new ArgumentNullException("`workspaceId` is required for `GetSynonym`");
        if (string.IsNullOrEmpty(entity))
            throw new ArgumentNullException("`entity` is required for `GetSynonym`");
        if (string.IsNullOrEmpty(value))
            throw new ArgumentNullException("`value` is required for `GetSynonym`");
        if (string.IsNullOrEmpty(synonym))
            throw new ArgumentNullException("`synonym` is required for `GetSynonym`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Synonym> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms/{synonym}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (includeAudit != null)
                    restRequest.WithArgument("include_audit", includeAudit);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "GetSynonym"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Synonym>().Result;
                if (result == null)
                    result = new DetailedResponse<Synonym>();
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
        ///
        /// This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="synonym">The text of the synonym.</param>
        /// <param name="body">The updated entity value synonym.</param>
        /// <returns><see cref="Synonym" />Synonym</returns>
        public DetailedResponse<Synonym> UpdateSynonym(string workspaceId, string entity, string value, string synonym, string newSynonym = null)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `UpdateSynonym`");
        if (string.IsNullOrEmpty(entity))
            throw new ArgumentNullException("`entity` is required for `UpdateSynonym`");
        if (string.IsNullOrEmpty(value))
            throw new ArgumentNullException("`value` is required for `UpdateSynonym`");
        if (string.IsNullOrEmpty(synonym))
            throw new ArgumentNullException("`synonym` is required for `UpdateSynonym`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Synonym> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms/{synonym}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(newSynonym))
                    bodyObject["synonym"] = newSynonym;
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "UpdateSynonym"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Synonym>().Result;
                if (result == null)
                    result = new DetailedResponse<Synonym>();
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
        ///
        /// This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="synonym">The text of the synonym.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteSynonym(string workspaceId, string entity, string value, string synonym)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `DeleteSynonym`");
        if (string.IsNullOrEmpty(entity))
            throw new ArgumentNullException("`entity` is required for `DeleteSynonym`");
        if (string.IsNullOrEmpty(value))
            throw new ArgumentNullException("`value` is required for `DeleteSynonym`");
        if (string.IsNullOrEmpty(synonym))
            throw new ArgumentNullException("`synonym` is required for `DeleteSynonym`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms/{synonym}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "DeleteSynonym"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<object>().Result;
                if (result == null)
                    result = new DetailedResponse<object>();
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
        ///
        /// This operation is limited to 2500 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional,
        /// default to false)</param>
        /// <param name="sort">The attribute by which returned dialog nodes will be sorted. To reverse the sort order,
        /// prefix the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="DialogNodeCollection" />DialogNodeCollection</returns>
        public DetailedResponse<DialogNodeCollection> ListDialogNodes(string workspaceId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `ListDialogNodes`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<DialogNodeCollection> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/dialog_nodes");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (pageLimit != null)
                    restRequest.WithArgument("page_limit", pageLimit);
                if (includeCount != null)
                    restRequest.WithArgument("include_count", includeCount);
                if (!string.IsNullOrEmpty(sort))
                    restRequest.WithArgument("sort", sort);
                if (!string.IsNullOrEmpty(cursor))
                    restRequest.WithArgument("cursor", cursor);
                if (includeAudit != null)
                    restRequest.WithArgument("include_audit", includeAudit);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "ListDialogNodes"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<DialogNodeCollection>().Result;
                if (result == null)
                    result = new DetailedResponse<DialogNodeCollection>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Create dialog node.
        ///
        /// Create a new dialog node.
        ///
        /// If you want to create multiple dialog nodes with a single API call, consider using the **[Update
        /// workspace](#update-workspace)** method instead.
        ///
        /// This operation is limited to 500 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="properties">A CreateDialogNode object defining the content of the new dialog node.</param>
        /// <returns><see cref="DialogNode" />DialogNode</returns>
        public DetailedResponse<DialogNode> CreateDialogNode(string workspaceId, string dialogNode, string description = null, string conditions = null, string parent = null, string previousSibling = null, JObject output = null, Dictionary<string, object> context = null, Dictionary<string, object> metadata = null, DialogNodeNextStep nextStep = null, string title = null, string nodeType = null, string eventName = null, string variable = null, List<DialogNodeAction> actions = null, string digressIn = null, string digressOut = null, string digressOutSlots = null, string userLabel = null)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `CreateDialogNode`");
        if (string.IsNullOrEmpty(dialogNode))
            throw new ArgumentNullException("`dialogNode` is required for `CreateDialogNode`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<DialogNode> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/dialog_nodes");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(dialogNode))
                    bodyObject["dialog_node"] = dialogNode;
                if (!string.IsNullOrEmpty(description))
                    bodyObject["description"] = description;
                if (!string.IsNullOrEmpty(conditions))
                    bodyObject["conditions"] = conditions;
                if (!string.IsNullOrEmpty(parent))
                    bodyObject["parent"] = parent;
                if (!string.IsNullOrEmpty(previousSibling))
                    bodyObject["previous_sibling"] = previousSibling;
                if (output != null)
                    bodyObject["output"] = JToken.FromObject(output);
                if (context != null)
                    bodyObject["context"] = JToken.FromObject(context);
                if (metadata != null)
                    bodyObject["metadata"] = JToken.FromObject(metadata);
                if (nextStep != null)
                    bodyObject["next_step"] = JToken.FromObject(nextStep);
                if (!string.IsNullOrEmpty(title))
                    bodyObject["title"] = title;
                if (!string.IsNullOrEmpty(nodeType))
                    bodyObject["type"] = nodeType;
                if (!string.IsNullOrEmpty(eventName))
                    bodyObject["event_name"] = eventName;
                if (!string.IsNullOrEmpty(variable))
                    bodyObject["variable"] = variable;
                if (actions != null && actions.Count > 0)
                    bodyObject["actions"] = JToken.FromObject(actions);
                if (!string.IsNullOrEmpty(digressIn))
                    bodyObject["digress_in"] = digressIn;
                if (!string.IsNullOrEmpty(digressOut))
                    bodyObject["digress_out"] = digressOut;
                if (!string.IsNullOrEmpty(digressOutSlots))
                    bodyObject["digress_out_slots"] = digressOutSlots;
                if (!string.IsNullOrEmpty(userLabel))
                    bodyObject["user_label"] = userLabel;
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "CreateDialogNode"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<DialogNode>().Result;
                if (result == null)
                    result = new DetailedResponse<DialogNode>();
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
        ///
        /// This operation is limited to 6000 requests per 5 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="dialogNode">The dialog node ID (for example, `get_order`).</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="DialogNode" />DialogNode</returns>
        public DetailedResponse<DialogNode> GetDialogNode(string workspaceId, string dialogNode, bool? includeAudit = null)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `GetDialogNode`");
        if (string.IsNullOrEmpty(dialogNode))
            throw new ArgumentNullException("`dialogNode` is required for `GetDialogNode`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<DialogNode> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/dialog_nodes/{dialogNode}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (includeAudit != null)
                    restRequest.WithArgument("include_audit", includeAudit);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "GetDialogNode"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<DialogNode>().Result;
                if (result == null)
                    result = new DetailedResponse<DialogNode>();
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
        ///
        /// This operation is limited to 500 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="dialogNode">The dialog node ID (for example, `get_order`).</param>
        /// <param name="properties">The updated content of the dialog node.
        ///
        /// Any elements included in the new data will completely replace the equivalent existing elements, including
        /// all subelements. (Previously existing subelements are not retained unless they are also included in the new
        /// data.) For example, if you update the actions for a dialog node, the previously existing actions are
        /// discarded and replaced with the new actions specified in the update.</param>
        /// <returns><see cref="DialogNode" />DialogNode</returns>
        public DetailedResponse<DialogNode> UpdateDialogNode(string workspaceId, string dialogNode, string newDialogNode = null, string newDescription = null, string newConditions = null, string newParent = null, string newPreviousSibling = null, JObject newOutput = null, Dictionary<string, object> newContext = null, Dictionary<string, object> newMetadata = null, DialogNodeNextStep newNextStep = null, string newTitle = null, string newNodeType = null, string newEventName = null, string newVariable = null, List<DialogNodeAction> newActions = null, string newDigressIn = null, string newDigressOut = null, string newDigressOutSlots = null, string newUserLabel = null)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `UpdateDialogNode`");
        if (string.IsNullOrEmpty(dialogNode))
            throw new ArgumentNullException("`dialogNode` is required for `UpdateDialogNode`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<DialogNode> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/dialog_nodes/{dialogNode}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(newDialogNode))
                    bodyObject["dialog_node"] = newDialogNode;
                if (!string.IsNullOrEmpty(newDescription))
                    bodyObject["description"] = newDescription;
                if (!string.IsNullOrEmpty(newConditions))
                    bodyObject["conditions"] = newConditions;
                if (!string.IsNullOrEmpty(newParent))
                    bodyObject["parent"] = newParent;
                if (!string.IsNullOrEmpty(newPreviousSibling))
                    bodyObject["previous_sibling"] = newPreviousSibling;
                if (newOutput != null)
                    bodyObject["output"] = JToken.FromObject(newOutput);
                if (newContext != null)
                    bodyObject["context"] = JToken.FromObject(newContext);
                if (newMetadata != null)
                    bodyObject["metadata"] = JToken.FromObject(newMetadata);
                if (newNextStep != null)
                    bodyObject["next_step"] = JToken.FromObject(newNextStep);
                if (!string.IsNullOrEmpty(newTitle))
                    bodyObject["title"] = newTitle;
                if (!string.IsNullOrEmpty(newNodeType))
                    bodyObject["type"] = newNodeType;
                if (!string.IsNullOrEmpty(newEventName))
                    bodyObject["event_name"] = newEventName;
                if (!string.IsNullOrEmpty(newVariable))
                    bodyObject["variable"] = newVariable;
                if (newActions != null && newActions.Count > 0)
                    bodyObject["actions"] = JToken.FromObject(newActions);
                if (!string.IsNullOrEmpty(newDigressIn))
                    bodyObject["digress_in"] = newDigressIn;
                if (!string.IsNullOrEmpty(newDigressOut))
                    bodyObject["digress_out"] = newDigressOut;
                if (!string.IsNullOrEmpty(newDigressOutSlots))
                    bodyObject["digress_out_slots"] = newDigressOutSlots;
                if (!string.IsNullOrEmpty(newUserLabel))
                    bodyObject["user_label"] = newUserLabel;
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "UpdateDialogNode"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<DialogNode>().Result;
                if (result == null)
                    result = new DetailedResponse<DialogNode>();
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
        ///
        /// This operation is limited to 500 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="dialogNode">The dialog node ID (for example, `get_order`).</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteDialogNode(string workspaceId, string dialogNode)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `DeleteDialogNode`");
        if (string.IsNullOrEmpty(dialogNode))
            throw new ArgumentNullException("`dialogNode` is required for `DeleteDialogNode`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/dialog_nodes/{dialogNode}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "DeleteDialogNode"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<object>().Result;
                if (result == null)
                    result = new DetailedResponse<object>();
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
        /// If **cursor** is not specified, this operation is limited to 40 requests per 30 minutes. If **cursor** is
        /// specified, the limit is 120 requests per minute. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="sort">How to sort the returned log events. You can sort by **request_timestamp**. To reverse
        /// the sort order, prefix the parameter value with a minus sign (`-`). (optional, default to
        /// request_timestamp)</param>
        /// <param name="filter">A cacheable parameter that limits the results to those matching the specified filter.
        /// For more information, see the
        /// [documentation](https://cloud.ibm.com/docs/services/assistant?topic=assistant-filter-reference#filter-reference).
        /// (optional)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <returns><see cref="LogCollection" />LogCollection</returns>
        public DetailedResponse<LogCollection> ListLogs(string workspaceId, string sort = null, string filter = null, long? pageLimit = null, string cursor = null)
        {
        if (string.IsNullOrEmpty(workspaceId))
            throw new ArgumentNullException("`workspaceId` is required for `ListLogs`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<LogCollection> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/logs");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(sort))
                    restRequest.WithArgument("sort", sort);
                if (!string.IsNullOrEmpty(filter))
                    restRequest.WithArgument("filter", filter);
                if (pageLimit != null)
                    restRequest.WithArgument("page_limit", pageLimit);
                if (!string.IsNullOrEmpty(cursor))
                    restRequest.WithArgument("cursor", cursor);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "ListLogs"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<LogCollection>().Result;
                if (result == null)
                    result = new DetailedResponse<LogCollection>();
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
        ///
        /// If **cursor** is not specified, this operation is limited to 40 requests per 30 minutes. If **cursor** is
        /// specified, the limit is 120 requests per minute. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="filter">A cacheable parameter that limits the results to those matching the specified filter.
        /// You must specify a filter query that includes a value for `language`, as well as a value for `workspace_id`
        /// or `request.context.metadata.deployment`. For more information, see the
        /// [documentation](https://cloud.ibm.com/docs/services/assistant?topic=assistant-filter-reference#filter-reference).</param>
        /// <param name="sort">How to sort the returned log events. You can sort by **request_timestamp**. To reverse
        /// the sort order, prefix the parameter value with a minus sign (`-`). (optional, default to
        /// request_timestamp)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <returns><see cref="LogCollection" />LogCollection</returns>
        public DetailedResponse<LogCollection> ListAllLogs(string filter, string sort = null, long? pageLimit = null, string cursor = null)
        {
        if (string.IsNullOrEmpty(filter))
            throw new ArgumentNullException("`filter` is required for `ListAllLogs`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<LogCollection> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/logs");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(filter))
                    restRequest.WithArgument("filter", filter);
                if (!string.IsNullOrEmpty(sort))
                    restRequest.WithArgument("sort", sort);
                if (pageLimit != null)
                    restRequest.WithArgument("page_limit", pageLimit);
                if (!string.IsNullOrEmpty(cursor))
                    restRequest.WithArgument("cursor", cursor);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "ListAllLogs"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<LogCollection>().Result;
                if (result == null)
                    result = new DetailedResponse<LogCollection>();
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
        /// security](https://cloud.ibm.com/docs/services/assistant?topic=assistant-information-security#information-security).
        /// </summary>
        /// <param name="customerId">The customer ID for which all data is to be deleted.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteUserData(string customerId)
        {
        if (string.IsNullOrEmpty(customerId))
            throw new ArgumentNullException("`customerId` is required for `DeleteUserData`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/user_data");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(customerId))
                    restRequest.WithArgument("customer_id", customerId);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("conversation", "v1", "DeleteUserData"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<object>().Result;
                if (result == null)
                    result = new DetailedResponse<object>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
