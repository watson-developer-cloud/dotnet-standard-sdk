using Newtonsoft.Json;
using System.Collections.Generic;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Model
{
    /// <summary>
    /// IntentCollectionResponse object
    /// </summary>
    public class IntentCollectionResponse
    {
        /// <summary>
        /// An array of Intent collection ,
        /// </summary>
        [JsonProperty("intents")]
        public List<IntentExportResponse> Intents { get; set; }

        /// <summary>
        /// The pagination of for IntentCollectionResponse.
        /// </summary>
        [JsonProperty("pagination")]
        public PaginationResponse Pagination { get; set; }
    }
}
