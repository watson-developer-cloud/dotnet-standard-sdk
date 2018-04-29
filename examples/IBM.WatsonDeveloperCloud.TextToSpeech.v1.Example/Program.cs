using System;
using System.Threading.Tasks;
using IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model;
using IBM.WatsonDeveloperCloud.Util;
using Newtonsoft.Json.Linq;

namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1.Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string credentials = string.Empty;

            try
            {
                credentials = Utility.SimpleGet(
                    Environment.GetEnvironmentVariable("VCAP_URL"),
                    Environment.GetEnvironmentVariable("VCAP_USERNAME"),
                    Environment.GetEnvironmentVariable("VCAP_PASSWORD")).Result;
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Failed to get credentials: {0}", e.Message));
            }

            Task.WaitAll();

            var vcapServices = JObject.Parse(credentials);
            var _url = vcapServices["text_to_speech"]["url"].Value<string>();
            var _username = vcapServices["text_to_speech"]["username"].Value<string>();
            var _password = vcapServices["text_to_speech"]["password"].Value<string>();
            string _synthesizeText = "Hello, welcome to the Watson dotnet SDK!";

            Text synthesizeText = new Text
            {
                _Text = _synthesizeText
            };

            var objTextSpeech = new TextToSpeechService(_username, _password);
            objTextSpeech.Endpoint = _url;

            // MemoryStream Result with .wav data
            var synthesizeResult = objTextSpeech.Synthesize(synthesizeText, "audio/wav");
            Console.ReadKey();
        }
    }
}
