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

        public ExampleResponse CreateCounterexample(string workspaceId, CreateExample body)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ExampleResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples")
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateExample>(body)
                                .As<ExampleResponse>()
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
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

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

        public ExampleResponse GetCounterexample(string workspaceId, string text)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ExampleResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples/{text}")
                                .WithArgument("version", VersionDate)
                                .As<ExampleResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public CounterexampleCollectionResponse ListCounterexamples(string workspaceId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            CounterexampleCollectionResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples")
                                .WithArgument("version", VersionDate)
                                .WithArgument("page_limit", pageLimit)
                                .WithArgument("include_count", includeCount)
                                .WithArgument("sort", sort)
                                .WithArgument("cursor", cursor)
                                .As<CounterexampleCollectionResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public ExampleResponse UpdateCounterexample(string workspaceId, string text, UpdateExample body)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ExampleResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples/{text}")
                                .WithArgument("version", VersionDate)
                                .WithBody<UpdateExample>(body)
                                .As<ExampleResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public EntityResponse CreateEntity(string workspaceId, CreateEntity body)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            EntityResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities")
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateEntity>(body)
                                .As<EntityResponse>()
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
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));

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

        public EntityExportResponse GetEntity(string workspaceId, string entity, bool? export = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            EntityExportResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}")
                                .WithArgument("version", VersionDate)
                                .WithArgument("export", export)
                                .As<EntityExportResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public EntityCollectionResponse ListEntities(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            EntityCollectionResponse result = null;

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
                                .As<EntityCollectionResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public EntityResponse UpdateEntity(string workspaceId, string entity, UpdateEntity body)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            EntityResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}")
                                .WithArgument("version", VersionDate)
                                .WithBody<UpdateEntity>(body)
                                .As<EntityResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public ExampleResponse CreateExample(string workspaceId, string intent, CreateExample body)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException(nameof(intent));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ExampleResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples")
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateExample>(body)
                                .As<ExampleResponse>()
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
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException(nameof(intent));
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

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

        public ExampleResponse GetExample(string workspaceId, string intent, string text)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException(nameof(intent));
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ExampleResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples/{text}")
                                .WithArgument("version", VersionDate)
                                .As<ExampleResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public ExampleCollectionResponse ListExamples(string workspaceId, string intent, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException(nameof(intent));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ExampleCollectionResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples")
                                .WithArgument("version", VersionDate)
                                .WithArgument("page_limit", pageLimit)
                                .WithArgument("include_count", includeCount)
                                .WithArgument("sort", sort)
                                .WithArgument("cursor", cursor)
                                .As<ExampleCollectionResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public ExampleResponse UpdateExample(string workspaceId, string intent, string text, UpdateExample body)
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
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ExampleResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples/{text}")
                                .WithArgument("version", VersionDate)
                                .WithBody<UpdateExample>(body)
                                .As<ExampleResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public IntentResponse CreateIntent(string workspaceId, CreateIntent body)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            IntentResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents")
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateIntent>(body)
                                .As<IntentResponse>()
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
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException(nameof(intent));

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

        public IntentExportResponse GetIntent(string workspaceId, string intent, bool? export = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException(nameof(intent));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            IntentExportResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}")
                                .WithArgument("version", VersionDate)
                                .WithArgument("export", export)
                                .As<IntentExportResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public IntentCollectionResponse ListIntents(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            IntentCollectionResponse result = null;

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
                                .As<IntentCollectionResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public IntentResponse UpdateIntent(string workspaceId, string intent, UpdateIntent body)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException(nameof(intent));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            IntentResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}")
                                .WithArgument("version", VersionDate)
                                .WithBody<UpdateIntent>(body)
                                .As<IntentResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public LogCollectionResponse ListLogs(string workspaceId, string sort = null, string filter = null, long? pageLimit = null, string cursor = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            LogCollectionResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/logs")
                                .WithArgument("version", VersionDate)
                                .WithArgument("sort", sort)
                                .WithArgument("filter", filter)
                                .WithArgument("page_limit", pageLimit)
                                .WithArgument("cursor", cursor)
                                .As<LogCollectionResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public MessageResponse Message(string workspaceId, MessageRequest body = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            MessageResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/message")
                                .WithArgument("version", VersionDate)
                                .WithBody<MessageRequest>(body)
                                .As<MessageResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public SynonymResponse CreateSynonym(string workspaceId, string entity, string value, CreateSynonym body)
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
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            SynonymResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms")
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateSynonym>(body)
                                .As<SynonymResponse>()
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
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
            if (string.IsNullOrEmpty(synonym))
                throw new ArgumentNullException(nameof(synonym));

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

        public SynonymResponse GetSynonym(string workspaceId, string entity, string value, string synonym)
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
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            SynonymResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms/{synonym}")
                                .WithArgument("version", VersionDate)
                                .As<SynonymResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public SynonymCollectionResponse ListSynonyms(string workspaceId, string entity, string value, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            SynonymCollectionResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms")
                                .WithArgument("version", VersionDate)
                                .WithArgument("page_limit", pageLimit)
                                .WithArgument("include_count", includeCount)
                                .WithArgument("sort", sort)
                                .WithArgument("cursor", cursor)
                                .As<SynonymCollectionResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public SynonymResponse UpdateSynonym(string workspaceId, string entity, string value, string synonym, UpdateSynonym body)
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
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            SynonymResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms/{synonym}")
                                .WithArgument("version", VersionDate)
                                .WithBody<UpdateSynonym>(body)
                                .As<SynonymResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public ValueResponse CreateValue(string workspaceId, string entity, CreateValue body)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ValueResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values")
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateValue>(body)
                                .As<ValueResponse>()
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
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

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

        public ValueExportResponse GetValue(string workspaceId, string entity, string value, bool? export = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ValueExportResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}")
                                .WithArgument("version", VersionDate)
                                .WithArgument("export", export)
                                .As<ValueExportResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public ValueCollectionResponse ListValues(string workspaceId, string entity, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ValueCollectionResponse result = null;

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
                                .As<ValueCollectionResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public ValueResponse UpdateValue(string workspaceId, string entity, string value, UpdateValue body)
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
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ValueResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}")
                                .WithArgument("version", VersionDate)
                                .WithBody<UpdateValue>(body)
                                .As<ValueResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public WorkspaceResponse CreateWorkspace(CreateWorkspace body = null)
        {

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            WorkspaceResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces")
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateWorkspace>(body)
                                .As<WorkspaceResponse>()
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
                throw new ArgumentNullException(nameof(workspaceId));

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

        public WorkspaceExportResponse GetWorkspace(string workspaceId, bool? export = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            WorkspaceExportResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}")
                                .WithArgument("version", VersionDate)
                                .WithArgument("export", export)
                                .As<WorkspaceExportResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public WorkspaceCollectionResponse ListWorkspaces(long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null)
        {

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            WorkspaceCollectionResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/workspaces")
                                .WithArgument("version", VersionDate)
                                .WithArgument("page_limit", pageLimit)
                                .WithArgument("include_count", includeCount)
                                .WithArgument("sort", sort)
                                .WithArgument("cursor", cursor)
                                .As<WorkspaceCollectionResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public WorkspaceResponse UpdateWorkspace(string workspaceId, UpdateWorkspace body = null)
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            WorkspaceResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/workspaces/{workspaceId}")
                                .WithArgument("version", VersionDate)
                                .WithBody<UpdateWorkspace>(body)
                                .As<WorkspaceResponse>()
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
