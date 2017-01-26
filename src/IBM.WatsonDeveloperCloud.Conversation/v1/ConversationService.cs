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
        const string PATH_MESSAGE = "/v1/workspaces/{0}/message";
        const string VERSION_DATE_2016_09_20 = "2016-09-20";
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

        public MessageResponse Message(string workspaceId, MessageRequest request)
        {
            MessageResponse result = null;

            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException("parameter: workspaceId");

            try
            {
                result =
                this.Client.WithAuthentication(this.UserName, this.Password)
                      .PostAsync(this.Endpoint + string.Format(PATH_MESSAGE, workspaceId))
                      .WithArgument("version", VERSION_DATE_2016_09_20)
                      .WithHeader("accept", HttpMediaType.APPLICATION_JSON)
                      .WithBody<MessageRequest>(request, MediaTypeHeaderValue.Parse(HttpMediaType.APPLICATION_JSON))
                      .As<MessageResponse>()
                      .Result;
            }
            catch (AggregateException ae)
            {
                ExceptionDispatchInfo.Capture(ae.Flatten().InnerException).Throw(); ;
            }

            return result;
        }
    }
}
