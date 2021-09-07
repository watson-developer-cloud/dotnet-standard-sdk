/**
* (C) Copyright IBM Corp. 2021.
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
using JsonSubTypes;
using Newtonsoft.Json;

namespace IBM.Watson.Assistant.v2.Model
{
    /// <summary>
    /// RuntimeResponseGeneric.
    /// Classes which extend this class:
    /// - RuntimeResponseGenericRuntimeResponseTypeText
    /// - RuntimeResponseGenericRuntimeResponseTypePause
    /// - RuntimeResponseGenericRuntimeResponseTypeImage
    /// - RuntimeResponseGenericRuntimeResponseTypeOption
    /// - RuntimeResponseGenericRuntimeResponseTypeConnectToAgent
    /// - RuntimeResponseGenericRuntimeResponseTypeSuggestion
    /// - RuntimeResponseGenericRuntimeResponseTypeChannelTransfer
    /// - RuntimeResponseGenericRuntimeResponseTypeSearch
    /// - RuntimeResponseGenericRuntimeResponseTypeUserDefined
    /// </summary>
    [JsonConverter(typeof(JsonSubtypes), "response_type")]
    [JsonSubtypes.KnownSubType(typeof(RuntimeResponseGenericRuntimeResponseTypeChannelTransfer), "channel_transfer")]
    [JsonSubtypes.KnownSubType(typeof(RuntimeResponseGenericRuntimeResponseTypeConnectToAgent), "connect_to_agent")]
    [JsonSubtypes.KnownSubType(typeof(RuntimeResponseGenericRuntimeResponseTypeImage), "image")]
    [JsonSubtypes.KnownSubType(typeof(RuntimeResponseGenericRuntimeResponseTypeOption), "option")]
    [JsonSubtypes.KnownSubType(typeof(RuntimeResponseGenericRuntimeResponseTypeSuggestion), "suggestion")]
    [JsonSubtypes.KnownSubType(typeof(RuntimeResponseGenericRuntimeResponseTypePause), "pause")]
    [JsonSubtypes.KnownSubType(typeof(RuntimeResponseGenericRuntimeResponseTypeSearch), "search")]
    [JsonSubtypes.KnownSubType(typeof(RuntimeResponseGenericRuntimeResponseTypeText), "text")]
    [JsonSubtypes.KnownSubType(typeof(RuntimeResponseGenericRuntimeResponseTypeUserDefined), "user_defined")]
    public class RuntimeResponseGeneric
    {
        /// This ctor is protected to prevent instantiation of this base class.
        /// Instead, users should instantiate one of the subclasses listed below:
        /// - RuntimeResponseGenericRuntimeResponseTypeText
        /// - RuntimeResponseGenericRuntimeResponseTypePause
        /// - RuntimeResponseGenericRuntimeResponseTypeImage
        /// - RuntimeResponseGenericRuntimeResponseTypeOption
        /// - RuntimeResponseGenericRuntimeResponseTypeConnectToAgent
        /// - RuntimeResponseGenericRuntimeResponseTypeSuggestion
        /// - RuntimeResponseGenericRuntimeResponseTypeChannelTransfer
        /// - RuntimeResponseGenericRuntimeResponseTypeSearch
        /// - RuntimeResponseGenericRuntimeResponseTypeUserDefined
        protected RuntimeResponseGeneric()
        {
        }

        /// <summary>
        /// The preferred type of control to display.
        /// </summary>
        public class PreferenceEnumValue
        {
            /// <summary>
            /// Constant DROPDOWN for dropdown
            /// </summary>
            public const string DROPDOWN = "dropdown";
            /// <summary>
            /// Constant BUTTON for button
            /// </summary>
            public const string BUTTON = "button";
            
        }

        /// <summary>
        /// The preferred type of control to display.
        /// Constants for possible values can be found using RuntimeResponseGeneric.PreferenceEnumValue
        /// </summary>
        [JsonProperty("preference", NullValueHandling = NullValueHandling.Ignore)]
        public string Preference { get; set; }
        /// <summary>
        /// The type of response returned by the dialog node. The specified response type must be supported by the
        /// client application or channel.
        /// </summary>
        [JsonProperty("response_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ResponseType { get; protected set; }
        /// <summary>
        /// The text of the response.
        /// </summary>
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; protected set; }
        /// <summary>
        /// An array of objects specifying channels for which the response is intended. If **channels** is present, the
        /// response is intended for a built-in integration and should not be handled by an API client.
        /// </summary>
        [JsonProperty("channels", NullValueHandling = NullValueHandling.Ignore)]
        public List<ResponseGenericChannel> Channels { get; protected set; }
        /// <summary>
        /// How long to pause, in milliseconds.
        /// </summary>
        [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
        public long? Time { get; protected set; }
        /// <summary>
        /// Whether to send a "user is typing" event during the pause.
        /// </summary>
        [JsonProperty("typing", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Typing { get; protected set; }
        /// <summary>
        /// The `https:` URL of the image.
        /// </summary>
        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public string Source { get; protected set; }
        /// <summary>
        /// The title to show before the response.
        /// </summary>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; protected set; }
        /// <summary>
        /// The description to show with the the response.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; protected set; }
        /// <summary>
        /// Descriptive text that can be used for screen readers or other situations where the image cannot be seen.
        /// </summary>
        [JsonProperty("alt_text", NullValueHandling = NullValueHandling.Ignore)]
        public string AltText { get; protected set; }
        /// <summary>
        /// An array of objects describing the options from which the user can choose.
        /// </summary>
        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public List<DialogNodeOutputOptionsElement> Options { get; protected set; }
        /// <summary>
        /// A message to be sent to the human agent who will be taking over the conversation.
        /// </summary>
        [JsonProperty("message_to_human_agent", NullValueHandling = NullValueHandling.Ignore)]
        public string MessageToHumanAgent { get; protected set; }
        /// <summary>
        /// An optional message to be displayed to the user to indicate that the conversation will be transferred to the
        /// next available agent.
        /// </summary>
        [JsonProperty("agent_available", NullValueHandling = NullValueHandling.Ignore)]
        public AgentAvailabilityMessage AgentAvailable { get; protected set; }
        /// <summary>
        /// An optional message to be displayed to the user to indicate that no online agent is available to take over
        /// the conversation.
        /// </summary>
        [JsonProperty("agent_unavailable", NullValueHandling = NullValueHandling.Ignore)]
        public AgentAvailabilityMessage AgentUnavailable { get; protected set; }
        /// <summary>
        /// A label identifying the topic of the conversation, derived from the **title** property of the relevant node
        /// or the **topic** property of the dialog node response.
        /// </summary>
        [JsonProperty("topic", NullValueHandling = NullValueHandling.Ignore)]
        public string Topic { get; protected set; }
        /// <summary>
        /// An array of objects describing the possible matching dialog nodes from which the user can choose.
        /// </summary>
        [JsonProperty("suggestions", NullValueHandling = NullValueHandling.Ignore)]
        public List<DialogSuggestion> Suggestions { get; protected set; }
        /// <summary>
        /// The message to display to the user when initiating a channel transfer.
        /// </summary>
        [JsonProperty("message_to_user", NullValueHandling = NullValueHandling.Ignore)]
        public string MessageToUser { get; protected set; }
        /// <summary>
        /// The title or introductory text to show before the response. This text is defined in the search skill
        /// configuration.
        /// </summary>
        [JsonProperty("header", NullValueHandling = NullValueHandling.Ignore)]
        public string Header { get; protected set; }
        /// <summary>
        /// An array of objects that contains the search results to be displayed in the initial response to the user.
        /// </summary>
        [JsonProperty("primary_results", NullValueHandling = NullValueHandling.Ignore)]
        public List<SearchResult> PrimaryResults { get; protected set; }
        /// <summary>
        /// An array of objects that contains additional search results that can be displayed to the user upon request.
        /// </summary>
        [JsonProperty("additional_results", NullValueHandling = NullValueHandling.Ignore)]
        public List<SearchResult> AdditionalResults { get; protected set; }
        /// <summary>
        /// An object containing any properties for the user-defined response type.
        /// </summary>
        [JsonProperty("user_defined", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> UserDefined { get; protected set; }
    }

}
