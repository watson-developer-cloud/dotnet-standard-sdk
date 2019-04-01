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

using IBM.Cloud.SDK.Core;
using System.Collections.Generic;
using IBM.Watson.SpeechToText.v1.Model;

namespace IBM.Watson.SpeechToText.v1
{
    public partial interface ISpeechToTextService
    {
        SpeechModel GetModel(string modelId, Dictionary<string, object> customData = null);
        SpeechModels ListModels(Dictionary<string, object> customData = null);
        SpeechRecognitionResults RecognizeSessionless(byte[] audio, string contentType = null, string model = null, string languageCustomizationId = null, string acousticCustomizationId = null, string baseModelVersion = null, double? customizationWeight = null, long? inactivityTimeout = null, List<string> keywords = null, float? keywordsThreshold = null, long? maxAlternatives = null, float? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool? profanityFilter = null, bool? smartFormatting = null, bool? speakerLabels = null, string customizationId = null, string grammarName = null, bool? redaction = null, Dictionary<string, object> customData = null);
        RecognitionJob CheckJob(string id, Dictionary<string, object> customData = null);
        RecognitionJobs CheckJobs(Dictionary<string, object> customData = null);
        RecognitionJob CreateJob(byte[] audio, string contentType = null, string model = null, string callbackUrl = null, string events = null, string userToken = null, long? resultsTtl = null, string languageCustomizationId = null, string acousticCustomizationId = null, string baseModelVersion = null, double? customizationWeight = null, long? inactivityTimeout = null, List<string> keywords = null, float? keywordsThreshold = null, long? maxAlternatives = null, float? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool? profanityFilter = null, bool? smartFormatting = null, bool? speakerLabels = null, string customizationId = null, string grammarName = null, bool? redaction = null, Dictionary<string, object> customData = null);
        BaseModel DeleteJob(string id, Dictionary<string, object> customData = null);
        RegisterStatus RegisterCallback(string callbackUrl, string userSecret = null, Dictionary<string, object> customData = null);
        BaseModel UnregisterCallback(string callbackUrl, Dictionary<string, object> customData = null);
        LanguageModel CreateLanguageModel(CreateLanguageModel createLanguageModel, Dictionary<string, object> customData = null);
        BaseModel DeleteLanguageModel(string customizationId, Dictionary<string, object> customData = null);
        LanguageModel GetLanguageModel(string customizationId, Dictionary<string, object> customData = null);
        LanguageModels ListLanguageModels(string language = null, Dictionary<string, object> customData = null);
        BaseModel ResetLanguageModel(string customizationId, Dictionary<string, object> customData = null);
        BaseModel TrainLanguageModel(string customizationId, string wordTypeToAdd = null, double? customizationWeight = null, Dictionary<string, object> customData = null);
        BaseModel UpgradeLanguageModel(string customizationId, Dictionary<string, object> customData = null);
        BaseModel AddCorpus(string customizationId, string corpusName, System.IO.FileStream corpusFile, bool? allowOverwrite = null, Dictionary<string, object> customData = null);
        BaseModel DeleteCorpus(string customizationId, string corpusName, Dictionary<string, object> customData = null);
        Corpus GetCorpus(string customizationId, string corpusName, Dictionary<string, object> customData = null);
        Corpora ListCorpora(string customizationId, Dictionary<string, object> customData = null);
        BaseModel AddWord(string customizationId, string wordName, CustomWord customWord, Dictionary<string, object> customData = null);
        BaseModel AddWords(string customizationId, CustomWords customWords, Dictionary<string, object> customData = null);
        BaseModel DeleteWord(string customizationId, string wordName, Dictionary<string, object> customData = null);
        Word GetWord(string customizationId, string wordName, Dictionary<string, object> customData = null);
        Words ListWords(string customizationId, string wordType = null, string sort = null, Dictionary<string, object> customData = null);
        BaseModel AddGrammar(string customizationId, string grammarName, string grammarFile, string contentType, bool? allowOverwrite = null, Dictionary<string, object> customData = null);
        BaseModel DeleteGrammar(string customizationId, string grammarName, Dictionary<string, object> customData = null);
        Grammar GetGrammar(string customizationId, string grammarName, Dictionary<string, object> customData = null);
        Grammars ListGrammars(string customizationId, Dictionary<string, object> customData = null);
        AcousticModel CreateAcousticModel(CreateAcousticModel createAcousticModel, Dictionary<string, object> customData = null);
        BaseModel DeleteAcousticModel(string customizationId, Dictionary<string, object> customData = null);
        AcousticModel GetAcousticModel(string customizationId, Dictionary<string, object> customData = null);
        AcousticModels ListAcousticModels(string language = null, Dictionary<string, object> customData = null);
        BaseModel ResetAcousticModel(string customizationId, Dictionary<string, object> customData = null);
        BaseModel TrainAcousticModel(string customizationId, string customLanguageModelId = null, Dictionary<string, object> customData = null);
        BaseModel UpgradeAcousticModel(string customizationId, string customLanguageModelId = null, Dictionary<string, object> customData = null, bool? force = null);
        BaseModel AddAudio(string customizationId, string audioName, byte[] audioResource, string contentType = null, string containedContentType = null, bool? allowOverwrite = null, Dictionary<string, object> customData = null);
        BaseModel DeleteAudio(string customizationId, string audioName, Dictionary<string, object> customData = null);
        AudioListing GetAudio(string customizationId, string audioName, Dictionary<string, object> customData = null);
        AudioResources ListAudio(string customizationId, Dictionary<string, object> customData = null);
        BaseModel DeleteUserData(string customerId, Dictionary<string, object> customData = null);
    }
}
