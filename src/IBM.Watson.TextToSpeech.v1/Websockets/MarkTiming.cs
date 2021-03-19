using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.Watson.TextToSpeech.v1.Websockets
{
    public class MarkTiming
    {
        [JsonProperty("marks", NullValueHandling = NullValueHandling.Ignore)]
        public List<List<string>> Marks { get; set; }
    }
}
