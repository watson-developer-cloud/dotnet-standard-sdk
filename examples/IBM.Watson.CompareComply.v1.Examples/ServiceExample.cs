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

using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.CompareComply.v1.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace IBM.Watson.CompareComply.v1.Examples
{
    public class ServiceExample
    {
        string apikey = "{apikey}";
        string url = "{url}";
        string versionDate = "{versionDate}";
        private string contractAFilePath = @"CompareComplyTestData/contract_A.pdf";
        private string contractBFilePath = @"CompareComplyTestData/contract_B.pdf";
        private string tableFilePath = @"CompareComplyTestData/TestTable.pdf";
        private static string objectStorageCredentialsInputFilepath;
        private static string objectStorageCredentialsOutputFilepath;
        private string feedbackId;
        private string batchId;

        static void Main(string[] args)
        {
            ServiceExample example = new ServiceExample();

            var parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.Parent.FullName;

            objectStorageCredentialsInputFilepath = parentDirectory + Path.DirectorySeparatorChar + "sdk-credentials" + Path.DirectorySeparatorChar + "cloud-object-storage-credentials-input.json";
            objectStorageCredentialsOutputFilepath = parentDirectory + Path.DirectorySeparatorChar + "sdk-credentials" + Path.DirectorySeparatorChar + "cloud-object-storage-credentials-output.json";

            example.ConvertToHtml();
            example.ClassifyElements();
            example.ExtractTables();
            example.CompareDocuments();

            example.ListFeedback();
            example.AddFeedback();
            example.GetFeedback();
            example.DeleteFeedback();

            example.ListBatches();
            example.CreateBatch();
            example.GetBatch();
            example.UpdateBatch();

            Console.WriteLine("Examples complete. Press any key to close the application.");
            Console.ReadKey();
        }

        #region HTML Conversion
        public void ConvertToHtml()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            CompareComplyService service = new CompareComplyService("2018-10-15", config);
            service.SetEndpoint("{url}");

            using (FileStream fs = File.OpenRead("{path-to-file}"))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    var result = service.ConvertToHtml(
                        file: ms,
                        filename: Path.GetFileName("{path-to-file}"),
                        fileContentType: "{file-content-type}"
                        );

                    Console.WriteLine(result.Response);
                }
            }
        }
        #endregion

        #region Element Classification
        public void ClassifyElements()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            CompareComplyService service = new CompareComplyService("2018-10-15", config);
            service.SetEndpoint("{url}");

            using (FileStream fs = File.OpenRead("{path-to-file}"))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    var result = service.ClassifyElements(
                        file: ms,
                        fileContentType: "{file-content-type}"
                        );

                    Console.WriteLine(result.Response);
                }
            }
        }
        #endregion

        #region Tables
        public void ExtractTables()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            CompareComplyService service = new CompareComplyService("2018-10-15", config);
            service.SetEndpoint("{url}");

            using (FileStream fs = File.OpenRead("{path-to-file}"))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    var result = service.ExtractTables(
                        file: ms,
                        fileContentType: "{file-content-type}"
                        );

                    Console.WriteLine(result.Response);
                }
            }
        }
        #endregion

        #region Comparision
        public void CompareDocuments()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            CompareComplyService service = new CompareComplyService("2018-10-15", config);
            service.SetEndpoint("{url}");

            using (FileStream fs0 = File.OpenRead("{path-to-file-1}"))
            {
                using (FileStream fs1 = File.OpenRead("{path-to-file-2}"))
                {
                    using (MemoryStream ms0 = new MemoryStream())
                    {
                        using (MemoryStream ms1 = new MemoryStream())
                        {
                            fs0.CopyTo(ms0);
                            fs1.CopyTo(ms1);
                            var result = service.CompareDocuments(
                                file1: ms0,
                                file2: ms1,
                                file1ContentType: "{file-1-content-type}",
                                file2ContentType: "{file-2-content-type}",
                                file1Label: "{file-1-label}",
                                file2Label: "{file-2-label}"
                                );

                            Console.WriteLine(result.Response);
                        }
                    }
                }
            }
        }
        #endregion

        #region Feedback
        public void ListFeedback()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            CompareComplyService service = new CompareComplyService("2018-10-15", config);
            service.SetEndpoint("{url}");

            var result = service.ListFeedback();

            Console.WriteLine(result.Response);
        }

        public void AddFeedback()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            CompareComplyService service = new CompareComplyService("2018-10-15", config);
            service.SetEndpoint("{url}");

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

            var result = service.AddFeedback(
                feedbackData: feedbackData,
                userId: "{user-id}",
                comment: "{user-comment}"
                );

            feedbackId = result.Result.FeedbackId;

            Console.WriteLine(result.Response);
        }

        public void GetFeedback()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            CompareComplyService service = new CompareComplyService("2018-10-15", config);
            service.SetEndpoint("{url}");

            var result = service.GetFeedback(
                feedbackId: feedbackId
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteFeedback()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            CompareComplyService service = new CompareComplyService("2018-10-15", config);
            service.SetEndpoint("{url}");

            var result = service.DeleteFeedback(
                feedbackId: "{feedback-id}"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Batches
        public void ListBatches()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            CompareComplyService service = new CompareComplyService("2018-10-15", config);
            service.SetEndpoint("{url}");

            var result = service.ListBatches();

            Console.WriteLine(result.Response);
        }

        public void CreateBatch()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            CompareComplyService service = new CompareComplyService("2018-10-15", config);
            service.SetEndpoint("{url}");

            using (FileStream fsInput = File.OpenRead("{object-storage-input-credentials-filepath}"))
            {
                using (FileStream fsOutput = File.OpenRead("{object-storage-output-credentials-filepath}"))
                {
                    using (MemoryStream msInput = new MemoryStream())
                    {
                        using (MemoryStream msOutput = new MemoryStream())
                        {
                            fsInput.CopyTo(msInput);
                            fsOutput.CopyTo(msOutput);
                            var result = service.CreateBatch(
                                function: "html_conversion",
                                inputCredentialsFile: msInput,
                                inputBucketLocation: "us-south",
                                inputBucketName: "bucket-input",
                                outputCredentialsFile: msOutput,
                                outputBucketLocation: "us-south",
                                outputBucketName: "bucket-output"
                                );

                            batchId = result.Result.BatchId;
                            Console.WriteLine(result.Response);
                        }
                    }
                }
            }
        }

        public void GetBatch()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            CompareComplyService service = new CompareComplyService("2018-10-15", config);
            service.SetEndpoint("{url}");

            var result = service.GetBatch(
                batchId: "{batch-id}"
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateBatch()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            CompareComplyService service = new CompareComplyService("2018-10-15", config);
            service.SetEndpoint("{url}");

            var result = service.UpdateBatch(
                batchId: "{batch-id}",
                action: "rescan"
                );

            Console.WriteLine(result.Response);
        }
        #endregion
    }
}
