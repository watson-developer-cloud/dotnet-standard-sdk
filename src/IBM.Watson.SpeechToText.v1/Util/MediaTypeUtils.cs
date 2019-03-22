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

using System.IO;
using IBM.Watson.Http;

namespace IBM.Watson.SpeechToText.v1.Util
{
    public static class MediaTypeUtils
    {
        public static string GetMediaTypeFromFile(this FileStream audio)
        {
            string contentType = string.Empty;
            string fileExtension = Path.GetExtension(audio.Name);

            if (fileExtension.Equals(".wav"))
                contentType = HttpMediaType.AUDIO_WAV;
            else if (fileExtension.Equals(".ogg"))
                contentType = HttpMediaType.AUDIO_OGG;
            else if (fileExtension.Equals(".oga"))
                contentType = HttpMediaType.AUDIO_OGG;
            else if (fileExtension.Equals(".flac"))
                contentType = HttpMediaType.AUDIO_FLAC;
            else if (fileExtension.Equals(".raw"))
                contentType = HttpMediaType.AUDIO_RAW;

            return contentType;
        }
    }
}