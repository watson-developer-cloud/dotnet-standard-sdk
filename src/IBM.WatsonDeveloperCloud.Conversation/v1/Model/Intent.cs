using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Model
{
    /// <summary>
    /// Terms from the request that are identified as intents. 
    /// </summary>
    public class Intent
    {
        /// <summary>
        /// The name of the recognized intent.
        /// </summary>
        [JsonProperty("intent")]
        public string IntentDescription { get; set; }

        /// <summary>
        /// A decimal percentage that represents the confidence that Watson has in this intent.
        /// </summary>
        [JsonProperty("confidence")]
        public double Confidence { get; set; }
    }
}
