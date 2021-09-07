/**
* (C) Copyright IBM Corp. 2018, 2021.
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

using IBM.Watson.TextToSpeech.v1.Model;
using IBM.Cloud.SDK.Core.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using IBM.Cloud.SDK.Core;

namespace IBM.Watson.TextToSpeech.v1.IntegrationTests
{
    [TestClass]
    public class TextToSpeechServiceIntegrationTests
    {
        private static string apikey;
        private static string endpoint;
        private TextToSpeechService service;
        private static string credentials = string.Empty;
        private const string allisonVoice = "en-US_AllisonVoice";
        private string synthesizeText = "Hello, welcome to the Watson dotnet SDK!";
        private string voiceModelName = "dotnet-sdk-voice-model";
        private string voiceModelUpdatedName = "dotnet-sdk-voice-model-updated";
        private string voiceModelDescription = "Custom voice model for .NET SDK integration tests.";
        private string voiceModelUpdatedDescription = "Custom voice model for .NET SDK integration tests. Updated.";
        private string wavFile = @"Assets/test-audio.wav";

        [TestInitialize]
        public void Setup()
        {
            service = new TextToSpeechService();
        }

        #region Voices
        [TestMethod]
        public void Voices_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var listVoicesResult = service.ListVoices();

            service.WithHeader("X-Watson-Test", "1");
            var getVoiceResult = service.GetVoice(
                voice: allisonVoice
                );

            Assert.IsNotNull(getVoiceResult);
            Assert.IsTrue(!string.IsNullOrEmpty(getVoiceResult.Result.Name));
            Assert.IsNotNull(listVoicesResult);
            Assert.IsNotNull(listVoicesResult.Result._Voices);
        }
        #endregion

        #region Synthesize
        [TestMethod]
        public void Synthesize_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var synthesizeResult = service.Synthesize(
                text: synthesizeText,
                accept: "audio/wav",
                voice: allisonVoice
                );

            //  Save file
            using (FileStream fs = File.Create("synthesize.wav"))
            {
                synthesizeResult.Result.WriteTo(fs);
                fs.Close();
                synthesizeResult.Result.Close();
            }

            Assert.IsNotNull(synthesizeResult.Result);
        }
        #endregion

        #region Pronunciation
        [TestMethod]
        public void Pronunciation_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var getPronunciationResult = service.GetPronunciation(
                text: "IBM",
                voice: allisonVoice,
                format: "ipa"
                );

            Assert.IsNotNull(getPronunciationResult.Result);
            Assert.IsNotNull(getPronunciationResult.Result._Pronunciation);
        }
        #endregion

        #region Custom Voice Models
        [TestMethod]
        public void CustomVoiceModels_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var listVoiceModelsResult = service.ListCustomModels();

            service.WithHeader("X-Watson-Test", "1");
            var createVoiceModelResult = service.CreateCustomModel(
                name: voiceModelName,
                language: "en-US",
                description: voiceModelDescription);
            var customizationId = createVoiceModelResult.Result.CustomizationId;

            service.WithHeader("X-Watson-Test", "1");
            var getVoiceModelResult = service.GetCustomModel(
                customizationId: customizationId
                );

            var words = new List<Word>()
            {
                new Word()
                {
                    _Word = "hello",
                    Translation = "hullo"
                },
                new Word()
                {
                    _Word = "goodbye",
                    Translation = "gbye"
                },
                new Word()
                {
                    _Word = "hi",
                    Translation = "ohioooo"
                }
            };

            service.WithHeader("X-Watson-Test", "1");
            var updateVoiceModelResult = service.UpdateCustomModel(
                customizationId: customizationId,
                name: voiceModelUpdatedName,
                description: voiceModelUpdatedDescription,
                words: words
                );

            service.WithHeader("X-Watson-Test", "1");
            var getVoiceModelResult2 = service.GetCustomModel(
                customizationId: customizationId
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteVoiceModelResult = service.DeleteCustomModel(
                customizationId: customizationId
                );

            Assert.IsNotNull(deleteVoiceModelResult.StatusCode == 204);
            Assert.IsNotNull(getVoiceModelResult2.Result);
            Assert.IsTrue(getVoiceModelResult2.Result.Name == voiceModelUpdatedName);
            Assert.IsTrue(getVoiceModelResult2.Result.Description == voiceModelUpdatedDescription);
            Assert.IsTrue(getVoiceModelResult2.Result.Words.Count == 3);
            Assert.IsNotNull(getVoiceModelResult.Result);
            Assert.IsTrue(getVoiceModelResult.Result.Name == voiceModelName);
            Assert.IsTrue(getVoiceModelResult.Result.Description == voiceModelDescription);
            Assert.IsNotNull(createVoiceModelResult.Result);
            Assert.IsNotNull(listVoiceModelsResult.Result);
            Assert.IsNotNull(listVoiceModelsResult.Result.Customizations);
        }
        #endregion

        #region Words
        [TestMethod]
        public void Words_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var createVoiceModelResult = service.CreateCustomModel(
                name: voiceModelName,
                language: "en-US",
                description: voiceModelDescription
                );
            var customizationId = createVoiceModelResult.Result.CustomizationId;

            var words = new List<Word>()
            {
                new Word()
                {
                    _Word = "hello",
                    Translation = "hullo"
                },
                new Word()
                {
                    _Word = "goodbye",
                    Translation = "gbye"
                },
                new Word()
                {
                    _Word = "hi",
                    Translation = "ohioooo"
                }
            };

            service.WithHeader("X-Watson-Test", "1");
            var addWordsResult = service.AddWords(
                customizationId: customizationId,
                words: words
                );

            service.WithHeader("X-Watson-Test", "1");
            var listWordsResult = service.ListWords(
                customizationId: customizationId
                );

            service.WithHeader("X-Watson-Test", "1");
            var getWordResult = service.GetWord(
                customizationId: customizationId,
                word: "hello"
                );

            service.WithHeader("X-Watson-Test", "1");
            var addWordResult = service.AddWord(
                customizationId: customizationId,
                word: "IBM",
                translation: "eye bee m",
                partOfSpeech: "noun"
                );

            service.WithHeader("X-Watson-Test", "1");
            var checkAddWordResult = service.ListWords(
                customizationId: customizationId
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteWordResult = service.DeleteWord(
                customizationId: customizationId,
                word: "hi"
                );

            service.WithHeader("X-Watson-Test", "1");
            var checkDeleteWordResult = service.ListWords(
                customizationId: customizationId
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteVoiceModelResult = service.DeleteCustomModel(
                customizationId: customizationId
                );

            Assert.IsNotNull(checkDeleteWordResult.Result);
            Assert.IsNotNull(checkDeleteWordResult.Result._Words);
            Assert.IsTrue(checkDeleteWordResult.Result._Words.Count == 3);
            Assert.IsNotNull(checkAddWordResult.Result);
            Assert.IsNotNull(checkAddWordResult.Result._Words);
            Assert.IsTrue(checkAddWordResult.Result._Words.Count == 4);
            Assert.IsNotNull(getWordResult.Result);
            Assert.IsTrue(getWordResult.Result._Translation == "hullo");
            Assert.IsNotNull(listWordsResult.Result);
            Assert.IsNotNull(listWordsResult.Result._Words);
            Assert.IsTrue(listWordsResult.Result._Words.Count == 3);
            Assert.IsNotNull(addWordsResult.Result);
        }
        #endregion

        #region Miscellaneous
        [TestMethod]
        public void testListCustomPrompts()
        {
            service.WithHeader("X-Watson-Test", "1");
            var customModel = service.CreateCustomModel(
                description: "testString",
                name: "testString",
                language: TextToSpeechService.ListCustomModelsEnums.LanguageValue.EN_US
                );

            var customizationId = customModel.Result.CustomizationId;

            var prompts = service.ListCustomPrompts(
                customizationId: customizationId
                );

            Assert.IsNotNull(prompts.Result._Prompts);

            service.DeleteCustomModel(
                customizationId: customizationId
                );
        }

        [TestMethod]
        public void testAddCustomPrompts()
        {
            service.WithHeader("X-Watson-Test", "1");
            string customizationId = "";
            try
            {
                var customModel = service.CreateCustomModel(
                    description: "testString",
                    name: "testString",
                    language: TextToSpeechService.ListCustomModelsEnums.LanguageValue.EN_US
                    );

                customizationId = customModel.Result.CustomizationId;

                var promptMetadata = new PromptMetadata()
                {
                    PromptText = "promptText"
                };

                MemoryStream file = new MemoryStream();

                var prompt = service.AddCustomPrompt(
                    customizationId: customizationId,
                    promptId: "testId",
                    metadata: promptMetadata,
                    file: file
                    );

                Assert.IsNotNull(prompt.Result.Status);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                service.DeleteCustomModel(
                    customizationId: customizationId
                    );
            }
        }

        [TestMethod]
        public void testGetCustomPrompts()
        {
            service.WithHeader("X-Watson-Test", "1");
            string customizationId = "";
            try
            {
                var customModel = service.CreateCustomModel(
                    description: "testString",
                    name: "testString",
                    language: TextToSpeechService.ListCustomModelsEnums.LanguageValue.EN_US
                    );

                customizationId = customModel.Result.CustomizationId;

                var promptMetadata = new PromptMetadata()
                {
                    PromptText = "promptText"
                };

                MemoryStream file = new MemoryStream();

                var prompt = service.AddCustomPrompt(
                    customizationId: customizationId,
                    promptId: "testId",
                    metadata: promptMetadata,
                    file: file
                    );

                Assert.IsNotNull(prompt.Result.Status);

                var prompt1 = service.GetCustomPrompt(
                    customizationId: customizationId,
                    promptId: "testId"
                    );

                Assert.IsNotNull(prompt1.Result.Status);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                service.DeleteCustomModel(
                    customizationId: customizationId
                    );
            }
        }

        [TestMethod]
        public void testDeleteCustomPrompts()
        {
            service.WithHeader("X-Watson-Test", "1");
            string customizationId = "";
            try
            {
                var customModel = service.CreateCustomModel(
                    description: "testString",
                    name: "testString",
                    language: TextToSpeechService.ListCustomModelsEnums.LanguageValue.EN_US
                    );

                customizationId = customModel.Result.CustomizationId;

                var promptMetadata = new PromptMetadata()
                {
                    PromptText = "promptText"
                };

                MemoryStream file = new MemoryStream();

                var prompt = service.AddCustomPrompt(
                    customizationId: customizationId,
                    promptId: "testId",
                    metadata: promptMetadata,
                    file: file
                    );

                Assert.IsNotNull(prompt.Result.Status);

                var response = service.DeleteCustomPrompt(
                    customizationId: customizationId,
                    promptId: prompt.Result.PromptId
                    );

                Assert.IsNotNull(response.Result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                service.DeleteCustomModel(
                    customizationId: customizationId
                    );
            }
        }

        [TestMethod]
        public void testCreateSpeakerModel()
        {
            service.WithHeader("X-Watson-Test", "1");
            string speakerId = "";
            try
            {
                MemoryStream ms = new MemoryStream();
                FileStream fs = File.OpenRead(wavFile);
                fs.CopyTo(ms);
                var speakerModel = service.CreateSpeakerModel(
                    speakerName: "speakerNameDotNet",
                    audio: ms
                    );
                speakerId = speakerModel.Result.SpeakerId;

                Assert.IsNotNull(speakerModel.Result.SpeakerId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                service.DeleteSpeakerModel(
                    speakerId: speakerId
                    );
            }
        }

        [TestMethod]
        public void testListSpeakerModel()
        {
            service.WithHeader("X-Watson-Test", "1");
            string speakerId = "";
            try
            {
                MemoryStream ms = new MemoryStream();
                FileStream fs = File.OpenRead(wavFile);

                fs.CopyTo(ms);

                var speakerModel = service.CreateSpeakerModel(
                    speakerName: "speakerNameDotNet",
                    audio: ms
                    );

                speakerId = speakerModel.Result.SpeakerId;

                Assert.IsNotNull(speakerModel.Result.SpeakerId);

                var speakers = service.ListSpeakerModels();

                Assert.IsNotNull(speakers.Result._Speakers);
                Assert.IsTrue(speakers.Result._Speakers.Count > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                service.DeleteSpeakerModel(
                    speakerId: speakerId
                    );
            }
        }

        [TestMethod]
        public void testGetSpeakerModel()
        {
            service.WithHeader("X-Watson-Test", "1");
            string speakerId = "";
            try
            {
                MemoryStream ms = new MemoryStream();
                FileStream fs = File.OpenRead(wavFile);

                fs.CopyTo(ms);

                var speakerModel = service.CreateSpeakerModel(
                    speakerName: "speakerNameDotNet",
                    audio: ms
                    );

                speakerId = speakerModel.Result.SpeakerId;

                Assert.IsNotNull(speakerModel.Result.SpeakerId);

                var response = service.GetSpeakerModel(
                    speakerId: speakerId
                    );

                Assert.IsNotNull(response.Result.Customizations);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                service.DeleteSpeakerModel(
                    speakerId: speakerId
                    );
            }
        }

        [TestMethod]
        public void testDeleteSpeakerModel()
        {
            service.WithHeader("X-Watson-Test", "1");
            MemoryStream ms = new MemoryStream();
            FileStream fs = File.OpenRead(wavFile);

            fs.CopyTo(ms);

            var speakerModel = service.CreateSpeakerModel(
                speakerName: "speakerNameDotNet",
                audio: ms
                );

            string speakerId = speakerModel.Result.SpeakerId;
            Assert.IsNotNull(speakerModel.Result.SpeakerId);

            var response = service.GetSpeakerModel(
                speakerId: speakerId
                );

            Assert.IsNotNull(response.Result.Customizations);

            service.DeleteSpeakerModel(
                speakerId: speakerId
                );
        }
        #endregion
    }
}
