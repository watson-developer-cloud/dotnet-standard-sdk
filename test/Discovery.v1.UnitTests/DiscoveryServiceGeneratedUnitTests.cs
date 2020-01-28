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
using Environment = IBM.Watson.Discovery.v1.Model.Environment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;
using IBM.Watson.Discovery.v1.Model;
using IBM.Cloud.SDK.Core.Authentication.NoAuth;
using System.Threading.Tasks;

namespace IBM.Watson.Discovery.v1.UnitTests
{
    [TestClass]
    public class DiscoveryServiceUnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            DiscoveryService service = new DiscoveryService(httpClient: null);
        }

        [TestMethod]
        public void ConstructorHttpClient()
        {
            DiscoveryService service = new DiscoveryService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorExternalConfig()
        {
            var apikey = System.Environment.GetEnvironmentVariable("DISCOVERY_APIKEY");
            var url = System.Environment.GetEnvironmentVariable("DISCOVERY_URL");
            System.Environment.SetEnvironmentVariable("DISCOVERY_APIKEY", "apikey");
            System.Environment.SetEnvironmentVariable("DISCOVERY_URL", "http://www.url.com");
            DiscoveryService service = Substitute.For<DiscoveryService>("versionDate");
            Assert.IsNotNull(service);
            System.Environment.SetEnvironmentVariable("DISCOVERY_URL", url);
            System.Environment.SetEnvironmentVariable("DISCOVERY_APIKEY", apikey);
        }

        [TestMethod]
        public void Constructor()
        {
            DiscoveryService service = new DiscoveryService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorAuthenticator()
        {
            DiscoveryService service = new DiscoveryService("versionDate", new NoAuthAuthenticator());
            Assert.IsNotNull(service);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNoVersion()
        {
            DiscoveryService service = new DiscoveryService(null, new NoAuthAuthenticator());
        }

        [TestMethod]
        public void ConstructorNoUrl()
        {
            var apikey = System.Environment.GetEnvironmentVariable("DISCOVERY_APIKEY");
            var url = System.Environment.GetEnvironmentVariable("DISCOVERY_URL");
            System.Environment.SetEnvironmentVariable("DISCOVERY_APIKEY", "apikey");
            System.Environment.SetEnvironmentVariable("DISCOVERY_URL", null);
            DiscoveryService service = Substitute.For<DiscoveryService>("versionDate");
            Assert.IsTrue(service.ServiceUrl == "https://gateway.watsonplatform.net/discovery/api");
            System.Environment.SetEnvironmentVariable("DISCOVERY_URL", url);
            System.Environment.SetEnvironmentVariable("DISCOVERY_APIKEY", apikey);
        }
        #endregion

        [TestMethod]
        public void TestConfigurationModel()
        {
            SourceOptionsBuckets SourceOptionsBucketsModel = new SourceOptionsBuckets()
            {
                Name = "testString",
                Limit = 38
            };

            var SourceOptionsWebCrawlBlacklist = new List<string> { "testString" };
            SourceOptionsWebCrawl SourceOptionsWebCrawlModel = new SourceOptionsWebCrawl()
            {
                Url = "testString",
                LimitToStartingHosts = true,
                CrawlSpeed = "gentle",
                AllowUntrustedCertificate = true,
                MaximumHops = 38,
                RequestTimeout = 38,
                OverrideRobotsTxt = true,
                Blacklist = SourceOptionsWebCrawlBlacklist
            };

            SourceOptionsSiteColl SourceOptionsSiteCollModel = new SourceOptionsSiteColl()
            {
                SiteCollectionPath = "testString",
                Limit = 38
            };

            SourceOptionsObject SourceOptionsObjectModel = new SourceOptionsObject()
            {
                Name = "testString",
                Limit = 38
            };

            SourceOptionsFolder SourceOptionsFolderModel = new SourceOptionsFolder()
            {
                OwnerUserId = "testString",
                FolderId = "testString",
                Limit = 38
            };

            var SourceOptionsFolders = new List<SourceOptionsFolder> { SourceOptionsFolderModel };
            var SourceOptionsObjects = new List<SourceOptionsObject> { SourceOptionsObjectModel };
            var SourceOptionsSiteCollections = new List<SourceOptionsSiteColl> { SourceOptionsSiteCollModel };
            var SourceOptionsUrls = new List<SourceOptionsWebCrawl> { SourceOptionsWebCrawlModel };
            var SourceOptionsBuckets = new List<SourceOptionsBuckets> { SourceOptionsBucketsModel };
            SourceOptions SourceOptionsModel = new SourceOptions()
            {
                Folders = SourceOptionsFolders,
                Objects = SourceOptionsObjects,
                SiteCollections = SourceOptionsSiteCollections,
                Urls = SourceOptionsUrls,
                Buckets = SourceOptionsBuckets,
                CrawlAllBuckets = true
            };

            SourceSchedule SourceScheduleModel = new SourceSchedule()
            {
                Enabled = true,
                TimeZone = "testString",
                Frequency = "daily"
            };

            Source SourceModel = new Source()
            {
                Type = "box",
                CredentialId = "testString",
                Schedule = SourceScheduleModel,
                Options = SourceOptionsModel
            };

            NormalizationOperation NormalizationOperationModel = new NormalizationOperation()
            {
                Operation = "copy",
                SourceField = "testString",
                DestinationField = "testString"
            };

            NluEnrichmentConcepts NluEnrichmentConceptsModel = new NluEnrichmentConcepts()
            {
                Limit = 38
            };

            NluEnrichmentRelations NluEnrichmentRelationsModel = new NluEnrichmentRelations()
            {
                Model = "testString"
            };

            NluEnrichmentSemanticRoles NluEnrichmentSemanticRolesModel = new NluEnrichmentSemanticRoles()
            {
                Entities = true,
                Keywords = true,
                Limit = 38
            };

            NluEnrichmentCategories NluEnrichmentCategoriesModel = new NluEnrichmentCategories()
            {
            };

            var NluEnrichmentEmotionTargets = new List<string> { "testString" };
            NluEnrichmentEmotion NluEnrichmentEmotionModel = new NluEnrichmentEmotion()
            {
                Document = true,
                Targets = NluEnrichmentEmotionTargets
            };

            var NluEnrichmentSentimentTargets = new List<string> { "testString" };
            NluEnrichmentSentiment NluEnrichmentSentimentModel = new NluEnrichmentSentiment()
            {
                Document = true,
                Targets = NluEnrichmentSentimentTargets
            };

            NluEnrichmentEntities NluEnrichmentEntitiesModel = new NluEnrichmentEntities()
            {
                Sentiment = true,
                Emotion = true,
                Limit = 38,
                Mentions = true,
                MentionTypes = true,
                SentenceLocations = true,
                Model = "testString"
            };

            NluEnrichmentKeywords NluEnrichmentKeywordsModel = new NluEnrichmentKeywords()
            {
                Sentiment = true,
                Emotion = true,
                Limit = 38
            };

            NluEnrichmentFeatures NluEnrichmentFeaturesModel = new NluEnrichmentFeatures()
            {
                Keywords = NluEnrichmentKeywordsModel,
                Entities = NluEnrichmentEntitiesModel,
                Sentiment = NluEnrichmentSentimentModel,
                Emotion = NluEnrichmentEmotionModel,
                Categories = NluEnrichmentCategoriesModel,
                SemanticRoles = NluEnrichmentSemanticRolesModel,
                Relations = NluEnrichmentRelationsModel,
                Concepts = NluEnrichmentConceptsModel
            };

            EnrichmentOptions EnrichmentOptionsModel = new EnrichmentOptions()
            {
                Features = NluEnrichmentFeaturesModel,
                Language = "ar",
                Model = "testString"
            };

            Enrichment EnrichmentModel = new Enrichment()
            {
                Description = "testString",
                DestinationField = "testString",
                SourceField = "testString",
                Overwrite = true,
                _Enrichment = "testString",
                IgnoreDownstreamErrors = true,
                Options = EnrichmentOptionsModel
            };

            var SegmentSettingsSelectorTags = new List<string> { "testString" };
            var SegmentSettingsAnnotatedFields = new List<string> { "testString" };
            SegmentSettings SegmentSettingsModel = new SegmentSettings()
            {
                Enabled = true,
                SelectorTags = SegmentSettingsSelectorTags,
                AnnotatedFields = SegmentSettingsAnnotatedFields
            };

            var XPathPatternsXpaths = new List<string> { "testString" };
            XPathPatterns XPathPatternsModel = new XPathPatterns()
            {
                Xpaths = XPathPatternsXpaths
            };

            var HtmlSettingsExcludeTagsCompletely = new List<string> { "testString" };
            var HtmlSettingsExcludeTagsKeepContent = new List<string> { "testString" };
            var HtmlSettingsKeepTagAttributes = new List<string> { "testString" };
            var HtmlSettingsExcludeTagAttributes = new List<string> { "testString" };
            HtmlSettings HtmlSettingsModel = new HtmlSettings()
            {
                ExcludeTagsCompletely = HtmlSettingsExcludeTagsCompletely,
                ExcludeTagsKeepContent = HtmlSettingsExcludeTagsKeepContent,
                KeepContent = XPathPatternsModel,
                ExcludeContent = XPathPatternsModel,
                KeepTagAttributes = HtmlSettingsKeepTagAttributes,
                ExcludeTagAttributes = HtmlSettingsExcludeTagAttributes
            };

            var WordStyleNames = new List<string> { "testString" };
            WordStyle WordStyleModel = new WordStyle()
            {
                Level = 38,
                Names = WordStyleNames
            };

            FontSetting FontSettingModel = new FontSetting()
            {
                Level = 38,
                MinSize = 38,
                MaxSize = 38,
                Bold = true,
                Italic = true,
                Name = "testString"
            };

            var WordHeadingDetectionFonts = new List<FontSetting> { FontSettingModel };
            var WordHeadingDetectionStyles = new List<WordStyle> { WordStyleModel };
            WordHeadingDetection WordHeadingDetectionModel = new WordHeadingDetection()
            {
                Fonts = WordHeadingDetectionFonts,
                Styles = WordHeadingDetectionStyles
            };

            WordSettings WordSettingsModel = new WordSettings()
            {
                Heading = WordHeadingDetectionModel
            };

            var PdfHeadingDetectionFonts = new List<FontSetting> { FontSettingModel };
            PdfHeadingDetection PdfHeadingDetectionModel = new PdfHeadingDetection()
            {
                Fonts = PdfHeadingDetectionFonts
            };

            PdfSettings PdfSettingsModel = new PdfSettings()
            {
                Heading = PdfHeadingDetectionModel
            };

            var ConversionsJsonNormalizations = new List<NormalizationOperation> { NormalizationOperationModel };
            Conversions ConversionsModel = new Conversions()
            {
                Pdf = PdfSettingsModel,
                Word = WordSettingsModel,
                Html = HtmlSettingsModel,
                Segment = SegmentSettingsModel,
                JsonNormalizations = ConversionsJsonNormalizations,
                ImageTextRecognition = true
            };

            var ConfigurationEnrichments = new List<Enrichment> { EnrichmentModel };
            var ConfigurationNormalizations = new List<NormalizationOperation> { NormalizationOperationModel };
            Configuration testRequestModel = new Configuration()
            {
                Name = "testString",
                Description = "testString",
                Conversions = ConversionsModel,
                Enrichments = ConfigurationEnrichments,
                Normalizations = ConfigurationNormalizations,
                Source = SourceModel
            };

            Assert.IsTrue(testRequestModel.Name == "testString");
            Assert.IsTrue(testRequestModel.Description == "testString");
            Assert.IsTrue(testRequestModel.Conversions == ConversionsModel);
            Assert.IsTrue(testRequestModel.Enrichments == ConfigurationEnrichments);
            Assert.IsTrue(testRequestModel.Normalizations == ConfigurationNormalizations);
            Assert.IsTrue(testRequestModel.Source == SourceModel);
        }

        [TestMethod]
        public void TestConversionsModel()
        {
            NormalizationOperation NormalizationOperationModel = new NormalizationOperation()
            {
                Operation = "copy",
                SourceField = "testString",
                DestinationField = "testString"
            };

            var SegmentSettingsSelectorTags = new List<string> { "testString" };
            var SegmentSettingsAnnotatedFields = new List<string> { "testString" };
            SegmentSettings SegmentSettingsModel = new SegmentSettings()
            {
                Enabled = true,
                SelectorTags = SegmentSettingsSelectorTags,
                AnnotatedFields = SegmentSettingsAnnotatedFields
            };

            var XPathPatternsXpaths = new List<string> { "testString" };
            XPathPatterns XPathPatternsModel = new XPathPatterns()
            {
                Xpaths = XPathPatternsXpaths
            };

            var HtmlSettingsExcludeTagsCompletely = new List<string> { "testString" };
            var HtmlSettingsExcludeTagsKeepContent = new List<string> { "testString" };
            var HtmlSettingsKeepTagAttributes = new List<string> { "testString" };
            var HtmlSettingsExcludeTagAttributes = new List<string> { "testString" };
            HtmlSettings HtmlSettingsModel = new HtmlSettings()
            {
                ExcludeTagsCompletely = HtmlSettingsExcludeTagsCompletely,
                ExcludeTagsKeepContent = HtmlSettingsExcludeTagsKeepContent,
                KeepContent = XPathPatternsModel,
                ExcludeContent = XPathPatternsModel,
                KeepTagAttributes = HtmlSettingsKeepTagAttributes,
                ExcludeTagAttributes = HtmlSettingsExcludeTagAttributes
            };

            var WordStyleNames = new List<string> { "testString" };
            WordStyle WordStyleModel = new WordStyle()
            {
                Level = 38,
                Names = WordStyleNames
            };

            FontSetting FontSettingModel = new FontSetting()
            {
                Level = 38,
                MinSize = 38,
                MaxSize = 38,
                Bold = true,
                Italic = true,
                Name = "testString"
            };

            var WordHeadingDetectionFonts = new List<FontSetting> { FontSettingModel };
            var WordHeadingDetectionStyles = new List<WordStyle> { WordStyleModel };
            WordHeadingDetection WordHeadingDetectionModel = new WordHeadingDetection()
            {
                Fonts = WordHeadingDetectionFonts,
                Styles = WordHeadingDetectionStyles
            };

            WordSettings WordSettingsModel = new WordSettings()
            {
                Heading = WordHeadingDetectionModel
            };

            var PdfHeadingDetectionFonts = new List<FontSetting> { FontSettingModel };
            PdfHeadingDetection PdfHeadingDetectionModel = new PdfHeadingDetection()
            {
                Fonts = PdfHeadingDetectionFonts
            };

            PdfSettings PdfSettingsModel = new PdfSettings()
            {
                Heading = PdfHeadingDetectionModel
            };

            var ConversionsJsonNormalizations = new List<NormalizationOperation> { NormalizationOperationModel };
            Conversions testRequestModel = new Conversions()
            {
                Pdf = PdfSettingsModel,
                Word = WordSettingsModel,
                Html = HtmlSettingsModel,
                Segment = SegmentSettingsModel,
                JsonNormalizations = ConversionsJsonNormalizations,
                ImageTextRecognition = true
            };

            Assert.IsTrue(testRequestModel.Pdf == PdfSettingsModel);
            Assert.IsTrue(testRequestModel.Word == WordSettingsModel);
            Assert.IsTrue(testRequestModel.Html == HtmlSettingsModel);
            Assert.IsTrue(testRequestModel.Segment == SegmentSettingsModel);
            Assert.IsTrue(testRequestModel.JsonNormalizations == ConversionsJsonNormalizations);
            Assert.IsTrue(testRequestModel.ImageTextRecognition == true);
        }

        [TestMethod]
        public void TestCredentialDetailsModel()
        {

            CredentialDetails testRequestModel = new CredentialDetails()
            {
                CredentialType = "oauth2",
                ClientId = "testString",
                EnterpriseId = "testString",
                Url = "testString",
                Username = "testString",
                OrganizationUrl = "testString",
                SiteCollectionPath = "testString",
                ClientSecret = "testString",
                PublicKeyId = "testString",
                PrivateKey = "testString",
                Passphrase = "testString",
                Password = "testString",
                GatewayId = "testString",
                SourceVersion = "online",
                WebApplicationUrl = "testString",
                Domain = "testString",
                Endpoint = "testString",
                AccessKeyId = "testString",
                SecretAccessKey = "testString"
            };

            Assert.IsTrue(testRequestModel.CredentialType == "oauth2");
            Assert.IsTrue(testRequestModel.ClientId == "testString");
            Assert.IsTrue(testRequestModel.EnterpriseId == "testString");
            Assert.IsTrue(testRequestModel.Url == "testString");
            Assert.IsTrue(testRequestModel.Username == "testString");
            Assert.IsTrue(testRequestModel.OrganizationUrl == "testString");
            Assert.IsTrue(testRequestModel.SiteCollectionPath == "testString");
            Assert.IsTrue(testRequestModel.ClientSecret == "testString");
            Assert.IsTrue(testRequestModel.PublicKeyId == "testString");
            Assert.IsTrue(testRequestModel.PrivateKey == "testString");
            Assert.IsTrue(testRequestModel.Passphrase == "testString");
            Assert.IsTrue(testRequestModel.Password == "testString");
            Assert.IsTrue(testRequestModel.GatewayId == "testString");
            Assert.IsTrue(testRequestModel.SourceVersion == "online");
            Assert.IsTrue(testRequestModel.WebApplicationUrl == "testString");
            Assert.IsTrue(testRequestModel.Domain == "testString");
            Assert.IsTrue(testRequestModel.Endpoint == "testString");
            Assert.IsTrue(testRequestModel.AccessKeyId == "testString");
            Assert.IsTrue(testRequestModel.SecretAccessKey == "testString");
        }

        [TestMethod]
        public void TestCredentialsModel()
        {
            CredentialDetails CredentialDetailsModel = new CredentialDetails()
            {
                CredentialType = "oauth2",
                ClientId = "testString",
                EnterpriseId = "testString",
                Url = "testString",
                Username = "testString",
                OrganizationUrl = "testString",
                SiteCollectionPath = "testString",
                ClientSecret = "testString",
                PublicKeyId = "testString",
                PrivateKey = "testString",
                Passphrase = "testString",
                Password = "testString",
                GatewayId = "testString",
                SourceVersion = "online",
                WebApplicationUrl = "testString",
                Domain = "testString",
                Endpoint = "testString",
                AccessKeyId = "testString",
                SecretAccessKey = "testString"
            };

            Credentials testRequestModel = new Credentials()
            {
                SourceType = "box",
                CredentialDetails = CredentialDetailsModel,
                Status = "connected"
            };

            Assert.IsTrue(testRequestModel.SourceType == "box");
            Assert.IsTrue(testRequestModel.CredentialDetails == CredentialDetailsModel);
            Assert.IsTrue(testRequestModel.Status == "connected");
        }

        [TestMethod]
        public void TestEnrichmentModel()
        {
            NluEnrichmentConcepts NluEnrichmentConceptsModel = new NluEnrichmentConcepts()
            {
                Limit = 38
            };

            NluEnrichmentRelations NluEnrichmentRelationsModel = new NluEnrichmentRelations()
            {
                Model = "testString"
            };

            NluEnrichmentSemanticRoles NluEnrichmentSemanticRolesModel = new NluEnrichmentSemanticRoles()
            {
                Entities = true,
                Keywords = true,
                Limit = 38
            };

            NluEnrichmentCategories NluEnrichmentCategoriesModel = new NluEnrichmentCategories()
            {
            };

            var NluEnrichmentEmotionTargets = new List<string> { "testString" };
            NluEnrichmentEmotion NluEnrichmentEmotionModel = new NluEnrichmentEmotion()
            {
                Document = true,
                Targets = NluEnrichmentEmotionTargets
            };

            var NluEnrichmentSentimentTargets = new List<string> { "testString" };
            NluEnrichmentSentiment NluEnrichmentSentimentModel = new NluEnrichmentSentiment()
            {
                Document = true,
                Targets = NluEnrichmentSentimentTargets
            };

            NluEnrichmentEntities NluEnrichmentEntitiesModel = new NluEnrichmentEntities()
            {
                Sentiment = true,
                Emotion = true,
                Limit = 38,
                Mentions = true,
                MentionTypes = true,
                SentenceLocations = true,
                Model = "testString"
            };

            NluEnrichmentKeywords NluEnrichmentKeywordsModel = new NluEnrichmentKeywords()
            {
                Sentiment = true,
                Emotion = true,
                Limit = 38
            };

            NluEnrichmentFeatures NluEnrichmentFeaturesModel = new NluEnrichmentFeatures()
            {
                Keywords = NluEnrichmentKeywordsModel,
                Entities = NluEnrichmentEntitiesModel,
                Sentiment = NluEnrichmentSentimentModel,
                Emotion = NluEnrichmentEmotionModel,
                Categories = NluEnrichmentCategoriesModel,
                SemanticRoles = NluEnrichmentSemanticRolesModel,
                Relations = NluEnrichmentRelationsModel,
                Concepts = NluEnrichmentConceptsModel
            };

            EnrichmentOptions EnrichmentOptionsModel = new EnrichmentOptions()
            {
                Features = NluEnrichmentFeaturesModel,
                Language = "ar",
                Model = "testString"
            };

            Enrichment testRequestModel = new Enrichment()
            {
                Description = "testString",
                DestinationField = "testString",
                SourceField = "testString",
                Overwrite = true,
                _Enrichment = "testString",
                IgnoreDownstreamErrors = true,
                Options = EnrichmentOptionsModel
            };

            Assert.IsTrue(testRequestModel.Description == "testString");
            Assert.IsTrue(testRequestModel.DestinationField == "testString");
            Assert.IsTrue(testRequestModel.SourceField == "testString");
            Assert.IsTrue(testRequestModel.Overwrite == true);
            Assert.IsTrue(testRequestModel._Enrichment == "testString");
            Assert.IsTrue(testRequestModel.IgnoreDownstreamErrors == true);
            Assert.IsTrue(testRequestModel.Options == EnrichmentOptionsModel);
        }

        [TestMethod]
        public void TestEnrichmentOptionsModel()
        {
            NluEnrichmentConcepts NluEnrichmentConceptsModel = new NluEnrichmentConcepts()
            {
                Limit = 38
            };

            NluEnrichmentRelations NluEnrichmentRelationsModel = new NluEnrichmentRelations()
            {
                Model = "testString"
            };

            NluEnrichmentSemanticRoles NluEnrichmentSemanticRolesModel = new NluEnrichmentSemanticRoles()
            {
                Entities = true,
                Keywords = true,
                Limit = 38
            };

            NluEnrichmentCategories NluEnrichmentCategoriesModel = new NluEnrichmentCategories()
            {
            };

            var NluEnrichmentEmotionTargets = new List<string> { "testString" };
            NluEnrichmentEmotion NluEnrichmentEmotionModel = new NluEnrichmentEmotion()
            {
                Document = true,
                Targets = NluEnrichmentEmotionTargets
            };

            var NluEnrichmentSentimentTargets = new List<string> { "testString" };
            NluEnrichmentSentiment NluEnrichmentSentimentModel = new NluEnrichmentSentiment()
            {
                Document = true,
                Targets = NluEnrichmentSentimentTargets
            };

            NluEnrichmentEntities NluEnrichmentEntitiesModel = new NluEnrichmentEntities()
            {
                Sentiment = true,
                Emotion = true,
                Limit = 38,
                Mentions = true,
                MentionTypes = true,
                SentenceLocations = true,
                Model = "testString"
            };

            NluEnrichmentKeywords NluEnrichmentKeywordsModel = new NluEnrichmentKeywords()
            {
                Sentiment = true,
                Emotion = true,
                Limit = 38
            };

            NluEnrichmentFeatures NluEnrichmentFeaturesModel = new NluEnrichmentFeatures()
            {
                Keywords = NluEnrichmentKeywordsModel,
                Entities = NluEnrichmentEntitiesModel,
                Sentiment = NluEnrichmentSentimentModel,
                Emotion = NluEnrichmentEmotionModel,
                Categories = NluEnrichmentCategoriesModel,
                SemanticRoles = NluEnrichmentSemanticRolesModel,
                Relations = NluEnrichmentRelationsModel,
                Concepts = NluEnrichmentConceptsModel
            };

            EnrichmentOptions testRequestModel = new EnrichmentOptions()
            {
                Features = NluEnrichmentFeaturesModel,
                Language = "ar",
                Model = "testString"
            };

            Assert.IsTrue(testRequestModel.Features == NluEnrichmentFeaturesModel);
            Assert.IsTrue(testRequestModel.Language == "ar");
            Assert.IsTrue(testRequestModel.Model == "testString");
        }

        [TestMethod]
        public void TestEventDataModel()
        {

            EventData testRequestModel = new EventData()
            {
                EnvironmentId = "testString",
                SessionToken = "testString",
                ClientTimestamp = new DateTime(2019, 1, 1),
                DisplayRank = 38,
                CollectionId = "testString",
                DocumentId = "testString",
            };

            Assert.IsTrue(testRequestModel.EnvironmentId == "testString");
            Assert.IsTrue(testRequestModel.SessionToken == "testString");
            Assert.IsTrue(testRequestModel.ClientTimestamp == new DateTime(2019, 1, 1));
            Assert.IsTrue(testRequestModel.DisplayRank == 38);
            Assert.IsTrue(testRequestModel.CollectionId == "testString");
            Assert.IsTrue(testRequestModel.DocumentId == "testString");
        }

        [TestMethod]
        public void TestExpansionModel()
        {

            var ExpansionInputTerms = new List<string> { "testString" };
            var ExpansionExpandedTerms = new List<string> { "testString" };
            Expansion testRequestModel = new Expansion()
            {
                InputTerms = ExpansionInputTerms,
                ExpandedTerms = ExpansionExpandedTerms
            };

            Assert.IsTrue(testRequestModel.InputTerms == ExpansionInputTerms);
            Assert.IsTrue(testRequestModel.ExpandedTerms == ExpansionExpandedTerms);
        }

        [TestMethod]
        public void TestExpansionsModel()
        {
            var ExpansionInputTerms = new List<string> { "testString" };
            var ExpansionExpandedTerms = new List<string> { "testString" };
            Expansion ExpansionModel = new Expansion()
            {
                InputTerms = ExpansionInputTerms,
                ExpandedTerms = ExpansionExpandedTerms
            };

            var Expansions_Expansions = new List<Expansion> { ExpansionModel };
            Expansions testRequestModel = new Expansions()
            {
                _Expansions = Expansions_Expansions
            };

            Assert.IsTrue(testRequestModel._Expansions == Expansions_Expansions);
        }

        [TestMethod]
        public void TestFontSettingModel()
        {

            FontSetting testRequestModel = new FontSetting()
            {
                Level = 38,
                MinSize = 38,
                MaxSize = 38,
                Bold = true,
                Italic = true,
                Name = "testString"
            };

            Assert.IsTrue(testRequestModel.Level == 38);
            Assert.IsTrue(testRequestModel.MinSize == 38);
            Assert.IsTrue(testRequestModel.MaxSize == 38);
            Assert.IsTrue(testRequestModel.Bold == true);
            Assert.IsTrue(testRequestModel.Italic == true);
            Assert.IsTrue(testRequestModel.Name == "testString");
        }

        [TestMethod]
        public void TestHtmlSettingsModel()
        {
            var XPathPatternsXpaths = new List<string> { "testString" };
            XPathPatterns XPathPatternsModel = new XPathPatterns()
            {
                Xpaths = XPathPatternsXpaths
            };

            var HtmlSettingsExcludeTagsCompletely = new List<string> { "testString" };
            var HtmlSettingsExcludeTagsKeepContent = new List<string> { "testString" };
            var HtmlSettingsKeepTagAttributes = new List<string> { "testString" };
            var HtmlSettingsExcludeTagAttributes = new List<string> { "testString" };
            HtmlSettings testRequestModel = new HtmlSettings()
            {
                ExcludeTagsCompletely = HtmlSettingsExcludeTagsCompletely,
                ExcludeTagsKeepContent = HtmlSettingsExcludeTagsKeepContent,
                KeepContent = XPathPatternsModel,
                ExcludeContent = XPathPatternsModel,
                KeepTagAttributes = HtmlSettingsKeepTagAttributes,
                ExcludeTagAttributes = HtmlSettingsExcludeTagAttributes
            };

            Assert.IsTrue(testRequestModel.ExcludeTagsCompletely == HtmlSettingsExcludeTagsCompletely);
            Assert.IsTrue(testRequestModel.ExcludeTagsKeepContent == HtmlSettingsExcludeTagsKeepContent);
            Assert.IsTrue(testRequestModel.KeepContent == XPathPatternsModel);
            Assert.IsTrue(testRequestModel.ExcludeContent == XPathPatternsModel);
            Assert.IsTrue(testRequestModel.KeepTagAttributes == HtmlSettingsKeepTagAttributes);
            Assert.IsTrue(testRequestModel.ExcludeTagAttributes == HtmlSettingsExcludeTagAttributes);
        }

        [TestMethod]
        public void TestNluEnrichmentConceptsModel()
        {

            NluEnrichmentConcepts testRequestModel = new NluEnrichmentConcepts()
            {
                Limit = 38
            };

            Assert.IsTrue(testRequestModel.Limit == 38);
        }

        [TestMethod]
        public void TestNluEnrichmentEmotionModel()
        {

            var NluEnrichmentEmotionTargets = new List<string> { "testString" };
            NluEnrichmentEmotion testRequestModel = new NluEnrichmentEmotion()
            {
                Document = true,
                Targets = NluEnrichmentEmotionTargets
            };

            Assert.IsTrue(testRequestModel.Document == true);
            Assert.IsTrue(testRequestModel.Targets == NluEnrichmentEmotionTargets);
        }

        [TestMethod]
        public void TestNluEnrichmentEntitiesModel()
        {

            NluEnrichmentEntities testRequestModel = new NluEnrichmentEntities()
            {
                Sentiment = true,
                Emotion = true,
                Limit = 38,
                Mentions = true,
                MentionTypes = true,
                SentenceLocations = true,
                Model = "testString"
            };

            Assert.IsTrue(testRequestModel.Sentiment == true);
            Assert.IsTrue(testRequestModel.Emotion == true);
            Assert.IsTrue(testRequestModel.Limit == 38);
            Assert.IsTrue(testRequestModel.Mentions == true);
            Assert.IsTrue(testRequestModel.MentionTypes == true);
            Assert.IsTrue(testRequestModel.SentenceLocations == true);
            Assert.IsTrue(testRequestModel.Model == "testString");
        }

        [TestMethod]
        public void TestNluEnrichmentFeaturesModel()
        {
            NluEnrichmentConcepts NluEnrichmentConceptsModel = new NluEnrichmentConcepts()
            {
                Limit = 38
            };

            NluEnrichmentRelations NluEnrichmentRelationsModel = new NluEnrichmentRelations()
            {
                Model = "testString"
            };

            NluEnrichmentSemanticRoles NluEnrichmentSemanticRolesModel = new NluEnrichmentSemanticRoles()
            {
                Entities = true,
                Keywords = true,
                Limit = 38
            };

            NluEnrichmentCategories NluEnrichmentCategoriesModel = new NluEnrichmentCategories()
            {
            };

            var NluEnrichmentEmotionTargets = new List<string> { "testString" };
            NluEnrichmentEmotion NluEnrichmentEmotionModel = new NluEnrichmentEmotion()
            {
                Document = true,
                Targets = NluEnrichmentEmotionTargets
            };

            var NluEnrichmentSentimentTargets = new List<string> { "testString" };
            NluEnrichmentSentiment NluEnrichmentSentimentModel = new NluEnrichmentSentiment()
            {
                Document = true,
                Targets = NluEnrichmentSentimentTargets
            };

            NluEnrichmentEntities NluEnrichmentEntitiesModel = new NluEnrichmentEntities()
            {
                Sentiment = true,
                Emotion = true,
                Limit = 38,
                Mentions = true,
                MentionTypes = true,
                SentenceLocations = true,
                Model = "testString"
            };

            NluEnrichmentKeywords NluEnrichmentKeywordsModel = new NluEnrichmentKeywords()
            {
                Sentiment = true,
                Emotion = true,
                Limit = 38
            };

            NluEnrichmentFeatures testRequestModel = new NluEnrichmentFeatures()
            {
                Keywords = NluEnrichmentKeywordsModel,
                Entities = NluEnrichmentEntitiesModel,
                Sentiment = NluEnrichmentSentimentModel,
                Emotion = NluEnrichmentEmotionModel,
                Categories = NluEnrichmentCategoriesModel,
                SemanticRoles = NluEnrichmentSemanticRolesModel,
                Relations = NluEnrichmentRelationsModel,
                Concepts = NluEnrichmentConceptsModel
            };

            Assert.IsTrue(testRequestModel.Keywords == NluEnrichmentKeywordsModel);
            Assert.IsTrue(testRequestModel.Entities == NluEnrichmentEntitiesModel);
            Assert.IsTrue(testRequestModel.Sentiment == NluEnrichmentSentimentModel);
            Assert.IsTrue(testRequestModel.Emotion == NluEnrichmentEmotionModel);
            Assert.IsTrue(testRequestModel.Categories == NluEnrichmentCategoriesModel);
            Assert.IsTrue(testRequestModel.SemanticRoles == NluEnrichmentSemanticRolesModel);
            Assert.IsTrue(testRequestModel.Relations == NluEnrichmentRelationsModel);
            Assert.IsTrue(testRequestModel.Concepts == NluEnrichmentConceptsModel);
        }

        [TestMethod]
        public void TestNluEnrichmentKeywordsModel()
        {

            NluEnrichmentKeywords testRequestModel = new NluEnrichmentKeywords()
            {
                Sentiment = true,
                Emotion = true,
                Limit = 38
            };

            Assert.IsTrue(testRequestModel.Sentiment == true);
            Assert.IsTrue(testRequestModel.Emotion == true);
            Assert.IsTrue(testRequestModel.Limit == 38);
        }

        [TestMethod]
        public void TestNluEnrichmentRelationsModel()
        {

            NluEnrichmentRelations testRequestModel = new NluEnrichmentRelations()
            {
                Model = "testString"
            };

            Assert.IsTrue(testRequestModel.Model == "testString");
        }

        [TestMethod]
        public void TestNluEnrichmentSemanticRolesModel()
        {

            NluEnrichmentSemanticRoles testRequestModel = new NluEnrichmentSemanticRoles()
            {
                Entities = true,
                Keywords = true,
                Limit = 38
            };

            Assert.IsTrue(testRequestModel.Entities == true);
            Assert.IsTrue(testRequestModel.Keywords == true);
            Assert.IsTrue(testRequestModel.Limit == 38);
        }

        [TestMethod]
        public void TestNluEnrichmentSentimentModel()
        {

            var NluEnrichmentSentimentTargets = new List<string> { "testString" };
            NluEnrichmentSentiment testRequestModel = new NluEnrichmentSentiment()
            {
                Document = true,
                Targets = NluEnrichmentSentimentTargets
            };

            Assert.IsTrue(testRequestModel.Document == true);
            Assert.IsTrue(testRequestModel.Targets == NluEnrichmentSentimentTargets);
        }

        [TestMethod]
        public void TestNormalizationOperationModel()
        {

            NormalizationOperation testRequestModel = new NormalizationOperation()
            {
                Operation = "copy",
                SourceField = "testString",
                DestinationField = "testString"
            };

            Assert.IsTrue(testRequestModel.Operation == "copy");
            Assert.IsTrue(testRequestModel.SourceField == "testString");
            Assert.IsTrue(testRequestModel.DestinationField == "testString");
        }

        [TestMethod]
        public void TestPdfHeadingDetectionModel()
        {
            FontSetting FontSettingModel = new FontSetting()
            {
                Level = 38,
                MinSize = 38,
                MaxSize = 38,
                Bold = true,
                Italic = true,
                Name = "testString"
            };

            var PdfHeadingDetectionFonts = new List<FontSetting> { FontSettingModel };
            PdfHeadingDetection testRequestModel = new PdfHeadingDetection()
            {
                Fonts = PdfHeadingDetectionFonts
            };

            Assert.IsTrue(testRequestModel.Fonts == PdfHeadingDetectionFonts);
        }

        [TestMethod]
        public void TestPdfSettingsModel()
        {
            FontSetting FontSettingModel = new FontSetting()
            {
                Level = 38,
                MinSize = 38,
                MaxSize = 38,
                Bold = true,
                Italic = true,
                Name = "testString"
            };

            var PdfHeadingDetectionFonts = new List<FontSetting> { FontSettingModel };
            PdfHeadingDetection PdfHeadingDetectionModel = new PdfHeadingDetection()
            {
                Fonts = PdfHeadingDetectionFonts
            };

            PdfSettings testRequestModel = new PdfSettings()
            {
                Heading = PdfHeadingDetectionModel
            };

            Assert.IsTrue(testRequestModel.Heading == PdfHeadingDetectionModel);
        }

        [TestMethod]
        public void TestSegmentSettingsModel()
        {

            var SegmentSettingsSelectorTags = new List<string> { "testString" };
            var SegmentSettingsAnnotatedFields = new List<string> { "testString" };
            SegmentSettings testRequestModel = new SegmentSettings()
            {
                Enabled = true,
                SelectorTags = SegmentSettingsSelectorTags,
                AnnotatedFields = SegmentSettingsAnnotatedFields
            };

            Assert.IsTrue(testRequestModel.Enabled == true);
            Assert.IsTrue(testRequestModel.SelectorTags == SegmentSettingsSelectorTags);
            Assert.IsTrue(testRequestModel.AnnotatedFields == SegmentSettingsAnnotatedFields);
        }

        [TestMethod]
        public void TestSourceModel()
        {
            SourceOptionsBuckets SourceOptionsBucketsModel = new SourceOptionsBuckets()
            {
                Name = "testString",
                Limit = 38
            };

            var SourceOptionsWebCrawlBlacklist = new List<string> { "testString" };
            SourceOptionsWebCrawl SourceOptionsWebCrawlModel = new SourceOptionsWebCrawl()
            {
                Url = "testString",
                LimitToStartingHosts = true,
                CrawlSpeed = "gentle",
                AllowUntrustedCertificate = true,
                MaximumHops = 38,
                RequestTimeout = 38,
                OverrideRobotsTxt = true,
                Blacklist = SourceOptionsWebCrawlBlacklist
            };

            SourceOptionsSiteColl SourceOptionsSiteCollModel = new SourceOptionsSiteColl()
            {
                SiteCollectionPath = "testString",
                Limit = 38
            };

            SourceOptionsObject SourceOptionsObjectModel = new SourceOptionsObject()
            {
                Name = "testString",
                Limit = 38
            };

            SourceOptionsFolder SourceOptionsFolderModel = new SourceOptionsFolder()
            {
                OwnerUserId = "testString",
                FolderId = "testString",
                Limit = 38
            };

            var SourceOptionsFolders = new List<SourceOptionsFolder> { SourceOptionsFolderModel };
            var SourceOptionsObjects = new List<SourceOptionsObject> { SourceOptionsObjectModel };
            var SourceOptionsSiteCollections = new List<SourceOptionsSiteColl> { SourceOptionsSiteCollModel };
            var SourceOptionsUrls = new List<SourceOptionsWebCrawl> { SourceOptionsWebCrawlModel };
            var SourceOptionsBuckets = new List<SourceOptionsBuckets> { SourceOptionsBucketsModel };
            SourceOptions SourceOptionsModel = new SourceOptions()
            {
                Folders = SourceOptionsFolders,
                Objects = SourceOptionsObjects,
                SiteCollections = SourceOptionsSiteCollections,
                Urls = SourceOptionsUrls,
                Buckets = SourceOptionsBuckets,
                CrawlAllBuckets = true
            };

            SourceSchedule SourceScheduleModel = new SourceSchedule()
            {
                Enabled = true,
                TimeZone = "testString",
                Frequency = "daily"
            };

            Source testRequestModel = new Source()
            {
                Type = "box",
                CredentialId = "testString",
                Schedule = SourceScheduleModel,
                Options = SourceOptionsModel
            };

            Assert.IsTrue(testRequestModel.Type == "box");
            Assert.IsTrue(testRequestModel.CredentialId == "testString");
            Assert.IsTrue(testRequestModel.Schedule == SourceScheduleModel);
            Assert.IsTrue(testRequestModel.Options == SourceOptionsModel);
        }

        [TestMethod]
        public void TestSourceOptionsModel()
        {
            SourceOptionsBuckets SourceOptionsBucketsModel = new SourceOptionsBuckets()
            {
                Name = "testString",
                Limit = 38
            };

            var SourceOptionsWebCrawlBlacklist = new List<string> { "testString" };
            SourceOptionsWebCrawl SourceOptionsWebCrawlModel = new SourceOptionsWebCrawl()
            {
                Url = "testString",
                LimitToStartingHosts = true,
                CrawlSpeed = "gentle",
                AllowUntrustedCertificate = true,
                MaximumHops = 38,
                RequestTimeout = 38,
                OverrideRobotsTxt = true,
                Blacklist = SourceOptionsWebCrawlBlacklist
            };

            SourceOptionsSiteColl SourceOptionsSiteCollModel = new SourceOptionsSiteColl()
            {
                SiteCollectionPath = "testString",
                Limit = 38
            };

            SourceOptionsObject SourceOptionsObjectModel = new SourceOptionsObject()
            {
                Name = "testString",
                Limit = 38
            };

            SourceOptionsFolder SourceOptionsFolderModel = new SourceOptionsFolder()
            {
                OwnerUserId = "testString",
                FolderId = "testString",
                Limit = 38
            };

            var SourceOptionsFolders = new List<SourceOptionsFolder> { SourceOptionsFolderModel };
            var SourceOptionsObjects = new List<SourceOptionsObject> { SourceOptionsObjectModel };
            var SourceOptionsSiteCollections = new List<SourceOptionsSiteColl> { SourceOptionsSiteCollModel };
            var SourceOptionsUrls = new List<SourceOptionsWebCrawl> { SourceOptionsWebCrawlModel };
            var SourceOptionsBuckets = new List<SourceOptionsBuckets> { SourceOptionsBucketsModel };
            SourceOptions testRequestModel = new SourceOptions()
            {
                Folders = SourceOptionsFolders,
                Objects = SourceOptionsObjects,
                SiteCollections = SourceOptionsSiteCollections,
                Urls = SourceOptionsUrls,
                Buckets = SourceOptionsBuckets,
                CrawlAllBuckets = true
            };

            Assert.IsTrue(testRequestModel.Folders == SourceOptionsFolders);
            Assert.IsTrue(testRequestModel.Objects == SourceOptionsObjects);
            Assert.IsTrue(testRequestModel.SiteCollections == SourceOptionsSiteCollections);
            Assert.IsTrue(testRequestModel.Urls == SourceOptionsUrls);
            Assert.IsTrue(testRequestModel.Buckets == SourceOptionsBuckets);
            Assert.IsTrue(testRequestModel.CrawlAllBuckets == true);
        }

        [TestMethod]
        public void TestSourceOptionsBucketsModel()
        {

            SourceOptionsBuckets testRequestModel = new SourceOptionsBuckets()
            {
                Name = "testString",
                Limit = 38
            };

            Assert.IsTrue(testRequestModel.Name == "testString");
            Assert.IsTrue(testRequestModel.Limit == 38);
        }

        [TestMethod]
        public void TestSourceOptionsFolderModel()
        {

            SourceOptionsFolder testRequestModel = new SourceOptionsFolder()
            {
                OwnerUserId = "testString",
                FolderId = "testString",
                Limit = 38
            };

            Assert.IsTrue(testRequestModel.OwnerUserId == "testString");
            Assert.IsTrue(testRequestModel.FolderId == "testString");
            Assert.IsTrue(testRequestModel.Limit == 38);
        }

        [TestMethod]
        public void TestSourceOptionsObjectModel()
        {

            SourceOptionsObject testRequestModel = new SourceOptionsObject()
            {
                Name = "testString",
                Limit = 38
            };

            Assert.IsTrue(testRequestModel.Name == "testString");
            Assert.IsTrue(testRequestModel.Limit == 38);
        }

        [TestMethod]
        public void TestSourceOptionsSiteCollModel()
        {

            SourceOptionsSiteColl testRequestModel = new SourceOptionsSiteColl()
            {
                SiteCollectionPath = "testString",
                Limit = 38
            };

            Assert.IsTrue(testRequestModel.SiteCollectionPath == "testString");
            Assert.IsTrue(testRequestModel.Limit == 38);
        }

        [TestMethod]
        public void TestSourceOptionsWebCrawlModel()
        {

            var SourceOptionsWebCrawlBlacklist = new List<string> { "testString" };
            SourceOptionsWebCrawl testRequestModel = new SourceOptionsWebCrawl()
            {
                Url = "testString",
                LimitToStartingHosts = true,
                CrawlSpeed = "gentle",
                AllowUntrustedCertificate = true,
                MaximumHops = 38,
                RequestTimeout = 38,
                OverrideRobotsTxt = true,
                Blacklist = SourceOptionsWebCrawlBlacklist
            };

            Assert.IsTrue(testRequestModel.Url == "testString");
            Assert.IsTrue(testRequestModel.LimitToStartingHosts == true);
            Assert.IsTrue(testRequestModel.CrawlSpeed == "gentle");
            Assert.IsTrue(testRequestModel.AllowUntrustedCertificate == true);
            Assert.IsTrue(testRequestModel.MaximumHops == 38);
            Assert.IsTrue(testRequestModel.RequestTimeout == 38);
            Assert.IsTrue(testRequestModel.OverrideRobotsTxt == true);
            Assert.IsTrue(testRequestModel.Blacklist == SourceOptionsWebCrawlBlacklist);
        }

        [TestMethod]
        public void TestSourceScheduleModel()
        {

            SourceSchedule testRequestModel = new SourceSchedule()
            {
                Enabled = true,
                TimeZone = "testString",
                Frequency = "daily"
            };

            Assert.IsTrue(testRequestModel.Enabled == true);
            Assert.IsTrue(testRequestModel.TimeZone == "testString");
            Assert.IsTrue(testRequestModel.Frequency == "daily");
        }

        [TestMethod]
        public void TestTokenDictRuleModel()
        {

            var TokenDictRuleTokens = new List<string> { "testString" };
            var TokenDictRuleReadings = new List<string> { "testString" };
            TokenDictRule testRequestModel = new TokenDictRule()
            {
                Text = "testString",
                Tokens = TokenDictRuleTokens,
                Readings = TokenDictRuleReadings,
                PartOfSpeech = "testString"
            };

            Assert.IsTrue(testRequestModel.Text == "testString");
            Assert.IsTrue(testRequestModel.Tokens == TokenDictRuleTokens);
            Assert.IsTrue(testRequestModel.Readings == TokenDictRuleReadings);
            Assert.IsTrue(testRequestModel.PartOfSpeech == "testString");
        }

        [TestMethod]
        public void TestTrainingExampleModel()
        {

            TrainingExample testRequestModel = new TrainingExample()
            {
                DocumentId = "testString",
                CrossReference = "testString",
                Relevance = 38
            };

            Assert.IsTrue(testRequestModel.DocumentId == "testString");
            Assert.IsTrue(testRequestModel.CrossReference == "testString");
            Assert.IsTrue(testRequestModel.Relevance == 38);
        }

        [TestMethod]
        public void TestWordHeadingDetectionModel()
        {
            var WordStyleNames = new List<string> { "testString" };
            WordStyle WordStyleModel = new WordStyle()
            {
                Level = 38,
                Names = WordStyleNames
            };

            FontSetting FontSettingModel = new FontSetting()
            {
                Level = 38,
                MinSize = 38,
                MaxSize = 38,
                Bold = true,
                Italic = true,
                Name = "testString"
            };

            var WordHeadingDetectionFonts = new List<FontSetting> { FontSettingModel };
            var WordHeadingDetectionStyles = new List<WordStyle> { WordStyleModel };
            WordHeadingDetection testRequestModel = new WordHeadingDetection()
            {
                Fonts = WordHeadingDetectionFonts,
                Styles = WordHeadingDetectionStyles
            };

            Assert.IsTrue(testRequestModel.Fonts == WordHeadingDetectionFonts);
            Assert.IsTrue(testRequestModel.Styles == WordHeadingDetectionStyles);
        }

        [TestMethod]
        public void TestWordSettingsModel()
        {
            var WordStyleNames = new List<string> { "testString" };
            WordStyle WordStyleModel = new WordStyle()
            {
                Level = 38,
                Names = WordStyleNames
            };

            FontSetting FontSettingModel = new FontSetting()
            {
                Level = 38,
                MinSize = 38,
                MaxSize = 38,
                Bold = true,
                Italic = true,
                Name = "testString"
            };

            var WordHeadingDetectionFonts = new List<FontSetting> { FontSettingModel };
            var WordHeadingDetectionStyles = new List<WordStyle> { WordStyleModel };
            WordHeadingDetection WordHeadingDetectionModel = new WordHeadingDetection()
            {
                Fonts = WordHeadingDetectionFonts,
                Styles = WordHeadingDetectionStyles
            };

            WordSettings testRequestModel = new WordSettings()
            {
                Heading = WordHeadingDetectionModel
            };

            Assert.IsTrue(testRequestModel.Heading == WordHeadingDetectionModel);
        }

        [TestMethod]
        public void TestWordStyleModel()
        {

            var WordStyleNames = new List<string> { "testString" };
            WordStyle testRequestModel = new WordStyle()
            {
                Level = 38,
                Names = WordStyleNames
            };

            Assert.IsTrue(testRequestModel.Level == 38);
            Assert.IsTrue(testRequestModel.Names == WordStyleNames);
        }

        [TestMethod]
        public void TestXPathPatternsModel()
        {

            var XPathPatternsXpaths = new List<string> { "testString" };
            XPathPatterns testRequestModel = new XPathPatterns()
            {
                Xpaths = XPathPatternsXpaths
            };

            Assert.IsTrue(testRequestModel.Xpaths == XPathPatternsXpaths);
        }

        [TestMethod]
        public void TestNluEnrichmentCategoriesModel()
        {

            NluEnrichmentCategories testRequestModel = new NluEnrichmentCategories()
            {
            };

        }

        [TestMethod]
        public void TestTestCreateEnvironmentAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'environment_id': 'EnvironmentId', 'name': 'Name', 'description': 'Description', 'status': 'active', 'read_only': false, 'size': 'LT', 'requested_size': 'RequestedSize', 'index_capacity': {'documents': {'indexed': 7, 'maximum_allowed': 14}, 'disk_usage': {'used_bytes': 9, 'maximum_allowed_bytes': 19}, 'collections': {'available': 9, 'maximum_allowed': 14}}, 'search_status': {'scope': 'Scope', 'status': 'NO_DATA', 'status_description': 'StatusDescription'}}";
            var response = new DetailedResponse<Environment>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Environment>(responseJson),
                StatusCode = 201
            };


            request.As<Environment>().Returns(Task.FromResult(response));

            var result = service.CreateEnvironment(name: "testString", description: "testString", size: "LT");

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListEnvironmentsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'environments': [{'environment_id': 'EnvironmentId', 'name': 'Name', 'description': 'Description', 'status': 'active', 'read_only': false, 'size': 'LT', 'requested_size': 'RequestedSize', 'index_capacity': {'documents': {'indexed': 7, 'maximum_allowed': 14}, 'disk_usage': {'used_bytes': 9, 'maximum_allowed_bytes': 19}, 'collections': {'available': 9, 'maximum_allowed': 14}}, 'search_status': {'scope': 'Scope', 'status': 'NO_DATA', 'status_description': 'StatusDescription'}}]}";
            var response = new DetailedResponse<ListEnvironmentsResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<ListEnvironmentsResponse>(responseJson),
                StatusCode = 200
            };

            string name = "testString";

            request.As<ListEnvironmentsResponse>().Returns(Task.FromResult(response));

            var result = service.ListEnvironments(name: name);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetEnvironmentAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'environment_id': 'EnvironmentId', 'name': 'Name', 'description': 'Description', 'status': 'active', 'read_only': false, 'size': 'LT', 'requested_size': 'RequestedSize', 'index_capacity': {'documents': {'indexed': 7, 'maximum_allowed': 14}, 'disk_usage': {'used_bytes': 9, 'maximum_allowed_bytes': 19}, 'collections': {'available': 9, 'maximum_allowed': 14}}, 'search_status': {'scope': 'Scope', 'status': 'NO_DATA', 'status_description': 'StatusDescription'}}";
            var response = new DetailedResponse<Environment>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Environment>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";

            request.As<Environment>().Returns(Task.FromResult(response));

            var result = service.GetEnvironment(environmentId: environmentId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestUpdateEnvironmentAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'environment_id': 'EnvironmentId', 'name': 'Name', 'description': 'Description', 'status': 'active', 'read_only': false, 'size': 'LT', 'requested_size': 'RequestedSize', 'index_capacity': {'documents': {'indexed': 7, 'maximum_allowed': 14}, 'disk_usage': {'used_bytes': 9, 'maximum_allowed_bytes': 19}, 'collections': {'available': 9, 'maximum_allowed': 14}}, 'search_status': {'scope': 'Scope', 'status': 'NO_DATA', 'status_description': 'StatusDescription'}}";
            var response = new DetailedResponse<Environment>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Environment>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";

            request.As<Environment>().Returns(Task.FromResult(response));

            var result = service.UpdateEnvironment(environmentId: environmentId, name: "testString", description: "testString", size: "S");

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}";
            client.Received().PutAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteEnvironmentAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'environment_id': 'EnvironmentId', 'status': 'deleted'}";
            var response = new DetailedResponse<DeleteEnvironmentResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<DeleteEnvironmentResponse>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";

            request.As<DeleteEnvironmentResponse>().Returns(Task.FromResult(response));

            var result = service.DeleteEnvironment(environmentId: environmentId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListFieldsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'fields': [{'field': '_Field', 'type': 'nested'}]}";
            var response = new DetailedResponse<ListCollectionFieldsResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<ListCollectionFieldsResponse>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            List<string> collectionIds = new List<string> { "testString" };

            request.As<ListCollectionFieldsResponse>().Returns(Task.FromResult(response));

            var result = service.ListFields(environmentId: environmentId, collectionIds: collectionIds);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/fields";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestCreateConfigurationAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'configuration_id': 'ConfigurationId', 'name': 'Name', 'description': 'Description', 'conversions': {'pdf': {'heading': {'fonts': [{'level': 5, 'min_size': 7, 'max_size': 7, 'bold': true, 'italic': true, 'name': 'Name'}]}}, 'word': {'heading': {'fonts': [{'level': 5, 'min_size': 7, 'max_size': 7, 'bold': true, 'italic': true, 'name': 'Name'}], 'styles': [{'level': 5, 'names': ['Names']}]}}, 'html': {'exclude_tags_completely': ['ExcludeTagsCompletely'], 'exclude_tags_keep_content': ['ExcludeTagsKeepContent'], 'keep_content': {'xpaths': ['Xpaths']}, 'exclude_content': {'xpaths': ['Xpaths']}, 'keep_tag_attributes': ['KeepTagAttributes'], 'exclude_tag_attributes': ['ExcludeTagAttributes']}, 'segment': {'enabled': false, 'selector_tags': ['SelectorTags'], 'annotated_fields': ['AnnotatedFields']}, 'json_normalizations': [{'operation': 'copy', 'source_field': 'SourceField', 'destination_field': 'DestinationField'}], 'image_text_recognition': true}, 'enrichments': [{'description': 'Description', 'destination_field': 'DestinationField', 'source_field': 'SourceField', 'overwrite': false, 'enrichment': '_Enrichment', 'ignore_downstream_errors': true, 'options': {'features': {'keywords': {'sentiment': false, 'emotion': false, 'limit': 5}, 'entities': {'sentiment': false, 'emotion': false, 'limit': 5, 'mentions': true, 'mention_types': true, 'sentence_locations': false, 'model': 'Model'}, 'sentiment': {'document': true, 'targets': ['Target']}, 'emotion': {'document': true, 'targets': ['Target']}, 'categories': {}, 'semantic_roles': {'entities': true, 'keywords': true, 'limit': 5}, 'relations': {'model': 'Model'}, 'concepts': {'limit': 5}}, 'language': 'ar', 'model': 'Model'}}], 'normalizations': [{'operation': 'copy', 'source_field': 'SourceField', 'destination_field': 'DestinationField'}], 'source': {'type': 'box', 'credential_id': 'CredentialId', 'schedule': {'enabled': false, 'time_zone': 'TimeZone', 'frequency': 'daily'}, 'options': {'folders': [{'owner_user_id': 'OwnerUserId', 'folder_id': 'FolderId', 'limit': 5}], 'objects': [{'name': 'Name', 'limit': 5}], 'site_collections': [{'site_collection_path': 'SiteCollectionPath', 'limit': 5}], 'urls': [{'url': 'Url', 'limit_to_starting_hosts': true, 'crawl_speed': 'gentle', 'allow_untrusted_certificate': false, 'maximum_hops': 11, 'request_timeout': 14, 'override_robots_txt': false, 'blacklist': ['Blacklist']}], 'buckets': [{'name': 'Name', 'limit': 5}], 'crawl_all_buckets': false}}}";
            var response = new DetailedResponse<Configuration>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Configuration>(responseJson),
                StatusCode = 201
            };

            SourceOptionsBuckets SourceOptionsBucketsModel = new SourceOptionsBuckets()
            {
                Name = "testString",
                Limit = 38
            };

            var SourceOptionsWebCrawlBlacklist = new List<string> { "testString" };
            SourceOptionsWebCrawl SourceOptionsWebCrawlModel = new SourceOptionsWebCrawl()
            {
                Url = "testString",
                LimitToStartingHosts = true,
                CrawlSpeed = "gentle",
                AllowUntrustedCertificate = true,
                MaximumHops = 38,
                RequestTimeout = 38,
                OverrideRobotsTxt = true,
                Blacklist = SourceOptionsWebCrawlBlacklist
            };

            SourceOptionsSiteColl SourceOptionsSiteCollModel = new SourceOptionsSiteColl()
            {
                SiteCollectionPath = "testString",
                Limit = 38
            };

            SourceOptionsObject SourceOptionsObjectModel = new SourceOptionsObject()
            {
                Name = "testString",
                Limit = 38
            };

            SourceOptionsFolder SourceOptionsFolderModel = new SourceOptionsFolder()
            {
                OwnerUserId = "testString",
                FolderId = "testString",
                Limit = 38
            };

            var SourceOptionsFolders = new List<SourceOptionsFolder> { SourceOptionsFolderModel };
            var SourceOptionsObjects = new List<SourceOptionsObject> { SourceOptionsObjectModel };
            var SourceOptionsSiteCollections = new List<SourceOptionsSiteColl> { SourceOptionsSiteCollModel };
            var SourceOptionsUrls = new List<SourceOptionsWebCrawl> { SourceOptionsWebCrawlModel };
            var SourceOptionsBuckets = new List<SourceOptionsBuckets> { SourceOptionsBucketsModel };
            SourceOptions SourceOptionsModel = new SourceOptions()
            {
                Folders = SourceOptionsFolders,
                Objects = SourceOptionsObjects,
                SiteCollections = SourceOptionsSiteCollections,
                Urls = SourceOptionsUrls,
                Buckets = SourceOptionsBuckets,
                CrawlAllBuckets = true
            };

            SourceSchedule SourceScheduleModel = new SourceSchedule()
            {
                Enabled = true,
                TimeZone = "testString",
                Frequency = "daily"
            };

            Source SourceModel = new Source()
            {
                Type = "box",
                CredentialId = "testString",
                Schedule = SourceScheduleModel,
                Options = SourceOptionsModel
            };

            NormalizationOperation NormalizationOperationModel = new NormalizationOperation()
            {
                Operation = "copy",
                SourceField = "testString",
                DestinationField = "testString"
            };

            NluEnrichmentConcepts NluEnrichmentConceptsModel = new NluEnrichmentConcepts()
            {
                Limit = 38
            };

            NluEnrichmentRelations NluEnrichmentRelationsModel = new NluEnrichmentRelations()
            {
                Model = "testString"
            };

            NluEnrichmentSemanticRoles NluEnrichmentSemanticRolesModel = new NluEnrichmentSemanticRoles()
            {
                Entities = true,
                Keywords = true,
                Limit = 38
            };

            NluEnrichmentCategories NluEnrichmentCategoriesModel = new NluEnrichmentCategories()
            {
            };

            var NluEnrichmentEmotionTargets = new List<string> { "testString" };
            NluEnrichmentEmotion NluEnrichmentEmotionModel = new NluEnrichmentEmotion()
            {
                Document = true,
                Targets = NluEnrichmentEmotionTargets
            };

            var NluEnrichmentSentimentTargets = new List<string> { "testString" };
            NluEnrichmentSentiment NluEnrichmentSentimentModel = new NluEnrichmentSentiment()
            {
                Document = true,
                Targets = NluEnrichmentSentimentTargets
            };

            NluEnrichmentEntities NluEnrichmentEntitiesModel = new NluEnrichmentEntities()
            {
                Sentiment = true,
                Emotion = true,
                Limit = 38,
                Mentions = true,
                MentionTypes = true,
                SentenceLocations = true,
                Model = "testString"
            };

            NluEnrichmentKeywords NluEnrichmentKeywordsModel = new NluEnrichmentKeywords()
            {
                Sentiment = true,
                Emotion = true,
                Limit = 38
            };

            NluEnrichmentFeatures NluEnrichmentFeaturesModel = new NluEnrichmentFeatures()
            {
                Keywords = NluEnrichmentKeywordsModel,
                Entities = NluEnrichmentEntitiesModel,
                Sentiment = NluEnrichmentSentimentModel,
                Emotion = NluEnrichmentEmotionModel,
                Categories = NluEnrichmentCategoriesModel,
                SemanticRoles = NluEnrichmentSemanticRolesModel,
                Relations = NluEnrichmentRelationsModel,
                Concepts = NluEnrichmentConceptsModel
            };

            EnrichmentOptions EnrichmentOptionsModel = new EnrichmentOptions()
            {
                Features = NluEnrichmentFeaturesModel,
                Language = "ar",
                Model = "testString"
            };

            Enrichment EnrichmentModel = new Enrichment()
            {
                Description = "testString",
                DestinationField = "testString",
                SourceField = "testString",
                Overwrite = true,
                _Enrichment = "testString",
                IgnoreDownstreamErrors = true,
                Options = EnrichmentOptionsModel
            };

            var SegmentSettingsSelectorTags = new List<string> { "testString" };
            var SegmentSettingsAnnotatedFields = new List<string> { "testString" };
            SegmentSettings SegmentSettingsModel = new SegmentSettings()
            {
                Enabled = true,
                SelectorTags = SegmentSettingsSelectorTags,
                AnnotatedFields = SegmentSettingsAnnotatedFields
            };

            var XPathPatternsXpaths = new List<string> { "testString" };
            XPathPatterns XPathPatternsModel = new XPathPatterns()
            {
                Xpaths = XPathPatternsXpaths
            };

            var HtmlSettingsExcludeTagsCompletely = new List<string> { "testString" };
            var HtmlSettingsExcludeTagsKeepContent = new List<string> { "testString" };
            var HtmlSettingsKeepTagAttributes = new List<string> { "testString" };
            var HtmlSettingsExcludeTagAttributes = new List<string> { "testString" };
            HtmlSettings HtmlSettingsModel = new HtmlSettings()
            {
                ExcludeTagsCompletely = HtmlSettingsExcludeTagsCompletely,
                ExcludeTagsKeepContent = HtmlSettingsExcludeTagsKeepContent,
                KeepContent = XPathPatternsModel,
                ExcludeContent = XPathPatternsModel,
                KeepTagAttributes = HtmlSettingsKeepTagAttributes,
                ExcludeTagAttributes = HtmlSettingsExcludeTagAttributes
            };

            var WordStyleNames = new List<string> { "testString" };
            WordStyle WordStyleModel = new WordStyle()
            {
                Level = 38,
                Names = WordStyleNames
            };

            FontSetting FontSettingModel = new FontSetting()
            {
                Level = 38,
                MinSize = 38,
                MaxSize = 38,
                Bold = true,
                Italic = true,
                Name = "testString"
            };

            var WordHeadingDetectionFonts = new List<FontSetting> { FontSettingModel };
            var WordHeadingDetectionStyles = new List<WordStyle> { WordStyleModel };
            WordHeadingDetection WordHeadingDetectionModel = new WordHeadingDetection()
            {
                Fonts = WordHeadingDetectionFonts,
                Styles = WordHeadingDetectionStyles
            };

            WordSettings WordSettingsModel = new WordSettings()
            {
                Heading = WordHeadingDetectionModel
            };

            var PdfHeadingDetectionFonts = new List<FontSetting> { FontSettingModel };
            PdfHeadingDetection PdfHeadingDetectionModel = new PdfHeadingDetection()
            {
                Fonts = PdfHeadingDetectionFonts
            };

            PdfSettings PdfSettingsModel = new PdfSettings()
            {
                Heading = PdfHeadingDetectionModel
            };

            var ConversionsJsonNormalizations = new List<NormalizationOperation> { NormalizationOperationModel };
            Conversions ConversionsModel = new Conversions()
            {
                Pdf = PdfSettingsModel,
                Word = WordSettingsModel,
                Html = HtmlSettingsModel,
                Segment = SegmentSettingsModel,
                JsonNormalizations = ConversionsJsonNormalizations,
                ImageTextRecognition = true
            };
            string environmentId = "testString";

            request.As<Configuration>().Returns(Task.FromResult(response));

            var result = service.CreateConfiguration(environmentId: environmentId, name: "testString", description: "testString", conversions: ConversionsModel, enrichments: new List<Enrichment> { EnrichmentModel }, normalizations: new List<NormalizationOperation> { NormalizationOperationModel }, source: SourceModel);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/configurations";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListConfigurationsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'configurations': [{'configuration_id': 'ConfigurationId', 'name': 'Name', 'description': 'Description', 'conversions': {'pdf': {'heading': {'fonts': [{'level': 5, 'min_size': 7, 'max_size': 7, 'bold': true, 'italic': true, 'name': 'Name'}]}}, 'word': {'heading': {'fonts': [{'level': 5, 'min_size': 7, 'max_size': 7, 'bold': true, 'italic': true, 'name': 'Name'}], 'styles': [{'level': 5, 'names': ['Names']}]}}, 'html': {'exclude_tags_completely': ['ExcludeTagsCompletely'], 'exclude_tags_keep_content': ['ExcludeTagsKeepContent'], 'keep_content': {'xpaths': ['Xpaths']}, 'exclude_content': {'xpaths': ['Xpaths']}, 'keep_tag_attributes': ['KeepTagAttributes'], 'exclude_tag_attributes': ['ExcludeTagAttributes']}, 'segment': {'enabled': false, 'selector_tags': ['SelectorTags'], 'annotated_fields': ['AnnotatedFields']}, 'json_normalizations': [{'operation': 'copy', 'source_field': 'SourceField', 'destination_field': 'DestinationField'}], 'image_text_recognition': true}, 'enrichments': [{'description': 'Description', 'destination_field': 'DestinationField', 'source_field': 'SourceField', 'overwrite': false, 'enrichment': '_Enrichment', 'ignore_downstream_errors': true, 'options': {'features': {'keywords': {'sentiment': false, 'emotion': false, 'limit': 5}, 'entities': {'sentiment': false, 'emotion': false, 'limit': 5, 'mentions': true, 'mention_types': true, 'sentence_locations': false, 'model': 'Model'}, 'sentiment': {'document': true, 'targets': ['Target']}, 'emotion': {'document': true, 'targets': ['Target']}, 'categories': {}, 'semantic_roles': {'entities': true, 'keywords': true, 'limit': 5}, 'relations': {'model': 'Model'}, 'concepts': {'limit': 5}}, 'language': 'ar', 'model': 'Model'}}], 'normalizations': [{'operation': 'copy', 'source_field': 'SourceField', 'destination_field': 'DestinationField'}], 'source': {'type': 'box', 'credential_id': 'CredentialId', 'schedule': {'enabled': false, 'time_zone': 'TimeZone', 'frequency': 'daily'}, 'options': {'folders': [{'owner_user_id': 'OwnerUserId', 'folder_id': 'FolderId', 'limit': 5}], 'objects': [{'name': 'Name', 'limit': 5}], 'site_collections': [{'site_collection_path': 'SiteCollectionPath', 'limit': 5}], 'urls': [{'url': 'Url', 'limit_to_starting_hosts': true, 'crawl_speed': 'gentle', 'allow_untrusted_certificate': false, 'maximum_hops': 11, 'request_timeout': 14, 'override_robots_txt': false, 'blacklist': ['Blacklist']}], 'buckets': [{'name': 'Name', 'limit': 5}], 'crawl_all_buckets': false}}}]}";
            var response = new DetailedResponse<ListConfigurationsResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<ListConfigurationsResponse>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string name = "testString";

            request.As<ListConfigurationsResponse>().Returns(Task.FromResult(response));

            var result = service.ListConfigurations(environmentId: environmentId, name: name);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/configurations";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetConfigurationAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'configuration_id': 'ConfigurationId', 'name': 'Name', 'description': 'Description', 'conversions': {'pdf': {'heading': {'fonts': [{'level': 5, 'min_size': 7, 'max_size': 7, 'bold': true, 'italic': true, 'name': 'Name'}]}}, 'word': {'heading': {'fonts': [{'level': 5, 'min_size': 7, 'max_size': 7, 'bold': true, 'italic': true, 'name': 'Name'}], 'styles': [{'level': 5, 'names': ['Names']}]}}, 'html': {'exclude_tags_completely': ['ExcludeTagsCompletely'], 'exclude_tags_keep_content': ['ExcludeTagsKeepContent'], 'keep_content': {'xpaths': ['Xpaths']}, 'exclude_content': {'xpaths': ['Xpaths']}, 'keep_tag_attributes': ['KeepTagAttributes'], 'exclude_tag_attributes': ['ExcludeTagAttributes']}, 'segment': {'enabled': false, 'selector_tags': ['SelectorTags'], 'annotated_fields': ['AnnotatedFields']}, 'json_normalizations': [{'operation': 'copy', 'source_field': 'SourceField', 'destination_field': 'DestinationField'}], 'image_text_recognition': true}, 'enrichments': [{'description': 'Description', 'destination_field': 'DestinationField', 'source_field': 'SourceField', 'overwrite': false, 'enrichment': '_Enrichment', 'ignore_downstream_errors': true, 'options': {'features': {'keywords': {'sentiment': false, 'emotion': false, 'limit': 5}, 'entities': {'sentiment': false, 'emotion': false, 'limit': 5, 'mentions': true, 'mention_types': true, 'sentence_locations': false, 'model': 'Model'}, 'sentiment': {'document': true, 'targets': ['Target']}, 'emotion': {'document': true, 'targets': ['Target']}, 'categories': {}, 'semantic_roles': {'entities': true, 'keywords': true, 'limit': 5}, 'relations': {'model': 'Model'}, 'concepts': {'limit': 5}}, 'language': 'ar', 'model': 'Model'}}], 'normalizations': [{'operation': 'copy', 'source_field': 'SourceField', 'destination_field': 'DestinationField'}], 'source': {'type': 'box', 'credential_id': 'CredentialId', 'schedule': {'enabled': false, 'time_zone': 'TimeZone', 'frequency': 'daily'}, 'options': {'folders': [{'owner_user_id': 'OwnerUserId', 'folder_id': 'FolderId', 'limit': 5}], 'objects': [{'name': 'Name', 'limit': 5}], 'site_collections': [{'site_collection_path': 'SiteCollectionPath', 'limit': 5}], 'urls': [{'url': 'Url', 'limit_to_starting_hosts': true, 'crawl_speed': 'gentle', 'allow_untrusted_certificate': false, 'maximum_hops': 11, 'request_timeout': 14, 'override_robots_txt': false, 'blacklist': ['Blacklist']}], 'buckets': [{'name': 'Name', 'limit': 5}], 'crawl_all_buckets': false}}}";
            var response = new DetailedResponse<Configuration>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Configuration>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string configurationId = "testString";

            request.As<Configuration>().Returns(Task.FromResult(response));

            var result = service.GetConfiguration(environmentId: environmentId, configurationId: configurationId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/configurations/{configurationId}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestUpdateConfigurationAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'configuration_id': 'ConfigurationId', 'name': 'Name', 'description': 'Description', 'conversions': {'pdf': {'heading': {'fonts': [{'level': 5, 'min_size': 7, 'max_size': 7, 'bold': true, 'italic': true, 'name': 'Name'}]}}, 'word': {'heading': {'fonts': [{'level': 5, 'min_size': 7, 'max_size': 7, 'bold': true, 'italic': true, 'name': 'Name'}], 'styles': [{'level': 5, 'names': ['Names']}]}}, 'html': {'exclude_tags_completely': ['ExcludeTagsCompletely'], 'exclude_tags_keep_content': ['ExcludeTagsKeepContent'], 'keep_content': {'xpaths': ['Xpaths']}, 'exclude_content': {'xpaths': ['Xpaths']}, 'keep_tag_attributes': ['KeepTagAttributes'], 'exclude_tag_attributes': ['ExcludeTagAttributes']}, 'segment': {'enabled': false, 'selector_tags': ['SelectorTags'], 'annotated_fields': ['AnnotatedFields']}, 'json_normalizations': [{'operation': 'copy', 'source_field': 'SourceField', 'destination_field': 'DestinationField'}], 'image_text_recognition': true}, 'enrichments': [{'description': 'Description', 'destination_field': 'DestinationField', 'source_field': 'SourceField', 'overwrite': false, 'enrichment': '_Enrichment', 'ignore_downstream_errors': true, 'options': {'features': {'keywords': {'sentiment': false, 'emotion': false, 'limit': 5}, 'entities': {'sentiment': false, 'emotion': false, 'limit': 5, 'mentions': true, 'mention_types': true, 'sentence_locations': false, 'model': 'Model'}, 'sentiment': {'document': true, 'targets': ['Target']}, 'emotion': {'document': true, 'targets': ['Target']}, 'categories': {}, 'semantic_roles': {'entities': true, 'keywords': true, 'limit': 5}, 'relations': {'model': 'Model'}, 'concepts': {'limit': 5}}, 'language': 'ar', 'model': 'Model'}}], 'normalizations': [{'operation': 'copy', 'source_field': 'SourceField', 'destination_field': 'DestinationField'}], 'source': {'type': 'box', 'credential_id': 'CredentialId', 'schedule': {'enabled': false, 'time_zone': 'TimeZone', 'frequency': 'daily'}, 'options': {'folders': [{'owner_user_id': 'OwnerUserId', 'folder_id': 'FolderId', 'limit': 5}], 'objects': [{'name': 'Name', 'limit': 5}], 'site_collections': [{'site_collection_path': 'SiteCollectionPath', 'limit': 5}], 'urls': [{'url': 'Url', 'limit_to_starting_hosts': true, 'crawl_speed': 'gentle', 'allow_untrusted_certificate': false, 'maximum_hops': 11, 'request_timeout': 14, 'override_robots_txt': false, 'blacklist': ['Blacklist']}], 'buckets': [{'name': 'Name', 'limit': 5}], 'crawl_all_buckets': false}}}";
            var response = new DetailedResponse<Configuration>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Configuration>(responseJson),
                StatusCode = 200
            };

            SourceOptionsBuckets SourceOptionsBucketsModel = new SourceOptionsBuckets()
            {
                Name = "testString",
                Limit = 38
            };

            var SourceOptionsWebCrawlBlacklist = new List<string> { "testString" };
            SourceOptionsWebCrawl SourceOptionsWebCrawlModel = new SourceOptionsWebCrawl()
            {
                Url = "testString",
                LimitToStartingHosts = true,
                CrawlSpeed = "gentle",
                AllowUntrustedCertificate = true,
                MaximumHops = 38,
                RequestTimeout = 38,
                OverrideRobotsTxt = true,
                Blacklist = SourceOptionsWebCrawlBlacklist
            };

            SourceOptionsSiteColl SourceOptionsSiteCollModel = new SourceOptionsSiteColl()
            {
                SiteCollectionPath = "testString",
                Limit = 38
            };

            SourceOptionsObject SourceOptionsObjectModel = new SourceOptionsObject()
            {
                Name = "testString",
                Limit = 38
            };

            SourceOptionsFolder SourceOptionsFolderModel = new SourceOptionsFolder()
            {
                OwnerUserId = "testString",
                FolderId = "testString",
                Limit = 38
            };

            var SourceOptionsFolders = new List<SourceOptionsFolder> { SourceOptionsFolderModel };
            var SourceOptionsObjects = new List<SourceOptionsObject> { SourceOptionsObjectModel };
            var SourceOptionsSiteCollections = new List<SourceOptionsSiteColl> { SourceOptionsSiteCollModel };
            var SourceOptionsUrls = new List<SourceOptionsWebCrawl> { SourceOptionsWebCrawlModel };
            var SourceOptionsBuckets = new List<SourceOptionsBuckets> { SourceOptionsBucketsModel };
            SourceOptions SourceOptionsModel = new SourceOptions()
            {
                Folders = SourceOptionsFolders,
                Objects = SourceOptionsObjects,
                SiteCollections = SourceOptionsSiteCollections,
                Urls = SourceOptionsUrls,
                Buckets = SourceOptionsBuckets,
                CrawlAllBuckets = true
            };

            SourceSchedule SourceScheduleModel = new SourceSchedule()
            {
                Enabled = true,
                TimeZone = "testString",
                Frequency = "daily"
            };

            Source SourceModel = new Source()
            {
                Type = "box",
                CredentialId = "testString",
                Schedule = SourceScheduleModel,
                Options = SourceOptionsModel
            };

            NormalizationOperation NormalizationOperationModel = new NormalizationOperation()
            {
                Operation = "copy",
                SourceField = "testString",
                DestinationField = "testString"
            };

            NluEnrichmentConcepts NluEnrichmentConceptsModel = new NluEnrichmentConcepts()
            {
                Limit = 38
            };

            NluEnrichmentRelations NluEnrichmentRelationsModel = new NluEnrichmentRelations()
            {
                Model = "testString"
            };

            NluEnrichmentSemanticRoles NluEnrichmentSemanticRolesModel = new NluEnrichmentSemanticRoles()
            {
                Entities = true,
                Keywords = true,
                Limit = 38
            };

            NluEnrichmentCategories NluEnrichmentCategoriesModel = new NluEnrichmentCategories()
            {
            };

            var NluEnrichmentEmotionTargets = new List<string> { "testString" };
            NluEnrichmentEmotion NluEnrichmentEmotionModel = new NluEnrichmentEmotion()
            {
                Document = true,
                Targets = NluEnrichmentEmotionTargets
            };

            var NluEnrichmentSentimentTargets = new List<string> { "testString" };
            NluEnrichmentSentiment NluEnrichmentSentimentModel = new NluEnrichmentSentiment()
            {
                Document = true,
                Targets = NluEnrichmentSentimentTargets
            };

            NluEnrichmentEntities NluEnrichmentEntitiesModel = new NluEnrichmentEntities()
            {
                Sentiment = true,
                Emotion = true,
                Limit = 38,
                Mentions = true,
                MentionTypes = true,
                SentenceLocations = true,
                Model = "testString"
            };

            NluEnrichmentKeywords NluEnrichmentKeywordsModel = new NluEnrichmentKeywords()
            {
                Sentiment = true,
                Emotion = true,
                Limit = 38
            };

            NluEnrichmentFeatures NluEnrichmentFeaturesModel = new NluEnrichmentFeatures()
            {
                Keywords = NluEnrichmentKeywordsModel,
                Entities = NluEnrichmentEntitiesModel,
                Sentiment = NluEnrichmentSentimentModel,
                Emotion = NluEnrichmentEmotionModel,
                Categories = NluEnrichmentCategoriesModel,
                SemanticRoles = NluEnrichmentSemanticRolesModel,
                Relations = NluEnrichmentRelationsModel,
                Concepts = NluEnrichmentConceptsModel
            };

            EnrichmentOptions EnrichmentOptionsModel = new EnrichmentOptions()
            {
                Features = NluEnrichmentFeaturesModel,
                Language = "ar",
                Model = "testString"
            };

            Enrichment EnrichmentModel = new Enrichment()
            {
                Description = "testString",
                DestinationField = "testString",
                SourceField = "testString",
                Overwrite = true,
                _Enrichment = "testString",
                IgnoreDownstreamErrors = true,
                Options = EnrichmentOptionsModel
            };

            var SegmentSettingsSelectorTags = new List<string> { "testString" };
            var SegmentSettingsAnnotatedFields = new List<string> { "testString" };
            SegmentSettings SegmentSettingsModel = new SegmentSettings()
            {
                Enabled = true,
                SelectorTags = SegmentSettingsSelectorTags,
                AnnotatedFields = SegmentSettingsAnnotatedFields
            };

            var XPathPatternsXpaths = new List<string> { "testString" };
            XPathPatterns XPathPatternsModel = new XPathPatterns()
            {
                Xpaths = XPathPatternsXpaths
            };

            var HtmlSettingsExcludeTagsCompletely = new List<string> { "testString" };
            var HtmlSettingsExcludeTagsKeepContent = new List<string> { "testString" };
            var HtmlSettingsKeepTagAttributes = new List<string> { "testString" };
            var HtmlSettingsExcludeTagAttributes = new List<string> { "testString" };
            HtmlSettings HtmlSettingsModel = new HtmlSettings()
            {
                ExcludeTagsCompletely = HtmlSettingsExcludeTagsCompletely,
                ExcludeTagsKeepContent = HtmlSettingsExcludeTagsKeepContent,
                KeepContent = XPathPatternsModel,
                ExcludeContent = XPathPatternsModel,
                KeepTagAttributes = HtmlSettingsKeepTagAttributes,
                ExcludeTagAttributes = HtmlSettingsExcludeTagAttributes
            };

            var WordStyleNames = new List<string> { "testString" };
            WordStyle WordStyleModel = new WordStyle()
            {
                Level = 38,
                Names = WordStyleNames
            };

            FontSetting FontSettingModel = new FontSetting()
            {
                Level = 38,
                MinSize = 38,
                MaxSize = 38,
                Bold = true,
                Italic = true,
                Name = "testString"
            };

            var WordHeadingDetectionFonts = new List<FontSetting> { FontSettingModel };
            var WordHeadingDetectionStyles = new List<WordStyle> { WordStyleModel };
            WordHeadingDetection WordHeadingDetectionModel = new WordHeadingDetection()
            {
                Fonts = WordHeadingDetectionFonts,
                Styles = WordHeadingDetectionStyles
            };

            WordSettings WordSettingsModel = new WordSettings()
            {
                Heading = WordHeadingDetectionModel
            };

            var PdfHeadingDetectionFonts = new List<FontSetting> { FontSettingModel };
            PdfHeadingDetection PdfHeadingDetectionModel = new PdfHeadingDetection()
            {
                Fonts = PdfHeadingDetectionFonts
            };

            PdfSettings PdfSettingsModel = new PdfSettings()
            {
                Heading = PdfHeadingDetectionModel
            };

            var ConversionsJsonNormalizations = new List<NormalizationOperation> { NormalizationOperationModel };
            Conversions ConversionsModel = new Conversions()
            {
                Pdf = PdfSettingsModel,
                Word = WordSettingsModel,
                Html = HtmlSettingsModel,
                Segment = SegmentSettingsModel,
                JsonNormalizations = ConversionsJsonNormalizations,
                ImageTextRecognition = true
            };
            string environmentId = "testString";
            string configurationId = "testString";

            request.As<Configuration>().Returns(Task.FromResult(response));

            var result = service.UpdateConfiguration(environmentId: environmentId, configurationId: configurationId, name: "testString", description: "testString", conversions: ConversionsModel, enrichments: new List<Enrichment> { EnrichmentModel }, normalizations: new List<NormalizationOperation> { NormalizationOperationModel }, source: SourceModel);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/configurations/{configurationId}";
            client.Received().PutAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteConfigurationAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'configuration_id': 'ConfigurationId', 'status': 'deleted', 'notices': [{'notice_id': 'NoticeId', 'document_id': 'DocumentId', 'query_id': 'QueryId', 'severity': 'warning', 'step': 'Step', 'description': 'Description'}]}";
            var response = new DetailedResponse<DeleteConfigurationResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<DeleteConfigurationResponse>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string configurationId = "testString";

            request.As<DeleteConfigurationResponse>().Returns(Task.FromResult(response));

            var result = service.DeleteConfiguration(environmentId: environmentId, configurationId: configurationId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/configurations/{configurationId}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestCreateCollectionAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'collection_id': 'CollectionId', 'name': 'Name', 'description': 'Description', 'status': 'active', 'configuration_id': 'ConfigurationId', 'language': 'Language', 'document_counts': {'available': 9, 'processing': 10, 'failed': 6, 'pending': 7}, 'disk_usage': {'used_bytes': 9}, 'training_status': {'total_examples': 13, 'available': false, 'processing': true, 'minimum_queries_added': false, 'minimum_examples_added': true, 'sufficient_label_diversity': true, 'notices': 7}, 'crawl_status': {'source_crawl': {'status': 'running'}}, 'smart_document_understanding': {'enabled': false, 'total_annotated_pages': 19, 'total_pages': 10, 'total_documents': 14, 'custom_fields': {'defined': 7, 'maximum_allowed': 14}}}";
            var response = new DetailedResponse<Collection>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Collection>(responseJson),
                StatusCode = 201
            };

            string environmentId = "testString";

            request.As<Collection>().Returns(Task.FromResult(response));

            var result = service.CreateCollection(environmentId: environmentId, name: "testString", description: "testString", configurationId: "testString", language: "en");

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListCollectionsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'collections': [{'collection_id': 'CollectionId', 'name': 'Name', 'description': 'Description', 'status': 'active', 'configuration_id': 'ConfigurationId', 'language': 'Language', 'document_counts': {'available': 9, 'processing': 10, 'failed': 6, 'pending': 7}, 'disk_usage': {'used_bytes': 9}, 'training_status': {'total_examples': 13, 'available': false, 'processing': true, 'minimum_queries_added': false, 'minimum_examples_added': true, 'sufficient_label_diversity': true, 'notices': 7}, 'crawl_status': {'source_crawl': {'status': 'running'}}, 'smart_document_understanding': {'enabled': false, 'total_annotated_pages': 19, 'total_pages': 10, 'total_documents': 14, 'custom_fields': {'defined': 7, 'maximum_allowed': 14}}}]}";
            var response = new DetailedResponse<ListCollectionsResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<ListCollectionsResponse>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string name = "testString";

            request.As<ListCollectionsResponse>().Returns(Task.FromResult(response));

            var result = service.ListCollections(environmentId: environmentId, name: name);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetCollectionAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'collection_id': 'CollectionId', 'name': 'Name', 'description': 'Description', 'status': 'active', 'configuration_id': 'ConfigurationId', 'language': 'Language', 'document_counts': {'available': 9, 'processing': 10, 'failed': 6, 'pending': 7}, 'disk_usage': {'used_bytes': 9}, 'training_status': {'total_examples': 13, 'available': false, 'processing': true, 'minimum_queries_added': false, 'minimum_examples_added': true, 'sufficient_label_diversity': true, 'notices': 7}, 'crawl_status': {'source_crawl': {'status': 'running'}}, 'smart_document_understanding': {'enabled': false, 'total_annotated_pages': 19, 'total_pages': 10, 'total_documents': 14, 'custom_fields': {'defined': 7, 'maximum_allowed': 14}}}";
            var response = new DetailedResponse<Collection>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Collection>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string collectionId = "testString";

            request.As<Collection>().Returns(Task.FromResult(response));

            var result = service.GetCollection(environmentId: environmentId, collectionId: collectionId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestUpdateCollectionAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'collection_id': 'CollectionId', 'name': 'Name', 'description': 'Description', 'status': 'active', 'configuration_id': 'ConfigurationId', 'language': 'Language', 'document_counts': {'available': 9, 'processing': 10, 'failed': 6, 'pending': 7}, 'disk_usage': {'used_bytes': 9}, 'training_status': {'total_examples': 13, 'available': false, 'processing': true, 'minimum_queries_added': false, 'minimum_examples_added': true, 'sufficient_label_diversity': true, 'notices': 7}, 'crawl_status': {'source_crawl': {'status': 'running'}}, 'smart_document_understanding': {'enabled': false, 'total_annotated_pages': 19, 'total_pages': 10, 'total_documents': 14, 'custom_fields': {'defined': 7, 'maximum_allowed': 14}}}";
            var response = new DetailedResponse<Collection>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Collection>(responseJson),
                StatusCode = 201
            };

            string environmentId = "testString";
            string collectionId = "testString";

            request.As<Collection>().Returns(Task.FromResult(response));

            var result = service.UpdateCollection(environmentId: environmentId, collectionId: collectionId, name: "testString", description: "testString", configurationId: "testString");

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}";
            client.Received().PutAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteCollectionAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'collection_id': 'CollectionId', 'status': 'deleted'}";
            var response = new DetailedResponse<DeleteCollectionResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<DeleteCollectionResponse>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string collectionId = "testString";

            request.As<DeleteCollectionResponse>().Returns(Task.FromResult(response));

            var result = service.DeleteCollection(environmentId: environmentId, collectionId: collectionId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListCollectionFieldsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'fields': [{'field': '_Field', 'type': 'nested'}]}";
            var response = new DetailedResponse<ListCollectionFieldsResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<ListCollectionFieldsResponse>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string collectionId = "testString";

            request.As<ListCollectionFieldsResponse>().Returns(Task.FromResult(response));

            var result = service.ListCollectionFields(environmentId: environmentId, collectionId: collectionId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/fields";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListExpansionsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'expansions': [{'input_terms': ['InputTerms'], 'expanded_terms': ['ExpandedTerms']}]}";
            var response = new DetailedResponse<Expansions>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Expansions>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string collectionId = "testString";

            request.As<Expansions>().Returns(Task.FromResult(response));

            var result = service.ListExpansions(environmentId: environmentId, collectionId: collectionId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/expansions";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestCreateExpansionsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'expansions': [{'input_terms': ['InputTerms'], 'expanded_terms': ['ExpandedTerms']}]}";
            var response = new DetailedResponse<Expansions>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Expansions>(responseJson),
                StatusCode = 200
            };

            var ExpansionInputTerms = new List<string> { "testString" };
            var ExpansionExpandedTerms = new List<string> { "testString" };
            Expansion ExpansionModel = new Expansion()
            {
                InputTerms = ExpansionInputTerms,
                ExpandedTerms = ExpansionExpandedTerms
            };
            string environmentId = "testString";
            string collectionId = "testString";

            request.As<Expansions>().Returns(Task.FromResult(response));

            var result = service.CreateExpansions(environmentId: environmentId, collectionId: collectionId, expansions: new List<Expansion> { ExpansionModel });

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/expansions";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteExpansionsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{}";
            var response = new DetailedResponse<object>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<object>(responseJson),
                StatusCode = 204
            };

            string environmentId = "testString";
            string collectionId = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteExpansions(environmentId: environmentId, collectionId: collectionId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/expansions";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetTokenizationDictionaryStatusAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'status': 'active', 'type': 'Type'}";
            var response = new DetailedResponse<TokenDictStatusResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<TokenDictStatusResponse>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string collectionId = "testString";

            request.As<TokenDictStatusResponse>().Returns(Task.FromResult(response));

            var result = service.GetTokenizationDictionaryStatus(environmentId: environmentId, collectionId: collectionId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/word_lists/tokenization_dictionary";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestCreateTokenizationDictionaryAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'status': 'active', 'type': 'Type'}";
            var response = new DetailedResponse<TokenDictStatusResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<TokenDictStatusResponse>(responseJson),
                StatusCode = 202
            };

            var TokenDictRuleTokens = new List<string> { "testString" };
            var TokenDictRuleReadings = new List<string> { "testString" };
            TokenDictRule TokenDictRuleModel = new TokenDictRule()
            {
                Text = "testString",
                Tokens = TokenDictRuleTokens,
                Readings = TokenDictRuleReadings,
                PartOfSpeech = "testString"
            };
            string environmentId = "testString";
            string collectionId = "testString";

            request.As<TokenDictStatusResponse>().Returns(Task.FromResult(response));

            var result = service.CreateTokenizationDictionary(environmentId: environmentId, collectionId: collectionId, tokenizationRules: new List<TokenDictRule> { TokenDictRuleModel });

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/word_lists/tokenization_dictionary";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteTokenizationDictionaryAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{}";
            var response = new DetailedResponse<object>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<object>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string collectionId = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteTokenizationDictionary(environmentId: environmentId, collectionId: collectionId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/word_lists/tokenization_dictionary";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetStopwordListStatusAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'status': 'active', 'type': 'Type'}";
            var response = new DetailedResponse<TokenDictStatusResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<TokenDictStatusResponse>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string collectionId = "testString";

            request.As<TokenDictStatusResponse>().Returns(Task.FromResult(response));

            var result = service.GetStopwordListStatus(environmentId: environmentId, collectionId: collectionId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/word_lists/stopwords";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestCreateStopwordListAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'status': 'active', 'type': 'Type'}";
            var response = new DetailedResponse<TokenDictStatusResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<TokenDictStatusResponse>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string collectionId = "testString";
            System.IO.MemoryStream stopwordFile = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file."));
            string stopwordFilename = "testString";

            request.As<TokenDictStatusResponse>().Returns(Task.FromResult(response));

            var result = service.CreateStopwordList(environmentId: environmentId, collectionId: collectionId, stopwordFile: stopwordFile, stopwordFilename: stopwordFilename);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/word_lists/stopwords";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteStopwordListAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{}";
            var response = new DetailedResponse<object>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<object>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string collectionId = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteStopwordList(environmentId: environmentId, collectionId: collectionId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/word_lists/stopwords";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestAddDocumentAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'document_id': 'DocumentId', 'status': 'processing', 'notices': [{'notice_id': 'NoticeId', 'document_id': 'DocumentId', 'query_id': 'QueryId', 'severity': 'warning', 'step': 'Step', 'description': 'Description'}]}";
            var response = new DetailedResponse<DocumentAccepted>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<DocumentAccepted>(responseJson),
                StatusCode = 202
            };

            string environmentId = "testString";
            string collectionId = "testString";
            System.IO.MemoryStream file = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file."));
            string filename = "testString";
            string fileContentType = "application/json";
            string metadata = "testString";

            request.As<DocumentAccepted>().Returns(Task.FromResult(response));

            var result = service.AddDocument(environmentId: environmentId, collectionId: collectionId, file: file, filename: filename, fileContentType: fileContentType, metadata: metadata);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/documents";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetDocumentStatusAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'document_id': 'DocumentId', 'configuration_id': 'ConfigurationId', 'status': 'available', 'status_description': 'StatusDescription', 'filename': 'Filename', 'file_type': 'pdf', 'sha1': 'Sha1', 'notices': [{'notice_id': 'NoticeId', 'document_id': 'DocumentId', 'query_id': 'QueryId', 'severity': 'warning', 'step': 'Step', 'description': 'Description'}]}";
            var response = new DetailedResponse<DocumentStatus>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<DocumentStatus>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string collectionId = "testString";
            string documentId = "testString";

            request.As<DocumentStatus>().Returns(Task.FromResult(response));

            var result = service.GetDocumentStatus(environmentId: environmentId, collectionId: collectionId, documentId: documentId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/documents/{documentId}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestUpdateDocumentAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'document_id': 'DocumentId', 'status': 'processing', 'notices': [{'notice_id': 'NoticeId', 'document_id': 'DocumentId', 'query_id': 'QueryId', 'severity': 'warning', 'step': 'Step', 'description': 'Description'}]}";
            var response = new DetailedResponse<DocumentAccepted>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<DocumentAccepted>(responseJson),
                StatusCode = 202
            };

            string environmentId = "testString";
            string collectionId = "testString";
            string documentId = "testString";
            System.IO.MemoryStream file = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file."));
            string filename = "testString";
            string fileContentType = "application/json";
            string metadata = "testString";

            request.As<DocumentAccepted>().Returns(Task.FromResult(response));

            var result = service.UpdateDocument(environmentId: environmentId, collectionId: collectionId, documentId: documentId, file: file, filename: filename, fileContentType: fileContentType, metadata: metadata);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/documents/{documentId}";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteDocumentAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'document_id': 'DocumentId', 'status': 'deleted'}";
            var response = new DetailedResponse<DeleteDocumentResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<DeleteDocumentResponse>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string collectionId = "testString";
            string documentId = "testString";

            request.As<DeleteDocumentResponse>().Returns(Task.FromResult(response));

            var result = service.DeleteDocument(environmentId: environmentId, collectionId: collectionId, documentId: documentId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/documents/{documentId}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestQueryAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'matching_results': 15, 'results': [{'id': 'Id', 'metadata': {}, 'collection_id': 'CollectionId', 'result_metadata': {'score': 5, 'confidence': 10}}], 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'passages': [{'document_id': 'DocumentId', 'passage_score': 12, 'passage_text': 'PassageText', 'start_offset': 11, 'end_offset': 9, 'field': 'Field'}], 'duplicates_removed': 17, 'session_token': 'SessionToken', 'retrieval_details': {'document_retrieval_strategy': 'untrained'}, 'suggested_query': 'SuggestedQuery'}";
            var response = new DetailedResponse<QueryResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<QueryResponse>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string collectionId = "testString";
            bool? xWatsonLoggingOptOut = true;

            request.As<QueryResponse>().Returns(Task.FromResult(response));

            var result = service.Query(environmentId: environmentId, collectionId: collectionId, filter: "testString", query: "testString", naturalLanguageQuery: "testString", passages: true, aggregation: "testString", count: 38, _return: "testString", offset: 38, sort: "testString", highlight: true, passagesFields: "testString", passagesCount: 38, passagesCharacters: 38, deduplicate: true, deduplicateField: "testString", similar: true, similarDocumentIds: "testString", similarFields: "testString", bias: "testString", spellingSuggestions: true, xWatsonLoggingOptOut: xWatsonLoggingOptOut);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/query";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestQueryNoticesAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'matching_results': 15, 'results': [{'id': 'Id', 'metadata': {}, 'collection_id': 'CollectionId', 'result_metadata': {'score': 5, 'confidence': 10}, 'code': 4, 'filename': 'Filename', 'file_type': 'pdf', 'sha1': 'Sha1', 'notices': [{'notice_id': 'NoticeId', 'document_id': 'DocumentId', 'query_id': 'QueryId', 'severity': 'warning', 'step': 'Step', 'description': 'Description'}]}], 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'passages': [{'document_id': 'DocumentId', 'passage_score': 12, 'passage_text': 'PassageText', 'start_offset': 11, 'end_offset': 9, 'field': 'Field'}], 'duplicates_removed': 17}";
            var response = new DetailedResponse<QueryNoticesResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<QueryNoticesResponse>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string collectionId = "testString";
            string filter = "testString";
            string query = "testString";
            string naturalLanguageQuery = "testString";
            bool? passages = true;
            string aggregation = "testString";
            long? count = 38;
            List<string> _return = new List<string> { "testString" };
            long? offset = 38;
            List<string> sort = new List<string> { "testString" };
            bool? highlight = true;
            List<string> passagesFields = new List<string> { "testString" };
            long? passagesCount = 38;
            long? passagesCharacters = 38;
            string deduplicateField = "testString";
            bool? similar = true;
            List<string> similarDocumentIds = new List<string> { "testString" };
            List<string> similarFields = new List<string> { "testString" };

            request.As<QueryNoticesResponse>().Returns(Task.FromResult(response));

            var result = service.QueryNotices(environmentId: environmentId, collectionId: collectionId, filter: filter, query: query, naturalLanguageQuery: naturalLanguageQuery, passages: passages, aggregation: aggregation, count: count, _return: _return, offset: offset, sort: sort, highlight: highlight, passagesFields: passagesFields, passagesCount: passagesCount, passagesCharacters: passagesCharacters, deduplicateField: deduplicateField, similar: similar, similarDocumentIds: similarDocumentIds, similarFields: similarFields);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/notices";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestFederatedQueryAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'matching_results': 15, 'results': [{'id': 'Id', 'metadata': {}, 'collection_id': 'CollectionId', 'result_metadata': {'score': 5, 'confidence': 10}}], 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'passages': [{'document_id': 'DocumentId', 'passage_score': 12, 'passage_text': 'PassageText', 'start_offset': 11, 'end_offset': 9, 'field': 'Field'}], 'duplicates_removed': 17, 'session_token': 'SessionToken', 'retrieval_details': {'document_retrieval_strategy': 'untrained'}, 'suggested_query': 'SuggestedQuery'}";
            var response = new DetailedResponse<QueryResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<QueryResponse>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            bool? xWatsonLoggingOptOut = true;

            request.As<QueryResponse>().Returns(Task.FromResult(response));

            var result = service.FederatedQuery(environmentId: environmentId, collectionIds: "testString", filter: "testString", query: "testString", naturalLanguageQuery: "testString", passages: true, aggregation: "testString", count: 38, _return: "testString", offset: 38, sort: "testString", highlight: true, passagesFields: "testString", passagesCount: 38, passagesCharacters: 38, deduplicate: true, deduplicateField: "testString", similar: true, similarDocumentIds: "testString", similarFields: "testString", bias: "testString", xWatsonLoggingOptOut: xWatsonLoggingOptOut);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/query";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestFederatedQueryNoticesAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'matching_results': 15, 'results': [{'id': 'Id', 'metadata': {}, 'collection_id': 'CollectionId', 'result_metadata': {'score': 5, 'confidence': 10}, 'code': 4, 'filename': 'Filename', 'file_type': 'pdf', 'sha1': 'Sha1', 'notices': [{'notice_id': 'NoticeId', 'document_id': 'DocumentId', 'query_id': 'QueryId', 'severity': 'warning', 'step': 'Step', 'description': 'Description'}]}], 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}]}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [{'key': 'Key', 'matching_results': 15, 'aggregations': []}], 'matching_results': 15, 'aggregations': [{'type': 'histogram', 'results': [], 'matching_results': 15, 'aggregations': [], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'field': 'Field', 'interval': 8}], 'passages': [{'document_id': 'DocumentId', 'passage_score': 12, 'passage_text': 'PassageText', 'start_offset': 11, 'end_offset': 9, 'field': 'Field'}], 'duplicates_removed': 17}";
            var response = new DetailedResponse<QueryNoticesResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<QueryNoticesResponse>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            List<string> collectionIds = new List<string> { "testString" };
            string filter = "testString";
            string query = "testString";
            string naturalLanguageQuery = "testString";
            string aggregation = "testString";
            long? count = 38;
            List<string> _return = new List<string> { "testString" };
            long? offset = 38;
            List<string> sort = new List<string> { "testString" };
            bool? highlight = true;
            string deduplicateField = "testString";
            bool? similar = true;
            List<string> similarDocumentIds = new List<string> { "testString" };
            List<string> similarFields = new List<string> { "testString" };

            request.As<QueryNoticesResponse>().Returns(Task.FromResult(response));

            var result = service.FederatedQueryNotices(environmentId: environmentId, collectionIds: collectionIds, filter: filter, query: query, naturalLanguageQuery: naturalLanguageQuery, aggregation: aggregation, count: count, _return: _return, offset: offset, sort: sort, highlight: highlight, deduplicateField: deduplicateField, similar: similar, similarDocumentIds: similarDocumentIds, similarFields: similarFields);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/notices";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetAutocompletionAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'completions': ['Completions']}";
            var response = new DetailedResponse<Completions>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Completions>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string collectionId = "testString";
            string prefix = "testString";
            string field = "testString";
            long? count = 38;

            request.As<Completions>().Returns(Task.FromResult(response));

            var result = service.GetAutocompletion(environmentId: environmentId, collectionId: collectionId, prefix: prefix, field: field, count: count);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/autocompletion";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListTrainingDataAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'environment_id': 'EnvironmentId', 'collection_id': 'CollectionId', 'queries': [{'query_id': 'QueryId', 'natural_language_query': 'NaturalLanguageQuery', 'filter': 'Filter', 'examples': [{'document_id': 'DocumentId', 'cross_reference': 'CrossReference', 'relevance': 9}]}]}";
            var response = new DetailedResponse<TrainingDataSet>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<TrainingDataSet>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string collectionId = "testString";

            request.As<TrainingDataSet>().Returns(Task.FromResult(response));

            var result = service.ListTrainingData(environmentId: environmentId, collectionId: collectionId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/training_data";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestAddTrainingDataAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'query_id': 'QueryId', 'natural_language_query': 'NaturalLanguageQuery', 'filter': 'Filter', 'examples': [{'document_id': 'DocumentId', 'cross_reference': 'CrossReference', 'relevance': 9}]}";
            var response = new DetailedResponse<TrainingQuery>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<TrainingQuery>(responseJson),
                StatusCode = 200
            };

            TrainingExample TrainingExampleModel = new TrainingExample()
            {
                DocumentId = "testString",
                CrossReference = "testString",
                Relevance = 38
            };
            string environmentId = "testString";
            string collectionId = "testString";

            request.As<TrainingQuery>().Returns(Task.FromResult(response));

            var result = service.AddTrainingData(environmentId: environmentId, collectionId: collectionId, naturalLanguageQuery: "testString", filter: "testString", examples: new List<TrainingExample> { TrainingExampleModel });

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/training_data";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteAllTrainingDataAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{}";
            var response = new DetailedResponse<object>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<object>(responseJson),
                StatusCode = 204
            };

            string environmentId = "testString";
            string collectionId = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteAllTrainingData(environmentId: environmentId, collectionId: collectionId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/training_data";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetTrainingDataAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'query_id': 'QueryId', 'natural_language_query': 'NaturalLanguageQuery', 'filter': 'Filter', 'examples': [{'document_id': 'DocumentId', 'cross_reference': 'CrossReference', 'relevance': 9}]}";
            var response = new DetailedResponse<TrainingQuery>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<TrainingQuery>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string collectionId = "testString";
            string queryId = "testString";

            request.As<TrainingQuery>().Returns(Task.FromResult(response));

            var result = service.GetTrainingData(environmentId: environmentId, collectionId: collectionId, queryId: queryId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteTrainingDataAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{}";
            var response = new DetailedResponse<object>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<object>(responseJson),
                StatusCode = 204
            };

            string environmentId = "testString";
            string collectionId = "testString";
            string queryId = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteTrainingData(environmentId: environmentId, collectionId: collectionId, queryId: queryId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListTrainingExamplesAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'examples': [{'document_id': 'DocumentId', 'cross_reference': 'CrossReference', 'relevance': 9}]}";
            var response = new DetailedResponse<TrainingExampleList>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<TrainingExampleList>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string collectionId = "testString";
            string queryId = "testString";

            request.As<TrainingExampleList>().Returns(Task.FromResult(response));

            var result = service.ListTrainingExamples(environmentId: environmentId, collectionId: collectionId, queryId: queryId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestCreateTrainingExampleAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'document_id': 'DocumentId', 'cross_reference': 'CrossReference', 'relevance': 9}";
            var response = new DetailedResponse<TrainingExample>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<TrainingExample>(responseJson),
                StatusCode = 201
            };

            string environmentId = "testString";
            string collectionId = "testString";
            string queryId = "testString";

            request.As<TrainingExample>().Returns(Task.FromResult(response));

            var result = service.CreateTrainingExample(environmentId: environmentId, collectionId: collectionId, queryId: queryId, documentId: "testString", crossReference: "testString", relevance: 38);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteTrainingExampleAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{}";
            var response = new DetailedResponse<object>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<object>(responseJson),
                StatusCode = 204
            };

            string environmentId = "testString";
            string collectionId = "testString";
            string queryId = "testString";
            string exampleId = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteTrainingExample(environmentId: environmentId, collectionId: collectionId, queryId: queryId, exampleId: exampleId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples/{exampleId}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestUpdateTrainingExampleAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'document_id': 'DocumentId', 'cross_reference': 'CrossReference', 'relevance': 9}";
            var response = new DetailedResponse<TrainingExample>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<TrainingExample>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string collectionId = "testString";
            string queryId = "testString";
            string exampleId = "testString";

            request.As<TrainingExample>().Returns(Task.FromResult(response));

            var result = service.UpdateTrainingExample(environmentId: environmentId, collectionId: collectionId, queryId: queryId, exampleId: exampleId, crossReference: "testString", relevance: 38);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples/{exampleId}";
            client.Received().PutAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetTrainingExampleAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'document_id': 'DocumentId', 'cross_reference': 'CrossReference', 'relevance': 9}";
            var response = new DetailedResponse<TrainingExample>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<TrainingExample>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string collectionId = "testString";
            string queryId = "testString";
            string exampleId = "testString";

            request.As<TrainingExample>().Returns(Task.FromResult(response));

            var result = service.GetTrainingExample(environmentId: environmentId, collectionId: collectionId, queryId: queryId, exampleId: exampleId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples/{exampleId}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteUserDataAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{}";
            var response = new DetailedResponse<object>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<object>(responseJson),
                StatusCode = 200
            };

            string customerId = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteUserData(customerId: customerId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/user_data";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestCreateEventAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'type': 'click', 'data': {'environment_id': 'EnvironmentId', 'session_token': 'SessionToken', 'display_rank': 11, 'collection_id': 'CollectionId', 'document_id': 'DocumentId', 'query_id': 'QueryId'}}";
            var response = new DetailedResponse<CreateEventResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<CreateEventResponse>(responseJson),
                StatusCode = 201
            };

            EventData EventDataModel = new EventData()
            {
                EnvironmentId = "testString",
                SessionToken = "testString",
                ClientTimestamp = new DateTime(2019, 1, 1),
                DisplayRank = 38,
                CollectionId = "testString",
                DocumentId = "testString",
            };

            request.As<CreateEventResponse>().Returns(Task.FromResult(response));

            var result = service.CreateEvent(type: "click", data: EventDataModel);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/events";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestQueryLogAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'matching_results': 15, 'results': [{'environment_id': 'EnvironmentId', 'customer_id': 'CustomerId', 'document_type': 'query', 'natural_language_query': 'NaturalLanguageQuery', 'document_results': {'results': [{'position': 8, 'document_id': 'DocumentId', 'score': 5, 'confidence': 10, 'collection_id': 'CollectionId'}], 'count': 5}, 'query_id': 'QueryId', 'session_token': 'SessionToken', 'collection_id': 'CollectionId', 'display_rank': 11, 'document_id': 'DocumentId', 'event_type': 'click', 'result_type': 'document'}]}";
            var response = new DetailedResponse<LogQueryResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<LogQueryResponse>(responseJson),
                StatusCode = 200
            };

            string filter = "testString";
            string query = "testString";
            long? count = 38;
            long? offset = 38;
            List<string> sort = new List<string> { "testString" };

            request.As<LogQueryResponse>().Returns(Task.FromResult(response));

            var result = service.QueryLog(filter: filter, query: query, count: count, offset: offset, sort: sort);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/logs";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetMetricsQueryAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'aggregations': [{'interval': 'Interval', 'event_type': 'EventType', 'results': [{'key': 3, 'matching_results': 15, 'event_rate': 9}]}]}";
            var response = new DetailedResponse<MetricResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<MetricResponse>(responseJson),
                StatusCode = 200
            };

            DateTime? startTime = new DateTime(2019, 1, 1);
            DateTime? endTime = new DateTime(2019, 1, 1);
            string resultType = "document";

            request.As<MetricResponse>().Returns(Task.FromResult(response));

            var result = service.GetMetricsQuery(startTime: startTime, endTime: endTime, resultType: resultType);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/metrics/number_of_queries";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetMetricsQueryEventAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'aggregations': [{'interval': 'Interval', 'event_type': 'EventType', 'results': [{'key': 3, 'matching_results': 15, 'event_rate': 9}]}]}";
            var response = new DetailedResponse<MetricResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<MetricResponse>(responseJson),
                StatusCode = 200
            };

            DateTime? startTime = new DateTime(2019, 1, 1);
            DateTime? endTime = new DateTime(2019, 1, 1);
            string resultType = "document";

            request.As<MetricResponse>().Returns(Task.FromResult(response));

            var result = service.GetMetricsQueryEvent(startTime: startTime, endTime: endTime, resultType: resultType);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/metrics/number_of_queries_with_event";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetMetricsQueryNoResultsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'aggregations': [{'interval': 'Interval', 'event_type': 'EventType', 'results': [{'key': 3, 'matching_results': 15, 'event_rate': 9}]}]}";
            var response = new DetailedResponse<MetricResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<MetricResponse>(responseJson),
                StatusCode = 200
            };

            DateTime? startTime = new DateTime(2019, 1, 1);
            DateTime? endTime = new DateTime(2019, 1, 1);
            string resultType = "document";

            request.As<MetricResponse>().Returns(Task.FromResult(response));

            var result = service.GetMetricsQueryNoResults(startTime: startTime, endTime: endTime, resultType: resultType);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/metrics/number_of_queries_with_no_search_results";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetMetricsEventRateAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'aggregations': [{'interval': 'Interval', 'event_type': 'EventType', 'results': [{'key': 3, 'matching_results': 15, 'event_rate': 9}]}]}";
            var response = new DetailedResponse<MetricResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<MetricResponse>(responseJson),
                StatusCode = 200
            };

            DateTime? startTime = new DateTime(2019, 1, 1);
            DateTime? endTime = new DateTime(2019, 1, 1);
            string resultType = "document";

            request.As<MetricResponse>().Returns(Task.FromResult(response));

            var result = service.GetMetricsEventRate(startTime: startTime, endTime: endTime, resultType: resultType);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/metrics/event_rate";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetMetricsQueryTokenEventAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'aggregations': [{'event_type': 'EventType', 'results': [{'key': 'Key', 'matching_results': 15, 'event_rate': 9}]}]}";
            var response = new DetailedResponse<MetricTokenResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<MetricTokenResponse>(responseJson),
                StatusCode = 200
            };

            long? count = 38;

            request.As<MetricTokenResponse>().Returns(Task.FromResult(response));

            var result = service.GetMetricsQueryTokenEvent(count: count);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/metrics/top_query_tokens_with_event_rate";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListCredentialsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'credentials': [{'credential_id': 'CredentialId', 'source_type': 'box', 'credential_details': {'credential_type': 'oauth2', 'client_id': 'ClientId', 'enterprise_id': 'EnterpriseId', 'url': 'Url', 'username': 'Username', 'organization_url': 'OrganizationUrl', 'site_collection.path': 'SiteCollectionPath', 'client_secret': 'ClientSecret', 'public_key_id': 'PublicKeyId', 'private_key': 'PrivateKey', 'passphrase': 'Passphrase', 'password': 'Password', 'gateway_id': 'GatewayId', 'source_version': 'online', 'web_application_url': 'WebApplicationUrl', 'domain': 'Domain', 'endpoint': 'Endpoint', 'access_key_id': 'AccessKeyId', 'secret_access_key': 'SecretAccessKey'}, 'status': 'connected'}]}";
            var response = new DetailedResponse<CredentialsList>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<CredentialsList>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";

            request.As<CredentialsList>().Returns(Task.FromResult(response));

            var result = service.ListCredentials(environmentId: environmentId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/credentials";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestCreateCredentialsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'credential_id': 'CredentialId', 'source_type': 'box', 'credential_details': {'credential_type': 'oauth2', 'client_id': 'ClientId', 'enterprise_id': 'EnterpriseId', 'url': 'Url', 'username': 'Username', 'organization_url': 'OrganizationUrl', 'site_collection.path': 'SiteCollectionPath', 'client_secret': 'ClientSecret', 'public_key_id': 'PublicKeyId', 'private_key': 'PrivateKey', 'passphrase': 'Passphrase', 'password': 'Password', 'gateway_id': 'GatewayId', 'source_version': 'online', 'web_application_url': 'WebApplicationUrl', 'domain': 'Domain', 'endpoint': 'Endpoint', 'access_key_id': 'AccessKeyId', 'secret_access_key': 'SecretAccessKey'}, 'status': 'connected'}";
            var response = new DetailedResponse<Credentials>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Credentials>(responseJson),
                StatusCode = 200
            };

            CredentialDetails CredentialDetailsModel = new CredentialDetails()
            {
                CredentialType = "oauth2",
                ClientId = "testString",
                EnterpriseId = "testString",
                Url = "testString",
                Username = "testString",
                OrganizationUrl = "testString",
                SiteCollectionPath = "testString",
                ClientSecret = "testString",
                PublicKeyId = "testString",
                PrivateKey = "testString",
                Passphrase = "testString",
                Password = "testString",
                GatewayId = "testString",
                SourceVersion = "online",
                WebApplicationUrl = "testString",
                Domain = "testString",
                Endpoint = "testString",
                AccessKeyId = "testString",
                SecretAccessKey = "testString"
            };
            string environmentId = "testString";

            request.As<Credentials>().Returns(Task.FromResult(response));

            var result = service.CreateCredentials(environmentId: environmentId, sourceType: "box", credentialDetails: CredentialDetailsModel, status: "connected");

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/credentials";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetCredentialsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'credential_id': 'CredentialId', 'source_type': 'box', 'credential_details': {'credential_type': 'oauth2', 'client_id': 'ClientId', 'enterprise_id': 'EnterpriseId', 'url': 'Url', 'username': 'Username', 'organization_url': 'OrganizationUrl', 'site_collection.path': 'SiteCollectionPath', 'client_secret': 'ClientSecret', 'public_key_id': 'PublicKeyId', 'private_key': 'PrivateKey', 'passphrase': 'Passphrase', 'password': 'Password', 'gateway_id': 'GatewayId', 'source_version': 'online', 'web_application_url': 'WebApplicationUrl', 'domain': 'Domain', 'endpoint': 'Endpoint', 'access_key_id': 'AccessKeyId', 'secret_access_key': 'SecretAccessKey'}, 'status': 'connected'}";
            var response = new DetailedResponse<Credentials>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Credentials>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string credentialId = "testString";

            request.As<Credentials>().Returns(Task.FromResult(response));

            var result = service.GetCredentials(environmentId: environmentId, credentialId: credentialId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/credentials/{credentialId}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestUpdateCredentialsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'credential_id': 'CredentialId', 'source_type': 'box', 'credential_details': {'credential_type': 'oauth2', 'client_id': 'ClientId', 'enterprise_id': 'EnterpriseId', 'url': 'Url', 'username': 'Username', 'organization_url': 'OrganizationUrl', 'site_collection.path': 'SiteCollectionPath', 'client_secret': 'ClientSecret', 'public_key_id': 'PublicKeyId', 'private_key': 'PrivateKey', 'passphrase': 'Passphrase', 'password': 'Password', 'gateway_id': 'GatewayId', 'source_version': 'online', 'web_application_url': 'WebApplicationUrl', 'domain': 'Domain', 'endpoint': 'Endpoint', 'access_key_id': 'AccessKeyId', 'secret_access_key': 'SecretAccessKey'}, 'status': 'connected'}";
            var response = new DetailedResponse<Credentials>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Credentials>(responseJson),
                StatusCode = 200
            };

            CredentialDetails CredentialDetailsModel = new CredentialDetails()
            {
                CredentialType = "oauth2",
                ClientId = "testString",
                EnterpriseId = "testString",
                Url = "testString",
                Username = "testString",
                OrganizationUrl = "testString",
                SiteCollectionPath = "testString",
                ClientSecret = "testString",
                PublicKeyId = "testString",
                PrivateKey = "testString",
                Passphrase = "testString",
                Password = "testString",
                GatewayId = "testString",
                SourceVersion = "online",
                WebApplicationUrl = "testString",
                Domain = "testString",
                Endpoint = "testString",
                AccessKeyId = "testString",
                SecretAccessKey = "testString"
            };
            string environmentId = "testString";
            string credentialId = "testString";

            request.As<Credentials>().Returns(Task.FromResult(response));

            var result = service.UpdateCredentials(environmentId: environmentId, credentialId: credentialId, sourceType: "box", credentialDetails: CredentialDetailsModel, status: "connected");

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/credentials/{credentialId}";
            client.Received().PutAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteCredentialsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'credential_id': 'CredentialId', 'status': 'deleted'}";
            var response = new DetailedResponse<DeleteCredentials>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<DeleteCredentials>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string credentialId = "testString";

            request.As<DeleteCredentials>().Returns(Task.FromResult(response));

            var result = service.DeleteCredentials(environmentId: environmentId, credentialId: credentialId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/credentials/{credentialId}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListGatewaysAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'gateways': [{'gateway_id': 'GatewayId', 'name': 'Name', 'status': 'connected', 'token': 'Token', 'token_id': 'TokenId'}]}";
            var response = new DetailedResponse<GatewayList>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<GatewayList>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";

            request.As<GatewayList>().Returns(Task.FromResult(response));

            var result = service.ListGateways(environmentId: environmentId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/gateways";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestCreateGatewayAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'gateway_id': 'GatewayId', 'name': 'Name', 'status': 'connected', 'token': 'Token', 'token_id': 'TokenId'}";
            var response = new DetailedResponse<Gateway>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Gateway>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";

            request.As<Gateway>().Returns(Task.FromResult(response));

            var result = service.CreateGateway(environmentId: environmentId, name: "testString");

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/gateways";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetGatewayAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'gateway_id': 'GatewayId', 'name': 'Name', 'status': 'connected', 'token': 'Token', 'token_id': 'TokenId'}";
            var response = new DetailedResponse<Gateway>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Gateway>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string gatewayId = "testString";

            request.As<Gateway>().Returns(Task.FromResult(response));

            var result = service.GetGateway(environmentId: environmentId, gatewayId: gatewayId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/gateways/{gatewayId}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteGatewayAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'gateway_id': 'GatewayId', 'status': 'Status'}";
            var response = new DetailedResponse<GatewayDelete>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<GatewayDelete>(responseJson),
                StatusCode = 200
            };

            string environmentId = "testString";
            string gatewayId = "testString";

            request.As<GatewayDelete>().Returns(Task.FromResult(response));

            var result = service.DeleteGateway(environmentId: environmentId, gatewayId: gatewayId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v1/environments/{environmentId}/gateways/{gatewayId}";
            client.Received().DeleteAsync(messageUrl);
        }

    }
}
