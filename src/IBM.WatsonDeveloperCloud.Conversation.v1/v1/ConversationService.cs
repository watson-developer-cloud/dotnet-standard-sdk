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

using System.Net.Http.Headers;
using IBM.WatsonDeveloperCloud.Conversation.v1.Model;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Service;
using System;
using System.Runtime.ExceptionServices;

namespace IBM.WatsonDeveloperCloud.Conversation.v1
{
    public class ConversationService : WatsonService, IConversationService
    {
        const string PATH_CONVERSATION = "/v1/workspaces";
        const string VERSION_DATE_2016_09_20 = "2016-09-20";
        const string VERSION_DATE_2017_02_03 = "2017-02-03";
        const string SERVICE_NAME = "conversation";
        const string URL = "https://gateway.watsonplatform.net/conversation/api";

        public ConversationService()
            : base(SERVICE_NAME, URL)
        {
            if (!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }

        public ConversationService(string userName, string password)
            : this()
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);
        }

        public ConversationService(IClient httpClient)
            : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        #region Workspaces

        public WorkspaceCollectionResponse ListWorkspaces()
        {
            WorkspaceCollectionResponse result = null;

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .GetAsync($"{this.Endpoint}{PATH_CONVERSATION}")
                               .WithArgument("version", VERSION_DATE_2017_02_03)
                               .WithHeader("accept", HttpMediaType.APPLICATION_JSON)
                               .As<WorkspaceCollectionResponse>()
                               .Result;
            }
            catch (AggregateException ae)
            {

               throw ae.Flatten();
            }

            return result;
        }

        public WorkspaceResponse CreateWorkspace(CreateWorkspace request)
        {
            WorkspaceResponse result = null;

            if (request == null)
                throw new ArgumentNullException("parameter: request");

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .PostAsync($"{this.Endpoint}{PATH_CONVERSATION}")
                               .WithArgument("version", VERSION_DATE_2017_02_03)
                               .WithHeader("accept", HttpMediaType.TEXT_HTML)
                               .WithBody<CreateWorkspace>(request, MediaTypeHeaderValue.Parse(HttpMediaType.APPLICATION_JSON))
                               .As<WorkspaceResponse>()
                               .Result;
            }
            catch (AggregateException ae)
            {

                throw ae.Flatten();
            }

            return result;
        }

        public object DeleteWorkspace(string workspaceId)
        {
            object result = null;

            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("parameter: workspaceId");

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .DeleteAsync($"{this.Endpoint}{PATH_CONVERSATION}/{workspaceId}")
                               .WithArgument("version", VERSION_DATE_2017_02_03)
                               .WithHeader("accept", HttpMediaType.TEXT_HTML)
                               .As<object>()
                               .Result;
            }
            catch (AggregateException ae)
            {

                throw ae.Flatten();
            }

            return result;
        }

        public WorkspaceExportResponse GetWorkspace(string workspaceId)
        {
            return this.GetWorkspace(workspaceId, false);
        }

        public WorkspaceExportResponse GetWorkspace(string workspaceId, bool export = false)
        {
            WorkspaceExportResponse result = null;

            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("parameter: workspaceId");

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .GetAsync($"{this.Endpoint}{PATH_CONVERSATION}/{workspaceId}")
                               .WithArgument("version", VERSION_DATE_2017_02_03)
                               .WithArgument("export", export)
                               .WithHeader("accept", HttpMediaType.APPLICATION_JSON)
                               .As<WorkspaceExportResponse>()
                               .Result;
            }
            catch (AggregateException ae)
            {

                throw ae.Flatten();
            }

            return result;
        }

        public WorkspaceResponse UpdateWorkspace(string workspaceId, UpdateWorkspace request)
        {
            WorkspaceResponse result = null;

            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("parameter: workspaceId");
            if (request == null)
                throw new ArgumentNullException("parameter: request");

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .PostAsync($"{this.Endpoint}{PATH_CONVERSATION}/{workspaceId}")
                               .WithArgument("version", VERSION_DATE_2017_02_03)
                               .WithHeader("accept", HttpMediaType.TEXT_HTML)
                               .WithBody<UpdateWorkspace>(request, MediaTypeHeaderValue.Parse(HttpMediaType.APPLICATION_JSON))
                               .As<WorkspaceResponse>()
                               .Result;
            }
            catch (AggregateException ae)
            {

                throw ae.Flatten();
            }

            return result;
        }

        #endregion

        #region Message

        public MessageResponse Message(string workspaceId, MessageRequest request)
        {
            MessageResponse result = null;

            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("parameter: workspaceId");

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .PostAsync($"{this.Endpoint}{PATH_CONVERSATION}/{workspaceId}/message")
                               .WithArgument("version", VERSION_DATE_2017_02_03)
                               .WithHeader("accept", HttpMediaType.APPLICATION_JSON)
                               .WithBody<MessageRequest>(request, MediaTypeHeaderValue.Parse(HttpMediaType.APPLICATION_JSON))
                               .As<MessageResponse>()
                               .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        #endregion

        #region Intents

        public IntentCollectionResponse ListIntents(string workspaceId)
        {
            return this.ListIntents(workspaceId, false);
        }

        public IntentCollectionResponse ListIntents(string workspaceId, bool export = false)
        {
            IntentCollectionResponse result = null;

            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("parameter: workspaceId");

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .GetAsync($"{this.Endpoint}{PATH_CONVERSATION}/{workspaceId}/intents")
                               .WithArgument("version", VERSION_DATE_2017_02_03)
                               .WithArgument("export", export)
                               .WithHeader("accept", HttpMediaType.APPLICATION_JSON)
                               .As<IntentCollectionResponse>()
                               .Result;
            }
            catch (AggregateException ae)
            {

                throw ae.Flatten();
            }

            return result;
        }

        public IntentResponse CreateIntent(string workspaceId, CreateIntent request)
        {
            IntentResponse result = null;

            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("parameter: workspaceId");
            if (request == null)
                throw new ArgumentNullException("parameter: request");

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .PostAsync($"{this.Endpoint}{PATH_CONVERSATION}/{workspaceId}/intents")
                               .WithArgument("version", VERSION_DATE_2017_02_03)
                               .WithHeader("accept", HttpMediaType.TEXT_HTML)
                               .WithBody<CreateIntent>(request, MediaTypeHeaderValue.Parse(HttpMediaType.APPLICATION_JSON))
                               .As<IntentResponse>()
                               .Result;
            }
            catch (AggregateException ae)
            {

                throw ae.Flatten();
            }

            return result;
        }

        public object DeleteIntent(string workspaceId, string intent)
        {
            object result = null;

            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("parameter: workspaceId");
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException("parameter: intent");

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .DeleteAsync($"{this.Endpoint}{PATH_CONVERSATION}/{workspaceId}/intents/{intent}")                               
                               .WithArgument("version", VERSION_DATE_2017_02_03)
                               .WithHeader("accept", HttpMediaType.TEXT_HTML)
                               .As<object>()
                               .Result;
            }
            catch (AggregateException ae)
            {

                throw ae.Flatten();
            }

            return result;
        }

        public IntentExportResponse GetIntent(string workspaceId, string intent)
        {
            return this.GetIntent(workspaceId, intent, false);
        }

        public IntentExportResponse GetIntent(string workspaceId, string intent, bool export = false)
        {
            IntentExportResponse result = null;

            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("parameter: workspaceId");
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException("parameter: intent");

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .GetAsync($"{this.Endpoint}{PATH_CONVERSATION}/{workspaceId}/intents/{intent}")
                               .WithArgument("version", VERSION_DATE_2017_02_03)
                               .WithArgument("export", export)
                               .WithHeader("accept", HttpMediaType.APPLICATION_JSON)
                               .As<IntentExportResponse>()
                               .Result;
            }
            catch (AggregateException ae)
            {

                throw ae.Flatten();
            }

            return result;
        }

        public IntentResponse UpdateIntent(string workspaceId, string intent, UpdateIntent request)
        {
            IntentResponse result = null;

            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("parameter: workspaceId");
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException("parameter: intent");
            if (request == null)
                throw new ArgumentNullException("parameter: request");

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .PostAsync($"{this.Endpoint}{PATH_CONVERSATION}/{workspaceId}")
                               .WithArgument("version", VERSION_DATE_2017_02_03)
                               .WithHeader("accept", HttpMediaType.TEXT_HTML)
                               .WithBody<UpdateIntent>(request, MediaTypeHeaderValue.Parse(HttpMediaType.APPLICATION_JSON))
                               .As<IntentResponse>()
                               .Result;
            }
            catch (AggregateException ae)
            {

                throw ae.Flatten();
            }

            return result;
        }

        #endregion

        #region Examples

        public ExampleCollectionResponse ListExamples(string workspaceId, string intent)
        {
            ExampleCollectionResponse result = null;

            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("parameter: workspaceId");
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException("parameter: intent");

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .GetAsync($"{this.Endpoint}{PATH_CONVERSATION}/{workspaceId}/intents/{intent}/examples")
                               .WithArgument("version", VERSION_DATE_2017_02_03)
                               .WithHeader("accept", HttpMediaType.APPLICATION_JSON)
                               .As<ExampleCollectionResponse>()
                               .Result;
            }
            catch (AggregateException ae)
            {

                throw ae.Flatten();
            }

            return result;
        }

        public ExampleResponse CreateExample(string workspaceId, string intent, CreateExample request)
        {
            ExampleResponse result = null;

            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("parameter: workspaceId");
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException("parameter: intent");
            if (request == null)
                throw new ArgumentNullException("parameter: request");

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .PostAsync($"{this.Endpoint}{PATH_CONVERSATION}/{workspaceId}/intents/{intent}/examples")
                               .WithArgument("version", VERSION_DATE_2017_02_03)
                               .WithHeader("accept", HttpMediaType.TEXT_HTML)
                               .WithBody<CreateExample>(request, MediaTypeHeaderValue.Parse(HttpMediaType.APPLICATION_JSON))
                               .As<ExampleResponse>()
                               .Result;
            }
            catch (AggregateException ae)
            {

                throw ae.Flatten();
            }

            return result;
        }

        public object DeleteExample(string workspaceId, string intent, string text)
        {
            object result = null;

            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("parameter: workspaceId");
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException("parameter: intent");
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("parameter: text");

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .DeleteAsync($"{this.Endpoint}{PATH_CONVERSATION}/{workspaceId}/intents/{intent}/examples/{text}")
                               .WithArgument("version", VERSION_DATE_2017_02_03)
                               .WithHeader("accept", HttpMediaType.TEXT_HTML)
                               .As<object>()
                               .Result;
            }
            catch (AggregateException ae)
            {

                throw ae.Flatten();
            }

            return result;
        }

        public ExampleResponse GetExample(string workspaceId, string intent, string text)
        {
            ExampleResponse result = null;

            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("parameter: workspaceId");
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException("parameter: intent");
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("parameter: text");

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .GetAsync($"{this.Endpoint}{PATH_CONVERSATION}/{workspaceId}/intents/{intent}/examples/{text}")
                               .WithArgument("version", VERSION_DATE_2017_02_03)
                               .WithHeader("accept", HttpMediaType.APPLICATION_JSON)
                               .As<ExampleResponse>()
                               .Result;
            }
            catch (AggregateException ae)
            {

                throw ae.Flatten();
            }

            return result;
        }

        public ExampleResponse UpdateExample(string workspaceId, string intent, string text, UpdateExample request)
        {
            ExampleResponse result = null;

            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("parameter: workspaceId");
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException("parameter: intent");
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("parameter: text");
            if (request == null)
                throw new ArgumentNullException("parameter: request");

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .PostAsync($"{this.Endpoint}{PATH_CONVERSATION}/{workspaceId}/intents/{intent}/examples/{text}")
                               .WithArgument("version", VERSION_DATE_2017_02_03)
                               .WithHeader("accept", HttpMediaType.TEXT_HTML)
                               .WithBody<UpdateExample>(request, MediaTypeHeaderValue.Parse(HttpMediaType.APPLICATION_JSON))
                               .As<ExampleResponse>()
                               .Result;
            }
            catch (AggregateException ae)
            {

                throw ae.Flatten();
            }

            return result;
        }

        #endregion
    }
}
