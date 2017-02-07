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

using System;
using IBM.WatsonDeveloperCloud.TextToSpeech.v1;
using IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model;

namespace IBM.WatsonDeveloperCloud.TextToSpeech.Example
{
    public class TextToSpeechServiceExample
    {
        private TextToSpeechService _textToSpeech = new TextToSpeechService();
        private string _voice = "en-US_AllisonVoice";
        private string _pronunciation = "";
        private string _text = "I'm sorry, Dave. I'm afraid I can't do that.";

        public TextToSpeechServiceExample(string username, string password)
        {
            _textToSpeech.SetCredential(username, password);

            GetVoices();
            GetVoice(_voice);
            GetPronunciation(_pronunciationToGet);
            Synthesize(_text, _voice, AudioType.WAV);
            GetCustomVoiceModels();
            GetCustomVoiceModel();
            DeleteCustomVoiceModel();
            GetWords();
            SaveWords();
            DeleteWord();
        }
    }
}
