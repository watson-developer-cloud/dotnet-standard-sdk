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

using IBM.WatsonDeveloperCloud.PersonalityInsights.v3;
using IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Models;
using System;
using System.IO;

namespace IBM.WatsonDeveloperCloud.PersonalityInsights.Example
{
    public class PersonalityInsightsServiceExample
    {
        private PersonalityInsightsService _personalityInsight = new PersonalityInsightsService();

        public PersonalityInsightsServiceExample(string username, string password)
        {
            _personalityInsight.SetCredential(username, password);

            GetProfile();
        }

        #region Get Profile
        private void GetProfile()
        {
            Console.WriteLine("Calling GetProfile()...");

            var result = _personalityInsight.GetProfile();

            if (result != null)
            {
                if (result.Personality != null && result.Personality.Count > 0)
                {
                    foreach (TraitTreeNode node in result.Personality)
                    {
                        Console.WriteLine("Name: {0} | RawScore: {1}", node.Name, node.RawScore);
                    }
                }
                else
                {
                    Console.WriteLine("Could not find personality.");
                }
            }
            else
            {
                Console.WriteLine("Results are null.");
            }
        }
        #endregion
    }
}
