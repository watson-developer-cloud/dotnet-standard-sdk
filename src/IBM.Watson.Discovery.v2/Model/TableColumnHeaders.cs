/**
* (C) Copyright IBM Corp. 2018, 2023.
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

namespace IBM.Watson.Discovery.v2.Model
{
    /// <summary>
    /// Column-level cells, each applicable as a header to other cells in the same column as itself, of the current
    /// table.
    /// </summary>
    public class TableColumnHeaders
    {
        /// <summary>
        /// The unique ID of the cell in the current table.
        /// </summary>
        [JsonProperty("cell_id", NullValueHandling = NullValueHandling.Ignore)]
        public string CellId { get; set; }
        /// <summary>
        /// The location of the column header cell in the current table as defined by its `begin` and `end` offsets,
        /// respectfully, in the input document.
        /// </summary>
        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Location { get; set; }
        /// <summary>
        /// The textual contents of this cell from the input document without associated markup content.
        /// </summary>
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }
        /// <summary>
        /// If you provide customization input, the normalized version of the cell text according to the customization;
        /// otherwise, the same value as `text`.
        /// </summary>
        [JsonProperty("text_normalized", NullValueHandling = NullValueHandling.Ignore)]
        public string TextNormalized { get; set; }
        /// <summary>
        /// The `begin` index of this cell's `row` location in the current table.
        /// </summary>
        [JsonProperty("row_index_begin", NullValueHandling = NullValueHandling.Ignore)]
        public long? RowIndexBegin { get; set; }
        /// <summary>
        /// The `end` index of this cell's `row` location in the current table.
        /// </summary>
        [JsonProperty("row_index_end", NullValueHandling = NullValueHandling.Ignore)]
        public long? RowIndexEnd { get; set; }
        /// <summary>
        /// The `begin` index of this cell's `column` location in the current table.
        /// </summary>
        [JsonProperty("column_index_begin", NullValueHandling = NullValueHandling.Ignore)]
        public long? ColumnIndexBegin { get; set; }
        /// <summary>
        /// The `end` index of this cell's `column` location in the current table.
        /// </summary>
        [JsonProperty("column_index_end", NullValueHandling = NullValueHandling.Ignore)]
        public long? ColumnIndexEnd { get; set; }
    }

}
