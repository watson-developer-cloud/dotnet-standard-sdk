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

using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Model
{
    /// <summary>
    /// DialogNodeAction.
    /// </summary>
    public class DialogNodeAction
    {
        /// <summary>
        /// The type of action to invoke.
        /// </summary>
        /// <value>The type of action to invoke.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ActionTypeEnum
        {
            
            /// <summary>
            /// Enum CLIENT for client
            /// </summary>
            [EnumMember(Value = "client")]
            CLIENT,
            
            /// <summary>
            /// Enum SERVER for server
            /// </summary>
            [EnumMember(Value = "server")]
            SERVER
        }

        /// <summary>
        /// The type of action to invoke.
        /// </summary>
        /// <value>The type of action to invoke.</value>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public ActionTypeEnum? ActionType { get; set; }
        /// <summary>
        /// The name of the action.
        /// </summary>
        /// <value>The name of the action.</value>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// A map of key/value pairs to be provided to the action.
        /// </summary>
        /// <value>A map of key/value pairs to be provided to the action.</value>
        [JsonProperty("parameters", NullValueHandling = NullValueHandling.Ignore)]
        public object Parameters { get; set; }
        /// <summary>
        /// The location in the dialog context where the result of the action is stored.
        /// </summary>
        /// <value>The location in the dialog context where the result of the action is stored.</value>
        [JsonProperty("result_variable", NullValueHandling = NullValueHandling.Ignore)]
        public string ResultVariable { get; set; }
        /// <summary>
        /// The name of the context variable that the client application will use to pass in credentials for the action.
        /// </summary>
        /// <value>The name of the context variable that the client application will use to pass in credentials for the action.</value>
        [JsonProperty("credentials", NullValueHandling = NullValueHandling.Ignore)]
        public string Credentials { get; set; }
    }

}
