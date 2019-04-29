/**
* Copyright 2018 IBM Corp. All Rights Reserved.
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

using IBM.Watson.NaturalLanguageClassifier.v1.Model;
using IBM.Cloud.SDK.Core.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using IBM.Cloud.SDK.Core.Http;

namespace IBM.Watson.NaturalLanguageClassifier.v1.IntegrationTests
{
    [TestClass]
    public class NaturalLanguageClassifierServiceIntegrationTests
    {
        private static string apikey;
        private static string endpoint;
        private NaturalLanguageClassifierService service;
        private static string credentials = string.Empty;
        private string classifierDataFilePath = @"NaturalLanguageClassifierTestData/weather-data.csv";
        private string metadataDataFilePath = @"NaturalLanguageClassifierTestData/metadata.json";
        private string textToClassify0 = "Is it raining?";
        private string textToClassify1 = "Will it be hot today?";

        [TestInitialize]
        public void Setup()
        {
            #region Get Credentials
            if (string.IsNullOrEmpty(credentials))
            {
                var parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.Parent.FullName;
                string credentialsFilepath = parentDirectory + Path.DirectorySeparatorChar + "sdk-credentials" + Path.DirectorySeparatorChar + "credentials.json";
                if (File.Exists(credentialsFilepath))
                {
                    try
                    {
                        credentials = File.ReadAllText(credentialsFilepath);
                        credentials = Utility.AddTopLevelObjectToJson(credentials, "VCAP_SERVICES");
                    }
                    catch (Exception e)
                    {
                        throw new Exception(string.Format("Failed to load credentials: {0}", e.Message));
                    }
                }
                else
                {
                    Console.WriteLine("Credentials file does not exist.");
                }

                VcapCredentials vcapCredentials = JsonConvert.DeserializeObject<VcapCredentials>(credentials);
                var vcapServices = JObject.Parse(credentials);

                Credential credential = vcapCredentials.GetCredentialByname("natural-language-classifier-sdk")[0].Credentials;
                endpoint = credential.Url;
                apikey = credential.IamApikey;
            }
            #endregion
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = endpoint
            };

            service = new NaturalLanguageClassifierService(tokenOptions);
            service.SetEndpoint(endpoint);
        }

        #region Classifiers
        [TestMethod]
        public void TestClassifiers_Success()
        {
            var listClassifiersResult = service.ListClassifiers();

            string classifierId = null;
            if (listClassifiersResult.Result.Classifiers.Count > 0)
                classifierId = listClassifiersResult.Result.Classifiers[0].ClassifierId;

            DetailedResponse<Classification> classifyResult = null;
            if (!string.IsNullOrEmpty(classifierId))
            {

                classifyResult = service.Classify(
                    classifierId: classifierId,
                    text: textToClassify1
                    );
            }

            DetailedResponse<ClassificationCollection> classifyCollectionResult = null;
            if (!string.IsNullOrEmpty(classifierId))
            {
                var collection = new List<ClassifyInput>()
                    {
                        new ClassifyInput()
                        {
                            Text = textToClassify0
                        },
                        new ClassifyInput()
                        {
                            Text = textToClassify1
                        }
                };

                classifyCollectionResult = service.ClassifyCollection(
                    classifierId: classifierId,
                    collection: collection
                    );
            }

            DetailedResponse<Classifier> createClassifierResult = null;
            using (FileStream trainingDataFile = File.OpenRead(classifierDataFilePath), metadataFile = File.OpenRead(metadataDataFilePath))
            {
                using (MemoryStream trainingData = new MemoryStream(), metadata = new MemoryStream())
                {
                    trainingDataFile.CopyTo(trainingData);
                    metadataFile.CopyTo(metadata);
                    createClassifierResult = service.CreateClassifier(
                        metadata: metadata,
                        trainingData: trainingData
                        );
                }
            }

            var createdClassifierId = createClassifierResult.Result.ClassifierId;

            var getClassifierResult = service.GetClassifier(
                classifierId: createdClassifierId
                );

            if (!string.IsNullOrEmpty(classifierId) && !string.IsNullOrEmpty(createdClassifierId))
            {
                service.DeleteClassifier(
                    classifierId: createdClassifierId
                    );
            }

            if (!string.IsNullOrEmpty(classifierId))
            {
                Assert.IsNotNull(classifyResult);
                Assert.IsNotNull(classifyCollectionResult);
            }
            Assert.IsNotNull(getClassifierResult);
            Assert.IsTrue(createdClassifierId == getClassifierResult.Result.ClassifierId);
            Assert.IsNotNull(createClassifierResult);
            Assert.IsNotNull(listClassifiersResult);
        }
        #endregion
    }
}