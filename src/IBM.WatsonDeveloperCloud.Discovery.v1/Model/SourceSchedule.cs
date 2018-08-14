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

using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    /// <summary>
    /// Object containing the schedule information for the source.
    /// </summary>
    public class SourceSchedule : BaseModel
    {
        /// <summary>
        /// The crawl schedule in the specified **time_zone**.
        ///
        /// -  `daily`: Runs every day between 00:00 and 06:00.
        /// -  `weekly`: Runs every week on Sunday between 00:00 and 06:00.
        /// -  `monthly`: Runs the on the first Sunday of every month between 00:00 and 06:00.
        /// </summary>
        /// <value>
        /// The crawl schedule in the specified **time_zone**.
        ///
        /// -  `daily`: Runs every day between 00:00 and 06:00.
        /// -  `weekly`: Runs every week on Sunday between 00:00 and 06:00.
        /// -  `monthly`: Runs the on the first Sunday of every month between 00:00 and 06:00.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum FrequencyEnum
        {
            
            /// <summary>
            /// Enum DAILY for daily
            /// </summary>
            [EnumMember(Value = "daily")]
            DAILY,
            
            /// <summary>
            /// Enum WEEKLY for weekly
            /// </summary>
            [EnumMember(Value = "weekly")]
            WEEKLY,
            
            /// <summary>
            /// Enum MONTHLY for monthly
            /// </summary>
            [EnumMember(Value = "monthly")]
            MONTHLY
        }

        /// <summary>
        /// The crawl schedule in the specified **time_zone**.
        ///
        /// -  `daily`: Runs every day between 00:00 and 06:00.
        /// -  `weekly`: Runs every week on Sunday between 00:00 and 06:00.
        /// -  `monthly`: Runs the on the first Sunday of every month between 00:00 and 06:00.
        /// </summary>
        [JsonProperty("frequency", NullValueHandling = NullValueHandling.Ignore)]
        public FrequencyEnum? Frequency { get; set; }
        /// <summary>
        /// When `true`, the source is re-crawled based on the **frequency** field in this object. When `false` the
        /// source is not re-crawled; When `false` and connecting to Salesforce the source is crawled annually.
        /// </summary>
        [JsonProperty("enabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Enabled { get; set; }
        /// <summary>
        /// The time zone to base source crawl times on. Possible values correspond to the IANA (Internet Assigned
        /// Numbers Authority) time zones list.
        /// </summary>
        [JsonProperty("time_zone", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeZone { get; set; }
    }

}
