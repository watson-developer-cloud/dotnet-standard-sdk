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
using System.Runtime.Serialization;
using IBM.WatsonDeveloperCloud.Discovery.v1.Model;

namespace IBM.WatsonDeveloperCloud.Discovery.v1
{
    public partial interface IDiscoveryService
    {
        Environment CreateEnvironment(CreateEnvironmentRequest body, Dictionary<string, object> customData = null);
        DeleteEnvironmentResponse DeleteEnvironment(string environmentId, Dictionary<string, object> customData = null);
        Environment GetEnvironment(string environmentId, Dictionary<string, object> customData = null);
        ListEnvironmentsResponse ListEnvironments(string name = null, Dictionary<string, object> customData = null);
        ListCollectionFieldsResponse ListFields(string environmentId, List<string> collectionIds, Dictionary<string, object> customData = null);
        Environment UpdateEnvironment(string environmentId, UpdateEnvironmentRequest body, Dictionary<string, object> customData = null);
        Configuration CreateConfiguration(string environmentId, Configuration configuration, Dictionary<string, object> customData = null);
        DeleteConfigurationResponse DeleteConfiguration(string environmentId, string configurationId, Dictionary<string, object> customData = null);
        Configuration GetConfiguration(string environmentId, string configurationId, Dictionary<string, object> customData = null);
        ListConfigurationsResponse ListConfigurations(string environmentId, string name = null, Dictionary<string, object> customData = null);
        Configuration UpdateConfiguration(string environmentId, string configurationId, Configuration configuration, Dictionary<string, object> customData = null);
        TestDocument TestConfigurationInEnvironment(string environmentId, string configuration = null, string step = null, string configurationId = null, System.IO.FileStream file = null, string metadata = null, string fileContentType = null, Dictionary<string, object> customData = null);
        Collection CreateCollection(string environmentId, CreateCollectionRequest body, Dictionary<string, object> customData = null);
        DeleteCollectionResponse DeleteCollection(string environmentId, string collectionId, Dictionary<string, object> customData = null);
        Collection GetCollection(string environmentId, string collectionId, Dictionary<string, object> customData = null);
        ListCollectionFieldsResponse ListCollectionFields(string environmentId, string collectionId, Dictionary<string, object> customData = null);
        ListCollectionsResponse ListCollections(string environmentId, string name = null, Dictionary<string, object> customData = null);
        Collection UpdateCollection(string environmentId, string collectionId, UpdateCollectionRequest body = null, Dictionary<string, object> customData = null);
        Expansions CreateExpansions(string environmentId, string collectionId, Expansions body, Dictionary<string, object> customData = null);
        BaseModel DeleteExpansions(string environmentId, string collectionId, Dictionary<string, object> customData = null);
        Expansions ListExpansions(string environmentId, string collectionId, Dictionary<string, object> customData = null);
        DocumentAccepted AddDocument(string environmentId, string collectionId, System.IO.FileStream file = null, string metadata = null, string fileContentType = null, Dictionary<string, object> customData = null);
        DeleteDocumentResponse DeleteDocument(string environmentId, string collectionId, string documentId, Dictionary<string, object> customData = null);
        DocumentStatus GetDocumentStatus(string environmentId, string collectionId, string documentId, Dictionary<string, object> customData = null);
        DocumentAccepted UpdateDocument(string environmentId, string collectionId, string documentId, System.IO.FileStream file = null, string metadata = null, string fileContentType = null, Dictionary<string, object> customData = null);
        QueryResponse FederatedQuery(string environmentId, QueryLarge queryLong = null, bool? loggingOptOut = null, Dictionary<string, object> customData = null);
        QueryNoticesResponse FederatedQueryNotices(string environmentId, List<string> collectionIds, string filter = null, string query = null, string naturalLanguageQuery = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, string deduplicateField = null, bool? similar = null, List<string> similarDocumentIds = null, List<string> similarFields = null, Dictionary<string, object> customData = null);
        QueryResponse Query(string environmentId, string collectionId, QueryLarge queryLong = null, bool? loggingOptOut = null, Dictionary<string, object> customData = null);
        QueryEntitiesResponse QueryEntities(string environmentId, string collectionId, QueryEntities entityQuery, Dictionary<string, object> customData = null);
        QueryNoticesResponse QueryNotices(string environmentId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, List<string> passagesFields = null, long? passagesCount = null, long? passagesCharacters = null, string deduplicateField = null, bool? similar = null, List<string> similarDocumentIds = null, List<string> similarFields = null, Dictionary<string, object> customData = null);
        QueryRelationsResponse QueryRelations(string environmentId, string collectionId, QueryRelations relationshipQuery, Dictionary<string, object> customData = null);
        TrainingQuery AddTrainingData(string environmentId, string collectionId, NewTrainingQuery body, Dictionary<string, object> customData = null);
        TrainingExample CreateTrainingExample(string environmentId, string collectionId, string queryId, TrainingExample body, Dictionary<string, object> customData = null);
        BaseModel DeleteAllTrainingData(string environmentId, string collectionId, Dictionary<string, object> customData = null);
        BaseModel DeleteTrainingData(string environmentId, string collectionId, string queryId, Dictionary<string, object> customData = null);
        BaseModel DeleteTrainingExample(string environmentId, string collectionId, string queryId, string exampleId, Dictionary<string, object> customData = null);
        TrainingQuery GetTrainingData(string environmentId, string collectionId, string queryId, Dictionary<string, object> customData = null);
        TrainingExample GetTrainingExample(string environmentId, string collectionId, string queryId, string exampleId, Dictionary<string, object> customData = null);
        TrainingDataSet ListTrainingData(string environmentId, string collectionId, Dictionary<string, object> customData = null);
        TrainingExampleList ListTrainingExamples(string environmentId, string collectionId, string queryId, Dictionary<string, object> customData = null);
        TrainingExample UpdateTrainingExample(string environmentId, string collectionId, string queryId, string exampleId, TrainingExamplePatch body, Dictionary<string, object> customData = null);
        BaseModel DeleteUserData(string customerId, Dictionary<string, object> customData = null);
        CreateEventResponse CreateEvent(CreateEventObject queryEvent, Dictionary<string, object> customData = null);
        MetricResponse GetMetricsEventRate(DateTime startTime = null, DateTime endTime = null, string resultType = null, Dictionary<string, object> customData = null);
        MetricResponse GetMetricsQuery(DateTime startTime = null, DateTime endTime = null, string resultType = null, Dictionary<string, object> customData = null);
        MetricResponse GetMetricsQueryEvent(DateTime startTime = null, DateTime endTime = null, string resultType = null, Dictionary<string, object> customData = null);
        MetricResponse GetMetricsQueryNoResults(DateTime startTime = null, DateTime endTime = null, string resultType = null, Dictionary<string, object> customData = null);
        MetricTokenResponse GetMetricsQueryTokenEvent(long? count = null, Dictionary<string, object> customData = null);
        LogQueryResponse QueryLog(string filter = null, string query = null, long? count = null, long? offset = null, List<string> sort = null, Dictionary<string, object> customData = null);
        Credentials CreateCredentials(string environmentId, Credentials credentialsParameter, Dictionary<string, object> customData = null);
        DeleteCredentials DeleteCredentials(string environmentId, string credentialId, Dictionary<string, object> customData = null);
        Credentials GetCredentials(string environmentId, string credentialId, Dictionary<string, object> customData = null);
        CredentialsList ListCredentials(string environmentId, Dictionary<string, object> customData = null);
        Credentials UpdateCredentials(string environmentId, string credentialId, Credentials credentialsParameter, Dictionary<string, object> customData = null);
    }
}
