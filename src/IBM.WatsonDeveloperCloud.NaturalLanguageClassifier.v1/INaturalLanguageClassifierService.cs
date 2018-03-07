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

using IBM.WatsonDeveloperCloud.NaturalLanguageClassifier.v1.Model;

namespace IBM.WatsonDeveloperCloud.NaturalLanguageClassifier.v1
{
    public interface INaturalLanguageClassifierService
    {
        /// <summary>
        /// Classify a phrase. Returns label information for the input. The status must be `Available` before you can use the classifier to classify text.
        /// </summary>
        /// <param name="classifierId">Classifier ID to use.</param>
        /// <param name="body">Phrase to classify. The maximum length of the text phrase is 1024 characters.</param>
        /// <returns><see cref="Classification" />Classification</returns>
        Classification Classify(string classifierId, ClassifyInput body);
        /// <summary>
        /// Create classifier. Sends data to create and train a classifier and returns information about the new classifier.
        /// </summary>
        /// <param name="metadata">Metadata in JSON format. The metadata identifies the language of the data, and an optional name to identify the classifier.</param>
        /// <param name="trainingData">Training data in CSV format. Each text value must have at least one class. The data can include up to 15,000 records. For details, see [Using your own data](https://console.bluemix.net/docs/services/natural-language-classifier/using-your-data.html).</param>
        /// <returns><see cref="Classifier" />Classifier</returns>
        Classifier CreateClassifier(System.IO.Stream metadata, System.IO.Stream trainingData);

        /// <summary>
        /// Delete classifier. 
        /// </summary>
        /// <param name="classifierId">Classifier ID to delete.</param>
        /// <returns><see cref="object" />object</returns>
        object DeleteClassifier(string classifierId);

        /// <summary>
        /// Get information about a classifier. Returns status and other information about a classifier.
        /// </summary>
        /// <param name="classifierId">Classifier ID to query.</param>
        /// <returns><see cref="Classifier" />Classifier</returns>
        Classifier GetClassifier(string classifierId);

        /// <summary>
        /// List classifiers. Returns an empty array if no classifiers are available.
        /// </summary>
        /// <returns><see cref="ClassifierList" />ClassifierList</returns>
        ClassifierList ListClassifiers();
    }
}
