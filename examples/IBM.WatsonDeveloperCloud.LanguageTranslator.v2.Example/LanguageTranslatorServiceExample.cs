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

using IBM.WatsonDeveloperCloud.LanguageTranslator.v2;
using IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Model;
using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Example
{
    public class LanguageTranslatorServiceExample
    {
        private LanguageTranslatorService _languageTranslator = new LanguageTranslatorService();
        private string _glossaryPath = "glossary.tmx";
        private string _baseModel = "en-fr";
        private string _customModelName = "dotnetExampleModel";
        private string _customModelID = "en-fr";
        private string _text = "I'm sorry, Dave. I'm afraid I can't do that.";

        public LanguageTranslatorServiceExample(string username, string password)
        {
            _languageTranslator.SetCredential(username, password);

            ListModels();
            CreateModel();
            GetModelDetails();
            Translate();
            GetIdentifiableLanguages();
            Identify();
            DeleteModel();

            Console.WriteLine("\nLanguage Translation examples complete.");
        }

        #region List Models
        private void ListModels()
        {
            Console.WriteLine("Calling ListModels()...");

            var result = _languageTranslator.ListModels();

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }
        #endregion

        #region Create Model
        private void CreateModel()
        {
            using (FileStream fs = File.OpenRead(_glossaryPath))
            {
                Console.WriteLine(string.Format("Calling CreateModel({0}, {1}, {2})...", _baseModel, _customModelName, _glossaryPath));
                
                var result = _languageTranslator.CreateModel(_baseModel, _customModelName, fs);

                if (result != null)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                    _customModelID = result.ModelId;
                }
                else
                {
                    Console.WriteLine("result is null.");
                }
            }
        }
        #endregion

        #region Get Model Details
        private void GetModelDetails()
        {
            Console.WriteLine(string.Format("Calling GetModelDetails({0})...", _customModelID));

            var result = _languageTranslator.GetModel(_customModelID);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }
        #endregion

        #region Translate
        private void Translate()
        {
            Console.WriteLine(string.Format("Calling Translate({0}, {1})...", _baseModel, _text));

            var translateRequest = new TranslateRequest()
            {
                Text = new List<string>()
                {
                    _text
                },
                ModelId = _baseModel
            };

            var result = _languageTranslator.Translate(translateRequest);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }
        #endregion

        #region Get Identifiable Languages
        private void GetIdentifiableLanguages()
        {
            Console.WriteLine("Calling ListIdentifiableLanguages()...");

            var result = _languageTranslator.ListIdentifiableLanguages();

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }
        #endregion

        #region Identify
        private void Identify()
        {
            Console.WriteLine(string.Format("Calling Identify({0})...", _text));

            var result = _languageTranslator.Identify(_text);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }
        #endregion

        #region Delete Model
        private void DeleteModel()
        {
            Console.WriteLine(string.Format("Calling DeleteModel({0})...", _customModelID));

            var result = _languageTranslator.DeleteModel(_customModelID);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }
        #endregion
    }
}
