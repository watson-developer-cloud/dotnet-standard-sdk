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
    /// Only included if identified celebrity, otherwise set to \&quot;\&quot;.
    /// </summary>
    public class OneFaceResultIdentity
    {
        /// <summary>
        /// Name of the person.
        /// </summary>
        /// <value>Name of the person.</value>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Confidence score, on a scale of 0.0 to 1.0.
        /// </summary>
        [JsonProperty("score")]
        public float Score { get; set; }
        /// <summary>
        /// Type hierarchy, e.g. 'People/Leaders/Presidents/USA/Barack Obama'. Only included if found.
        /// </summary>
        [JsonProperty("type_hierarchy")]
        public string TypeHierarchy { get; set; }
    }
}
