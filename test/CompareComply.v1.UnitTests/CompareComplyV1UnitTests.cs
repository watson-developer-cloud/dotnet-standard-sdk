/**
* Copyright 2018 IBM Corp. All Rights Reserved.
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

using IBM.Watson.CompareComply.v1.Model;
using IBM.Cloud.SDK.Core.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Http.Exceptions;

namespace IBM.Watson.CompareComply.v1.UT
{
    [TestClass]
    public class CompareComplyV1UnitTests
    {
        public string tableFilePath = @"CompareComplyTestData/TestTable.pdf";

        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            CompareComplyService service =
                new CompareComplyService(null);
        }

        [TestMethod, ExpectedException(typeof(NullReferenceException))]
        public void Constructor_TokenOptions_Null()
        {
            CompareComplyService service =
                new CompareComplyService(null, "2018-02-16");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Version_Null()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = "iamApikey"
            };
            CompareComplyService service =
                new CompareComplyService(tokenOptions, null);
        }

        [TestMethod]
        public void Constructor_With_TokenOptions()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = "iamApikey"
            };
            CompareComplyService service =
                new CompareComplyService(tokenOptions, "2018-02-16");

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Constructor()
        {
            CompareComplyService service =
                new CompareComplyService(new IBMHttpClient());

            Assert.IsNotNull(service);
        }
        #endregion

        #region Create Client
        private IClient CreateClient()
        {
            IClient client = Substitute.For<IClient>();
            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                .Returns(client);

            return client;
        }
        #endregion

        #region HTML Conversion
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ConvertToHtml_No_File()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = "iamApikey"
            };
            CompareComplyService service = new CompareComplyService(tokenOptions, "versionDate");

            service.ConvertToHtml(null, "filename");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ConvertToHtml_No_FileName()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = "iamApikey"
            };
            CompareComplyService service = new CompareComplyService(tokenOptions, "versionDate");

            using (MemoryStream ms = Arg.Any<MemoryStream>())
            {
                service.ConvertToHtml(ms, null);
            }
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ConvertToHtml_No_VersionDate()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = "iamApikey"
            };
            CompareComplyService service = new CompareComplyService(tokenOptions, "versionDate");
            service.VersionDate = null;

            using (MemoryStream ms = Arg.Any<MemoryStream>())
            {
                service.ConvertToHtml(ms, "filenmame");
            }
        }

        //[TestMethod, ExpectedException(typeof(AggregateException))]
        public void ConvertToHtml_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            CompareComplyService service = new CompareComplyService(client);
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamAccessToken = "iamAccessToken",
                ServiceUrl = "https://www.serviceurl.com"
            };
            service.SetCredential(tokenOptions);
            service.VersionDate = "2018-02-16";

            using (MemoryStream fs = new MemoryStream())
            {
                service.ConvertToHtml(fs, "filename");
            }
        }

        //[TestMethod]
        public void ConvertToHtml_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = Substitute.For<DetailedResponse<HTMLReturn>>();
            response.Result = Substitute.For<HTMLReturn>();
            response.Result.Author = "author";
            response.Result.Html = "html";
            response.Result.NumPages = "1";
            response.Result.PublicationDate = "date";
            response.Result.Title = "title";
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<FileStream>(Arg.Any<FileStream>())
                .Returns(request);
            request.As<HTMLReturn>()
                .Returns(Task.FromResult(response));

            CompareComplyService service = new CompareComplyService(client);
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamAccessToken = "iamAccessToken",
                ServiceUrl = "https://www.serviceurl.com"
            };
            service.SetCredential(tokenOptions);
            service.VersionDate = "versionDate";

            DetailedResponse<HTMLReturn> result = null;
            using (MemoryStream fs = new MemoryStream())
            {
                result = service.ConvertToHtml(fs, "filename");
            }

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.Result.Author == "author");
            Assert.IsTrue(result.Result.Html == "html");
            Assert.IsTrue(result.Result.NumPages == "1");
            Assert.IsTrue(result.Result.PublicationDate == "date");
            Assert.IsTrue(result.Result.Title == "title");
        }
        #endregion

        #region Element Classification
        #endregion

        #region Extract Tables
        #endregion

        #region Comparision
        #endregion

        #region Feedback
        #endregion

        #region Batches
        #endregion
    }
}
