using IBM.WatsonDeveloperCloud.ToneAnalyzer.v3;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IBM.WatsonDeveloperCloud.ToneAnalyzer.IntegrationTests
{
    [TestClass]
    public class ToneAnalyzerServiceIntegrationTest
    {
        [TestMethod]
        public void AnalyzeTone_Success()
        {
            ToneAnalizerService service = new ToneAnalizerService();

            service.Endpoint = "https://watson-api-explorer.mybluemix.net/tone-analyzer/api";
            var results = service.AnalyzeTone("A word is dead when it is said, some say. Emily Dickinson");

            Assert.IsNotNull(results);
            Assert.IsTrue(results.DocumentTone.ToneCategories.Count >= 1);
        }
    }
}
