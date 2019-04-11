/**
* Copyright 2018, 2019 IBM Corp. All Rights Reserved.
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
using Newtonsoft.Json.Linq;
using IBM.Watson.SpeechToText.v1.Model;

namespace IBM.Watson.SpeechToText.v1
{
    public partial interface ISpeechToTextService
    {
        DetailedResponse<SpeechModel> GetModel(string modelId);
        DetailedResponse<SpeechModels> ListModels();
        DetailedResponse<SpeechRecognitionResults> Recognize(byte[] audio, string contentType = null, string model = null, string languageCustomizationId = null, string acousticCustomizationId = null, string baseModelVersion = null, double? customizationWeight = null, long? inactivityTimeout = null, List<string> keywords = null, float? keywordsThreshold = null, long? maxAlternatives = null, float? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool? profanityFilter = null, bool? smartFormatting = null, bool? speakerLabels = null, string customizationId = null, string grammarName = null, bool? redaction = null);
        DetailedResponse<RecognitionJob> CheckJob(string id);
        DetailedResponse<RecognitionJobs> CheckJobs();
        DetailedResponse<RecognitionJob> CreateJob(byte[] audio, string contentType = null, string model = null, string callbackUrl = null, string events = null, string userToken = null, long? resultsTtl = null, string languageCustomizationId = null, string acousticCustomizationId = null, string baseModelVersion = null, double? customizationWeight = null, long? inactivityTimeout = null, List<string> keywords = null, float? keywordsThreshold = null, long? maxAlternatives = null, float? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool? profanityFilter = null, bool? smartFormatting = null, bool? speakerLabels = null, string customizationId = null, string grammarName = null, bool? redaction = null);
        DetailedResponse<object> DeleteJob(string id);
        DetailedResponse<RegisterStatus> RegisterCallback(string callbackUrl, string userSecret = null);
        DetailedResponse<object> UnregisterCallback(string callbackUrl);
        DetailedResponse<LanguageModel> CreateLanguageModel(string name, string baseModelName, string dialect = null, string description = null);
        DetailedResponse<object> DeleteLanguageModel(string customizationId);
        DetailedResponse<LanguageModel> GetLanguageModel(string customizationId);
        DetailedResponse<LanguageModels> ListLanguageModels(string language = null);
        DetailedResponse<object> ResetLanguageModel(string customizationId);
        DetailedResponse<object> TrainLanguageModel(string customizationId, string wordTypeToAdd = null, double? customizationWeight = null);
        DetailedResponse<object> UpgradeLanguageModel(string customizationId);
        DetailedResponse<object> AddCorpus(string customizationId, string corpusName, System.IO.MemoryStream corpusFile, bool? allowOverwrite = null);
        DetailedResponse<object> DeleteCorpus(string customizationId, string corpusName);
        DetailedResponse<Corpus> GetCorpus(string customizationId, string corpusName);
        DetailedResponse<Corpora> ListCorpora(string customizationId);
        DetailedResponse<object> AddWord(string customizationId, string wordName, string word = null, List<string> soundsLike = null, string displayAs = null);
        DetailedResponse<object> AddWords(string customizationId, List<CustomWord> words);
        DetailedResponse<object> DeleteWord(string customizationId, string wordName);
        DetailedResponse<Word> GetWord(string customizationId, string wordName);
        DetailedResponse<Words> ListWords(string customizationId, string wordType = null, string sort = null);
        DetailedResponse<object> AddGrammar(string customizationId, string grammarName, string grammarFile, string contentType, bool? allowOverwrite = null);
        DetailedResponse<object> DeleteGrammar(string customizationId, string grammarName);
        DetailedResponse<Grammar> GetGrammar(string customizationId, string grammarName);
        DetailedResponse<Grammars> ListGrammars(string customizationId);
        DetailedResponse<AcousticModel> CreateAcousticModel(string name, string baseModelName, string description = null);
        DetailedResponse<object> DeleteAcousticModel(string customizationId);
        DetailedResponse<AcousticModel> GetAcousticModel(string customizationId);
        DetailedResponse<AcousticModels> ListAcousticModels(string language = null);
        DetailedResponse<object> ResetAcousticModel(string customizationId);
        DetailedResponse<object> TrainAcousticModel(string customizationId, string customLanguageModelId = null);
        DetailedResponse<object> UpgradeAcousticModel(string customizationId, string customLanguageModelId = null, bool? force = null);
        DetailedResponse<object> AddAudio(string customizationId, string audioName, byte[] audioResource, string contentType = null, string containedContentType = null, bool? allowOverwrite = null);
        DetailedResponse<object> DeleteAudio(string customizationId, string audioName);
        DetailedResponse<AudioListing> GetAudio(string customizationId, string audioName);
        DetailedResponse<AudioResources> ListAudio(string customizationId);
        DetailedResponse<object> DeleteUserData(string customerId);
    }
}
