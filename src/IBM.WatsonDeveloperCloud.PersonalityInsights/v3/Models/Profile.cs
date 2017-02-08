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

namespace IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Models
{
    public class Profile
    {
        [JsonProperty("processed_language")]
        public string ProcessedLanguage { get; set; }

        [JsonProperty("word_count")]
        public int WordCount { get; set; }

        [JsonProperty("word_count_message")]
        public string WordCountMessage { get; set; }

        [JsonProperty("personality")]
        public List<TraitTreeNode> Personality { get; set; }

        [JsonProperty("values")]
        public List<TraitTreeNode> Values { get; set; }

        [JsonProperty("needs")]
        public List<TraitTreeNode> Needs { get; set; }

        [JsonProperty("behavior")]
        public List<BehaviorNode> Behavior { get; set; }

        [JsonProperty("consumption_preferences")]
        public List<ConsumptionPreferencesCategoryNode> ConsumptionPreferences { get; set; }

        [JsonProperty("warning")]
        public List<Warning> Warning { get; set; }
    }
}