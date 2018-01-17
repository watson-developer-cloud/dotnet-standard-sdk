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

        /** The Constant CONVERSATION_VERSION_DATE_2017_05_26. */
        public static string CONVERSATION_VERSION_DATE_2017_05_26 = "2017-05-26";

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
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            VersionDate = versionDate;
        }

        public ConversationService(IClient httpClient) : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        public Workspace CreateWorkspace(CreateWorkspace properties = null)
        {

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            Workspace result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces")
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateWorkspace>(properties)
                                .As<Workspace>()
                                .Result;
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
                throw new ArgumentNullException("'workspaceId' is required for 'DeleteWorkspace()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            object result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}")
                                .WithArgument("version", VersionDate)
                                .As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public WorkspaceExport GetWorkspace(string workspaceId, bool? export = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("'workspaceId' is required for 'GetWorkspace()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            WorkspaceExport result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}")
                                .WithArgument("version", VersionDate)
                                .WithArgument("export", export)
                                .As<WorkspaceExport>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public WorkspaceCollection ListWorkspaces(long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null)
        {

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            WorkspaceCollection result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces")
                                .WithArgument("version", VersionDate)
                                .WithArgument("page_limit", pageLimit)
                                .WithArgument("include_count", includeCount)
                                .WithArgument("sort", sort)
                                .WithArgument("cursor", cursor)
                                .As<WorkspaceCollection>()
                                .Result;
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
                throw new ArgumentNullException("'workspaceId' is required for 'UpdateWorkspace()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            Workspace result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}")
                                .WithArgument("version", VersionDate)
                                .WithArgument("append", append)
                                .WithBody<UpdateWorkspace>(properties)
                                .As<Workspace>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public MessageResponse Message(string workspaceId, MessageRequest request = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("'workspaceId' is required for 'Message()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            MessageResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/message")
                                .WithArgument("version", VersionDate)
                                .WithBody<MessageRequest>(request)
                                .As<MessageResponse>()
                                .Result;
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
                throw new ArgumentNullException("'workspaceId' is required for 'CreateIntent()'");
            if (body == null)
                throw new ArgumentNullException("'body' is required for 'CreateIntent()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            Intent result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents")
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateIntent>(body)
                                .As<Intent>()
                                .Result;
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
                throw new ArgumentNullException("'workspaceId' is required for 'DeleteIntent()'");
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException("'intent' is required for 'DeleteIntent()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            object result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}")
                                .WithArgument("version", VersionDate)
                                .As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public IntentExport GetIntent(string workspaceId, string intent, bool? export = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("'workspaceId' is required for 'GetIntent()'");
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException("'intent' is required for 'GetIntent()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            IntentExport result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}")
                                .WithArgument("version", VersionDate)
                                .WithArgument("export", export)
                                .As<IntentExport>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public IntentCollection ListIntents(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("'workspaceId' is required for 'ListIntents()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            IntentCollection result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents")
                                .WithArgument("version", VersionDate)
                                .WithArgument("export", export)
                                .WithArgument("page_limit", pageLimit)
                                .WithArgument("include_count", includeCount)
                                .WithArgument("sort", sort)
                                .WithArgument("cursor", cursor)
                                .As<IntentCollection>()
                                .Result;
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
                throw new ArgumentNullException("'workspaceId' is required for 'UpdateIntent()'");
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException("'intent' is required for 'UpdateIntent()'");
            if (body == null)
                throw new ArgumentNullException("'body' is required for 'UpdateIntent()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            Intent result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}")
                                .WithArgument("version", VersionDate)
                                .WithBody<UpdateIntent>(body)
                                .As<Intent>()
                                .Result;
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
                throw new ArgumentNullException("'workspaceId' is required for 'CreateExample()'");
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException("'intent' is required for 'CreateExample()'");
            if (body == null)
                throw new ArgumentNullException("'body' is required for 'CreateExample()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            Example result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples")
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateExample>(body)
                                .As<Example>()
                                .Result;
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
                throw new ArgumentNullException("'workspaceId' is required for 'DeleteExample()'");
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException("'intent' is required for 'DeleteExample()'");
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("'text' is required for 'DeleteExample()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            object result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples/{text}")
                                .WithArgument("version", VersionDate)
                                .As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Example GetExample(string workspaceId, string intent, string text)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("'workspaceId' is required for 'GetExample()'");
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException("'intent' is required for 'GetExample()'");
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("'text' is required for 'GetExample()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            Example result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples/{text}")
                                .WithArgument("version", VersionDate)
                                .As<Example>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public ExampleCollection ListExamples(string workspaceId, string intent, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("'workspaceId' is required for 'ListExamples()'");
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException("'intent' is required for 'ListExamples()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ExampleCollection result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples")
                                .WithArgument("version", VersionDate)
                                .WithArgument("page_limit", pageLimit)
                                .WithArgument("include_count", includeCount)
                                .WithArgument("sort", sort)
                                .WithArgument("cursor", cursor)
                                .As<ExampleCollection>()
                                .Result;
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
                throw new ArgumentNullException("'workspaceId' is required for 'UpdateExample()'");
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException("'intent' is required for 'UpdateExample()'");
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("'text' is required for 'UpdateExample()'");
            if (body == null)
                throw new ArgumentNullException("'body' is required for 'UpdateExample()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            Example result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples/{text}")
                                .WithArgument("version", VersionDate)
                                .WithBody<UpdateExample>(body)
                                .As<Example>()
                                .Result;
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
                throw new ArgumentNullException("'workspaceId' is required for 'CreateEntity()'");
            if (properties == null)
                throw new ArgumentNullException("'properties' is required for 'CreateEntity()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            Entity result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities")
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateEntity>(properties)
                                .As<Entity>()
                                .Result;
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
                throw new ArgumentNullException("'workspaceId' is required for 'DeleteEntity()'");
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException("'entity' is required for 'DeleteEntity()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            object result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}")
                                .WithArgument("version", VersionDate)
                                .As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public EntityExport GetEntity(string workspaceId, string entity, bool? export = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("'workspaceId' is required for 'GetEntity()'");
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException("'entity' is required for 'GetEntity()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            EntityExport result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}")
                                .WithArgument("version", VersionDate)
                                .WithArgument("export", export)
                                .As<EntityExport>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public EntityCollection ListEntities(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("'workspaceId' is required for 'ListEntities()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            EntityCollection result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities")
                                .WithArgument("version", VersionDate)
                                .WithArgument("export", export)
                                .WithArgument("page_limit", pageLimit)
                                .WithArgument("include_count", includeCount)
                                .WithArgument("sort", sort)
                                .WithArgument("cursor", cursor)
                                .As<EntityCollection>()
                                .Result;
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
                throw new ArgumentNullException("'workspaceId' is required for 'UpdateEntity()'");
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException("'entity' is required for 'UpdateEntity()'");
            if (properties == null)
                throw new ArgumentNullException("'properties' is required for 'UpdateEntity()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            Entity result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}")
                                .WithArgument("version", VersionDate)
                                .WithBody<UpdateEntity>(properties)
                                .As<Entity>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public Value CreateValue(string workspaceId, string entity, CreateValue body)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("'workspaceId' is required for 'CreateValue()'");
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException("'entity' is required for 'CreateValue()'");
            if (body == null)
                throw new ArgumentNullException("'body' is required for 'CreateValue()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            Value result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values")
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateValue>(body)
                                .As<Value>()
                                .Result;
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
                throw new ArgumentNullException("'workspaceId' is required for 'DeleteValue()'");
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException("'entity' is required for 'DeleteValue()'");
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("'value' is required for 'DeleteValue()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            object result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}")
                                .WithArgument("version", VersionDate)
                                .As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public ValueExport GetValue(string workspaceId, string entity, string value, bool? export = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("'workspaceId' is required for 'GetValue()'");
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException("'entity' is required for 'GetValue()'");
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("'value' is required for 'GetValue()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ValueExport result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}")
                                .WithArgument("version", VersionDate)
                                .WithArgument("export", export)
                                .As<ValueExport>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public ValueCollection ListValues(string workspaceId, string entity, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("'workspaceId' is required for 'ListValues()'");
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException("'entity' is required for 'ListValues()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ValueCollection result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values")
                                .WithArgument("version", VersionDate)
                                .WithArgument("export", export)
                                .WithArgument("page_limit", pageLimit)
                                .WithArgument("include_count", includeCount)
                                .WithArgument("sort", sort)
                                .WithArgument("cursor", cursor)
                                .As<ValueCollection>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Value UpdateValue(string workspaceId, string entity, string value, UpdateValue body)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("'workspaceId' is required for 'UpdateValue()'");
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException("'entity' is required for 'UpdateValue()'");
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("'value' is required for 'UpdateValue()'");
            if (body == null)
                throw new ArgumentNullException("'body' is required for 'UpdateValue()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            Value result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}")
                                .WithArgument("version", VersionDate)
                                .WithBody<UpdateValue>(body)
                                .As<Value>()
                                .Result;
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
                throw new ArgumentNullException("'workspaceId' is required for 'CreateSynonym()'");
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException("'entity' is required for 'CreateSynonym()'");
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("'value' is required for 'CreateSynonym()'");
            if (body == null)
                throw new ArgumentNullException("'body' is required for 'CreateSynonym()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            Synonym result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms")
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateSynonym>(body)
                                .As<Synonym>()
                                .Result;
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
                throw new ArgumentNullException("'workspaceId' is required for 'DeleteSynonym()'");
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException("'entity' is required for 'DeleteSynonym()'");
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("'value' is required for 'DeleteSynonym()'");
            if (string.IsNullOrEmpty(synonym))
                throw new ArgumentNullException("'synonym' is required for 'DeleteSynonym()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            object result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms/{synonym}")
                                .WithArgument("version", VersionDate)
                                .As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Synonym GetSynonym(string workspaceId, string entity, string value, string synonym)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("'workspaceId' is required for 'GetSynonym()'");
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException("'entity' is required for 'GetSynonym()'");
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("'value' is required for 'GetSynonym()'");
            if (string.IsNullOrEmpty(synonym))
                throw new ArgumentNullException("'synonym' is required for 'GetSynonym()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            Synonym result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms/{synonym}")
                                .WithArgument("version", VersionDate)
                                .As<Synonym>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public SynonymCollection ListSynonyms(string workspaceId, string entity, string value, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("'workspaceId' is required for 'ListSynonyms()'");
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException("'entity' is required for 'ListSynonyms()'");
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("'value' is required for 'ListSynonyms()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            SynonymCollection result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms")
                                .WithArgument("version", VersionDate)
                                .WithArgument("page_limit", pageLimit)
                                .WithArgument("include_count", includeCount)
                                .WithArgument("sort", sort)
                                .WithArgument("cursor", cursor)
                                .As<SynonymCollection>()
                                .Result;
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
                throw new ArgumentNullException("'workspaceId' is required for 'UpdateSynonym()'");
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException("'entity' is required for 'UpdateSynonym()'");
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("'value' is required for 'UpdateSynonym()'");
            if (string.IsNullOrEmpty(synonym))
                throw new ArgumentNullException("'synonym' is required for 'UpdateSynonym()'");
            if (body == null)
                throw new ArgumentNullException("'body' is required for 'UpdateSynonym()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            Synonym result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms/{synonym}")
                                .WithArgument("version", VersionDate)
                                .WithBody<UpdateSynonym>(body)
                                .As<Synonym>()
                                .Result;
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
                throw new ArgumentNullException("'workspaceId' is required for 'CreateDialogNode()'");
            if (properties == null)
                throw new ArgumentNullException("'properties' is required for 'CreateDialogNode()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            DialogNode result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/dialog_nodes")
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateDialogNode>(properties)
                                .As<DialogNode>()
                                .Result;
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
                throw new ArgumentNullException("'workspaceId' is required for 'DeleteDialogNode()'");
            if (string.IsNullOrEmpty(dialogNode))
                throw new ArgumentNullException("'dialogNode' is required for 'DeleteDialogNode()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            object result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/dialog_nodes/{dialogNode}")
                                .WithArgument("version", VersionDate)
                                .As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public DialogNode GetDialogNode(string workspaceId, string dialogNode)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("'workspaceId' is required for 'GetDialogNode()'");
            if (string.IsNullOrEmpty(dialogNode))
                throw new ArgumentNullException("'dialogNode' is required for 'GetDialogNode()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            DialogNode result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/dialog_nodes/{dialogNode}")
                                .WithArgument("version", VersionDate)
                                .As<DialogNode>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public DialogNodeCollection ListDialogNodes(string workspaceId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("'workspaceId' is required for 'ListDialogNodes()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            DialogNodeCollection result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/dialog_nodes")
                                .WithArgument("version", VersionDate)
                                .WithArgument("page_limit", pageLimit)
                                .WithArgument("include_count", includeCount)
                                .WithArgument("sort", sort)
                                .WithArgument("cursor", cursor)
                                .As<DialogNodeCollection>()
                                .Result;
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
                throw new ArgumentNullException("'workspaceId' is required for 'UpdateDialogNode()'");
            if (string.IsNullOrEmpty(dialogNode))
                throw new ArgumentNullException("'dialogNode' is required for 'UpdateDialogNode()'");
            if (properties == null)
                throw new ArgumentNullException("'properties' is required for 'UpdateDialogNode()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            DialogNode result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/dialog_nodes/{dialogNode}")
                                .WithArgument("version", VersionDate)
                                .WithBody<UpdateDialogNode>(properties)
                                .As<DialogNode>()
                                .Result;
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
                throw new ArgumentNullException("'filter' is required for 'ListAllLogs()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            LogCollection result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/logs")
                                .WithArgument("version", VersionDate)
                                .WithArgument("filter", filter)
                                .WithArgument("sort", sort)
                                .WithArgument("page_limit", pageLimit)
                                .WithArgument("cursor", cursor)
                                .As<LogCollection>()
                                .Result;
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
                throw new ArgumentNullException("'workspaceId' is required for 'ListLogs()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            LogCollection result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/logs")
                                .WithArgument("version", VersionDate)
                                .WithArgument("sort", sort)
                                .WithArgument("filter", filter)
                                .WithArgument("page_limit", pageLimit)
                                .WithArgument("cursor", cursor)
                                .As<LogCollection>()
                                .Result;
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
                throw new ArgumentNullException("'workspaceId' is required for 'CreateCounterexample()'");
            if (body == null)
                throw new ArgumentNullException("'body' is required for 'CreateCounterexample()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            Counterexample result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples")
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateCounterexample>(body)
                                .As<Counterexample>()
                                .Result;
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
                throw new ArgumentNullException("'workspaceId' is required for 'DeleteCounterexample()'");
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("'text' is required for 'DeleteCounterexample()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            object result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples/{text}")
                                .WithArgument("version", VersionDate)
                                .As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Counterexample GetCounterexample(string workspaceId, string text)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("'workspaceId' is required for 'GetCounterexample()'");
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("'text' is required for 'GetCounterexample()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            Counterexample result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples/{text}")
                                .WithArgument("version", VersionDate)
                                .As<Counterexample>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public CounterexampleCollection ListCounterexamples(string workspaceId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("'workspaceId' is required for 'ListCounterexamples()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            CounterexampleCollection result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples")
                                .WithArgument("version", VersionDate)
                                .WithArgument("page_limit", pageLimit)
                                .WithArgument("include_count", includeCount)
                                .WithArgument("sort", sort)
                                .WithArgument("cursor", cursor)
                                .As<CounterexampleCollection>()
                                .Result;
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
                throw new ArgumentNullException("'workspaceId' is required for 'UpdateCounterexample()'");
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("'text' is required for 'UpdateCounterexample()'");
            if (body == null)
                throw new ArgumentNullException("'body' is required for 'UpdateCounterexample()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            Counterexample result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples/{text}")
                                .WithArgument("version", VersionDate)
                                .WithBody<UpdateCounterexample>(body)
                                .As<Counterexample>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
