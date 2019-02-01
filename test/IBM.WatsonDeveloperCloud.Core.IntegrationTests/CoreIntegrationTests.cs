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

using IBM.WatsonDeveloperCloud.CompareComply.v1;
using IBM.WatsonDeveloperCloud.Discovery.v1;
using IBM.WatsonDeveloperCloud.LanguageTranslator.v3;
using IBM.WatsonDeveloperCloud.NaturalLanguageClassifier.v1;
using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1;
using IBM.WatsonDeveloperCloud.PersonalityInsights.v3;
using IBM.WatsonDeveloperCloud.SpeechToText.v1;
using IBM.WatsonDeveloperCloud.TextToSpeech.v1;
using IBM.WatsonDeveloperCloud.ToneAnalyzer.v3;
using IBM.WatsonDeveloperCloud.Util;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IBM.WatsonDeveloperCloud.Core.IntegrationTests
{
    [TestClass]
    public class CoreIntegrationTests
    {
        [TestMethod]
        public void GetCredentialsPaths_Success()
        {
            var credentialsPaths = Utility.GetCredentialsPaths();

            Assert.IsNotNull(credentialsPaths);
            Assert.IsTrue(credentialsPaths.Count > 0);
        }

        [TestMethod]
        public void LoadCredentials_Success()
        {
            int envVariableCount = Environment.GetEnvironmentVariables().Count;
            int newEnvVariableCount = envVariableCount;
            foreach (string path in Utility.GetCredentialsPaths())
            {
                if (Utility.LoadEnvFile(path))
                {
                    newEnvVariableCount = Environment.GetEnvironmentVariables().Count;
                    break;
                }
                else
                {
                    Assert.Fail();
                }

                Assert.IsTrue(newEnvVariableCount > envVariableCount);
            }
        }

        [TestMethod]
        public void AssistantV1WithLoadedCredentials_Success()
        {
            Assistant.v1.AssistantService service = new Assistant.v1.AssistantService();
            Assert.IsTrue(!string.IsNullOrEmpty(service.ApiKey));
            Assert.IsTrue(!string.IsNullOrEmpty(service.Url));
        }
        [TestMethod]
        public void AssistantV2WithLoadedCredentials_Success()
        {
            Assistant.v2.AssistantService service = new Assistant.v2.AssistantService();
            Assert.IsTrue(!string.IsNullOrEmpty(service.ApiKey));
            Assert.IsTrue(!string.IsNullOrEmpty(service.Url));
        }
        [TestMethod]
        public void CompareComplyV1WithLoadedCredentials_Success()
        {
            CompareComplyService service = new CompareComplyService();
            Assert.IsTrue(!string.IsNullOrEmpty(service.ApiKey));
            Assert.IsTrue(!string.IsNullOrEmpty(service.Url));
        }
        [TestMethod]
        public void DiscoveryV1WithLoadedCredentials_Success()
        {
            DiscoveryService service = new DiscoveryService();
            Assert.IsTrue(!string.IsNullOrEmpty(service.ApiKey));
            Assert.IsTrue(!string.IsNullOrEmpty(service.Url));
        }
        [TestMethod]
        public void LangaugeTranslatorV3WithLoadedCredentials_Success()
        {
            LanguageTranslatorService service = new LanguageTranslatorService();
            Assert.IsTrue(!string.IsNullOrEmpty(service.ApiKey));
            Assert.IsTrue(!string.IsNullOrEmpty(service.Url));
        }
        [TestMethod]
        public void NaturalLanguageClassifierV1WithLoadedCredentials_Success()
        {
            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService();
            Assert.IsTrue(!string.IsNullOrEmpty(service.ApiKey));
            Assert.IsTrue(!string.IsNullOrEmpty(service.Url));
        }
        [TestMethod]
        public void NaturalLanguageUnderstandingV1WithLoadedCredentials_Success()
        {
            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService();
            Assert.IsTrue(!string.IsNullOrEmpty(service.ApiKey));
            Assert.IsTrue(!string.IsNullOrEmpty(service.Url));
        }
        [TestMethod]
        public void PersonalityInsightsV3WithLoadedCredentials_Success()
        {
            PersonalityInsightsService service = new PersonalityInsightsService();
            Assert.IsTrue(!string.IsNullOrEmpty(service.ApiKey));
            Assert.IsTrue(!string.IsNullOrEmpty(service.Url));
        }
        [TestMethod]
        public void SpeechToTextV1WithLoadedCredentials_Success()
        {
            SpeechToTextService service = new SpeechToTextService();
            Assert.IsTrue(!string.IsNullOrEmpty(service.ApiKey));
            Assert.IsTrue(!string.IsNullOrEmpty(service.Url));
        }
        [TestMethod]
        public void TextToSpeechV1WithLoadedCredentials_Success()
        {
            TextToSpeechService service = new TextToSpeechService();
            Assert.IsTrue(!string.IsNullOrEmpty(service.ApiKey));
            Assert.IsTrue(!string.IsNullOrEmpty(service.Url));
        }
        [TestMethod]
        public void ToneAnalyzeV3WithLoadedCredentials_Success()
        {
            ToneAnalyzerService service = new ToneAnalyzerService();
            Assert.IsTrue(!string.IsNullOrEmpty(service.ApiKey));
            Assert.IsTrue(!string.IsNullOrEmpty(service.Url));
        }
        [TestMethod]
        public void VisualRecognitionV3WithLoadedCredentials_Success()
        {
            VisualRecognitionService service = new VisualRecognitionService();
            Assert.IsTrue(!string.IsNullOrEmpty(service.ApiKey));
            Assert.IsTrue(!string.IsNullOrEmpty(service.Url));
        }
    }
}
