using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.Watson.TextToSpeech.v1.Websockets.Model
{
    public class WordTiming
    {
        [JsonProperty("words", NullValueHandling = NullValueHandling.Ignore)]
        public List<List<string>> Words { get; set; }
       
    }
}
