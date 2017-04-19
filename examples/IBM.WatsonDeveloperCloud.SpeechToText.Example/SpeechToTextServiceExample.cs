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

namespace IBM.WatsonDeveloperCloud.SpeechToText.Example
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
            AddCustomWords();
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
        }

        #region Get Models
        private void GetModels()
        {
            Console.WriteLine("Calling GetModels()...");
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
            Console.WriteLine(string.Format("Calling GetModel({0})...", _modelToGet));
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
                Console.WriteLine("Calling Recognize...");
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
            FileStream audio = File.OpenRead(@"Assets\test-audio.wav");

            var results = _speechToText.Recognize(audio.GetMediaTypeFromFile(),
                                  new Metadata()
                                  {
                                      PartContentType = audio.GetMediaTypeFromFile()
                                  },
                                  audio);
        }
        #endregion

        #region Create Session
        private void CreateSession()
        {
            Console.WriteLine(string.Format("Calling CreateSession({0})...", _modelToGet));
            var session = _speechToText.CreateSession(_modelToGet);

            if (session != null)
            {
                Console.WriteLine("Session received...");
                Console.WriteLine("SessionId: {0} | NewSessionUri: {1} | Recognize: {2} | RecognizeWS: {3} | ObserveResult: {4}",
                    session.SessionId,
                    session.NewSessionUri,
                    session.Recognize,
                    session.RecognizeWS,
                    session.ObserveResult);

                _sessionID = session.SessionId;
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
            Console.WriteLine(string.Format("Calling GetSessionStatus({0})...", _sessionID));
            var recognizeStatus = _speechToText.GetSessionStatus(_sessionID);


            Console.WriteLine("RecognizeStatus received...");
            Console.WriteLine("State: {0} | Model: {1} | Recognize: {2} | RecognizeWS: {3} | ObserveResult: {4}",
                recognizeStatus.Session.State,
                recognizeStatus.Session.Model,
                recognizeStatus.Session.Recognize,
                recognizeStatus.Session.RecognizeWS,
                recognizeStatus.Session.ObserveResult);
        }
        #endregion

        #region Recognize Session Body
        private void RecognizeSessionBody()
        {
            FileStream audio = File.OpenRead(@"Assets\test-audio.wav");

            var results = _speechToText.RecognizeWithSession(_sessionID, audio.GetMediaTypeFromFile(), audio);
        }
        #endregion

        #region Recognize Session Form Data
        private void RecognizeSessionFormData()
        {
            FileStream audio = File.OpenRead(@"Assets\test-audio.wav");

            var results = _speechToText.RecognizeWithSession(_sessionID,
                                             audio.GetMediaTypeFromFile(),
                                             new Metadata()
                                             {
                                                 PartContentType = audio.GetMediaTypeFromFile()
                                             },
                                             audio);
        }
        #endregion

        #region Delete Session
        private void DeleteSession()
        {
            Console.WriteLine(string.Format("Calling DeleteSession({0})...", _sessionID));
            _speechToText.DeleteSession(_sessionID);
            Console.WriteLine(string.Format("Session {0} deleted.", _sessionID));
        }
        #endregion

        #region Create Custom Model
        private void CreateCustomModel()
        {
            var result = _speechToText.CreateCustomModel("STT Custom Model", modelName, "A custom speech to text model created from the example.");
            _createdCustomizationID = result.CustomizationId;
        }
        #endregion

        #region List Custom Models
        private void ListCustomModels()
        {
            var customizations = _speechToText.ListCustomModels();
        }
        #endregion

        #region List Custom Model
        private void ListCustomModel()
        {
            var result = _speechToText.ListCustomModel(_createdCustomizationID);
        }
        #endregion

        #region Add Custom Corpus
        private void AddCustomCorpus()
        {
            var body =
                File.OpenRead(@"Assets\test-stt-corpus.txt");

            object result = _speechToText.AddCorpus(_createdCustomizationID,
                              _corpusName,
                              false,
                              body);
        }
        #endregion

        #region List Custom Corpora
        private void ListCustomCorpora()
        {
            var result = _speechToText.ListCorpora(_createdCustomizationID);
        }
        #endregion

        #region Get Custom Corpus
        private void GetCustomCorpus()
        {
            var result = _speechToText.GetCorpus(_createdCustomizationID, _corpusName);
        }
        #endregion

        #region Delete Custom Corpus
        private void DeleteCustomCorpus()
        {
            var result = _speechToText.DeleteCorpus(_createdCustomizationID, _corpusName);
        }
        #endregion

        #region Add Custom Words
        private void AddCustomWords()
        {
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
        }
        #endregion

        #region Add Custom Word
        private void AddCustomWord()
        {
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
        }
        #endregion

        #region List Custom Words
        private void ListCustomWords()
        {
            var words = _speechToText.ListCustomWords(_createdCustomizationID, null, null);
        }
        #endregion

        #region List Custom Word
        private void ListCustomWord(string wordToList)
        {
            var words = _speechToText.ListCustomWord(_createdCustomizationID, _wordName);
        }
        #endregion

        #region Delete Custom Word
        private void DeleteCustomWord(string wordToDelete)
        {
            object result = _speechToText.DeleteCustomWord(_createdCustomizationID, _wordName);
        }
        #endregion

        #region Reset Custom Model
        private void ResetCustomModel()
        {
            var result = _speechToText.ResetCustomModel(_createdCustomizationID);
        }
        #endregion

        #region Upgrade Custom Model
        private void UpgradeCustomModel()
        {
            var result = _speechToText.UpgradeCustomModel(_createdCustomizationID);
        }
        #endregion

        #region Delete Custom Model
        private void DeleteCustomModel()
        {
            var result = _speechToText.DeleteCustomModel(_createdCustomizationID);
        }
        #endregion

        #region Train Custom Model
        private void TrainCustomModel()
        {
            var result = _speechToText.TrainCustomModel(_createdCustomizationID);
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
