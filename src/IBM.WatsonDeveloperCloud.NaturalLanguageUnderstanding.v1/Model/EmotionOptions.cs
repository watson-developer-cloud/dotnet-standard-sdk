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
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model
{
    /// <summary>
    /// Whether or not to return emotion analysis of the content.
    /// </summary>
    public class EmotionOptions
    {
        /// <summary>
        /// Set this to false to hide document-level emotion results.
        /// </summary>
        /// <value>Set this to false to hide document-level emotion results.</value>
        [JsonProperty("document", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Document { get; set; }
        /// <summary>
        /// Emotion results will be returned for each target string that is found in the document.
        /// </summary>
        /// <value>Emotion results will be returned for each target string that is found in the document.</value>
        [JsonProperty("targets", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Targets { get; set; }
    }

}
