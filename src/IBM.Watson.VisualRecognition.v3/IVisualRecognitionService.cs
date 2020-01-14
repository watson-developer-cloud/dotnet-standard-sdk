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
using IBM.Cloud.SDK.Core.Http;
using IBM.Watson.VisualRecognition.v3.Model;

namespace IBM.Watson.VisualRecognition.v3
{
    public partial interface IVisualRecognitionService
    {
        DetailedResponse<ClassifiedImages> Classify(System.IO.MemoryStream imagesFile = null, string imagesFilename = null, string imagesFileContentType = null, string url = null, float? threshold = null, List<string> owners = null, List<string> classifierIds = null, string acceptLanguage = null);
        DetailedResponse<Classifier> CreateClassifier(string name, Dictionary<string, System.IO.MemoryStream> positiveExamples, System.IO.MemoryStream negativeExamples = null, string negativeExamplesFilename = null);
        DetailedResponse<Classifiers> ListClassifiers(bool? verbose = null);
        DetailedResponse<Classifier> GetClassifier(string classifierId);
        DetailedResponse<Classifier> UpdateClassifier(string classifierId, Dictionary<string, System.IO.MemoryStream> positiveExamples = null, System.IO.MemoryStream negativeExamples = null, string negativeExamplesFilename = null);
        DetailedResponse<object> DeleteClassifier(string classifierId);
        DetailedResponse<System.IO.MemoryStream> GetCoreMlModel(string classifierId);
        DetailedResponse<object> DeleteUserData(string customerId);
    }
}
