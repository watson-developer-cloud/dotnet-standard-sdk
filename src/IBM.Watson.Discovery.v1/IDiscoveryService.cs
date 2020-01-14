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
using System;
using IBM.Watson.Discovery.v1.Model;
using Environment = IBM.Watson.Discovery.v1.Model.Environment;

namespace IBM.Watson.Discovery.v1
{
    public partial interface IDiscoveryService
    {
        DetailedResponse<Environment> CreateEnvironment(string name, string description = null, string size = null);
        DetailedResponse<ListEnvironmentsResponse> ListEnvironments(string name = null);
        DetailedResponse<Environment> GetEnvironment(string environmentId);
        DetailedResponse<Environment> UpdateEnvironment(string environmentId, string name = null, string description = null, string size = null);
        DetailedResponse<DeleteEnvironmentResponse> DeleteEnvironment(string environmentId);
        DetailedResponse<ListCollectionFieldsResponse> ListFields(string environmentId, List<string> collectionIds);
        DetailedResponse<Configuration> CreateConfiguration(string environmentId, string name, string description = null, Conversions conversions = null, List<Enrichment> enrichments = null, List<NormalizationOperation> normalizations = null, Source source = null);
        DetailedResponse<ListConfigurationsResponse> ListConfigurations(string environmentId, string name = null);
        DetailedResponse<Configuration> GetConfiguration(string environmentId, string configurationId);
        DetailedResponse<Configuration> UpdateConfiguration(string environmentId, string configurationId, string name, string description = null, Conversions conversions = null, List<Enrichment> enrichments = null, List<NormalizationOperation> normalizations = null, Source source = null);
        DetailedResponse<DeleteConfigurationResponse> DeleteConfiguration(string environmentId, string configurationId);
        DetailedResponse<Collection> CreateCollection(string environmentId, string name, string description = null, string configurationId = null, string language = null);
        DetailedResponse<ListCollectionsResponse> ListCollections(string environmentId, string name = null);
        DetailedResponse<Collection> GetCollection(string environmentId, string collectionId);
        DetailedResponse<Collection> UpdateCollection(string environmentId, string collectionId, string name, string description = null, string configurationId = null);
        DetailedResponse<DeleteCollectionResponse> DeleteCollection(string environmentId, string collectionId);
        DetailedResponse<ListCollectionFieldsResponse> ListCollectionFields(string environmentId, string collectionId);
        DetailedResponse<Expansions> ListExpansions(string environmentId, string collectionId);
        DetailedResponse<Expansions> CreateExpansions(string environmentId, string collectionId, List<Expansion> expansions);
        DetailedResponse<object> DeleteExpansions(string environmentId, string collectionId);
        DetailedResponse<TokenDictStatusResponse> GetTokenizationDictionaryStatus(string environmentId, string collectionId);
        DetailedResponse<TokenDictStatusResponse> CreateTokenizationDictionary(string environmentId, string collectionId, List<TokenDictRule> tokenizationRules = null);
        DetailedResponse<object> DeleteTokenizationDictionary(string environmentId, string collectionId);
        DetailedResponse<TokenDictStatusResponse> GetStopwordListStatus(string environmentId, string collectionId);
        DetailedResponse<TokenDictStatusResponse> CreateStopwordList(string environmentId, string collectionId, System.IO.MemoryStream stopwordFile, string stopwordFilename);
        DetailedResponse<object> DeleteStopwordList(string environmentId, string collectionId);
        DetailedResponse<DocumentAccepted> AddDocument(string environmentId, string collectionId, System.IO.MemoryStream file = null, string filename = null, string fileContentType = null, string metadata = null);
        DetailedResponse<DocumentStatus> GetDocumentStatus(string environmentId, string collectionId, string documentId);
        DetailedResponse<DocumentAccepted> UpdateDocument(string environmentId, string collectionId, string documentId, System.IO.MemoryStream file = null, string filename = null, string fileContentType = null, string metadata = null);
        DetailedResponse<DeleteDocumentResponse> DeleteDocument(string environmentId, string collectionId, string documentId);
        DetailedResponse<QueryResponse> Query(string environmentId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, string _return = null, long? offset = null, string sort = null, bool? highlight = null, string passagesFields = null, long? passagesCount = null, long? passagesCharacters = null, bool? deduplicate = null, string deduplicateField = null, bool? similar = null, string similarDocumentIds = null, string similarFields = null, string bias = null, bool? spellingSuggestions = null, bool? xWatsonLoggingOptOut = null);
        DetailedResponse<QueryNoticesResponse> QueryNotices(string environmentId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, List<string> _return = null, long? offset = null, List<string> sort = null, bool? highlight = null, List<string> passagesFields = null, long? passagesCount = null, long? passagesCharacters = null, string deduplicateField = null, bool? similar = null, List<string> similarDocumentIds = null, List<string> similarFields = null);
        DetailedResponse<QueryResponse> FederatedQuery(string environmentId, string collectionIds, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, string _return = null, long? offset = null, string sort = null, bool? highlight = null, string passagesFields = null, long? passagesCount = null, long? passagesCharacters = null, bool? deduplicate = null, string deduplicateField = null, bool? similar = null, string similarDocumentIds = null, string similarFields = null, string bias = null, bool? xWatsonLoggingOptOut = null);
        DetailedResponse<QueryNoticesResponse> FederatedQueryNotices(string environmentId, List<string> collectionIds, string filter = null, string query = null, string naturalLanguageQuery = null, string aggregation = null, long? count = null, List<string> _return = null, long? offset = null, List<string> sort = null, bool? highlight = null, string deduplicateField = null, bool? similar = null, List<string> similarDocumentIds = null, List<string> similarFields = null);
        DetailedResponse<Completions> GetAutocompletion(string environmentId, string collectionId, string prefix, string field = null, long? count = null);
        DetailedResponse<TrainingDataSet> ListTrainingData(string environmentId, string collectionId);
        DetailedResponse<TrainingQuery> AddTrainingData(string environmentId, string collectionId, string naturalLanguageQuery = null, string filter = null, List<TrainingExample> examples = null);
        DetailedResponse<object> DeleteAllTrainingData(string environmentId, string collectionId);
        DetailedResponse<TrainingQuery> GetTrainingData(string environmentId, string collectionId, string queryId);
        DetailedResponse<object> DeleteTrainingData(string environmentId, string collectionId, string queryId);
        DetailedResponse<TrainingExampleList> ListTrainingExamples(string environmentId, string collectionId, string queryId);
        DetailedResponse<TrainingExample> CreateTrainingExample(string environmentId, string collectionId, string queryId, string documentId = null, string crossReference = null, long? relevance = null);
        DetailedResponse<object> DeleteTrainingExample(string environmentId, string collectionId, string queryId, string exampleId);
        DetailedResponse<TrainingExample> UpdateTrainingExample(string environmentId, string collectionId, string queryId, string exampleId, string crossReference = null, long? relevance = null);
        DetailedResponse<TrainingExample> GetTrainingExample(string environmentId, string collectionId, string queryId, string exampleId);
        DetailedResponse<object> DeleteUserData(string customerId);
        DetailedResponse<CreateEventResponse> CreateEvent(string type, EventData data);
        DetailedResponse<LogQueryResponse> QueryLog(string filter = null, string query = null, long? count = null, long? offset = null, List<string> sort = null);
        DetailedResponse<MetricResponse> GetMetricsQuery(DateTime? startTime = null, DateTime? endTime = null, string resultType = null);
        DetailedResponse<MetricResponse> GetMetricsQueryEvent(DateTime? startTime = null, DateTime? endTime = null, string resultType = null);
        DetailedResponse<MetricResponse> GetMetricsQueryNoResults(DateTime? startTime = null, DateTime? endTime = null, string resultType = null);
        DetailedResponse<MetricResponse> GetMetricsEventRate(DateTime? startTime = null, DateTime? endTime = null, string resultType = null);
        DetailedResponse<MetricTokenResponse> GetMetricsQueryTokenEvent(long? count = null);
        DetailedResponse<CredentialsList> ListCredentials(string environmentId);
        DetailedResponse<Credentials> CreateCredentials(string environmentId, string sourceType = null, CredentialDetails credentialDetails = null, string status = null);
        DetailedResponse<Credentials> GetCredentials(string environmentId, string credentialId);
        DetailedResponse<Credentials> UpdateCredentials(string environmentId, string credentialId, string sourceType = null, CredentialDetails credentialDetails = null, string status = null);
        DetailedResponse<DeleteCredentials> DeleteCredentials(string environmentId, string credentialId);
        DetailedResponse<GatewayList> ListGateways(string environmentId);
        DetailedResponse<Gateway> CreateGateway(string environmentId, string name = null);
        DetailedResponse<Gateway> GetGateway(string environmentId, string gatewayId);
        DetailedResponse<GatewayDelete> DeleteGateway(string environmentId, string gatewayId);
    }
}
