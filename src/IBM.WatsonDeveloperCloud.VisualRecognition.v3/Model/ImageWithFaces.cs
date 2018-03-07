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

using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model
{
    /// <summary>
    /// ImageWithFaces.
    /// </summary>
    public class ImageWithFaces
    {
        /// <summary>
        /// An array of the faces detected in the images.
        /// </summary>
        /// <value>An array of the faces detected in the images.</value>
        [JsonProperty("faces", NullValueHandling = NullValueHandling.Ignore)]
        public List<Face> Faces { get; set; }
        /// <summary>
        /// Relative path of the image file if uploaded directly. Not returned when the image is passed by URL.
        /// </summary>
        /// <value>Relative path of the image file if uploaded directly. Not returned when the image is passed by URL.</value>
        [JsonProperty("image", NullValueHandling = NullValueHandling.Ignore)]
        public string Image { get; set; }
        /// <summary>
        /// Source of the image before any redirects. Not returned when the image is uploaded.
        /// </summary>
        /// <value>Source of the image before any redirects. Not returned when the image is uploaded.</value>
        [JsonProperty("source_url", NullValueHandling = NullValueHandling.Ignore)]
        public string SourceUrl { get; set; }
        /// <summary>
        /// Fully resolved URL of the image after redirects are followed. Not returned when the image is uploaded.
        /// </summary>
        /// <value>Fully resolved URL of the image after redirects are followed. Not returned when the image is uploaded.</value>
        [JsonProperty("resolved_url", NullValueHandling = NullValueHandling.Ignore)]
        public string ResolvedUrl { get; set; }
        /// <summary>
        /// Gets or Sets Error
        /// </summary>
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public ErrorInfo Error { get; set; }
    }

}
