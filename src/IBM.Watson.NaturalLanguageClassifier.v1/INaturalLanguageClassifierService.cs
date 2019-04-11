/**
* Copyright 2018, 2019 IBM Corp. All Rights Reserved.
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
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using IBM.Watson.NaturalLanguageClassifier.v1.Model;

namespace IBM.Watson.NaturalLanguageClassifier.v1
{
    public partial interface INaturalLanguageClassifierService
    {
        DetailedResponse<Classification> Classify(string classifierId, string text);
        DetailedResponse<ClassificationCollection> ClassifyCollection(string classifierId, List<ClassifyInput> collection);
        DetailedResponse<Classifier> CreateClassifier(System.IO.MemoryStream metadata, System.IO.MemoryStream trainingData);
        DetailedResponse<object> DeleteClassifier(string classifierId);
        DetailedResponse<Classifier> GetClassifier(string classifierId);
        DetailedResponse<ClassifierList> ListClassifiers();
    }
}
