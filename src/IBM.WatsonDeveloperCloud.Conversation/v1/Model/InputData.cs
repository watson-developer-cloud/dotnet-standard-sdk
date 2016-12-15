using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Model
{
    /// <summary>
    /// An input object that includes the input text. 
    /// </summary>
    public class InputData
    {
        /// <summary>
        /// The user's input.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
