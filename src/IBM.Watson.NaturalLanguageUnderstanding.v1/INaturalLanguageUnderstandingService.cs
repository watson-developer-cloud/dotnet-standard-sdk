/**
* (C) Copyright IBM Corp. 2019, 2021.
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
using IBM.Watson.NaturalLanguageUnderstanding.v1.Model;

namespace IBM.Watson.NaturalLanguageUnderstanding.v1
{
    public partial interface INaturalLanguageUnderstandingService
    {
        DetailedResponse<AnalysisResults> Analyze(Features features, string text = null, string html = null, string url = null, bool? clean = null, string xpath = null, bool? fallbackToRaw = null, bool? returnAnalyzedText = null, string language = null, long? limitTextCharacters = null);
        DetailedResponse<ListModelsResults> ListModels();
        DetailedResponse<DeleteModelResults> DeleteModel(string modelId);
        DetailedResponse<SentimentModel> CreateSentimentModel(string language, System.IO.MemoryStream trainingData, string name = null, string description = null, string modelVersion = null, string workspaceId = null, string versionDescription = null);
        DetailedResponse<ListSentimentModelsResponse> ListSentimentModels();
        DetailedResponse<SentimentModel> GetSentimentModel(string modelId);
        DetailedResponse<SentimentModel> UpdateSentimentModel(string modelId, string language, System.IO.MemoryStream trainingData, string name = null, string description = null, string modelVersion = null, string workspaceId = null, string versionDescription = null);
        DetailedResponse<DeleteModelResults> DeleteSentimentModel(string modelId);
        DetailedResponse<CategoriesModel> CreateCategoriesModel(string language, System.IO.MemoryStream trainingData, string trainingDataContentType = null, string name = null, string description = null, string modelVersion = null, string workspaceId = null, string versionDescription = null);
        DetailedResponse<CategoriesModelList> ListCategoriesModels();
        DetailedResponse<CategoriesModel> GetCategoriesModel(string modelId);
        DetailedResponse<CategoriesModel> UpdateCategoriesModel(string modelId, string language, System.IO.MemoryStream trainingData, string trainingDataContentType = null, string name = null, string description = null, string modelVersion = null, string workspaceId = null, string versionDescription = null);
        DetailedResponse<DeleteModelResults> DeleteCategoriesModel(string modelId);
        DetailedResponse<ClassificationsModel> CreateClassificationsModel(string language, System.IO.MemoryStream trainingData, string trainingDataContentType = null, string name = null, string description = null, string modelVersion = null, string workspaceId = null, string versionDescription = null);
        DetailedResponse<ClassificationsModelList> ListClassificationsModels();
        DetailedResponse<ClassificationsModel> GetClassificationsModel(string modelId);
        DetailedResponse<ClassificationsModel> UpdateClassificationsModel(string modelId, string language, System.IO.MemoryStream trainingData, string trainingDataContentType = null, string name = null, string description = null, string modelVersion = null, string workspaceId = null, string versionDescription = null);
        DetailedResponse<DeleteModelResults> DeleteClassificationsModel(string modelId);
    }
}
