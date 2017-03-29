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
using IBM.WatsonDeveloperCloud.VisualRecognition.v3;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model;
using IBM.WatsonDeveloperCloud.Http.Extensions;
using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.VisualRecognition.Example
{

    public class VisualRecognitionServiceExample
    {
        private VisualRecognitionService _visualRecognition = new VisualRecognitionService();
        private string _imageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/bb/Kittyply_edit1.jpg/1200px-Kittyply_edit1.jpg";
        private string _faceUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/8d/President_Barack_Obama.jpg/220px-President_Barack_Obama.jpg";
        private string _localGiraffeFilePath = @"exampleData\giraffe_to_classify.jpg";
        private string _localImageMetadataPath = @"exampleData\imageMetadata.json";
        private string _localFaceFilePath = @"exampleData\obama.jpg";
        private string _localTurtleFilePath = @"exampleData\turtle_to_classify.jpg";
        private string _localGiraffePositiveExamplesFilePath = @"exampleData\giraffe_positive_examples.zip";
        private string _giraffeClassname = "giraffe_positive_examples";
        private string _localTurtlePositiveExamplesFilePath = @"exampleData\turtle_positive_examples.zip";
        private string _turtleClassname = "turtle_positive_examples";
        private string _localNegativeExamplesFilePath = @"exampleData\negative_examples.zip";
        private string _createdClassifierName = "dotnet-standard-test-classifier";
        private string _collectionNameToCreate = "dotnet-standard-test-collection";

        public VisualRecognitionServiceExample(string apikey)
        {
            _visualRecognition.SetCredential(apikey);

            //ClassifyGet();
            //ClassifyPost();
            //DetectFacesGet();
            //DetectFacesPost();
            //GetClassifiersBrief();
            //GetClassifiersVerbose();
            //CreateClassifier();
            //DeleteClassifier();
            //GetClassifier();
            //UpdateClassifier();
            //GetCollections();
            //CreateCollection();
            //DeleteCollection();
            //GetCollection();
            //GetCollectionImages();
            //AddCollectionImages();
            //DeleteCollectionImage();
            //GetCollectionImage();
            //DeleteCollectionImageMetadata();
            //GetCollectionImageMetadata();
            AddCollectionImageMetadata();
            //FindSimilar();
        }

        private void ClassifyGet()
        {
            Console.WriteLine(string.Format("Calling Classify(\"{0}\")...", _imageUrl));
            var result = _visualRecognition.Classify(_imageUrl);

            if (result != null)
            {
                foreach (ClassifyTopLevelSingle image in result.Images)
                    foreach (ClassifyPerClassifier classifier in image.Classifiers)
                        foreach (ClassResult classResult in classifier.Classes)
                            Console.WriteLine(string.Format("class: {0} | score: {1} | type hierarchy: {2}", classResult._Class, classResult.Score, classResult.TypeHierarchy));
            }
            else
            {
                Console.WriteLine("Classify() result is null.");
            }
        }

        private void ClassifyPost()
        {
            using (FileStream fs = File.OpenRead(_localGiraffeFilePath))
            {
                Console.WriteLine(string.Format("Calling Classify(\"{0}\")...", _localGiraffeFilePath));
                var result = _visualRecognition.Classify((fs as Stream).ReadAllBytes(), Path.GetFileName(_localGiraffeFilePath), "image/jpeg");

                foreach (Classifiers image in result.Images)
                    foreach (ClassifyPerClassifier classifier in image.classifiers)
                        foreach (ClassResult classResult in classifier.Classes)
                            Console.WriteLine(string.Format("class: {0} | score: {1} | type hierarchy: {2}", classResult._Class, classResult.Score, classResult.TypeHierarchy));
            }
        }

        private void DetectFacesGet()
        {
            Console.WriteLine(string.Format("Calling DetectFaces(\"{0}\")...", _faceUrl));
            var result = _visualRecognition.DetectFaces(_faceUrl);

            if (result != null)
            {
                foreach (FacesTopLevelSingle image in result.Images)
                    foreach (OneFaceResult face in image.Faces)
                    {
                        if (face.Identity != null)
                            Console.WriteLine(string.Format("name: {0} | score: {1} | type hierarchy: {2}", face.Identity.Name, face.Identity.Score, face.Identity.TypeHierarchy));
                        else
                            Console.WriteLine("identity is null.");

                        if (face.Age != null)
                            Console.WriteLine(string.Format("Age: {0} - {1} | score: {2}", face.Age.Min, face.Age.Max, face.Age.Score));
                        else
                            Console.WriteLine("age is null.");

                        if (face.Gender != null)
                            Console.WriteLine(string.Format("gender: {0} | score: {1}", face.Gender.Gender, face.Gender.Score));
                        else
                            Console.WriteLine("gender is null.");
                    }
            }
            else
            {
                Console.WriteLine("DetectFaces() result is null.");
            }
        }

        private void DetectFacesPost()
        {
            using (FileStream fs = File.OpenRead(_localFaceFilePath))
            {
                Console.WriteLine(string.Format("Calling DetectFaces(\"{0}\")...", _localFaceFilePath));
                var result = _visualRecognition.DetectFaces((fs as Stream).ReadAllBytes(), Path.GetFileName(_localFaceFilePath), "image/jpeg");

                if (result != null)
                {
                    foreach (FacesTopLevelSingle image in result.Images)
                        foreach (OneFaceResult face in image.Faces)
                        {
                            if (face.Identity != null)
                                Console.WriteLine(string.Format("name: {0} | score: {1} | type hierarchy: {2}", face.Identity.Name, face.Identity.Score, face.Identity.TypeHierarchy));
                            else
                                Console.WriteLine("identity is null.");

                            if (face.Age != null)
                                Console.WriteLine(string.Format("Age: {0} - {1} | score: {2}", face.Age.Min, face.Age.Max, face.Age.Score));
                            else
                                Console.WriteLine("age is null.");

                            if (face.Gender != null)
                                Console.WriteLine(string.Format("gender: {0} | score: {1}", face.Gender.Gender, face.Gender.Score));
                            else
                                Console.WriteLine("gender is null.");
                        }
                }
                else
                {
                    Console.WriteLine("DetectFaces() result is null.");
                }
            }
        }

        private void GetClassifiersBrief()
        {
            Console.WriteLine("Calling GetClassifiersBrief()...");

            var result = _visualRecognition.GetClassifiersBrief();

            if (result != null)
            {
                foreach (GetClassifiersPerClassifierBrief classifier in result.Classifiers)
                    Console.WriteLine(string.Format("name: {0} | id: {1} | status: {2}", classifier.Name, classifier.ClassifierId, classifier.Status));
            }
            else
            {
                Console.WriteLine("GetClassifiers() result is null.");
            }
        }

        private void GetClassifiersVerbose()
        {
            Console.WriteLine("Calling GetClassifiersVerbose()...");

            var result = _visualRecognition.GetClassifiersVerbose();

            if (result != null)
            {
                foreach (GetClassifiersPerClassifierVerbose classifier in result.Classifiers)
                    Console.WriteLine(string.Format("name: {0} | id: {1} | status: {2}", classifier.Name, classifier.ClassifierId, classifier.Status));
            }
            else
            {
                Console.WriteLine("GetClassifiers() result is null.");
            }
        }

        private void CreateClassifier()
        {
            using (FileStream positiveExamplesStream = File.OpenRead(_localGiraffePositiveExamplesFilePath), negativeExamplesStream = File.OpenRead(_localNegativeExamplesFilePath))
            {
                Console.WriteLine(string.Format("Calling CreateClassifier(\"{0}\")", _createdClassifierName));

                Dictionary<string, byte[]> positiveExamples = new Dictionary<string, byte[]>();
                positiveExamples.Add(_giraffeClassname, positiveExamplesStream.ReadAllBytes());

                var result = _visualRecognition.CreateClassifier(_createdClassifierName, positiveExamples, negativeExamplesStream.ReadAllBytes());

                if (result != null)
                {
                    Console.WriteLine(string.Format("name: {0} | classifierID: {1} | status: {2}", result.Name, result.ClassifierId, result.Status));
                    foreach (ModelClass _class in result.Classes)
                        Console.WriteLine(string.Format("\tclass: {0}", _class._Class));
                }
                else
                {
                    Console.WriteLine("Result is null.");
                }
            }
        }

        private void DeleteClassifier()
        {
            string classifierToDelete = "";
            if (string.IsNullOrEmpty(classifierToDelete))
                throw new ArgumentNullException(nameof(classifierToDelete));

            Console.WriteLine(string.Format("Calling DeleteClassifier(\"{0}\")...", classifierToDelete));

            var result = _visualRecognition.DeleteClassifier(classifierToDelete);
        }

        private void GetClassifier()
        {
            string classifierToGet = "";
            if (string.IsNullOrEmpty(classifierToGet))
                throw new ArgumentNullException(nameof(classifierToGet));

            Console.WriteLine(string.Format("Calling GetClassifier(\"{0}\")...", classifierToGet));

            var result = _visualRecognition.GetClassifier(classifierToGet);

            if (result != null)
            {
                Console.WriteLine(string.Format("name: {0} | id: {1} | status: {2}", result.Name, result.ClassifierId, result.Status));

                if (result.Classes != null && result.Classes.Count > 0)
                {
                    foreach (ModelClass _class in result.Classes)
                        Console.WriteLine(string.Format("\tclass: {0}", _class._Class));
                }
                else
                {
                    Console.WriteLine("There are no classes.");
                }
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        private void UpdateClassifier()
        {
            string classifierToUpdate = "";

            if (string.IsNullOrEmpty(classifierToUpdate))
                throw new ArgumentNullException(nameof(classifierToUpdate));

            using (FileStream positiveExamplesStream = File.OpenRead(_localTurtlePositiveExamplesFilePath))
            {
                Console.WriteLine(string.Format("Calling UpdateClassifier(\"{0}\", \"{1}\")...", classifierToUpdate, _localTurtlePositiveExamplesFilePath));

                Dictionary<string, byte[]> positiveExamples = new Dictionary<string, byte[]>();
                positiveExamples.Add(_turtleClassname, positiveExamplesStream.ReadAllBytes());

                var result = _visualRecognition.UpdateClassifier(classifierToUpdate, positiveExamples);

                if (result != null)
                {
                    Console.WriteLine(string.Format("name: {0} | classifierID: {1} | status: {2}", result.Name, result.ClassifierId, result.Status));
                    foreach (ModelClass _class in result.Classes)
                        Console.WriteLine(string.Format("\tclass: {0}", _class._Class));
                }
                else
                {
                    Console.WriteLine("Result is null.");
                }
            }
        }

        private void GetCollections()
        {
            Console.WriteLine("Calling GetCollections()...");

            var result = _visualRecognition.GetCollections();

            if (result != null)
            {
                if (result.Collections != null && result.Collections.Count > 0)
                {
                    foreach (CreateCollection collection in result.Collections)
                        Console.WriteLine(string.Format("name: {0} | collection id: {1} | status: {2}", collection.Name, collection.CollectionId, collection.Status));
                }
                else
                {
                    Console.WriteLine("There are no collections.");
                }
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        private void CreateCollection()
        {
            Console.WriteLine("Calling CreateCollection()...");

            var result = _visualRecognition.CreateCollection(_collectionNameToCreate);

            if (result != null)
            {
                Console.WriteLine(string.Format("name: {0} | collection id: {1} | status: {2}", result.Name, result.CollectionId, result.Status));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        private void DeleteCollection()
        {
            string collectionToDelete = "";
            if (string.IsNullOrEmpty(collectionToDelete))
                throw new ArgumentNullException(nameof(collectionToDelete));

            Console.WriteLine(string.Format("Calling DeleteCollection(\"{0}\")...", collectionToDelete));

            var result = _visualRecognition.DeleteCollection(collectionToDelete);
        }

        private void GetCollection()
        {
            string collectionToGet = "swift-sdk-unit-test-faces_dd0040";
            if (string.IsNullOrEmpty(collectionToGet))
                throw new ArgumentNullException(nameof(collectionToGet));

            Console.WriteLine(string.Format("Calling GetCollection(\"{0}\")...", collectionToGet));

            var result = _visualRecognition.GetCollection(collectionToGet);

            if (result != null)
            {
                Console.WriteLine(string.Format("name: {0} | collection id: {1} | status: {2}", result.Name, result.CollectionId, result.Status));

            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        private void GetCollectionImages()
        {
            string collectionToGetImages = "dotnet-standard-test-collection_6cb25d";
            if (string.IsNullOrEmpty(collectionToGetImages))
                throw new ArgumentNullException(nameof(collectionToGetImages));

            Console.WriteLine(string.Format("Calling GetCollectionImages(\"{0}\")...", collectionToGetImages));

            var result = _visualRecognition.GetCollectionImages(collectionToGetImages);

            if (result != null)
            {
                if (result.Images != null && result.Images.Count > 0)
                {
                    foreach (GetCollectionsBrief image in result.Images)
                        Console.WriteLine(string.Format("imageId: {0} | imageFile: {1} | created: {2} | metadata{3}", image.ImageId, image.ImageFile, image.Created, image.Metadata));
                }
                else
                {
                    Console.WriteLine("There are no images.");
                }
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        private void AddCollectionImages()
        {
            string collectionId = "dotnet-standard-test-collection_6cb25d";
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));

            using (FileStream imageStream = File.OpenRead(_localGiraffeFilePath), metadataStream = File.OpenRead(_localImageMetadataPath))
            {
                Console.WriteLine(string.Format("Calling AddImage(\"{0}\", \"{1}\")...", collectionId, _localGiraffeFilePath));

                var result = _visualRecognition.AddImage(collectionId, imageStream.ReadAllBytes(), Path.GetFileName(_localGiraffeFilePath), metadataStream.ReadAllBytes());

                if (result != null)
                {
                    Console.WriteLine("Number of images processed: {0}", result.ImagesProcessed);
                    foreach (CollectionImagesConfig image in result.Images)
                    {
                        Console.WriteLine("file: {0} | id: {1}", image.ImageFile, image.ImageId);

                        if(image.Metadata != null && image.Metadata.Count > 0)
                        {
                            foreach (KeyValuePair<string, string> kvp in image.Metadata)
                                Console.WriteLine("\t{0} : {1}", kvp.Key, kvp.Value);
                        }
                        else
                        {
                            Console.WriteLine("There is no metadata.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Result is null.");
                }
            }
        }

        private void DeleteCollectionImage()
        {
            string collectionId = "";
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));

            string imageId = "";
            if (string.IsNullOrEmpty(imageId))
                throw new ArgumentNullException(nameof(imageId));

            Console.WriteLine(string.Format("Calling DeleteImage(\"{0}\", \"{1}\")...", collectionId, imageId));

            var result = _visualRecognition.DeleteImage(collectionId, imageId);
        }

        private void GetCollectionImage()
        {
            string collectionToGetImage = "swift-sdk-unit-test-faces_dd0040";
            string collectionImageToGet = "4fbc3c";

            if (string.IsNullOrEmpty(collectionToGetImage))
                throw new ArgumentNullException(nameof(collectionToGetImage));

            if (string.IsNullOrEmpty(collectionImageToGet))
                throw new ArgumentNullException(nameof(collectionImageToGet));

            Console.WriteLine(string.Format("Calling GetCollectionImages(\"{0}\", \"{1}\")...", collectionToGetImage, collectionImageToGet));

            var result = _visualRecognition.GetImage(collectionToGetImage, collectionImageToGet);

            if (result != null)
            {
                Console.WriteLine(string.Format("imageId: {0} | imageFile: {1} | created: {2} | metadata{3}", result.ImageId, result.ImageFile, result.Created, result.Metadata));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        private void DeleteCollectionImageMetadata()
        {
            string collectionId = "";
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));

            string imageId = "";
            if (string.IsNullOrEmpty(imageId))
                throw new ArgumentNullException(nameof(imageId));

            Console.WriteLine(string.Format("Calling DeleteImageMetadata(\"{0}\", \"{1}\")...", collectionId, imageId));

            var result = _visualRecognition.DeleteImageMetadata(collectionId, imageId);
        }

        private void GetCollectionImageMetadata()
        {
            string collectionToGetImage = "swift-sdk-unit-test-faces_dd0040";
            string collectionImageToGetMetadata = "4fbc3c";

            if (string.IsNullOrEmpty(collectionToGetImage))
                throw new ArgumentNullException(nameof(collectionToGetImage));

            if (string.IsNullOrEmpty(collectionImageToGetMetadata))
                throw new ArgumentNullException(nameof(collectionImageToGetMetadata));

            Console.WriteLine(string.Format("Calling GetMetadata(\"{0}\", \"{1}\")...", collectionToGetImage, collectionImageToGetMetadata));

            var result = _visualRecognition.GetMetadata(collectionToGetImage, collectionImageToGetMetadata);

            if (result != null)
            {
                foreach (KeyValuePair<string, string> item in result)
                    Console.WriteLine(string.Format("Metadata: {0}, {1}", item.Key, item.Value));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        private void AddCollectionImageMetadata()
        {
            string collectionToGetImage = "dotnet-standard-test-collection_6cb25d";
            string collectionImageToAddMetadata = "6aee1c";

            if (string.IsNullOrEmpty(collectionToGetImage))
                throw new ArgumentNullException(nameof(collectionToGetImage));

            if (string.IsNullOrEmpty(collectionImageToAddMetadata))
                throw new ArgumentNullException(nameof(collectionImageToAddMetadata));

            using (FileStream metadataStream = File.OpenRead(_localImageMetadataPath))
            {
                Console.WriteLine(string.Format("Calling AddMetadata(\"{0}\", \"{1}\")...", collectionToGetImage, collectionImageToAddMetadata));

                var result = _visualRecognition.AddImageMetadata(collectionToGetImage, collectionImageToAddMetadata, metadataStream.ReadAllBytes());

                if (result != null && result.Count > 0)
                {
                    foreach (KeyValuePair<string, string> kvp in result)
                        Console.WriteLine("\t{0} : {1}", kvp.Key, kvp.Value);
                }
                else
                {
                    Console.WriteLine("Result is null.");
                }
            }

        }

        private void FindSimilar()
        {
            throw new NotImplementedException();
        }
    }
}
