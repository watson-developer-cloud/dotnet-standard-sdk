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
    /// The list of fetched fields.  The fields are returned using a fully qualified name format, however, the format differs slightly from that used by the query operations.    * Fields which contain nested JSON objects are assigned a type of "nested".    * Fields which belong to a nested object are prefixed with `.properties` (for example, `warnings.properties.severity` means that the `warnings` object has a property called `severity`).    * Fields returned from the News collection are prefixed with `v{N}-fullnews-t3-{YEAR}.mappings` (for example, `v5-fullnews-t3-2016.mappings.text.properties.author`).
    /// </summary>
    public class ListCollectionFieldsResponse
    {
        /// <summary>
        /// An array containing information about each field in the collections.
        /// </summary>
        /// <value>An array containing information about each field in the collections.</value>
        [JsonProperty("fields", NullValueHandling = NullValueHandling.Ignore)]
        public List<Field> Fields { get; set; }
    }

}
