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

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Example
{
    public class LanguageTranslatorServiceExample
    {
        private LanguageTranslatorService _languageTranslator = new LanguageTranslatorService();
        private string _glossaryPath = "glossary.tmx";
        private string _baseModel = "en-es";
        private string _customModelName = "dotnet-standard-custom-translation-model";
        private string _customModelID = "en-es";
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
            //DeleteModel();
        }

        #region List Models
        private void ListModels()
        {
            Console.WriteLine("Calling ListModels()...");

            var result = _languageTranslator.ListModels();

            if(result != null)
            {
                if(result.Models != null && result.Models.Count > 0)
                {
                    Console.WriteLine("Models found:");
                    foreach (TranslationModel model in result.Models)
                    {
                        Console.WriteLine(string.Format("Name: {0} | Status: {1} | ModelID: {2}", model.Name, model.Status, model.ModelId));
                    }
                }
                else
                {
                    Console.WriteLine("No models were found.");
                }
            }
            else
            {
                Console.WriteLine("Failed to list models.");
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
                    Console.WriteLine(string.Format("Model ID: {0}", result.ModelId));
                    _customModelID = result.ModelId;
                }
                else
                {
                    Console.WriteLine("Failed to create custom model.");
                }
            }
        }
        #endregion

        #region Get Model Details
        private void GetModelDetails()
        {
            Console.WriteLine(string.Format("Calling GetModdelDetails({0})...", _customModelID));

            var result = _languageTranslator.GetModel(_customModelID);

            if (result != null)
            {
                Console.WriteLine(string.Format("Name: {0} | Status: {1} | ModelID: {2}", result.Name, result.Status, result.ModelId));
            }
            else
            {
                Console.WriteLine(string.Format("Failed to get details for model {0}.", _customModelID));
            }
        }
        #endregion

        #region Translate
        private void Translate()
        {
            Console.WriteLine(string.Format("Calling Translate({0}, {1})...", _customModelID, _text));

            var translateRequest = new TranslateRequest()
            {
                Text = new List<string>()
                {
                    _text
                },
                ModelId = _customModelID
            };

            var result = _languageTranslator.Translate(translateRequest);

            if (result != null)
            {
                Console.WriteLine(string.Format("Word Count: {0} | Character Count: {1}", result.WordCount, result.CharacterCount));
                if(result.Translations != null && result.Translations.Count > 0)
                {
                    foreach(Translation translation in result.Translations)
                    {
                        Console.WriteLine(string.Format("Translation: {0}", translation.TranslationOutput));
                    }
                }
                else
                {
                    Console.WriteLine("Translation was not found.");
                }
            }
            else
            {
                Console.WriteLine("Failed to translate.");
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
                if (result.Languages != null && result.Languages.Count > 0)
                {
                    Console.WriteLine("Languages found.");
                    foreach (IdentifiableLanguage language in result.Languages)
                    {
                        Console.WriteLine(string.Format("Name: {0} | Lanuguage: {1}", language.Name, language.Language));
                    }
                }
                else
                {
                    Console.WriteLine("Identifiable languages were not found.");
                }
            }
            else
            {
                Console.WriteLine("Failed to get identifiable languages.");
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
                if (result.Languages != null && result.Languages.Count > 0)
                {
                    Console.WriteLine("Languages identified.");
                    foreach (IdentifiedLanguage language in result.Languages)
                    {
                        Console.WriteLine(string.Format("Language: {0} | Confidence: {1}", language.Language, language.Confidence));
                    }
                }
                else
                {
                    Console.WriteLine("Language was not found.");
                }
            }
            else
            {
                Console.WriteLine("Failed to identify languages.");
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
                Console.WriteLine("deleted: {0}", result.Status);
            }
            else
            {
                Console.WriteLine("Failed to delete models.");
            }
        }
        #endregion
    }
}
