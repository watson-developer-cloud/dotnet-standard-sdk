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

using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Model
{
    public class Corpus
    {
        /// <summary>
        /// Gets or sets the name of the corpus.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the total number of words in the corpus. The value is
        /// `0` while the corpus is being processed.
        /// </summary>
        [JsonProperty(PropertyName = "total_words")]
        public int TotalWords { get; set; }

        /// <summary>
        /// Gets or sets the number of OOV words in the corpus. The value is
        /// `0` while the corpus is being processed.
        /// </summary>
        [JsonProperty(PropertyName = "out_of_vocabulary_words")]
        public int OutOfVocabularyWords { get; set; }

        /// <summary>
        /// Gets or sets the status of the corpus: `analyzed` indicates that
        /// the service has successfully analyzed the corpus; the custom
        /// model can be trained with data from the corpus. `being_processed`
        /// indicates that the service is still analyzing the corpus; the
        /// service cannot accept requests to add new corpora or words, or to
        /// train the custom model. `undetermined` indicates that the service
        /// encountered an error while processing the corpus. Possible values
        /// include: 'analyzed', 'being_processed', 'undetermined'
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets if the status of the corpus is `undetermined`, the
        /// following message: `Analysis of corpus 'name' failed. Please try
        /// adding the corpus again by setting the 'allow_overwrite' flag to
        /// 'true'`.
        /// </summary>
        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }
    }
}
