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

using IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model;
using System.Collections.Generic;
using System.IO;

namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1
{
    interface ITextToSpeechService
    {
        List<Voice> GetVoices();
        Voice GetVoice(string voiceName);
        Pronunciation GetPronunciation(string text);
        Pronunciation GetPronunciation(string text, Voice voice, Phoneme phoneme);
        Stream Synthesize(string text, Voice voice, AudioType audioType);
        List<CustomVoiceModel> GetCustomVoiceModels(string language);
        CustomVoiceModel GetCustomVoiceModel(string modelId);
        CustomVoiceModel SaveCustomVoiceModel(CustomVoiceModel model);
        void DeleteCustomVoiceModel(CustomVoiceModel model);
        void DeleteCustomVoiceModel(string modelID);
        List<CustomWordTranslation> GetWords(CustomVoiceModel model);
        List<CustomWordTranslation> GetWords(string modelID);
        void SaveWords(CustomVoiceModel model, params CustomWordTranslation[] translations);
        void SaveWords(string modelID, params CustomWordTranslation[] translations);
        void DeleteWord(CustomVoiceModel model, CustomWordTranslation translation);
        void DeleteWord(string modelID, CustomWordTranslation translation);
        void DeleteWord(CustomVoiceModel model, string word);
        void DeleteWord(string modelID, string word);
    }

}
