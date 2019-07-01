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
                apikey: apikey
                );

            VisualRecognitionService service = new VisualRecognitionService(versionDate, config);
            service.SetEndpoint(url);

            DetailedResponse<ClassifiedImages> result;
            using (FileStream fs = File.OpenRead(localGiraffeFilePath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    result = service.Classify(
                        imagesFile: ms,
                        imagesFilename: Path.GetFileName(localGiraffeFilePath),
                        imagesFileContentType: "image/jpeg",
                        threshold: 0.5f,
                        acceptLanguage: "en-US"
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
                apikey: apikey
                );

            VisualRecognitionService service = new VisualRecognitionService(versionDate, config);
            service.SetEndpoint(url);

            DetailedResponse<DetectedFaces> result;
            using (FileStream fs = File.OpenRead(localFaceFilePath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    result = service.DetectFaces(
                        imagesFile: ms,
                        imagesFilename: Path.GetFileName(localFaceFilePath),
                        imagesFileContentType: "image/jpeg",
                        acceptLanguage: "en"
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
                apikey: apikey
                );

            VisualRecognitionService service = new VisualRecognitionService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.ListClassifiers();

            Console.WriteLine(result.Response);
        }

        public void CreateClassifier()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            VisualRecognitionService service = new VisualRecognitionService(versionDate, config);
            service.SetEndpoint(url);

            DetailedResponse<Classifier> result = null;
            using (FileStream positiveExamplesFileStream = File.OpenRead(localGiraffePositiveExamplesFilePath), negativeExamplesFileStream = File.OpenRead(localNegativeExamplesFilePath))
            {
                using (MemoryStream positiveExamplesMemoryStream = new MemoryStream(), negativeExamplesMemoryStream = new MemoryStream())
                {
                    positiveExamplesFileStream.CopyTo(positiveExamplesMemoryStream);
                    negativeExamplesFileStream.CopyTo(negativeExamplesMemoryStream);
                    Dictionary<string, MemoryStream> positiveExamples = new Dictionary<string, MemoryStream>();
                    positiveExamples.Add(giraffeClassname, positiveExamplesMemoryStream);
                    result = service.CreateClassifier(
                        name: createdClassifierName,
                        positiveExamples: positiveExamples,
                        negativeExamples: negativeExamplesMemoryStream,
                        negativeExamplesFilename: Path.GetFileName(localNegativeExamplesFilePath)
                        );
                }
            }

            Console.WriteLine(result.Response);
            classifierId = result.Result.ClassifierId;
        }

        public void GetClassifier()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            VisualRecognitionService service = new VisualRecognitionService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetClassifier(
                    classifierId: classifierId
                    );

            Console.WriteLine(result.Response);
        }

        public void UpdateClassifier()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            VisualRecognitionService service = new VisualRecognitionService(versionDate, config);
            service.SetEndpoint(url);

            DetailedResponse<Classifier> result = null;
            using (FileStream positiveExamplesStream = File.OpenRead(localTurtlePositiveExamplesFilePath))
            {
                using (MemoryStream positiveExamplesMemoryStream = new MemoryStream())
                {
                    Dictionary<string, MemoryStream> positiveExamples = new Dictionary<string, MemoryStream>();
                    positiveExamples.Add(turtleClassname, positiveExamplesMemoryStream);
                    result = service.UpdateClassifier(
                        classifierId: classifierId,
                        positiveExamples: positiveExamples
                        );
                }
            }

            Console.WriteLine(result.Response);
        }

        public void DeleteClassifier()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            VisualRecognitionService service = new VisualRecognitionService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteClassifier(
                classifierId: classifierId
                );

            Console.WriteLine(result.StatusCode);
        }
        #endregion

        #region Core ML
        public void GetCoreMlModel()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            VisualRecognitionService service = new VisualRecognitionService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetCoreMlModel(
                    classifierId: classifierId
                    );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region User Data
        public void DeleteUserData()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            VisualRecognitionService service = new VisualRecognitionService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteUserData(
                customerId: "customerId"
                );

            Console.WriteLine(result.Response);
        }
        #endregion
    }
}
