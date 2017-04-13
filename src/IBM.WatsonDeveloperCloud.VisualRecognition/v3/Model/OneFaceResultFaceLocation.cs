/**
* Copyright 2017 IBM Corp. All Rights Reserved.
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
namespace IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model
{
    /// <summary>
    /// OneFaceResultFaceLocation
    /// </summary>
    public class OneFaceResultFaceLocation
    {
        /// <summary>
        /// Width in pixels of face region.
        /// </summary>
        /// <value>Width in pixels of face region.</value>
        [JsonProperty("width")]
        public decimal? Width { get; set; }
        /// <summary>
        /// Height in pixels of face region.
        /// </summary>
        /// <value>Height in pixels of face region.</value>
        [JsonProperty("height")]
        public decimal? Height { get; set; }
        /// <summary>
        /// x-position of top-left pixel of face region.
        /// </summary>
        /// <value>x-position of top-left pixel of face region.</value>
        [JsonProperty("left")]
        public decimal? Left { get; set; }
        /// <summary>
        /// y-position of top-left pixel of face region.
        /// </summary>
        /// <value>y-position of top-left pixel of face region.</value>
        [JsonProperty("top")]
        public decimal? Top { get; set; }
    }
}
