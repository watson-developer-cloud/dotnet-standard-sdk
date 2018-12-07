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
using IBM.WatsonDeveloperCloud.Assistant.v2.Model;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.Util;
using Newtonsoft.Json;
using System;

namespace IBM.WatsonDeveloperCloud.Assistant.v2
{
    public partial class AssistantService : WatsonService, IAssistantService
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

        public AssistantService(TokenOptions options, string versionDate) : this()
        {
            if (string.IsNullOrEmpty(options.IamApiKey) && string.IsNullOrEmpty(options.IamAccessToken))
                throw new ArgumentNullException(nameof(options.IamAccessToken) + ", " + nameof(options.IamApiKey));
            if(string.IsNullOrEmpty(versionDate))
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

        public AssistantService(IClient httpClient) : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        /// <summary>
        /// Create a session.
        ///
        /// Create a new session. A session is used to send user input to a skill and receive responses. It also
        /// maintains the state of the conversation.
        /// </summary>
        /// <param name="assistantId">Unique identifier of the assistant. You can find the assistant ID of an assistant
        /// on the **Assistants** tab of the Watson Assistant tool. For information about creating assistants, see the
        /// [documentation](https://console.bluemix.net/docs/services/assistant/create-assistant.html#creating-assistants).
        ///
        /// **Note:** Currently, the v2 API does not support creating assistants.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="SessionResponse" />SessionResponse</returns>
        public SessionResponse CreateSession(string assistantId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(assistantId))
                throw new ArgumentNullException(nameof(assistantId));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            SessionResponse result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v2/assistants/{assistantId}/sessions");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<SessionResponse>().Result;
                if(result == null)
                    result = new SessionResponse();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete session.
        ///
        /// Deletes a session explicitly before it times out.
        /// </summary>
        /// <param name="assistantId">Unique identifier of the assistant. You can find the assistant ID of an assistant
        /// on the **Assistants** tab of the Watson Assistant tool. For information about creating assistants, see the
        /// [documentation](https://console.bluemix.net/docs/services/assistant/create-assistant.html#creating-assistants).
        ///
        /// **Note:** Currently, the v2 API does not support creating assistants.</param>
        /// <param name="sessionId">Unique identifier of the session.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel DeleteSession(string assistantId, string sessionId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(assistantId))
                throw new ArgumentNullException(nameof(assistantId));
            if (string.IsNullOrEmpty(sessionId))
                throw new ArgumentNullException(nameof(sessionId));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            BaseModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.DeleteAsync($"{this.Endpoint}/v2/assistants/{assistantId}/sessions/{sessionId}");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Send user input to assistant.
        ///
        /// Send user input to an assistant and receive a response.
        ///
        /// There is no rate limit for this operation.
        /// </summary>
        /// <param name="assistantId">Unique identifier of the assistant. You can find the assistant ID of an assistant
        /// on the **Assistants** tab of the Watson Assistant tool. For information about creating assistants, see the
        /// [documentation](https://console.bluemix.net/docs/services/assistant/create-assistant.html#creating-assistants).
        ///
        /// **Note:** Currently, the v2 API does not support creating assistants.</param>
        /// <param name="sessionId">Unique identifier of the session.</param>
        /// <param name="request">The message to be sent. This includes the user's input, along with optional content
        /// such as intents and entities. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="MessageResponse" />MessageResponse</returns>
        public MessageResponse Message(string assistantId, string sessionId, MessageRequest request = null, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(assistantId))
                throw new ArgumentNullException(nameof(assistantId));
            if (string.IsNullOrEmpty(sessionId))
                throw new ArgumentNullException(nameof(sessionId));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            MessageResponse result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v2/assistants/{assistantId}/sessions/{sessionId}/message");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBody<MessageRequest>(request);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<MessageResponse>().Result;
                if(result == null)
                    result = new MessageResponse();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
