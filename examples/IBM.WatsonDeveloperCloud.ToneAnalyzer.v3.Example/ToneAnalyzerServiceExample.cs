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

using IBM.WatsonDeveloperCloud.ToneAnalyzer.v3;
using IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Model;
using System;

namespace IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Example
{
    public class ToneAnalyzerServiceExample
    {
        private ToneAnalyzerService _toneAnalyzer = new ToneAnalyzerService();
        private string _text = "I know I've made some very poor decisions recently, but I can give you my complete assurance that my work will be back to normal. I've still got the greatest enthusiasm and confidence in the mission. And I want to help you.";

        public ToneAnalyzerServiceExample(string username, string password)
        {
            _toneAnalyzer.SetCredential(username, password);

            AnalyzeTone();
        }

        #region Analyze Tone
        private void AnalyzeTone()
        {
            Console.WriteLine("Calling GetTone()...");

            var result = _toneAnalyzer.AnalyzeTone(_text);

            if(result != null)
            {
                Console.WriteLine("Document tone");
                if (result.DocumentTone != null)
                {
                    if (result.DocumentTone.ToneCategories != null && result.DocumentTone.ToneCategories.Count > 0)
                    {
                        foreach (ToneCategory toneCategory in result.DocumentTone.ToneCategories)
                        {
                            Console.WriteLine(string.Format("Category name: {0} | Category ID: {1}", toneCategory.CategoryName, toneCategory.CategoryId));
                            if (toneCategory.Tones != null && toneCategory.Tones.Count > 0)
                            {
                                foreach (ToneScore toneScore in toneCategory.Tones)
                                {
                                    Console.WriteLine(string.Format("Tone name: {0} | Tone ID: {1} | Tone Score: {2}", toneScore.ToneName, toneScore.ToneId, toneScore.Score));
                                }
                            }
                        }

                    }
                }


                Console.WriteLine("Sentence tone");
                if (result.SentencesTone != null && result.SentencesTone.Count > 0)
                {
                    foreach (SentenceAnalysis sentenceTone in result.SentencesTone)
                    {
                        Console.WriteLine(string.Format("SentenceID: {0} | Text: {1} | InputFrom: {2} | InputTo: {3}",
                            sentenceTone.SentenceId,
                            sentenceTone.Text,
                            sentenceTone.InputFrom,
                            sentenceTone.InputTo));

                        if (sentenceTone.ToneCategories != null && sentenceTone.ToneCategories.Count > 0)
                        {
                            foreach (ToneCategory toneCategory in sentenceTone.ToneCategories)
                            {
                                Console.WriteLine(string.Format("Category name: {0} | Category ID: {1}", toneCategory.CategoryName, toneCategory.CategoryId));
                                if (toneCategory.Tones != null && toneCategory.Tones.Count > 0)
                                {
                                    foreach (ToneScore toneScore in toneCategory.Tones)
                                    {
                                        Console.WriteLine(string.Format("Tone name: {0} | Tone ID: {1} | Tone Score: {2}", toneScore.ToneName, toneScore.ToneId, toneScore.Score));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }
        #endregion
    }
}
