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
using IBM.WatsonDeveloperCloud.Discovery.v1.Model;

namespace IBM.WatsonDeveloperCloud.Discovery.v1
{
    public interface IDiscoveryService
    {
        Environment CreateEnvironment(CreateEnvironmentRequest body);

        DeleteEnvironmentResponse DeleteEnvironment(string environmentId);

        Environment GetEnvironment(string environmentId);

        ListEnvironmentsResponse ListEnvironments(string name = null);

        ListCollectionFieldsResponse ListFields(string environmentId, List<string> collectionIds);

        Environment UpdateEnvironment(string environmentId, UpdateEnvironmentRequest body);
        Configuration CreateConfiguration(string environmentId, Configuration configuration);

        DeleteConfigurationResponse DeleteConfiguration(string environmentId, string configurationId);

        Configuration GetConfiguration(string environmentId, string configurationId);

        ListConfigurationsResponse ListConfigurations(string environmentId, string name = null);

        Configuration UpdateConfiguration(string environmentId, string configurationId, Configuration configuration);
        TestDocument TestConfigurationInEnvironment(string environmentId, string configuration = null, string step = null, string configurationId = null, System.IO.Stream file = null, string metadata = null, string fileContentType = null);
        Collection CreateCollection(string environmentId, CreateCollectionRequest body);

        Expansions CreateExpansions(string environmentId, string collectionId, Expansions body);

        DeleteCollectionResponse DeleteCollection(string environmentId, string collectionId);

        object DeleteExpansions(string environmentId, string collectionId);

        Collection GetCollection(string environmentId, string collectionId);

        ListCollectionFieldsResponse ListCollectionFields(string environmentId, string collectionId);

        ListCollectionsResponse ListCollections(string environmentId, string name = null);

        Expansions ListExpansions(string environmentId, string collectionId);

        Collection UpdateCollection(string environmentId, string collectionId, UpdateCollectionRequest body = null);
        DocumentAccepted AddDocument(string environmentId, string collectionId, System.IO.Stream file = null, string metadata = null, string fileContentType = null);

        DeleteDocumentResponse DeleteDocument(string environmentId, string collectionId, string documentId);

        DocumentStatus GetDocumentStatus(string environmentId, string collectionId, string documentId);

        DocumentAccepted UpdateDocument(string environmentId, string collectionId, string documentId, System.IO.Stream file = null, string metadata = null, string fileContentType = null);
        QueryResponse FederatedQuery(string environmentId, List<string> collectionIds, string filter = null, string query = null, string naturalLanguageQuery = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, bool? deduplicate = null, string deduplicateField = null, bool? similar = null, List<string> similarDocumentIds = null, List<string> similarFields = null);

        QueryNoticesResponse FederatedQueryNotices(string environmentId, List<string> collectionIds, string filter = null, string query = null, string naturalLanguageQuery = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, string deduplicateField = null, bool? similar = null, List<string> similarDocumentIds = null, List<string> similarFields = null);

        QueryResponse Query(string environmentId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, List<string> passagesFields = null, long? passagesCount = null, long? passagesCharacters = null, bool? deduplicate = null, string deduplicateField = null, bool? similar = null, List<string> similarDocumentIds = null, List<string> similarFields = null);

        QueryEntitiesResponse QueryEntities(string environmentId, string collectionId, QueryEntities entityQuery);

        QueryNoticesResponse QueryNotices(string environmentId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, List<string> passagesFields = null, long? passagesCount = null, long? passagesCharacters = null, string deduplicateField = null, bool? similar = null, List<string> similarDocumentIds = null, List<string> similarFields = null);

        QueryRelationsResponse QueryRelations(string environmentId, string collectionId, QueryRelations relationshipQuery);
        TrainingQuery AddTrainingData(string environmentId, string collectionId, NewTrainingQuery body);

        TrainingExample CreateTrainingExample(string environmentId, string collectionId, string queryId, TrainingExample body);

        object DeleteAllTrainingData(string environmentId, string collectionId);

        object DeleteTrainingData(string environmentId, string collectionId, string queryId);

        object DeleteTrainingExample(string environmentId, string collectionId, string queryId, string exampleId);

        TrainingQuery GetTrainingData(string environmentId, string collectionId, string queryId);

        TrainingExample GetTrainingExample(string environmentId, string collectionId, string queryId, string exampleId);

        TrainingDataSet ListTrainingData(string environmentId, string collectionId);

        TrainingExampleList ListTrainingExamples(string environmentId, string collectionId, string queryId);

        TrainingExample UpdateTrainingExample(string environmentId, string collectionId, string queryId, string exampleId, TrainingExamplePatch body);
    }
}
