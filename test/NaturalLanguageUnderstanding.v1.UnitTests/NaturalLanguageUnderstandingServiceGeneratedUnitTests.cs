/**
* (C) Copyright IBM Corp. 2018, 2020.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using IBM.Cloud.SDK.Core.Http;
using NSubstitute;
using System;
using IBM.Watson.NaturalLanguageUnderstanding.v1.Model;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;
using IBM.Cloud.SDK.Core.Authentication.NoAuth;
using System.Threading.Tasks;

namespace IBM.Watson.NaturalLanguageUnderstanding.v1.UnitTests
{
    [TestClass]
    public class NaturalLanguageUnderstandingServiceUnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(httpClient: null);
        }

        [TestMethod]
        public void ConstructorHttpClient()
        {
            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorExternalConfig()
        {
            var apikey = System.Environment.GetEnvironmentVariable("NATURAL_LANGUAGE_UNDERSTANDING_APIKEY");
            var url = System.Environment.GetEnvironmentVariable("NATURAL_LANGUAGE_UNDERSTANDING_URL");
            System.Environment.SetEnvironmentVariable("NATURAL_LANGUAGE_UNDERSTANDING_APIKEY", "apikey");
            System.Environment.SetEnvironmentVariable("NATURAL_LANGUAGE_UNDERSTANDING_URL", "http://www.url.com");
            NaturalLanguageUnderstandingService service = Substitute.For<NaturalLanguageUnderstandingService>("testString");
            Assert.IsNotNull(service);
            System.Environment.SetEnvironmentVariable("NATURAL_LANGUAGE_UNDERSTANDING_URL", url);
            System.Environment.SetEnvironmentVariable("NATURAL_LANGUAGE_UNDERSTANDING_APIKEY", apikey);
        }

        [TestMethod]
        public void Constructor()
        {
            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorNoUrl()
        {
            var apikey = System.Environment.GetEnvironmentVariable("TEST_SERVICE_APIKEY");
            System.Environment.SetEnvironmentVariable("TEST_SERVICE_APIKEY", "apikey");
            NaturalLanguageUnderstandingService service = Substitute.For<NaturalLanguageUnderstandingService>("testString", "test_service");
            Assert.IsTrue(service.ServiceUrl == "https://gateway.watsonplatform.net/natural-language-understanding/api");
            System.Environment.SetEnvironmentVariable("TEST_SERVICE_APIKEY", apikey);
        }
        #endregion

        [TestMethod]
        public void TestCategoriesOptionsModel()
        {

            CategoriesOptions testRequestModel = new CategoriesOptions()
            {
                Explanation = true,
                Limit = 38,
                Model = "testString"
            };

            Assert.IsTrue(testRequestModel.Explanation == true);
            Assert.IsTrue(testRequestModel.Limit == 38);
            Assert.IsTrue(testRequestModel.Model == "testString");
        }

        [TestMethod]
        public void TestConceptsOptionsModel()
        {

            ConceptsOptions testRequestModel = new ConceptsOptions()
            {
                Limit = 38
            };

            Assert.IsTrue(testRequestModel.Limit == 38);
        }

        [TestMethod]
        public void TestEmotionOptionsModel()
        {

            var EmotionOptionsTargets = new List<string> { "testString" };
            EmotionOptions testRequestModel = new EmotionOptions()
            {
                Document = true,
                Targets = EmotionOptionsTargets
            };

            Assert.IsTrue(testRequestModel.Document == true);
            Assert.IsTrue(testRequestModel.Targets == EmotionOptionsTargets);
        }

        [TestMethod]
        public void TestEntitiesOptionsModel()
        {

            EntitiesOptions testRequestModel = new EntitiesOptions()
            {
                Limit = 38,
                Mentions = true,
                Model = "testString",
                Sentiment = true,
                Emotion = true
            };

            Assert.IsTrue(testRequestModel.Limit == 38);
            Assert.IsTrue(testRequestModel.Mentions == true);
            Assert.IsTrue(testRequestModel.Model == "testString");
            Assert.IsTrue(testRequestModel.Sentiment == true);
            Assert.IsTrue(testRequestModel.Emotion == true);
        }

        [TestMethod]
        public void TestFeaturesModel()
        {
            SyntaxOptionsTokens SyntaxOptionsTokensModel = new SyntaxOptionsTokens()
            {
                Lemma = true,
                PartOfSpeech = true
            };

            SyntaxOptions SyntaxOptionsModel = new SyntaxOptions()
            {
                Tokens = SyntaxOptionsTokensModel,
                Sentences = true
            };

            CategoriesOptions CategoriesOptionsModel = new CategoriesOptions()
            {
                Explanation = true,
                Limit = 38,
                Model = "testString"
            };

            var SentimentOptionsTargets = new List<string> { "testString" };
            SentimentOptions SentimentOptionsModel = new SentimentOptions()
            {
                Document = true,
                Targets = SentimentOptionsTargets
            };

            SemanticRolesOptions SemanticRolesOptionsModel = new SemanticRolesOptions()
            {
                Limit = 38,
                Keywords = true,
                Entities = true
            };

            RelationsOptions RelationsOptionsModel = new RelationsOptions()
            {
                Model = "testString"
            };

            MetadataOptions MetadataOptionsModel = new MetadataOptions()
            {
            };

            KeywordsOptions KeywordsOptionsModel = new KeywordsOptions()
            {
                Limit = 38,
                Sentiment = true,
                Emotion = true
            };

            EntitiesOptions EntitiesOptionsModel = new EntitiesOptions()
            {
                Limit = 38,
                Mentions = true,
                Model = "testString",
                Sentiment = true,
                Emotion = true
            };

            var EmotionOptionsTargets = new List<string> { "testString" };
            EmotionOptions EmotionOptionsModel = new EmotionOptions()
            {
                Document = true,
                Targets = EmotionOptionsTargets
            };

            ConceptsOptions ConceptsOptionsModel = new ConceptsOptions()
            {
                Limit = 38
            };

            Features testRequestModel = new Features()
            {
                Concepts = ConceptsOptionsModel,
                Emotion = EmotionOptionsModel,
                Entities = EntitiesOptionsModel,
                Keywords = KeywordsOptionsModel,
                Metadata = MetadataOptionsModel,
                Relations = RelationsOptionsModel,
                SemanticRoles = SemanticRolesOptionsModel,
                Sentiment = SentimentOptionsModel,
                Categories = CategoriesOptionsModel,
                Syntax = SyntaxOptionsModel
            };

            Assert.IsTrue(testRequestModel.Concepts == ConceptsOptionsModel);
            Assert.IsTrue(testRequestModel.Emotion == EmotionOptionsModel);
            Assert.IsTrue(testRequestModel.Entities == EntitiesOptionsModel);
            Assert.IsTrue(testRequestModel.Keywords == KeywordsOptionsModel);
            Assert.IsTrue(testRequestModel.Metadata == MetadataOptionsModel);
            Assert.IsTrue(testRequestModel.Relations == RelationsOptionsModel);
            Assert.IsTrue(testRequestModel.SemanticRoles == SemanticRolesOptionsModel);
            Assert.IsTrue(testRequestModel.Sentiment == SentimentOptionsModel);
            Assert.IsTrue(testRequestModel.Categories == CategoriesOptionsModel);
            Assert.IsTrue(testRequestModel.Syntax == SyntaxOptionsModel);
        }

        [TestMethod]
        public void TestKeywordsOptionsModel()
        {

            KeywordsOptions testRequestModel = new KeywordsOptions()
            {
                Limit = 38,
                Sentiment = true,
                Emotion = true
            };

            Assert.IsTrue(testRequestModel.Limit == 38);
            Assert.IsTrue(testRequestModel.Sentiment == true);
            Assert.IsTrue(testRequestModel.Emotion == true);
        }

        [TestMethod]
        public void TestMetadataOptionsModel()
        {

            MetadataOptions testRequestModel = new MetadataOptions()
            {
            };

        }

        [TestMethod]
        public void TestRelationsOptionsModel()
        {

            RelationsOptions testRequestModel = new RelationsOptions()
            {
                Model = "testString"
            };

            Assert.IsTrue(testRequestModel.Model == "testString");
        }

        [TestMethod]
        public void TestSemanticRolesOptionsModel()
        {

            SemanticRolesOptions testRequestModel = new SemanticRolesOptions()
            {
                Limit = 38,
                Keywords = true,
                Entities = true
            };

            Assert.IsTrue(testRequestModel.Limit == 38);
            Assert.IsTrue(testRequestModel.Keywords == true);
            Assert.IsTrue(testRequestModel.Entities == true);
        }

        [TestMethod]
        public void TestSentimentOptionsModel()
        {

            var SentimentOptionsTargets = new List<string> { "testString" };
            SentimentOptions testRequestModel = new SentimentOptions()
            {
                Document = true,
                Targets = SentimentOptionsTargets
            };

            Assert.IsTrue(testRequestModel.Document == true);
            Assert.IsTrue(testRequestModel.Targets == SentimentOptionsTargets);
        }

        [TestMethod]
        public void TestSyntaxOptionsModel()
        {
            SyntaxOptionsTokens SyntaxOptionsTokensModel = new SyntaxOptionsTokens()
            {
                Lemma = true,
                PartOfSpeech = true
            };

            SyntaxOptions testRequestModel = new SyntaxOptions()
            {
                Tokens = SyntaxOptionsTokensModel,
                Sentences = true
            };

            Assert.IsTrue(testRequestModel.Tokens == SyntaxOptionsTokensModel);
            Assert.IsTrue(testRequestModel.Sentences == true);
        }

        [TestMethod]
        public void TestSyntaxOptionsTokensModel()
        {

            SyntaxOptionsTokens testRequestModel = new SyntaxOptionsTokens()
            {
                Lemma = true,
                PartOfSpeech = true
            };

            Assert.IsTrue(testRequestModel.Lemma == true);
            Assert.IsTrue(testRequestModel.PartOfSpeech == true);
        }

        [TestMethod]
        public void TestTestAnalyzeAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'language': 'Language', 'analyzed_text': 'AnalyzedText', 'retrieved_url': 'RetrievedUrl', 'usage': {'features': 8, 'text_characters': 14, 'text_units': 9}, 'concepts': [{'text': 'Text', 'relevance': 9, 'dbpedia_resource': 'DbpediaResource'}], 'entities': [{'type': 'Type', 'text': 'Text', 'relevance': 9, 'confidence': 10, 'mentions': [{'text': 'Text', 'location': [8], 'confidence': 10}], 'count': 5, 'emotion': {'anger': 5, 'disgust': 7, 'fear': 4, 'joy': 3, 'sadness': 7}, 'sentiment': {'score': 5}, 'disambiguation': {'name': 'Name', 'dbpedia_resource': 'DbpediaResource', 'subtype': ['Subtype']}}], 'keywords': [{'count': 5, 'relevance': 9, 'text': 'Text', 'emotion': {'anger': 5, 'disgust': 7, 'fear': 4, 'joy': 3, 'sadness': 7}, 'sentiment': {'score': 5}}], 'categories': [{'label': 'Label', 'score': 5, 'explanation': {'relevant_text': [{'text': 'Text'}]}}], 'emotion': {'document': {'emotion': {'anger': 5, 'disgust': 7, 'fear': 4, 'joy': 3, 'sadness': 7}}, 'targets': [{'text': 'Text', 'emotion': {'anger': 5, 'disgust': 7, 'fear': 4, 'joy': 3, 'sadness': 7}}]}, 'metadata': {'authors': [{'name': 'Name'}], 'publication_date': 'PublicationDate', 'title': 'Title', 'image': 'Image', 'feeds': [{'link': 'Link'}]}, 'relations': [{'score': 5, 'sentence': 'Sentence', 'type': 'Type', 'arguments': [{'entities': [{'text': 'Text', 'type': 'Type'}], 'location': [8], 'text': 'Text'}]}], 'semantic_roles': [{'sentence': 'Sentence', 'subject': {'text': 'Text', 'entities': [{'type': 'Type', 'text': 'Text'}], 'keywords': [{'text': 'Text'}]}, 'action': {'text': 'Text', 'normalized': 'Normalized', 'verb': {'text': 'Text', 'tense': 'Tense'}}, 'object': {'text': 'Text', 'keywords': [{'text': 'Text'}]}}], 'sentiment': {'document': {'label': 'Label', 'score': 5}, 'targets': [{'text': 'Text', 'score': 5}]}, 'syntax': {'tokens': [{'text': 'Text', 'part_of_speech': 'ADJ', 'location': [8], 'lemma': 'Lemma'}], 'sentences': [{'text': 'Text', 'location': [8]}]}}";
            var response = new DetailedResponse<AnalysisResults>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<AnalysisResults>(responseJson),
                StatusCode = 200
            };

            SyntaxOptionsTokens SyntaxOptionsTokensModel = new SyntaxOptionsTokens()
            {
                Lemma = true,
                PartOfSpeech = true
            };

            SyntaxOptions SyntaxOptionsModel = new SyntaxOptions()
            {
                Tokens = SyntaxOptionsTokensModel,
                Sentences = true
            };

            CategoriesOptions CategoriesOptionsModel = new CategoriesOptions()
            {
                Explanation = true,
                Limit = 38,
                Model = "testString"
            };

            var SentimentOptionsTargets = new List<string> { "testString" };
            SentimentOptions SentimentOptionsModel = new SentimentOptions()
            {
                Document = true,
                Targets = SentimentOptionsTargets
            };

            SemanticRolesOptions SemanticRolesOptionsModel = new SemanticRolesOptions()
            {
                Limit = 38,
                Keywords = true,
                Entities = true
            };

            RelationsOptions RelationsOptionsModel = new RelationsOptions()
            {
                Model = "testString"
            };

            MetadataOptions MetadataOptionsModel = new MetadataOptions()
            {
            };

            KeywordsOptions KeywordsOptionsModel = new KeywordsOptions()
            {
                Limit = 38,
                Sentiment = true,
                Emotion = true
            };

            EntitiesOptions EntitiesOptionsModel = new EntitiesOptions()
            {
                Limit = 38,
                Mentions = true,
                Model = "testString",
                Sentiment = true,
                Emotion = true
            };

            var EmotionOptionsTargets = new List<string> { "testString" };
            EmotionOptions EmotionOptionsModel = new EmotionOptions()
            {
                Document = true,
                Targets = EmotionOptionsTargets
            };

            ConceptsOptions ConceptsOptionsModel = new ConceptsOptions()
            {
                Limit = 38
            };

            Features FeaturesModel = new Features()
            {
                Concepts = ConceptsOptionsModel,
                Emotion = EmotionOptionsModel,
                Entities = EntitiesOptionsModel,
                Keywords = KeywordsOptionsModel,
                Metadata = MetadataOptionsModel,
                Relations = RelationsOptionsModel,
                SemanticRoles = SemanticRolesOptionsModel,
                Sentiment = SentimentOptionsModel,
                Categories = CategoriesOptionsModel,
                Syntax = SyntaxOptionsModel
            };
            Features features = FeaturesModel;
            string text = "testString";
            string html = "testString";
            string url = "testString";
            bool? clean = true;
            string xpath = "testString";
            bool? fallbackToRaw = true;
            bool? returnAnalyzedText = true;
            string language = "testString";
            long? limitTextCharacters = 38;

            request.As<AnalysisResults>().Returns(Task.FromResult(response));

            var result = service.Analyze(features: features, text: text, html: html, url: url, clean: clean, xpath: xpath, fallbackToRaw: fallbackToRaw, returnAnalyzedText: returnAnalyzedText, language: language, limitTextCharacters: limitTextCharacters);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v1/analyze";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListModelsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'models': [{'status': 'Status', 'model_id': 'ModelId', 'language': 'Language', 'description': 'Description', 'workspace_id': 'WorkspaceId', 'version': 'Version', 'version_description': 'VersionDescription'}]}";
            var response = new DetailedResponse<ListModelsResults>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<ListModelsResults>(responseJson),
                StatusCode = 200
            };


            request.As<ListModelsResults>().Returns(Task.FromResult(response));

            var result = service.ListModels();

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v1/models";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteModelAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'deleted': 'Deleted'}";
            var response = new DetailedResponse<DeleteModelResults>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<DeleteModelResults>(responseJson),
                StatusCode = 200
            };

            string modelId = "testString";

            request.As<DeleteModelResults>().Returns(Task.FromResult(response));

            var result = service.DeleteModel(modelId: modelId);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v1/models/{modelId}";
            client.Received().DeleteAsync(messageUrl);
        }

    }
}
