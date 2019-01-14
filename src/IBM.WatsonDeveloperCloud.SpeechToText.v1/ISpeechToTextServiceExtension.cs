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
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Model;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1
{
    public partial interface ISpeechToTextService
    {
        SpeechRecognitionResults Recognize(string contentType,
                                           Stream audio,
                                           string transferEncoding = "",
                                           string model = "en-US_BroadbandModel",
                                           string languageCustomizationId = null,
                                           bool? continuous = null,
                                           int? inactivityTimeout = null,
                                           string[] keywords = null,
                                           double? keywordsThreshold = null,
                                           int? maxAlternatives = null,
                                           double? wordAlternativesThreshold = null,
                                           bool? wordConfidence = null,
                                           bool? timestamps = null,
                                           bool profanityFilter = false,
                                           bool? smartFormatting = null,
                                           bool? speakerLabels = null,
                                           string customizationId = null);
    }
}
