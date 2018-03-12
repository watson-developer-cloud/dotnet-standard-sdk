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
using IBM.WatsonDeveloperCloud.Assistant.v1.Model;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Service;
using Newtonsoft.Json;
using System;

namespace IBM.WatsonDeveloperCloud.Assistant.v1
{
    public class AssistantService : WatsonService, IAssistantService
    {
        const string SERVICE_NAME = "assistant";
        const string URL = "https://gateway.watsonplatform.net/assistant/api";
        private string _versionDate;
        public string VersionDate
        {
            get { return _versionDate; }
            set { _versionDate = value; }
        }

        public AssistantService() : base(SERVICE_NAME, URL)
        {
            if(!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }


        public AssistantService(string userName, string password, string versionDate) : this()
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

        public AssistantService(IClient httpClient) : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

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
                                .DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/dialogNodes/{dialogNode}");
                request.WithArgument("version", VersionDate);
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

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
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/dialogNodes/{dialogNode}");
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
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/dialogNodes/{dialogNode}");
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
    }
}
