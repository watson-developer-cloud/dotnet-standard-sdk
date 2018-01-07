/**
* Copyright 2017 IBM Corp. All Rights Reserved.
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
    public interface IVisualRecognitionService
    {
        /// <summary>
        /// Classifies an image.
        /// </summary>
        /// <param name="url">URL of an image (.jpg, .png). Redirects are followed, so you can use shortened URLs. The resolved URL is returned in the response. Maximum image size is 2 MB.</param>
        /// <param name="classifierIDs">An array of classifier IDs to use. "default" is the ID of the system classifier.</param>
        /// <param name="owners">Specifies which classifiers to run. The value must be 'IBM', 'me', or both.</param>
        /// <param name="threshold">A floating value that specifies the minimum score a class must have to be displayed in the response.</param>
        /// <param name="acceptLanguage">Specifies the language of the output class names. Can be 'en' (English), 'es' (Spanish), 'ar' (Arabic) or 'ja' (Japanese). Classes for which no translation is available are omitted.</param>
        /// <returns>ClassifyTopLevelMultiple</returns>
        ClassifyTopLevelMultiple Classify(string url, string[] classifierIDs = default(string[]), string[] owners = default(string[]), float threshold = 0f, string acceptLanguage = "en");
        /// <summary>
        /// Classifies an image.
        /// </summary>
        /// <param name="imageData">A byte[] of image data.</param>
        /// <param name="imageName">The name for the image.</param>
        /// <param name="imageMimeType">The image's mimetype.</param>
        /// <param name="urls">An array of URLs to classify.</param>
        /// <param name="classifierIDs">An array of classifier IDs to classify the images against.</param>
        /// <param name="owners">An array with the value(s) "IBM" and/or "me" to specify which classifiers to run.</param>
        /// <param name="threshold">A floating value that specifies the minimum score a class must have to be displayed in the response.</param>
        /// <param name="acceptLanguage">Specifies the language of the output class names. Can be 'en' (English), 'es' (Spanish), 'ar' (Arabic) or 'ja' (Japanese). Classes for which no translation is available are omitted.</param>
        /// <returns>ClassifyPost</returns>
        ClassifyPost Classify(byte[] imageData, string imageName, string imageMimeType, string[] urls = null, string[] classifierIDs = default(string[]), string[] owners = default(string[]), float threshold = 0f, string acceptLanguage = "en");
        /// <summary>
        /// Detects faces in a single URL.
        /// </summary>
        /// <param name="url">URL of an image (.jpg, .png). Redirects are followed, so you can use shortened URLs. The resolved URL is returned in the response. Maximum image size is 2 MB.</param>
        /// <returns>Faces</returns>
        Faces DetectFaces(string url);
        /// <summary>
        /// Detectes faces in image(s).
        /// </summary>
        /// <param name="imageData">The image file (.jpg, .png) or compressed (.zip) file of images. The total number of images is limited to 20, with a max .zip size of 5 MB.</param>
        /// <param name="imageDataName">The image file name.</param>
        /// <param name="imageDataMimeType">The image file mimetype.</param>
        /// <param name="urls">An array of image URLs to analyze.</param>
        /// <returns>Faces</returns>
        Faces DetectFaces(byte[] imageData = default(byte[]), string imageDataName = default(string), string imageDataMimeType = default(string), string[] urls = default(string[]));
        /// <summary>
        /// Retrieve a brief list of custom classifiers.
        /// </summary>
        /// <returns>GetClassifiersTopLevelBrief</returns>
        GetClassifiersTopLevelBrief GetClassifiersBrief();
        /// <summary>
        /// Retrieve a verbose list of custom classifiers.
        /// </summary>
        /// <returns>GetClassifiersTopLevelVerbose</returns>
        GetClassifiersTopLevelVerbose GetClassifiersVerbose();
        ///// <summary>
        ///// Trains a new classifier
        ///// </summary>
        ///// <param name="classifierName">The classifier's name.</param>
        ///// <param name="positiveExamples">A dictionary of class names and paths to a zip file of the class's positive examples. Must contain a minimum of 10 images.</param>
        ///// <param name="negativeExamplesPath">A path to a compressed (.zip) file of images that do not depict the visual subject of any of the classes of the new classifier. Must contain a minimum of 10 images.</param>
        ///// <returns>GetClassifiersTopLevelBrief</returns>
        //GetClassifiersTopLevelBrief CreateClassifier(string classifierName, Dictionary<string, string> positiveExamples, string negativeExamplesPath = default(string));
        /// <summary>
        /// Trains a new classifier
        /// </summary>
        /// <param name="classifierName">The classifier's name.</param>
        /// <param name="positiveExamplesData">A dictionary of class names and zip data of the class's positive examples. Must contain a minimum of 10 images.</param>
        /// <param name="negativeExamplesData">A byte[] of a zip file of images that do not depict the visual subject of any of the classes of the new classifier. Must contain a minimum of 10 images.</param>
        /// <returns>GetClassifiersTopLevelBrief</returns>
        GetClassifiersPerClassifierVerbose CreateClassifier(string classifierName, Dictionary<string, byte[]> positiveExamplesData, byte[] negativeExamplesData = null);
        /// <summary>
        /// Deletes a custom classifier.
        /// </summary>
        /// <param name="classifierId">The unique identifier of the custom classifier.</param>
        object DeleteClassifier(string classifierId);
        /// <summary>
        /// Retrive information about a custom classifier.
        /// </summary>
        /// <param name="classifierId">The unique identifier of the custom classifier.</param>
        /// <returns>GetClassifiersPerClassifierVerbose</returns>
        GetClassifiersPerClassifierVerbose GetClassifier(string classifierId);
        ///// <summary>
        ///// Updates classifier
        ///// </summary>
        ///// <param name="classifierId">The classifier's unique identifier.</param>
        ///// <param name="positiveExamples">A dictionary of class names and paths to a zip file of the class's positive examples. Must contain a minimum of 10 images.</param>
        ///// <param name="negativeExamplesPath">A path to a compressed (.zip) file of images that do not depict the visual subject of any of the classes of the new classifier. Must contain a minimum of 10 images.</param>
        ///// <returns>GetClassifiersPerClassifierVerbose</returns>
        //GetClassifiersPerClassifierVerbose UpdateClassifier(string classifierId, Dictionary<string, string> positiveExamples, string negativeExamplesPath = default(string));
        /// <summary>
        /// Updates classifier
        /// </summary>
        /// <param name="classifierId">The classifier's unique identifier.</param>
        /// <param name="positiveExamplesData">A dictionary of class names and zip data of the class's positive examples. Must contain a minimum of 10 images.</param>
        /// <param name="negativeExamplesData">A byte[] of a zip file of images that do not depict the visual subject of any of the classes of the new classifier. Must contain a minimum of 10 images.</param>
        /// <returns>GetClassifiersTopLevelBrief</returns>
        GetClassifiersPerClassifierVerbose UpdateClassifier(string classifierId, Dictionary<string, byte[]> positiveExamplesData, byte[] negativeExamplesData = null);
    }
}
