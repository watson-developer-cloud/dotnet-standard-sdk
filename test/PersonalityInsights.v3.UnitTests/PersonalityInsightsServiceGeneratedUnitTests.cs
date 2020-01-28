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
using NSubstitute;
using System;
using Newtonsoft.Json;
using IBM.Watson.PersonalityInsights.v3.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;
using IBM.Cloud.SDK.Core.Authentication.NoAuth;
using System.Threading.Tasks;

namespace IBM.Watson.PersonalityInsights.v3.UnitTests
{
    [TestClass]
    public class PersonalityInsightsServiceUnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            PersonalityInsightsService service = new PersonalityInsightsService(httpClient: null);
        }

        [TestMethod]
        public void ConstructorHttpClient()
        {
            PersonalityInsightsService service = new PersonalityInsightsService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorExternalConfig()
        {
            var apikey = System.Environment.GetEnvironmentVariable("PERSONALITY_INSIGHTS_APIKEY");
            var url = System.Environment.GetEnvironmentVariable("PERSONALITY_INSIGHTS_URL");
            System.Environment.SetEnvironmentVariable("PERSONALITY_INSIGHTS_APIKEY", "apikey");
            System.Environment.SetEnvironmentVariable("PERSONALITY_INSIGHTS_URL", "http://www.url.com");
            PersonalityInsightsService service = Substitute.For<PersonalityInsightsService>("versionDate");
            Assert.IsNotNull(service);
            System.Environment.SetEnvironmentVariable("PERSONALITY_INSIGHTS_URL", url);
            System.Environment.SetEnvironmentVariable("PERSONALITY_INSIGHTS_APIKEY", apikey);
        }

        [TestMethod]
        public void Constructor()
        {
            PersonalityInsightsService service = new PersonalityInsightsService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorAuthenticator()
        {
            PersonalityInsightsService service = new PersonalityInsightsService("versionDate", new NoAuthAuthenticator());
            Assert.IsNotNull(service);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNoVersion()
        {
            PersonalityInsightsService service = new PersonalityInsightsService(null, new NoAuthAuthenticator());
        }

        [TestMethod]
        public void ConstructorNoUrl()
        {
            var apikey = System.Environment.GetEnvironmentVariable("PERSONALITY_INSIGHTS_APIKEY");
            var url = System.Environment.GetEnvironmentVariable("PERSONALITY_INSIGHTS_URL");
            System.Environment.SetEnvironmentVariable("PERSONALITY_INSIGHTS_APIKEY", "apikey");
            System.Environment.SetEnvironmentVariable("PERSONALITY_INSIGHTS_URL", null);
            PersonalityInsightsService service = Substitute.For<PersonalityInsightsService>("versionDate");
            Assert.IsTrue(service.ServiceUrl == "https://gateway.watsonplatform.net/personality-insights/api");
            System.Environment.SetEnvironmentVariable("PERSONALITY_INSIGHTS_URL", url);
            System.Environment.SetEnvironmentVariable("PERSONALITY_INSIGHTS_APIKEY", apikey);
        }
        #endregion

        [TestMethod]
        public void TestContentModel()
        {
            ContentItem ContentItemModel = new ContentItem()
            {
                Content = "testString",
                Id = "testString",
                Created = 26L,
                Updated = 26L,
                Contenttype = "text/plain",
                Language = "ar",
                Parentid = "testString",
                Reply = true,
                Forward = true
            };

            var ContentContentItems = new List<ContentItem> { ContentItemModel };
            Content testRequestModel = new Content()
            {
                ContentItems = ContentContentItems
            };

            Assert.IsTrue(testRequestModel.ContentItems == ContentContentItems);
        }

        [TestMethod]
        public void TestContentItemModel()
        {

            ContentItem testRequestModel = new ContentItem()
            {
                Content = "testString",
                Id = "testString",
                Created = 26L,
                Updated = 26L,
                Contenttype = "text/plain",
                Language = "ar",
                Parentid = "testString",
                Reply = true,
                Forward = true
            };

            Assert.IsTrue(testRequestModel.Content == "testString");
            Assert.IsTrue(testRequestModel.Id == "testString");
            Assert.IsTrue(testRequestModel.Created == 26L);
            Assert.IsTrue(testRequestModel.Updated == 26L);
            Assert.IsTrue(testRequestModel.Contenttype == "text/plain");
            Assert.IsTrue(testRequestModel.Language == "ar");
            Assert.IsTrue(testRequestModel.Parentid == "testString");
            Assert.IsTrue(testRequestModel.Reply == true);
            Assert.IsTrue(testRequestModel.Forward == true);
        }

        [TestMethod]
        public void TestTestProfileAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            PersonalityInsightsService service = new PersonalityInsightsService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'processed_language': 'ar', 'word_count': 9, 'word_count_message': 'WordCountMessage', 'personality': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': []}]}]}]}]}]}]}]}]}]}], 'needs': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': []}]}]}]}]}]}]}]}]}]}], 'values': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'personality', 'percentile': 10, 'raw_score': 8, 'significant': false, 'children': []}]}]}]}]}]}]}]}]}]}], 'behavior': [{'trait_id': 'TraitId', 'name': 'Name', 'category': 'Category', 'percentage': 10}], 'consumption_preferences': [{'consumption_preference_category_id': 'ConsumptionPreferenceCategoryId', 'name': 'Name', 'consumption_preferences': [{'consumption_preference_id': 'ConsumptionPreferenceId', 'name': 'Name', 'score': 5}]}], 'warnings': [{'warning_id': 'WORD_COUNT_MESSAGE', 'message': 'Message'}]}";
            var response = new DetailedResponse<Profile>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Profile>(responseJson),
                StatusCode = 200
            };

            ContentItem ContentItemModel = new ContentItem()
            {
                Content = "testString",
                Id = "testString",
                Created = 26L,
                Updated = 26L,
                Contenttype = "text/plain",
                Language = "ar",
                Parentid = "testString",
                Reply = true,
                Forward = true
            };

            var ContentContentItems = new List<ContentItem> { ContentItemModel };
            Content ContentModel = new Content()
            {
                ContentItems = ContentContentItems
            };
            Content content = ContentModel;
            string contentType = "application/json";
            string contentLanguage = "ar";
            string acceptLanguage = "ar";
            bool? rawScores = true;
            bool? csvHeaders = true;
            bool? consumptionPreferences = true;

            request.As<Profile>().Returns(Task.FromResult(response));

            var result = service.Profile(content: content, contentType: contentType, contentLanguage: contentLanguage, acceptLanguage: acceptLanguage, rawScores: rawScores, csvHeaders: csvHeaders, consumptionPreferences: consumptionPreferences);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v3/profile";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestProfileAsCsvAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            PersonalityInsightsService service = new PersonalityInsightsService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var response = new DetailedResponse<System.IO.MemoryStream>()
            {
                Result = new System.IO.MemoryStream(),
                StatusCode = 200
            };
            ContentItem ContentItemModel = new ContentItem()
            {
                Content = "testString",
                Id = "testString",
                Created = 26L,
                Updated = 26L,
                Contenttype = "text/plain",
                Language = "ar",
                Parentid = "testString",
                Reply = true,
                Forward = true
            };

            var ContentContentItems = new List<ContentItem> { ContentItemModel };
            Content ContentModel = new Content()
            {
                ContentItems = ContentContentItems
            };
            Content content = ContentModel;
            string contentType = "application/json";
            string contentLanguage = "ar";
            string acceptLanguage = "ar";
            bool? rawScores = true;
            bool? csvHeaders = true;
            bool? consumptionPreferences = true;

            request.As<System.IO.MemoryStream>().Returns(Task.FromResult(response));

            var result = service.ProfileAsCsv(content: content, contentType: contentType, contentLanguage: contentLanguage, acceptLanguage: acceptLanguage, rawScores: rawScores, csvHeaders: csvHeaders, consumptionPreferences: consumptionPreferences);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v3/profile";
            client.Received().PostAsync(messageUrl);
        }

    }
}
