

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

    public partial class WordStyle
    {
        /// <summary>
        /// Initializes a new instance of the WordStyle class.
        /// </summary>
        public WordStyle() { }

        /// <summary>
        /// Initializes a new instance of the WordStyle class.
        /// </summary>
        public WordStyle(double? level = default(double?),List<string> names = default(System.Collections.Generic.IList<string>))
        {
            Level = level;
            Names = names;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "level")]
        public double? Level { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "names")]
        public List<string> Names { get; set; }

    }
}
