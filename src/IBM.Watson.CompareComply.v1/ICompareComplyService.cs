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
using System;
using IBM.Watson.CompareComply.v1.Model;

namespace IBM.Watson.CompareComply.v1
{
    public partial interface ICompareComplyService
    {
        DetailedResponse<HTMLReturn> ConvertToHtml(System.IO.MemoryStream file, string fileContentType = null, string model = null);
        DetailedResponse<ClassifyReturn> ClassifyElements(System.IO.MemoryStream file, string fileContentType = null, string model = null);
        DetailedResponse<TableReturn> ExtractTables(System.IO.MemoryStream file, string fileContentType = null, string model = null);
        DetailedResponse<CompareReturn> CompareDocuments(System.IO.MemoryStream file1, System.IO.MemoryStream file2, string file1ContentType = null, string file2ContentType = null, string file1Label = null, string file2Label = null, string model = null);
        DetailedResponse<FeedbackReturn> AddFeedback(FeedbackDataInput feedbackData, string userId = null, string comment = null);
        DetailedResponse<FeedbackList> ListFeedback(string feedbackType = null, DateTime? before = null, DateTime? after = null, string documentTitle = null, string modelId = null, string modelVersion = null, string categoryRemoved = null, string categoryAdded = null, string categoryNotChanged = null, string typeRemoved = null, string typeAdded = null, string typeNotChanged = null, long? pageLimit = null, string cursor = null, string sort = null, bool? includeTotal = null);
        DetailedResponse<GetFeedback> GetFeedback(string feedbackId, string model = null);
        DetailedResponse<FeedbackDeleted> DeleteFeedback(string feedbackId, string model = null);
        DetailedResponse<BatchStatus> CreateBatch(string function, System.IO.MemoryStream inputCredentialsFile, string inputBucketLocation, string inputBucketName, System.IO.MemoryStream outputCredentialsFile, string outputBucketLocation, string outputBucketName, string model = null);
        DetailedResponse<Batches> ListBatches();
        DetailedResponse<BatchStatus> GetBatch(string batchId);
        DetailedResponse<BatchStatus> UpdateBatch(string batchId, string action, string model = null);
    }
}
