using Newtonsoft.Json;
using System.Collections.Generic;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Model
{
    /// <summary>
    /// OutputData object
    /// </summary>
    public class OutputData
    {
        /// <summary>
        /// Up to 50 messages logged with the request.
        /// </summary>
        [JsonProperty("log_messages")]
        public List<LogMessageResponse> LogMessages { get; set; }

        /// <summary>
        /// Responses to the user.
        /// </summary>
        [JsonProperty("text")]
        public List<string> Text { get; set; }

        /// <summary>
        /// The nodes that were executed to create the response.
        /// </summary>
        [JsonProperty("nodes_visited")]
        public List<string> NodesVisited { get; set; }
    }
}
