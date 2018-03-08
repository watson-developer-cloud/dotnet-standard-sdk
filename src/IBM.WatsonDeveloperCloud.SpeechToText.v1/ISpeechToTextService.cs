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
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Model;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1
{
    public partial interface ISpeechToTextService
    {
        SpeechModel GetModel(string modelId);

        SpeechModels ListModels();
        SpeechRecognitionResults RecognizeSessionless(string model = null, string customizationId = null, string acousticCustomizationId = null, double? customizationWeight = null, string version = null, byte[] audio = null, string contentType = null, long? inactivityTimeout = null, List<string> keywords = null, float? keywordsThreshold = null, long? maxAlternatives = null, float? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool? profanityFilter = null, bool? smartFormatting = null, bool? speakerLabels = null);
        RecognitionJob CheckJob(string id);

        RecognitionJobs CheckJobs();

        RecognitionJob CreateJob(byte[] audio, string contentType, string model = null, string callbackUrl = null, string events = null, string userToken = null, long? resultsTtl = null, string customizationId = null, string acousticCustomizationId = null, double? customizationWeight = null, string version = null, long? inactivityTimeout = null, List<string> keywords = null, float? keywordsThreshold = null, long? maxAlternatives = null, float? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool? profanityFilter = null, bool? smartFormatting = null, bool? speakerLabels = null);

        object DeleteJob(string id);

        RegisterStatus RegisterCallback(string callbackUrl, string userSecret = null);

        object UnregisterCallback(string callbackUrl);
        LanguageModel CreateLanguageModel(CreateLanguageModel createLanguageModel);

        object DeleteLanguageModel(string customizationId);

        LanguageModel GetLanguageModel(string customizationId);

        LanguageModels ListLanguageModels(string language = null);

        object ResetLanguageModel(string customizationId);

        object TrainLanguageModel(string customizationId, string wordTypeToAdd = null, double? customizationWeight = null);

        object UpgradeLanguageModel(string customizationId);
        object AddCorpus(string customizationId, string corpusName, System.IO.Stream corpusFile, bool? allowOverwrite = null, string corpusFileContentType = null);

        object DeleteCorpus(string customizationId, string corpusName);

        Corpus GetCorpus(string customizationId, string corpusName);

        Corpora ListCorpora(string customizationId);
        object AddWord(string customizationId, string wordName, CustomWord customWord);

        object AddWords(string customizationId, CustomWords customWords);

        object DeleteWord(string customizationId, string wordName);

        Word GetWord(string customizationId, string wordName);

        Words ListWords(string customizationId, string wordType = null, string sort = null);
        AcousticModel CreateAcousticModel(CreateAcousticModel createAcousticModel);

        object DeleteAcousticModel(string customizationId);

        AcousticModel GetAcousticModel(string customizationId);

        AcousticModels ListAcousticModels(string language = null);

        object ResetAcousticModel(string customizationId);

        object TrainAcousticModel(string customizationId, string customLanguageModelId = null);

        object UpgradeAcousticModel(string customizationId, string customLanguageModelId = null);
        object AddAudio(string customizationId, string audioName, byte[] audioResource, string contentType, string containedContentType = null, bool? allowOverwrite = null);

        object DeleteAudio(string customizationId, string audioName);

        AudioListing GetAudio(string customizationId, string audioName);

        AudioResources ListAudio(string customizationId);
    }
}
