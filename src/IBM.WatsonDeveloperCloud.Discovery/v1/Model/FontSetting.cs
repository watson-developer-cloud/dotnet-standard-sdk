

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

    public partial class FontSetting
    {
        /// <summary>
        /// Initializes a new instance of the FontSetting class.
        /// </summary>
        public FontSetting() { }

        /// <summary>
        /// Initializes a new instance of the FontSetting class.
        /// </summary>
        public FontSetting(double? level = default(double?), double? minSize = default(double?), double? maxSize = default(double?), bool? bold = default(bool?), bool? italic = default(bool?), string name = default(string))
        {
            Level = level;
            MinSize = minSize;
            MaxSize = maxSize;
            Bold = bold;
            Italic = italic;
            Name = name;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "level")]
        public double? Level { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "min_size")]
        public double? MinSize { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "max_size")]
        public double? MaxSize { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bold")]
        public bool? Bold { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "italic")]
        public bool? Italic { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

    }
}
