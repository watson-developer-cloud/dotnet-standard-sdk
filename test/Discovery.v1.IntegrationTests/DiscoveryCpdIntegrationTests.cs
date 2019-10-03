/**
* Copyright 2019 IBM Corp. All Rights Reserved.
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

using IBM.Cloud.SDK.Core.Authentication.Bearer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IBM.Watson.Discovery.v1.IntegrationTests
{
    [TestClass]
    public class DiscoveryCpdIntegrationTests
    {
        private DiscoveryService service;
        private string environmentId;
        private string collectionId;
        private string bearerToken;
        private string serviceUrl;

        [TestInitialize]
        public void Setup()
        {
            BearerTokenAuthenticator authenticator = new BearerTokenAuthenticator(
                bearerToken: bearerToken
                );
            service = new DiscoveryService("2019-10-03", authenticator);
            service.SetServiceUrl(serviceUrl);
            service.DisableSslVerification(true);

            var listEnvironmentsResult = service.ListEnvironments();

            environmentId = listEnvironmentsResult.Result.Environments[0].EnvironmentId;

            var listCollectionsResult = service.ListCollections(environmentId: environmentId);

            collectionId = listCollectionsResult.Result.Collections[0].CollectionId;
        }

        [TestMethod]
        public void TestAutocompletion()
        {
            var autoCompletionResult = service.GetAutocompletion(environmentId: environmentId, collectionId: collectionId, prefix: "ho");

            Assert.IsNotNull(autoCompletionResult.Result);
            Assert.IsNotNull(autoCompletionResult.Result._Completions);
            Assert.IsNotNull(autoCompletionResult.Result._Completions[0]);
        }

        [TestMethod]
        public void TestSpellingSuggestions()
        {
            var queryResult = service.Query(environmentId: environmentId, collectionId: collectionId, naturalLanguageQuery: "cluod", spellingSuggestions: true);

            Assert.IsNotNull(queryResult.Result);
            Assert.IsNotNull(queryResult.Result.SuggestedQuery);
        }
    }
}
