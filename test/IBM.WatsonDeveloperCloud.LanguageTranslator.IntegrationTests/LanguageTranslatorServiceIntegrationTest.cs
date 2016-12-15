using System.IO;
using IBM.WatsonDeveloperCloud.LanguageTranslator.v2;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.IntegrationTests
{
    [TestClass]
    public class LanguageTranslatorServiceIntegrationTest
    {
        public void GetIdentifiableLanguages_Sucess()
        {
            LanguageTranslatorService service =
                new LanguageTranslatorService();

            service.Endpoint = "https://watson-api-explorer.mybluemix.net/language-translation/api";
            var results = service.GetIdentifiableLanguages();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Languages.Count > 0);
        }

        [TestMethod]
        public void Identify_Sucess()
        {
            LanguageTranslatorService service =
                new LanguageTranslatorService();

            service.Endpoint = "https://watson-api-explorer.mybluemix.net/language-translation/api";
            var results = service.Identify("Hello! How are you?");

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Languages.Count > 0);
        }

        [TestMethod]
        public void Translate_Sucess()
        {
            LanguageTranslatorService service =
                new LanguageTranslatorService();

            service.Endpoint = "https://watson-api-explorer.mybluemix.net/language-translation/api";
            var results = service.Translate("en", "pt", "Hello! How are you?");

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Translations.Count > 0);
        }

        [TestMethod]
        public void LisListModels_Sucess()
        {
            LanguageTranslatorService service =
                new LanguageTranslatorService();

            service.Endpoint = "https://watson-api-explorer.mybluemix.net/language-translation/api";
            var results = service.ListModels(true, "en", string.Empty);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Models.Count > 0);
        }

        [TestMethod]
        public void GetModelDetails()
        {
            LanguageTranslatorService service =
                new LanguageTranslatorService();

            service.Endpoint = "https://watson-api-explorer.mybluemix.net/language-translation/api";
            var results = service.GetModelDetails("en-pt");

            Assert.IsNotNull(results);
            Assert.IsFalse(string.IsNullOrEmpty(results.ModelId));
        }
    }
}