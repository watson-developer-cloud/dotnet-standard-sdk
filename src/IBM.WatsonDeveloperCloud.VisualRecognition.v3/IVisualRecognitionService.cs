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

using IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model;
using System.Collections.Generic;

namespace IBM.WatsonDeveloperCloud.VisualRecognition.v3
{
    public partial interface IVisualRecognitionService
    {
        ClassifiedImages Classify(System.IO.Stream imagesFile = null, string acceptLanguage = null, string url = null, float? threshold = null, List<string> owners = null, List<string> classifierIds = null, string imagesFileContentType = null);
        DetectedFaces DetectFaces(System.IO.Stream imagesFile = null, string url = null, string imagesFileContentType = null);
        Classifier CreateClassifier(string name, System.IO.Stream classnamePositiveExamples, System.IO.Stream negativeExamples = null);

        object DeleteClassifier(string classifierId);

        Classifier GetClassifier(string classifierId);

        Classifiers ListClassifiers(bool? verbose = null);

        Classifier UpdateClassifier(string classifierId, System.IO.Stream classnamePositiveExamples = null, System.IO.Stream negativeExamples = null);
    }
}
