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
    /// List of similar images. Maximum response size 200 kB.
    /// </summary>
    public class SimilarImagesConfig
    {
        /// <summary>
        /// Gets or Sets SimilarImages
        /// </summary>
        [JsonProperty("similar_images")]
        public List<SimilarImageConfig> SimilarImages { get; set; }
        /// <summary>
        /// The number of images processed in this call.
        /// </summary>
        /// <value>The number of images processed in this call.</value>
        [JsonProperty("images_processed")]
        public decimal? ImagesProcessed { get; set; }
    }
}
