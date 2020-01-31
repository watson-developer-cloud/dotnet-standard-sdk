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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;
using IBM.Cloud.SDK.Core.Authentication.NoAuth;
using System.Threading.Tasks;
using IBM.Watson.CompareComply.v1.Model;

namespace IBM.Watson.CompareComply.v1.UnitTests
{
    [TestClass]
    public class CompareComplyServiceUnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            CompareComplyService service = new CompareComplyService(httpClient: null);
        }

        [TestMethod]
        public void ConstructorHttpClient()
        {
            CompareComplyService service = new CompareComplyService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorExternalConfig()
        {
            var apikey = System.Environment.GetEnvironmentVariable("COMPARE_COMPLY_APIKEY");
            var url = System.Environment.GetEnvironmentVariable("COMPARE_COMPLY_URL");
            System.Environment.SetEnvironmentVariable("COMPARE_COMPLY_APIKEY", "apikey");
            System.Environment.SetEnvironmentVariable("COMPARE_COMPLY_URL", "http://www.url.com");
            CompareComplyService service = Substitute.For<CompareComplyService>("testString");
            Assert.IsNotNull(service);
            System.Environment.SetEnvironmentVariable("COMPARE_COMPLY_URL", url);
            System.Environment.SetEnvironmentVariable("COMPARE_COMPLY_APIKEY", apikey);
        }

        [TestMethod]
        public void Constructor()
        {
            CompareComplyService service = new CompareComplyService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorNoUrl()
        {
            var apikey = System.Environment.GetEnvironmentVariable("TEST_SERVICE_APIKEY");
            System.Environment.SetEnvironmentVariable("TEST_SERVICE_APIKEY", "apikey");
            CompareComplyService service = Substitute.For<CompareComplyService>("testString", "test_service");
            Assert.IsTrue(service.ServiceUrl == "https://gateway.watsonplatform.net/compare-comply/api");
            System.Environment.SetEnvironmentVariable("TEST_SERVICE_APIKEY", apikey);
        }
        #endregion

        [TestMethod]
        public void TestCategoryModel()
        {

            var CategoryProvenanceIds = new List<string> { "testString" };
            Category testRequestModel = new Category()
            {
                Label = "Amendments",
                ProvenanceIds = CategoryProvenanceIds
            };

            Assert.IsTrue(testRequestModel.Label == "Amendments");
            Assert.IsTrue(testRequestModel.ProvenanceIds == CategoryProvenanceIds);
        }

        [TestMethod]
        public void TestFeedbackDataInputModel()
        {
            var CategoryProvenanceIds = new List<string> { "testString" };
            Category CategoryModel = new Category()
            {
                Label = "Amendments",
                ProvenanceIds = CategoryProvenanceIds
            };

            Label LabelModel = new Label()
            {
                Nature = "testString",
                Party = "testString"
            };

            var TypeLabelProvenanceIds = new List<string> { "testString" };
            TypeLabel TypeLabelModel = new TypeLabel()
            {
                Label = LabelModel,
                ProvenanceIds = TypeLabelProvenanceIds
            };

            var UpdatedLabelsInTypes = new List<TypeLabel> { TypeLabelModel };
            var UpdatedLabelsInCategories = new List<Category> { CategoryModel };
            UpdatedLabelsIn UpdatedLabelsInModel = new UpdatedLabelsIn()
            {
                Types = UpdatedLabelsInTypes,
                Categories = UpdatedLabelsInCategories
            };

            var OriginalLabelsInTypes = new List<TypeLabel> { TypeLabelModel };
            var OriginalLabelsInCategories = new List<Category> { CategoryModel };
            OriginalLabelsIn OriginalLabelsInModel = new OriginalLabelsIn()
            {
                Types = OriginalLabelsInTypes,
                Categories = OriginalLabelsInCategories
            };

            Location LocationModel = new Location()
            {
                Begin = 26L,
                End = 26L
            };

            ShortDoc ShortDocModel = new ShortDoc()
            {
                Title = "testString",
                Hash = "testString"
            };

            FeedbackDataInput testRequestModel = new FeedbackDataInput()
            {
                FeedbackType = "testString",
                Document = ShortDocModel,
                ModelId = "testString",
                ModelVersion = "testString",
                Location = LocationModel,
                Text = "testString",
                OriginalLabels = OriginalLabelsInModel,
                UpdatedLabels = UpdatedLabelsInModel
            };

            Assert.IsTrue(testRequestModel.FeedbackType == "testString");
            Assert.IsTrue(testRequestModel.Document == ShortDocModel);
            Assert.IsTrue(testRequestModel.ModelId == "testString");
            Assert.IsTrue(testRequestModel.ModelVersion == "testString");
            Assert.IsTrue(testRequestModel.Location == LocationModel);
            Assert.IsTrue(testRequestModel.Text == "testString");
            Assert.IsTrue(testRequestModel.OriginalLabels == OriginalLabelsInModel);
            Assert.IsTrue(testRequestModel.UpdatedLabels == UpdatedLabelsInModel);
        }

        [TestMethod]
        public void TestLabelModel()
        {

            Label testRequestModel = new Label()
            {
                Nature = "testString",
                Party = "testString"
            };

            Assert.IsTrue(testRequestModel.Nature == "testString");
            Assert.IsTrue(testRequestModel.Party == "testString");
        }

        [TestMethod]
        public void TestLocationModel()
        {

            Location testRequestModel = new Location()
            {
                Begin = 26L,
                End = 26L
            };

            Assert.IsTrue(testRequestModel.Begin == 26L);
            Assert.IsTrue(testRequestModel.End == 26L);
        }

        [TestMethod]
        public void TestOriginalLabelsInModel()
        {
            var CategoryProvenanceIds = new List<string> { "testString" };
            Category CategoryModel = new Category()
            {
                Label = "Amendments",
                ProvenanceIds = CategoryProvenanceIds
            };

            Label LabelModel = new Label()
            {
                Nature = "testString",
                Party = "testString"
            };

            var TypeLabelProvenanceIds = new List<string> { "testString" };
            TypeLabel TypeLabelModel = new TypeLabel()
            {
                Label = LabelModel,
                ProvenanceIds = TypeLabelProvenanceIds
            };

            var OriginalLabelsInTypes = new List<TypeLabel> { TypeLabelModel };
            var OriginalLabelsInCategories = new List<Category> { CategoryModel };
            OriginalLabelsIn testRequestModel = new OriginalLabelsIn()
            {
                Types = OriginalLabelsInTypes,
                Categories = OriginalLabelsInCategories
            };

            Assert.IsTrue(testRequestModel.Types == OriginalLabelsInTypes);
            Assert.IsTrue(testRequestModel.Categories == OriginalLabelsInCategories);
        }

        [TestMethod]
        public void TestShortDocModel()
        {

            ShortDoc testRequestModel = new ShortDoc()
            {
                Title = "testString",
                Hash = "testString"
            };

            Assert.IsTrue(testRequestModel.Title == "testString");
            Assert.IsTrue(testRequestModel.Hash == "testString");
        }

        [TestMethod]
        public void TestTypeLabelModel()
        {
            Label LabelModel = new Label()
            {
                Nature = "testString",
                Party = "testString"
            };

            var TypeLabelProvenanceIds = new List<string> { "testString" };
            TypeLabel testRequestModel = new TypeLabel()
            {
                Label = LabelModel,
                ProvenanceIds = TypeLabelProvenanceIds
            };

            Assert.IsTrue(testRequestModel.Label == LabelModel);
            Assert.IsTrue(testRequestModel.ProvenanceIds == TypeLabelProvenanceIds);
        }

        [TestMethod]
        public void TestUpdatedLabelsInModel()
        {
            var CategoryProvenanceIds = new List<string> { "testString" };
            Category CategoryModel = new Category()
            {
                Label = "Amendments",
                ProvenanceIds = CategoryProvenanceIds
            };

            Label LabelModel = new Label()
            {
                Nature = "testString",
                Party = "testString"
            };

            var TypeLabelProvenanceIds = new List<string> { "testString" };
            TypeLabel TypeLabelModel = new TypeLabel()
            {
                Label = LabelModel,
                ProvenanceIds = TypeLabelProvenanceIds
            };

            var UpdatedLabelsInTypes = new List<TypeLabel> { TypeLabelModel };
            var UpdatedLabelsInCategories = new List<Category> { CategoryModel };
            UpdatedLabelsIn testRequestModel = new UpdatedLabelsIn()
            {
                Types = UpdatedLabelsInTypes,
                Categories = UpdatedLabelsInCategories
            };

            Assert.IsTrue(testRequestModel.Types == UpdatedLabelsInTypes);
            Assert.IsTrue(testRequestModel.Categories == UpdatedLabelsInCategories);
        }

        [TestMethod]
        public void TestTestConvertToHtmlAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            CompareComplyService service = new CompareComplyService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'num_pages': 'NumPages', 'author': 'Author', 'publication_date': 'PublicationDate', 'title': 'Title', 'html': 'Html'}";
            var response = new DetailedResponse<HTMLReturn>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<HTMLReturn>(responseJson),
                StatusCode = 200
            };

            System.IO.MemoryStream file = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file."));
            string fileContentType = "application/pdf";
            string model = "contracts";

            request.As<HTMLReturn>().Returns(Task.FromResult(response));

            var result = service.ConvertToHtml(file: file, fileContentType: fileContentType, model: model);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v1/html_conversion";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestClassifyElementsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            CompareComplyService service = new CompareComplyService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'document': {'title': 'Title', 'html': 'Html', 'hash': 'Hash', 'label': 'Label'}, 'model_id': 'ModelId', 'model_version': 'ModelVersion', 'elements': [{'location': {'begin': 5, 'end': 3}, 'text': 'Text', 'types': [{'label': {'nature': 'Nature', 'party': 'Party'}, 'provenance_ids': ['ProvenanceIds']}], 'categories': [{'label': 'Amendments', 'provenance_ids': ['ProvenanceIds']}], 'attributes': [{'type': 'Currency', 'text': 'Text', 'location': {'begin': 5, 'end': 3}}]}], 'effective_dates': [{'confidence_level': 'High', 'text': 'Text', 'text_normalized': 'TextNormalized', 'provenance_ids': ['ProvenanceIds'], 'location': {'begin': 5, 'end': 3}}], 'contract_amounts': [{'confidence_level': 'High', 'text': 'Text', 'text_normalized': 'TextNormalized', 'interpretation': {'value': 'Value', 'numeric_value': 12, 'unit': 'Unit'}, 'provenance_ids': ['ProvenanceIds'], 'location': {'begin': 5, 'end': 3}}], 'termination_dates': [{'confidence_level': 'High', 'text': 'Text', 'text_normalized': 'TextNormalized', 'provenance_ids': ['ProvenanceIds'], 'location': {'begin': 5, 'end': 3}}], 'contract_types': [{'confidence_level': 'High', 'text': 'Text', 'provenance_ids': ['ProvenanceIds'], 'location': {'begin': 5, 'end': 3}}], 'contract_terms': [{'confidence_level': 'High', 'text': 'Text', 'text_normalized': 'TextNormalized', 'interpretation': {'value': 'Value', 'numeric_value': 12, 'unit': 'Unit'}, 'provenance_ids': ['ProvenanceIds'], 'location': {'begin': 5, 'end': 3}}], 'payment_terms': [{'confidence_level': 'High', 'text': 'Text', 'text_normalized': 'TextNormalized', 'interpretation': {'value': 'Value', 'numeric_value': 12, 'unit': 'Unit'}, 'provenance_ids': ['ProvenanceIds'], 'location': {'begin': 5, 'end': 3}}], 'contract_currencies': [{'confidence_level': 'High', 'text': 'Text', 'text_normalized': 'TextNormalized', 'provenance_ids': ['ProvenanceIds'], 'location': {'begin': 5, 'end': 3}}], 'tables': [{'location': {'begin': 5, 'end': 3}, 'text': 'Text', 'section_title': {'text': 'Text', 'location': {'begin': 5, 'end': 3}}, 'title': {'location': {'begin': 5, 'end': 3}, 'text': 'Text'}, 'table_headers': [{'cell_id': 'CellId', 'location': 'unknown property type: Location', 'text': 'Text', 'row_index_begin': 13, 'row_index_end': 11, 'column_index_begin': 16, 'column_index_end': 14}], 'row_headers': [{'cell_id': 'CellId', 'location': {'begin': 5, 'end': 3}, 'text': 'Text', 'text_normalized': 'TextNormalized', 'row_index_begin': 13, 'row_index_end': 11, 'column_index_begin': 16, 'column_index_end': 14}], 'column_headers': [{'cell_id': 'CellId', 'location': 'unknown property type: Location', 'text': 'Text', 'text_normalized': 'TextNormalized', 'row_index_begin': 13, 'row_index_end': 11, 'column_index_begin': 16, 'column_index_end': 14}], 'body_cells': [{'cell_id': 'CellId', 'location': {'begin': 5, 'end': 3}, 'text': 'Text', 'row_index_begin': 13, 'row_index_end': 11, 'column_index_begin': 16, 'column_index_end': 14, 'row_header_ids': ['RowHeaderIds'], 'row_header_texts': ['RowHeaderTexts'], 'row_header_texts_normalized': ['RowHeaderTextsNormalized'], 'column_header_ids': ['ColumnHeaderIds'], 'column_header_texts': ['ColumnHeaderTexts'], 'column_header_texts_normalized': ['ColumnHeaderTextsNormalized'], 'attributes': [{'type': 'Currency', 'text': 'Text', 'location': {'begin': 5, 'end': 3}}]}], 'contexts': [{'text': 'Text', 'location': {'begin': 5, 'end': 3}}], 'key_value_pairs': [{'key': {'cell_id': 'CellId', 'location': {'begin': 5, 'end': 3}, 'text': 'Text'}, 'value': [{'cell_id': 'CellId', 'location': {'begin': 5, 'end': 3}, 'text': 'Text'}]}]}], 'document_structure': {'section_titles': [{'text': 'Text', 'location': {'begin': 5, 'end': 3}, 'level': 5, 'element_locations': [{'begin': 5, 'end': 3}]}], 'leading_sentences': [{'text': 'Text', 'location': {'begin': 5, 'end': 3}, 'element_locations': [{'begin': 5, 'end': 3}]}], 'paragraphs': [{'location': {'begin': 5, 'end': 3}}]}, 'parties': [{'party': 'Party', 'role': 'Role', 'importance': 'Primary', 'addresses': [{'text': 'Text', 'location': {'begin': 5, 'end': 3}}], 'contacts': [{'name': 'Name', 'role': 'Role'}], 'mentions': [{'text': 'Text', 'location': {'begin': 5, 'end': 3}}]}]}";
            var response = new DetailedResponse<ClassifyReturn>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<ClassifyReturn>(responseJson),
                StatusCode = 200
            };

            System.IO.MemoryStream file = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file."));
            string fileContentType = "application/pdf";
            string model = "contracts";

            request.As<ClassifyReturn>().Returns(Task.FromResult(response));

            var result = service.ClassifyElements(file: file, fileContentType: fileContentType, model: model);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v1/element_classification";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestExtractTablesAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            CompareComplyService service = new CompareComplyService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'document': {'html': 'Html', 'title': 'Title', 'hash': 'Hash'}, 'model_id': 'ModelId', 'model_version': 'ModelVersion', 'tables': [{'location': {'begin': 5, 'end': 3}, 'text': 'Text', 'section_title': {'text': 'Text', 'location': {'begin': 5, 'end': 3}}, 'title': {'location': {'begin': 5, 'end': 3}, 'text': 'Text'}, 'table_headers': [{'cell_id': 'CellId', 'location': 'unknown property type: Location', 'text': 'Text', 'row_index_begin': 13, 'row_index_end': 11, 'column_index_begin': 16, 'column_index_end': 14}], 'row_headers': [{'cell_id': 'CellId', 'location': {'begin': 5, 'end': 3}, 'text': 'Text', 'text_normalized': 'TextNormalized', 'row_index_begin': 13, 'row_index_end': 11, 'column_index_begin': 16, 'column_index_end': 14}], 'column_headers': [{'cell_id': 'CellId', 'location': 'unknown property type: Location', 'text': 'Text', 'text_normalized': 'TextNormalized', 'row_index_begin': 13, 'row_index_end': 11, 'column_index_begin': 16, 'column_index_end': 14}], 'body_cells': [{'cell_id': 'CellId', 'location': {'begin': 5, 'end': 3}, 'text': 'Text', 'row_index_begin': 13, 'row_index_end': 11, 'column_index_begin': 16, 'column_index_end': 14, 'row_header_ids': ['RowHeaderIds'], 'row_header_texts': ['RowHeaderTexts'], 'row_header_texts_normalized': ['RowHeaderTextsNormalized'], 'column_header_ids': ['ColumnHeaderIds'], 'column_header_texts': ['ColumnHeaderTexts'], 'column_header_texts_normalized': ['ColumnHeaderTextsNormalized'], 'attributes': [{'type': 'Currency', 'text': 'Text', 'location': {'begin': 5, 'end': 3}}]}], 'contexts': [{'text': 'Text', 'location': {'begin': 5, 'end': 3}}], 'key_value_pairs': [{'key': {'cell_id': 'CellId', 'location': {'begin': 5, 'end': 3}, 'text': 'Text'}, 'value': [{'cell_id': 'CellId', 'location': {'begin': 5, 'end': 3}, 'text': 'Text'}]}]}]}";
            var response = new DetailedResponse<TableReturn>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<TableReturn>(responseJson),
                StatusCode = 200
            };

            System.IO.MemoryStream file = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file."));
            string fileContentType = "application/pdf";
            string model = "contracts";

            request.As<TableReturn>().Returns(Task.FromResult(response));

            var result = service.ExtractTables(file: file, fileContentType: fileContentType, model: model);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v1/tables";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestCompareDocumentsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            CompareComplyService service = new CompareComplyService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'model_id': 'ModelId', 'model_version': 'ModelVersion', 'documents': [{'title': 'Title', 'html': 'Html', 'hash': 'Hash', 'label': 'Label'}], 'aligned_elements': [{'element_pair': [{'document_label': 'DocumentLabel', 'text': 'Text', 'location': {'begin': 5, 'end': 3}, 'types': [{'label': {'nature': 'Nature', 'party': 'Party'}}], 'categories': [{'label': 'Amendments'}], 'attributes': [{'type': 'Currency', 'text': 'Text', 'location': {'begin': 5, 'end': 3}}]}], 'identical_text': false, 'provenance_ids': ['ProvenanceIds'], 'significant_elements': false}], 'unaligned_elements': [{'document_label': 'DocumentLabel', 'location': {'begin': 5, 'end': 3}, 'text': 'Text', 'types': [{'label': {'nature': 'Nature', 'party': 'Party'}}], 'categories': [{'label': 'Amendments'}], 'attributes': [{'type': 'Currency', 'text': 'Text', 'location': {'begin': 5, 'end': 3}}]}]}";
            var response = new DetailedResponse<CompareReturn>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<CompareReturn>(responseJson),
                StatusCode = 200
            };

            System.IO.MemoryStream file1 = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file."));
            System.IO.MemoryStream file2 = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file."));
            string file1ContentType = "application/pdf";
            string file2ContentType = "application/pdf";
            string file1Label = "testString";
            string file2Label = "testString";
            string model = "contracts";

            request.As<CompareReturn>().Returns(Task.FromResult(response));

            var result = service.CompareDocuments(file1: file1, file2: file2, file1ContentType: file1ContentType, file2ContentType: file2ContentType, file1Label: file1Label, file2Label: file2Label, model: model);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v1/comparison";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestAddFeedbackAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            CompareComplyService service = new CompareComplyService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'feedback_id': 'FeedbackId', 'user_id': 'UserId', 'comment': 'Comment', 'feedback_data': {'feedback_type': 'FeedbackType', 'document': {'title': 'Title', 'hash': 'Hash'}, 'model_id': 'ModelId', 'model_version': 'ModelVersion', 'location': {'begin': 5, 'end': 3}, 'text': 'Text', 'original_labels': {'types': [{'label': {'nature': 'Nature', 'party': 'Party'}, 'provenance_ids': ['ProvenanceIds']}], 'categories': [{'label': 'Amendments', 'provenance_ids': ['ProvenanceIds']}], 'modification': 'added'}, 'updated_labels': {'types': [{'label': {'nature': 'Nature', 'party': 'Party'}, 'provenance_ids': ['ProvenanceIds']}], 'categories': [{'label': 'Amendments', 'provenance_ids': ['ProvenanceIds']}], 'modification': 'added'}, 'pagination': {'refresh_cursor': 'RefreshCursor', 'next_cursor': 'NextCursor', 'refresh_url': 'RefreshUrl', 'next_url': 'NextUrl', 'total': 5}}}";
            var response = new DetailedResponse<FeedbackReturn>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<FeedbackReturn>(responseJson),
                StatusCode = 200
            };

            var CategoryProvenanceIds = new List<string> { "testString" };
            Category CategoryModel = new Category()
            {
                Label = "Amendments",
                ProvenanceIds = CategoryProvenanceIds
            };

            Label LabelModel = new Label()
            {
                Nature = "testString",
                Party = "testString"
            };

            var TypeLabelProvenanceIds = new List<string> { "testString" };
            TypeLabel TypeLabelModel = new TypeLabel()
            {
                Label = LabelModel,
                ProvenanceIds = TypeLabelProvenanceIds
            };

            var UpdatedLabelsInTypes = new List<TypeLabel> { TypeLabelModel };
            var UpdatedLabelsInCategories = new List<Category> { CategoryModel };
            UpdatedLabelsIn UpdatedLabelsInModel = new UpdatedLabelsIn()
            {
                Types = UpdatedLabelsInTypes,
                Categories = UpdatedLabelsInCategories
            };

            var OriginalLabelsInTypes = new List<TypeLabel> { TypeLabelModel };
            var OriginalLabelsInCategories = new List<Category> { CategoryModel };
            OriginalLabelsIn OriginalLabelsInModel = new OriginalLabelsIn()
            {
                Types = OriginalLabelsInTypes,
                Categories = OriginalLabelsInCategories
            };

            Location LocationModel = new Location()
            {
                Begin = 26L,
                End = 26L
            };

            ShortDoc ShortDocModel = new ShortDoc()
            {
                Title = "testString",
                Hash = "testString"
            };

            FeedbackDataInput FeedbackDataInputModel = new FeedbackDataInput()
            {
                FeedbackType = "testString",
                Document = ShortDocModel,
                ModelId = "testString",
                ModelVersion = "testString",
                Location = LocationModel,
                Text = "testString",
                OriginalLabels = OriginalLabelsInModel,
                UpdatedLabels = UpdatedLabelsInModel
            };
            FeedbackDataInput feedbackData = FeedbackDataInputModel;
            string userId = "testString";
            string comment = "testString";

            request.As<FeedbackReturn>().Returns(Task.FromResult(response));

            var result = service.AddFeedback(feedbackData: feedbackData, userId: userId, comment: comment);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v1/feedback";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListFeedbackAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            CompareComplyService service = new CompareComplyService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'feedback': [{'feedback_id': 'FeedbackId', 'comment': 'Comment', 'feedback_data': {'feedback_type': 'FeedbackType', 'document': {'title': 'Title', 'hash': 'Hash'}, 'model_id': 'ModelId', 'model_version': 'ModelVersion', 'location': {'begin': 5, 'end': 3}, 'text': 'Text', 'original_labels': {'types': [{'label': {'nature': 'Nature', 'party': 'Party'}, 'provenance_ids': ['ProvenanceIds']}], 'categories': [{'label': 'Amendments', 'provenance_ids': ['ProvenanceIds']}], 'modification': 'added'}, 'updated_labels': {'types': [{'label': {'nature': 'Nature', 'party': 'Party'}, 'provenance_ids': ['ProvenanceIds']}], 'categories': [{'label': 'Amendments', 'provenance_ids': ['ProvenanceIds']}], 'modification': 'added'}, 'pagination': {'refresh_cursor': 'RefreshCursor', 'next_cursor': 'NextCursor', 'refresh_url': 'RefreshUrl', 'next_url': 'NextUrl', 'total': 5}}}]}";
            var response = new DetailedResponse<FeedbackList>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<FeedbackList>(responseJson),
                StatusCode = 200
            };

            string feedbackType = "testString";
            DateTime? before = new DateTime(2019, 1, 1);
            DateTime? after = new DateTime(2019, 1, 1);
            string documentTitle = "testString";
            string modelId = "testString";
            string modelVersion = "testString";
            string categoryRemoved = "testString";
            string categoryAdded = "testString";
            string categoryNotChanged = "testString";
            string typeRemoved = "testString";
            string typeAdded = "testString";
            string typeNotChanged = "testString";
            long? pageLimit = 38;
            string cursor = "testString";
            string sort = "testString";
            bool? includeTotal = true;

            request.As<FeedbackList>().Returns(Task.FromResult(response));

            var result = service.ListFeedback(feedbackType: feedbackType, before: before, after: after, documentTitle: documentTitle, modelId: modelId, modelVersion: modelVersion, categoryRemoved: categoryRemoved, categoryAdded: categoryAdded, categoryNotChanged: categoryNotChanged, typeRemoved: typeRemoved, typeAdded: typeAdded, typeNotChanged: typeNotChanged, pageLimit: pageLimit, cursor: cursor, sort: sort, includeTotal: includeTotal);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v1/feedback";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetFeedbackAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            CompareComplyService service = new CompareComplyService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'feedback_id': 'FeedbackId', 'comment': 'Comment', 'feedback_data': {'feedback_type': 'FeedbackType', 'document': {'title': 'Title', 'hash': 'Hash'}, 'model_id': 'ModelId', 'model_version': 'ModelVersion', 'location': {'begin': 5, 'end': 3}, 'text': 'Text', 'original_labels': {'types': [{'label': {'nature': 'Nature', 'party': 'Party'}, 'provenance_ids': ['ProvenanceIds']}], 'categories': [{'label': 'Amendments', 'provenance_ids': ['ProvenanceIds']}], 'modification': 'added'}, 'updated_labels': {'types': [{'label': {'nature': 'Nature', 'party': 'Party'}, 'provenance_ids': ['ProvenanceIds']}], 'categories': [{'label': 'Amendments', 'provenance_ids': ['ProvenanceIds']}], 'modification': 'added'}, 'pagination': {'refresh_cursor': 'RefreshCursor', 'next_cursor': 'NextCursor', 'refresh_url': 'RefreshUrl', 'next_url': 'NextUrl', 'total': 5}}}";
            var response = new DetailedResponse<GetFeedback>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<GetFeedback>(responseJson),
                StatusCode = 200
            };

            string feedbackId = "testString";
            string model = "contracts";

            request.As<GetFeedback>().Returns(Task.FromResult(response));

            var result = service.GetFeedback(feedbackId: feedbackId, model: model);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v1/feedback/{feedbackId}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteFeedbackAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            CompareComplyService service = new CompareComplyService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'status': 6, 'message': 'Message'}";
            var response = new DetailedResponse<FeedbackDeleted>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<FeedbackDeleted>(responseJson),
                StatusCode = 200
            };

            string feedbackId = "testString";
            string model = "contracts";

            request.As<FeedbackDeleted>().Returns(Task.FromResult(response));

            var result = service.DeleteFeedback(feedbackId: feedbackId, model: model);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v1/feedback/{feedbackId}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestCreateBatchAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            CompareComplyService service = new CompareComplyService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'function': 'element_classification', 'input_bucket_location': 'InputBucketLocation', 'input_bucket_name': 'InputBucketName', 'output_bucket_location': 'OutputBucketLocation', 'output_bucket_name': 'OutputBucketName', 'batch_id': 'BatchId', 'document_counts': {'total': 5, 'pending': 7, 'successful': 10, 'failed': 6}, 'status': 'Status'}";
            var response = new DetailedResponse<BatchStatus>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<BatchStatus>(responseJson),
                StatusCode = 200
            };

            string function = "html_conversion";
            System.IO.MemoryStream inputCredentialsFile = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file."));
            string inputBucketLocation = "testString";
            string inputBucketName = "testString";
            System.IO.MemoryStream outputCredentialsFile = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file."));
            string outputBucketLocation = "testString";
            string outputBucketName = "testString";
            string model = "contracts";

            request.As<BatchStatus>().Returns(Task.FromResult(response));

            var result = service.CreateBatch(function: function, inputCredentialsFile: inputCredentialsFile, inputBucketLocation: inputBucketLocation, inputBucketName: inputBucketName, outputCredentialsFile: outputCredentialsFile, outputBucketLocation: outputBucketLocation, outputBucketName: outputBucketName, model: model);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v1/batches";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListBatchesAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            CompareComplyService service = new CompareComplyService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'batches': [{'function': 'element_classification', 'input_bucket_location': 'InputBucketLocation', 'input_bucket_name': 'InputBucketName', 'output_bucket_location': 'OutputBucketLocation', 'output_bucket_name': 'OutputBucketName', 'batch_id': 'BatchId', 'document_counts': {'total': 5, 'pending': 7, 'successful': 10, 'failed': 6}, 'status': 'Status'}]}";
            var response = new DetailedResponse<Batches>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Batches>(responseJson),
                StatusCode = 200
            };


            request.As<Batches>().Returns(Task.FromResult(response));

            var result = service.ListBatches();

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v1/batches";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetBatchAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            CompareComplyService service = new CompareComplyService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'function': 'element_classification', 'input_bucket_location': 'InputBucketLocation', 'input_bucket_name': 'InputBucketName', 'output_bucket_location': 'OutputBucketLocation', 'output_bucket_name': 'OutputBucketName', 'batch_id': 'BatchId', 'document_counts': {'total': 5, 'pending': 7, 'successful': 10, 'failed': 6}, 'status': 'Status'}";
            var response = new DetailedResponse<BatchStatus>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<BatchStatus>(responseJson),
                StatusCode = 200
            };

            string batchId = "testString";

            request.As<BatchStatus>().Returns(Task.FromResult(response));

            var result = service.GetBatch(batchId: batchId);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v1/batches/{batchId}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestUpdateBatchAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                .Returns(request);

            CompareComplyService service = new CompareComplyService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'function': 'element_classification', 'input_bucket_location': 'InputBucketLocation', 'input_bucket_name': 'InputBucketName', 'output_bucket_location': 'OutputBucketLocation', 'output_bucket_name': 'OutputBucketName', 'batch_id': 'BatchId', 'document_counts': {'total': 5, 'pending': 7, 'successful': 10, 'failed': 6}, 'status': 'Status'}";
            var response = new DetailedResponse<BatchStatus>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<BatchStatus>(responseJson),
                StatusCode = 200
            };

            string batchId = "testString";
            string action = "rescan";
            string model = "contracts";

            request.As<BatchStatus>().Returns(Task.FromResult(response));

            var result = service.UpdateBatch(batchId: batchId, action: action, model: model);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v1/batches/{batchId}";
            client.Received().PutAsync(messageUrl);
        }

    }
}
