

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

    public partial class HtmlSettings
    {
        /// <summary>
        /// Initializes a new instance of the HtmlSettings class.
        /// </summary>
        public HtmlSettings() { }

        /// <summary>
        /// Initializes a new instance of the HtmlSettings class.
        /// </summary>
        public HtmlSettings(System.Collections.Generic.IList<string> excludeTagsCompletely = default(System.Collections.Generic.IList<string>),List<string> excludeTagsKeepContent = default(System.Collections.Generic.IList<string>), XPathPatterns keepContent = default(XPathPatterns), XPathPatterns excludeContent = default(XPathPatterns),List<string> keepTagAttributes = default(System.Collections.Generic.IList<string>),List<string> excludeTagAttributes = default(System.Collections.Generic.IList<string>))
        {
            ExcludeTagsCompletely = excludeTagsCompletely;
            ExcludeTagsKeepContent = excludeTagsKeepContent;
            KeepContent = keepContent;
            ExcludeContent = excludeContent;
            KeepTagAttributes = keepTagAttributes;
            ExcludeTagAttributes = excludeTagAttributes;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "exclude_tags_completely")]
        public List<string> ExcludeTagsCompletely { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "exclude_tags_keep_content")]
        public List<string> ExcludeTagsKeepContent { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "keep_content")]
        public XPathPatterns KeepContent { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "exclude_content")]
        public XPathPatterns ExcludeContent { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "keep_tag_attributes")]
        public List<string> KeepTagAttributes { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "exclude_tag_attributes")]
        public List<string> ExcludeTagAttributes { get; set; }

    }
}
