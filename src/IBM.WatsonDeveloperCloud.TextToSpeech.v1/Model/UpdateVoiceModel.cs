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

namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model
{
    /// <summary>
    /// UpdateVoiceModel.
    /// </summary>
    public class UpdateVoiceModel
    {
        /// <summary>
        /// A new name for the custom voice model.
        /// </summary>
        /// <value>A new name for the custom voice model.</value>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// A new description for the custom voice model.
        /// </summary>
        /// <value>A new description for the custom voice model.</value>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// An array of words and their translations that are to be added or updated for the custom voice model. Pass an empty array to make no additions or updates.
        /// </summary>
        /// <value>An array of words and their translations that are to be added or updated for the custom voice model. Pass an empty array to make no additions or updates.</value>
        [JsonProperty("words", NullValueHandling = NullValueHandling.Ignore)]
        public List<Word> Words { get; set; }
    }

}
