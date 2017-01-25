/**
* Copyright 2015 IBM Corp. All Rights Reserved.
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

namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model
{
    public class AudioType
    {
        public string Value { get; private set; }

        public static readonly AudioType OGG = new AudioType("audio=ogg;codecs=opus");
        public static readonly AudioType WAV = new AudioType("audio/wav");
        public static readonly AudioType FLAC = new AudioType("audio/flac");
        public static readonly AudioType L16 = new AudioType("audio/l16");
        public static readonly AudioType BASIC = new AudioType("audio/basic");

        public AudioType(string type)
        {
            this.Value = type;
        }

    }
}
