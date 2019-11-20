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

using IBM.Watson.Discovery.v2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace IBM.Watson.Discovery.v2.IntegrationTests
{
    [TestClass]
    public class DiscoveryIntegrationTests
    {
        private DiscoveryService service;
        private string versionDate = "2019-11-20";
        private string projectId = "";
        private string collectionId;
        private string queryId;

        [TestInitialize]
        public void Setup()
        {
            service = new DiscoveryService(versionDate);
        }

        #region Collections
        [TestMethod]
        public void TestListCollections()
        {
            service.WithHeader("X-Watson-Test", "1");
            var listCollectionResult = service.ListCollections(
                projectId: projectId
                );

            Assert.IsNotNull(listCollectionResult.Result.Collections);
            Assert.IsTrue(listCollectionResult.Result.Collections.Count > 0);
            Assert.IsNotNull(listCollectionResult.Result.Collections[0].CollectionId);
            Assert.IsNotNull(listCollectionResult.Result.Collections[0].Name);
        }
        #endregion

        #region Queries
        [TestMethod]
        public void TestQuery()
        {
            service.WithHeader("X-Watson-Test", "1");
            string filter = "";
            string query = "text:IBM";
            string naturalLanguageQuery = "";
            string aggregation = "";
            long count = 5;
            List<string> _return = new List<string>() { "" };
            long offset = 1;
            string sort = "";
            bool highlight = true;
            bool spellingSuggestions = true;
            QueryLargeTableResults tableResults = new QueryLargeTableResults()
            {
                Enabled = true,
                Count = 3
            };
            QueryLargeSuggestedRefinements suggestedRefinements = new QueryLargeSuggestedRefinements()
            {
                Enabled = true,
                Count = 3
            };
            QueryLargePassages passages = new QueryLargePassages()
            {
                Enabled = true,
                PerDocument = true,
                MaxPerDocument = 3,
                Fields = new List<string>()
                {
                    ""
                },
                Count = 3,
                Characters = 100
            };
            var queryResult = service.Query(
                projectId: projectId,
                collectionIds: new List<string>() { collectionId },
                filter: filter,
                query: query,
                naturalLanguageQuery: naturalLanguageQuery,
                aggregation: aggregation,
                count: count,
                _return: _return,
                offset: offset,
                sort: sort,
                highlight: highlight,
                spellingSuggestions: spellingSuggestions,
                tableResults: tableResults,
                suggestedRefinements: suggestedRefinements,
                passages: passages
                );

            Assert.IsNotNull(queryResult.Result);
        }

        [TestMethod]
        public void TestGetAutocompletion()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestQueryNotices()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestListFields()
        {
            Assert.Fail();
        }
        #endregion

        #region Component Settings
        [TestMethod]
        public void TestGetComponentSettings()
        {
            Assert.Fail();
        }
        #endregion

        #region Documents
        [TestMethod]
        public void TestAddDocument()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestDeleteDocument()
        {
            Assert.Fail();
        }
        #endregion

        #region Training Data
        [TestMethod]
        public void TestListTrainingQueries()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestDeleteTrainingQueries()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestCreateTrainingQuery()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestGetTrainingQuery()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestUpdateTrainingQuery()
        {
            Assert.Fail();
        }
        #endregion
    }
}
