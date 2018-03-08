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

using System.Runtime.Serialization;
using System.Collections.Generic;
using IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model;

namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1
{
    public interface ITextToSpeechService
    {
        Voice GetVoice(string voice, string customizationId = null);

        Voices ListVoices();
        System.IO.Stream Synthesize(Text text, string accept = null, string voice = null, string customizationId = null);
        Pronunciation GetPronunciation(string text, string voice = null, string format = null, string customizationId = null);
        VoiceModel CreateVoiceModel(CreateVoiceModel createVoiceModel);

        object DeleteVoiceModel(string customizationId);

        VoiceModel GetVoiceModel(string customizationId);

        VoiceModels ListVoiceModels(string language = null);

        object UpdateVoiceModel(string customizationId, UpdateVoiceModel updateVoiceModel);
        object AddWord(string customizationId, string word, Translation translation);

        object AddWords(string customizationId, Words customWords);

        object DeleteWord(string customizationId, string word);

        Translation GetWord(string customizationId, string word);

        Words ListWords(string customizationId);
    }
}
