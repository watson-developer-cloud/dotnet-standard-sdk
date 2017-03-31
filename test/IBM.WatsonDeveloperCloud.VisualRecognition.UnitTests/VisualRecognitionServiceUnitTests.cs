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

            client.WithAuthentication(null, null)
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
                new VisualRecognitionService("", null);
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
        public void Classify_Get_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

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
                                        Score = 0.98f
                                    }
                                }
                            }
                        }
                    }
                }
            };

            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<float>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);

            request.As<ClassifyTopLevelMultiple>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var classifications = service.Classify("url-to-classify");

            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsNotNull(classifications);
            Assert.IsTrue(classifications.Images.Count > 0);
            Assert.IsTrue(classifications.Images[0].Classifiers[0].Classes[0]._Class == "turtle");
            Assert.IsTrue(classifications.Images[0].Classifiers[0].Classes[0].Score == 0.98f);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Classify_Get_No_Url()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var classifications = service.Classify();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Classify_Post_NoImage_NoUrl()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var classifications = service.Classify(Arg.Any<byte[]>(), null, null, null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Classify_Post_NoImage_OrName()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var classifications = service.Classify(Arg.Any<byte[]>(), null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Classify_Post_IncorrectOwners()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var classifications = service.Classify(Arg.Any<byte[]>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string[]>(), Arg.Any<string[]>());
        }

        [TestMethod]
        public void Classify_Post_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            ClassifyPost response = new ClassifyPost()
            {
                Images = new List<Classifiers>()
                {
                    _Classifiers = new List<ClassifyPerClassifier>()
                    {
                        new ClassifyPerClassifier()
                        {
                            Classes = new List<ClassResult>()
                            {
                                new ClassResult()
                                {
                                    _Class = "turtle",
                                    Score = 0.98f
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
