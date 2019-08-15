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

using IBM.Watson.CompareComply.v1;
using IBM.Watson.Discovery.v1;
using IBM.Watson.LanguageTranslator.v3;
using IBM.Watson.NaturalLanguageClassifier.v1;
using IBM.Watson.NaturalLanguageUnderstanding.v1;
using IBM.Watson.PersonalityInsights.v3;
using IBM.Watson.SpeechToText.v1;
using IBM.Watson.TextToSpeech.v1;
using IBM.Watson.ToneAnalyzer.v3;
using IBM.Watson.VisualRecognition.v3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IBM.Watson.IntegrationTests
{
    [TestClass]
    public class ExternalCredentialsIntegrationTests
    {
        [TestMethod]
        public void AssistantV1WithLoadedCredentials_Success()
        {
            Assistant.v1.AssistantService service = new Assistant.v1.AssistantService();
            Assert.IsNotNull(service.GetAuthenticator());
        }
        [TestMethod]
        public void AssistantV2WithLoadedCredentials_Success()
        {
            Assistant.v2.AssistantService service = new Assistant.v2.AssistantService();
            Assert.IsNotNull(service.GetAuthenticator());
        }
        [TestMethod]
        public void CompareComplyV1WithLoadedCredentials_Success()
        {
            CompareComplyService service = new CompareComplyService();
            Assert.IsNotNull(service.GetAuthenticator());
        }
        [TestMethod]
        public void DiscoveryV1WithLoadedCredentials_Success()
        {
            DiscoveryService service = new DiscoveryService();
            Assert.IsNotNull(service.GetAuthenticator());
        }
        [TestMethod]
        public void LangaugeTranslatorV3WithLoadedCredentials_Success()
        {
            LanguageTranslatorService service = new LanguageTranslatorService();
            Assert.IsNotNull(service.GetAuthenticator());
        }
        [TestMethod]
        public void NaturalLanguageClassifierV1WithLoadedCredentials_Success()
        {
            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService();
            Assert.IsNotNull(service.GetAuthenticator());
        }
        [TestMethod]
        public void NaturalLanguageUnderstandingV1WithLoadedCredentials_Success()
        {
            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService();
            Assert.IsNotNull(service.GetAuthenticator());
        }
        [TestMethod]
        public void PersonalityInsightsV3WithLoadedCredentials_Success()
        {
            PersonalityInsightsService service = new PersonalityInsightsService();
            Assert.IsNotNull(service.GetAuthenticator());
        }
        [TestMethod]
        public void SpeechToTextV1WithLoadedCredentials_Success()
        {
            SpeechToTextService service = new SpeechToTextService();
            Assert.IsNotNull(service.GetAuthenticator());
        }
        [TestMethod]
        public void TextToSpeechV1WithLoadedCredentials_Success()
        {
            TextToSpeechService service = new TextToSpeechService();
            Assert.IsNotNull(service.GetAuthenticator());
        }
        [TestMethod]
        public void ToneAnalyzeV3WithLoadedCredentials_Success()
        {
            ToneAnalyzerService service = new ToneAnalyzerService();
            Assert.IsNotNull(service.GetAuthenticator());
        }
        [TestMethod]
        public void VisualRecognitionV3WithLoadedCredentials_Success()
        {
            VisualRecognitionService service = new VisualRecognitionService();
            Assert.IsNotNull(service.GetAuthenticator());
        }
    }
}
