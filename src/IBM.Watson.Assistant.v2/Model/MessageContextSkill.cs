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

using IBM.Cloud.SDK.Core;
using Newtonsoft.Json;

namespace IBM.Watson.Assistant.v2.Model
{
    /// <summary>
    /// Contains information specific to a particular skill within the Assistant.
    /// </summary>
    public class MessageContextSkill : BaseModel
    {
        /// <summary>
        /// Arbitrary variables that can be read and written to by a particular skill within the Assistant.
        /// </summary>
        [JsonProperty("user_defined", NullValueHandling = NullValueHandling.Ignore)]
        public string UserDefined { get; set; }
    }

}
