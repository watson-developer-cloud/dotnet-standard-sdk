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
using IBM.Cloud.SDK.Core;

namespace IBM.Watson.NaturalLanguageClassifier.v1.IntegrationTests
{
    [TestClass]
    public class NaturalLanguageClassifierServiceIntegrationTests
    {
        private static string apikey;
        private static string endpoint;
        private NaturalLanguageClassifierService service;
        private static string credentials = string.Empty;
        private string classifierDataFilePath = @"NLCTestData/weather-data.csv";
        private string metadataDataFilePath = @"NLCTestData/metadata.json";
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
            var listClassifiersResult = ListClassifiers();

            string classifierId = null;
            if (listClassifiersResult.Classifiers.Count > 0)
                classifierId = listClassifiersResult.Classifiers[0].ClassifierId;

            Classification classifyResult = null;
            if (!string.IsNullOrEmpty(classifierId))
            {
                ClassifyInput classifyInput = new ClassifyInput
                {
                    Text = textToClassify1
                };

                classifyResult = Classify(classifierId, classifyInput);
            }

            ClassificationCollection classifyCollectionResult = null;
            if(!string.IsNullOrEmpty(classifierId))
            {
                ClassifyCollectionInput classifyCollectionInput = new ClassifyCollectionInput
                {
                    Collection = new List<ClassifyInput>()
                    {
                        new ClassifyInput()
                        {
                            Text = textToClassify0
                        },
                        new ClassifyInput()
                        {
                            Text = textToClassify1
                        }
                    }
                };

                classifyCollectionResult = ClassifyCollection(classifierId, classifyCollectionInput);
            }

            Classifier createClassifierResult = null;
            using (FileStream classifierData = File.OpenRead(classifierDataFilePath), metadata = File.OpenRead(metadataDataFilePath))
            {
                createClassifierResult = service.CreateClassifier(metadata, classifierData);
            }

            var createdClassifierId = createClassifierResult.ClassifierId;

            var getClassifierResult = GetClassifier(createdClassifierId);

            if (!string.IsNullOrEmpty(classifierId) && !string.IsNullOrEmpty(createdClassifierId))
            {
                DeleteClassifier(createdClassifierId);
            }

            if (!string.IsNullOrEmpty(classifierId))
            {
                Assert.IsNotNull(classifyResult);
                Assert.IsNotNull(classifyCollectionResult);
            }
            Assert.IsNotNull(getClassifierResult);
            Assert.IsTrue(createdClassifierId == getClassifierResult.ClassifierId);
            Assert.IsNotNull(createClassifierResult);
            Assert.IsNotNull(listClassifiersResult);
        }
        #endregion

        #region Generated
        #region Classify
        private Classification Classify(string classifierId, ClassifyInput body, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to Classify()");
            var result = service.Classify(classifierId: classifierId, body: body, customData: customData);

            if (result != null)
            {
                Console.WriteLine("Classify() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to Classify()");
            }

            return result;
        }
        #endregion

        #region ClassifyCollection
        private ClassificationCollection ClassifyCollection(string classifierId, ClassifyCollectionInput body, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ClassifyCollection()");
            var result = service.ClassifyCollection(classifierId: classifierId, body: body, customData: customData);

            if (result != null)
            {
                Console.WriteLine("ClassifyCollection() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ClassifyCollection()");
            }

            return result;
        }
        #endregion

        #region CreateClassifier
        private Classifier CreateClassifier(System.IO.FileStream metadata, System.IO.FileStream trainingData, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CreateClassifier()");
            var result = service.CreateClassifier(metadata: metadata, trainingData: trainingData, customData: customData);

            if (result != null)
            {
                Console.WriteLine("CreateClassifier() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CreateClassifier()");
            }

            return result;
        }
        #endregion

        #region DeleteClassifier
        private BaseModel DeleteClassifier(string classifierId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteClassifier()");
            var result = service.DeleteClassifier(classifierId: classifierId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DeleteClassifier() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteClassifier()");
            }

            return result;
        }
        #endregion

        #region GetClassifier
        private Classifier GetClassifier(string classifierId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetClassifier()");
            var result = service.GetClassifier(classifierId: classifierId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("GetClassifier() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetClassifier()");
            }

            return result;
        }
        #endregion

        #region ListClassifiers
        private ClassifierList ListClassifiers(Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListClassifiers()");
            var result = service.ListClassifiers(customData: customData);

            if (result != null)
            {
                Console.WriteLine("ListClassifiers() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListClassifiers()");
            }

            return result;
        }
        #endregion

        #endregion
    }
}