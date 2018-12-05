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

using System;
using System.Collections.Generic;
using IBM.WatsonDeveloperCloud.CompareComply.v1.Model;

namespace IBM.WatsonDeveloperCloud.CompareComply.v1
{
    public partial interface ICompareComplyService
    {
        HTMLReturn ConvertToHtml(System.IO.FileStream file, string modelId = null, string fileContentType = null, Dictionary<string, object> customData = null);
        ClassifyReturn ClassifyElements(System.IO.FileStream file, string modelId = null, string fileContentType = null, Dictionary<string, object> customData = null);
        TableReturn ExtractTables(System.IO.FileStream file, string modelId = null, string fileContentType = null, Dictionary<string, object> customData = null);
        CompareReturn CompareDocuments(System.IO.FileStream file1, System.IO.FileStream file2, string file1Label = null, string file2Label = null, string modelId = null, string file1ContentType = null, string file2ContentType = null, Dictionary<string, object> customData = null);
        FeedbackReturn AddFeedback(FeedbackInput feedbackData, Dictionary<string, object> customData = null);
        FeedbackDeleted DeleteFeedback(string feedbackId, string modelId = null, Dictionary<string, object> customData = null);
        GetFeedback GetFeedback(string feedbackId, string modelId = null, Dictionary<string, object> customData = null);
        FeedbackList ListFeedback(string feedbackType = null, DateTime? before = null, DateTime? after = null, string documentTitle = null, string modelId = null, string modelVersion = null, string categoryRemoved = null, string categoryAdded = null, string categoryNotChanged = null, string typeRemoved = null, string typeAdded = null, string typeNotChanged = null, long? pageLimit = null, string cursor = null, string sort = null, bool? includeTotal = null, Dictionary<string, object> customData = null);
        BatchStatus CreateBatch(string function, System.IO.FileStream inputCredentialsFile, string inputBucketLocation, string inputBucketName, System.IO.FileStream outputCredentialsFile, string outputBucketLocation, string outputBucketName, string modelId = null, Dictionary<string, object> customData = null);
        BatchStatus GetBatch(string batchId, Dictionary<string, object> customData = null);
        Batches ListBatches(Dictionary<string, object> customData = null);
        BatchStatus UpdateBatch(string batchId, string action, string modelId = null, Dictionary<string, object> customData = null);
    }
}
