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

using Newtonsoft.Json;

namespace IBM.Watson.Assistant.v2.Model
{
    /// <summary>
    /// Built-in system properties that apply to all skills used by the assistant.
    /// </summary>
    public class MessageContextGlobalSystem
    {
        /// <summary>
        /// The language code for localization in the user input. The specified locale overrides the default for the
        /// assistant, and is used for interpreting entity values in user input such as date values. For example,
        /// `04/03/2018` might be interpreted either as April 3 or March 4, depending on the locale.
        ///
        ///  This property is included only if the new system entities are enabled for the skill.
        /// </summary>
        public class LocaleEnumValue
        {
            /// <summary>
            /// Constant EN_US for en-us
            /// </summary>
            public const string EN_US = "en-us";
            /// <summary>
            /// Constant EN_CA for en-ca
            /// </summary>
            public const string EN_CA = "en-ca";
            /// <summary>
            /// Constant EN_GB for en-gb
            /// </summary>
            public const string EN_GB = "en-gb";
            /// <summary>
            /// Constant AR_AR for ar-ar
            /// </summary>
            public const string AR_AR = "ar-ar";
            /// <summary>
            /// Constant CS_CZ for cs-cz
            /// </summary>
            public const string CS_CZ = "cs-cz";
            /// <summary>
            /// Constant DE_DE for de-de
            /// </summary>
            public const string DE_DE = "de-de";
            /// <summary>
            /// Constant ES_ES for es-es
            /// </summary>
            public const string ES_ES = "es-es";
            /// <summary>
            /// Constant FR_FR for fr-fr
            /// </summary>
            public const string FR_FR = "fr-fr";
            /// <summary>
            /// Constant IT_IT for it-it
            /// </summary>
            public const string IT_IT = "it-it";
            /// <summary>
            /// Constant JA_JP for ja-jp
            /// </summary>
            public const string JA_JP = "ja-jp";
            /// <summary>
            /// Constant KO_KR for ko-kr
            /// </summary>
            public const string KO_KR = "ko-kr";
            /// <summary>
            /// Constant NL_NL for nl-nl
            /// </summary>
            public const string NL_NL = "nl-nl";
            /// <summary>
            /// Constant PT_BR for pt-br
            /// </summary>
            public const string PT_BR = "pt-br";
            /// <summary>
            /// Constant ZH_CN for zh-cn
            /// </summary>
            public const string ZH_CN = "zh-cn";
            /// <summary>
            /// Constant ZH_TW for zh-tw
            /// </summary>
            public const string ZH_TW = "zh-tw";
            
        }

        /// <summary>
        /// The language code for localization in the user input. The specified locale overrides the default for the
        /// assistant, and is used for interpreting entity values in user input such as date values. For example,
        /// `04/03/2018` might be interpreted either as April 3 or March 4, depending on the locale.
        ///
        ///  This property is included only if the new system entities are enabled for the skill.
        /// Constants for possible values can be found using MessageContextGlobalSystem.LocaleEnumValue
        /// </summary>
        [JsonProperty("locale", NullValueHandling = NullValueHandling.Ignore)]
        public string Locale { get; set; }
        /// <summary>
        /// The user time zone. The assistant uses the time zone to correctly resolve relative time references.
        /// </summary>
        [JsonProperty("timezone", NullValueHandling = NullValueHandling.Ignore)]
        public string Timezone { get; set; }
        /// <summary>
        /// A string value that identifies the user who is interacting with the assistant. The client must provide a
        /// unique identifier for each individual end user who accesses the application. For user-based plans, this user
        /// ID is used to identify unique users for billing purposes. This string cannot contain carriage return,
        /// newline, or tab characters. If no value is specified in the input, **user_id** is automatically set to the
        /// value of **context.global.session_id**.
        ///
        /// **Note:** This property is the same as the **user_id** property at the root of the message body. If
        /// **user_id** is specified in both locations in a message request, the value specified at the root is used.
        /// </summary>
        [JsonProperty("user_id", NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }
        /// <summary>
        /// A counter that is automatically incremented with each turn of the conversation. A value of 1 indicates that
        /// this is the the first turn of a new conversation, which can affect the behavior of some skills (for example,
        /// triggering the start node of a dialog).
        /// </summary>
        [JsonProperty("turn_count", NullValueHandling = NullValueHandling.Ignore)]
        public long? TurnCount { get; set; }
        /// <summary>
        /// The base time for interpreting any relative time mentions in the user input. The specified time overrides
        /// the current server time, and is used to calculate times mentioned in relative terms such as `now` or
        /// `tomorrow`. This can be useful for simulating past or future times for testing purposes, or when analyzing
        /// documents such as news articles.
        ///
        /// This value must be a UTC time value formatted according to ISO 8601 (for example, `2021-06-26T12:00:00Z` for
        /// noon UTC on 26 June 2021).
        ///
        /// This property is included only if the new system entities are enabled for the skill.
        /// </summary>
        [JsonProperty("reference_time", NullValueHandling = NullValueHandling.Ignore)]
        public string ReferenceTime { get; set; }
        /// <summary>
        /// The time at which the session started. With the stateful `message` method, the start time is always present,
        /// and is set by the service based on the time the session was created. With the stateless `message` method,
        /// the start time is set by the service in the response to the first message, and should be returned as part of
        /// the context with each subsequent message in the session.
        ///
        /// This value is a UTC time value formatted according to ISO 8601 (for example, `2021-06-26T12:00:00Z` for noon
        /// UTC on 26 June 2021).
        /// </summary>
        [JsonProperty("session_start_time", NullValueHandling = NullValueHandling.Ignore)]
        public string SessionStartTime { get; set; }
        /// <summary>
        /// An encoded string that represents the configuration state of the assistant at the beginning of the
        /// conversation. If you are using the stateless `message` method, save this value and then send it in the
        /// context of the subsequent message request to avoid disruptions if there are configuration changes during the
        /// conversation (such as a change to a skill the assistant uses).
        /// </summary>
        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }
    }

}
