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
using IBM.Watson.CompareComply.v1.Model;
using IBM.Cloud.SDK.Core.Model;

namespace IBM.Watson.CompareComply.v1.UnitTests
{
    [TestClass]
    public class CompareComplyServiceUnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            CompareComplyService service = new CompareComplyService(httpClient: null);
        }

        [TestMethod]
        public void ConstructorHttpClient()
        {
            CompareComplyService service = new CompareComplyService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorExternalConfig()
        {
            var apikey = System.Environment.GetEnvironmentVariable("COMPARE_COMPLY_APIKEY");
            System.Environment.SetEnvironmentVariable("COMPARE_COMPLY_APIKEY", "apikey");
            CompareComplyService service = Substitute.For<CompareComplyService>("versionDate");
            Assert.IsNotNull(service);
            System.Environment.SetEnvironmentVariable("COMPARE_COMPLY_APIKEY", apikey);
        }

        [TestMethod]
        public void Constructor()
        {
            CompareComplyService service = new CompareComplyService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorAuthenticator()
        {
            CompareComplyService service = new CompareComplyService("versionDate", new NoAuthAuthenticator());
            Assert.IsNotNull(service);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNoVersion()
        {
            CompareComplyService service = new CompareComplyService(null, new NoAuthAuthenticator());
        }

        [TestMethod]
        public void ConstructorNoUrl()
        {
            var apikey = System.Environment.GetEnvironmentVariable("COMPARE_COMPLY_APIKEY");
            System.Environment.SetEnvironmentVariable("COMPARE_COMPLY_APIKEY", "apikey");
            var url = System.Environment.GetEnvironmentVariable("COMPARE_COMPLY_URL");
            System.Environment.SetEnvironmentVariable("COMPARE_COMPLY_URL", null);
            CompareComplyService service = Substitute.For<CompareComplyService>("versionDate");
            Assert.IsTrue(service.ServiceUrl == "https://gateway.watsonplatform.net/compare-comply/api");
            System.Environment.SetEnvironmentVariable("COMPARE_COMPLY_URL", url);
        }
        #endregion

        [TestMethod]
        public void ConvertToHtml_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            CompareComplyService service = new CompareComplyService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var file = new MemoryStream();
            var fileContentType = "fileContentType";
            var model = "model";

            var result = service.ConvertToHtml(file: file, fileContentType: fileContentType, model: model);

            request.Received().WithArgument("version", versionDate);
        }
        [TestMethod]
        public void ClassifyElements_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            CompareComplyService service = new CompareComplyService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var file = new MemoryStream();
            var fileContentType = "fileContentType";
            var model = "model";

            var result = service.ClassifyElements(file: file, fileContentType: fileContentType, model: model);

            request.Received().WithArgument("version", versionDate);
        }
        [TestMethod]
        public void ExtractTables_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            CompareComplyService service = new CompareComplyService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var file = new MemoryStream();
            var fileContentType = "fileContentType";
            var model = "model";

            var result = service.ExtractTables(file: file, fileContentType: fileContentType, model: model);

            request.Received().WithArgument("version", versionDate);
        }
        [TestMethod]
        public void CompareDocuments_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            CompareComplyService service = new CompareComplyService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var file1 = new MemoryStream();
            var file2 = new MemoryStream();
            var file1ContentType = "file1ContentType";
            var file2ContentType = "file2ContentType";
            var file1Label = "file1Label";
            var file2Label = "file2Label";
            var model = "model";

            var result = service.CompareDocuments(file1: file1, file2: file2, file1ContentType: file1ContentType, file2ContentType: file2ContentType, file1Label: file1Label, file2Label: file2Label, model: model);

            request.Received().WithArgument("version", versionDate);
        }
        [TestMethod]
        public void AddFeedback_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            CompareComplyService service = new CompareComplyService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var feedbackData = new FeedbackDataInput();
            var userId = "userId";
            var comment = "comment";

            var result = service.AddFeedback(feedbackData: feedbackData, userId: userId, comment: comment);

            JObject bodyObject = new JObject();
            if (feedbackData != null)
            {
                bodyObject["feedback_data"] = JToken.FromObject(feedbackData);
            }
            if (!string.IsNullOrEmpty(userId))
            {
                bodyObject["user_id"] = JToken.FromObject(userId);
            }
            if (!string.IsNullOrEmpty(comment))
            {
                bodyObject["comment"] = JToken.FromObject(comment);
            }
            var json = JsonConvert.SerializeObject(bodyObject);
            request.Received().WithArgument("version", versionDate);
            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
        }
        [TestMethod]
        public void ListFeedback_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            CompareComplyService service = new CompareComplyService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var feedbackType = "feedbackType";
            DateTime? before = DateTime.MaxValue;
            DateTime? after = DateTime.MaxValue;
            var documentTitle = "documentTitle";
            var modelId = "modelId";
            var modelVersion = "modelVersion";
            var categoryRemoved = "categoryRemoved";
            var categoryAdded = "categoryAdded";
            var categoryNotChanged = "categoryNotChanged";
            var typeRemoved = "typeRemoved";
            var typeAdded = "typeAdded";
            var typeNotChanged = "typeNotChanged";
            long? pageLimit = 1;
            var cursor = "cursor";
            var sort = "sort";
            var includeTotal = false;

            var result = service.ListFeedback(feedbackType: feedbackType, before: before, after: after, documentTitle: documentTitle, modelId: modelId, modelVersion: modelVersion, categoryRemoved: categoryRemoved, categoryAdded: categoryAdded, categoryNotChanged: categoryNotChanged, typeRemoved: typeRemoved, typeAdded: typeAdded, typeNotChanged: typeNotChanged, pageLimit: pageLimit, cursor: cursor, sort: sort, includeTotal: includeTotal);

            request.Received().WithArgument("version", versionDate);
        }
        [TestMethod]
        public void GetFeedback_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            CompareComplyService service = new CompareComplyService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var feedbackId = "feedbackId";
            var model = "model";

            var result = service.GetFeedback(feedbackId: feedbackId, model: model);

            request.Received().WithArgument("version", versionDate);
            client.Received().GetAsync($"{service.ServiceUrl}/v1/feedback/{feedbackId}");
        }
        [TestMethod]
        public void DeleteFeedback_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            CompareComplyService service = new CompareComplyService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var feedbackId = "feedbackId";
            var model = "model";

            var result = service.DeleteFeedback(feedbackId: feedbackId, model: model);

            request.Received().WithArgument("version", versionDate);
            client.Received().DeleteAsync($"{service.ServiceUrl}/v1/feedback/{feedbackId}");
        }
        [TestMethod]
        public void CreateBatch_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            CompareComplyService service = new CompareComplyService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var function = "function";
            var inputCredentialsFile = new MemoryStream();
            var inputBucketLocation = "inputBucketLocation";
            var inputBucketName = "inputBucketName";
            var outputCredentialsFile = new MemoryStream();
            var outputBucketLocation = "outputBucketLocation";
            var outputBucketName = "outputBucketName";
            var model = "model";

            var result = service.CreateBatch(function: function, inputCredentialsFile: inputCredentialsFile, inputBucketLocation: inputBucketLocation, inputBucketName: inputBucketName, outputCredentialsFile: outputCredentialsFile, outputBucketLocation: outputBucketLocation, outputBucketName: outputBucketName, model: model);

            request.Received().WithArgument("version", versionDate);
        }
        [TestMethod]
        public void ListBatches_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            CompareComplyService service = new CompareComplyService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;


            var result = service.ListBatches();

            request.Received().WithArgument("version", versionDate);
        }
        [TestMethod]
        public void GetBatch_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            CompareComplyService service = new CompareComplyService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var batchId = "batchId";

            var result = service.GetBatch(batchId: batchId);

            request.Received().WithArgument("version", versionDate);
            client.Received().GetAsync($"{service.ServiceUrl}/v1/batches/{batchId}");
        }
        [TestMethod]
        public void UpdateBatch_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                .Returns(request);

            CompareComplyService service = new CompareComplyService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var batchId = "batchId";
            var action = "action";
            var model = "model";

            var result = service.UpdateBatch(batchId: batchId, action: action, model: model);

            request.Received().WithArgument("version", versionDate);
            client.Received().PutAsync($"{service.ServiceUrl}/v1/batches/{batchId}");
        }
    }
}
