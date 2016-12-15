using Newtonsoft.Json;
using System.Collections.Generic;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Model
{
    /// <summary>
    /// Information about the dialog.
    /// </summary>
    public class SystemResponse
    {
        /// <summary>
        /// An array of dialog node IDs that are in focus in the conversation.
        /// </summary>
        [JsonProperty("dialog_stack")]
        public List<string> DialogStack { get; set; }

        /// <summary>
        /// The number of cycles of user input and response in this conversation.
        /// </summary>
        [JsonProperty("dialog_turn_counter")]
        public int DialogTurnCounter { get; set; }

        /// <summary>
        /// The number of inputs in this conversation. This counter might be higher than the dialog_turn_counter counter 
        /// when multiple inputs are needed before a response can be returned.
        /// </summary>
        [JsonProperty("dialog_request_counter")]
        public int DialogRequestCounter { get; set; }
    }
}
