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
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    /// <summary>
    /// Document conversion settings.
    /// </summary>
    public class Conversions
    {
        /// <summary>
        /// A list of PDF conversion settings.
        /// </summary>
        /// <value>A list of PDF conversion settings.</value>
        [JsonProperty("pdf", NullValueHandling = NullValueHandling.Ignore)]
        public PdfSettings Pdf { get; set; }
        /// <summary>
        /// A list of Word conversion settings.
        /// </summary>
        /// <value>A list of Word conversion settings.</value>
        [JsonProperty("word", NullValueHandling = NullValueHandling.Ignore)]
        public WordSettings Word { get; set; }
        /// <summary>
        /// A list of HTML conversion settings.
        /// </summary>
        /// <value>A list of HTML conversion settings.</value>
        [JsonProperty("html", NullValueHandling = NullValueHandling.Ignore)]
        public HtmlSettings Html { get; set; }
        /// <summary>
        /// Defines operations that can be used to transform the final output JSON into a normalized form. Operations are executed in the order that they appear in the array.
        /// </summary>
        /// <value>Defines operations that can be used to transform the final output JSON into a normalized form. Operations are executed in the order that they appear in the array.</value>
        [JsonProperty("json_normalizations", NullValueHandling = NullValueHandling.Ignore)]
        public List<NormalizationOperation> JsonNormalizations { get; set; }
    }

}
