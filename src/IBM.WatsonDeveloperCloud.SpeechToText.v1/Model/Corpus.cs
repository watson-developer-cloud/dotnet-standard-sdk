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

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Model
{
    /// <summary>
    /// Corpus.
    /// </summary>
    public class Corpus
    {
        /// <summary>
        /// The status of the corpus: * `analyzed` indicates that the service has successfully analyzed the corpus; the custom model can be trained with data from the corpus. * `being_processed` indicates that the service is still analyzing the corpus; the service cannot accept requests to add new corpora or words, or to train the custom model. * `undetermined` indicates that the service encountered an error while processing the corpus.
        /// </summary>
        /// <value>The status of the corpus: * `analyzed` indicates that the service has successfully analyzed the corpus; the custom model can be trained with data from the corpus. * `being_processed` indicates that the service is still analyzing the corpus; the service cannot accept requests to add new corpora or words, or to train the custom model. * `undetermined` indicates that the service encountered an error while processing the corpus.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            
            /// <summary>
            /// Enum ANALYZED for analyzed
            /// </summary>
            [EnumMember(Value = "analyzed")]
            ANALYZED,
            
            /// <summary>
            /// Enum BEING_PROCESSED for being_processed
            /// </summary>
            [EnumMember(Value = "being_processed")]
            BEING_PROCESSED,
            
            /// <summary>
            /// Enum UNDETERMINED for undetermined
            /// </summary>
            [EnumMember(Value = "undetermined")]
            UNDETERMINED
        }

        /// <summary>
        /// The status of the corpus: * `analyzed` indicates that the service has successfully analyzed the corpus; the custom model can be trained with data from the corpus. * `being_processed` indicates that the service is still analyzing the corpus; the service cannot accept requests to add new corpora or words, or to train the custom model. * `undetermined` indicates that the service encountered an error while processing the corpus.
        /// </summary>
        /// <value>The status of the corpus: * `analyzed` indicates that the service has successfully analyzed the corpus; the custom model can be trained with data from the corpus. * `being_processed` indicates that the service is still analyzing the corpus; the service cannot accept requests to add new corpora or words, or to train the custom model. * `undetermined` indicates that the service encountered an error while processing the corpus.</value>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// The name of the corpus.
        /// </summary>
        /// <value>The name of the corpus.</value>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// The total number of words in the corpus. The value is `0` while the corpus is being processed.
        /// </summary>
        /// <value>The total number of words in the corpus. The value is `0` while the corpus is being processed.</value>
        [JsonProperty("total_words", NullValueHandling = NullValueHandling.Ignore)]
        public long? TotalWords { get; set; }
        /// <summary>
        /// The number of OOV words in the corpus. The value is `0` while the corpus is being processed.
        /// </summary>
        /// <value>The number of OOV words in the corpus. The value is `0` while the corpus is being processed.</value>
        [JsonProperty("out_of_vocabulary_words", NullValueHandling = NullValueHandling.Ignore)]
        public long? OutOfVocabularyWords { get; set; }
        /// <summary>
        /// If the status of the corpus is `undetermined`, the following message: `Analysis of corpus 'name' failed. Please try adding the corpus again by setting the 'allow_overwrite' flag to 'true'`.
        /// </summary>
        /// <value>If the status of the corpus is `undetermined`, the following message: `Analysis of corpus 'name' failed. Please try adding the corpus again by setting the 'allow_overwrite' flag to 'true'`.</value>
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }
    }

}
