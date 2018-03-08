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
        /// <summary>
        /// Classify images. Classify images with built-in or custom classifiers.
        /// </summary>
        /// <param name="imagesFile">An image file (.jpg, .png) or .zip file with images. Maximum image size is 10 MB. Include no more than 20 images and limit the .zip file to 100 MB. Encode the image and .zip file names in UTF-8 if they contain non-ASCII characters. The service assumes UTF-8 encoding if it encounters non-ASCII characters. You can also include images with the `url` property in the **parameters** object. (optional)</param>
        /// <param name="acceptLanguage">Specifies the language of the output class names.  Can be `en` (English), `ar` (Arabic), `de` (German), `es` (Spanish), `it` (Italian), `ja` (Japanese), or `ko` (Korean).  Classes for which no translation is available are omitted.  The response might not be in the specified language under these conditions: - English is returned when the requested language is not supported. - Classes are not returned when there is no translation for them. - Custom classifiers returned with this method return tags in the language of the custom classifier. (optional, default to en)</param>
        /// <param name="url">A string with the image URL to analyze. Must be in .jpg, or .png format. The minimum recommended pixel density is 32X32 pixels per inch, and the maximum image size is 10 MB. You can also include images in the **images_file** parameter. (optional)</param>
        /// <param name="threshold">A floating point value that specifies the minimum score a class must have to be displayed in the response. The default threshold for returning scores from a classifier is `0.5`. Set the threshold to `0.0` to ignore the classification score and return all values. (optional)</param>
        /// <param name="owners">An array of the categories of classifiers to apply. Use `IBM` to classify against the `default` general classifier, and use `me` to classify against your custom classifiers. To analyze the image against both classifier categories, set the value to both `IBM` and `me`.   The built-in `default` classifier is used if both **classifier_ids** and **owners** parameters are empty.  The **classifier_ids** parameter overrides **owners**, so make sure that **classifier_ids** is empty. (optional)</param>
        /// <param name="classifierIds">The **classifier_ids** parameter overrides **owners**, so make sure that **classifier_ids** is empty. - **classifier_ids**: Specifies which classifiers to apply and overrides the **owners** parameter. You can specify both custom and built-in classifiers. The built-in `default` classifier is used if both **classifier_ids** and **owners** parameters are empty.  The following built-in classifier IDs require no training: - `default`: Returns classes from thousands of general tags. - `food`: (Beta) Enhances specificity and accuracy for images of food items. - `explicit`: (Beta) Evaluates whether the image might be pornographic.  Example: `"classifier_ids="CarsvsTrucks_1479118188","explicit"`. (optional)</param>
        /// <param name="imagesFileContentType">The content type of imagesFile. (optional)</param>
        /// <returns><see cref="ClassifiedImages" />ClassifiedImages</returns>
        ClassifiedImages Classify(System.IO.Stream imagesFile = null, string acceptLanguage = null, string url = null, float? threshold = null, List<string> owners = null, List<string> classifierIds = null, string imagesFileContentType = null);
        /// <summary>
        /// Detect faces in images. Analyze and get data about faces in images. Responses can include estimated age and gender, and the service can identify celebrities. This feature uses a built-in classifier, so you do not train it on custom classifiers. The Detect faces method does not support general biometric facial recognition.
        /// </summary>
        /// <param name="imagesFile">An image file (.jpg, .png) or .zip file with images. Include no more than 15 images. You can also include an image with the**url** parameter.  All faces are detected, but if there are more than 10 faces in an image, age and gender confidence scores might return scores of 0. (optional)</param>
        /// <param name="url">A string with the image URL to analyze. (optional)</param>
        /// <param name="imagesFileContentType">The content type of imagesFile. (optional)</param>
        /// <returns><see cref="DetectedFaces" />DetectedFaces</returns>
        DetectedFaces DetectFaces(System.IO.Stream imagesFile = null, string url = null, string imagesFileContentType = null);

        /// <summary>
        /// Delete a classifier. 
        /// </summary>
        /// <param name="classifierId">The ID of the classifier.</param>
        /// <returns><see cref="object" />object</returns>
        object DeleteClassifier(string classifierId);

        /// <summary>
        /// Retrieve classifier details. Retrieve information about a custom classifier.
        /// </summary>
        /// <param name="classifierId">The ID of the classifier.</param>
        /// <returns><see cref="Classifier" />Classifier</returns>
        Classifier GetClassifier(string classifierId);

        /// <summary>
        /// Retrieve a list of custom classifiers. 
        /// </summary>
        /// <param name="verbose">Specify `true` to return details about the classifiers. Omit this parameter to return a brief list of classifiers. (optional)</param>
        /// <returns><see cref="Classifiers" />Classifiers</returns>
        Classifiers ListClassifiers(bool? verbose = null);
    }
}
