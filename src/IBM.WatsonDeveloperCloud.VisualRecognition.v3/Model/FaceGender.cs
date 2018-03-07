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
    /// Provides information about the gender of the face. If there are more than 10 faces in an image, the response might return the confidence score 0.
    /// </summary>
    public class FaceGender
    {
        /// <summary>
        /// Gender identified by the face. For example, `MALE` or `FEMALE`.
        /// </summary>
        /// <value>Gender identified by the face. For example, `MALE` or `FEMALE`.</value>
        [JsonProperty("gender", NullValueHandling = NullValueHandling.Ignore)]
        public string Gender { get; set; }
        /// <summary>
        /// Confidence score for the property in the range of 0 to 1. A higher score indicates greater likelihood that the class is depicted in the image. The default threshold for returning scores from a classifier is 0.5.
        /// </summary>
        /// <value>Confidence score for the property in the range of 0 to 1. A higher score indicates greater likelihood that the class is depicted in the image. The default threshold for returning scores from a classifier is 0.5.</value>
        [JsonProperty("score", NullValueHandling = NullValueHandling.Ignore)]
        public float? Score { get; set; }
    }

}
