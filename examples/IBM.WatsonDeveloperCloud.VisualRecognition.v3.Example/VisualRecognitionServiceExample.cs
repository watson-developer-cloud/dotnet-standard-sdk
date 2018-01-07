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

using IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model;
using IBM.WatsonDeveloperCloud.Http.Extensions;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.VisualRecognition.v3.Example
{

    public class VisualRecognitionServiceExample
    {
        private VisualRecognitionService _visualRecognition;
        private string _imageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/bb/Kittyply_edit1.jpg/1200px-Kittyply_edit1.jpg";
        private string _faceUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/8d/President_Barack_Obama.jpg/220px-President_Barack_Obama.jpg";
        private string _localGiraffeFilePath = @"VisualRecognitionTestData\giraffe_to_classify.jpg";
        private string _localFaceFilePath = @"VisualRecognitionTestData\obama.jpg";
        private string _localTurtleFilePath = @"VisualRecognitionTestData\turtle_to_classify.jpg";
        private string _localGiraffePositiveExamplesFilePath = @"VisualRecognitionTestData\giraffe_positive_examples.zip";
        private string _giraffeClassname = "giraffe";
        private string _localTurtlePositiveExamplesFilePath = @"VisualRecognitionTestData\turtle_positive_examples.zip";
        private string _turtleClassname = "turtle";
        private string _localNegativeExamplesFilePath = @"VisualRecognitionTestData\negative_examples.zip";
        private string _createdClassifierName = "dotnet-standard-test-classifier";
        private string _createdClassifierId = "";
        AutoResetEvent autoEvent = new AutoResetEvent(false);

        public VisualRecognitionServiceExample(string url, string apikey)
        {
            _visualRecognition = new VisualRecognitionService(apikey, url);

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
            IsClassifierReady(_createdClassifierId);
            autoEvent.WaitOne();
            ClassifyWithClassifier();
            GetClassifiersVerbose();
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
                    foreach (ClassifyPerClassifier classifier in image._Classifiers)
                        foreach (ClassResult classResult in classifier.Classes)
                            Console.WriteLine(string.Format("class: {0} | score: {1} | type hierarchy: {2}", classResult._Class, classResult.Score, classResult.TypeHierarchy));
            }
        }

        private void ClassifyWithClassifier()
        {
            string[] classifierIDs = { _createdClassifierId };
            using (FileStream fs = File.OpenRead(_localTurtleFilePath))
            {
                Console.WriteLine(string.Format("\nCalling Classify(\"{0}\")...", _localTurtleFilePath));
                var result = _visualRecognition.Classify((fs as Stream).ReadAllBytes(), Path.GetFileName(_localTurtleFilePath), "image/jpeg", classifierIDs: classifierIDs);

                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
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

            #region Delay
            Delay(_delayTime);
            #endregion

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

        #region Delay
        //  Introducing a delay because of a known issue with Visual Recognition where newly created classifiers 
        //  will disappear without being deleted if a delete is attempted less than ~10 seconds after creation.
        private int _delayTime = 15000;
        private void Delay(int delayTime)
        {
            Console.WriteLine(string.Format("Delaying for {0} ms", delayTime));
            Thread.Sleep(delayTime);
        }
        #endregion
    }
}
