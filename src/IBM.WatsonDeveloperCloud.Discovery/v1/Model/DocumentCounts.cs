

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

    public partial class DocumentCounts
    {
        /// <summary>
        /// Initializes a new instance of the DocumentCounts class.
        /// </summary>
        public DocumentCounts() { }

        /// <summary>
        /// Initializes a new instance of the DocumentCounts class.
        /// </summary>
        /// <param name="available">The total number of available documents in
        /// the collection</param>
        /// <param name="processing">The number of documents in the collection
        /// that are currently being processed</param>
        /// <param name="failed">The number of documents in the collection
        /// that failed to be ingested</param>
        public DocumentCounts(long? available = default(long?), long? processing = default(long?), long? failed = default(long?))
        {
            Available = available;
            Processing = processing;
            Failed = failed;
        }

        /// <summary>
        /// Gets the total number of available documents in the collection
        /// </summary>
        [JsonProperty(PropertyName = "available")]
        public long? Available { get; private set; }

        /// <summary>
        /// Gets the number of documents in the collection that are currently
        /// being processed
        /// </summary>
        [JsonProperty(PropertyName = "processing")]
        public long? Processing { get; private set; }

        /// <summary>
        /// Gets the number of documents in the collection that failed to be
        /// ingested
        /// </summary>
        [JsonProperty(PropertyName = "failed")]
        public long? Failed { get; private set; }

    }
}
