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
    /// Provides age information about a face. If there are more than 10 faces in an image, the response might return the confidence score `0g.
    /// </summary>
    public class FaceAge
    {
        /// <summary>
        /// Estimated minimum age.
        /// </summary>
        /// <value>Estimated minimum age.</value>
        [JsonProperty("min", NullValueHandling = NullValueHandling.Ignore)]
        public long? Min { get; set; }
        /// <summary>
        /// Estimated maximum age.
        /// </summary>
        /// <value>Estimated maximum age.</value>
        [JsonProperty("max", NullValueHandling = NullValueHandling.Ignore)]
        public long? Max { get; set; }
        /// <summary>
        /// Confidence score for the property in the range of 0 to 1. A higher score indicates greater likelihood that the class is depicted in the image. The default threshold for returning scores from a classifier is 0.5.
        /// </summary>
        /// <value>Confidence score for the property in the range of 0 to 1. A higher score indicates greater likelihood that the class is depicted in the image. The default threshold for returning scores from a classifier is 0.5.</value>
        [JsonProperty("score", NullValueHandling = NullValueHandling.Ignore)]
        public float? Score { get; set; }
    }

}
