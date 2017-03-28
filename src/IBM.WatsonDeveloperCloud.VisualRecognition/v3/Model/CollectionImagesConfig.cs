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
    /// Success
    /// </summary>
    public class CollectionImagesConfig
    {
        /// <summary>
        /// The unique ID of the image. Save this to add or remove it from the collection
        /// </summary>
        /// <value>The unique ID of the image. Save this to add or remove it from the collection</value>
        [JsonProperty("image_id")]
        public string ImageId { get; set; }
        /// <summary>
        /// Date the image was added to the collection
        /// </summary>
        /// <value>Date the image was added to the collection</value>
        [JsonProperty("created")]
        public string Created { get; set; }
        /// <summary>
        /// File name of the image
        /// </summary>
        /// <value>File name of the image</value>
        [JsonProperty("image_file")]
        public string ImageFile { get; set; }
        /// <summary>
        /// Metadat JSON object (key value pairs)
        /// </summary>
        /// <value>Metadata JSON object (key value pairs)</value>
        [JsonProperty("metadata")]
        public Dictionary<string, string> Metadata { get; set; }
    }
}
