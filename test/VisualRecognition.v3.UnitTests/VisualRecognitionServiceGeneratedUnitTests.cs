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
            VisualRecognitionService service = Substitute.For<VisualRecognitionService>("versionDate");
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Constructor()
        {
            VisualRecognitionService service = new VisualRecognitionService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorAuthenticator()
        {
            VisualRecognitionService service = new VisualRecognitionService("versionDate", new NoAuthAuthenticator());
            Assert.IsNotNull(service);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNoVersion()
        {
            VisualRecognitionService service = new VisualRecognitionService(null, new NoAuthAuthenticator());
        }

        [TestMethod]
        public void ConstructorNoUrl()
        {
            var url = System.Environment.GetEnvironmentVariable("VISUAL_RECOGNITION_SERVICE_URL");
            System.Environment.SetEnvironmentVariable("VISUAL_RECOGNITION_SERVICE_URL", null);
            VisualRecognitionService service = Substitute.For<VisualRecognitionService>("versionDate");
            Assert.IsTrue(service.ServiceUrl == "https://gateway.watsonplatform.net/visual-recognition/api");
            System.Environment.SetEnvironmentVariable("VISUAL_RECOGNITION_SERVICE_URL", url);
        }
        #endregion

        [TestMethod]
        public void Classify_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var imagesFile = new MemoryStream();
            var imagesFilename = "imagesFilename";
            var imagesFileContentType = "imagesFileContentType";
            var url = "url";
            float? threshold = 0.5f;
            var owners = new List<string>() { "owners0", "owners1" };
            var classifierIds = new List<string>() { "classifierIds0", "classifierIds1" };
            var acceptLanguage = "acceptLanguage";

            var result = service.;

            request.Received().WithArgument("version", versionDate);
        }
        [TestMethod]
        public void CreateClassifier_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var name = "name";
            var positiveExamples = new Dictionary<string, System.IO.MemoryStream>();
            positiveExamples.Add("positiveExamples", new System.IO.MemoryStream());
            var negativeExamples = new MemoryStream();
            var negativeExamplesFilename = "negativeExamplesFilename";

            var result = service.;

            request.Received().WithArgument("version", versionDate);
        }
        [TestMethod]
        public void ListClassifiers_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var verbose = false;

            var result = service.;

            request.Received().WithArgument("version", versionDate);
        }
        [TestMethod]
        public void GetClassifier_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var classifierId = "classifierId";

            var result = service.;

            request.Received().WithArgument("version", versionDate);
            client.Received().GetAsync($"{service.ServiceUrl}/v3/classifiers/{classifierId}");
        }
        [TestMethod]
        public void UpdateClassifier_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var classifierId = "classifierId";
            var positiveExamples = new Dictionary<string, System.IO.MemoryStream>();
            positiveExamples.Add("positiveExamples", new System.IO.MemoryStream());
            var negativeExamples = new MemoryStream();
            var negativeExamplesFilename = "negativeExamplesFilename";

            var result = service.;

            request.Received().WithArgument("version", versionDate);
            client.Received().PostAsync($"{service.ServiceUrl}/v3/classifiers/{classifierId}");
        }
        [TestMethod]
        public void DeleteClassifier_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var classifierId = "classifierId";

            var result = service.;

            request.Received().WithArgument("version", versionDate);
            client.Received().DeleteAsync($"{service.ServiceUrl}/v3/classifiers/{classifierId}");
        }
        [TestMethod]
        public void GetCoreMlModel_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var classifierId = "classifierId";

            var result = service.;

            request.Received().WithArgument("version", versionDate);
            client.Received().GetAsync($"{service.ServiceUrl}/v3/classifiers/{classifierId}/core_ml_model");
        }
        [TestMethod]
        public void DeleteUserData_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var customerId = "customerId";

            var result = service.;

            request.Received().WithArgument("version", versionDate);
        }
    }
}
