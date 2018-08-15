/**
* Copyright 2018 IBM Corp. All Rights Reserved.
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

using System.Collections.Generic;
using System.Runtime.Serialization;
using IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model;

namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1
{
    public partial interface ITextToSpeechService
    {
        Voice GetVoice(string voice, string customizationId = null, Dictionary<string, object> customData = null);
        Voices ListVoices(Dictionary<string, object> customData = null);
        System.IO.MemoryStream Synthesize(Text text, string accept = null, string voice = null, string customizationId = null, Dictionary<string, object> customData = null);
        Pronunciation GetPronunciation(string text, string voice = null, string format = null, string customizationId = null, Dictionary<string, object> customData = null);
        VoiceModel CreateVoiceModel(CreateVoiceModel createVoiceModel, Dictionary<string, object> customData = null);
        BaseModel DeleteVoiceModel(string customizationId, Dictionary<string, object> customData = null);
        VoiceModel GetVoiceModel(string customizationId, Dictionary<string, object> customData = null);
        VoiceModels ListVoiceModels(string language = null, Dictionary<string, object> customData = null);
        BaseModel UpdateVoiceModel(string customizationId, UpdateVoiceModel updateVoiceModel, Dictionary<string, object> customData = null);
        BaseModel AddWord(string customizationId, string word, Translation translation, Dictionary<string, object> customData = null);
        BaseModel AddWords(string customizationId, Words customWords, Dictionary<string, object> customData = null);
        BaseModel DeleteWord(string customizationId, string word, Dictionary<string, object> customData = null);
        Translation GetWord(string customizationId, string word, Dictionary<string, object> customData = null);
        Words ListWords(string customizationId, Dictionary<string, object> customData = null);
        BaseModel DeleteUserData(string customerId, Dictionary<string, object> customData = null);
    }
}
