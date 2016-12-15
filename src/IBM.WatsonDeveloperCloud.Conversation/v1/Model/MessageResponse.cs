using Newtonsoft.Json;
using System.Collections.Generic;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Model
{
    /// <summary>
    /// Returns the last user input, the recognized intents and entities, and the updated context and system output. 
    /// The response can include properties that are added by dialog node output or by the client app.
    /// </summary>
    public class MessageResponse
    {
        /// <summary>
        /// The user input from the request.
        /// </summary>
        [JsonProperty("input")]
        public InputData Input { get; set; }

        /// <summary>
        /// Whether to return more than one intent. Included in the response only.
        /// </summary>
        [JsonProperty("alternate_intents")]
        public bool AlternateIntents { get; set; }

        /// <summary>
        /// A context object that includes state information for the conversation.
        /// </summary>
        [JsonProperty("context")]
        public Context Context { get; set; }

        /// <summary>
        /// An entities object that includes terms from the request that are identified as entities. Returns an empty array 
        /// if no entities are returned.
        /// </summary>
        [JsonProperty("entities")]
        public List<EntityResponse> Entities { get; set; }

        /// <summary>
        /// An array of intent name-confidence pairs for the user input. The list is sorted in descending order of confidence. 
        /// If there are 10 or fewer intents, the sum of the confidence values is 100%. Returns an empty array if no intents are returned. 
        /// </summary>
        [JsonProperty("intents")]
        public List<Intent> Intents { get; set; }

        /// <summary>
        /// An output object that includes the response to the user, the nodes that were hit, and messages from the log.
        /// </summary>
        [JsonProperty("output")]
        public OutputData Output { get; set; }
    }
}
