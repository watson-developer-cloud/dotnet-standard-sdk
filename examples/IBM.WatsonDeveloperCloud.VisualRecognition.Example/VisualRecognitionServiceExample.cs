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

namespace IBM.WatsonDeveloperCloud.VisualRecognition.Example
{

    public class VisualRecognitionServiceExample
    {
        private VisualRecognitionService _visualRecognition = new VisualRecognitionService();
        private string _imageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/bb/Kittyply_edit1.jpg/1200px-Kittyply_edit1.jpg";
        private string _faceUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/8d/President_Barack_Obama.jpg/220px-President_Barack_Obama.jpg";
        private string _localGiraffeFilePath = @"exampleData\giraffe_to_classify.jpg";
        private string _localFaceFilePath = @"exampleData\obama.jpg";
        private string _localTurtleFilePath = @"exampleData\turtle_to_classify.jpg";
        private string _localGiraffePositiveExamplesFilePath = @"exampleData\giraffe_positive_examples.zip";
        private string _giraffeClassname = "giraffe_positive_examples";
        private string _localTurtlePositiveExamplesFilePath = @"exampleData\turtle_positive_examples.zip";
        private string _turtleClassname = "turtle_positive_examples";
        private string _localNegativeExamplesFilePath = @"exampleData\negative_examples.zip";
        private string _createdClassifierName = "dotnet-standard-test-classifier";

        public VisualRecognitionServiceExample(string apikey)
        {
            _visualRecognition.SetCredential(apikey);

            //ClassifyGet();
            //ClassifyPost();
            //DetectFacesGet();
            //DetectFacesPost();
            //GetClassifiersBrief();
            GetClassifiersVerbose();
            //CreateClassifier();
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

                if(result != null)
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
    }
}
