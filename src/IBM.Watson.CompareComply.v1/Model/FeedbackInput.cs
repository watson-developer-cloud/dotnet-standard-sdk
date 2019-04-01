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

namespace IBM.Watson.CompareComply.v1.Model
{
    /// <summary>
    /// The feedback to be added to an element in the document.
    /// </summary>
    public class FeedbackInput : BaseModel
    {
        /// <summary>
        /// An optional string identifying the user.
        /// </summary>
        [JsonProperty("user_id", NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }
        /// <summary>
        /// An optional comment on or description of the feedback.
        /// </summary>
        [JsonProperty("comment", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; }
        /// <summary>
        /// Feedback data for submission.
        /// </summary>
        [JsonProperty("feedback_data", NullValueHandling = NullValueHandling.Ignore)]
        public FeedbackDataInput FeedbackData { get; set; }
    }

}
