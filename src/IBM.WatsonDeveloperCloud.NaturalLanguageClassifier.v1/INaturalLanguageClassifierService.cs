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
using IBM.WatsonDeveloperCloud.NaturalLanguageClassifier.v1.Model;

namespace IBM.WatsonDeveloperCloud.NaturalLanguageClassifier.v1
{
    public interface INaturalLanguageClassifierService
    {
        Classification Classify(string classifierId, ClassifyInput body);

        ClassificationCollection ClassifyCollection(string classifierId, ClassifyCollectionInput body);
        Classifier CreateClassifier(System.IO.Stream metadata, System.IO.Stream trainingData);

        object DeleteClassifier(string classifierId);

        Classifier GetClassifier(string classifierId);

        ClassifierList ListClassifiers();
    }
}
