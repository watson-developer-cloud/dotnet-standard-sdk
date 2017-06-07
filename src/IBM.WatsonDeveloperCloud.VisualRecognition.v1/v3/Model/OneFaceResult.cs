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

using System.Collections.Generic;
using Newtonsoft.Json;
namespace IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model
{
    /// <summary>
    /// OneFaceResult
    /// </summary>
    public class OneFaceResult
    {
        /// <summary>
        /// Gets or Sets Age
        /// </summary>
        [JsonProperty("age")]
        public OneFaceResultAge Age { get; set; }
        /// <summary>
        /// Gets or Sets Gender
        /// </summary>
        [JsonProperty("gender")]
        public OneFaceResultGender Gender { get; set; }
        /// <summary>
        /// Gets or Sets FaceLocation
        /// </summary>
        [JsonProperty("face_location")]
        public OneFaceResultFaceLocation FaceLocation { get; set; }
        /// <summary>
        /// Gets or Sets Identity
        /// </summary>
        [JsonProperty("identity")]
        public OneFaceResultIdentity Identity { get; set; }
    }
}
