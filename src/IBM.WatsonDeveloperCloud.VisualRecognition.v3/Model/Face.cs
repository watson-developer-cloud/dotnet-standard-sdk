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
    /// Provides information about the face.
    /// </summary>
    public class Face
    {
        /// <summary>
        /// Gets or Sets Age
        /// </summary>
        [JsonProperty("age", NullValueHandling = NullValueHandling.Ignore)]
        public FaceAge Age { get; set; }
        /// <summary>
        /// Gets or Sets Gender
        /// </summary>
        [JsonProperty("gender", NullValueHandling = NullValueHandling.Ignore)]
        public FaceGender Gender { get; set; }
        /// <summary>
        /// Gets or Sets FaceLocation
        /// </summary>
        [JsonProperty("face_location", NullValueHandling = NullValueHandling.Ignore)]
        public FaceLocation FaceLocation { get; set; }
        /// <summary>
        /// Gets or Sets Identity
        /// </summary>
        [JsonProperty("identity", NullValueHandling = NullValueHandling.Ignore)]
        public FaceIdentity Identity { get; set; }
    }

}
