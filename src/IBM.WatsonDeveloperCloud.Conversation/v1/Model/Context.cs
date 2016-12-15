using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Model
{
    /// <summary>
    /// Context object
    /// </summary>
    public class Context
    {
        /// <summary>
        /// The unique identifier of the conversation.
        /// </summary>
        [JsonProperty("conversation_id")]
        public string ConversationId { get; set; }

        /// <summary>
        /// Information about the dialog.
        /// </summary>
        [JsonProperty("system")]
        public SystemResponse System { get; set; }
    }
}
