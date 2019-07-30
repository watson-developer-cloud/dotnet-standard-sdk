﻿/**
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

using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Cloud.SDK.Core.Http;
using IBM.Watson.SpeechToText.v1.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace IBM.Watson.SpeechToText.v1.Examples
{
    public class ServiceExample
    {
        string apikey = "{apikey}";
        string url = "{url}";
        private string testAudioPath = @"SpeechToTextTestData/test-audio.wav";
        private string corpusPath = @"SpeechToTextTestData/theJabberwocky-utf8.txt";
        private string grammarName = "dotnet-sdk-grammars";
        private string grammarPath = @"SpeechToTextTestData/confirm.abnf";
        private string grammarsContentType = "application/srgs";
        private string audioName = "firstOrbit";
        private static string acousticResourceUrl = "https://archive.org/download/Greatest_Speeches_of_the_20th_Century/KeynoteAddressforDemocraticConvention_64kb.mp3";
        private string jobId;
        private string customizationId;
        private string corpusName;
        private string acousticCustomizationId;
        private static byte[] acousticResourceData;

        static void Main(string[] args)
        {
            ServiceExample example = new ServiceExample();

            try
            {
                acousticResourceData = DownloadAcousticResource(acousticResourceUrl).Result;
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Failed to get acoustic resource data: {0}", e.Message));
            }

            example.ListModels();
            example.GetModel();

            example.Recognize();

            example.RegisterCallback();
            example.UnregisterCallback();

            example.CreateJob();
            example.CheckJobs();
            example.CheckJob();

            example.ListLanguageModels();
            example.CreateLanguageModel();
            example.GetLanguageModel();
            example.TrainLanguageModel();
            example.ResetLanguageModel();
            example.UpgradeLanguageModel();

            example.ListCorpora();
            example.AddCorpus();
            example.GetCorpus();

            example.ListWords();
            example.AddWords();
            example.AddWord();
            example.GetWord();

            example.ListGrammars();
            example.AddGrammar();
            example.GetGrammar();

            example.ListAcousticModels();
            example.CreateAcousticModel();
            example.GetAcousticModel();
            example.TrainAcousticModel();
            example.ResetAcousticModel();
            example.UpgradeAcousticModel();

            example.ListAudio();
            example.AddAudio();
            example.GetAudio();

            example.DeleteAudio();
            example.DeleteGrammar();
            example.DeleteWord();
            example.DeleteCorpus();
            example.DeleteJob();
            example.DeleteLanguageModel();

            Console.WriteLine("Examples complete. Press any key to close the application.");
            Console.ReadKey();
        }

        #region Models
        public void ListModels()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.ListModels();

            Console.WriteLine(result.Response);
        }

        public void GetModel()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.GetModel(
                modelId: "en-US_BroadbandModel"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Synchronous
        public void Recognize()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.Recognize(
                audio: File.ReadAllBytes("audio-file2.flac"),
                contentType: "audio/flac",
                wordAlternativesThreshold: 0.9f,
                keywords: new List<string>()
                {
                    "colorado",
                    "tornado",
                    "tornadoes"
                },
                keywordsThreshold: 0.5f
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Asynchronous
        public void RegisterCallback()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.RegisterCallback(
                callbackUrl: "http://{user_callback_path}/job_results",
                userSecret: "ThisIsMySecret"
                );

            Console.WriteLine(result.Response);
        }

        public void UnregisterCallback()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.UnregisterCallback(
                callbackUrl: "http://{user_callback_path}/job_results"
                );

            Console.WriteLine(result.StatusCode);
        }

        public void CreateJob()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.CreateJob(
                callbackUrl: "http://{user_callback_path}/job_results",
                userToken: "job25",
                audio: File.ReadAllBytes("audio-file.flac"),
                contentType: "audio/flac",
                timestamps: true
                );

            Console.WriteLine(result.Response);

            jobId = result.Result.Id;
        }

        public void CheckJobs()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.CheckJobs();

            Console.WriteLine(result.Response);
        }

        public void CheckJob()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.CheckJob(
                id: "{id}"
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteJob()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.DeleteJob(
                id: "{id}"
                );

            Console.WriteLine(result.StatusCode);
        }
        #endregion

        #region Custom Langauge Models
        public void ListLanguageModels()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.ListLanguageModels();

            Console.WriteLine(result.Response);
        }

        public void CreateLanguageModel()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.CreateLanguageModel(
                name: "First example language model",
                baseModelName: "en-US_BroadbandModel",
                description: "First custom language model example"
                );

            Console.WriteLine(result.Response);

            customizationId = result.Result.CustomizationId;
        }

        public void GetLanguageModel()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.GetLanguageModel(
                customizationId: "{customizationId}"
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteLanguageModel()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.DeleteLanguageModel(
                customizationId: "{customizationId}"
                );

            Console.WriteLine(result.StatusCode);
        }

        public void TrainLanguageModel()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.TrainLanguageModel(
                customizationId: "{customizationId}"
                );

            Console.WriteLine(result.Response);
        }

        public void ResetLanguageModel()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.ResetLanguageModel(
                customizationId: "{customizationId}"
                );

            Console.WriteLine(result.Response);
        }

        public void UpgradeLanguageModel()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.UpgradeLanguageModel(
                customizationId: "{customizationId}"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Custom Corpora
        public void ListCorpora()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.ListCorpora(
                customizationId: "{customizationId}"
                );

            Console.WriteLine(result.Response);
        }

        public void AddCorpus()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            DetailedResponse<object> result = null;
            using (FileStream fs = File.OpenRead("corpus1.txt"))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    result = service.AddCorpus(
                        customizationId: "{customizationId}",
                        corpusFile: ms,
                        corpusName: "corpus1"
                        );
                }
            }

            Console.WriteLine(result.Response);
        }

        public void GetCorpus()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.GetCorpus(
                customizationId: "{customizationId}",
                corpusName: "corpus1"
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteCorpus()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.DeleteCorpus(
                customizationId: "{customizationId}",
                corpusName: "corpus1"
                );

            Console.WriteLine(result.StatusCode);
        }
        #endregion

        #region Custom Words
        public void ListWords()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.ListWords(
                customizationId: "{customizationId}",
                sort: "+alphabetical"
                );

            Console.WriteLine(result.Response);
        }

        public void AddWords()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var customWords = new List<CustomWord>()
            {
                new CustomWord()
                {
                    DisplayAs = "HHonors",
                    SoundsLike = new List<string>()
                    {
                        "hilton honors",
                        "H. honors"
                    },
                    Word = "HHonors"
                },
                new CustomWord()
                {
                    SoundsLike = new List<string>()
                    {
                        "I. tripe E."
                    },
                    Word = "IEEE"
                }
            };

            var result = service.AddWords(
                customizationId: "{customizationId}",
                words: customWords
                );

            Console.WriteLine(result.Response);
        }

        public void AddWord()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.AddWord(
                customizationId: "{customizationId}",
                wordName: "NCAA",
                word: "dotnet",
                soundsLike: new List<string>()
                {
                    "N. C. A. A.",
                    "N. C. double A."
                },
                displayAs: "NCAA"
                );

            Console.WriteLine(result.Response);
        }

        public void GetWord()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.GetWord(
                customizationId: "{customizationId}",
                wordName: "NCAA"
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteWord()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.DeleteWord(
                customizationId: "{customizationId}",
                wordName: "NCAA"
                );

            Console.WriteLine(result.StatusCode);
        }
        #endregion

        #region Custom Grammars
        public void ListGrammars()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.ListGrammars(
                customizationId: "{customizationId}"
                );

            Console.WriteLine(result.Response);
        }

        public void AddGrammar()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.AddGrammar(
                customizationId: "{customizationId}",
                grammarFile: File.ReadAllText("list.abnf"),
                grammarName: "list-abnf",
                contentType: "application/srgs"
                );

            Console.WriteLine(result.Response);
        }

        public void GetGrammar()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.GetGrammar(
                customizationId: "{customizationId}",
                grammarName: "list-abnf"
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteGrammar()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.DeleteGrammar(
                customizationId: "{customizationId}",
                grammarName: "list-abnf"
                );

            Console.WriteLine(result.StatusCode);
        }
        #endregion

        #region Custom Acoustic Models
        public void ListAcousticModels()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.ListAcousticModels(
                language: "en-US"
                );

            Console.WriteLine(result.Response);
        }

        public void CreateAcousticModel()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.CreateAcousticModel(
                name: "First example acoustic model",
                baseModelName: "en-US_BroadbandModel",
                description: "First custom acoustic model example"
                );

            Console.WriteLine(result.Response);

            acousticCustomizationId = result.Result.CustomizationId;
        }

        public void GetAcousticModel()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.GetAcousticModel(
                customizationId: "{customizationId}"
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteAcousticModel()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.DeleteAcousticModel(
                customizationId: "{customizationId}"
                );

            Console.WriteLine(result.StatusCode);
        }

        public void TrainAcousticModel()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.TrainAcousticModel(
                customizationId: "{customizationId}"
                );

            Console.WriteLine(result.Response);
        }

        public void ResetAcousticModel()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.ResetAcousticModel(
                customizationId: "{customizationId}"
                );

            Console.WriteLine(result.Response);
        }

        public void UpgradeAcousticModel()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.UpgradeAcousticModel(
                customizationId: "{customizationId}"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Custom Audio Resources
        public void ListAudio()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.ListAudio(
                customizationId: "{customizationId}"
                );

            Console.WriteLine(result.Response);
        }

        public void AddAudio()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.AddAudio(
                customizationId: "{customizationId}",
                contentType: "audio/wav",
                audioResource: File.ReadAllBytes("audio1.wav"),
                audioName: "audio1"
                );

            Console.WriteLine(result.Response);
        }

        public void GetAudio()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.GetAudio(
                customizationId: "{customizationId}",
                audioName: "audio2"
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteAudio()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.DeleteAudio(
                customizationId: "{customizationId}",
                audioName: "audio1"
                );

            Console.WriteLine(result.StatusCode);
        }
        #endregion

        #region User Data
        public void DeleteUserData()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            SpeechToTextService service = new SpeechToTextService(config);
            service.SetEndpoint("{url}");

            var result = service.DeleteUserData(
                customerId: "customer_ID"
                );

            Console.WriteLine(result.StatusCode);
        }
        #endregion

        #region Acoustic Resource Data
        public static async Task<byte[]> DownloadAcousticResource(string acousticResourceUrl)
        {
            var client = new HttpClient();
            var task = client.GetByteArrayAsync(acousticResourceUrl);
            var msg = await task;

            return msg;
        }
        #endregion
    }
}
