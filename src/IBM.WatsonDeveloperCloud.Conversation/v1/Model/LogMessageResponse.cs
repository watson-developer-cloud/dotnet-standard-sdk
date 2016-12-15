using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Model
{
    /// <summary>
    /// Messages logged with the request.
    /// </summary>
    public class LogMessageResponse
    {
        /// <summary>
        /// An array of dialog node IDs that are in focus in the conversation.
        /// </summary>
        [JsonProperty("level")]
        public string Level { get; set; }

        /// <summary>
        /// The message
        /// </summary>
        [JsonProperty("msg")]
        public string Msg { get; set; }
    }
}
