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
using IBM.Watson.VisualRecognition.v3.Model;
using System.Collections.Generic;
using System.Text;
using IBM.Cloud.SDK.Core.Authentication.NoAuth;
using System.Threading.Tasks;

namespace IBM.Watson.VisualRecognition.v3.UnitTests
{
    [TestClass]
    public class VisualRecognitionServiceUnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            VisualRecognitionService service = new VisualRecognitionService(httpClient: null);
        }

        [TestMethod]
        public void ConstructorHttpClient()
        {
            VisualRecognitionService service = new VisualRecognitionService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorExternalConfig()
        {
            var apikey = System.Environment.GetEnvironmentVariable("VISUAL_RECOGNITION_APIKEY");
            var url = System.Environment.GetEnvironmentVariable("VISUAL_RECOGNITION_URL");
            System.Environment.SetEnvironmentVariable("VISUAL_RECOGNITION_APIKEY", "apikey");
            System.Environment.SetEnvironmentVariable("VISUAL_RECOGNITION_URL", "http://www.url.com");
            VisualRecognitionService service = Substitute.For<VisualRecognitionService>("testString");
            Assert.IsNotNull(service);
            System.Environment.SetEnvironmentVariable("VISUAL_RECOGNITION_URL", url);
            System.Environment.SetEnvironmentVariable("VISUAL_RECOGNITION_APIKEY", apikey);
        }

        [TestMethod]
        public void Constructor()
        {
            VisualRecognitionService service = new VisualRecognitionService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorNoUrl()
        {
            var apikey = System.Environment.GetEnvironmentVariable("TEST_SERVICE_APIKEY");
            System.Environment.SetEnvironmentVariable("TEST_SERVICE_APIKEY", "apikey");
            VisualRecognitionService service = Substitute.For<VisualRecognitionService>("testString", "test_service");
            Assert.IsTrue(service.ServiceUrl == "https://gateway.watsonplatform.net/visual-recognition/api");
            System.Environment.SetEnvironmentVariable("TEST_SERVICE_APIKEY", apikey);
        }
        #endregion

        [TestMethod]
        public void TestTestClassifyAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'custom_classes': 13, 'images_processed': 15, 'images': [{'source_url': 'SourceUrl', 'resolved_url': 'ResolvedUrl', 'image': 'Image', 'error': {'code': 4, 'description': 'Description', 'error_id': 'ErrorId'}, 'classifiers': [{'name': 'Name', 'classifier_id': 'ClassifierId', 'classes': [{'class': '_Class', 'score': 5, 'type_hierarchy': 'TypeHierarchy'}]}]}], 'warnings': [{'warning_id': 'WarningId', 'description': 'Description'}]}";
            var response = new DetailedResponse<ClassifiedImages>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<ClassifiedImages>(responseJson),
                StatusCode = 200
            };

            System.IO.MemoryStream imagesFile = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file."));
            string imagesFilename = "testString";
            string imagesFileContentType = "testString";
            string url = "testString";
            float? threshold = 36.0f;
            List<string> owners = new List<string> { "testString" };
            List<string> classifierIds = new List<string> { "testString" };
            string acceptLanguage = "en";

            request.As<ClassifiedImages>().Returns(Task.FromResult(response));

            var result = service.Classify(imagesFile: imagesFile, imagesFilename: imagesFilename, imagesFileContentType: imagesFileContentType, url: url, threshold: threshold, owners: owners, classifierIds: classifierIds, acceptLanguage: acceptLanguage);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v3/classify";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestCreateClassifierAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'classifier_id': 'ClassifierId', 'name': 'Name', 'owner': 'Owner', 'status': 'ready', 'core_ml_enabled': false, 'explanation': 'Explanation', 'classes': [{'class': '_Class'}]}";
            var response = new DetailedResponse<Classifier>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Classifier>(responseJson),
                StatusCode = 200
            };

            string name = "testString";
            Dictionary<string, System.IO.MemoryStream> positiveExamples = new Dictionary<string, System.IO.MemoryStream>() { { "key1", new System.IO.MemoryStream() } };
            System.IO.MemoryStream negativeExamples = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file."));
            string negativeExamplesFilename = "testString";

            request.As<Classifier>().Returns(Task.FromResult(response));

            var result = service.CreateClassifier(name: name, positiveExamples: positiveExamples, negativeExamples: negativeExamples, negativeExamplesFilename: negativeExamplesFilename);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v3/classifiers";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListClassifiersAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'classifiers': [{'classifier_id': 'ClassifierId', 'name': 'Name', 'owner': 'Owner', 'status': 'ready', 'core_ml_enabled': false, 'explanation': 'Explanation', 'classes': [{'class': '_Class'}]}]}";
            var response = new DetailedResponse<Classifiers>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Classifiers>(responseJson),
                StatusCode = 200
            };

            bool? verbose = true;

            request.As<Classifiers>().Returns(Task.FromResult(response));

            var result = service.ListClassifiers(verbose: verbose);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v3/classifiers";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetClassifierAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'classifier_id': 'ClassifierId', 'name': 'Name', 'owner': 'Owner', 'status': 'ready', 'core_ml_enabled': false, 'explanation': 'Explanation', 'classes': [{'class': '_Class'}]}";
            var response = new DetailedResponse<Classifier>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Classifier>(responseJson),
                StatusCode = 200
            };

            string classifierId = "testString";

            request.As<Classifier>().Returns(Task.FromResult(response));

            var result = service.GetClassifier(classifierId: classifierId);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v3/classifiers/{classifierId}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestUpdateClassifierAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'classifier_id': 'ClassifierId', 'name': 'Name', 'owner': 'Owner', 'status': 'ready', 'core_ml_enabled': false, 'explanation': 'Explanation', 'classes': [{'class': '_Class'}]}";
            var response = new DetailedResponse<Classifier>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Classifier>(responseJson),
                StatusCode = 200
            };

            string classifierId = "testString";
            Dictionary<string, System.IO.MemoryStream> positiveExamples = new Dictionary<string, System.IO.MemoryStream>() { { "key1", new System.IO.MemoryStream() } };
            System.IO.MemoryStream negativeExamples = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file."));
            string negativeExamplesFilename = "testString";

            request.As<Classifier>().Returns(Task.FromResult(response));

            var result = service.UpdateClassifier(classifierId: classifierId, positiveExamples: positiveExamples, negativeExamples: negativeExamples, negativeExamplesFilename: negativeExamplesFilename);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v3/classifiers/{classifierId}";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteClassifierAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 200
            };

            string classifierId = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteClassifier(classifierId: classifierId);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v3/classifiers/{classifierId}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetCoreMlModelAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var response = new DetailedResponse<byte[]>()
            {
                Result = new byte[4],
                StatusCode = 200
            };
            string classifierId = "testString";

            request.As<byte[]>().Returns(Task.FromResult(response));

            var result = service.GetCoreMlModel(classifierId: classifierId);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v3/classifiers/{classifierId}/core_ml_model";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteUserDataAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 202
            };

            string customerId = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteUserData(customerId: customerId);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v3/user_data";
            client.Received().DeleteAsync(messageUrl);
        }

    }
}
