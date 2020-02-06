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
using IBM.Cloud.SDK.Core.Model;
using IBM.Cloud.SDK.Core.Util;
using IBM.Watson.VisualRecognition.v4.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace IBM.Watson.VisualRecognition.v4.Examples
{
    public class ServiceExample
    {
        string apikey = "{apikey}";
        string url = "{serviceUrl}";
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

            example.Analyze();


            Console.WriteLine("Examples complete. Press any key to close the application.");
            Console.ReadKey();
        }

        #region Analysis
        public void Analyze()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2019-02-11", authenticator);

            DetailedResponse<AnalyzeResponse> result = null;
            List<FileWithMetadata> imagesFile = new List<FileWithMetadata>();
            using (FileStream hondaFilestream = File.OpenRead("./honda.jpg"), diceFilestream = File.OpenRead("./dice.png"))
            {
                using (MemoryStream hondaMemoryStream = new MemoryStream(), diceMemoryStream = new MemoryStream())
                {
                    hondaFilestream.CopyTo(hondaMemoryStream);
                    diceFilestream.CopyTo(diceMemoryStream);
                    FileWithMetadata hondaFile = new FileWithMetadata()
                    {
                        Data = hondaMemoryStream,
                        ContentType = "image/jpeg",
                        Filename = "honda.jpg"
                    };
                    FileWithMetadata diceFile = new FileWithMetadata()
                    {
                        Data = diceMemoryStream,
                        ContentType = "image/png",
                        Filename = "dice.png"
                    };
                    imagesFile.Add(hondaFile);
                    imagesFile.Add(diceFile);

                    result = service.Analyze(
                        collectionIds: new List<string>() { "{collectionId}" },
                        features: new List<string>() { "objects" },
                        imagesFile: imagesFile);

                    Console.WriteLine(result.Response);
                }
            }
        }
        #endregion

        #region Collections
        public void CreateCollection()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2019-02-11", authenticator);

            var result = service.CreateCollection(
                name: Utility.ConvertToUtf8("my-collection"),
                description: Utility.ConvertToUtf8("A description of my collection")
                );

            Console.WriteLine(result.Response);
        }

        public void ListCollections()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2019-02-11", authenticator);

            var result = service.ListCollections();

            Console.WriteLine(result.Response);
        }

        public void GetCollection()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2019-02-11", authenticator);

            var result = service.GetCollection(
                collectionId: "5826c5ec-6f86-44b1-ab2b-cca6c75f2fc7"
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateCollection()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2019-02-11", authenticator);

            var result = service.UpdateCollection(
                collectionId: "5826c5ec-6f86-44b1-ab2b-cca6c75f2fc7",
                description: "Updated description of my collection"
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteCollection()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2019-02-11", authenticator);

            var result = service.DeleteCollection(
                collectionId: "5826c5ec-6f86-44b1-ab2b-cca6c75f2fc7"
                );

            Console.WriteLine(result.StatusCode);
        }
        #endregion

        #region Images
        public void AddImages()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2019-02-11", authenticator);

            string trainingData = File.ReadAllText("training_data.json");
            var result = service.AddImages(
                collectionId: "60b4a98f-2472-4e2b-9c73-28bcaea6fa00",
                imageUrl: new List<string> { "https://watson-developer-cloud.github.io/doc-tutorial-downloads/visual-recognition/2018-Honda-Fit.jpg" },
                trainingData: trainingData
                );

            Console.WriteLine(result.Response);
        }

        public void ListImages()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2019-02-11", authenticator);

            var result = service.ListImages(
                collectionId: "60b4a98f-2472-4e2b-9c73-28bcaea6fa00"
                );

            Console.WriteLine(result.Response);
        }

        public void GetImage()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2019-02-11", authenticator);

            var result = service.GetImageDetails(
                collectionId: "60b4a98f-2472-4e2b-9c73-28bcaea6fa00",
                imageId: "2018-Honda-Fit_dc0e7bbb5169dd0337ef5b753027ca90"
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteImage()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2019-02-11", authenticator);

            var result = service.DeleteImage(
                collectionId: "60b4a98f-2472-4e2b-9c73-28bcaea6fa00",
                imageId: "2018-Honda-Fit_dc0e7bbb5169dd0337ef5b753027ca90"
                );

            Console.WriteLine(result.StatusCode);
        }

        public void GetJpgImage()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2019-02-11", authenticator);

            var result = service.GetJpegImage(
                collectionId: "60b4a98f-2472-4e2b-9c73-28bcaea6fa00",
                imageId: "2018-Honda-Fit_dc0e7bbb5169dd0337ef5b753027ca90"
                );

            //  Save file
            using (FileStream fs = File.Create("2018-Honda-Fit.jpg"))
            {
                result.Result.WriteTo(fs);
                fs.Close();
                result.Result.Close();
            }

            Console.WriteLine(result.StatusCode);
        }
        #endregion

        #region Training
        public void TrainCollection()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2019-02-11", authenticator);

            var result = service.Train(
                collectionId: "60b4a98f-2472-4e2b-9c73-28bcaea6fa00"
                );

            Console.WriteLine(result.Response);
        }

        public void AddTrainingData()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2019-02-11", authenticator);

            List<TrainingDataObject> objects = new List<TrainingDataObject>()
            {
                new TrainingDataObject()
                {
                    _Object = "2018-Fit",
                    Location = new Location()
                    {
                        Left = 13,
                        Top = 5,
                        Width = 760,
                        Height = 419
                    }

                }
            };

            var result = service.AddImageTrainingData(
                collectionId: "60b4a98f-2472-4e2b-9c73-28bcaea6fa00",
                imageId: "2018-Honda-Fit_dc0e7bbb5169dd0337ef5b753027ca90",
                objects: objects
                );

            Console.WriteLine(result.Response);
        }

        public void GetTrainingUsage()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2019-02-11", authenticator);

            var result = service.GetTrainingUsage(
                startTime: "2019-01-01",
                endTime: "2019-01-31"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Objects
        public void ListObjectMetadata()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2019-02-11", authenticator);

            var result = service.ListObjectMetadata(
                collectionId: "5826c5ec-6f86-44b1-ab2b-cca6c75f2fc7"
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateObject()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2019-02-11", authenticator);

            var result = service.UpdateObjectMetadata(
                collectionId: "5826c5ec-6f86-44b1-ab2b-cca6c75f2fc7",
                _object: "2018-Fit",
                newObject: "subcompact"
                );


            Console.WriteLine(result.Response);
        }

        public void GetObjectMetadata()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2019-02-11", authenticator);

            var result = service.GetObjectMetadata(
                collectionId: "5826c5ec-6f86-44b1-ab2b-cca6c75f2fc7",
                _object: "subcompact"
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteObject()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2019-02-11", authenticator);

            var result = service.DeleteObject(
                collectionId: "5826c5ec-6f86-44b1-ab2b-cca6c75f2fc7",
                _object: "subcompact"
                );

            Console.WriteLine(result.Response);
        }

        #endregion

        #region User Data
        public void DeleteUserData()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}"
                );

            VisualRecognitionService service = new VisualRecognitionService("2018-03-19", authenticator);

            var result = service.DeleteUserData(
                customerId: "my_customer_ID"
                );

            Console.WriteLine(result.Response);
        }
        #endregion
    }
}
