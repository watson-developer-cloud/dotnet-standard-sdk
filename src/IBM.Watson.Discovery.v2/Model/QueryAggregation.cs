/**
* (C) Copyright IBM Corp. 2023.
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
using JsonSubTypes;
using Newtonsoft.Json;

namespace IBM.Watson.Discovery.v2.Model
{
    /// <summary>
    /// An object that defines how to aggregate query results.
    /// Classes which extend this class:
    /// - QueryAggregationQueryTermAggregation
    /// - QueryAggregationQueryGroupByAggregation
    /// - QueryAggregationQueryHistogramAggregation
    /// - QueryAggregationQueryTimesliceAggregation
    /// - QueryAggregationQueryNestedAggregation
    /// - QueryAggregationQueryFilterAggregation
    /// - QueryAggregationQueryCalculationAggregation
    /// - QueryAggregationQueryTopHitsAggregation
    /// - QueryAggregationQueryPairAggregation
    /// - QueryAggregationQueryTrendAggregation
    /// - QueryAggregationQueryTopicAggregation
    /// </summary>
    [JsonConverter(typeof(JsonSubtypes), "type")]
    [JsonSubtypes.KnownSubType(typeof(QueryAggregationQueryTermAggregation), "term")]
    [JsonSubtypes.KnownSubType(typeof(QueryAggregationQueryGroupByAggregation), "group_by")]
    [JsonSubtypes.KnownSubType(typeof(QueryAggregationQueryHistogramAggregation), "histogram")]
    [JsonSubtypes.KnownSubType(typeof(QueryAggregationQueryTimesliceAggregation), "timeslice")]
    [JsonSubtypes.KnownSubType(typeof(QueryAggregationQueryNestedAggregation), "nested")]
    [JsonSubtypes.KnownSubType(typeof(QueryAggregationQueryFilterAggregation), "filter")]
    [JsonSubtypes.KnownSubType(typeof(QueryAggregationQueryCalculationAggregation), "min")]
    [JsonSubtypes.KnownSubType(typeof(QueryAggregationQueryCalculationAggregation), "max")]
    [JsonSubtypes.KnownSubType(typeof(QueryAggregationQueryCalculationAggregation), "sum")]
    [JsonSubtypes.KnownSubType(typeof(QueryAggregationQueryCalculationAggregation), "average")]
    [JsonSubtypes.KnownSubType(typeof(QueryAggregationQueryCalculationAggregation), "unique_count")]
    [JsonSubtypes.KnownSubType(typeof(QueryAggregationQueryTopHitsAggregation), "top_hits")]
    [JsonSubtypes.KnownSubType(typeof(QueryAggregationQueryPairAggregation), "pair")]
    [JsonSubtypes.KnownSubType(typeof(QueryAggregationQueryTrendAggregation), "trend")]
    [JsonSubtypes.KnownSubType(typeof(QueryAggregationQueryTopicAggregation), "topic")]
    public class QueryAggregation
    {
        /// This ctor is protected to prevent instantiation of this base class.
        /// Instead, users should instantiate one of the subclasses listed below:
        /// - QueryAggregationQueryTermAggregation
        /// - QueryAggregationQueryGroupByAggregation
        /// - QueryAggregationQueryHistogramAggregation
        /// - QueryAggregationQueryTimesliceAggregation
        /// - QueryAggregationQueryNestedAggregation
        /// - QueryAggregationQueryFilterAggregation
        /// - QueryAggregationQueryCalculationAggregation
        /// - QueryAggregationQueryTopHitsAggregation
        /// - QueryAggregationQueryPairAggregation
        /// - QueryAggregationQueryTrendAggregation
        /// - QueryAggregationQueryTopicAggregation
        protected QueryAggregation()
        {
        }

        /// <summary>
        /// Specifies that the aggregation type is `term`.
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; protected set; }
        /// <summary>
        /// The field in the document where the values come from.
        /// </summary>
        [JsonProperty("field", NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; protected set; }
        /// <summary>
        /// The number of results returned. Not returned if `relevancy:true` is specified in the request.
        /// </summary>
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public long? Count { get; protected set; }
        /// <summary>
        /// Identifier specified in the query request of this aggregation. Not returned if `relevancy:true` is specified
        /// in the request.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; protected set; }
        /// <summary>
        /// The path to the document field to scope subsequent aggregations to.
        /// </summary>
        [JsonProperty("path", NullValueHandling = NullValueHandling.Ignore)]
        public string Path { get; protected set; }
        /// <summary>
        /// Number of nested documents found in the specified field.
        /// </summary>
        [JsonProperty("matching_results", NullValueHandling = NullValueHandling.Ignore)]
        public long? MatchingResults { get; protected set; }
        /// <summary>
        /// An array of subaggregations.
        /// </summary>
        [JsonProperty("aggregations", NullValueHandling = NullValueHandling.Ignore)]
        public List<Dictionary<string, object>> Aggregations { get; protected set; }
        /// <summary>
        /// The filter that is written in Discovery Query Language syntax and is applied to the documents before
        /// subaggregations are run.
        /// </summary>
        [JsonProperty("match", NullValueHandling = NullValueHandling.Ignore)]
        public string Match { get; protected set; }
        /// <summary>
        /// The value of the calculation.
        /// </summary>
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public double? Value { get; protected set; }
        /// <summary>
        /// The number of documents to return.
        /// </summary>
        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public long? Size { get; protected set; }
        /// <summary>
        /// A query response that contains the matching documents for the preceding aggregations.
        /// </summary>
        [JsonProperty("hits", NullValueHandling = NullValueHandling.Ignore)]
        public QueryTopHitsAggregationResult Hits { get; protected set; }
        /// <summary>
        /// Specifies the first aggregation in the pair. The aggregation must be a `term`, `group_by`, `histogram`, or
        /// `timeslice` aggregation type.
        /// </summary>
        [JsonProperty("first", NullValueHandling = NullValueHandling.Ignore)]
        public string First { get; protected set; }
        /// <summary>
        /// Specifies the second aggregation in the pair. The aggregation must be a `term`, `group_by`, `histogram`, or
        /// `timeslice` aggregation type.
        /// </summary>
        [JsonProperty("second", NullValueHandling = NullValueHandling.Ignore)]
        public string Second { get; protected set; }
        /// <summary>
        /// Indicates whether to include estimated matching result information.
        /// </summary>
        [JsonProperty("show_estimated_matching_results", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ShowEstimatedMatchingResults { get; protected set; }
        /// <summary>
        /// Indicates whether to include total matching documents information.
        /// </summary>
        [JsonProperty("show_total_matching_documents", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ShowTotalMatchingDocuments { get; protected set; }
        /// <summary>
        /// Specifies the `term` or `group_by` aggregation for the facet that you want to analyze.
        /// </summary>
        [JsonProperty("facet", NullValueHandling = NullValueHandling.Ignore)]
        public string Facet { get; protected set; }
        /// <summary>
        /// Specifies the `timeslice` aggregation that defines the time segments.
        /// </summary>
        [JsonProperty("time_segments", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeSegments { get; protected set; }
    }

}
