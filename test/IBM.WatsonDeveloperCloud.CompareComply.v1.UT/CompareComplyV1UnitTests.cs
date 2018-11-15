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

using IBM.WatsonDeveloperCloud.CompareComply.v1.Model;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Http.Exceptions;
using IBM.WatsonDeveloperCloud.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace IBM.WatsonDeveloperCloud.CompareComply.v1.UT
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

        [TestMethod]
        public void Constructor_HttpClient()
        {
            CompareComplyService service =
                new CompareComplyService(CreateClient());

            Assert.IsNotNull(service);
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
                new CompareComplyService();

            Assert.IsNotNull(service);
        }
        #endregion

        #region Create Client
        private IClient CreateClient()
        {
            IClient client = Substitute.For<IClient>();
            client.WithAuthentication(Arg.Any<string>())
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

            service.ConvertToHtml(null);
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

            using (FileStream fs = Arg.Any<FileStream>())
            {
                service.ConvertToHtml(fs);
            }
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
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

            using (FileStream fs = File.OpenRead(tableFilePath))
            {
                service.ConvertToHtml(fs);
            }
        }

        [TestMethod]
        public void ConvertToHtml_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = Substitute.For<HTMLReturn>();
            response.Author = "author";
            response.Html = "html";
            response.NumPages = "1";
            response.PublicationDate = "date";
            response.Title = "title";
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
                ServiceUrl="https://www.serviceurl.com"
            };
            service.SetCredential(tokenOptions);
            service.VersionDate = "versionDate";

            HTMLReturn result = null;
            using (FileStream fs = File.OpenRead(tableFilePath))
            {
                result = service.ConvertToHtml(fs);
            }

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.Author == "author");
            Assert.IsTrue(result.Html == "html");
            Assert.IsTrue(result.NumPages == "1");
            Assert.IsTrue(result.PublicationDate == "date");
            Assert.IsTrue(result.Title == "title");
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
