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
    /// success
    /// </summary>
    public class CreateCollection
    {
        /// <summary>
        /// Gets or Sets CollectionId
        /// </summary>
        [JsonProperty("collection_id")]
        public string CollectionId { get; set; }
        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Gets or Sets Created
        /// </summary>
        [JsonProperty("created")]
        public string Created { get; set; }
        /// <summary>
        /// Gets or Sets Images
        /// </summary>
        [JsonProperty("images")]
        public int? Images { get; set; }
        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
        /// <summary>
        /// Gets or Sets Capacity
        /// </summary>
        [JsonProperty("capacity")]
        public string Capacity { get; set; }
    }
}
