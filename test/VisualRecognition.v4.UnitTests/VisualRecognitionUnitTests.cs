/**
* (C) Copyright IBM Corp. 2017, 2019.
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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Authentication.NoAuth;
using IBM.Watson.VisualRecognition.v4.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IBM.Watson.VisualRecognition.v4.UnitTests
{
    [TestClass]
    public class VisualRecognitionUnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            VisualRecognitionService service =
                new VisualRecognitionService(httpClient: null);
        }

        [TestMethod]
        public void Constructor_HttpClient()
        {
            VisualRecognitionService service =
                new VisualRecognitionService(CreateClient());

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Constructor()
        {
            VisualRecognitionService service =
                new VisualRecognitionService(new IBMHttpClient());

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Constructor_Authenticator()
        {
            VisualRecognitionService service = new VisualRecognitionService("versionDate", new NoAuthAuthenticator());

            Assert.IsNotNull(service);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NoVersion()
        {
            VisualRecognitionService service = new VisualRecognitionService(null, new NoAuthAuthenticator());
        }
        #endregion

        #region Analysis
        [TestMethod]
        public void Analysis()
        {
            IClient client = CreateClient();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region response
            var analyzeResponse = new AnalyzeResponse()
            {
                Images = new List<Image>()
                {
                    new Image()
                    {
                        Source = new ImageSource()
                        {
                            Type = "type",
                            Filename = "filename",
                            ArchiveFilename = "archiveFilename",
                            SourceUrl = "sourceUrl",
                            ResolvedUrl = "resolvedUrl"
                        },
                        Dimensions = new ImageDimensions()
                        {
                            Width = 100,
                            Height = 200
                        },
                        Objects = new DetectedObjects()
                        {
                            Collections = new List<CollectionObjects>()
                            {
                                new CollectionObjects()
                                {
                                    CollectionId = "collectionId",
                                    Objects = new List<ObjectDetail>()
                                    {
                                        new ObjectDetail()
                                        {
                                            _Object = "object",
                                            Location = new Location()
                                            {
                                                Top = 0,
                                                Left = 1,
                                                Width = 2,
                                                Height = 3
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var response =new DetailedResponse<AnalyzeResponse>();
            response.Result = analyzeResponse;
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(new MultipartFormDataContent())
                .Returns(request);
            request.As<AnalyzeResponse>()
                .Returns(Task.FromResult(response));

            NoAuthAuthenticator authenticator = new NoAuthAuthenticator();
            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";
            service.Analyze(
                collectionIds: new List<string> { "colletionIds" },
                features: new List<string> { "features" });

            Assert.IsTrue(response.Result.Images[0].Source.Type == "type");
            Assert.IsTrue(response.Result.Images[0].Source.Filename == "filename");
            Assert.IsTrue(response.Result.Images[0].Source.ArchiveFilename == "archiveFilename");
            Assert.IsTrue(response.Result.Images[0].Source.SourceUrl == "sourceUrl");
            Assert.IsTrue(response.Result.Images[0].Source.ResolvedUrl == "resolvedUrl");
            Assert.IsTrue(response.Result.Images[0].Dimensions.Width == 100);
            Assert.IsTrue(response.Result.Images[0].Dimensions.Height == 200);
            Assert.IsTrue(response.Result.Images[0].Objects.Collections[0].CollectionId == "collectionId");
            Assert.IsTrue(response.Result.Images[0].Objects.Collections[0].Objects[0]._Object == "object");
            Assert.IsTrue(response.Result.Images[0].Objects.Collections[0].Objects[0].Location.Top == 0);
            Assert.IsTrue(response.Result.Images[0].Objects.Collections[0].Objects[0].Location.Left == 1);
            Assert.IsTrue(response.Result.Images[0].Objects.Collections[0].Objects[0].Location.Width == 2);
            Assert.IsTrue(response.Result.Images[0].Objects.Collections[0].Objects[0].Location.Height == 3);

        }
        #endregion

        #region CreateCollection
        #endregion

        #region ListCollections
        #endregion

        #region GetCollection
        #endregion

        #region UpdateCollection
        #endregion

        #region DeleteCollection
        #endregion

        #region AddImages
        #endregion

        #region ListImages
        #endregion

        #region GetImageDetails
        #endregion

        #region DeleteImage
        #endregion

        #region GetJpgImage
        #endregion

        #region Train
        #endregion

        #region AddImageTrainingData
        #endregion

        #region DeleteUserData
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
        
    }
}
