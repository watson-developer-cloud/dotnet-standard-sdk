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
using System.IO;
using System.Net.Http;
using System.Text;
using IBM.Cloud.SDK.Core;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Http.Extensions;
using IBM.Cloud.SDK.Core.Service;
using IBM.Cloud.SDK.Core.Util;
using IBM.Watson.CompareComply.v1.Model;
using Newtonsoft.Json;
using System;

namespace IBM.Watson.CompareComply.v1
{
    public partial class CompareComplyService : IBMService, ICompareComplyService
    {
        new const string SERVICE_NAME = "compare_comply";
        const string URL = "https://gateway.watsonplatform.net/compare-comply/api";
        private string _versionDate;
        public string VersionDate
        {
            get { return _versionDate; }
            set { _versionDate = value; }
        }

        public CompareComplyService() : base(SERVICE_NAME) { }
        
        public CompareComplyService(TokenOptions options, string versionDate) : base(SERVICE_NAME, URL)
        {
            if (string.IsNullOrEmpty(options.IamApiKey) && string.IsNullOrEmpty(options.IamAccessToken))
                throw new ArgumentNullException(nameof(options.IamAccessToken) + ", " + nameof(options.IamApiKey));
            if (string.IsNullOrEmpty(versionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            VersionDate = versionDate;

            if (!string.IsNullOrEmpty(options.ServiceUrl))
            {
                this.Endpoint = options.ServiceUrl;
            }
            else
            {
                options.ServiceUrl = this.Endpoint;
            }

            _tokenManager = new TokenManager(options);
        }

        public CompareComplyService(IClient httpClient) : base(SERVICE_NAME, URL)
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        /// <summary>
        /// Convert document to HTML.
        ///
        /// Converts a document to HTML.
        /// </summary>
        /// <param name="file">The document to convert.</param>
        /// <param name="fileContentType">The content type of file. (optional)</param>
        /// <param name="model">The analysis model to be used by the service. For the **Element classification** and
        /// **Compare two documents** methods, the default is `contracts`. For the **Extract tables** method, the
        /// default is `tables`. These defaults apply to the standalone methods as well as to the methods' use in
        /// batch-processing requests. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="HTMLReturn" />HTMLReturn</returns>
        public HTMLReturn ConvertToHtml(System.IO.FileStream file, string fileContentType = null, string model = null, Dictionary<string, object> customData = null)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            HTMLReturn result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (file != null)
                {
                    var fileContent = new ByteArrayContent((file as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(fileContentType, out contentType);
                    fileContent.Headers.ContentType = contentType;
                    formData.Add(fileContent, "file", file.Name);
                }

                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/html_conversion");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(model))
                    restRequest.WithArgument("model", model);
                restRequest.WithBodyContent(formData);
                if (customData != null)
                    restRequest.WithCustomData(customData);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("compare-comply", "v1", "ConvertToHtml"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<HTMLReturn>().Result;
                result.CustomData = restRequest.CustomData;
                if (result == null)
                    result = new HTMLReturn();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Classify the elements of a document.
        ///
        /// Analyzes the structural and semantic elements of a document.
        /// </summary>
        /// <param name="file">The document to classify.</param>
        /// <param name="fileContentType">The content type of file. (optional)</param>
        /// <param name="model">The analysis model to be used by the service. For the **Element classification** and
        /// **Compare two documents** methods, the default is `contracts`. For the **Extract tables** method, the
        /// default is `tables`. These defaults apply to the standalone methods as well as to the methods' use in
        /// batch-processing requests. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="ClassifyReturn" />ClassifyReturn</returns>
        public ClassifyReturn ClassifyElements(System.IO.FileStream file, string fileContentType = null, string model = null, Dictionary<string, object> customData = null)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            ClassifyReturn result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (file != null)
                {
                    var fileContent = new ByteArrayContent((file as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(fileContentType, out contentType);
                    fileContent.Headers.ContentType = contentType;
                    formData.Add(fileContent, "file", file.Name);
                }

                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/element_classification");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(model))
                    restRequest.WithArgument("model", model);
                restRequest.WithBodyContent(formData);
                if (customData != null)
                    restRequest.WithCustomData(customData);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("compare-comply", "v1", "ClassifyElements"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<ClassifyReturn>().Result;
                result.CustomData = restRequest.CustomData;
                if (result == null)
                    result = new ClassifyReturn();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Extract a document's tables.
        ///
        /// Analyzes the tables in a document.
        /// </summary>
        /// <param name="file">The document on which to run table extraction.</param>
        /// <param name="fileContentType">The content type of file. (optional)</param>
        /// <param name="model">The analysis model to be used by the service. For the **Element classification** and
        /// **Compare two documents** methods, the default is `contracts`. For the **Extract tables** method, the
        /// default is `tables`. These defaults apply to the standalone methods as well as to the methods' use in
        /// batch-processing requests. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="TableReturn" />TableReturn</returns>
        public TableReturn ExtractTables(System.IO.FileStream file, string fileContentType = null, string model = null, Dictionary<string, object> customData = null)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            TableReturn result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (file != null)
                {
                    var fileContent = new ByteArrayContent((file as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(fileContentType, out contentType);
                    fileContent.Headers.ContentType = contentType;
                    formData.Add(fileContent, "file", file.Name);
                }

                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/tables");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(model))
                    restRequest.WithArgument("model", model);
                restRequest.WithBodyContent(formData);
                if (customData != null)
                    restRequest.WithCustomData(customData);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("compare-comply", "v1", "ExtractTables"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<TableReturn>().Result;
                result.CustomData = restRequest.CustomData;
                if (result == null)
                    result = new TableReturn();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Compare two documents.
        ///
        /// Compares two input documents. Documents must be in the same format.
        /// </summary>
        /// <param name="file1">The first document to compare.</param>
        /// <param name="file2">The second document to compare.</param>
        /// <param name="file1ContentType">The content type of file1. (optional)</param>
        /// <param name="file2ContentType">The content type of file2. (optional)</param>
        /// <param name="file1Label">A text label for the first document. (optional, default to file_1)</param>
        /// <param name="file2Label">A text label for the second document. (optional, default to file_2)</param>
        /// <param name="model">The analysis model to be used by the service. For the **Element classification** and
        /// **Compare two documents** methods, the default is `contracts`. For the **Extract tables** method, the
        /// default is `tables`. These defaults apply to the standalone methods as well as to the methods' use in
        /// batch-processing requests. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="CompareReturn" />CompareReturn</returns>
        public CompareReturn CompareDocuments(System.IO.FileStream file1, System.IO.FileStream file2, string file1ContentType = null, string file2ContentType = null, string file1Label = null, string file2Label = null, string model = null, Dictionary<string, object> customData = null)
        {
            if (file1 == null)
                throw new ArgumentNullException(nameof(file1));
            if (file2 == null)
                throw new ArgumentNullException(nameof(file2));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            CompareReturn result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (file1 != null)
                {
                    var file1Content = new ByteArrayContent((file1 as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(file1ContentType, out contentType);
                    file1Content.Headers.ContentType = contentType;
                    formData.Add(file1Content, "file_1", file1.Name);
                }

                if (file2 != null)
                {
                    var file2Content = new ByteArrayContent((file2 as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(file2ContentType, out contentType);
                    file2Content.Headers.ContentType = contentType;
                    formData.Add(file2Content, "file_2", file2.Name);
                }

                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/comparison");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(file1Label))
                    restRequest.WithArgument("file_1_label", file1Label);
                if (!string.IsNullOrEmpty(file2Label))
                    restRequest.WithArgument("file_2_label", file2Label);
                if (!string.IsNullOrEmpty(model))
                    restRequest.WithArgument("model", model);
                restRequest.WithBodyContent(formData);
                if (customData != null)
                    restRequest.WithCustomData(customData);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("compare-comply", "v1", "CompareDocuments"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<CompareReturn>().Result;
                result.CustomData = restRequest.CustomData;
                if (result == null)
                    result = new CompareReturn();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Add feedback.
        ///
        /// Adds feedback in the form of _labels_ from a subject-matter expert (SME) to a governing document.
        /// **Important:** Feedback is not immediately incorporated into the training model, nor is it guaranteed to be
        /// incorporated at a later date. Instead, submitted feedback is used to suggest future updates to the training
        /// model.
        /// </summary>
        /// <param name="feedbackData">An object that defines the feedback to be submitted.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="FeedbackReturn" />FeedbackReturn</returns>
        public FeedbackReturn AddFeedback(FeedbackInput feedbackData, Dictionary<string, object> customData = null)
        {
            if (feedbackData == null)
                throw new ArgumentNullException(nameof(feedbackData));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            FeedbackReturn result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/feedback");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBody<FeedbackInput>(feedbackData);
                if (customData != null)
                    restRequest.WithCustomData(customData);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("compare-comply", "v1", "AddFeedback"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<FeedbackReturn>().Result;
                result.CustomData = restRequest.CustomData;
                if (result == null)
                    result = new FeedbackReturn();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete a specified feedback entry.
        ///
        /// Deletes a feedback entry with a specified `feedback_id`.
        /// </summary>
        /// <param name="feedbackId">A string that specifies the feedback entry to be deleted from the document.</param>
        /// <param name="model">The analysis model to be used by the service. For the **Element classification** and
        /// **Compare two documents** methods, the default is `contracts`. For the **Extract tables** method, the
        /// default is `tables`. These defaults apply to the standalone methods as well as to the methods' use in
        /// batch-processing requests. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="FeedbackDeleted" />FeedbackDeleted</returns>
        public FeedbackDeleted DeleteFeedback(string feedbackId, string model = null, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(feedbackId))
                throw new ArgumentNullException(nameof(feedbackId));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            FeedbackDeleted result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                
                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/feedback/{feedbackId}");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(model))
                    restRequest.WithArgument("model", model);
                if (customData != null)
                    restRequest.WithCustomData(customData);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("compare-comply", "v1", "DeleteFeedback"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<FeedbackDeleted>().Result;
                result.CustomData = restRequest.CustomData;
                if (result == null)
                    result = new FeedbackDeleted();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List a specified feedback entry.
        ///
        /// Lists a feedback entry with a specified `feedback_id`.
        /// </summary>
        /// <param name="feedbackId">A string that specifies the feedback entry to be included in the output.</param>
        /// <param name="model">The analysis model to be used by the service. For the **Element classification** and
        /// **Compare two documents** methods, the default is `contracts`. For the **Extract tables** method, the
        /// default is `tables`. These defaults apply to the standalone methods as well as to the methods' use in
        /// batch-processing requests. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="GetFeedback" />GetFeedback</returns>
        public GetFeedback GetFeedback(string feedbackId, string model = null, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(feedbackId))
                throw new ArgumentNullException(nameof(feedbackId));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            GetFeedback result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/feedback/{feedbackId}");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(model))
                    restRequest.WithArgument("model", model);
                if (customData != null)
                    restRequest.WithCustomData(customData);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("compare-comply", "v1", "GetFeedback"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<GetFeedback>().Result;
                result.CustomData = restRequest.CustomData;
                if (result == null)
                    result = new GetFeedback();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List the feedback in a document.
        ///
        /// Lists the feedback in a document.
        /// </summary>
        /// <param name="feedbackType">An optional string that filters the output to include only feedback with the
        /// specified feedback type. The only permitted value is `element_classification`. (optional)</param>
        /// <param name="before">An optional string in the format `YYYY-MM-DD` that filters the output to include only
        /// feedback that was added before the specified date. (optional)</param>
        /// <param name="after">An optional string in the format `YYYY-MM-DD` that filters the output to include only
        /// feedback that was added after the specified date. (optional)</param>
        /// <param name="documentTitle">An optional string that filters the output to include only feedback from the
        /// document with the specified `document_title`. (optional)</param>
        /// <param name="modelId">An optional string that filters the output to include only feedback with the specified
        /// `model_id`. The only permitted value is `contracts`. (optional)</param>
        /// <param name="modelVersion">An optional string that filters the output to include only feedback with the
        /// specified `model_version`. (optional)</param>
        /// <param name="categoryRemoved">An optional string in the form of a comma-separated list of categories. If
        /// this is specified, the service filters the output to include only feedback that has at least one category
        /// from the list removed. (optional)</param>
        /// <param name="categoryAdded">An optional string in the form of a comma-separated list of categories. If this
        /// is specified, the service filters the output to include only feedback that has at least one category from
        /// the list added. (optional)</param>
        /// <param name="categoryNotChanged">An optional string in the form of a comma-separated list of categories. If
        /// this is specified, the service filters the output to include only feedback that has at least one category
        /// from the list unchanged. (optional)</param>
        /// <param name="typeRemoved">An optional string of comma-separated `nature`:`party` pairs. If this is
        /// specified, the service filters the output to include only feedback that has at least one `nature`:`party`
        /// pair from the list removed. (optional)</param>
        /// <param name="typeAdded">An optional string of comma-separated `nature`:`party` pairs. If this is specified,
        /// the service filters the output to include only feedback that has at least one `nature`:`party` pair from the
        /// list removed. (optional)</param>
        /// <param name="typeNotChanged">An optional string of comma-separated `nature`:`party` pairs. If this is
        /// specified, the service filters the output to include only feedback that has at least one `nature`:`party`
        /// pair from the list unchanged. (optional)</param>
        /// <param name="pageLimit">An optional integer specifying the number of documents that you want the service to
        /// return. (optional)</param>
        /// <param name="cursor">An optional string that returns the set of documents after the previous set. Use this
        /// parameter with the `page_limit` parameter. (optional)</param>
        /// <param name="sort">An optional comma-separated list of fields in the document to sort on. You can optionally
        /// specify the sort direction by prefixing the value of the field with `-` for descending order or `+` for
        /// ascending order (the default). Currently permitted sorting fields are `created`, `user_id`, and
        /// `document_title`. (optional)</param>
        /// <param name="includeTotal">An optional boolean value. If specified as `true`, the `pagination` object in the
        /// output includes a value called `total` that gives the total count of feedback created. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="FeedbackList" />FeedbackList</returns>
        public FeedbackList ListFeedback(string feedbackType = null, DateTime? before = null, DateTime? after = null, string documentTitle = null, string modelId = null, string modelVersion = null, string categoryRemoved = null, string categoryAdded = null, string categoryNotChanged = null, string typeRemoved = null, string typeAdded = null, string typeNotChanged = null, long? pageLimit = null, string cursor = null, string sort = null, bool? includeTotal = null, Dictionary<string, object> customData = null)
        {

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            FeedbackList result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/feedback");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(feedbackType))
                    restRequest.WithArgument("feedback_type", feedbackType);
                if (before != null)
                    restRequest.WithArgument("before", before);
                if (after != null)
                    restRequest.WithArgument("after", after);
                if (!string.IsNullOrEmpty(documentTitle))
                    restRequest.WithArgument("document_title", documentTitle);
                if (!string.IsNullOrEmpty(modelId))
                    restRequest.WithArgument("model_id", modelId);
                if (!string.IsNullOrEmpty(modelVersion))
                    restRequest.WithArgument("model_version", modelVersion);
                if (!string.IsNullOrEmpty(categoryRemoved))
                    restRequest.WithArgument("category_removed", categoryRemoved);
                if (!string.IsNullOrEmpty(categoryAdded))
                    restRequest.WithArgument("category_added", categoryAdded);
                if (!string.IsNullOrEmpty(categoryNotChanged))
                    restRequest.WithArgument("category_not_changed", categoryNotChanged);
                if (!string.IsNullOrEmpty(typeRemoved))
                    restRequest.WithArgument("type_removed", typeRemoved);
                if (!string.IsNullOrEmpty(typeAdded))
                    restRequest.WithArgument("type_added", typeAdded);
                if (!string.IsNullOrEmpty(typeNotChanged))
                    restRequest.WithArgument("type_not_changed", typeNotChanged);
                if (pageLimit != null)
                    restRequest.WithArgument("page_limit", pageLimit);
                if (!string.IsNullOrEmpty(cursor))
                    restRequest.WithArgument("cursor", cursor);
                if (!string.IsNullOrEmpty(sort))
                    restRequest.WithArgument("sort", sort);
                if (includeTotal != null)
                    restRequest.WithArgument("include_total", includeTotal);
                if (customData != null)
                    restRequest.WithCustomData(customData);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("compare-comply", "v1", "ListFeedback"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<FeedbackList>().Result;
                result.CustomData = restRequest.CustomData;
                if (result == null)
                    result = new FeedbackList();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Submit a batch-processing request.
        ///
        /// Run Compare and Comply methods over a collection of input documents.
        /// **Important:** Batch processing requires the use of the [IBM Cloud Object Storage
        /// service](https://cloud.ibm.com/docs/services/cloud-object-storage/about-cos.html#about-ibm-cloud-object-storage).
        /// The use of IBM Cloud Object Storage with Compare and Comply is discussed at [Using batch
        /// processing](https://cloud.ibm.com/docs/services/compare-comply/batching.html#before-you-batch).
        /// </summary>
        /// <param name="function">The Compare and Comply method to run across the submitted input documents.</param>
        /// <param name="inputCredentialsFile">A JSON file containing the input Cloud Object Storage credentials. At a
        /// minimum, the credentials must enable `READ` permissions on the bucket defined by the `input_bucket_name`
        /// parameter.</param>
        /// <param name="inputBucketLocation">The geographical location of the Cloud Object Storage input bucket as
        /// listed on the **Endpoint** tab of your Cloud Object Storage instance; for example, `us-geo`, `eu-geo`, or
        /// `ap-geo`.</param>
        /// <param name="inputBucketName">The name of the Cloud Object Storage input bucket.</param>
        /// <param name="outputCredentialsFile">A JSON file that lists the Cloud Object Storage output credentials. At a
        /// minimum, the credentials must enable `READ` and `WRITE` permissions on the bucket defined by the
        /// `output_bucket_name` parameter.</param>
        /// <param name="outputBucketLocation">The geographical location of the Cloud Object Storage output bucket as
        /// listed on the **Endpoint** tab of your Cloud Object Storage instance; for example, `us-geo`, `eu-geo`, or
        /// `ap-geo`.</param>
        /// <param name="outputBucketName">The name of the Cloud Object Storage output bucket.</param>
        /// <param name="model">The analysis model to be used by the service. For the **Element classification** and
        /// **Compare two documents** methods, the default is `contracts`. For the **Extract tables** method, the
        /// default is `tables`. These defaults apply to the standalone methods as well as to the methods' use in
        /// batch-processing requests. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BatchStatus" />BatchStatus</returns>
        public BatchStatus CreateBatch(string function, System.IO.FileStream inputCredentialsFile, string inputBucketLocation, string inputBucketName, System.IO.FileStream outputCredentialsFile, string outputBucketLocation, string outputBucketName, string model = null, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(function))
                throw new ArgumentNullException(nameof(function));
            if (inputCredentialsFile == null)
                throw new ArgumentNullException(nameof(inputCredentialsFile));
            if (string.IsNullOrEmpty(inputBucketLocation))
                throw new ArgumentNullException(nameof(inputBucketLocation));
            if (string.IsNullOrEmpty(inputBucketName))
                throw new ArgumentNullException(nameof(inputBucketName));
            if (outputCredentialsFile == null)
                throw new ArgumentNullException(nameof(outputCredentialsFile));
            if (string.IsNullOrEmpty(outputBucketLocation))
                throw new ArgumentNullException(nameof(outputBucketLocation));
            if (string.IsNullOrEmpty(outputBucketName))
                throw new ArgumentNullException(nameof(outputBucketName));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            BatchStatus result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (inputCredentialsFile != null)
                {
                    var inputCredentialsFileContent = new ByteArrayContent((inputCredentialsFile as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/json", out contentType);
                    inputCredentialsFileContent.Headers.ContentType = contentType;
                    formData.Add(inputCredentialsFileContent, "input_credentials_file", inputCredentialsFile.Name);
                }

                if (inputBucketLocation != null)
                {
                    var inputBucketLocationContent = new StringContent(inputBucketLocation, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    inputBucketLocationContent.Headers.ContentType = null;
                    formData.Add(inputBucketLocationContent, "input_bucket_location");
                }

                if (inputBucketName != null)
                {
                    var inputBucketNameContent = new StringContent(inputBucketName, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    inputBucketNameContent.Headers.ContentType = null;
                    formData.Add(inputBucketNameContent, "input_bucket_name");
                }

                if (outputCredentialsFile != null)
                {
                    var outputCredentialsFileContent = new ByteArrayContent((outputCredentialsFile as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/json", out contentType);
                    outputCredentialsFileContent.Headers.ContentType = contentType;
                    formData.Add(outputCredentialsFileContent, "output_credentials_file", outputCredentialsFile.Name);
                }

                if (outputBucketLocation != null)
                {
                    var outputBucketLocationContent = new StringContent(outputBucketLocation, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    outputBucketLocationContent.Headers.ContentType = null;
                    formData.Add(outputBucketLocationContent, "output_bucket_location");
                }

                if (outputBucketName != null)
                {
                    var outputBucketNameContent = new StringContent(outputBucketName, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    outputBucketNameContent.Headers.ContentType = null;
                    formData.Add(outputBucketNameContent, "output_bucket_name");
                }

                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/batches");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(function))
                    restRequest.WithArgument("function", function);
                if (!string.IsNullOrEmpty(model))
                    restRequest.WithArgument("model", model);
                restRequest.WithBodyContent(formData);
                if (customData != null)
                    restRequest.WithCustomData(customData);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("compare-comply", "v1", "CreateBatch"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<BatchStatus>().Result;
                result.CustomData = restRequest.CustomData;
                if (result == null)
                    result = new BatchStatus();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get information about a specific batch-processing job.
        ///
        /// Gets information about a batch-processing job with a specified ID.
        /// </summary>
        /// <param name="batchId">The ID of the batch-processing job whose information you want to retrieve.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BatchStatus" />BatchStatus</returns>
        public BatchStatus GetBatch(string batchId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(batchId))
                throw new ArgumentNullException(nameof(batchId));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            BatchStatus result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/batches/{batchId}");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("compare-comply", "v1", "GetBatch"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<BatchStatus>().Result;
                result.CustomData = restRequest.CustomData;
                if (result == null)
                    result = new BatchStatus();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List submitted batch-processing jobs.
        ///
        /// Lists batch-processing jobs submitted by users.
        /// </summary>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Batches" />Batches</returns>
        public Batches ListBatches(Dictionary<string, object> customData = null)
        {

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Batches result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/batches");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("compare-comply", "v1", "ListBatches"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Batches>().Result;
                result.CustomData = restRequest.CustomData;
                if (result == null)
                    result = new Batches();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update a pending or active batch-processing job.
        ///
        /// Updates a pending or active batch-processing job. You can rescan the input bucket to check for new documents
        /// or cancel a job.
        /// </summary>
        /// <param name="batchId">The ID of the batch-processing job you want to update.</param>
        /// <param name="action">The action you want to perform on the specified batch-processing job.</param>
        /// <param name="model">The analysis model to be used by the service. For the **Element classification** and
        /// **Compare two documents** methods, the default is `contracts`. For the **Extract tables** method, the
        /// default is `tables`. These defaults apply to the standalone methods as well as to the methods' use in
        /// batch-processing requests. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BatchStatus" />BatchStatus</returns>
        public BatchStatus UpdateBatch(string batchId, string action, string model = null, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(batchId))
                throw new ArgumentNullException(nameof(batchId));
            if (string.IsNullOrEmpty(action))
                throw new ArgumentNullException(nameof(action));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            BatchStatus result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                
                var restRequest = client.PutAsync($"{this.Endpoint}/v1/batches/{batchId}");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(action))
                    restRequest.WithArgument("action", action);
                if (!string.IsNullOrEmpty(model))
                    restRequest.WithArgument("model", model);
                if (customData != null)
                    restRequest.WithCustomData(customData);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("compare-comply", "v1", "UpdateBatch"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<BatchStatus>().Result;
                result.CustomData = restRequest.CustomData;
                if (result == null)
                    result = new BatchStatus();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
