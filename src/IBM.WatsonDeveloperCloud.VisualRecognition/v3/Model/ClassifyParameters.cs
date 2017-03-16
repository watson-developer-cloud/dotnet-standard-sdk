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

namespace IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model
{
    public class ClassifyParameters
    {
        /// <summary>
        /// Array of image URLs to analyze.
        /// </summary>
        [JsonProperty("urls")]
        public string[] URLs { get; set; }
        /// <summary>
        /// An array of classifier identifiers to use for image analysis.
        /// </summary>
        [JsonProperty("classifier_ids")]
        public string[] ClassifierIds { get; set; }
        /// <summary>
        /// An array of owners to use.
        /// </summary>
        [JsonProperty("owners")]
        public string[] Owners { get; set; }
        /// <summary>
        /// Score threshold to return images.
        /// </summary>
        [JsonProperty("threshold")]
        public float Threshold { get; set; }
    }
}
