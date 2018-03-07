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

using IBM.WatsonDeveloperCloud.NaturalLanguageClassifier.v1.Model;
using IBM.WatsonDeveloperCloud.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace IBM.WatsonDeveloperCloud.NaturalLanguageClassifier.v1.IntegrationTests
{
    [TestClass]
    public class NaturalLanguageClassifierServiceIntegrationTests
    {
        private static string _username;
        private static string _password;
        private static string _endpoint;
        private NaturalLanguageClassifierService service;
        private static string credentials = string.Empty;
        private string _classifierDataFilePath = @"NLCTestData/weather-data.csv";
        private string _metadataDataFilePath = @"NLCTestData/metadata.json";
        private string _textToClassify = "Is it raining?";

        [TestInitialize]
        public void Setup()
        {
            if (string.IsNullOrEmpty(credentials))
            {
                try
                {
                    credentials = Utility.SimpleGet(
                        Environment.GetEnvironmentVariable("VCAP_URL"),
                        Environment.GetEnvironmentVariable("VCAP_USERNAME"),
                        Environment.GetEnvironmentVariable("VCAP_PASSWORD")).Result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("Failed to get credentials: {0}", e.Message));
                }

                Task.WaitAll();

                var vcapServices = JObject.Parse(credentials);
                _endpoint = vcapServices["natural_language_classifier"]["url"].Value<string>();
                _username = vcapServices["natural_language_classifier"]["username"].Value<string>();
                _password = vcapServices["natural_language_classifier"]["password"].Value<string>();
            }

            service = new NaturalLanguageClassifierService(_username, _password);
            service.Endpoint = _endpoint;
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
                    Text = _textToClassify
                };

                classifyResult = Classify(classifierId, classifyInput);
            }

            Classifier createClassifierResult = null;
            using (FileStream classifierData = File.OpenRead(_classifierDataFilePath), metadata = File.OpenRead(_metadataDataFilePath))
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
                Assert.IsNotNull(classifyResult);
            Assert.IsNotNull(getClassifierResult);
            Assert.IsTrue(createdClassifierId == getClassifierResult.ClassifierId);
            Assert.IsNotNull(createClassifierResult);
            Assert.IsNotNull(listClassifiersResult);
        }
        #endregion

        #region Classify
        private Classification Classify(string classifierId, ClassifyInput body)
        {
            Console.WriteLine("\nAttempting to Classify()");
            var result = service.Classify(classifierId: classifierId, body: body);

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

        #region CreateClassifier
        private Classifier CreateClassifier(System.IO.Stream metadata, System.IO.Stream trainingData)
        {
            Console.WriteLine("\nAttempting to CreateClassifier()");
            var result = service.CreateClassifier(metadata: metadata, trainingData: trainingData);

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
        private object DeleteClassifier(string classifierId)
        {
            Console.WriteLine("\nAttempting to DeleteClassifier()");
            var result = service.DeleteClassifier(classifierId: classifierId);

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
        private Classifier GetClassifier(string classifierId)
        {
            Console.WriteLine("\nAttempting to GetClassifier()");
            var result = service.GetClassifier(classifierId: classifierId);

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
        private ClassifierList ListClassifiers()
        {
            Console.WriteLine("\nAttempting to ListClassifiers()");
            var result = service.ListClassifiers();

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

        #region Generated
        #region Classify
        private Classification Classify(string classifierId, ClassifyInput body)
        {
            Console.WriteLine("\nAttempting to Classify()");
            var result = service.Classify(classifierId: classifierId, body: body);

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

        #region CreateClassifier
        private Classifier CreateClassifier(System.IO.Stream metadata, System.IO.Stream trainingData)
        {
            Console.WriteLine("\nAttempting to CreateClassifier()");
            var result = service.CreateClassifier(metadata: metadata, trainingData: trainingData);

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
        private object DeleteClassifier(string classifierId)
        {
            Console.WriteLine("\nAttempting to DeleteClassifier()");
            var result = service.DeleteClassifier(classifierId: classifierId);

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
        private Classifier GetClassifier(string classifierId)
        {
            Console.WriteLine("\nAttempting to GetClassifier()");
            var result = service.GetClassifier(classifierId: classifierId);

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
        private ClassifierList ListClassifiers()
        {
            Console.WriteLine("\nAttempting to ListClassifiers()");
            var result = service.ListClassifiers();

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