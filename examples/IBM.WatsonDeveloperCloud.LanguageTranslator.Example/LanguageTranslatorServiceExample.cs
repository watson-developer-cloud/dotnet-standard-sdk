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
using IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Models;
using System;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.Example
{
    public class LanguageTranslatorServiceExample
    {
        private LanguageTranslatorService _languageTranslator = new LanguageTranslatorService();

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
                    foreach (ModelPayload model in result.Models)
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
            Console.WriteLine("Calling CreateModel()...");

            //var result = _languageTranslator.CreateModel();

            if (result != null)
            {
                
            }
            else
            {
                Console.WriteLine("Failed to list models.");
            }
        }
        #endregion

        #region Get Model Details
        private void GetModelDetails()
        {
            Console.WriteLine("Calling ListModels()...");

            var result = _languageTranslator.ListModels();

            if (result != null)
            {
                
            }
            else
            {
                Console.WriteLine("Failed to list models.");
            }
        }
        #endregion

        #region Translate
        private void Translate()
        {
            Console.WriteLine("Calling ListModels()...");

            var result = _languageTranslator.ListModels();

            if (result != null)
            {
                
            }
            else
            {
                Console.WriteLine("Failed to list models.");
            }
        }
        #endregion

        #region Get Identifiable Languages
        private void GetIdentifiableLanguages()
        {
            Console.WriteLine("Calling ListModels()...");

            var result = _languageTranslator.ListModels();

            if (result != null)
            {
                
            }
            else
            {
                Console.WriteLine("Failed to list models.");
            }
        }
        #endregion

        #region Identify
        private void Identify()
        {
            Console.WriteLine("Calling ListModels()...");

            var result = _languageTranslator.ListModels();

            if (result != null)
            {
                
            }
            else
            {
                Console.WriteLine("Failed to list models.");
            }
        }
        #endregion

        #region Delete Model
        private void DeleteModel()
        {
            Console.WriteLine("Calling ListModels()...");

            var result = _languageTranslator.ListModels();

            if (result != null)
            {
                
            }
            else
            {
                Console.WriteLine("Failed to list models.");
            }
        }
        #endregion
    }
}
