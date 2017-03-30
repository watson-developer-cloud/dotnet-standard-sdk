/**
* Copyright 2017 IBM Corp. All Rights Reserved.
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

using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBM.WatsonDeveloperCloud.VisualRecognition.UnitTests
{
    [TestClass]
    public class VisualRecognitionServiceUnitTests
    {
        /// <summary>
        /// Create a IClient Mock
        /// </summary>
        /// <returns></returns>
        private IClient CreateClient()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            return client;
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            VisualRecognitionService service =
                new VisualRecognitionService(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ApiKey_Null()
        {
            VisualRecognitionService service =
                new VisualRecognitionService(null, null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Endpoint_Null()
        {
            VisualRecognitionService service =
                new VisualRecognitionService(Arg.Any<string>(), null);
        }

        [TestMethod]
        public void Constructor_With_ApiKey_Endpoint()
        {
            VisualRecognitionService service =
                new VisualRecognitionService("apikey", "http://www.anyuri.com");
        }

        [TestMethod]
        public void Constructor_With_ApiKey()
        {
            VisualRecognitionService service =
                new VisualRecognitionService("apikey", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_With_Endpoint()
        {
            VisualRecognitionService service =
                new VisualRecognitionService(null, "endpoint");
        }

        [TestMethod]
        public void Classify_Success()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            ClassifyTopLevelMultiple response = new ClassifyTopLevelMultiple()
            {
                Images = new List<ClassifyTopLevelSingle>()
                {
                    new ClassifyTopLevelSingle()
                    {
                        Classifiers = new List<ClassifyPerClassifier>()
                        {
                            new ClassifyPerClassifier()
                            {
                                Classes = new List<ClassResult>()
                                {
                                    new ClassResult()
                                    {
                                        _Class = "turtle",
                                        Score = 1.00f
                                    }
                                }
                            }
                        }
                    }
                }
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);

            request.As<ClassifyTopLevelMultiple>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service = 
                new VisualRecognitionService(client);

            var result = service.Classify("url-to-classify");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(result.Images.Count > 0);
        }
    }
}
