/**
* (C) Copyright IBM Corp. 2017, 2020.
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

using IBM.Watson.CompareComply.v1.Model;
using IBM.Cloud.SDK.Core.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace IBM.Watson.CompareComply.v1.IntegrationTests
{
    [TestClass]
    public class CompareComplyServiceIntegrationTests
    {
        private static string apikey;
        private static string endpoint;
        private CompareComplyService service;
        private static string credentials = string.Empty;
        private readonly string versionDate = "2019-06-03";

        private string contractAFilePath = @"CompareComplyTestData/contract_A.pdf";
        private string contractBFilePath = @"CompareComplyTestData/contract_B.pdf";
        private string tableFilePath = @"CompareComplyTestData/TestTable.png";
        private string objectStorageCredentialsInputFilepath;
        private string objectStorageCredentialsOutputFilepath;
        private string compareComplyModel = "contracts";
        private string extractTablesModel = "tables";

        [TestInitialize]
        public void Setup()
        {
            service = new CompareComplyService(versionDate);

            bool isWindows = System.Runtime.InteropServices.RuntimeInformation
                                               .IsOSPlatform(OSPlatform.Windows);
            if (isWindows)
            {
                objectStorageCredentialsInputFilepath = "C:\\projects\\sdk-credentials\\cloud-object-storage-credentials-input.json";
                objectStorageCredentialsOutputFilepath = "C:\\projects\\sdk-credentials\\cloud-object-storage-credentials-output.json";
            }
            else
            {
                // overwrite if you are using a different OS.
                objectStorageCredentialsInputFilepath = "C:\\projects\\sdk-credentials\\cloud-object-storage-credentials-input.json";
                objectStorageCredentialsOutputFilepath = "C:\\projects\\sdk-credentials\\cloud-object-storage-credentials-output.json";
            }
        }

        #region HTML Conversion
        [TestMethod]
        public void HtmlConversion_Success()
        {
            using (FileStream fs = File.OpenRead(contractAFilePath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    service.WithHeader("X-Watson-Test", "1");
                    var htmlConversionResult = service.ConvertToHtml(
                        file: ms,
                        fileContentType: "application/pdf",
                        model: compareComplyModel
                        );

                    Assert.IsNotNull(htmlConversionResult);
                    Assert.IsTrue(htmlConversionResult.Result.Html != null);
                    Assert.IsTrue(htmlConversionResult.Result.Html.Contains("AGREEMENT BETWEEN OWNER AND CONTRACTOR"));
                }
            }
        }
        #endregion

        #region Element Classification
        [TestMethod]
        public void ElementClassification_Success()
        {
            using (FileStream fs = File.OpenRead(contractAFilePath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    service.WithHeader("X-Watson-Test", "1");
                    var elementClassificationResult = service.ClassifyElements(
                        file: ms,
                        fileContentType: "application/pdf",
                        model: compareComplyModel
                        );

                    Assert.IsNotNull(elementClassificationResult);
                    Assert.IsTrue(elementClassificationResult.Result.Document.Title.Contains("Microsoft Word - contract_A.doc"));
                }
            }
        }
        #endregion

        #region Tables
        [TestMethod]
        public void ExtractTables_Success()
        {
            using (FileStream fs = File.OpenRead(tableFilePath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    service.WithHeader("X-Watson-Test", "1");
                    var extractTablesResult = service.ExtractTables(
                        file: ms,
                        fileContentType: "image/png",
                        model: extractTablesModel
                        );

                    Assert.IsNotNull(extractTablesResult);
                    Assert.IsTrue(extractTablesResult.Result.Document.Title == "no title");
                    Assert.IsTrue(extractTablesResult.Result.ModelId == "tables");
                    Assert.IsTrue(extractTablesResult.Result.Tables[0].BodyCells[0].RowHeaderIds[0] is string);
                }
            }
        }
        #endregion

        #region Comparision
        [TestMethod]
        public void Comparison_Success()
        {
            using (FileStream fs0 = File.OpenRead(contractAFilePath))
            {
                using (FileStream fs1 = File.OpenRead(contractBFilePath))
                {
                    using (MemoryStream ms0 = new MemoryStream())
                    {
                        using (MemoryStream ms1 = new MemoryStream())
                        {
                            fs0.CopyTo(ms0);
                            fs1.CopyTo(ms1);
                            service.WithHeader("X-Watson-Test", "1");
                            var comparisonResults = service.CompareDocuments(
                                file1: ms0,
                                file2: ms1,
                                file1ContentType: "application/pdf",
                                file2ContentType: "application/pdf",
                                file1Label: "Contract A",
                                file2Label: "Contract B",
                                model: compareComplyModel
                                );

                            Assert.IsNotNull(comparisonResults.Result);
                            Assert.IsTrue(comparisonResults.Result.Documents[0].Title == "Microsoft Word - contract_A.doc");
                            Assert.IsTrue(comparisonResults.Result.Documents[1].Title == "Microsoft Word - contract_B.doc");
                        }
                    }
                }
            }
        }
        #endregion

        #region Feedback
        [TestMethod]
        public void Feedback_Success()
        {
            DateTime before = DateTime.Now;
            DateTime after = new DateTime(2018, 11, 13);
            service.WithHeader("X-Watson-Test", "1");
            var ListFeedbackResult = service.ListFeedback(
                feedbackType: "element_classification",
                before: before,
                after: after,
                documentTitle: "doc title",
                modelId: "contracts",
                modelVersion: "11.00",
                categoryRemoved: "categoryRemoved",
                categoryAdded: "categoryAdded",
                categoryNotChanged: "categoryUnchanged",
                typeRemoved: "nature:party",
                typeAdded: "nature:party",
                typeNotChanged: "nature:party",
                pageLimit: 3,
                sort: "created"
                );

            string userId = "user_id_123x";
            string comment = "Test feedback comment";

            #region feedbackData
            FeedbackDataInput feedbackData = new FeedbackDataInput()
            {
                FeedbackType = "element_classification",
                Document = new ShortDoc()
                {
                    Hash = "",
                    Title = "doc title"
                },
                ModelId = "contracts",
                ModelVersion = "11.00",
                Location = new Location()
                {
                    Begin = 241,
                    End = 237
                },
                Text = "1. IBM will provide a Senior Managing Consultant / expert resource, for up to 80 hours, to assist Florida Power & Light (FPL) with the creation of an IT infrastructure unit cost model for existing infrastructure.",
                OriginalLabels = new OriginalLabelsIn()
                {
                    Types = new List<TypeLabel>()
                        {
                            new TypeLabel()
                            {
                                Label = new Label()
                                {
                                    Nature = "Obligation",
                                    Party= "IBM"
                                },
                                ProvenanceIds = new List<string>()
                                {
                                    "85f5981a-ba91-44f5-9efa-0bd22e64b7bc",
                                    "ce0480a1-5ef1-4c3e-9861-3743b5610795"
                                }
                            },
                            new TypeLabel()
                            {
                                Label = new Label()
                                {
                                    Nature = "End User",
                                    Party= "Exclusion"
                                },
                                ProvenanceIds = new List<string>()
                                {
                                    "85f5981a-ba91-44f5-9efa-0bd22e64b7bc",
                                    "ce0480a1-5ef1-4c3e-9861-3743b5610795"
                                }
                            }
                        },
                    Categories = new List<Category>()
                        {
                            new Category()
                            {
                                Label = Category.LabelEnumValue.RESPONSIBILITIES,
                                ProvenanceIds = new List<string>(){ }
                            },
                            new Category()
                            {
                                Label = Category.LabelEnumValue.AMENDMENTS,
                                ProvenanceIds = new List<string>(){ }
                            }
                        }
                },
                UpdatedLabels = new UpdatedLabelsIn()
                {
                    Types = new List<TypeLabel>()
                        {
                            new TypeLabel()
                            {
                                Label = new Label()
                                {
                                    Nature = "Obligation",
                                    Party = "IBM"
                                }
                            },
                            new TypeLabel()
                            {
                                Label = new Label()
                                {
                                    Nature = "Disclaimer",
                                    Party = "buyer"
                                }
                            }
                        },
                    Categories = new List<Category>()
                        {
                            new Category()
                            {
                                Label = Category.LabelEnumValue.RESPONSIBILITIES,
                            },
                            new Category()
                            {
                                Label = Category.LabelEnumValue.AUDITS
                            }
                        }
                }
            };
            #endregion

            service.WithHeader("X-Watson-Test", "1");
            var addFeedbackResult = service.AddFeedback(
                feedbackData: feedbackData,
                userId: userId,
                comment: comment
                );

            string feedbackId = addFeedbackResult.Result.FeedbackId;

            service.WithHeader("X-Watson-Test", "1");
            var getFeedbackResult = service.GetFeedback(
                feedbackId: feedbackId,
                model: compareComplyModel
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteFeedbackResult = service.DeleteFeedback(
                feedbackId: feedbackId
                );

            Assert.IsNotNull(deleteFeedbackResult.Result.Status);
            Assert.IsNotNull(getFeedbackResult.Result.FeedbackId);
            Assert.IsNotNull(addFeedbackResult.Result.FeedbackId);
            Assert.IsNotNull(ListFeedbackResult.Result.Feedback);
        }
        #endregion

        #region Batches
        [TestMethod]
        public void Batches_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var getBatchesResult = service.ListBatches();
            string batchId = "";

            using (FileStream fsInput = File.OpenRead(objectStorageCredentialsInputFilepath))
            {
                using (FileStream fsOutput = File.OpenRead(objectStorageCredentialsOutputFilepath))
                {
                    using (MemoryStream msInput = new MemoryStream())
                    {
                        using (MemoryStream msOutput = new MemoryStream())
                        {
                            fsInput.CopyTo(msInput);
                            fsOutput.CopyTo(msOutput);
                            service.WithHeader("X-Watson-Test", "1");
                            var createBatchResult = service.CreateBatch(
                                function: "html_conversion",
                                inputCredentialsFile: msInput,
                                inputBucketLocation: "us-south",
                                inputBucketName: "compare-comply-integration-test-bucket-input",
                                outputCredentialsFile: msOutput,
                                outputBucketLocation: "us-south",
                                outputBucketName: "compare-comply-integration-test-bucket-output"
                                );

                            batchId = createBatchResult.Result.BatchId;
                            Assert.IsNotNull(createBatchResult.Result);
                            Assert.IsTrue(!string.IsNullOrEmpty(createBatchResult.Result.BatchId));
                        }
                    }
                }
            }

            service.WithHeader("X-Watson-Test", "1");
            var getBatchResult = service.GetBatch(
                batchId: batchId
                );

            service.WithHeader("X-Watson-Test", "1");
            var updateBatchResult = service.UpdateBatch(
                batchId: batchId,
                action: "rescan",
                model: compareComplyModel
                );

            Assert.IsNotNull(updateBatchResult);
            Assert.IsNotNull(getBatchResult);
            Assert.IsTrue(getBatchResult.Result.BatchId == batchId);
            Assert.IsTrue(getBatchesResult.Result._Batches != null);
            Assert.IsNotNull(getBatchesResult);
        }
        #endregion
    }
}
