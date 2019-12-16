/**
* (C) Copyright IBM Corp. 2018, 2019.
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


namespace IBM.Watson.NaturalLanguageClassifier.v1.UnitTests
{
    [TestClass]
    public class NaturalLanguageClassifierServiceUnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(httpClient: null);
        }

        [TestMethod]
        public void ConstructorHttpClient()
        {
            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorExternalConfig()
        {
            NaturalLanguageClassifierService service = Substitute.For<NaturalLanguageClassifierService>();
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Constructor()
        {
            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorAuthenticator()
        {
            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(new NoAuthAuthenticator());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorNoUrl()
        {
            var url = System.Environment.GetEnvironmentVariable("NATURAL_LANGUAGE_CLASSIFIER_SERVICE_URL");
            System.Environment.SetEnvironmentVariable("NATURAL_LANGUAGE_CLASSIFIER_SERVICE_URL", null);
            NaturalLanguageClassifierService service = Substitute.For<NaturalLanguageClassifierService>();
            Assert.IsTrue(service.ServiceUrl == "https://gateway.watsonplatform.net/natural-language-classifier/api");
            System.Environment.SetEnvironmentVariable("NATURAL_LANGUAGE_CLASSIFIER_SERVICE_URL", url);
        }
        #endregion

        [TestMethod]
        public void Classify_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(client);

            var classifierId = "classifierId";
            var text = "text";

            var result = service.;

            JObject bodyObject = new JObject();
            if (!string.IsNullOrEmpty(text))
            {
                bodyObject["text"] = JToken.FromObject(text);
            }
            var json = JsonConvert.SerializeObject(bodyObject);
            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
            client.Received().PostAsync($"{service.ServiceUrl}/v1/classifiers/{classifierId}/classify");
        }
        [TestMethod]
        public void ClassifyCollection_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(client);

            var classifierId = "classifierId";
            var collection = new List<ClassifyInput>();

            var result = service.;

            JObject bodyObject = new JObject();
            if (collection != null && collection.Count > 0)
            {
                bodyObject["collection"] = JToken.FromObject(collection);
            }
            var json = JsonConvert.SerializeObject(bodyObject);
            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
            client.Received().PostAsync($"{service.ServiceUrl}/v1/classifiers/{classifierId}/classify_collection");
        }
        [TestMethod]
        public void CreateClassifier_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(client);

            var trainingMetadata = new MemoryStream();
            var trainingData = new MemoryStream();

            var result = service.;

        }
        [TestMethod]
        public void ListClassifiers_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(client);


            var result = service.;

        }
        [TestMethod]
        public void GetClassifier_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(client);

            var classifierId = "classifierId";

            var result = service.;

            client.Received().GetAsync($"{service.ServiceUrl}/v1/classifiers/{classifierId}");
        }
        [TestMethod]
        public void DeleteClassifier_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(client);

            var classifierId = "classifierId";

            var result = service.;

            client.Received().DeleteAsync($"{service.ServiceUrl}/v1/classifiers/{classifierId}");
        }
    }
}
