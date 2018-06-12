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
    public partial interface INaturalLanguageClassifierService
    {
        /// <summary>
        /// Classify a phrase.
        ///
        /// Returns label information for the input. The status must be `Available` before you can use the classifier to
        /// classify text.
        /// </summary>
        /// <param name="classifierId">Classifier ID to use.</param>
        /// <param name="body">Phrase to classify. The maximum length of the text phrase is 1024 characters.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Classification" />Classification</returns>
        Classification Classify(string classifierId, ClassifyInput body, Dictionary<string, object> customData = null);
        /// <summary>
        /// Classify multiple phrases.
        ///
        /// Returns label information for multiple phrases. The status must be `Available` before you can use the
        /// classifier to classify text.
        ///
        /// Note that classifying Japanese texts is a beta feature.
        /// </summary>
        /// <param name="classifierId">Classifier ID to use.</param>
        /// <param name="body">Phrase to classify.
        ///
        /// The maximum length of the text phrase is 1024 characters. You can submit up to 30 text phrases in a
        /// request.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="ClassificationCollection" />ClassificationCollection</returns>
        ClassificationCollection ClassifyCollection(string classifierId, ClassifyCollectionInput body, Dictionary<string, object> customData = null);
        /// <summary>
        /// Create classifier.
        ///
        /// Sends data to create and train a classifier and returns information about the new classifier.
        /// </summary>
        /// <param name="metadata">Metadata in JSON format. The metadata identifies the language of the data, and an
        /// optional name to identify the classifier. Specify the language with the 2-letter primary language code as
        /// assigned in ISO standard 639.
        ///
        /// Supported languages are English (`en`), Arabic (`ar`), French (`fr`), German, (`de`), Italian (`it`),
        /// Japanese (`ja`), Korean (`ko`), Brazilian Portuguese (`pt`), and Spanish (`es`).</param>
        /// <param name="trainingData">Training data in CSV format. Each text value must have at least one class. The
        /// data can include up to 20,000 records. For details, see [Data
        /// preparation](https://console.bluemix.net/docs/services/natural-language-classifier/using-your-data.html).</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Classifier" />Classifier</returns>
        Classifier CreateClassifier(System.IO.FileStream metadata, System.IO.FileStream trainingData, Dictionary<string, object> customData = null);
        /// <summary>
        /// Delete classifier.
        /// </summary>
        /// <param name="classifierId">Classifier ID to delete.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        BaseModel DeleteClassifier(string classifierId, Dictionary<string, object> customData = null);
        /// <summary>
        /// Get information about a classifier.
        ///
        /// Returns status and other information about a classifier.
        /// </summary>
        /// <param name="classifierId">Classifier ID to query.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Classifier" />Classifier</returns>
        Classifier GetClassifier(string classifierId, Dictionary<string, object> customData = null);
        /// <summary>
        /// List classifiers.
        ///
        /// Returns an empty array if no classifiers are available.
        /// </summary>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="ClassifierList" />ClassifierList</returns>
        ClassifierList ListClassifiers(Dictionary<string, object> customData = null);
    }
}
