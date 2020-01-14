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
using IBM.Cloud.SDK.Core.Model;
using IBM.Cloud.SDK.Core.Http;
using IBM.Watson.VisualRecognition.v4.Model;

namespace IBM.Watson.VisualRecognition.v4
{
    public partial interface IVisualRecognitionService
    {
        DetailedResponse<AnalyzeResponse> Analyze(List<string> collectionIds, List<string> features, List<FileWithMetadata> imagesFile = null, List<string> imageUrl = null, float? threshold = null);
        DetailedResponse<Collection> CreateCollection(string name = null, string description = null);
        DetailedResponse<CollectionsList> ListCollections();
        DetailedResponse<Collection> GetCollection(string collectionId);
        DetailedResponse<Collection> UpdateCollection(string collectionId, string name = null, string description = null);
        DetailedResponse<object> DeleteCollection(string collectionId);
        DetailedResponse<ImageDetailsList> AddImages(string collectionId, List<FileWithMetadata> imagesFile = null, List<string> imageUrl = null, string trainingData = null);
        DetailedResponse<ImageSummaryList> ListImages(string collectionId);
        DetailedResponse<ImageDetails> GetImageDetails(string collectionId, string imageId);
        DetailedResponse<object> DeleteImage(string collectionId, string imageId);
        DetailedResponse<System.IO.MemoryStream> GetJpegImage(string collectionId, string imageId, string size = null);
        DetailedResponse<Collection> Train(string collectionId);
        DetailedResponse<TrainingDataObjects> AddImageTrainingData(string collectionId, string imageId, List<TrainingDataObject> objects = null);
        DetailedResponse<TrainingEvents> GetTrainingUsage(string startTime = null, string endTime = null);
        DetailedResponse<object> DeleteUserData(string customerId);
    }
}
