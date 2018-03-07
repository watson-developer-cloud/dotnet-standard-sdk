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
    /// Classifier and score combination.
    /// </summary>
    public class ClassifierResult
    {
        /// <summary>
        /// Name of the classifier.
        /// </summary>
        /// <value>Name of the classifier.</value>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// The ID of a classifier identified in the image.
        /// </summary>
        /// <value>The ID of a classifier identified in the image.</value>
        [JsonProperty("classifier_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ClassifierId { get; set; }
        /// <summary>
        /// An array of classes within the classifier.
        /// </summary>
        /// <value>An array of classes within the classifier.</value>
        [JsonProperty("classes", NullValueHandling = NullValueHandling.Ignore)]
        public List<ClassResult> Classes { get; set; }
    }

}
