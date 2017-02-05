using IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Models;

namespace IBM.WatsonDeveloperCloud.PersonalityInsights.v3
{
    public interface IPersonalityInsightsService
    {
        /// <summary>
        /// Generates a personality profile for the author of the input text. The service accepts a maximum of 20 MB of input content. It can analyze text in Arabic, English, Japanese, or Spanish and return its results in a variety of languages. You can provide plain text, HTML, or JSON input. The service returns output in JSON format by default, but you can request the output in CSV format.
        /// </summary>
        Profile GetProfile(ProfileOptions options);
    }
}