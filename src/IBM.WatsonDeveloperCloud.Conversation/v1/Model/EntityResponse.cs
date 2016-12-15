using Newtonsoft.Json;
using System.Collections.Generic;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Model
{
    /// <summary>
    /// Terms from the request that are identified as entities.
    /// </summary>
    public class EntityResponse
    {
        /// <summary>
        /// The recognized entity from a term in the input.
        /// </summary>
        [JsonProperty("entity")]
        public string Entity { get; set; }

        /// <summary>
        /// Zero-based character offsets that indicate where the entity value begins and ends in the input text.
        /// </summary>
        [JsonProperty("location")]
        public List<int> Location { get; set; }

        /// <summary>
        /// The term in the input text that was recognized
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
