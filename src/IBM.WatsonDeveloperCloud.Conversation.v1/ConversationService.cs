/**
* Copyright 2018 IBM Corp. All Rights Reserved.
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
using System.Text;
using IBM.WatsonDeveloperCloud.Conversation.v1.Model;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Service;
using Newtonsoft.Json;
using System;

namespace IBM.WatsonDeveloperCloud.Conversation.v1
{
    public class ConversationService : WatsonService, IConversationService
    {
        const string SERVICE_NAME = "conversation";
        const string URL = "https://gateway.watsonplatform.net/conversation/api";
        private string _versionDate;
        public string VersionDate
        {
            get { return _versionDate; }
            set { _versionDate = value; }
        }

        public ConversationService() : base(SERVICE_NAME, URL)
        {
            if(!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }


        public ConversationService(string userName, string password, string versionDate) : this()
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);

            if(string.IsNullOrEmpty(versionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            VersionDate = versionDate;
        }

        public ConversationService(IClient httpClient) : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        /// <summary>
        /// Create workspace. Create a workspace based on component objects. You must provide workspace components defining the content of the new workspace.
        /// </summary>
        /// <param name="properties">Valid data defining the content of the new workspace. (optional)</param>
        /// <returns><see cref="Workspace" />Workspace</returns>
        public Workspace CreateWorkspace(CreateWorkspace properties = null)
        {

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Workspace result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces");
                request.WithArgument("version", VersionDate);
                request.WithBody<CreateWorkspace>(properties);
                result = request.As<Workspace>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete workspace. Delete a workspace from the service instance.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <returns><see cref="object" />object</returns>
        public object DeleteWorkspace(string workspaceId)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}");
                request.WithArgument("version", VersionDate);
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get information about a workspace. Get information about a workspace, optionally including all workspace content.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <returns><see cref="WorkspaceExport" />WorkspaceExport</returns>
        public WorkspaceExport GetWorkspace(string workspaceId, bool? export = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            WorkspaceExport result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}");
                request.WithArgument("version", VersionDate);
                if (export != null)
                    request.WithArgument("export", export);
                if (includeAudit != null)
                    request.WithArgument("include_audit", includeAudit);
                result = request.As<WorkspaceExport>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List workspaces. List the workspaces associated with a Conversation service instance.
        /// </summary>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <returns><see cref="WorkspaceCollection" />WorkspaceCollection</returns>
        public WorkspaceCollection ListWorkspaces(long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            WorkspaceCollection result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces");
                request.WithArgument("version", VersionDate);
                if (pageLimit != null)
                    request.WithArgument("page_limit", pageLimit);
                if (includeCount != null)
                    request.WithArgument("include_count", includeCount);
                if (!string.IsNullOrEmpty(sort))
                    request.WithArgument("sort", sort);
                if (!string.IsNullOrEmpty(cursor))
                    request.WithArgument("cursor", cursor);
                if (includeAudit != null)
                    request.WithArgument("include_audit", includeAudit);
                result = request.As<WorkspaceCollection>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update workspace. Update an existing workspace with new or modified data. You must provide component objects defining the content of the updated workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="properties">Valid data defining the new workspace content. Any elements included in the new data will completely replace the existing elements, including all subelements. Previously existing subelements are not retained unless they are included in the new data. (optional)</param>
        /// <param name="append">Specifies that the elements included in the request body are to be appended to the existing data in the workspace. The default value is `false`. (optional, default to false)</param>
        /// <returns><see cref="Workspace" />Workspace</returns>
        public Workspace UpdateWorkspace(string workspaceId, UpdateWorkspace properties = null, bool? append = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Workspace result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}");
                request.WithArgument("version", VersionDate);
                if (append != null)
                    request.WithArgument("append", append);
                request.WithBody<UpdateWorkspace>(properties);
                result = request.As<Workspace>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Get a response to a user's input. 
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="request">The user's input, with optional intents, entities, and other properties from the response. (optional)</param>
        /// <param name="nodesVisitedDetails">Whether to include additional diagnostic information about the dialog nodes that were visited during processing of the message. (optional, default to false)</param>
        /// <returns><see cref="MessageResponse" />MessageResponse</returns>
        public MessageResponse Message(string workspaceId, MessageRequest messageRequest = null, bool? nodesVisitedDetails = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            MessageResponse result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/message");
                request.WithArgument("version", VersionDate);
                if (nodesVisitedDetails != null)
                    request.WithArgument("nodes_visited_details", nodesVisitedDetails);
                request.WithBody<MessageRequest>(messageRequest);
                result = request.As<MessageResponse>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Create intent. Create a new intent.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="body">A CreateIntent object defining the content of the new intent.</param>
        /// <returns><see cref="Intent" />Intent</returns>
        public Intent CreateIntent(string workspaceId, CreateIntent body)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Intent result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents");
                request.WithArgument("version", VersionDate);
                request.WithBody<CreateIntent>(body);
                result = request.As<Intent>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete intent. Delete an intent from a workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <returns><see cref="object" />object</returns>
        public object DeleteIntent(string workspaceId, string intent)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException(nameof(intent));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}");
                request.WithArgument("version", VersionDate);
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get intent. Get information about an intent, optionally including all intent content.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <returns><see cref="IntentExport" />IntentExport</returns>
        public IntentExport GetIntent(string workspaceId, string intent, bool? export = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException(nameof(intent));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            IntentExport result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}");
                request.WithArgument("version", VersionDate);
                if (export != null)
                    request.WithArgument("export", export);
                if (includeAudit != null)
                    request.WithArgument("include_audit", includeAudit);
                result = request.As<IntentExport>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List intents. List the intents for a workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <returns><see cref="IntentCollection" />IntentCollection</returns>
        public IntentCollection ListIntents(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            IntentCollection result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents");
                request.WithArgument("version", VersionDate);
                if (export != null)
                    request.WithArgument("export", export);
                if (pageLimit != null)
                    request.WithArgument("page_limit", pageLimit);
                if (includeCount != null)
                    request.WithArgument("include_count", includeCount);
                if (!string.IsNullOrEmpty(sort))
                    request.WithArgument("sort", sort);
                if (!string.IsNullOrEmpty(cursor))
                    request.WithArgument("cursor", cursor);
                if (includeAudit != null)
                    request.WithArgument("include_audit", includeAudit);
                result = request.As<IntentCollection>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update intent. Update an existing intent with new or modified data. You must provide data defining the content of the updated intent.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="body">An UpdateIntent object defining the updated content of the intent.</param>
        /// <returns><see cref="Intent" />Intent</returns>
        public Intent UpdateIntent(string workspaceId, string intent, UpdateIntent body)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException(nameof(intent));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Intent result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}");
                request.WithArgument("version", VersionDate);
                request.WithBody<UpdateIntent>(body);
                result = request.As<Intent>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Create user input example. Add a new user input example to an intent.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="body">A CreateExample object defining the content of the new user input example.</param>
        /// <returns><see cref="Example" />Example</returns>
        public Example CreateExample(string workspaceId, string intent, CreateExample body)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException(nameof(intent));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Example result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples");
                request.WithArgument("version", VersionDate);
                request.WithBody<CreateExample>(body);
                result = request.As<Example>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete user input example. Delete a user input example from an intent.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="text">The text of the user input example.</param>
        /// <returns><see cref="object" />object</returns>
        public object DeleteExample(string workspaceId, string intent, string text)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException(nameof(intent));
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples/{text}");
                request.WithArgument("version", VersionDate);
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get user input example. Get information about a user input example.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="text">The text of the user input example.</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <returns><see cref="Example" />Example</returns>
        public Example GetExample(string workspaceId, string intent, string text, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException(nameof(intent));
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Example result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples/{text}");
                request.WithArgument("version", VersionDate);
                if (includeAudit != null)
                    request.WithArgument("include_audit", includeAudit);
                result = request.As<Example>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List user input examples. List the user input examples for an intent.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <returns><see cref="ExampleCollection" />ExampleCollection</returns>
        public ExampleCollection ListExamples(string workspaceId, string intent, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException(nameof(intent));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            ExampleCollection result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples");
                request.WithArgument("version", VersionDate);
                if (pageLimit != null)
                    request.WithArgument("page_limit", pageLimit);
                if (includeCount != null)
                    request.WithArgument("include_count", includeCount);
                if (!string.IsNullOrEmpty(sort))
                    request.WithArgument("sort", sort);
                if (!string.IsNullOrEmpty(cursor))
                    request.WithArgument("cursor", cursor);
                if (includeAudit != null)
                    request.WithArgument("include_audit", includeAudit);
                result = request.As<ExampleCollection>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update user input example. Update the text of a user input example.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="text">The text of the user input example.</param>
        /// <param name="body">An UpdateExample object defining the new text for the user input example.</param>
        /// <returns><see cref="Example" />Example</returns>
        public Example UpdateExample(string workspaceId, string intent, string text, UpdateExample body)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException(nameof(intent));
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Example result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples/{text}");
                request.WithArgument("version", VersionDate);
                request.WithBody<UpdateExample>(body);
                result = request.As<Example>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Create entity. Create a new entity.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="properties">A CreateEntity object defining the content of the new entity.</param>
        /// <returns><see cref="Entity" />Entity</returns>
        public Entity CreateEntity(string workspaceId, CreateEntity properties)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Entity result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities");
                request.WithArgument("version", VersionDate);
                request.WithBody<CreateEntity>(properties);
                result = request.As<Entity>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete entity. Delete an entity from a workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <returns><see cref="object" />object</returns>
        public object DeleteEntity(string workspaceId, string entity)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}");
                request.WithArgument("version", VersionDate);
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get entity. Get information about an entity, optionally including all entity content.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <returns><see cref="EntityExport" />EntityExport</returns>
        public EntityExport GetEntity(string workspaceId, string entity, bool? export = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            EntityExport result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}");
                request.WithArgument("version", VersionDate);
                if (export != null)
                    request.WithArgument("export", export);
                if (includeAudit != null)
                    request.WithArgument("include_audit", includeAudit);
                result = request.As<EntityExport>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List entities. List the entities for a workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <returns><see cref="EntityCollection" />EntityCollection</returns>
        public EntityCollection ListEntities(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            EntityCollection result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities");
                request.WithArgument("version", VersionDate);
                if (export != null)
                    request.WithArgument("export", export);
                if (pageLimit != null)
                    request.WithArgument("page_limit", pageLimit);
                if (includeCount != null)
                    request.WithArgument("include_count", includeCount);
                if (!string.IsNullOrEmpty(sort))
                    request.WithArgument("sort", sort);
                if (!string.IsNullOrEmpty(cursor))
                    request.WithArgument("cursor", cursor);
                if (includeAudit != null)
                    request.WithArgument("include_audit", includeAudit);
                result = request.As<EntityCollection>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update entity. Update an existing entity with new or modified data.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="properties">An UpdateEntity object defining the updated content of the entity.</param>
        /// <returns><see cref="Entity" />Entity</returns>
        public Entity UpdateEntity(string workspaceId, string entity, UpdateEntity properties)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Entity result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}");
                request.WithArgument("version", VersionDate);
                request.WithBody<UpdateEntity>(properties);
                result = request.As<Entity>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Add entity value. Create a new value for an entity.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="properties">A CreateValue object defining the content of the new value for the entity.</param>
        /// <returns><see cref="Value" />Value</returns>
        public Value CreateValue(string workspaceId, string entity, CreateValue properties)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Value result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values");
                request.WithArgument("version", VersionDate);
                request.WithBody<CreateValue>(properties);
                result = request.As<Value>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete entity value. Delete a value for an entity.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <returns><see cref="object" />object</returns>
        public object DeleteValue(string workspaceId, string entity, string value)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}");
                request.WithArgument("version", VersionDate);
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get entity value. Get information about an entity value.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <returns><see cref="ValueExport" />ValueExport</returns>
        public ValueExport GetValue(string workspaceId, string entity, string value, bool? export = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            ValueExport result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}");
                request.WithArgument("version", VersionDate);
                if (export != null)
                    request.WithArgument("export", export);
                if (includeAudit != null)
                    request.WithArgument("include_audit", includeAudit);
                result = request.As<ValueExport>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List entity values. List the values for an entity.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <returns><see cref="ValueCollection" />ValueCollection</returns>
        public ValueCollection ListValues(string workspaceId, string entity, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            ValueCollection result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values");
                request.WithArgument("version", VersionDate);
                if (export != null)
                    request.WithArgument("export", export);
                if (pageLimit != null)
                    request.WithArgument("page_limit", pageLimit);
                if (includeCount != null)
                    request.WithArgument("include_count", includeCount);
                if (!string.IsNullOrEmpty(sort))
                    request.WithArgument("sort", sort);
                if (!string.IsNullOrEmpty(cursor))
                    request.WithArgument("cursor", cursor);
                if (includeAudit != null)
                    request.WithArgument("include_audit", includeAudit);
                result = request.As<ValueCollection>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update entity value. Update the content of a value for an entity.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="properties">An UpdateValue object defining the new content for value for the entity.</param>
        /// <returns><see cref="Value" />Value</returns>
        public Value UpdateValue(string workspaceId, string entity, string value, UpdateValue properties)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Value result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}");
                request.WithArgument("version", VersionDate);
                request.WithBody<UpdateValue>(properties);
                result = request.As<Value>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Add entity value synonym. Add a new synonym to an entity value.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="body">A CreateSynonym object defining the new synonym for the entity value.</param>
        /// <returns><see cref="Synonym" />Synonym</returns>
        public Synonym CreateSynonym(string workspaceId, string entity, string value, CreateSynonym body)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Synonym result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms");
                request.WithArgument("version", VersionDate);
                request.WithBody<CreateSynonym>(body);
                result = request.As<Synonym>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete entity value synonym. Delete a synonym for an entity value.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="synonym">The text of the synonym.</param>
        /// <returns><see cref="object" />object</returns>
        public object DeleteSynonym(string workspaceId, string entity, string value, string synonym)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
            if (string.IsNullOrEmpty(synonym))
                throw new ArgumentNullException(nameof(synonym));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms/{synonym}");
                request.WithArgument("version", VersionDate);
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get entity value synonym. Get information about a synonym for an entity value.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="synonym">The text of the synonym.</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <returns><see cref="Synonym" />Synonym</returns>
        public Synonym GetSynonym(string workspaceId, string entity, string value, string synonym, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
            if (string.IsNullOrEmpty(synonym))
                throw new ArgumentNullException(nameof(synonym));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Synonym result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms/{synonym}");
                request.WithArgument("version", VersionDate);
                if (includeAudit != null)
                    request.WithArgument("include_audit", includeAudit);
                result = request.As<Synonym>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List entity value synonyms. List the synonyms for an entity value.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <returns><see cref="SynonymCollection" />SynonymCollection</returns>
        public SynonymCollection ListSynonyms(string workspaceId, string entity, string value, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            SynonymCollection result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms");
                request.WithArgument("version", VersionDate);
                if (pageLimit != null)
                    request.WithArgument("page_limit", pageLimit);
                if (includeCount != null)
                    request.WithArgument("include_count", includeCount);
                if (!string.IsNullOrEmpty(sort))
                    request.WithArgument("sort", sort);
                if (!string.IsNullOrEmpty(cursor))
                    request.WithArgument("cursor", cursor);
                if (includeAudit != null)
                    request.WithArgument("include_audit", includeAudit);
                result = request.As<SynonymCollection>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update entity value synonym. Update the information about a synonym for an entity value.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="synonym">The text of the synonym.</param>
        /// <param name="body">An UpdateSynonym object defining the new information for an entity value synonym.</param>
        /// <returns><see cref="Synonym" />Synonym</returns>
        public Synonym UpdateSynonym(string workspaceId, string entity, string value, string synonym, UpdateSynonym body)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
            if (string.IsNullOrEmpty(synonym))
                throw new ArgumentNullException(nameof(synonym));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Synonym result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms/{synonym}");
                request.WithArgument("version", VersionDate);
                request.WithBody<UpdateSynonym>(body);
                result = request.As<Synonym>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Create dialog node. Create a dialog node.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="properties">A CreateDialogNode object defining the content of the new dialog node.</param>
        /// <returns><see cref="DialogNode" />DialogNode</returns>
        public DialogNode CreateDialogNode(string workspaceId, CreateDialogNode properties)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DialogNode result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/dialog_nodes");
                request.WithArgument("version", VersionDate);
                request.WithBody<CreateDialogNode>(properties);
                result = request.As<DialogNode>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete dialog node. Delete a dialog node from the workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="dialogNode">The dialog node ID (for example, `get_order`).</param>
        /// <returns><see cref="object" />object</returns>
        public object DeleteDialogNode(string workspaceId, string dialogNode)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(dialogNode))
                throw new ArgumentNullException(nameof(dialogNode));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/dialog_nodes/{dialogNode}");
                request.WithArgument("version", VersionDate);
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get dialog node. Get information about a dialog node.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="dialogNode">The dialog node ID (for example, `get_order`).</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <returns><see cref="DialogNode" />DialogNode</returns>
        public DialogNode GetDialogNode(string workspaceId, string dialogNode, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(dialogNode))
                throw new ArgumentNullException(nameof(dialogNode));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DialogNode result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/dialog_nodes/{dialogNode}");
                request.WithArgument("version", VersionDate);
                if (includeAudit != null)
                    request.WithArgument("include_audit", includeAudit);
                result = request.As<DialogNode>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List dialog nodes. List the dialog nodes in the workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <returns><see cref="DialogNodeCollection" />DialogNodeCollection</returns>
        public DialogNodeCollection ListDialogNodes(string workspaceId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DialogNodeCollection result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/dialog_nodes");
                request.WithArgument("version", VersionDate);
                if (pageLimit != null)
                    request.WithArgument("page_limit", pageLimit);
                if (includeCount != null)
                    request.WithArgument("include_count", includeCount);
                if (!string.IsNullOrEmpty(sort))
                    request.WithArgument("sort", sort);
                if (!string.IsNullOrEmpty(cursor))
                    request.WithArgument("cursor", cursor);
                if (includeAudit != null)
                    request.WithArgument("include_audit", includeAudit);
                result = request.As<DialogNodeCollection>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update dialog node. Update information for a dialog node.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="dialogNode">The dialog node ID (for example, `get_order`).</param>
        /// <param name="properties">An UpdateDialogNode object defining the new contents of the dialog node.</param>
        /// <returns><see cref="DialogNode" />DialogNode</returns>
        public DialogNode UpdateDialogNode(string workspaceId, string dialogNode, UpdateDialogNode properties)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(dialogNode))
                throw new ArgumentNullException(nameof(dialogNode));
            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DialogNode result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/dialog_nodes/{dialogNode}");
                request.WithArgument("version", VersionDate);
                request.WithBody<UpdateDialogNode>(properties);
                result = request.As<DialogNode>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// List log events in all workspaces. List log events in all workspaces in the service instance.
        /// </summary>
        /// <param name="filter">A cacheable parameter that limits the results to those matching the specified filter. You must specify a filter query that includes a value for `language`, as well as a value for `workspace_id` or `request.context.metadata.deployment`. For more information, see the [documentation](https://console.bluemix.net/docs/services/conversation/filter-reference.html#filter-query-syntax).</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="LogCollection" />LogCollection</returns>
        public LogCollection ListAllLogs(string filter, string sort = null, long? pageLimit = null, string cursor = null)
        {
            if (string.IsNullOrEmpty(filter))
                throw new ArgumentNullException(nameof(filter));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            LogCollection result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/logs");
                request.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(filter))
                    request.WithArgument("filter", filter);
                if (!string.IsNullOrEmpty(sort))
                    request.WithArgument("sort", sort);
                if (pageLimit != null)
                    request.WithArgument("page_limit", pageLimit);
                if (!string.IsNullOrEmpty(cursor))
                    request.WithArgument("cursor", cursor);
                result = request.As<LogCollection>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List log events in a workspace. List log events in a specific workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="filter">A cacheable parameter that limits the results to those matching the specified filter. For more information, see the [documentation](https://console.bluemix.net/docs/services/conversation/filter-reference.html#filter-query-syntax). (optional)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="LogCollection" />LogCollection</returns>
        public LogCollection ListLogs(string workspaceId, string sort = null, string filter = null, long? pageLimit = null, string cursor = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            LogCollection result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/logs");
                request.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(sort))
                    request.WithArgument("sort", sort);
                if (!string.IsNullOrEmpty(filter))
                    request.WithArgument("filter", filter);
                if (pageLimit != null)
                    request.WithArgument("page_limit", pageLimit);
                if (!string.IsNullOrEmpty(cursor))
                    request.WithArgument("cursor", cursor);
                result = request.As<LogCollection>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Create counterexample. Add a new counterexample to a workspace. Counterexamples are examples that have been marked as irrelevant input.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="body">An object defining the content of the new user input counterexample.</param>
        /// <returns><see cref="Counterexample" />Counterexample</returns>
        public Counterexample CreateCounterexample(string workspaceId, CreateCounterexample body)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Counterexample result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples");
                request.WithArgument("version", VersionDate);
                request.WithBody<CreateCounterexample>(body);
                result = request.As<Counterexample>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete counterexample. Delete a counterexample from a workspace. Counterexamples are examples that have been marked as irrelevant input.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="text">The text of a user input counterexample (for example, `What are you wearing?`).</param>
        /// <returns><see cref="object" />object</returns>
        public object DeleteCounterexample(string workspaceId, string text)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples/{text}");
                request.WithArgument("version", VersionDate);
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get counterexample. Get information about a counterexample. Counterexamples are examples that have been marked as irrelevant input.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="text">The text of a user input counterexample (for example, `What are you wearing?`).</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <returns><see cref="Counterexample" />Counterexample</returns>
        public Counterexample GetCounterexample(string workspaceId, string text, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Counterexample result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples/{text}");
                request.WithArgument("version", VersionDate);
                if (includeAudit != null)
                    request.WithArgument("include_audit", includeAudit);
                result = request.As<Counterexample>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List counterexamples. List the counterexamples for a workspace. Counterexamples are examples that have been marked as irrelevant input.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <returns><see cref="CounterexampleCollection" />CounterexampleCollection</returns>
        public CounterexampleCollection ListCounterexamples(string workspaceId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            CounterexampleCollection result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples");
                request.WithArgument("version", VersionDate);
                if (pageLimit != null)
                    request.WithArgument("page_limit", pageLimit);
                if (includeCount != null)
                    request.WithArgument("include_count", includeCount);
                if (!string.IsNullOrEmpty(sort))
                    request.WithArgument("sort", sort);
                if (!string.IsNullOrEmpty(cursor))
                    request.WithArgument("cursor", cursor);
                if (includeAudit != null)
                    request.WithArgument("include_audit", includeAudit);
                result = request.As<CounterexampleCollection>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update counterexample. Update the text of a counterexample. Counterexamples are examples that have been marked as irrelevant input.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="text">The text of a user input counterexample (for example, `What are you wearing?`).</param>
        /// <param name="body">An object defining the new text for the counterexample.</param>
        /// <returns><see cref="Counterexample" />Counterexample</returns>
        public Counterexample UpdateCounterexample(string workspaceId, string text, UpdateCounterexample body)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Counterexample result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples/{text}");
                request.WithArgument("version", VersionDate);
                request.WithBody<UpdateCounterexample>(body);
                result = request.As<Counterexample>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
