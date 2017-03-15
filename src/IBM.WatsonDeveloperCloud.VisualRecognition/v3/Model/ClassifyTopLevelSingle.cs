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
    /// Classifier results for one image.
    /// </summary>
    public class ClassifyTopLevelSingle
    {
        /// <summary>
        /// Source image URL. Omitted if the image was uploaded. This is the URL as given to the service, before any redirects.
        /// </summary>
        [JsonProperty("source_url")]
        public string SourceUrl { get; set; }
        /// <summary>
        /// Fully-resolved image URL. Omitted if the image was uploaded. Redirects are followed, for example with URL shorteners.
        /// </summary>
        [JsonProperty("resolved_url")]
        public string ResolvedUrl { get; set; }
        /// <summary>
        /// Relative path of the image file if uploaded directly (e.g., inside a zip file). Omitted if image was passed by URL.
        /// </summary>
        [JsonProperty("image")]
        public string Image { get; set; }
        /// <summary>
        /// Information about something that went wrong. Omitted if no error.
        /// </summary>
        [JsonProperty("error")]
        public ErrorInfoNoCode Error { get; set; }
        /// <summary>
        /// Gets or Sets Classifiers
        /// </summary>
        [JsonProperty("classifiers")]
        public List<ClassifyPerClassifier> Classifiers { get; set; }
    }
}
