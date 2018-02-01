

/**
* Copyright 2017 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 the "License";
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

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    using System.Linq;

    public partial class DocumentSnapshot
    {
        /// <summary>
        /// Initializes a new instance of the DocumentSnapshot class.
        /// </summary>
        public DocumentSnapshot() { }

        /// <summary>
        /// Initializes a new instance of the DocumentSnapshot class.
        /// </summary>
        /// <param name="step">Possible values include: 'html_input',
        /// 'html_output', 'json_output', 'json_normalizations_output',
        /// 'enrichments_output', 'normalizations_output'</param>
        public DocumentSnapshot(string step = default(string), object snapshot = default(object))
        {
            Step = step;
            Snapshot = snapshot;
        }

        /// <summary>
        /// Gets or sets possible values include: 'html_input', 'html_output',
        /// 'json_output', 'json_normalizations_output',
        /// 'enrichments_output', 'normalizations_output'
        /// </summary>
        [JsonProperty(PropertyName = "step")]
        public string Step { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "snapshot")]
        public object Snapshot { get; set; }

    }
}
