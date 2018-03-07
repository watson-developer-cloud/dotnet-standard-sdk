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

using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model
{
    /// <summary>
    /// Provides information about a celebrity who is detected in the image. Not returned when a celebrity is not detected.
    /// </summary>
    public class FaceIdentity
    {
        /// <summary>
        /// Name of the person.
        /// </summary>
        /// <value>Name of the person.</value>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// Confidence score for the property in the range of 0 to 1. A higher score indicates greater likelihood that the class is depicted in the image. The default threshold for returning scores from a classifier is 0.5.
        /// </summary>
        /// <value>Confidence score for the property in the range of 0 to 1. A higher score indicates greater likelihood that the class is depicted in the image. The default threshold for returning scores from a classifier is 0.5.</value>
        [JsonProperty("score", NullValueHandling = NullValueHandling.Ignore)]
        public float? Score { get; set; }
        /// <summary>
        /// Knowledge graph of the property. For example, `People/Leaders/Presidents/USA/Barack Obama`. Included only if identified.
        /// </summary>
        /// <value>Knowledge graph of the property. For example, `People/Leaders/Presidents/USA/Barack Obama`. Included only if identified.</value>
        [JsonProperty("type_hierarchy", NullValueHandling = NullValueHandling.Ignore)]
        public string TypeHierarchy { get; set; }
    }

}
