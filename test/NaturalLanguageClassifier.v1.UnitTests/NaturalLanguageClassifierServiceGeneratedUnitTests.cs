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
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;
using IBM.Cloud.SDK.Core.Authentication.NoAuth;
using System.Threading.Tasks;
using IBM.Watson.NaturalLanguageClassifier.v1.Model;

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
            var apikey = System.Environment.GetEnvironmentVariable("NATURAL_LANGUAGE_CLASSIFIER_APIKEY");
            var url = System.Environment.GetEnvironmentVariable("NATURAL_LANGUAGE_CLASSIFIER_URL");
            System.Environment.SetEnvironmentVariable("NATURAL_LANGUAGE_CLASSIFIER_APIKEY", "apikey");
            System.Environment.SetEnvironmentVariable("NATURAL_LANGUAGE_CLASSIFIER_URL", "http://www.url.com");
            NaturalLanguageClassifierService service = Substitute.For<NaturalLanguageClassifierService>();
            Assert.IsNotNull(service);
            System.Environment.SetEnvironmentVariable("NATURAL_LANGUAGE_CLASSIFIER_URL", url);
            System.Environment.SetEnvironmentVariable("NATURAL_LANGUAGE_CLASSIFIER_APIKEY", apikey);
        }

        [TestMethod]
        public void Constructor()
        {
            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorNoUrl()
        {
            var apikey = System.Environment.GetEnvironmentVariable("TEST_SERVICE_APIKEY");
            System.Environment.SetEnvironmentVariable("TEST_SERVICE_APIKEY", "apikey");
            NaturalLanguageClassifierService service = Substitute.For<NaturalLanguageClassifierService>("test_service");
            Assert.IsTrue(service.ServiceUrl == "https://gateway.watsonplatform.net/natural-language-classifier/api");
            System.Environment.SetEnvironmentVariable("TEST_SERVICE_APIKEY", apikey);
        }
        #endregion

        [TestMethod]
        public void TestClassifyInputModel()
        {

            ClassifyInput testRequestModel = new ClassifyInput()
            {
                Text = "testString"
            };

            Assert.IsTrue(testRequestModel.Text == "testString");
        }

        [TestMethod]
        public void TestTestClassifyAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(client);
            var responseJson = "{'classifier_id': 'ClassifierId', 'url': 'Url', 'text': 'Text', 'top_class': 'TopClass', 'classes': [{'confidence': 10, 'class_name': 'ClassName'}]}";
            var response = new DetailedResponse<Classification>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Classification>(responseJson),
                StatusCode = 200
            };

            string classifierId = "testString";
            string text = "testString";

            request.As<Classification>().Returns(Task.FromResult(response));

            var result = service.Classify(classifierId: classifierId, text: text);


            string messageUrl = $"{service.ServiceUrl}/v1/classifiers/{classifierId}/classify";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestClassifyCollectionAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(client);
            var responseJson = "{'classifier_id': 'ClassifierId', 'url': 'Url', 'collection': [{'text': 'Text', 'top_class': 'TopClass', 'classes': [{'confidence': 10, 'class_name': 'ClassName'}]}]}";
            var response = new DetailedResponse<ClassificationCollection>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<ClassificationCollection>(responseJson),
                StatusCode = 200
            };

            ClassifyInput ClassifyInputModel = new ClassifyInput()
            {
                Text = "testString"
            };
            string classifierId = "testString";
            List<ClassifyInput> collection = new List<ClassifyInput> { ClassifyInputModel };

            request.As<ClassificationCollection>().Returns(Task.FromResult(response));

            var result = service.ClassifyCollection(classifierId: classifierId, collection: collection);


            string messageUrl = $"{service.ServiceUrl}/v1/classifiers/{classifierId}/classify_collection";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestCreateClassifierAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(client);
            var responseJson = "{'name': 'Name', 'url': 'Url', 'status': 'Non Existent', 'classifier_id': 'ClassifierId', 'status_description': 'StatusDescription', 'language': 'Language'}";
            var response = new DetailedResponse<Classifier>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Classifier>(responseJson),
                StatusCode = 200
            };

            System.IO.MemoryStream trainingMetadata = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file."));
            System.IO.MemoryStream trainingData = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file."));

            request.As<Classifier>().Returns(Task.FromResult(response));

            var result = service.CreateClassifier(trainingMetadata: trainingMetadata, trainingData: trainingData);


            string messageUrl = $"{service.ServiceUrl}/v1/classifiers";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListClassifiersAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(client);
            var responseJson = "{'classifiers': [{'name': 'Name', 'url': 'Url', 'status': 'Non Existent', 'classifier_id': 'ClassifierId', 'status_description': 'StatusDescription', 'language': 'Language'}]}";
            var response = new DetailedResponse<ClassifierList>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<ClassifierList>(responseJson),
                StatusCode = 200
            };


            request.As<ClassifierList>().Returns(Task.FromResult(response));

            var result = service.ListClassifiers();


            string messageUrl = $"{service.ServiceUrl}/v1/classifiers";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetClassifierAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(client);
            var responseJson = "{'name': 'Name', 'url': 'Url', 'status': 'Non Existent', 'classifier_id': 'ClassifierId', 'status_description': 'StatusDescription', 'language': 'Language'}";
            var response = new DetailedResponse<Classifier>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Classifier>(responseJson),
                StatusCode = 200
            };

            string classifierId = "testString";

            request.As<Classifier>().Returns(Task.FromResult(response));

            var result = service.GetClassifier(classifierId: classifierId);


            string messageUrl = $"{service.ServiceUrl}/v1/classifiers/{classifierId}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteClassifierAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(client);
            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 200
            };

            string classifierId = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteClassifier(classifierId: classifierId);


            string messageUrl = $"{service.ServiceUrl}/v1/classifiers/{classifierId}";
            client.Received().DeleteAsync(messageUrl);
        }

    }
}
