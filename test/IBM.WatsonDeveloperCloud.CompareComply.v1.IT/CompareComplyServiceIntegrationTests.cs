/**
* Copyright 2017 IBM Corp. All Rights Reserved.
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

using IBM.WatsonDeveloperCloud.CompareComply.v1.Model;
using IBM.WatsonDeveloperCloud.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace IBM.WatsonDeveloperCloud.CompareComply.v1.IT
{
    [TestClass]
    public class CompareComplyServiceIntegrationTests
    {
        private static string apikey;
        private static string password;
        private static string endpoint;
        private CompareComplyService service;
        private static string credentials = string.Empty;
        private readonly string versionDate = "2018-11-12";

        private string contract_a = @"CompareComplyTestData/contract_A.pdf";
        private string contract_b = @"CompareComplyTestData/contract_B.pdf";
        private string tableFilePath = @"CompareComplyTestData/TestTable.pdf";
        private string objectStorageCredentialsInputFilepath;
        private string objectStorageCredentialsOutputFilepath;

        [TestInitialize]
        public void Setup()
        {
            #region Get Credentials
            if (string.IsNullOrEmpty(credentials))
            {
                var parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName;
                string credentialsFilepath = parentDirectory + Path.DirectorySeparatorChar + "sdk-credentials" + Path.DirectorySeparatorChar + "credentials.json";

                objectStorageCredentialsInputFilepath = parentDirectory + Path.DirectorySeparatorChar + "sdk-credentials" + Path.DirectorySeparatorChar + "cloud-object-storage-credentials-input.json";
                objectStorageCredentialsOutputFilepath = parentDirectory + Path.DirectorySeparatorChar + "sdk-credentials" + Path.DirectorySeparatorChar + "cloud-object-storage-credentials-output.json";

                if (File.Exists(credentialsFilepath))
                {
                    try
                    {
                        credentials = File.ReadAllText(credentialsFilepath);
                        credentials = Utility.AddTopLevelObjectToJson(credentials, "VCAP_SERVICES");
                    }
                    catch (Exception e)
                    {
                        throw new Exception(string.Format("Failed to load credentials: {0}", e.Message));
                    }
                }
                else
                {
                    Console.WriteLine("Credentials file does not exist.");
                }

                VcapCredentials vcapCredentials = JsonConvert.DeserializeObject<VcapCredentials>(credentials);
                var vcapServices = JObject.Parse(credentials);

                Credential credential = vcapCredentials.GetCredentialByname("compare-comply-sdk")[0].Credentials;
                endpoint = credential.Url;
                apikey = credential.IamApikey;
            }
            #endregion

            TokenOptions tokenOptions = new TokenOptions()
            {
                ServiceUrl = endpoint,
                IamApiKey = apikey
            };

            service = new CompareComplyService(tokenOptions, versionDate);
        }

        #region HTML Conversion
        [TestMethod]
        public void HtmlConversion_Success()
        {
            using (FileStream fs = File.OpenRead(contract_a))
            {
                var htmlConversionResult = service.ConvertToHtml(fs);

                Assert.IsNotNull(htmlConversionResult);
                Assert.IsTrue(htmlConversionResult.Html != null);
                Assert.IsTrue(htmlConversionResult.Html.Contains("AGREEMENT BETWEEN OWNER AND CONTRACTOR"));
            }
        }
        #endregion

        #region Element Classification
        [TestMethod]
        public void ElementClassification_Success()
        {
            using (FileStream fs = File.OpenRead(contract_a))
            {
                var elementClassificationResult = service.ClassifyElements(fs);

                Assert.IsNotNull(elementClassificationResult);
                Assert.IsTrue(elementClassificationResult.Document.Title.Contains("Microsoft Word - contract_A.doc"));
            }
        }
        #endregion

        #region Tables
        [TestMethod]
        public void ExtractTables_Success()
        {
            using (FileStream fs = File.OpenRead(tableFilePath))
            {
                var extractTablesResult = service.ExtractTables(fs);

                Assert.IsNotNull(extractTablesResult);
                Assert.IsTrue(extractTablesResult.Document.Title.Contains("Untitled spreadsheet"));
            }
        }
        #endregion

        #region Comparision
        [TestMethod]
        public void Comparison_Success()
        {
            using (FileStream fs0 = File.OpenRead(contract_a))
            {
                using (FileStream fs1 = File.OpenRead(contract_b))
                {
                    var comparisonResults = service.CompareDocuments(fs0, fs1);

                    Assert.IsNotNull(comparisonResults);
                    Assert.IsTrue(comparisonResults.Documents[0].Title == "Microsoft Word - contract_A.doc");
                    Assert.IsTrue(comparisonResults.Documents[1].Title == "Microsoft Word - contract_B.doc");
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
                sort: "sort"
                );

            FeedbackInput feedbackData = new FeedbackInput()
            {
                UserId = "user_id_123x",
                Comment = "Test feedback comment",
                FeedbackData = new FeedbackDataInput()
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
                                Label = Category.LabelEnum.RESPONSIBILITIES,
                                ProvenanceIds = new List<string>(){ }
                            },
                            new Category()
                            {
                                Label = Category.LabelEnum.AMENDMENTS,
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
                                Label = Category.LabelEnum.RESPONSIBILITIES,
                            },
                            new Category()
                            {
                                Label = Category.LabelEnum.AUDITS
                            }
                        }
                    }
                }
            };
            var addFeedbackResult = service.AddFeedback(feedbackData);
            string feedbackId = addFeedbackResult.FeedbackId;

            //  temporary fix for a bug requiring `x-watson-metadata` header
            Dictionary<string, object> customData = new Dictionary<string, object>();
            Dictionary<string, string> customHeaders = new Dictionary<string, string>();
            customHeaders.Add("x-watson-metadata", "customer_id=125");
            customData.Add(Constants.CUSTOM_REQUEST_HEADERS, customHeaders);

            var getFeedbackResult = service.GetFeedback(feedbackId, customData:customData);

            var deleteFeedbackResult = service.DeleteFeedback(feedbackId);

            Assert.IsNotNull(deleteFeedbackResult);
            Assert.IsNotNull(getFeedbackResult);
            Assert.IsNotNull(addFeedbackResult);
            Assert.IsNotNull(ListFeedbackResult);
        }
        #endregion

        #region Batches
        [TestMethod]
        public void Batches_Success()
        {
            var getBatchesResult = service.ListBatches();
            string batchId = "";

            using (FileStream fsInput = File.OpenRead(objectStorageCredentialsInputFilepath))
            {
                using (FileStream fsOutput = File.OpenRead(objectStorageCredentialsOutputFilepath))
                {
                    var createBatchResult = service.CreateBatch(
                        "html_conversion",
                        fsInput,
                        "us-south",
                        "compare-comply-integration-test-bucket-input",
                        fsOutput,
                        "us-south",
                        "compare-comply-integration-test-bucket-output"
                        );
                    batchId = createBatchResult.BatchId;
                    Assert.IsNotNull(createBatchResult);
                    Assert.IsTrue(!string.IsNullOrEmpty(createBatchResult.BatchId));
                }
            }

            var getBatchResult = service.GetBatch(batchId);
            var updateBatchResult = service.UpdateBatch(batchId, "rescan");

            Assert.IsNotNull(updateBatchResult);
            Assert.IsNotNull(getBatchResult);
            Assert.IsTrue(getBatchResult.BatchId == batchId);
            Assert.IsTrue(getBatchesResult._Batches != null);
            Assert.IsNotNull(getBatchesResult);
        }
        #endregion
    }
}
