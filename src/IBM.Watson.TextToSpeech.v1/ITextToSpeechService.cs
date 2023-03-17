/**
* (C) Copyright IBM Corp. 2016, 2023.
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

using IBM.Cloud.SDK.Core.Http;
using System.Collections.Generic;
using IBM.Watson.TextToSpeech.v1.Model;

namespace IBM.Watson.TextToSpeech.v1
{
    public partial interface ITextToSpeechService
    {
        DetailedResponse<Voices> ListVoices();
        DetailedResponse<Voice> GetVoice(string voice, string customizationId = null);
        DetailedResponse<System.IO.MemoryStream> Synthesize(string text, string accept = null, string voice = null, string customizationId = null, string spellOutMode = null, long? ratePercentage = null, long? pitchPercentage = null);
        DetailedResponse<Pronunciation> GetPronunciation(string text, string voice = null, string format = null, string customizationId = null);
        DetailedResponse<CustomModel> CreateCustomModel(string name, string language = null, string description = null);
        DetailedResponse<CustomModels> ListCustomModels(string language = null);
        DetailedResponse<object> UpdateCustomModel(string customizationId, string name = null, string description = null, List<Word> words = null);
        DetailedResponse<CustomModel> GetCustomModel(string customizationId);
        DetailedResponse<object> DeleteCustomModel(string customizationId);
        DetailedResponse<object> AddWords(string customizationId, List<Word> words);
        DetailedResponse<Words> ListWords(string customizationId);
        DetailedResponse<object> AddWord(string customizationId, string word, string translation, string partOfSpeech = null);
        DetailedResponse<Translation> GetWord(string customizationId, string word);
        DetailedResponse<object> DeleteWord(string customizationId, string word);
        DetailedResponse<Prompts> ListCustomPrompts(string customizationId);
        DetailedResponse<Prompt> AddCustomPrompt(string customizationId, string promptId, PromptMetadata metadata, System.IO.MemoryStream file);
        DetailedResponse<Prompt> GetCustomPrompt(string customizationId, string promptId);
        DetailedResponse<object> DeleteCustomPrompt(string customizationId, string promptId);
        DetailedResponse<Speakers> ListSpeakerModels();
        DetailedResponse<SpeakerModel> CreateSpeakerModel(string speakerName, System.IO.MemoryStream audio);
        DetailedResponse<SpeakerCustomModels> GetSpeakerModel(string speakerId);
        DetailedResponse<object> DeleteSpeakerModel(string speakerId);
        DetailedResponse<object> DeleteUserData(string customerId);
    }
}
