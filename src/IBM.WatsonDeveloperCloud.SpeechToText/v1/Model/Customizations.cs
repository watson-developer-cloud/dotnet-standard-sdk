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

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Model
{
    public partial class Customizations
    {
        /// <summary>
        /// Initializes a new instance of the Customizations class.
        /// </summary>
        public Customizations() { }

        /// <summary>
        /// Gets or sets information about each available custom model. The
        /// array is empty if the user owns no custom models (if no language
        /// is specified) or owns no custom models for the specified language.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "customizations")]
        public IList<Customization> Customization { get; set; }
    }
}