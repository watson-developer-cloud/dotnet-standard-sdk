/**
* (C) Copyright IBM Corp. 2018, 2020.
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
using IBM.Cloud.SDK.Core.Authentication;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Service;
using IBM.Watson.Assistant.v2.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace IBM.Watson.Assistant.v2
{
    public partial class AssistantService : IBMService, IAssistantService
    {
        const string defaultServiceName = "assistant";
        private const string defaultServiceUrl = "https://gateway.watsonplatform.net/assistant/api";
        public string VersionDate { get; set; }

        public AssistantService(string versionDate) : this(versionDate, ConfigBasedAuthenticatorFactory.GetAuthenticator(defaultServiceName)) { }
        public AssistantService(IClient httpClient) : base(defaultServiceName, httpClient) { }

        public AssistantService(string versionDate, IAuthenticator authenticator) : base(defaultServiceName, authenticator)
        {
            if (string.IsNullOrEmpty(versionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            VersionDate = versionDate;

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
        /// [documentation](https://cloud.ibm.com/docs/services/assistant?topic=assistant-assistant-settings).
        /// </summary>
        /// <param name="assistantId">Unique identifier of the assistant. To find the assistant ID in the Watson
        /// Assistant user interface, open the assistant settings and click **API Details**. For information about
        /// creating assistants, see the
        /// [documentation](https://cloud.ibm.com/docs/services/assistant?topic=assistant-assistant-add#assistant-add-task).
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

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<SessionResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/assistants/{assistantId}/sessions");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

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
        /// timeout, see the
        /// [documentation](https://cloud.ibm.com/docs/services/assistant?topic=assistant-assistant-settings)).
        /// </summary>
        /// <param name="assistantId">Unique identifier of the assistant. To find the assistant ID in the Watson
        /// Assistant user interface, open the assistant settings and click **API Details**. For information about
        /// creating assistants, see the
        /// [documentation](https://cloud.ibm.com/docs/services/assistant?topic=assistant-assistant-add#assistant-add-task).
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
            if (string.IsNullOrEmpty(sessionId))
            {
                throw new ArgumentNullException("`sessionId` is required for `DeleteSession`");
            }
            else
            {
                sessionId = Uri.EscapeDataString(sessionId);
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v2/assistants/{assistantId}/sessions/{sessionId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

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
        /// Send user input to assistant.
        ///
        /// Send user input to an assistant and receive a response.
        ///
        /// There is no rate limit for this operation.
        /// </summary>
        /// <param name="assistantId">Unique identifier of the assistant. To find the assistant ID in the Watson
        /// Assistant user interface, open the assistant settings and click **API Details**. For information about
        /// creating assistants, see the
        /// [documentation](https://cloud.ibm.com/docs/services/assistant?topic=assistant-assistant-add#assistant-add-task).
        ///
        /// **Note:** Currently, the v2 API does not support creating assistants.</param>
        /// <param name="sessionId">Unique identifier of the session.</param>
        /// <param name="input">An input object that includes the input text. (optional)</param>
        /// <param name="context">State information for the conversation. The context is stored by the assistant on a
        /// per-session basis. You can use this property to set or modify context variables, which can also be accessed
        /// by dialog nodes. (optional)</param>
        /// <returns><see cref="MessageResponse" />MessageResponse</returns>
        public DetailedResponse<MessageResponse> Message(string assistantId, string sessionId, MessageInput input = null, MessageContext context = null)
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

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<MessageResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/assistants/{assistantId}/sessions/{sessionId}/message");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
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
    }
}
