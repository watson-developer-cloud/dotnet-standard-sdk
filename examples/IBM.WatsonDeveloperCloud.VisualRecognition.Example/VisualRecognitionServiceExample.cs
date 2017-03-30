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
using System.Threading;
using System.Threading.Tasks;

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
        private string _giraffeClassname = "giraffe";
        private string _localTurtlePositiveExamplesFilePath = @"exampleData\turtle_positive_examples.zip";
        private string _turtleClassname = "turtle";
        private string _localNegativeExamplesFilePath = @"exampleData\negative_examples.zip";
        private string _createdClassifierName = "dotnet-standard-test-classifier";
        private string _createdClassifierId = "";
        private string _collectionNameToCreate = "dotnet-standard-test-collection";
        private string _createdCollectionId = "";
        private string _addedImageId = "";
        AutoResetEvent autoEvent = new AutoResetEvent(false);

        public VisualRecognitionServiceExample(string apikey)
        {
            _visualRecognition.SetCredential(apikey);

            ClassifyGet();
            ClassifyPost();
            DetectFacesGet();
            DetectFacesPost();
            GetClassifiersBrief();
            GetClassifiersVerbose();

            CreateClassifier();
            IsClassifierReady(_createdClassifierId);
            autoEvent.WaitOne();
            UpdateClassifier();
            GetCollections();

            DeleteDotnetCollections();

            CreateCollection();
            GetCollection();
            GetCollectionImages();
            AddCollectionImages();
            GetCollectionImage();
            GetCollectionImageMetadata();
            AddCollectionImageMetadata();
            DeleteCollectionImageMetadata();
            FindSimilar();
            DeleteCollectionImage();
            DeleteCollection();
            DeleteClassifier();

            Console.WriteLine("\n\nOperation complete");
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
                Console.WriteLine(string.Format("\nCalling Classify(\"{0}\")...", _localGiraffeFilePath));
                var result = _visualRecognition.Classify((fs as Stream).ReadAllBytes(), Path.GetFileName(_localGiraffeFilePath), "image/jpeg");

                foreach (Classifiers image in result.Images)
                    foreach (ClassifyPerClassifier classifier in image.classifiers)
                        foreach (ClassResult classResult in classifier.Classes)
                            Console.WriteLine(string.Format("class: {0} | score: {1} | type hierarchy: {2}", classResult._Class, classResult.Score, classResult.TypeHierarchy));
            }
        }

        private void DetectFacesGet()
        {
            Console.WriteLine(string.Format("\nCalling DetectFaces(\"{0}\")...", _faceUrl));
            var result = _visualRecognition.DetectFaces(_faceUrl);

            if (result != null)
            {
                if (result.Images != null && result.Images.Count > 0)
                {
                    foreach (FacesTopLevelSingle image in result.Images)
                    {
                        if (image.Faces != null && image.Faces.Count < 0)
                        {
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
                    }
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
                Console.WriteLine(string.Format("\nCalling DetectFaces(\"{0}\")...", _localFaceFilePath));
                var result = _visualRecognition.DetectFaces((fs as Stream).ReadAllBytes(), Path.GetFileName(_localFaceFilePath), "image/jpeg");

                if (result != null)
                {
                    if (result.Images != null && result.Images.Count > 0)
                    {
                        foreach (FacesTopLevelSingle image in result.Images)
                        {
                            if (image.Faces != null && image.Faces.Count < 0)
                            {
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
                        }
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
            Console.WriteLine("\nCalling GetClassifiersBrief()...");

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
            Console.WriteLine("\nCalling GetClassifiersVerbose()...");

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
                Console.WriteLine(string.Format("\nCalling CreateClassifier(\"{0}\")", _createdClassifierName));

                Dictionary<string, byte[]> positiveExamples = new Dictionary<string, byte[]>();
                positiveExamples.Add(_giraffeClassname, positiveExamplesStream.ReadAllBytes());

                var result = _visualRecognition.CreateClassifier(_createdClassifierName, positiveExamples, negativeExamplesStream.ReadAllBytes());

                if (result != null)
                {
                    Console.WriteLine(string.Format("name: {0} | classifierID: {1} | status: {2}", result.Name, result.ClassifierId, result.Status));
                    foreach (ModelClass _class in result.Classes)
                        Console.WriteLine(string.Format("class: {0}", _class._Class));

                    _createdClassifierId = result.ClassifierId;
                }
                else
                {
                    Console.WriteLine("Result is null.");
                }
            }
        }

        private void DeleteClassifier()
        {
            if (string.IsNullOrEmpty(_createdClassifierId))
                throw new ArgumentNullException(nameof(_createdClassifierId));

            Console.WriteLine(string.Format("\nCalling DeleteClassifier(\"{0}\")...", _createdClassifierId));

            var result = _visualRecognition.DeleteClassifier(_createdClassifierId);

            if (result != null)
                Console.WriteLine(string.Format("Classifier {0} deleted.", _createdClassifierId));
        }

        private void GetClassifier()
        {
            if (string.IsNullOrEmpty(_createdClassifierId))
                throw new ArgumentNullException(nameof(_createdClassifierId));

            Console.WriteLine(string.Format("\nCalling GetClassifier(\"{0}\")...", _createdClassifierId));

            var result = _visualRecognition.GetClassifier(_createdClassifierId);

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
            if (string.IsNullOrEmpty(_createdClassifierId))
                throw new ArgumentNullException(nameof(_createdClassifierId));

            using (FileStream positiveExamplesStream = File.OpenRead(_localTurtlePositiveExamplesFilePath))
            {
                Console.WriteLine(string.Format("\nCalling UpdateClassifier(\"{0}\", \"{1}\")...", _createdClassifierId, _localTurtlePositiveExamplesFilePath));

                Dictionary<string, byte[]> positiveExamples = new Dictionary<string, byte[]>();
                positiveExamples.Add(_turtleClassname, positiveExamplesStream.ReadAllBytes());

                var result = _visualRecognition.UpdateClassifier(_createdClassifierId, positiveExamples);

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
            Console.WriteLine("\nCalling GetCollections()...");

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
            Console.WriteLine(string.Format("\nCalling CreateCollection(\"{0}\")...", _collectionNameToCreate));

            var result = _visualRecognition.CreateCollection(_collectionNameToCreate);

            if (result != null)
            {
                Console.WriteLine(string.Format("name: {0} | collection id: {1} | status: {2}", result.Name, result.CollectionId, result.Status));

                _createdCollectionId = result.CollectionId;
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        private void DeleteCollection()
        {
            if (string.IsNullOrEmpty(_createdCollectionId))
                throw new ArgumentNullException(nameof(_createdCollectionId));

            Console.WriteLine(string.Format("\nCalling DeleteCollection(\"{0}\")...", _createdCollectionId));

            var result = _visualRecognition.DeleteCollection(_createdCollectionId);

            if (result != null)
                Console.WriteLine(string.Format("Collection {0} deleted.", _createdCollectionId));
        }

        private void GetCollection()
        {
            if (string.IsNullOrEmpty(_createdCollectionId))
                throw new ArgumentNullException(nameof(_createdCollectionId));

            Console.WriteLine(string.Format("\nCalling GetCollection(\"{0}\")...", _createdCollectionId));

            var result = _visualRecognition.GetCollection(_createdCollectionId);

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
            if (string.IsNullOrEmpty(_createdCollectionId))
                throw new ArgumentNullException(nameof(_createdCollectionId));

            Console.WriteLine(string.Format("\nCalling GetCollectionImages(\"{0}\")...", _createdCollectionId));

            var result = _visualRecognition.GetCollectionImages(_createdCollectionId);

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
            if (string.IsNullOrEmpty(_createdCollectionId))
                throw new ArgumentNullException(nameof(_createdCollectionId));

            using (FileStream imageStream = File.OpenRead(_localGiraffeFilePath), metadataStream = File.OpenRead(_localImageMetadataPath))
            {
                Console.WriteLine(string.Format("\nCalling AddImage(\"{0}\", \"{1}\")...", _createdCollectionId, _localGiraffeFilePath));

                var result = _visualRecognition.AddImage(_createdCollectionId, imageStream.ReadAllBytes(), Path.GetFileName(_localGiraffeFilePath), metadataStream.ReadAllBytes());

                if (result != null)
                {
                    Console.WriteLine("Number of images processed: {0}", result.ImagesProcessed);
                    foreach (CollectionImagesConfig image in result.Images)
                    {
                        Console.WriteLine("file: {0} | id: {1}", image.ImageFile, image.ImageId);

                        if (image.Metadata != null && image.Metadata.Count > 0)
                        {
                            foreach (KeyValuePair<string, string> kvp in image.Metadata)
                                Console.WriteLine("\t{0} : {1}", kvp.Key, kvp.Value);
                        }
                        else
                        {
                            Console.WriteLine("There is no metadata for this image.");
                        }

                        _addedImageId = image.ImageId;
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
            if (string.IsNullOrEmpty(_createdCollectionId))
                throw new ArgumentNullException(nameof(_createdCollectionId));

            if (string.IsNullOrEmpty(_addedImageId))
                throw new ArgumentNullException(nameof(_addedImageId));

            Console.WriteLine(string.Format("\nCalling DeleteImage(\"{0}\", \"{1}\")...", _createdCollectionId, _addedImageId));

            var result = _visualRecognition.DeleteImage(_createdCollectionId, _addedImageId);

            if (result != null)
                Console.WriteLine(string.Format("Image {0} deleted from collection {1}.", _addedImageId, _createdCollectionId));
        }

        private void GetCollectionImage()
        {
            if (string.IsNullOrEmpty(_createdCollectionId))
                throw new ArgumentNullException(nameof(_createdCollectionId));

            if (string.IsNullOrEmpty(_addedImageId))
                throw new ArgumentNullException(nameof(_addedImageId));

            Console.WriteLine(string.Format("\nCalling GetCollectionImages(\"{0}\", \"{1}\")...", _createdCollectionId, _addedImageId));

            var result = _visualRecognition.GetImage(_createdCollectionId, _addedImageId);

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
            if (string.IsNullOrEmpty(_createdCollectionId))
                throw new ArgumentNullException(nameof(_createdCollectionId));

            if (string.IsNullOrEmpty(_addedImageId))
                throw new ArgumentNullException(nameof(_addedImageId));

            Console.WriteLine(string.Format("\nCalling DeleteImageMetadata(\"{0}\", \"{1}\")...", _createdCollectionId, _addedImageId));

            var result = _visualRecognition.DeleteImageMetadata(_createdCollectionId, _addedImageId);
        }

        private void GetCollectionImageMetadata()
        {
            if (string.IsNullOrEmpty(_createdCollectionId))
                throw new ArgumentNullException(nameof(_createdCollectionId));

            if (string.IsNullOrEmpty(_addedImageId))
                throw new ArgumentNullException(nameof(_addedImageId));

            Console.WriteLine(string.Format("\nCalling GetMetadata(\"{0}\", \"{1}\")...", _createdCollectionId, _addedImageId));

            var result = _visualRecognition.GetMetadata(_createdCollectionId, _addedImageId);

            if (result != null)
            {
                foreach (KeyValuePair<string, string> item in result)
                    Console.WriteLine(string.Format("\tMetadata: {0}, {1}", item.Key, item.Value));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        private void AddCollectionImageMetadata()
        {
            if (string.IsNullOrEmpty(_createdCollectionId))
                throw new ArgumentNullException(nameof(_createdCollectionId));

            if (string.IsNullOrEmpty(_addedImageId))
                throw new ArgumentNullException(nameof(_addedImageId));

            using (FileStream metadataStream = File.OpenRead(_localImageMetadataPath))
            {
                Console.WriteLine(string.Format("\nCalling AddMetadata(\"{0}\", \"{1}\")...", _createdCollectionId, _addedImageId));

                var result = _visualRecognition.AddImageMetadata(_createdCollectionId, _addedImageId, metadataStream.ReadAllBytes());

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
            if (string.IsNullOrEmpty(_createdCollectionId))
                throw new ArgumentNullException(nameof(_createdCollectionId));

            using (FileStream imageStream = File.OpenRead(_localTurtleFilePath))
            {
                Console.WriteLine(string.Format("\nCalling FindSimilar(\"{0}\", \"{1}\")...", _createdCollectionId, _localTurtleFilePath));

                var result = _visualRecognition.FindSimilar(_createdCollectionId, imageStream.ReadAllBytes(), Path.GetFileName(_localGiraffeFilePath));

                if (result != null)
                {
                    Console.WriteLine("Number of images processed: {0}", result.ImagesProcessed);
                    foreach (SimilarImageConfig image in result.SimilarImages)
                    {
                        Console.WriteLine("file: {0} | id: {1} | score: {2}", image.ImageFile, image.ImageId, image.Score);

                        if (image.Metadata != null && image.Metadata.Count > 0)
                        {
                            foreach (KeyValuePair<string, string> kvp in image.Metadata)
                                Console.WriteLine("\t{0} : {1}", kvp.Key, kvp.Value);
                        }
                        else
                        {
                            Console.WriteLine("There is no metadata for this image.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Result is null.");
                }
            }
        }

        private void DeleteAllClassifiers()
        {
            Console.WriteLine("Getting classifiers");
            var classifiers = _visualRecognition.GetClassifiersBrief();

            List<string> classifierIds = new List<string>();

            foreach (GetClassifiersPerClassifierBrief classifier in classifiers.Classifiers)
                classifierIds.Add(classifier.ClassifierId);

            Console.WriteLine(string.Format("Deleting {0} classifiers", classifierIds.Count));

            foreach (string classifierId in classifierIds)
            {
                Console.WriteLine(string.Format("Deleting classifier {0}", classifierId));
                _visualRecognition.DeleteClassifier(classifierId);
                Console.WriteLine(string.Format("\tClassifier {0} deleted", classifierId));
            }

            Console.WriteLine("Operation complete");
        }

        private bool IsClassifierReady(string classifierId)
        {
            var result = _visualRecognition.GetClassifier(classifierId);
            Console.WriteLine(string.Format("\tClassifier {0} status is {1}.", classifierId, result.Status));

            if (result.Status.ToLower() == "ready")
                autoEvent.Set();
            else
            {
                Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(5000);
                    IsClassifierReady(classifierId);
                });
            }

            return result.Status.ToLower() == "ready";
        }

        private void DeleteDotnetCollections()
        {
            Console.WriteLine("\nGetting all collections");
            List<string> collectionIdsToDelete = new List<string>();

            var collections = _visualRecognition.GetCollections();

            foreach (CreateCollection collection in collections.Collections)
            {
                string name = collection.Name;
                string id = collection.CollectionId;
                Console.WriteLine(string.Format("name: {0} | id: {1}", name, id));

                if (name == _collectionNameToCreate)
                    collectionIdsToDelete.Add(id);
            }

            if (collectionIdsToDelete.Count > 0)
            {
                foreach (string collectionIdToDelete in collectionIdsToDelete)
                {
                    Console.WriteLine(string.Format("Deleting collection {0}.", collectionIdToDelete));
                    _visualRecognition.DeleteCollection(collectionIdToDelete);
                    Console.WriteLine(string.Format("\tCollection {0} deleted.", collectionIdToDelete));
                }
            }
            else
            {
                Console.WriteLine("There are no matching collections to delete.");
            }
        }
    }
}
