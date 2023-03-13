/**
* (C) Copyright IBM Corp. 2023.
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
using IBM.Watson.Discovery.v2.Model;

namespace IBM.Watson.Discovery.v2
{
    public partial interface IDiscoveryService
    {
        DetailedResponse<ListProjectsResponse> ListProjects();
        DetailedResponse<ProjectDetails> CreateProject(string name, string type, DefaultQueryParams defaultQueryParameters = null);
        DetailedResponse<ProjectDetails> GetProject(string projectId);
        DetailedResponse<ProjectDetails> UpdateProject(string projectId, string name = null);
        DetailedResponse<object> DeleteProject(string projectId);
        DetailedResponse<ListFieldsResponse> ListFields(string projectId, List<string> collectionIds = null);
        DetailedResponse<ListCollectionsResponse> ListCollections(string projectId);
        DetailedResponse<CollectionDetails> CreateCollection(string projectId, string name, string description = null, string language = null, List<CollectionEnrichment> enrichments = null);
        DetailedResponse<CollectionDetails> GetCollection(string projectId, string collectionId);
        DetailedResponse<CollectionDetails> UpdateCollection(string projectId, string collectionId, string name = null, string description = null, List<CollectionEnrichment> enrichments = null);
        DetailedResponse<object> DeleteCollection(string projectId, string collectionId);
        DetailedResponse<ListDocumentsResponse> ListDocuments(string projectId, string collectionId, long? count = null, string status = null, bool? hasNotices = null, bool? isParent = null, string parentDocumentId = null, string sha256 = null);
        DetailedResponse<DocumentAccepted> AddDocument(string projectId, string collectionId, System.IO.MemoryStream file = null, string filename = null, string fileContentType = null, string metadata = null, bool? xWatsonDiscoveryForce = null);
        DetailedResponse<DocumentDetails> GetDocument(string projectId, string collectionId, string documentId);
        DetailedResponse<DocumentAccepted> UpdateDocument(string projectId, string collectionId, string documentId, System.IO.MemoryStream file = null, string filename = null, string fileContentType = null, string metadata = null, bool? xWatsonDiscoveryForce = null);
        DetailedResponse<DeleteDocumentResponse> DeleteDocument(string projectId, string collectionId, string documentId, bool? xWatsonDiscoveryForce = null);
        DetailedResponse<QueryResponse> Query(string projectId, List<string> collectionIds = null, string filter = null, string query = null, string naturalLanguageQuery = null, string aggregation = null, long? count = null, List<string> _return = null, long? offset = null, string sort = null, bool? highlight = null, bool? spellingSuggestions = null, QueryLargeTableResults tableResults = null, QueryLargeSuggestedRefinements suggestedRefinements = null, QueryLargePassages passages = null, QueryLargeSimilar similar = null);
        DetailedResponse<Completions> GetAutocompletion(string projectId, string prefix, List<string> collectionIds = null, string field = null, long? count = null);
        DetailedResponse<QueryNoticesResponse> QueryCollectionNotices(string projectId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, long? count = null, long? offset = null);
        DetailedResponse<QueryNoticesResponse> QueryNotices(string projectId, string filter = null, string query = null, string naturalLanguageQuery = null, long? count = null, long? offset = null);
        DetailedResponse<StopWordList> GetStopwordList(string projectId, string collectionId);
        DetailedResponse<StopWordList> CreateStopwordList(string projectId, string collectionId, List<string> stopwords = null);
        DetailedResponse<object> DeleteStopwordList(string projectId, string collectionId);
        DetailedResponse<Expansions> ListExpansions(string projectId, string collectionId);
        DetailedResponse<Expansions> CreateExpansions(string projectId, string collectionId, List<Expansion> expansions);
        DetailedResponse<object> DeleteExpansions(string projectId, string collectionId);
        DetailedResponse<ComponentSettingsResponse> GetComponentSettings(string projectId);
        DetailedResponse<TrainingQuerySet> ListTrainingQueries(string projectId);
        DetailedResponse<object> DeleteTrainingQueries(string projectId);
        DetailedResponse<TrainingQuery> CreateTrainingQuery(string projectId, string naturalLanguageQuery, List<TrainingExample> examples, string filter = null);
        DetailedResponse<TrainingQuery> GetTrainingQuery(string projectId, string queryId);
        DetailedResponse<TrainingQuery> UpdateTrainingQuery(string projectId, string queryId, string naturalLanguageQuery, List<TrainingExample> examples, string filter = null);
        DetailedResponse<object> DeleteTrainingQuery(string projectId, string queryId);
        DetailedResponse<Enrichments> ListEnrichments(string projectId);
        DetailedResponse<Enrichment> CreateEnrichment(string projectId, CreateEnrichment enrichment, System.IO.MemoryStream file = null);
        DetailedResponse<Enrichment> GetEnrichment(string projectId, string enrichmentId);
        DetailedResponse<Enrichment> UpdateEnrichment(string projectId, string enrichmentId, string name, string description = null);
        DetailedResponse<object> DeleteEnrichment(string projectId, string enrichmentId);
        DetailedResponse<DocumentClassifiers> ListDocumentClassifiers(string projectId);
        DetailedResponse<DocumentClassifier> CreateDocumentClassifier(string projectId, System.IO.MemoryStream trainingData, CreateDocumentClassifier classifier, System.IO.MemoryStream testData = null);
        DetailedResponse<DocumentClassifier> GetDocumentClassifier(string projectId, string classifierId);
        DetailedResponse<DocumentClassifier> UpdateDocumentClassifier(string projectId, string classifierId, UpdateDocumentClassifier classifier, System.IO.MemoryStream trainingData = null, System.IO.MemoryStream testData = null);
        DetailedResponse<object> DeleteDocumentClassifier(string projectId, string classifierId);
        DetailedResponse<DocumentClassifierModels> ListDocumentClassifierModels(string projectId, string classifierId);
        DetailedResponse<DocumentClassifierModel> CreateDocumentClassifierModel(string projectId, string classifierId, string name, string description = null, double? learningRate = null, List<double?> l1RegularizationStrengths = null, List<double?> l2RegularizationStrengths = null, long? trainingMaxSteps = null, double? improvementRatio = null);
        DetailedResponse<DocumentClassifierModel> GetDocumentClassifierModel(string projectId, string classifierId, string modelId);
        DetailedResponse<DocumentClassifierModel> UpdateDocumentClassifierModel(string projectId, string classifierId, string modelId, string name = null, string description = null);
        DetailedResponse<object> DeleteDocumentClassifierModel(string projectId, string classifierId, string modelId);
        DetailedResponse<AnalyzedDocument> AnalyzeDocument(string projectId, string collectionId, System.IO.MemoryStream file = null, string filename = null, string fileContentType = null, string metadata = null);
        DetailedResponse<object> DeleteUserData(string customerId);
    }
}
