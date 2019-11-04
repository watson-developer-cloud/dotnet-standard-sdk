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

using NSubstitute;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Http.Exceptions;
using IBM.Cloud.SDK.Core.Authentication.NoAuth;
using IBM.Watson.NaturalLanguageClassifier.v1.Model;
using IBM.Cloud.SDK.Core.Model;

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
            System.Environment.SetEnvironmentVariable("NATURAL_LANGUAGE_CLASSIFIER_APIKEY", "apikey");
            NaturalLanguageClassifierService service = Substitute.For<NaturalLanguageClassifierService>();
            Assert.IsNotNull(service);
            System.Environment.SetEnvironmentVariable("NATURAL_LANGUAGE_CLASSIFIER_APIKEY", apikey);
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
            var apikey = System.Environment.GetEnvironmentVariable("NATURAL_LANGUAGE_CLASSIFIER_APIKEY");
            System.Environment.SetEnvironmentVariable("NATURAL_LANGUAGE_CLASSIFIER_APIKEY", "apikey");
            var url = System.Environment.GetEnvironmentVariable("NATURAL_LANGUAGE_CLASSIFIER_URL");
            System.Environment.SetEnvironmentVariable("NATURAL_LANGUAGE_CLASSIFIER_URL", null);
            NaturalLanguageClassifierService service = Substitute.For<NaturalLanguageClassifierService>();
            Assert.IsTrue(service.ServiceUrl == "https://gateway.watsonplatform.net/natural-language-classifier/api");
            System.Environment.SetEnvironmentVariable("NATURAL_LANGUAGE_CLASSIFIER_URL", url);
            System.Environment.SetEnvironmentVariable("NATURAL_LANGUAGE_CLASSIFIER_APIKEY", apikey);
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

            var result = service.Classify(classifierId: classifierId, text: text);

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

            var result = service.ClassifyCollection(classifierId: classifierId, collection: collection);

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

            var result = service.CreateClassifier(trainingMetadata: trainingMetadata, trainingData: trainingData);

        }
        [TestMethod]
        public void ListClassifiers_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(client);


            var result = service.ListClassifiers();

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

            var result = service.GetClassifier(classifierId: classifierId);

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

            var result = service.DeleteClassifier(classifierId: classifierId);

            client.Received().DeleteAsync($"{service.ServiceUrl}/v1/classifiers/{classifierId}");
        }
    }
}
