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
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Model
{
    /// <summary>
    /// RecognitionJob.
    /// </summary>
    public class RecognitionJob
    {
        /// <summary>
        /// The current status of the job: * `waiting`: The service is preparing the job for processing. The service returns this status when the job is initially created or when it is waiting for capacity to process the job. The job remains in this state until the service has the capacity to begin processing it. * `processing`: The service is actively processing the job. * `completed`: The service has finished processing the job. If the job specified a callback URL and the event `recognitions.completed_with_results`, the service sent the results with the callback notification; otherwise, you must retrieve the results by checking the individual job. * `failed`: The job failed.
        /// </summary>
        /// <value>The current status of the job: * `waiting`: The service is preparing the job for processing. The service returns this status when the job is initially created or when it is waiting for capacity to process the job. The job remains in this state until the service has the capacity to begin processing it. * `processing`: The service is actively processing the job. * `completed`: The service has finished processing the job. If the job specified a callback URL and the event `recognitions.completed_with_results`, the service sent the results with the callback notification; otherwise, you must retrieve the results by checking the individual job. * `failed`: The job failed.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            
            /// <summary>
            /// Enum WAITING for waiting
            /// </summary>
            [EnumMember(Value = "waiting")]
            WAITING,
            
            /// <summary>
            /// Enum PROCESSING for processing
            /// </summary>
            [EnumMember(Value = "processing")]
            PROCESSING,
            
            /// <summary>
            /// Enum COMPLETED for completed
            /// </summary>
            [EnumMember(Value = "completed")]
            COMPLETED,
            
            /// <summary>
            /// Enum FAILED for failed
            /// </summary>
            [EnumMember(Value = "failed")]
            FAILED
        }

        /// <summary>
        /// The current status of the job: * `waiting`: The service is preparing the job for processing. The service returns this status when the job is initially created or when it is waiting for capacity to process the job. The job remains in this state until the service has the capacity to begin processing it. * `processing`: The service is actively processing the job. * `completed`: The service has finished processing the job. If the job specified a callback URL and the event `recognitions.completed_with_results`, the service sent the results with the callback notification; otherwise, you must retrieve the results by checking the individual job. * `failed`: The job failed.
        /// </summary>
        /// <value>The current status of the job: * `waiting`: The service is preparing the job for processing. The service returns this status when the job is initially created or when it is waiting for capacity to process the job. The job remains in this state until the service has the capacity to begin processing it. * `processing`: The service is actively processing the job. * `completed`: The service has finished processing the job. If the job specified a callback URL and the event `recognitions.completed_with_results`, the service sent the results with the callback notification; otherwise, you must retrieve the results by checking the individual job. * `failed`: The job failed.</value>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// The ID of the job.
        /// </summary>
        /// <value>The ID of the job.</value>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        /// <summary>
        /// The date and time in Coordinated Universal Time (UTC) at which the job was created. The value is provided in full ISO 8601 format (`YYYY-MM-DDThh:mm:ss.sTZD`).
        /// </summary>
        /// <value>The date and time in Coordinated Universal Time (UTC) at which the job was created. The value is provided in full ISO 8601 format (`YYYY-MM-DDThh:mm:ss.sTZD`).</value>
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public string Created { get; set; }
        /// <summary>
        /// The date and time in Coordinated Universal Time (UTC) at which the job was last updated by the service. The value is provided in full ISO 8601 format (`YYYY-MM-DDThh:mm:ss.sTZD`). **Note:** This field is returned only when you list information about a specific or all existing jobs.
        /// </summary>
        /// <value>The date and time in Coordinated Universal Time (UTC) at which the job was last updated by the service. The value is provided in full ISO 8601 format (`YYYY-MM-DDThh:mm:ss.sTZD`). **Note:** This field is returned only when you list information about a specific or all existing jobs.</value>
        [JsonProperty("updated", NullValueHandling = NullValueHandling.Ignore)]
        public string Updated { get; set; }
        /// <summary>
        /// The URL to use to request information about the job with the `GET /v1/recognitions/{id}` method. **Note:** This field is returned only when you create a new job.
        /// </summary>
        /// <value>The URL to use to request information about the job with the `GET /v1/recognitions/{id}` method. **Note:** This field is returned only when you create a new job.</value>
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
        /// <summary>
        /// The user token associated with a job that was created with a callback URL and a user token. **Note:** This field can be returned only when you list information about all existing jobs.
        /// </summary>
        /// <value>The user token associated with a job that was created with a callback URL and a user token. **Note:** This field can be returned only when you list information about all existing jobs.</value>
        [JsonProperty("user_token", NullValueHandling = NullValueHandling.Ignore)]
        public string UserToken { get; set; }
        /// <summary>
        /// If the status is `completed`, the results of the recognition request as an array that includes a single instance of a `SpeechRecognitionResults` object. **Note:** This field can be returned only when you list information about a specific existing job.
        /// </summary>
        /// <value>If the status is `completed`, the results of the recognition request as an array that includes a single instance of a `SpeechRecognitionResults` object. **Note:** This field can be returned only when you list information about a specific existing job.</value>
        [JsonProperty("results", NullValueHandling = NullValueHandling.Ignore)]
        public List<SpeechRecognitionResults> Results { get; set; }
        /// <summary>
        /// An array of warning messages about invalid query parameters included with the request. Each warning includes a descriptive message and a list of invalid argument strings, for example, `"unexpected query parameter 'user_token', query parameter 'callback_url' was not specified"`. The request succeeds despite the warnings. **Note:** This field can be returned only when you create a new job.
        /// </summary>
        /// <value>An array of warning messages about invalid query parameters included with the request. Each warning includes a descriptive message and a list of invalid argument strings, for example, `"unexpected query parameter 'user_token', query parameter 'callback_url' was not specified"`. The request succeeds despite the warnings. **Note:** This field can be returned only when you create a new job.</value>
        [JsonProperty("warnings", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Warnings { get; set; }
    }

}
