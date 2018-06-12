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
    /// Information about the face.
    /// </summary>
    public class Face : BaseModel
    {
        /// <summary>
        /// Age information about a face.
        /// </summary>
        /// <value>
        /// Age information about a face.
        /// </value>
        [JsonProperty("age", NullValueHandling = NullValueHandling.Ignore)]
        public FaceAge Age { get; set; }
        /// <summary>
        /// Information about the gender of the face.
        /// </summary>
        /// <value>
        /// Information about the gender of the face.
        /// </value>
        [JsonProperty("gender", NullValueHandling = NullValueHandling.Ignore)]
        public FaceGender Gender { get; set; }
        /// <summary>
        /// The location of the bounding box around the face.
        /// </summary>
        /// <value>
        /// The location of the bounding box around the face.
        /// </value>
        [JsonProperty("face_location", NullValueHandling = NullValueHandling.Ignore)]
        public FaceLocation FaceLocation { get; set; }
    }

}
