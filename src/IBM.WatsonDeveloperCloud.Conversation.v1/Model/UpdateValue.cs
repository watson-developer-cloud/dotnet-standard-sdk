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
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Model
{
    /// <summary>
    /// UpdateValue.
    /// </summary>
    public class UpdateValue
    {
        /// <summary>
        /// Specifies the type of value (`synonyms` or `patterns`). The default value is `synonyms`.
        /// </summary>
        /// <value>Specifies the type of value (`synonyms` or `patterns`). The default value is `synonyms`.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ValueTypeEnum
        {
            
            /// <summary>
            /// Enum SYNONYMS for synonyms
            /// </summary>
            [EnumMember(Value = "synonyms")]
            SYNONYMS,
            
            /// <summary>
            /// Enum PATTERNS for patterns
            /// </summary>
            [EnumMember(Value = "patterns")]
            PATTERNS
        }

        /// <summary>
        /// Specifies the type of value (`synonyms` or `patterns`). The default value is `synonyms`.
        /// </summary>
        /// <value>Specifies the type of value (`synonyms` or `patterns`). The default value is `synonyms`.</value>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public ValueTypeEnum? ValueType { get; set; }
        /// <summary>
        /// The text of the entity value.
        /// </summary>
        /// <value>The text of the entity value.</value>
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
        /// <summary>
        /// Any metadata related to the entity value.
        /// </summary>
        /// <value>Any metadata related to the entity value.</value>
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public object Metadata { get; set; }
        /// <summary>
        /// An array of synonyms for the entity value.
        /// </summary>
        /// <value>An array of synonyms for the entity value.</value>
        [JsonProperty("synonyms", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Synonyms { get; set; }
        /// <summary>
        /// An array of patterns for the entity value. A pattern is specified as a regular expression.
        /// </summary>
        /// <value>An array of patterns for the entity value. A pattern is specified as a regular expression.</value>
        [JsonProperty("patterns", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Patterns { get; set; }
    }

}
