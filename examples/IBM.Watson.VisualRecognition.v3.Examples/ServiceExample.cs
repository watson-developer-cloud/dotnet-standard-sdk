/**
* Copyright 2019 IBM Corp. All Rights Reserved.
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

using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Cloud.SDK.Core.Http;
using IBM.Watson.VisualRecognition.v3.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace IBM.Watson.VisualRecognition.v3.Examples
{
    public class ServiceExample
    {
        string apikey = "{apikey}";
        string url = "{url}";
        private string versionDate = "{versionDate}";

        private string localGiraffeFilePath = @"VisualRecognitionTestData/giraffe_to_classify.jpg";
        private string localFaceFilePath = @"VisualRecognitionTestData/obama.jpg";
        private string localGiraffePositiveExamplesFilePath = @"VisualRecognitionTestData/giraffe_positive_examples.zip";
        private string giraffeClassname = "giraffe";
        private string localTurtlePositiveExamplesFilePath = @"VisualRecognitionTestData/turtle_positive_examples.zip";
        private string turtleClassname = "turtle";
        private string localNegativeExamplesFilePath = @"VisualRecognitionTestData/negative_examples.zip";
        private string createdClassifierName = "dotnet-standard-test-integration-classifier";
        private string classifierId;

        static void Main(string[] args)
        {
            ServiceExample example = new ServiceExample();

            example.Classify();
            example.DetectFaces();

            example.ListClassifiers();
            example.CreateClassifier();
            example.GetClassifier();
            //example.UpdateClassifier();   // Commented since we cannot update a classifier until it has been trainied.
            //example.DeleteClassifier();   // Commented since we cannot delte a classifier until the status is `ready` or `failed`

            example.GetCoreMlModel();

            example.DeleteUserData();

            Console.WriteLine("Examples complete. Press any key to close the application.");
            Console.ReadKey();
        }

        #region General
        public void Classify()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2018-03-19", config);

            DetailedResponse<ClassifiedImages> result;
            using (FileStream fs = File.OpenRead("./fruitbowl.jpg"))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    result = service.Classify(
                        imagesFile: ms,
                        threshold: 0.6f,
                        owners: new List<string>()
                        {
                            "me"
                        }
                        );
                }
            }

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Face
        public void DetectFaces()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2018-03-19", config);

            DetailedResponse<DetectedFaces> result;
            using (FileStream fs = File.OpenRead("./Ginni_Rometty.jpg"))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    result = service.DetectFaces(
                        imagesFile: ms
                        );
                }

            }
            Console.WriteLine(result.Response);
        }
        #endregion

        #region Custom
        public void ListClassifiers()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2018-03-19", config);

            var result = service.ListClassifiers(
                verbose: true
                );

            Console.WriteLine(result.Response);
        }

        public void CreateClassifier()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2018-03-19", config);

            DetailedResponse<Classifier> result = null;
            using (FileStream beagle = File.OpenRead("./beagle.zip"), goldenRetriever = File.OpenRead("./golden-retriever.zip"), husky = File.OpenRead("./husky.zip"), cats = File.OpenRead("./cats.zip"))
            {
                using (MemoryStream beagleMemoryStream = new MemoryStream(), goldenRetrieverMemoryStream = new MemoryStream(), huskyMemoryStream = new MemoryStream(), catsMemoryStream = new MemoryStream())
                {
                    beagle.CopyTo(beagleMemoryStream);
                    goldenRetriever.CopyTo(goldenRetrieverMemoryStream);
                    husky.CopyTo(huskyMemoryStream);
                    cats.CopyTo(catsMemoryStream);

                    Dictionary<string, MemoryStream> positiveExamples = new Dictionary<string, MemoryStream>();
                    positiveExamples.Add("beagle_positive_examples", beagleMemoryStream);
                    positiveExamples.Add("goldenretriever_positive_examples", goldenRetrieverMemoryStream);
                    positiveExamples.Add("husky_positive_examples", huskyMemoryStream);
                    result = service.CreateClassifier(
                        name: "dogs",
                        positiveExamples: positiveExamples,
                        negativeExamples: catsMemoryStream
                        );
                }
            }

            Console.WriteLine(result.Response);
            classifierId = result.Result.ClassifierId;
        }

        public void GetClassifier()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2018-03-19", config);

            var result = service.GetClassifier(
                    classifierId: "dogs_1477088859"
                    );

            Console.WriteLine(result.Response);
        }

        public void UpdateClassifier()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2018-03-19", config);

            DetailedResponse<Classifier> result = null;
            using (FileStream dalmatian = File.OpenRead("./dalmatian.zip"), moreCats= File.OpenRead("./more-cats.zip"))
            {
                using (MemoryStream dalmatianMemoryStream = new MemoryStream(), moreCatsMemoryStream= new MemoryStream())
                {
                    dalmatian.CopyTo(dalmatianMemoryStream);
                    moreCats.CopyTo(moreCatsMemoryStream);

                    Dictionary<string, MemoryStream> positiveExamples = new Dictionary<string, MemoryStream>();
                    positiveExamples.Add("dalmatian_positive_examples", dalmatianMemoryStream);
                    result = service.UpdateClassifier(
                        classifierId: "dogs_1477088859",
                        positiveExamples: positiveExamples,
                        negativeExamples: moreCatsMemoryStream
                        );
                }
            }

            Console.WriteLine(result.Response);
        }

        public void DeleteClassifier()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2018-03-19", config);

            var result = service.DeleteClassifier(
                classifierId: "dogs_1477088859"
                );

            Console.WriteLine(result.StatusCode);
        }
        #endregion

        #region Core ML
        public void GetCoreMlModel()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2018-03-19", config);

            var result = service.GetCoreMlModel(
                    classifierId: "dogs_1477088859"
                    );

            using (FileStream fs = File.Create("/tmp/dogs_1477088859.mlmodel"))
            {
                result.Result.WriteTo(fs);
                fs.Close();
                result.Result.Close();
            }

            Console.WriteLine(result.Response);
        }
        #endregion

        #region User Data
        public void DeleteUserData()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2018-03-19", config);

            var result = service.DeleteUserData(
                customerId: "my_customer_ID"
                );

            Console.WriteLine(result.Response);
        }
        #endregion
    }
}
