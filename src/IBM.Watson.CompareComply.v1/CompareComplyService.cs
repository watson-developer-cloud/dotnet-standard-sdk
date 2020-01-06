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

using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using IBM.Cloud.SDK.Core.Authentication;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Http.Extensions;
using IBM.Cloud.SDK.Core.Service;
using IBM.Watson.CompareComply.v1.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace IBM.Watson.CompareComply.v1
{
    public partial class CompareComplyService : IBMService, ICompareComplyService
    {
        const string defaultServiceName = "compare_comply";
        private const string defaultServiceUrl = "https://gateway.watsonplatform.net/compare-comply/api";
        public string VersionDate { get; set; }

        public CompareComplyService(string versionDate) : this(versionDate, ConfigBasedAuthenticatorFactory.GetAuthenticator(defaultServiceName)) { }
        public CompareComplyService(IClient httpClient) : base(defaultServiceName, httpClient) { }

        public CompareComplyService(string versionDate, IAuthenticator authenticator) : base(defaultServiceName, authenticator)
        {
            if (string.IsNullOrEmpty(versionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            VersionDate = versionDate;

            if (string.IsNullOrEmpty(ServiceUrl))
            {
                SetServiceUrl(defaultServiceUrl);
            }
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
        /// <returns><see cref="HTMLReturn" />HTMLReturn</returns>
        public DetailedResponse<HTMLReturn> ConvertToHtml(System.IO.MemoryStream file, string fileContentType = null, string model = null)
        {
            if (file == null)
            {
                throw new ArgumentNullException("`file` is required for `ConvertToHtml`");
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<HTMLReturn> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (file != null)
                {
                    var fileContent = new ByteArrayContent(file.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(fileContentType, out contentType);
                    fileContent.Headers.ContentType = contentType;
                    formData.Add(fileContent, "file", "filename");
                }

                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/html_conversion");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(model))
                {
                    restRequest.WithArgument("model", model);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("compare-comply", "v1", "ConvertToHtml"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<HTMLReturn>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<HTMLReturn>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for ConvertToHtml.
        /// </summary>
        public class ConvertToHtmlEnums
        {
            /// <summary>
            /// The content type of file.
            /// </summary>
            public class FileContentTypeValue
            {
                /// <summary>
                /// Constant APPLICATION_PDF for application/pdf
                /// </summary>
                public const string APPLICATION_PDF = "application/pdf";
                /// <summary>
                /// Constant APPLICATION_MSWORD for application/msword
                /// </summary>
                public const string APPLICATION_MSWORD = "application/msword";
                /// <summary>
                /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT for application/vnd.openxmlformats-officedocument.wordprocessingml.document
                /// </summary>
                public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                /// <summary>
                /// Constant IMAGE_BMP for image/bmp
                /// </summary>
                public const string IMAGE_BMP = "image/bmp";
                /// <summary>
                /// Constant IMAGE_GIF for image/gif
                /// </summary>
                public const string IMAGE_GIF = "image/gif";
                /// <summary>
                /// Constant IMAGE_JPEG for image/jpeg
                /// </summary>
                public const string IMAGE_JPEG = "image/jpeg";
                /// <summary>
                /// Constant IMAGE_PNG for image/png
                /// </summary>
                public const string IMAGE_PNG = "image/png";
                /// <summary>
                /// Constant IMAGE_TIFF for image/tiff
                /// </summary>
                public const string IMAGE_TIFF = "image/tiff";
                /// <summary>
                /// Constant TEXT_PLAIN for text/plain
                /// </summary>
                public const string TEXT_PLAIN = "text/plain";
                
            }
            /// <summary>
            /// The analysis model to be used by the service. For the **Element classification** and **Compare two
            /// documents** methods, the default is `contracts`. For the **Extract tables** method, the default is
            /// `tables`. These defaults apply to the standalone methods as well as to the methods' use in
            /// batch-processing requests.
            /// </summary>
            public class ModelValue
            {
                /// <summary>
                /// Constant CONTRACTS for contracts
                /// </summary>
                public const string CONTRACTS = "contracts";
                /// <summary>
                /// Constant TABLES for tables
                /// </summary>
                public const string TABLES = "tables";
                
            }
        }
        /// <summary>
        /// The content type of file.
        /// </summary>
        public class ConvertToHtmlFileContentTypeEnumValue
        {
            /// <summary>
            /// Constant APPLICATION_PDF for application/pdf
            /// </summary>
            public const string APPLICATION_PDF = "application/pdf";
            /// <summary>
            /// Constant APPLICATION_MSWORD for application/msword
            /// </summary>
            public const string APPLICATION_MSWORD = "application/msword";
            /// <summary>
            /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT for application/vnd.openxmlformats-officedocument.wordprocessingml.document
            /// </summary>
            public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            /// <summary>
            /// Constant IMAGE_BMP for image/bmp
            /// </summary>
            public const string IMAGE_BMP = "image/bmp";
            /// <summary>
            /// Constant IMAGE_GIF for image/gif
            /// </summary>
            public const string IMAGE_GIF = "image/gif";
            /// <summary>
            /// Constant IMAGE_JPEG for image/jpeg
            /// </summary>
            public const string IMAGE_JPEG = "image/jpeg";
            /// <summary>
            /// Constant IMAGE_PNG for image/png
            /// </summary>
            public const string IMAGE_PNG = "image/png";
            /// <summary>
            /// Constant IMAGE_TIFF for image/tiff
            /// </summary>
            public const string IMAGE_TIFF = "image/tiff";
            /// <summary>
            /// Constant TEXT_PLAIN for text/plain
            /// </summary>
            public const string TEXT_PLAIN = "text/plain";
            
        }
        /// <summary>
        /// The analysis model to be used by the service. For the **Element classification** and **Compare two
        /// documents** methods, the default is `contracts`. For the **Extract tables** method, the default is `tables`.
        /// These defaults apply to the standalone methods as well as to the methods' use in batch-processing requests.
        /// </summary>
        public class ConvertToHtmlModelEnumValue
        {
            /// <summary>
            /// Constant CONTRACTS for contracts
            /// </summary>
            public const string CONTRACTS = "contracts";
            /// <summary>
            /// Constant TABLES for tables
            /// </summary>
            public const string TABLES = "tables";
            
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
        /// <returns><see cref="ClassifyReturn" />ClassifyReturn</returns>
        public DetailedResponse<ClassifyReturn> ClassifyElements(System.IO.MemoryStream file, string fileContentType = null, string model = null)
        {
            if (file == null)
            {
                throw new ArgumentNullException("`file` is required for `ClassifyElements`");
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<ClassifyReturn> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (file != null)
                {
                    var fileContent = new ByteArrayContent(file.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(fileContentType, out contentType);
                    fileContent.Headers.ContentType = contentType;
                    formData.Add(fileContent, "file", "filename");
                }

                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/element_classification");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(model))
                {
                    restRequest.WithArgument("model", model);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("compare-comply", "v1", "ClassifyElements"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ClassifyReturn>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ClassifyReturn>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for ClassifyElements.
        /// </summary>
        public class ClassifyElementsEnums
        {
            /// <summary>
            /// The content type of file.
            /// </summary>
            public class FileContentTypeValue
            {
                /// <summary>
                /// Constant APPLICATION_PDF for application/pdf
                /// </summary>
                public const string APPLICATION_PDF = "application/pdf";
                /// <summary>
                /// Constant APPLICATION_MSWORD for application/msword
                /// </summary>
                public const string APPLICATION_MSWORD = "application/msword";
                /// <summary>
                /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT for application/vnd.openxmlformats-officedocument.wordprocessingml.document
                /// </summary>
                public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                /// <summary>
                /// Constant IMAGE_BMP for image/bmp
                /// </summary>
                public const string IMAGE_BMP = "image/bmp";
                /// <summary>
                /// Constant IMAGE_GIF for image/gif
                /// </summary>
                public const string IMAGE_GIF = "image/gif";
                /// <summary>
                /// Constant IMAGE_JPEG for image/jpeg
                /// </summary>
                public const string IMAGE_JPEG = "image/jpeg";
                /// <summary>
                /// Constant IMAGE_PNG for image/png
                /// </summary>
                public const string IMAGE_PNG = "image/png";
                /// <summary>
                /// Constant IMAGE_TIFF for image/tiff
                /// </summary>
                public const string IMAGE_TIFF = "image/tiff";
                
            }
            /// <summary>
            /// The analysis model to be used by the service. For the **Element classification** and **Compare two
            /// documents** methods, the default is `contracts`. For the **Extract tables** method, the default is
            /// `tables`. These defaults apply to the standalone methods as well as to the methods' use in
            /// batch-processing requests.
            /// </summary>
            public class ModelValue
            {
                /// <summary>
                /// Constant CONTRACTS for contracts
                /// </summary>
                public const string CONTRACTS = "contracts";
                /// <summary>
                /// Constant TABLES for tables
                /// </summary>
                public const string TABLES = "tables";
                
            }
        }
        /// <summary>
        /// The content type of file.
        /// </summary>
        public class ClassifyElementsFileContentTypeEnumValue
        {
            /// <summary>
            /// Constant APPLICATION_PDF for application/pdf
            /// </summary>
            public const string APPLICATION_PDF = "application/pdf";
            /// <summary>
            /// Constant APPLICATION_MSWORD for application/msword
            /// </summary>
            public const string APPLICATION_MSWORD = "application/msword";
            /// <summary>
            /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT for application/vnd.openxmlformats-officedocument.wordprocessingml.document
            /// </summary>
            public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            /// <summary>
            /// Constant IMAGE_BMP for image/bmp
            /// </summary>
            public const string IMAGE_BMP = "image/bmp";
            /// <summary>
            /// Constant IMAGE_GIF for image/gif
            /// </summary>
            public const string IMAGE_GIF = "image/gif";
            /// <summary>
            /// Constant IMAGE_JPEG for image/jpeg
            /// </summary>
            public const string IMAGE_JPEG = "image/jpeg";
            /// <summary>
            /// Constant IMAGE_PNG for image/png
            /// </summary>
            public const string IMAGE_PNG = "image/png";
            /// <summary>
            /// Constant IMAGE_TIFF for image/tiff
            /// </summary>
            public const string IMAGE_TIFF = "image/tiff";
            
        }
        /// <summary>
        /// The analysis model to be used by the service. For the **Element classification** and **Compare two
        /// documents** methods, the default is `contracts`. For the **Extract tables** method, the default is `tables`.
        /// These defaults apply to the standalone methods as well as to the methods' use in batch-processing requests.
        /// </summary>
        public class ClassifyElementsModelEnumValue
        {
            /// <summary>
            /// Constant CONTRACTS for contracts
            /// </summary>
            public const string CONTRACTS = "contracts";
            /// <summary>
            /// Constant TABLES for tables
            /// </summary>
            public const string TABLES = "tables";
            
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
        /// <returns><see cref="TableReturn" />TableReturn</returns>
        public DetailedResponse<TableReturn> ExtractTables(System.IO.MemoryStream file, string fileContentType = null, string model = null)
        {
            if (file == null)
            {
                throw new ArgumentNullException("`file` is required for `ExtractTables`");
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<TableReturn> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (file != null)
                {
                    var fileContent = new ByteArrayContent(file.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(fileContentType, out contentType);
                    fileContent.Headers.ContentType = contentType;
                    formData.Add(fileContent, "file", "filename");
                }

                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/tables");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(model))
                {
                    restRequest.WithArgument("model", model);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("compare-comply", "v1", "ExtractTables"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TableReturn>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TableReturn>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for ExtractTables.
        /// </summary>
        public class ExtractTablesEnums
        {
            /// <summary>
            /// The content type of file.
            /// </summary>
            public class FileContentTypeValue
            {
                /// <summary>
                /// Constant APPLICATION_PDF for application/pdf
                /// </summary>
                public const string APPLICATION_PDF = "application/pdf";
                /// <summary>
                /// Constant APPLICATION_MSWORD for application/msword
                /// </summary>
                public const string APPLICATION_MSWORD = "application/msword";
                /// <summary>
                /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT for application/vnd.openxmlformats-officedocument.wordprocessingml.document
                /// </summary>
                public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                /// <summary>
                /// Constant IMAGE_BMP for image/bmp
                /// </summary>
                public const string IMAGE_BMP = "image/bmp";
                /// <summary>
                /// Constant IMAGE_GIF for image/gif
                /// </summary>
                public const string IMAGE_GIF = "image/gif";
                /// <summary>
                /// Constant IMAGE_JPEG for image/jpeg
                /// </summary>
                public const string IMAGE_JPEG = "image/jpeg";
                /// <summary>
                /// Constant IMAGE_PNG for image/png
                /// </summary>
                public const string IMAGE_PNG = "image/png";
                /// <summary>
                /// Constant IMAGE_TIFF for image/tiff
                /// </summary>
                public const string IMAGE_TIFF = "image/tiff";
                /// <summary>
                /// Constant TEXT_PLAIN for text/plain
                /// </summary>
                public const string TEXT_PLAIN = "text/plain";
                
            }
            /// <summary>
            /// The analysis model to be used by the service. For the **Element classification** and **Compare two
            /// documents** methods, the default is `contracts`. For the **Extract tables** method, the default is
            /// `tables`. These defaults apply to the standalone methods as well as to the methods' use in
            /// batch-processing requests.
            /// </summary>
            public class ModelValue
            {
                /// <summary>
                /// Constant CONTRACTS for contracts
                /// </summary>
                public const string CONTRACTS = "contracts";
                /// <summary>
                /// Constant TABLES for tables
                /// </summary>
                public const string TABLES = "tables";
                
            }
        }
        /// <summary>
        /// The content type of file.
        /// </summary>
        public class ExtractTablesFileContentTypeEnumValue
        {
            /// <summary>
            /// Constant APPLICATION_PDF for application/pdf
            /// </summary>
            public const string APPLICATION_PDF = "application/pdf";
            /// <summary>
            /// Constant APPLICATION_MSWORD for application/msword
            /// </summary>
            public const string APPLICATION_MSWORD = "application/msword";
            /// <summary>
            /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT for application/vnd.openxmlformats-officedocument.wordprocessingml.document
            /// </summary>
            public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            /// <summary>
            /// Constant IMAGE_BMP for image/bmp
            /// </summary>
            public const string IMAGE_BMP = "image/bmp";
            /// <summary>
            /// Constant IMAGE_GIF for image/gif
            /// </summary>
            public const string IMAGE_GIF = "image/gif";
            /// <summary>
            /// Constant IMAGE_JPEG for image/jpeg
            /// </summary>
            public const string IMAGE_JPEG = "image/jpeg";
            /// <summary>
            /// Constant IMAGE_PNG for image/png
            /// </summary>
            public const string IMAGE_PNG = "image/png";
            /// <summary>
            /// Constant IMAGE_TIFF for image/tiff
            /// </summary>
            public const string IMAGE_TIFF = "image/tiff";
            /// <summary>
            /// Constant TEXT_PLAIN for text/plain
            /// </summary>
            public const string TEXT_PLAIN = "text/plain";
            
        }
        /// <summary>
        /// The analysis model to be used by the service. For the **Element classification** and **Compare two
        /// documents** methods, the default is `contracts`. For the **Extract tables** method, the default is `tables`.
        /// These defaults apply to the standalone methods as well as to the methods' use in batch-processing requests.
        /// </summary>
        public class ExtractTablesModelEnumValue
        {
            /// <summary>
            /// Constant CONTRACTS for contracts
            /// </summary>
            public const string CONTRACTS = "contracts";
            /// <summary>
            /// Constant TABLES for tables
            /// </summary>
            public const string TABLES = "tables";
            
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
        /// <returns><see cref="CompareReturn" />CompareReturn</returns>
        public DetailedResponse<CompareReturn> CompareDocuments(System.IO.MemoryStream file1, System.IO.MemoryStream file2, string file1ContentType = null, string file2ContentType = null, string file1Label = null, string file2Label = null, string model = null)
        {
            if (file1 == null)
            {
                throw new ArgumentNullException("`file1` is required for `CompareDocuments`");
            }
            if (file2 == null)
            {
                throw new ArgumentNullException("`file2` is required for `CompareDocuments`");
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<CompareReturn> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (file1 != null)
                {
                    var file1Content = new ByteArrayContent(file1.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(file1ContentType, out contentType);
                    file1Content.Headers.ContentType = contentType;
                    formData.Add(file1Content, "file_1", "filename");
                }

                if (file2 != null)
                {
                    var file2Content = new ByteArrayContent(file2.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(file2ContentType, out contentType);
                    file2Content.Headers.ContentType = contentType;
                    formData.Add(file2Content, "file_2", "filename");
                }

                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/comparison");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(file1Label))
                {
                    restRequest.WithArgument("file_1_label", file1Label);
                }
                if (!string.IsNullOrEmpty(file2Label))
                {
                    restRequest.WithArgument("file_2_label", file2Label);
                }
                if (!string.IsNullOrEmpty(model))
                {
                    restRequest.WithArgument("model", model);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("compare-comply", "v1", "CompareDocuments"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<CompareReturn>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<CompareReturn>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for CompareDocuments.
        /// </summary>
        public class CompareDocumentsEnums
        {
            /// <summary>
            /// The content type of file1.
            /// </summary>
            public class File1ContentTypeValue
            {
                /// <summary>
                /// Constant APPLICATION_PDF for application/pdf
                /// </summary>
                public const string APPLICATION_PDF = "application/pdf";
                /// <summary>
                /// Constant APPLICATION_JSON for application/json
                /// </summary>
                public const string APPLICATION_JSON = "application/json";
                /// <summary>
                /// Constant APPLICATION_MSWORD for application/msword
                /// </summary>
                public const string APPLICATION_MSWORD = "application/msword";
                /// <summary>
                /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT for application/vnd.openxmlformats-officedocument.wordprocessingml.document
                /// </summary>
                public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                /// <summary>
                /// Constant IMAGE_BMP for image/bmp
                /// </summary>
                public const string IMAGE_BMP = "image/bmp";
                /// <summary>
                /// Constant IMAGE_GIF for image/gif
                /// </summary>
                public const string IMAGE_GIF = "image/gif";
                /// <summary>
                /// Constant IMAGE_JPEG for image/jpeg
                /// </summary>
                public const string IMAGE_JPEG = "image/jpeg";
                /// <summary>
                /// Constant IMAGE_PNG for image/png
                /// </summary>
                public const string IMAGE_PNG = "image/png";
                /// <summary>
                /// Constant IMAGE_TIFF for image/tiff
                /// </summary>
                public const string IMAGE_TIFF = "image/tiff";
                
            }
            /// <summary>
            /// The content type of file2.
            /// </summary>
            public class File2ContentTypeValue
            {
                /// <summary>
                /// Constant APPLICATION_PDF for application/pdf
                /// </summary>
                public const string APPLICATION_PDF = "application/pdf";
                /// <summary>
                /// Constant APPLICATION_JSON for application/json
                /// </summary>
                public const string APPLICATION_JSON = "application/json";
                /// <summary>
                /// Constant APPLICATION_MSWORD for application/msword
                /// </summary>
                public const string APPLICATION_MSWORD = "application/msword";
                /// <summary>
                /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT for application/vnd.openxmlformats-officedocument.wordprocessingml.document
                /// </summary>
                public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                /// <summary>
                /// Constant IMAGE_BMP for image/bmp
                /// </summary>
                public const string IMAGE_BMP = "image/bmp";
                /// <summary>
                /// Constant IMAGE_GIF for image/gif
                /// </summary>
                public const string IMAGE_GIF = "image/gif";
                /// <summary>
                /// Constant IMAGE_JPEG for image/jpeg
                /// </summary>
                public const string IMAGE_JPEG = "image/jpeg";
                /// <summary>
                /// Constant IMAGE_PNG for image/png
                /// </summary>
                public const string IMAGE_PNG = "image/png";
                /// <summary>
                /// Constant IMAGE_TIFF for image/tiff
                /// </summary>
                public const string IMAGE_TIFF = "image/tiff";
                
            }
            /// <summary>
            /// The analysis model to be used by the service. For the **Element classification** and **Compare two
            /// documents** methods, the default is `contracts`. For the **Extract tables** method, the default is
            /// `tables`. These defaults apply to the standalone methods as well as to the methods' use in
            /// batch-processing requests.
            /// </summary>
            public class ModelValue
            {
                /// <summary>
                /// Constant CONTRACTS for contracts
                /// </summary>
                public const string CONTRACTS = "contracts";
                /// <summary>
                /// Constant TABLES for tables
                /// </summary>
                public const string TABLES = "tables";
                
            }
        }
        /// <summary>
        /// The content type of file1.
        /// </summary>
        public class CompareDocumentsFile1ContentTypeEnumValue
        {
            /// <summary>
            /// Constant APPLICATION_PDF for application/pdf
            /// </summary>
            public const string APPLICATION_PDF = "application/pdf";
            /// <summary>
            /// Constant APPLICATION_JSON for application/json
            /// </summary>
            public const string APPLICATION_JSON = "application/json";
            /// <summary>
            /// Constant APPLICATION_MSWORD for application/msword
            /// </summary>
            public const string APPLICATION_MSWORD = "application/msword";
            /// <summary>
            /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT for application/vnd.openxmlformats-officedocument.wordprocessingml.document
            /// </summary>
            public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            /// <summary>
            /// Constant IMAGE_BMP for image/bmp
            /// </summary>
            public const string IMAGE_BMP = "image/bmp";
            /// <summary>
            /// Constant IMAGE_GIF for image/gif
            /// </summary>
            public const string IMAGE_GIF = "image/gif";
            /// <summary>
            /// Constant IMAGE_JPEG for image/jpeg
            /// </summary>
            public const string IMAGE_JPEG = "image/jpeg";
            /// <summary>
            /// Constant IMAGE_PNG for image/png
            /// </summary>
            public const string IMAGE_PNG = "image/png";
            /// <summary>
            /// Constant IMAGE_TIFF for image/tiff
            /// </summary>
            public const string IMAGE_TIFF = "image/tiff";
            
        }
        /// <summary>
        /// The content type of file2.
        /// </summary>
        public class CompareDocumentsFile2ContentTypeEnumValue
        {
            /// <summary>
            /// Constant APPLICATION_PDF for application/pdf
            /// </summary>
            public const string APPLICATION_PDF = "application/pdf";
            /// <summary>
            /// Constant APPLICATION_JSON for application/json
            /// </summary>
            public const string APPLICATION_JSON = "application/json";
            /// <summary>
            /// Constant APPLICATION_MSWORD for application/msword
            /// </summary>
            public const string APPLICATION_MSWORD = "application/msword";
            /// <summary>
            /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT for application/vnd.openxmlformats-officedocument.wordprocessingml.document
            /// </summary>
            public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            /// <summary>
            /// Constant IMAGE_BMP for image/bmp
            /// </summary>
            public const string IMAGE_BMP = "image/bmp";
            /// <summary>
            /// Constant IMAGE_GIF for image/gif
            /// </summary>
            public const string IMAGE_GIF = "image/gif";
            /// <summary>
            /// Constant IMAGE_JPEG for image/jpeg
            /// </summary>
            public const string IMAGE_JPEG = "image/jpeg";
            /// <summary>
            /// Constant IMAGE_PNG for image/png
            /// </summary>
            public const string IMAGE_PNG = "image/png";
            /// <summary>
            /// Constant IMAGE_TIFF for image/tiff
            /// </summary>
            public const string IMAGE_TIFF = "image/tiff";
            
        }
        /// <summary>
        /// The analysis model to be used by the service. For the **Element classification** and **Compare two
        /// documents** methods, the default is `contracts`. For the **Extract tables** method, the default is `tables`.
        /// These defaults apply to the standalone methods as well as to the methods' use in batch-processing requests.
        /// </summary>
        public class CompareDocumentsModelEnumValue
        {
            /// <summary>
            /// Constant CONTRACTS for contracts
            /// </summary>
            public const string CONTRACTS = "contracts";
            /// <summary>
            /// Constant TABLES for tables
            /// </summary>
            public const string TABLES = "tables";
            
        }
        /// <summary>
        /// Add feedback.
        ///
        /// Adds feedback in the form of _labels_ from a subject-matter expert (SME) to a governing document.
        /// **Important:** Feedback is not immediately incorporated into the training model, nor is it guaranteed to be
        /// incorporated at a later date. Instead, submitted feedback is used to suggest future updates to the training
        /// model.
        /// </summary>
        /// <param name="feedbackData">Feedback data for submission.</param>
        /// <param name="userId">An optional string identifying the user. (optional)</param>
        /// <param name="comment">An optional comment on or description of the feedback. (optional)</param>
        /// <returns><see cref="FeedbackReturn" />FeedbackReturn</returns>
        public DetailedResponse<FeedbackReturn> AddFeedback(FeedbackDataInput feedbackData, string userId = null, string comment = null)
        {
            if (feedbackData == null)
            {
                throw new ArgumentNullException("`feedbackData` is required for `AddFeedback`");
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<FeedbackReturn> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/feedback");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (feedbackData != null)
                {
                    bodyObject["feedback_data"] = JToken.FromObject(feedbackData);
                }
                if (!string.IsNullOrEmpty(userId))
                {
                    bodyObject["user_id"] = userId;
                }
                if (!string.IsNullOrEmpty(comment))
                {
                    bodyObject["comment"] = comment;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("compare-comply", "v1", "AddFeedback"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<FeedbackReturn>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<FeedbackReturn>();
                }
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
        /// <param name="categoryRemoved">An optional string in the form of a comma-separated list of categories. If it
        /// is specified, the service filters the output to include only feedback that has at least one category from
        /// the list removed. (optional)</param>
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
        /// <returns><see cref="FeedbackList" />FeedbackList</returns>
        public DetailedResponse<FeedbackList> ListFeedback(string feedbackType = null, DateTime? before = null, DateTime? after = null, string documentTitle = null, string modelId = null, string modelVersion = null, string categoryRemoved = null, string categoryAdded = null, string categoryNotChanged = null, string typeRemoved = null, string typeAdded = null, string typeNotChanged = null, long? pageLimit = null, string cursor = null, string sort = null, bool? includeTotal = null)
        {

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<FeedbackList> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/feedback");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(feedbackType))
                {
                    restRequest.WithArgument("feedback_type", feedbackType);
                }
                if (before != null)
                {
                    restRequest.WithArgument("before", before?.ToString("yyyy-MM-dd"));
                }
                if (after != null)
                {
                    restRequest.WithArgument("after", after?.ToString("yyyy-MM-dd"));
                }
                if (!string.IsNullOrEmpty(documentTitle))
                {
                    restRequest.WithArgument("document_title", documentTitle);
                }
                if (!string.IsNullOrEmpty(modelId))
                {
                    restRequest.WithArgument("model_id", modelId);
                }
                if (!string.IsNullOrEmpty(modelVersion))
                {
                    restRequest.WithArgument("model_version", modelVersion);
                }
                if (!string.IsNullOrEmpty(categoryRemoved))
                {
                    restRequest.WithArgument("category_removed", categoryRemoved);
                }
                if (!string.IsNullOrEmpty(categoryAdded))
                {
                    restRequest.WithArgument("category_added", categoryAdded);
                }
                if (!string.IsNullOrEmpty(categoryNotChanged))
                {
                    restRequest.WithArgument("category_not_changed", categoryNotChanged);
                }
                if (!string.IsNullOrEmpty(typeRemoved))
                {
                    restRequest.WithArgument("type_removed", typeRemoved);
                }
                if (!string.IsNullOrEmpty(typeAdded))
                {
                    restRequest.WithArgument("type_added", typeAdded);
                }
                if (!string.IsNullOrEmpty(typeNotChanged))
                {
                    restRequest.WithArgument("type_not_changed", typeNotChanged);
                }
                if (pageLimit != null)
                {
                    restRequest.WithArgument("page_limit", pageLimit);
                }
                if (!string.IsNullOrEmpty(cursor))
                {
                    restRequest.WithArgument("cursor", cursor);
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    restRequest.WithArgument("sort", sort);
                }
                if (includeTotal != null)
                {
                    restRequest.WithArgument("include_total", includeTotal);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("compare-comply", "v1", "ListFeedback"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<FeedbackList>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<FeedbackList>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get a specified feedback entry.
        ///
        /// Gets a feedback entry with a specified `feedback_id`.
        /// </summary>
        /// <param name="feedbackId">A string that specifies the feedback entry to be included in the output.</param>
        /// <param name="model">The analysis model to be used by the service. For the **Element classification** and
        /// **Compare two documents** methods, the default is `contracts`. For the **Extract tables** method, the
        /// default is `tables`. These defaults apply to the standalone methods as well as to the methods' use in
        /// batch-processing requests. (optional)</param>
        /// <returns><see cref="GetFeedback" />GetFeedback</returns>
        public DetailedResponse<GetFeedback> GetFeedback(string feedbackId, string model = null)
        {
            if (string.IsNullOrEmpty(feedbackId))
            {
                throw new ArgumentNullException("`feedbackId` is required for `GetFeedback`");
            }
            else
            {
                feedbackId = Uri.EscapeDataString(feedbackId);
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<GetFeedback> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/feedback/{feedbackId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(model))
                {
                    restRequest.WithArgument("model", model);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("compare-comply", "v1", "GetFeedback"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<GetFeedback>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<GetFeedback>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for GetFeedback.
        /// </summary>
        public class GetFeedbackEnums
        {
            /// <summary>
            /// The analysis model to be used by the service. For the **Element classification** and **Compare two
            /// documents** methods, the default is `contracts`. For the **Extract tables** method, the default is
            /// `tables`. These defaults apply to the standalone methods as well as to the methods' use in
            /// batch-processing requests.
            /// </summary>
            public class ModelValue
            {
                /// <summary>
                /// Constant CONTRACTS for contracts
                /// </summary>
                public const string CONTRACTS = "contracts";
                /// <summary>
                /// Constant TABLES for tables
                /// </summary>
                public const string TABLES = "tables";
                
            }
        }

        /// <summary>
        /// The analysis model to be used by the service. For the **Element classification** and **Compare two
        /// documents** methods, the default is `contracts`. For the **Extract tables** method, the default is `tables`.
        /// These defaults apply to the standalone methods as well as to the methods' use in batch-processing requests.
        /// </summary>
        public class GetFeedbackModelEnumValue
        {
            /// <summary>
            /// Constant CONTRACTS for contracts
            /// </summary>
            public const string CONTRACTS = "contracts";
            /// <summary>
            /// Constant TABLES for tables
            /// </summary>
            public const string TABLES = "tables";
            
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
        /// <returns><see cref="FeedbackDeleted" />FeedbackDeleted</returns>
        public DetailedResponse<FeedbackDeleted> DeleteFeedback(string feedbackId, string model = null)
        {
            if (string.IsNullOrEmpty(feedbackId))
            {
                throw new ArgumentNullException("`feedbackId` is required for `DeleteFeedback`");
            }
            else
            {
                feedbackId = Uri.EscapeDataString(feedbackId);
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<FeedbackDeleted> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/feedback/{feedbackId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(model))
                {
                    restRequest.WithArgument("model", model);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("compare-comply", "v1", "DeleteFeedback"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<FeedbackDeleted>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<FeedbackDeleted>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for DeleteFeedback.
        /// </summary>
        public class DeleteFeedbackEnums
        {
            /// <summary>
            /// The analysis model to be used by the service. For the **Element classification** and **Compare two
            /// documents** methods, the default is `contracts`. For the **Extract tables** method, the default is
            /// `tables`. These defaults apply to the standalone methods as well as to the methods' use in
            /// batch-processing requests.
            /// </summary>
            public class ModelValue
            {
                /// <summary>
                /// Constant CONTRACTS for contracts
                /// </summary>
                public const string CONTRACTS = "contracts";
                /// <summary>
                /// Constant TABLES for tables
                /// </summary>
                public const string TABLES = "tables";
                
            }
        }
        /// <summary>
        /// The analysis model to be used by the service. For the **Element classification** and **Compare two
        /// documents** methods, the default is `contracts`. For the **Extract tables** method, the default is `tables`.
        /// These defaults apply to the standalone methods as well as to the methods' use in batch-processing requests.
        /// </summary>
        public class DeleteFeedbackModelEnumValue
        {
            /// <summary>
            /// Constant CONTRACTS for contracts
            /// </summary>
            public const string CONTRACTS = "contracts";
            /// <summary>
            /// Constant TABLES for tables
            /// </summary>
            public const string TABLES = "tables";
            
        }
        /// <summary>
        /// Submit a batch-processing request.
        ///
        /// Run Compare and Comply methods over a collection of input documents.
        ///
        /// **Important:** Batch processing requires the use of the [IBM Cloud Object Storage
        /// service](https://cloud.ibm.com/docs/services/cloud-object-storage?topic=cloud-object-storage-about#about-ibm-cloud-object-storage).
        /// The use of IBM Cloud Object Storage with Compare and Comply is discussed at [Using batch
        /// processing](https://cloud.ibm.com/docs/services/compare-comply?topic=compare-comply-batching#before-you-batch).
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
        /// <returns><see cref="BatchStatus" />BatchStatus</returns>
        public DetailedResponse<BatchStatus> CreateBatch(string function, System.IO.MemoryStream inputCredentialsFile, string inputBucketLocation, string inputBucketName, System.IO.MemoryStream outputCredentialsFile, string outputBucketLocation, string outputBucketName, string model = null)
        {
            if (string.IsNullOrEmpty(function))
            {
                throw new ArgumentNullException("`function` is required for `CreateBatch`");
            }
            if (inputCredentialsFile == null)
            {
                throw new ArgumentNullException("`inputCredentialsFile` is required for `CreateBatch`");
            }
            if (string.IsNullOrEmpty(inputBucketLocation))
            {
                throw new ArgumentNullException("`inputBucketLocation` is required for `CreateBatch`");
            }
            if (string.IsNullOrEmpty(inputBucketName))
            {
                throw new ArgumentNullException("`inputBucketName` is required for `CreateBatch`");
            }
            if (outputCredentialsFile == null)
            {
                throw new ArgumentNullException("`outputCredentialsFile` is required for `CreateBatch`");
            }
            if (string.IsNullOrEmpty(outputBucketLocation))
            {
                throw new ArgumentNullException("`outputBucketLocation` is required for `CreateBatch`");
            }
            if (string.IsNullOrEmpty(outputBucketName))
            {
                throw new ArgumentNullException("`outputBucketName` is required for `CreateBatch`");
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<BatchStatus> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (inputCredentialsFile != null)
                {
                    var inputCredentialsFileContent = new ByteArrayContent(inputCredentialsFile.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/json", out contentType);
                    inputCredentialsFileContent.Headers.ContentType = contentType;
                    formData.Add(inputCredentialsFileContent, "input_credentials_file", "filename");
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
                    var outputCredentialsFileContent = new ByteArrayContent(outputCredentialsFile.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/json", out contentType);
                    outputCredentialsFileContent.Headers.ContentType = contentType;
                    formData.Add(outputCredentialsFileContent, "output_credentials_file", "filename");
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
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/batches");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(function))
                {
                    restRequest.WithArgument("function", function);
                }
                if (!string.IsNullOrEmpty(model))
                {
                    restRequest.WithArgument("model", model);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("compare-comply", "v1", "CreateBatch"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<BatchStatus>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<BatchStatus>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for CreateBatch.
        /// </summary>
        public class CreateBatchEnums
        {
            /// <summary>
            /// The Compare and Comply method to run across the submitted input documents.
            /// </summary>
            public class FunctionValue
            {
                /// <summary>
                /// Constant HTML_CONVERSION for html_conversion
                /// </summary>
                public const string HTML_CONVERSION = "html_conversion";
                /// <summary>
                /// Constant ELEMENT_CLASSIFICATION for element_classification
                /// </summary>
                public const string ELEMENT_CLASSIFICATION = "element_classification";
                /// <summary>
                /// Constant TABLES for tables
                /// </summary>
                public const string TABLES = "tables";
                
            }
            /// <summary>
            /// The analysis model to be used by the service. For the **Element classification** and **Compare two
            /// documents** methods, the default is `contracts`. For the **Extract tables** method, the default is
            /// `tables`. These defaults apply to the standalone methods as well as to the methods' use in
            /// batch-processing requests.
            /// </summary>
            public class ModelValue
            {
                /// <summary>
                /// Constant CONTRACTS for contracts
                /// </summary>
                public const string CONTRACTS = "contracts";
                /// <summary>
                /// Constant TABLES for tables
                /// </summary>
                public const string TABLES = "tables";
                
            }
        }

        /// <summary>
        /// The Compare and Comply method to run across the submitted input documents.
        /// </summary>
        public class CreateBatchFunctionEnumValue
        {
            /// <summary>
            /// Constant HTML_CONVERSION for html_conversion
            /// </summary>
            public const string HTML_CONVERSION = "html_conversion";
            /// <summary>
            /// Constant ELEMENT_CLASSIFICATION for element_classification
            /// </summary>
            public const string ELEMENT_CLASSIFICATION = "element_classification";
            /// <summary>
            /// Constant TABLES for tables
            /// </summary>
            public const string TABLES = "tables";
            
        }
        /// <summary>
        /// The analysis model to be used by the service. For the **Element classification** and **Compare two
        /// documents** methods, the default is `contracts`. For the **Extract tables** method, the default is `tables`.
        /// These defaults apply to the standalone methods as well as to the methods' use in batch-processing requests.
        /// </summary>
        public class CreateBatchModelEnumValue
        {
            /// <summary>
            /// Constant CONTRACTS for contracts
            /// </summary>
            public const string CONTRACTS = "contracts";
            /// <summary>
            /// Constant TABLES for tables
            /// </summary>
            public const string TABLES = "tables";
            
        }
        /// <summary>
        /// List submitted batch-processing jobs.
        ///
        /// Lists batch-processing jobs submitted by users.
        /// </summary>
        /// <returns><see cref="Batches" />Batches</returns>
        public DetailedResponse<Batches> ListBatches()
        {

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<Batches> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/batches");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("compare-comply", "v1", "ListBatches"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Batches>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Batches>();
                }
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
        /// <returns><see cref="BatchStatus" />BatchStatus</returns>
        public DetailedResponse<BatchStatus> GetBatch(string batchId)
        {
            if (string.IsNullOrEmpty(batchId))
            {
                throw new ArgumentNullException("`batchId` is required for `GetBatch`");
            }
            else
            {
                batchId = Uri.EscapeDataString(batchId);
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<BatchStatus> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/batches/{batchId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("compare-comply", "v1", "GetBatch"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<BatchStatus>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<BatchStatus>();
                }
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
        /// <returns><see cref="BatchStatus" />BatchStatus</returns>
        public DetailedResponse<BatchStatus> UpdateBatch(string batchId, string action, string model = null)
        {
            if (string.IsNullOrEmpty(batchId))
            {
                throw new ArgumentNullException("`batchId` is required for `UpdateBatch`");
            }
            else
            {
                batchId = Uri.EscapeDataString(batchId);
            }
            if (string.IsNullOrEmpty(action))
            {
                throw new ArgumentNullException("`action` is required for `UpdateBatch`");
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<BatchStatus> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PutAsync($"{this.Endpoint}/v1/batches/{batchId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(action))
                {
                    restRequest.WithArgument("action", action);
                }
                if (!string.IsNullOrEmpty(model))
                {
                    restRequest.WithArgument("model", model);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("compare-comply", "v1", "UpdateBatch"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<BatchStatus>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<BatchStatus>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for UpdateBatch.
        /// </summary>
        public class UpdateBatchEnums
        {
            /// <summary>
            /// The action you want to perform on the specified batch-processing job.
            /// </summary>
            public class ActionValue
            {
                /// <summary>
                /// Constant RESCAN for rescan
                /// </summary>
                public const string RESCAN = "rescan";
                /// <summary>
                /// Constant CANCEL for cancel
                /// </summary>
                public const string CANCEL = "cancel";
                
            }
            /// <summary>
            /// The analysis model to be used by the service. For the **Element classification** and **Compare two
            /// documents** methods, the default is `contracts`. For the **Extract tables** method, the default is
            /// `tables`. These defaults apply to the standalone methods as well as to the methods' use in
            /// batch-processing requests.
            /// </summary>
            public class ModelValue
            {
                /// <summary>
                /// Constant CONTRACTS for contracts
                /// </summary>
                public const string CONTRACTS = "contracts";
                /// <summary>
                /// Constant TABLES for tables
                /// </summary>
                public const string TABLES = "tables";
                
            }
        }
        /// <summary>
        /// The action you want to perform on the specified batch-processing job.
        /// </summary>
        public class UpdateBatchActionEnumValue
        {
            /// <summary>
            /// Constant RESCAN for rescan
            /// </summary>
            public const string RESCAN = "rescan";
            /// <summary>
            /// Constant CANCEL for cancel
            /// </summary>
            public const string CANCEL = "cancel";
            
        }
        /// <summary>
        /// The analysis model to be used by the service. For the **Element classification** and **Compare two
        /// documents** methods, the default is `contracts`. For the **Extract tables** method, the default is `tables`.
        /// These defaults apply to the standalone methods as well as to the methods' use in batch-processing requests.
        /// </summary>
        public class UpdateBatchModelEnumValue
        {
            /// <summary>
            /// Constant CONTRACTS for contracts
            /// </summary>
            public const string CONTRACTS = "contracts";
            /// <summary>
            /// Constant TABLES for tables
            /// </summary>
            public const string TABLES = "tables";
            
        }
    }
}
