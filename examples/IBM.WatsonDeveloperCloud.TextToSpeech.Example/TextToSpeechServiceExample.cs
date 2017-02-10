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

using System;
using IBM.WatsonDeveloperCloud.TextToSpeech.v1;
using IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model;

namespace IBM.WatsonDeveloperCloud.TextToSpeech.Example
{
    public class TextToSpeechServiceExample
    {
        private TextToSpeechService _textToSpeech = new TextToSpeechService();
        private string _voice = "en-US_AllisonVoice";
        private string _pronunciation = "Watson";
        private string _text = "I'm sorry, Dave. I'm afraid I can't do that.";
        private string _customVoiceModelID;
        private string _customVoiceModelName = "dotnet-standard-custom-voice-model";
        private string _customVoiceModelDescription = "Custom voice model created by the .NET Standard SDK.";
        private string _customVoiceModelLanguage = "en-US";
        private string _customVoiceModelUpdatedLanguage = "es-ES";

        public TextToSpeechServiceExample(string username, string password)
        {
            _textToSpeech.SetCredential(username, password);

            GetVoices();
            GetVoice();
            GetPronunciation();
            Synthesize();
            GetCustomVoiceModels();
            SaveCustomVoiceModel();
            UpdateCustomVoiceModel();
            GetCustomVoiceModel();
            SaveWords();
            GetWords();
            DeleteWord();
            DeleteCustomVoiceModel();
        }

        #region Get Voices
        private void GetVoices()
        {
            Console.WriteLine("Calling GetVoices()...");

            var results = _textToSpeech.GetVoices();

            if (results != null && results.Count > 0)
            {
                Console.WriteLine("Voices found...");
                foreach (Voice voice in results)
                    Console.WriteLine(string.Format("name: {0} | language: {1} | gender: {2} | description {3}",
                        voice.Name,
                        voice.Language,
                        voice.Gender,
                        voice.Description));
            }
            else
            {
                Console.WriteLine("There are no voices.");
            }
        }
        #endregion

        #region Get Voice
        private void GetVoice()
        {
            Console.WriteLine(string.Format("Calling GetVoice({0})...", _voice));

            var results = _textToSpeech.GetVoice(_voice);

            if (results != null)
            {
                Console.WriteLine(string.Format("Voice {0} found...", _voice));
                Console.WriteLine(string.Format("name: {0} | language: {1} | gender: {2} | description {3}",
                        results.Name,
                        results.Language,
                        results.Gender,
                        results.Description));
            }
            else
            {
                Console.WriteLine("Voice not found.");
            }
        }
        #endregion

        #region Get Pronunciation
        private void GetPronunciation()
        {
            Console.WriteLine(string.Format("Calling GetPronunciation({0})...", _pronunciation));

            var results = _textToSpeech.GetPronunciation(_pronunciation);

            if (results != null)
            {
                Console.WriteLine(string.Format("Pronunciation: {0}", results.Value));
            }
            else
            {
                Console.WriteLine(string.Format("Unable to get pronunciation for {0}...", _pronunciation));
            }
        }
        #endregion

        #region Synthesize
        private void Synthesize()
        {
            Console.WriteLine(string.Format("Calling Synthesize({0})...", _text));

            var results = _textToSpeech.Synthesize(_text, Voice.EN_ALLISON, AudioType.WAV);

            if (results != null)
            {
                Console.WriteLine(string.Format("Succeeded to synthesize {0} | stream length: {1}", _text, results.Length));
            }
            else
            {
                Console.WriteLine("Failed to synthesize.");
            }
        }
        #endregion

        #region Get Custom Voice Models
        private void GetCustomVoiceModels()
        {
            Console.WriteLine("Calling GetCustomVoiceModels()...");

            var results = _textToSpeech.GetCustomVoiceModels();

            if (results != null && results.Count > 0)
            {
                Console.WriteLine("Voice models found...");

                foreach (CustomVoiceModel voiceModel in results)
                {
                    Console.WriteLine(string.Format("Name: {0} | Id: {1} | Language: {2} | Description: {3}", 
                        voiceModel.Name, 
                        voiceModel.Id, 
                        voiceModel.Language, 
                        voiceModel.Description));
                }
            }
            else
            {
                Console.WriteLine("There are no custom voice models.");
            }
        }
        #endregion

        #region Save Custom Voice Model
        private void SaveCustomVoiceModel()
        {
            Console.WriteLine("Calling SaveCustomVoiceModel()...");
            CustomVoiceModel voiceModel = new CustomVoiceModel()
            {
                Name = _customVoiceModelName,
                Description = _customVoiceModelDescription,
                Language = _customVoiceModelLanguage
            };

            var results = _textToSpeech.SaveCustomVoiceModel(voiceModel);

            if (results != null)
            {
                Console.WriteLine("Custom voice model created...");

                _customVoiceModelID = results.Id;

                Console.WriteLine(string.Format("Name: {0} | Id: {1} | Language: {2} | Description: {3}",
                        results.Name,
                        results.Id,
                        results.Language,
                        results.Description));
            }
            else
            {
                Console.WriteLine("Failed to create custom voice model.");
            }
        }
        #endregion

        #region Update Custom Voice Model
        private void UpdateCustomVoiceModel()
        {
            if (string.IsNullOrEmpty(_customVoiceModelID))
                throw new ArgumentNullException("customVoiceModelID");

            Console.WriteLine(string.Format("Calling UpdateCustomVoiceModel({0})...", _customVoiceModelID));
            CustomVoiceModel voiceModel = new CustomVoiceModel()
            {
                Name = _customVoiceModelName,
                Description = _customVoiceModelDescription,
                Language = _customVoiceModelUpdatedLanguage,
                Id = _customVoiceModelID
            };

            var results = _textToSpeech.SaveCustomVoiceModel(voiceModel);

            if (results != null)
            {
                Console.WriteLine("Custom voice model updated...");

                Console.WriteLine(string.Format("Name: {0} | Id: {1} | Language: {2} | Description: {3}",
                        results.Name,
                        results.Id,
                        results.Language,
                        results.Description));
            }
            else
            {
                Console.WriteLine("Failed to update custom voice model.");
            }
        }
        #endregion

        #region Get Custom Voice Model
        private void GetCustomVoiceModel()
        {
            if (string.IsNullOrEmpty(_customVoiceModelID))
                throw new ArgumentNullException("customVoiceModelID");

            Console.WriteLine(string.Format("Calling GetCustomVoiceModel({0})...", _customVoiceModelID));

            var results = _textToSpeech.GetCustomVoiceModel(_customVoiceModelID);

            if (results != null)
            {
                Console.WriteLine(string.Format("Name: {0} | Id: {1} | Language: {2} | Description: {3}",
                        results.Name,
                        results.Id,
                        results.Language,
                        results.Description));
            }
            else
            {
                Console.WriteLine(string.Format("Failed to find custom voice model {0} ...", _customVoiceModelID));
            }
        }
        #endregion

        #region Delete Custom Voice Model
        private void DeleteCustomVoiceModel()
        {
            if(string.IsNullOrEmpty(_customVoiceModelID))
                throw new ArgumentNullException("customVoiceModelID");

            Console.WriteLine(string.Format("Calling DeleteCustomVoiceModel({0})...", _customVoiceModelID));
            _textToSpeech.DeleteCustomVoiceModel(_customVoiceModelID);
            Console.WriteLine(string.Format("Custom voice model {0} deleted.", _customVoiceModelID));
        }
        #endregion

        #region Get Words
        private void GetWords()
        {
            if (string.IsNullOrEmpty(_customVoiceModelID))
                throw new ArgumentNullException("customVoiceModelID");

            Console.WriteLine(string.Format("Calling GetWords({0})...", _customVoiceModelID));

            var results = _textToSpeech.GetWords(_customVoiceModelID);

            if (results != null && results.Count > 0)
            {
                Console.WriteLine("Custom words found.");
                foreach (CustomWordTranslation word in results)
                {
                    Console.WriteLine(string.Format("word: {0} | translation: {1}", word.Word, word.Translation));
                }
            }
            else
            {
                Console.WriteLine(string.Format("There are no words for custom voice model {0}...", _customVoiceModelID));
            }
        }
        #endregion

        #region Save Words
        private void SaveWords()
        {
            Console.WriteLine("Calling SaveWords()...");
            
            CustomWordTranslation ibm = new CustomWordTranslation()
            {
                Word = "IBM",
                Translation = "eye bee M"
            };

            CustomWordTranslation iPhone = new CustomWordTranslation()
            {
                Word = "iPhone",
                Translation = "i Phone"
            };

            CustomWordTranslation jpl = new CustomWordTranslation()
            {
                Word = "jpl",
                Translation = "J P L"
            };

            _textToSpeech.SaveWords(_customVoiceModelID, ibm, iPhone, jpl);

            Console.WriteLine("Words saved.");
        }
        #endregion

        #region Delete Word
        private void DeleteWord()
        {
            Console.WriteLine(string.Format("Calling DeleteWords({0})...", "jpl"));

            _textToSpeech.DeleteWord(_customVoiceModelID, "jpl");

            Console.WriteLine("Word deleted.");
        }
        #endregion
    }
}
