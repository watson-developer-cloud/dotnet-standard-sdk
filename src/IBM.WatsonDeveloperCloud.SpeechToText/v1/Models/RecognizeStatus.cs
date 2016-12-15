using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Models
{
    public class RecognizeStatus
    {
        [JsonProperty("session")]
        SessionStatus Session { get; set; }
    }
}