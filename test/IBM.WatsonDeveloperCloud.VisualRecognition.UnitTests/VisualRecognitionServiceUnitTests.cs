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
using System.Net.Http;
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

            client.WithAuthentication("user", "password")
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

        #region Classify
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

            Assert.IsNotNull(classifications);
            client.Received().GetAsync(Arg.Any<string>());
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
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            ClassifyPost response = new ClassifyPost()
            {
                Images = new List<Classifiers>()
                {
                    new Classifiers()
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

                }
            };

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(Arg.Any<HttpContent>())
                .Returns(request);

            request.As<ClassifyPost>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var classifications = service.Classify(new byte[4], "name", "image/jpeg");

            Assert.IsNotNull(classifications);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(classifications.Images.Count > 0);
            Assert.IsTrue(classifications.Images[0]._Classifiers[0].Classes[0]._Class == "turtle");
            Assert.IsTrue(classifications.Images[0]._Classifiers[0].Classes[0].Score == 0.98f);
        }
        #endregion

        #region Detect Faces
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DetectFaces_Get_No_Url()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var classifications = service.DetectFaces();
        }

        [TestMethod]
        public void DetectFaces_Get_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            Faces response = new Faces()
            {
                Images = new List<FacesTopLevelSingle>()
                {
                    new FacesTopLevelSingle()
                    {
                        Faces = new List<OneFaceResult>()
                        {
                            new OneFaceResult()
                            {
                                Age = new OneFaceResultAge()
                                {
                                    Min = 20,
                                    Max = 30,
                                    Score = 0.98f
                                },
                                Gender = new OneFaceResultGender()
                                {
                                    Gender = "male",
                                    Score = 0.98f
                                },
                                Identity = new OneFaceResultIdentity()
                                {
                                    Name = "Sneaky Snake",
                                    Score = 0.98f,
                                    TypeHierarchy = "Regular Snakes/Sneaky Snake"
                                },
                                FaceLocation = new OneFaceResultFaceLocation()
                                {
                                    Top = 0,
                                    Left = 0,
                                    Width = 100,
                                    Height = 100
                                }
                            }
                        }
                    }
                },
                ImagesProcessed = 1,
                Warnings = new List<WarningInfo>()
                {
                    new WarningInfo()
                    {
                        WarningId = "warningId",
                        Description = "warningDescription"
                    }
                }
            };

            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);

            request.As<Faces>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var classifications = service.DetectFaces("url-to-classify");

            Assert.IsNotNull(classifications);
            client.Received().GetAsync(Arg.Any<string>());

            Assert.IsTrue(classifications.Images.Count > 0);
            Assert.IsTrue(classifications.Images[0].Faces[0].Identity.Name == "Sneaky Snake");
            Assert.IsTrue(classifications.Images[0].Faces[0].Identity.Score == 0.98f);
            Assert.IsTrue(classifications.Images[0].Faces[0].Identity.TypeHierarchy == "Regular Snakes/Sneaky Snake");
            Assert.IsTrue(classifications.Images[0].Faces[0].Gender.Gender == "male");
            Assert.IsTrue(classifications.Images[0].Faces[0].Gender.Score == 0.98f);
            Assert.IsTrue(classifications.Images[0].Faces[0].Age.Min == 20);
            Assert.IsTrue(classifications.Images[0].Faces[0].Age.Max == 30);
            Assert.IsTrue(classifications.Images[0].Faces[0].Age.Score == 0.98f);
            Assert.IsTrue(classifications.Images[0].Faces[0].FaceLocation.Left == 0);
            Assert.IsTrue(classifications.Images[0].Faces[0].FaceLocation.Top == 0);
            Assert.IsTrue(classifications.Images[0].Faces[0].FaceLocation.Width == 100);
            Assert.IsTrue(classifications.Images[0].Faces[0].FaceLocation.Height == 100);
            Assert.IsTrue(classifications.ImagesProcessed == 1);
            Assert.IsTrue(classifications.Warnings[0].WarningId == "warningId");
            Assert.IsTrue(classifications.Warnings[0].Description == "warningDescription");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DetectFaces_Post_No_Url_No_Image()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var classifications = service.DetectFaces(null, "name", "image/jpeg", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DetectFaces_Post_No_Name()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var classifications = service.DetectFaces(new byte[4], null, "Image/jpeg");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DetectFaces_Post_No_MimeType()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var classifications = service.DetectFaces(new byte[4], "name", null);
        }

        [TestMethod]
        public void DetectFaces_Post_ImageData_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            Faces response = new Faces()
            {
                Images = new List<FacesTopLevelSingle>()
                {
                    new FacesTopLevelSingle()
                    {
                        Faces = new List<OneFaceResult>()
                        {
                            new OneFaceResult()
                            {
                                Age = new OneFaceResultAge()
                                {
                                    Min = 20,
                                    Max = 30,
                                    Score = 0.98f
                                },
                                Gender = new OneFaceResultGender()
                                {
                                    Gender = "male",
                                    Score = 0.98f
                                },
                                Identity = new OneFaceResultIdentity()
                                {
                                    Name = "Sneaky Snake",
                                    Score = 0.98f,
                                    TypeHierarchy = "Regular Snakes/Sneaky Snake"
                                }
                            }
                        }
                    }
                }
            };

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(Arg.Any<HttpContent>())
                .Returns(request);

            request.As<Faces>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var classifications = service.DetectFaces(new byte[4], "name", "image/jpeg");

            Assert.IsNotNull(classifications);
            client.Received().PostAsync(Arg.Any<string>());

            Assert.IsTrue(classifications.Images.Count > 0);
            Assert.IsTrue(classifications.Images[0].Faces[0].Identity.Name == "Sneaky Snake");
            Assert.IsTrue(classifications.Images[0].Faces[0].Identity.Score == 0.98f);
            Assert.IsTrue(classifications.Images[0].Faces[0].Identity.TypeHierarchy == "Regular Snakes/Sneaky Snake");
            Assert.IsTrue(classifications.Images[0].Faces[0].Gender.Gender == "male");
            Assert.IsTrue(classifications.Images[0].Faces[0].Gender.Score == 0.98f);
            Assert.IsTrue(classifications.Images[0].Faces[0].Age.Min == 20);
            Assert.IsTrue(classifications.Images[0].Faces[0].Age.Max == 30);
            Assert.IsTrue(classifications.Images[0].Faces[0].Age.Score == 0.98f);
        }

        [TestMethod]
        public void DetectFaces_Post_Urls_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            Faces response = new Faces()
            {
                Images = new List<FacesTopLevelSingle>()
                {
                    new FacesTopLevelSingle()
                    {
                        Faces = new List<OneFaceResult>()
                        {
                            new OneFaceResult()
                            {
                                Age = new OneFaceResultAge()
                                {
                                    Min = 20,
                                    Max = 30,
                                    Score = 0.98f
                                },
                                Gender = new OneFaceResultGender()
                                {
                                    Gender = "male",
                                    Score = 0.98f
                                },
                                Identity = new OneFaceResultIdentity()
                                {
                                    Name = "Sneaky Snake",
                                    Score = 0.98f,
                                    TypeHierarchy = "Regular Snakes/Sneaky Snake"
                                }
                            }
                        }
                    }
                }
            };

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(Arg.Any<HttpContent>())
                .Returns(request);

            request.As<Faces>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var classifications = service.DetectFaces(null, null, null, new string[] { "http://faceurl1.com", "http://faceur2.com", "http://faceurl3.com" });

            Assert.IsNotNull(classifications);
            client.Received().PostAsync(Arg.Any<string>());

            Assert.IsTrue(classifications.Images.Count > 0);
            Assert.IsTrue(classifications.Images[0].Faces[0].Identity.Name == "Sneaky Snake");
            Assert.IsTrue(classifications.Images[0].Faces[0].Identity.Score == 0.98f);
            Assert.IsTrue(classifications.Images[0].Faces[0].Identity.TypeHierarchy == "Regular Snakes/Sneaky Snake");
            Assert.IsTrue(classifications.Images[0].Faces[0].Gender.Gender == "male");
            Assert.IsTrue(classifications.Images[0].Faces[0].Gender.Score == 0.98f);
            Assert.IsTrue(classifications.Images[0].Faces[0].Age.Min == 20);
            Assert.IsTrue(classifications.Images[0].Faces[0].Age.Max == 30);
            Assert.IsTrue(classifications.Images[0].Faces[0].Age.Score == 0.98f);
        }
        #endregion

        #region Classifiers
        [TestMethod]
        public void GetClassifiersBrief_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            GetClassifiersTopLevelBrief response = new GetClassifiersTopLevelBrief()
            {
                Classifiers = new List<GetClassifiersPerClassifierBrief>()
                {
                    new GetClassifiersPerClassifierBrief()
                    {
                        Name = "turtle-classifier",
                        ClassifierId = "turtle-classifier-id",
                        Status = "ready"
                    }
                }
            };

            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool>())
                .Returns(request);

            request.As<GetClassifiersTopLevelBrief>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var classifiers = service.GetClassifiersBrief();

            Assert.IsNotNull(classifiers);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(classifiers.Classifiers.Count > 0);
            Assert.IsTrue(classifiers.Classifiers[0].Name == "turtle-classifier");
            Assert.IsTrue(classifiers.Classifiers[0].ClassifierId == "turtle-classifier-id");
            Assert.IsTrue(classifiers.Classifiers[0].Status == "ready");
        }

        [TestMethod]
        public void GetClassifiersVerbose_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            GetClassifiersTopLevelVerbose response = new GetClassifiersTopLevelVerbose()
            {
                Classifiers = new List<GetClassifiersPerClassifierVerbose>()
                {
                    new GetClassifiersPerClassifierVerbose()
                    {
                        Name = "turtle-classifier",
                        ClassifierId = "turtle-classifier-id",
                        Status = "ready",
                        Explanation = "My Explination",
                        Owner = "me,IBM",
                        Created = "created",
                        Classes = new List<ModelClass>()
                        {
                            new ModelClass()
                            {
                                _Class = "class"
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
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool>())
                .Returns(request);

            request.As<GetClassifiersTopLevelVerbose>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var classifiers = service.GetClassifiersVerbose();

            Assert.IsNotNull(classifiers);
            client.Received().GetAsync(Arg.Any<string>());

            Assert.IsTrue(classifiers.Classifiers.Count > 0);
            Assert.IsTrue(classifiers.Classifiers[0].Name == "turtle-classifier");
            Assert.IsTrue(classifiers.Classifiers[0].ClassifierId == "turtle-classifier-id");
            Assert.IsTrue(classifiers.Classifiers[0].Status == "ready");
            Assert.IsTrue(classifiers.Classifiers[0].Explanation == "My Explination");
            Assert.IsTrue(classifiers.Classifiers[0].Owner == "me,IBM");
            Assert.IsTrue(classifiers.Classifiers[0].Created == "created");
            Assert.IsTrue(classifiers.Classifiers[0].Classes[0]._Class == "class");
        
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateClassifier_No_Name()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            Dictionary<string, byte[]> positiveExamples = new Dictionary<string, byte[]>();
            positiveExamples.Add("classifier", new byte[4]);

            byte[] negativeExamples = new byte[4];

            var classifier = service.CreateClassifier(null, positiveExamples, negativeExamples);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateClassifier_No_Positive_Examples()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            byte[] negativeExamples = new byte[4];

            var classifier = service.CreateClassifier("classifierName", null, negativeExamples);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateClassifier_Single_Positive_Example()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            Dictionary<string, byte[]> positiveExamples = new Dictionary<string, byte[]>();
            positiveExamples.Add("classifier", new byte[4]);

            var classifier = service.CreateClassifier(null, positiveExamples);
        }

        [TestMethod]
        public void CreateClassifier_Positive_And_Negative_Examples_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            GetClassifiersPerClassifierVerbose response = new GetClassifiersPerClassifierVerbose()
            {
                Name = "turtle-classifier",
                ClassifierId = "turtle-classifier-id",
                Status = "ready",
                Explanation = "My Explination"
            };

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(Arg.Any<HttpContent>())
                .Returns(request);

            request.As<GetClassifiersPerClassifierVerbose>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            Dictionary<string, byte[]> positiveExamples = new Dictionary<string, byte[]>();
            positiveExamples.Add("classifier", new byte[4]);

            byte[] negativeExamples = new byte[4];

            var classifier = service.CreateClassifier("classifierName", positiveExamples, negativeExamples);

            Assert.IsNotNull(classifier);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(classifier.Name == "turtle-classifier");
            Assert.IsTrue(classifier.ClassifierId == "turtle-classifier-id");
            Assert.IsTrue(classifier.Status == "ready");
            Assert.IsTrue(classifier.Explanation == "My Explination");
        }

        [TestMethod]
        public void CreateClassifier_Two_Positive_Examples_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            GetClassifiersPerClassifierVerbose response = new GetClassifiersPerClassifierVerbose()
            {
                Name = "turtle-classifier",
                ClassifierId = "turtle-classifier-id",
                Status = "ready",
                Explanation = "My Explination"
            };

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(Arg.Any<HttpContent>())
                .Returns(request);

            request.As<GetClassifiersPerClassifierVerbose>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            Dictionary<string, byte[]> positiveExamples = new Dictionary<string, byte[]>();
            positiveExamples.Add("className1", new byte[4]);
            positiveExamples.Add("className2", new byte[4]);

            var classifier = service.CreateClassifier("classifierName", positiveExamples);

            Assert.IsNotNull(classifier);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(classifier.Name == "turtle-classifier");
            Assert.IsTrue(classifier.ClassifierId == "turtle-classifier-id");
            Assert.IsTrue(classifier.Status == "ready");
            Assert.IsTrue(classifier.Explanation == "My Explination");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteClassifier_No_ClassifierId()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.DeleteClassifier(null);
        }

        [TestMethod]
        public void DeleteClassifier_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            object response = new object() { };

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);

            request.As<object>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var classifier = service.DeleteClassifier("classifierName");

            Assert.IsNotNull(classifier);
            client.Received().DeleteAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetClassifier_No_ClassifierId()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.GetClassifier(null);
        }

        [TestMethod]
        public void GetClassifier_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            GetClassifiersPerClassifierVerbose response = new GetClassifiersPerClassifierVerbose()
            {
                Name = "turtle-classifier",
                ClassifierId = "turtle-classifier-id",
                Status = "ready",
                Explanation = "My Explination"
            };

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);

            request.As<GetClassifiersPerClassifierVerbose>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var classifier = service.GetClassifier("classifierName");

            Assert.IsNotNull(classifier);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(classifier.Name == "turtle-classifier");
            Assert.IsTrue(classifier.ClassifierId == "turtle-classifier-id");
            Assert.IsTrue(classifier.Status == "ready");
            Assert.IsTrue(classifier.Explanation == "My Explination");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateClassifier_No_ClassifierId()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            Dictionary<string, byte[]> positiveExamples = new Dictionary<string, byte[]>();
            positiveExamples.Add("classifier", new byte[4]);

            byte[] negativeExamples = new byte[4];

            var result = service.UpdateClassifier(null, positiveExamples, negativeExamples);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateClassifier_No_Data()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.UpdateClassifier(null);
        }

        [TestMethod]
        public void UpdateClassifier_Positive_Examples_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            GetClassifiersPerClassifierVerbose response = new GetClassifiersPerClassifierVerbose()
            {
                Name = "turtle-classifier",
                ClassifierId = "turtle-classifier-id",
                Status = "ready",
                Explanation = "My Explination"
            };

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(Arg.Any<HttpContent>())
                .Returns(request);

            request.As<GetClassifiersPerClassifierVerbose>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            Dictionary<string, byte[]> positiveExamples = new Dictionary<string, byte[]>();
            positiveExamples.Add("className1", new byte[4]);

            var classifier = service.UpdateClassifier("classifierName", positiveExamples);

            Assert.IsNotNull(classifier);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(classifier.Name == "turtle-classifier");
            Assert.IsTrue(classifier.ClassifierId == "turtle-classifier-id");
            Assert.IsTrue(classifier.Status == "ready");
            Assert.IsTrue(classifier.Explanation == "My Explination");
        }

        [TestMethod]
        public void UpdateClassifier_Negative_Examples_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            GetClassifiersPerClassifierVerbose response = new GetClassifiersPerClassifierVerbose()
            {
                Name = "turtle-classifier",
                ClassifierId = "turtle-classifier-id",
                Status = "ready",
                Explanation = "My Explination"
            };

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(Arg.Any<HttpContent>())
                .Returns(request);

            request.As<GetClassifiersPerClassifierVerbose>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            byte[] negativeExamples = new byte[4];

            var classifier = service.UpdateClassifier("classifierName", null, negativeExamples);

            Assert.IsNotNull(classifier);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(classifier.Name == "turtle-classifier");
            Assert.IsTrue(classifier.ClassifierId == "turtle-classifier-id");
            Assert.IsTrue(classifier.Status == "ready");
            Assert.IsTrue(classifier.Explanation == "My Explination");
        }

        [TestMethod]
        public void UpdateClassifier_Positive_And_Negative_Examples_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            GetClassifiersPerClassifierVerbose response = new GetClassifiersPerClassifierVerbose()
            {
                Name = "turtle-classifier",
                ClassifierId = "turtle-classifier-id",
                Status = "ready",
                Explanation = "My Explination"
            };

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(Arg.Any<HttpContent>())
                .Returns(request);

            request.As<GetClassifiersPerClassifierVerbose>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            Dictionary<string, byte[]> positiveExamples = new Dictionary<string, byte[]>();
            positiveExamples.Add("className1", new byte[4]);

            byte[] negativeExamples = new byte[4];

            var classifier = service.UpdateClassifier("classifierName", positiveExamples, negativeExamples);

            Assert.IsNotNull(classifier);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(classifier.Name == "turtle-classifier");
            Assert.IsTrue(classifier.ClassifierId == "turtle-classifier-id");
            Assert.IsTrue(classifier.Status == "ready");
            Assert.IsTrue(classifier.Explanation == "My Explination");
        }
        #endregion

        #region Collections
        [TestMethod]
        public void GetCollections_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            GetCollections response = new GetCollections()
            {
                Collections = new List<CreateCollection>()
                {
                    new CreateCollection()
                    {
                        CollectionId = "collectionId",
                        Name = "collectionName",
                        Created = "createdDate",
                        Images = 1,
                        Status = "collectionStatus",
                        Capacity = "collectionCapacity"
                    }
                }
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);

            request.As<GetCollections>()
               .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.GetCollections();

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(result.Collections[0].CollectionId == "collectionId");
            Assert.IsTrue(result.Collections[0].Name == "collectionName");
            Assert.IsTrue(result.Collections[0].Created == "createdDate");
            Assert.IsTrue(result.Collections[0].Images == 1);
            Assert.IsTrue(result.Collections[0].Status == "collectionStatus");
            Assert.IsTrue(result.Collections[0].Capacity == "collectionCapacity");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateCollection_No_Name()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var collection = service.CreateCollection(null);
        }

        [TestMethod]
        public void CreateCollection_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            CreateCollection response = new CreateCollection()
            {
                CollectionId = "collectionId",
                Name = "collectionName",
                Created = "collectionCreated",
                Images = 1,
                Status = "collectionStatus",
                Capacity = "collectionCapacity"
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(Arg.Any<HttpContent>())
                .Returns(request);

            request.As<CreateCollection>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var collection = service.CreateCollection("collectionName");

            Assert.IsNotNull(collection);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(collection.Name == "collectionName");
            Assert.IsTrue(collection.CollectionId == "collectionId");
            Assert.IsTrue(collection.Status == "collectionStatus");
            Assert.IsTrue(collection.Created == "collectionCreated");
            Assert.IsTrue(collection.Images == 1);
            Assert.IsTrue(collection.Capacity == "collectionCapacity");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteCollection_No_CollectionId()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.DeleteCollection(null);
        }

        [TestMethod]
        public void DeleteCollection_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            object response = new object();

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);

            request.As<object>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var collection = service.DeleteCollection("collectionName");

            Assert.IsNotNull(collection);
            client.Received().DeleteAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetCollection_No_CollectionId()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.GetCollection(null);
        }

        [TestMethod]
        public void GetCollection_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            CreateCollection response = new CreateCollection()
            {
                CollectionId = "collectionId",
                Name = "collectionName",
                Created = "createdDate",
                Images = 1,
                Status = "collectionStatus",
                Capacity = "collectionCapacity"
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);

            request.As<CreateCollection>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var collection = service.GetCollection("collectionName");

            Assert.IsNotNull(collection);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(collection.CollectionId == "collectionId");
            Assert.IsTrue(collection.Name == "collectionName");
            Assert.IsTrue(collection.Created == "createdDate");
            Assert.IsTrue(collection.Images == 1);
            Assert.IsTrue(collection.Status == "collectionStatus");
            Assert.IsTrue(collection.Capacity == "collectionCapacity");
        }
        #endregion

        #region Images
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetCollectionImages_No_CollectionId()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.GetCollectionImages(null);
        }

        [TestMethod]
        public void GetCollectionImages_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            GetCollectionImages response = new GetCollectionImages()
            {
                Images = new List<GetCollectionsBrief>()
                {
                    new GetCollectionsBrief()
                    {
                        ImageId = "imageId",
                        Created = "created",
                        ImageFile = "imageFile"
                    }
                }
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);

            request.As<GetCollectionImages>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var collection = service.GetCollectionImages("collectionName");

            Assert.IsNotNull(collection);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(collection.Images[0].ImageId == "imageId");
            Assert.IsTrue(collection.Images[0].Created == "created");
            Assert.IsTrue(collection.Images[0].ImageFile == "imageFile");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddImage_No_CollectionId()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.AddImage(null, new byte[4], "imageFileName", new byte[4]);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddImage_No_ImageData_No_Filename_No_Metadata()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.AddImage("collectionId", null, null, null);
        }

        [TestMethod]
        public void AddImage_No_MetaData_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            CollectionsConfig response = new CollectionsConfig()
            {
                Images = new List<CollectionImagesConfig>()
                {
                    new CollectionImagesConfig()
                    {
                        ImageId = "imageId",
                        Created = "created",
                        ImageFile = "imageFile"
                    }
                },
                ImagesProcessed = 1
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(Arg.Any<HttpContent>())
                .Returns(request);

            request.As<CollectionsConfig>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var collection = service.AddImage("collectionName", new byte[4], "imageFilename.jpg");

            Assert.IsNotNull(collection);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(collection.Images[0].ImageId == "imageId");
            Assert.IsTrue(collection.Images[0].Created == "created");
            Assert.IsTrue(collection.Images[0].ImageFile == "imageFile");
        }

        [TestMethod]
        public void AddImage_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            CollectionsConfig response = new CollectionsConfig()
            {
                Images = new List<CollectionImagesConfig>()
                {
                    new CollectionImagesConfig()
                    {
                        ImageId = "imageId",
                        Created = "created",
                        ImageFile = "imageFile"
                    }
                },
                ImagesProcessed = 1
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(Arg.Any<HttpContent>())
                .Returns(request);

            request.As<CollectionsConfig>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var collection = service.AddImage("collectionName", new byte[4], "imageFilename.jpg", new byte[4]);

            Assert.IsNotNull(collection);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(collection.Images[0].ImageId == "imageId");
            Assert.IsTrue(collection.Images[0].Created == "created");
            Assert.IsTrue(collection.Images[0].ImageFile == "imageFile");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteImage_No_CollectionId_No_ImageId()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.DeleteImage(null, null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteImage_No_ImageId()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.DeleteImage("collectionId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteImage_No_CollectionId()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.DeleteImage(null, "imageId");
        }

        [TestMethod]
        public void DeleteImage_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            object response = new object();

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);

            request.As<object>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var collection = service.DeleteImage("collectionId", "ImageId");

            Assert.IsNotNull(collection);
            client.Received().DeleteAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetImage_No_CollectionId_No_ImageId()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.GetImage(null, null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetImage_No_ImageId()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.GetImage("collectionId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetImage_No_CollectionId()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.GetImage(null, "imageId");
        }

        [TestMethod]
        public void GetImage_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            GetCollectionsBrief response = new GetCollectionsBrief()
            {
                ImageId = "imageId",
                Created = "created",
                ImageFile = "imageFile",
                Metadata = new object() { }
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);

            request.As<GetCollectionsBrief>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var image = service.GetImage("collectionId", "imageId");

            Assert.IsNotNull(image);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(image.ImageId == "imageId");
            Assert.IsTrue(image.Created == "created");
            Assert.IsTrue(image.ImageFile == "imageFile");
        }
        #endregion

        #region Metadata
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteImageMetadata_No_CollectionId_No_ImageId()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.DeleteImageMetadata(null, null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteImageMetadata_No_ImageId()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.DeleteImageMetadata("collectionId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteImageMetadata_No_CollectionId()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.DeleteImageMetadata(null, "imageId");
        }

        [TestMethod]
        public void DeleteImageMetadata_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            object response = new object();

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);

            request.As<object>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var collection = service.DeleteImageMetadata("collectionId", "ImageId");

            Assert.IsNotNull(collection);
            client.Received().DeleteAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetImageMetadata_No_CollectionId_No_ImageId()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.GetMetadata(null, null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetImageMetadata_No_ImageId()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.GetMetadata("collectionId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetImageMetadata_No_CollectionId()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.GetMetadata(null, "imageId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddImageMetadata_No_CollectionId()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.AddImageMetadata(null, "imageId", new byte[4]);
        }


        [TestMethod]
        public void GetImageMetadata_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            Dictionary<string, string> response = new Dictionary<string, string>();
            response.Add("metadata", "test");

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);

            request.As<Dictionary<string, string>>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var metadata = service.GetMetadata("collectionId", "imageId");

            Assert.IsNotNull(metadata);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(metadata["metadata"] == "test");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddImageMetadata_No_ImageId()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.AddImageMetadata("collectionId", null, new byte[4]);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddImageMetadata_No_ImageId_No_Metadata()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.AddImageMetadata("collectionId", null, null);
        }

        [TestMethod]
        public void AddImageMetadata_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                  .Returns(request);

            Dictionary<string, string> response = new Dictionary<string, string>();
            response.Add("metadata", "test");

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(Arg.Any<HttpContent>())
                .Returns(request);

            request.As<Dictionary<string, string>>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var metadata = service.AddImageMetadata("collecionId", "imageId", new byte[4]);

            Assert.IsNotNull(metadata);
            client.Received().PutAsync(Arg.Any<string>());
            Assert.IsTrue(metadata["metadata"] == "test");
        }
        #endregion

        #region Find Similar
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]

        public void FindSimilar_No_CollectionId()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.FindSimilar(null, new byte[4], "imageFilename.jpg");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]

        public void FindSimilar_No_ImageData()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.FindSimilar("collectionId", null, "imageFilename.jpg");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]

        public void FindSimlar_No_ImageFilename()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var result = service.FindSimilar("collectionId", new byte[4], null);
        }

        [TestMethod]
        public void FindSimilar_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            SimilarImagesConfig response = new SimilarImagesConfig()
            {
                SimilarImages = new List<SimilarImageConfig>()
                {
                    new SimilarImageConfig()
                    {
                        ImageId = "imageId",
                        Created = "created",
                        ImageFile = "imageFile",
                        Score = 0.98f
                    }
                },
                ImagesProcessed = 1
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(Arg.Any<HttpContent>())
                .Returns(request);

            request.As<SimilarImagesConfig>()
                .Returns(Task.FromResult(response));

            VisualRecognitionService service =
                new VisualRecognitionService(client);

            var similarImages = service.FindSimilar("collectionName", new byte[4], "imageFilename.jpg");

            Assert.IsNotNull(similarImages);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(similarImages.SimilarImages[0].ImageId == "imageId");
            Assert.IsTrue(similarImages.SimilarImages[0].Created == "created");
            Assert.IsTrue(similarImages.SimilarImages[0].ImageFile == "imageFile");
            Assert.IsTrue(similarImages.SimilarImages[0].Score == 0.98f);
        }
        #endregion
    }
}
