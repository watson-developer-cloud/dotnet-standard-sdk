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

using IBM.WatsonDeveloperCloud.SpeechToText.v1;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Model;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Example
{
    public class SpeechToTextServiceExample
    {
        private SpeechToTextService _speechToText = new SpeechToTextService();
        private string _path = "test-audio.wav";
        private string _modelToGet = "en-US_BroadbandModel";
        private string _createdCustomizationID;
        private string _sessionID;
        private string _wordName = "social";
        private string _corpusName = "stt_integration";
        string modelName = "en-US_BroadbandModel";
        AutoResetEvent autoEvent = new AutoResetEvent(false);

        public SpeechToTextServiceExample(string username, string password)
        {
            _speechToText.SetCredential(username, password);

            GetModels();
            GetModel(_modelToGet);
            RecognizeBody();
            RecognizeFormData();

            CreateSession();
            GetSessionStatus();
            RecognizeSessionBody();
            RecognizeSessionFormData();
            DeleteSession();

            CreateCustomModel();
            ListCustomModels();
            ListCustomModel();

            AddCustomCorpus();
            TrainCustomModel();
            autoEvent.WaitOne();
            ListCustomCorpora();
            GetCustomCorpus();
            DeleteCustomCorpus();
            TrainCustomModel();
            autoEvent.WaitOne();

            AddCustomWords();
            TrainCustomModel();
            autoEvent.WaitOne();
            AddCustomWord();
            TrainCustomModel();
            autoEvent.WaitOne();
            ListCustomWords();
            ListCustomWord(_wordName);
            DeleteCustomWord(_wordName);
            TrainCustomModel();
            autoEvent.WaitOne();

            ResetCustomModel();
            UpgradeCustomModel();
            DeleteCustomModel();

            Console.WriteLine("\nexamples complete.");
        }

        #region Get Models
        private void GetModels()
        {
            Console.WriteLine("\nCalling GetModels()...");
            var modelSet = _speechToText.GetModels();

            if (modelSet == null)
            {
                Console.WriteLine("modelSet is null.");
                return;
            }
            else
            {
                Console.WriteLine("ModelSet received...");
            }

            if (modelSet.Models != null && modelSet.Models.Count > 0)
            {
                Console.WriteLine("Models:");
                foreach (SpeechModel model in modelSet.Models)
                    Console.WriteLine(String.Format("Name: {0} | Rate: {1} | Language: {2} | Sessions: {3} | Description: {4} | URL: {5}",
                        model.Name,
                        model.Rate,
                        model.Language,
                        model.Sessions,
                        model.Description,
                        model.Url
                        ));
            }
            else
            {
                Console.WriteLine("There are no models.");
            }
        }
        #endregion

        #region Get Model
        private void GetModel(string modelName)
        {
            Console.WriteLine(string.Format("\nCalling GetModel({0})...", _modelToGet));
            var model = _speechToText.GetModel(_modelToGet);

            if (model == null)
            {
                Console.WriteLine("model is null.");
                return;
            }
            else
            {
                Console.WriteLine("Model received...");
                Console.WriteLine(string.Format("Name: {0} | Rate: {1} | Language: {2} | Sessions: {3} | Description: {4} | URL: {5}",
                    model.Name,
                    model.Rate,
                    model.Language,
                    model.Sessions,
                    model.Description,
                    model.Url
                    ));
            }

        }
        #endregion

        #region Recognize Body
        private void RecognizeBody()
        {
            using (FileStream fs = File.OpenRead(_path))
            {
                Console.WriteLine("\nCalling RecognizeBody...");
                var speechEvent = _speechToText.Recognize(fs.GetMediaTypeFromFile(),
                                                          fs);

                Console.WriteLine("speechEvent received...");
                if (speechEvent.Results != null || speechEvent.Results.Count > 0)
                {
                    foreach (SpeechRecognitionResult result in speechEvent.Results)
                    {
                        if (result.Alternatives != null && result.Alternatives.Count > 0)
                        {
                            foreach (SpeechRecognitionAlternative alternative in result.Alternatives)
                            {
                                Console.WriteLine(string.Format("{0}, {1}", alternative.Transcript, alternative.Confidence));
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Recognize Form Data
        private void RecognizeFormData()
        {
            using (FileStream fs = File.OpenRead(@"SpeechToTextTestData\test-audio.wav"))
            {
                Console.WriteLine("\nCalling RecognizeFormData()...");
                var speechEvent = _speechToText.Recognize(fs.GetMediaTypeFromFile(),
                                  new Metadata()
                                  {
                                      PartContentType = fs.GetMediaTypeFromFile()
                                  },
                                  fs);

                if (speechEvent != null)
                {
                    if (speechEvent.Results != null || speechEvent.Results.Count > 0)
                    {
                        foreach (SpeechRecognitionResult result in speechEvent.Results)
                        {
                            if (result.Alternatives != null && result.Alternatives.Count > 0)
                            {
                                foreach (SpeechRecognitionAlternative alternative in result.Alternatives)
                                {
                                    Console.WriteLine(string.Format("{0}, {1}", alternative.Transcript, alternative.Confidence));
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Result is null");
                }
            }
        }
        #endregion

        #region Create Session
        private void CreateSession()
        {
            Console.WriteLine(string.Format("\nCalling CreateSession({0})...", _modelToGet));
            var result = _speechToText.CreateSession(_modelToGet);

            if (result != null)
            {
                Console.WriteLine("Session received...");
                Console.WriteLine("SessionId: {0} | NewSessionUri: {1} | Recognize: {2} | RecognizeWS: {3} | ObserveResult: {4}",
                    result.SessionId,
                    result.NewSessionUri,
                    result.Recognize,
                    result.RecognizeWS,
                    result.ObserveResult);

                _sessionID = result.SessionId;
            }
            else
            {
                Console.WriteLine("Session is null.");
            }
        }
        #endregion

        #region Get Session Status
        private void GetSessionStatus()
        {
            Console.WriteLine(string.Format("\nCalling GetSessionStatus({0})...", _sessionID));
            var result = _speechToText.GetSessionStatus(_sessionID);

            if (result != null)
            {
                Console.WriteLine("RecognizeStatus received...");
                Console.WriteLine("State: {0} | Model: {1} | Recognize: {2} | RecognizeWS: {3} | ObserveResult: {4}",
                    result.Session.State,
                    result.Session.Model,
                    result.Session.Recognize,
                    result.Session.RecognizeWS,
                    result.Session.ObserveResult);
            }
            else
            {
                Console.WriteLine("Result is null");
            }
        }
        #endregion

        #region Recognize Session Body
        private void RecognizeSessionBody()
        {
            using (FileStream fs = File.OpenRead(@"SpeechToTextTestData\test-audio.wav"))
            {
                Console.WriteLine("\nCalling RecognizeSessionBody()...");
                var speechEvent = _speechToText.RecognizeWithSession(_sessionID, fs.GetMediaTypeFromFile(), fs);

                if (speechEvent != null)
                {
                    if (speechEvent.Results != null || speechEvent.Results.Count > 0)
                    {
                        foreach (SpeechRecognitionResult result in speechEvent.Results)
                        {
                            if (result.Alternatives != null && result.Alternatives.Count > 0)
                            {
                                foreach (SpeechRecognitionAlternative alternative in result.Alternatives)
                                {
                                    Console.WriteLine(string.Format("{0}, {1}", alternative.Transcript, alternative.Confidence));
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Result is null");
                }
            }
        }
        #endregion

        #region Recognize Session Form Data
        private void RecognizeSessionFormData()
        {
            using (FileStream fs = File.OpenRead(@"SpeechToTextTestData\test-audio.wav"))
            {
                Console.WriteLine("\nCalling RecognizeSessionFormData()...");
                var speechEvent = _speechToText.RecognizeWithSession(_sessionID,
                                             fs.GetMediaTypeFromFile(),
                                             new Metadata()
                                             {
                                                 PartContentType = fs.GetMediaTypeFromFile()
                                             },
                                             fs);

                if (speechEvent != null)
                {
                    if (speechEvent.Results != null || speechEvent.Results.Count > 0)
                    {
                        foreach (SpeechRecognitionResult result in speechEvent.Results)
                        {
                            if (result.Alternatives != null && result.Alternatives.Count > 0)
                            {
                                foreach (SpeechRecognitionAlternative alternative in result.Alternatives)
                                {
                                    Console.WriteLine(string.Format("{0}, {1}", alternative.Transcript, alternative.Confidence));
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Result is null");
                }
            }
        }
        #endregion

        #region Delete Session
        private void DeleteSession()
        {
            Console.WriteLine(string.Format("\nCalling DeleteSession({0})...", _sessionID));
            var result = _speechToText.DeleteSession(_sessionID);

            if (result != null)
            {
                Console.WriteLine(string.Format("Session {0} deleted.", _sessionID));
            }
            else
            {
                Console.WriteLine("Result is null");
            }

        }
        #endregion

        #region Create Custom Model
        private void CreateCustomModel()
        {
            Console.WriteLine("CreateCustomModel()...");
            var result = _speechToText.CreateCustomModel("STT Custom Model", modelName, "A custom speech to text model created from the example.");

            if (result != null)
            {
                if (!string.IsNullOrEmpty(result.CustomizationId))
                {
                    Console.WriteLine("Custom model created: {0}", result.CustomizationId);
                    _createdCustomizationID = result.CustomizationId;
                }
                else
                {
                    Console.WriteLine("Customization id is empty.");
                }
            }
            else
            {
                Console.WriteLine("Result is null");
            }
        }
        #endregion

        #region List Custom Models
        private void ListCustomModels()
        {
            Console.WriteLine("\nCalling ListCustomModels()...");
            var result = _speechToText.ListCustomModels();

            if (result != null)
            {
                foreach (Customization customization in result.Customization)
                {
                    Console.WriteLine(string.Format("Customization: name: {0} | status: {1} | description: {2}", customization.Name, customization.Status, customization.Description));
                }
            }
            else
            {
                Console.WriteLine("Result is null");
            }
        }
        #endregion

        #region List Custom Model
        private void ListCustomModel()
        {
            Console.WriteLine("\nCalling ListCustomModel()...");
            var result = _speechToText.ListCustomModel(_createdCustomizationID);

            if (result != null)
            {
                Console.WriteLine(string.Format("Customization: name: {0} | status: {1} | description: {2}", result.Name, result.Status, result.Description));
            }
            else
            {
                Console.WriteLine("Result is null");
            }
        }
        #endregion

        #region Add Custom Corpus
        private void AddCustomCorpus()
        {
            using (FileStream fs = File.OpenRead(@"SpeechToTextTestData\test-stt-corpus.txt"))
            {
                Console.WriteLine("\nCalling AddCustomModel()...");
                object result = _speechToText.AddCorpus(_createdCustomizationID,
                              _corpusName,
                              false,
                              fs);

                if (result != null)
                {
                    Console.WriteLine("Corpus added.");
                }
                else
                {
                    Console.WriteLine("Result is null");
                }
            }
        }
        #endregion

        #region List Custom Corpora
        private void ListCustomCorpora()
        {
            Console.WriteLine("\nCalling ListCustomCorpora()...");
            var result = _speechToText.ListCorpora(_createdCustomizationID);

            if (result != null)
            {
                if (result.CorporaProperty != null || result.CorporaProperty.Count > 0)
                {
                    foreach (Corpus corpus in result.CorporaProperty)
                    {
                        Console.WriteLine(string.Format("Corpus: name: {0} | status: {1} | totalWords: {2}", corpus.Name, corpus.Status, corpus.TotalWords));
                    }
                }
                else
                {
                    Console.WriteLine("CorporaProperty is empty");
                }
            }
            else
            {
                Console.WriteLine("Result is null");
            }
        }
        #endregion

        #region Get Custom Corpus
        private void GetCustomCorpus()
        {
            Console.WriteLine("\nCalling GetCustomCorpus()...");
            var result = _speechToText.GetCorpus(_createdCustomizationID, _corpusName);

            if (result != null)
            {
                Console.WriteLine(string.Format("Corpus: name: {0} | status: {1} | totalWords: {2}", result.Name, result.Status, result.TotalWords));
            }
            else
            {
                Console.WriteLine("Result is null");
            }
        }
        #endregion

        #region Delete Custom Corpus
        private void DeleteCustomCorpus()
        {
            Console.WriteLine("\nCalling DeleteCustomCorpus()...");
            var result = _speechToText.DeleteCorpus(_createdCustomizationID, _corpusName);

            if (result != null)
            {
                Console.WriteLine("Corpus deleted.");
            }
            else
            {
                Console.WriteLine("Result is null");
            }
        }
        #endregion

        #region Add Custom Words
        private void AddCustomWords()
        {
            Console.WriteLine("\nCalling AddCustomWords()...");
            object result = _speechToText.AddCustomWords(_createdCustomizationID,
                                  new Words()
                                  {
                                      WordsProperty = new List<Word>()
                                      {
                                          new Word()
                                          {
                                             DisplayAs = "Watson",
                                             SoundsLike = new List<string>()
                                             {
                                                 "wat son"
                                             },
                                             WordProperty = "watson"
                                          },
                                          new Word()
                                          {
                                             DisplayAs = "C#",
                                             SoundsLike = new List<string>()
                                             {
                                                 "si sharp"
                                             },
                                             WordProperty = "csharp"
                                          },
                                           new Word()
                                          {
                                             DisplayAs = "SDK",
                                             SoundsLike = new List<string>()
                                             {
                                                 "S.D.K."
                                             },
                                             WordProperty = "sdk"
                                          }
                                      }
                                  });

            if (result != null)
            {
                Console.WriteLine("Words added.");
            }
            else
            {
                Console.WriteLine("Result is null");
            }
        }
        #endregion

        #region Add Custom Word
        private void AddCustomWord()
        {
            Console.WriteLine("\nCalling DeleteCustomWord()...");
            object result = _speechToText.AddCustomWord(_createdCustomizationID,
                                  _wordName,
                                  new WordDefinition()
                                  {
                                      DisplayAs = "Social",
                                      SoundsLike = new List<string>()
                                             {
                                                 "so cial"
                                             }
                                  });

            if (result != null)
            {
                Console.WriteLine("Word added.");
            }
            else
            {
                Console.WriteLine("Result is null");
            }
        }
        #endregion

        #region List Custom Words
        private void ListCustomWords()
        {
            Console.WriteLine("\nCalling ListCustomWords()...");
            var result = _speechToText.ListCustomWords(_createdCustomizationID, null, null);

            if (result != null)
            {
                foreach(WordData word in result.Words)
                {
                    Console.WriteLine(string.Format("Word: word: {0}, display as: {1} | sounds like: {2}", word.Word, word.DisplayAs, word.SoundsLike));
                }
            }
            else
            {
                Console.WriteLine("Result is null");
            }
        }
        #endregion

        #region List Custom Word
        private void ListCustomWord(string wordToList)
        {
            Console.WriteLine("\nCalling ListCustomWords()...");
            var result = _speechToText.ListCustomWord(_createdCustomizationID, wordToList);

            if (result != null)
            {
                Console.WriteLine(string.Format("Word: word: {0}, display as: {1} | sounds like: {2}", result.Word, result.DisplayAs, result.SoundsLike));
            }
            else
            {
                Console.WriteLine("Result is null");
            }
        }
        #endregion

        #region Delete Custom Word
        private void DeleteCustomWord(string wordToDelete)
        {
            Console.WriteLine("\nCalling DeleteCustomWord()...");
            object result = _speechToText.DeleteCustomWord(_createdCustomizationID, _wordName);

            if (result != null)
            {
                Console.WriteLine("Custom word deleted.");
            }
            else
            {
                Console.WriteLine("Result is null");
            }
        }
        #endregion

        #region Reset Custom Model
        private void ResetCustomModel()
        {
            Console.WriteLine("\nCalling ResetCustomModel()...");
            var result = _speechToText.ResetCustomModel(_createdCustomizationID);

            if (result != null)
            {
                Console.WriteLine("Custom model reset.");
            }
            else
            {
                Console.WriteLine("Result is null");
            }
        }
        #endregion

        #region Upgrade Custom Model
        private void UpgradeCustomModel()
        {
            Console.WriteLine("\nCalling UpgradeCustomModel()...");
            var result = _speechToText.UpgradeCustomModel(_createdCustomizationID);

            if (result != null)
            {
                Console.WriteLine("Custom model upgraded.");
            }
            else
            {
                Console.WriteLine("Result is null");
            }
        }
        #endregion

        #region Delete Custom Model
        private void DeleteCustomModel()
        {
            Console.WriteLine("\nCalling DeleteCustomModel()...");
            var result = _speechToText.DeleteCustomModel(_createdCustomizationID);

            if (result != null)
            {
                Console.WriteLine("Custom model deleted.");
            }
            else
            {
                Console.WriteLine("Result is null");
            }
        }
        #endregion

        #region Train Custom Model
        private void TrainCustomModel()
        {
            Console.WriteLine("\nCalling TrainCustomModel()...");
            var result = _speechToText.TrainCustomModel(_createdCustomizationID);
            if (result != null)
            {
                Console.WriteLine("Training...");
            }
            else
            {
                Console.WriteLine("Result is null");
            }

            IsTrainingComplete();
        }
        #endregion

        #region Is Training Complete
        private bool IsTrainingComplete()
        {
            var result = _speechToText.ListCustomModel(_createdCustomizationID);

            string status = result.Status.ToLower();
            Console.WriteLine(string.Format("Classifier status is {0}", status));

            if (status == "ready" || status == "available")
                autoEvent.Set();
            else
            {
                Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(5000);
                    IsTrainingComplete();
                });
            }

            return result.Status.ToLower() == "ready";
        }
        #endregion
    }
}
