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
using System.IO;

namespace IBM.WatsonDeveloperCloud.SpeechToText.Example
{
    public class SpeechToTextServiceExample
    {
        private SpeechToTextService _speechToText = new SpeechToTextService();
        private string _path = "test-audio.wav";
        private string _modelToGet = "en-US_BroadbandModel";
        private string _sessionID;

        public SpeechToTextServiceExample(string username, string password)
        {
            _speechToText.SetCredential(username, password);

            GetModels();
            GetModel(_modelToGet);
            Recognize();
            CreateSession();
            GetSessionStatus();
            DeleteSession();
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

        #region Recognize
        private void Recognize()
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

        #region Delete Session
        private void DeleteSession()
        {
            Console.WriteLine(string.Format("Calling DeleteSession({0})...", _sessionID));
            _speechToText.DeleteSession(_sessionID);
            Console.WriteLine(string.Format("Session {0} deleted.", _sessionID));
        }
        #endregion
    }
}
