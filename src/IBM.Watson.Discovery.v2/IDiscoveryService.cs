/**
* (C) Copyright IBM Corp. 2018, 2020.
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
        DetailedResponse<ListCollectionsResponse> ListCollections(string projectId);
        DetailedResponse<CollectionDetails> CreateCollection(string projectId, string name = null, string description = null, string language = null, List<CollectionEnrichment> enrichments = null);
        DetailedResponse<CollectionDetails> GetCollection(string projectId, string collectionId);
        DetailedResponse<CollectionDetails> UpdateCollection(string projectId, string collectionId, string name = null, string description = null, string language = null, List<CollectionEnrichment> enrichments = null);
        DetailedResponse<object> DeleteCollection(string projectId, string collectionId);
        DetailedResponse<QueryResponse> Query(string projectId, List<string> collectionIds = null, string filter = null, string query = null, string naturalLanguageQuery = null, string aggregation = null, long? count = null, List<string> _return = null, long? offset = null, string sort = null, bool? highlight = null, bool? spellingSuggestions = null, QueryLargeTableResults tableResults = null, QueryLargeSuggestedRefinements suggestedRefinements = null, QueryLargePassages passages = null);
        DetailedResponse<Completions> GetAutocompletion(string projectId, string prefix, List<string> collectionIds = null, string field = null, long? count = null);
        DetailedResponse<QueryNoticesResponse> QueryNotices(string projectId, string filter = null, string query = null, string naturalLanguageQuery = null, long? count = null, long? offset = null);
        DetailedResponse<ListFieldsResponse> ListFields(string projectId, List<string> collectionIds = null);
        DetailedResponse<ComponentSettingsResponse> GetComponentSettings(string projectId);
        DetailedResponse<ComponentSettingsResponse> UpdateComponentSettings(string projectId, ComponentSettingsFieldsShown fieldsShown = null, bool? autocomplete = null, bool? structuredSearch = null, long? resultsPerPage = null, List<ComponentSettingsAggregation> aggregations = null);
        DetailedResponse<DocumentAccepted> AddDocument(string projectId, string collectionId, System.IO.MemoryStream file = null, string filename = null, string fileContentType = null, string metadata = null, bool? xWatsonDiscoveryForce = null);
        DetailedResponse<DocumentAccepted> UpdateDocument(string projectId, string collectionId, string documentId, System.IO.MemoryStream file = null, string filename = null, string fileContentType = null, string metadata = null, bool? xWatsonDiscoveryForce = null);
        DetailedResponse<DeleteDocumentResponse> DeleteDocument(string projectId, string collectionId, string documentId, bool? xWatsonDiscoveryForce = null);
        DetailedResponse<TrainingQuerySet> ListTrainingQueries(string projectId);
        DetailedResponse<object> DeleteTrainingQueries(string projectId);
        DetailedResponse<TrainingQuery> CreateTrainingQuery(string projectId, string naturalLanguageQuery, List<TrainingExample> examples, string filter = null);
        DetailedResponse<TrainingQuery> GetTrainingQuery(string projectId, string queryId);
        DetailedResponse<TrainingQuery> UpdateTrainingQuery(string projectId, string queryId, string naturalLanguageQuery, List<TrainingExample> examples, string filter = null);
        DetailedResponse<AnalyzedDocument> AnalyzeDocument(string projectId, string collectionId, System.IO.MemoryStream file = null, string filename = null, string fileContentType = null, string metadata = null);
        DetailedResponse<Enrichments> ListEnrichments(string projectId);
        DetailedResponse<Enrichment> CreateEnrichment(string projectId, System.IO.MemoryStream file = null, Enrichment enrichment = null);
        DetailedResponse<Enrichment> GetEnrichment(string projectId, string enrichmentId);
        DetailedResponse<Enrichment> UpdateEnrichment(string projectId, string enrichmentId, string name = null, string description = null);
        DetailedResponse<object> DeleteEnrichment(string projectId, string enrichmentId);
        DetailedResponse<ListProjectsResponse> ListProjects();
        DetailedResponse<ProjectDetails> CreateProject(string name = null, string type = null, ProjectRelTrainStatus relevancyTrainingStatus = null, DefaultQueryParams defaultQueryParameters = null);
        DetailedResponse<ProjectDetails> GetProject(string projectId);
        DetailedResponse<ProjectDetails> UpdateProject(string projectId, string name = null);
        DetailedResponse<object> DeleteProject(string projectId);
        DetailedResponse<object> DeleteUserData(string customerId);
    }
}
