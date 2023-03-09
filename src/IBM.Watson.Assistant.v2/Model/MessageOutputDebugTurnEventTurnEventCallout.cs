/**
* (C) Copyright IBM Corp. 2023.
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

using Newtonsoft.Json;

namespace IBM.Watson.Assistant.v2.Model
{
    /// <summary>
    /// MessageOutputDebugTurnEventTurnEventCallout.
    /// </summary>
    public class MessageOutputDebugTurnEventTurnEventCallout : MessageOutputDebugTurnEvent
    {
        /// <summary>
        /// The type of turn event.
        /// </summary>
        [JsonProperty("event", NullValueHandling = NullValueHandling.Ignore)]
        public new string _Event
        {
            get { return base._Event; }
            set { base._Event = value; }
        }
        /// <summary>
        /// Gets or Sets Source
        /// </summary>
        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public new TurnEventActionSource Source
        {
            get { return base.Source; }
            set { base.Source = value; }
        }
        /// <summary>
        /// Gets or Sets Callout
        /// </summary>
        [JsonProperty("callout", NullValueHandling = NullValueHandling.Ignore)]
        public new TurnEventCalloutCallout Callout
        {
            get { return base.Callout; }
            set { base.Callout = value; }
        }
        /// <summary>
        /// Gets or Sets Error
        /// </summary>
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public new TurnEventCalloutError Error
        {
            get { return base.Error; }
            set { base.Error = value; }
        }
    }

}
