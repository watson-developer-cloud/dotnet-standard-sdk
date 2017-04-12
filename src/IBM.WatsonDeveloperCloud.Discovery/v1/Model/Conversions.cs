

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

    /// <summary>
    /// Document conversion settings
    /// </summary>
    public partial class Conversions
    {
        /// <summary>
        /// Initializes a new instance of the Conversions class.
        /// </summary>
        public Conversions() { }

        /// <summary>
        /// Initializes a new instance of the Conversions class.
        /// </summary>
        /// <param name="pdf">A list of PDF conversion settings</param>
        /// <param name="word">A list of Word conversion settings</param>
        /// <param name="html">A list of HTML conversion settings</param>
        /// <param name="jsonNormalizations">An array of JSON normalization
        /// operations</param>
        public Conversions(PdfSettings pdf = default(PdfSettings), WordSettings word = default(WordSettings), HtmlSettings html = default(HtmlSettings),List<NormalizationOperation> jsonNormalizations = default(System.Collections.Generic.IList<NormalizationOperation>))
        {
            Pdf = pdf;
            Word = word;
            Html = html;
            JsonNormalizations = jsonNormalizations;
        }

        /// <summary>
        /// Gets or sets a list of PDF conversion settings
        /// </summary>
        [JsonProperty(PropertyName = "pdf")]
        public PdfSettings Pdf { get; set; }

        /// <summary>
        /// Gets or sets a list of Word conversion settings
        /// </summary>
        [JsonProperty(PropertyName = "word")]
        public WordSettings Word { get; set; }

        /// <summary>
        /// Gets or sets a list of HTML conversion settings
        /// </summary>
        [JsonProperty(PropertyName = "html")]
        public HtmlSettings Html { get; set; }

        /// <summary>
        /// Gets or sets an array of JSON normalization operations
        /// </summary>
        [JsonProperty(PropertyName = "json_normalizations")]
        public List<NormalizationOperation> JsonNormalizations { get; set; }

    }
}
