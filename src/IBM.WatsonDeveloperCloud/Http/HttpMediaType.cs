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

using System;

namespace IBM.WatsonDeveloperCloud.Http
{
    public static class HttpMediaType
    {
        public const string APPLICATION_ATOM_XML = "application/atom+xml";
        public const string APPLICATION_FORM_URLENCODED = "application/x-www-form-urlencoded";
        public const string APPLICATION_JSON = "application/json";
        public const string APPLICATION_MS_WORD = "application/msword";
        public const string APPLICATION_MS_WORD_DOCX = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        public const string APPLICATION_OCTET_STREAM = "application/octet-stream";
        public const string APPLICATION_PDF = "application/pdf";
        public const string APPLICATION_SVG_XML = "application/svg+xml";
        public const string APPLICATION_XHTML_XML = "application/xhtml+xml";
        public const string APPLICATION_ZIP = "application/zip";
        public const string APPLICATION_XML = "application/xml";
        public const string AUDIO_OGG = "audio/ogg; codecs=opus";
        public const string AUDIO_OGG_VORBIS = "audio/ogg; codecs=vorbis";
        public const string AUDIO_WAV = "audio/wav";
        public const string AUDIO_PCM = "audio/l16";
        public const string AUDIO_BASIC = "audio/basic";
        public const string AUDIO_FLAC = "audio/flac";
        public const string AUDIO_RAW = "audio/l16";
        public static String createAudioRaw(int rate)
        {
            return AUDIO_RAW + "; rate=" + rate;
        }
        public const string BINARY_OCTET_STREAM = "binary/octet-stream";
        public const string JSON = APPLICATION_JSON + "; charset=utf-8";
        public const string MEDIA_TYPE_WILDCARD = "*";
        public const string MULTIPART_FORM_DATA = "multipart/form-data";
        public const string TEXT_CSV = "text/csv";
        public const string TEXT_HTML = "text/html";
        public const string TEXT_PLAIN = "text/plain";
        public const string TEXT = TEXT_PLAIN + "; charset=utf-8";
        public const string TEXT_XML = "text/xml";
        public const string WILDCARD = "*/*";

    }
}