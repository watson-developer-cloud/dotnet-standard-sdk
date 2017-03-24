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
        GetClassifiersTopLevelBrief CreateClassifier(string classifierName, Dictionary<string, byte[]> positiveExamplesData, byte[] negativeExamplesData = null);
        /// <summary>
        /// Deletes a custom classifier.
        /// </summary>
        /// <param name="classifierId">The unique identifier of the custom classifier.</param>
        void DeleteClassifier(string classifierId);
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
        /// <summary>
        /// List all custom collections.
        /// </summary>
        /// <returns>GetCollections</returns>
        GetCollections GetCollections();
        /// <summary>
        /// Create a new collection.
        /// </summary>
        /// <param name="name">The name of the new collection. The name can be a maximum of 128 UTF8 characters, with no spaces.</param>
        /// <returns>CreateCollection</returns>
        CreateCollection CreateCollection(string name);
        /// <summary>
        /// Delete's a colleciton.
        /// </summary>
        /// <param name="collectionId">The unique identifier of the collection to be deleted.</param>
        void DeleteCollection(string collectionId);
        /// <summary>
        /// Retrieve collection details.
        /// </summary>
        /// <param name="collectionId">The unique identifier of the requested collection.</param>
        /// <returns>CreateCollection</returns>
        CreateCollection GetCollection(string collectionId);
        /// <summary>
        /// List 100 images in a collection.
        /// </summary>
        /// <param name="collectionId">The unique identifier of the requested collection.</param>
        /// <returns>GetCollectionImages</returns>
        GetCollectionImages GetCollectionImages(string collectionId);
        ///// <summary>
        ///// Adds images to a collection.
        ///// </summary>
        ///// <param name="collectionId">The unique identifier of the collection to add images to.</param>
        ///// <param name="imageData">A byte[] of image data.</param>
        ///// <param name="imageName">The image file's name.</param>
        ///// <param name="imageMetadataFilepath">Filepath to a jsonb file that adds metadata to the image. Maximum 2 KB of metadata for each image.</param>
        ///// <returns>CollectionsConfig</returns>
        //CollectionsConfig AddImage(string collectionId, byte[] imageData, string imageName, string imageMetadataFilepath);
        /// <summary>
        /// Adds images to a collection.
        /// </summary>
        /// <param name="collectionId">The unique identifier of the collection to add images to.</param>
        /// <param name="imageData">A byte[] of image data.</param>
        /// <param name="imageName">The image file's name.</param>
        /// <param name="imageMetadataData">A byte[] of image metadata data.</param>
        /// <param name="imageMetadataFilename">The filename of the image metadata.</param>
        /// <returns></returns>
        CollectionsConfig AddImage(string collectionId, byte[] imageData, string imageName, byte[] imageMetadataData, string imageMetadataFilename = "metadata.json");
        /// <summary>
        /// Deletes an image from a collection.
        /// </summary>
        /// <param name="collectionId">The unique identifier for the collection to delete an image from.</param>
        /// <param name="imageId">The unique identifier for the image to delete from the collection.</param>
        void DeleteImage(string collectionId, string imageId);
        /// <summary>
        /// Lists an image's details.
        /// </summary>
        /// <param name="collectionId">The unique identifier for the collection to delete an image from.</param>
        /// <param name="imageId">The unique identifier for the image to delete from the collection.</param>
        /// <returns></returns>
        GetCollectionsBrief GetImage(string collectionId, string imageId);
        /// <summary>
        /// Deletes an image's metadata.
        /// </summary>
        /// <param name="collectionId">The unique identifier for the collection to delete an image's metadata from.</param>
        /// <param name="imageId">The unique identifier for the image to delete the metadata from the collection.</param>
        void DeleteImageMetadata(string collectionId, string imageId);
        /// <summary>
        /// Lists image's metadata.
        /// </summary>
        /// <param name="collectionId">The unique identifier for the collection to get an image's metadata from.</param>
        /// <param name="imageId">The unique identifier for the image to get the metadata.</param>
        /// <returns>MetadataResponse</returns>
        MetadataResponse GetMetadata(string collectionId, string imageId);
        ///// <summary>
        ///// Adds metadata to an image.
        ///// </summary>
        ///// <param name="collectionId">The unique identifier for the collection who's image to update metadata to.</param>
        ///// <param name="imageId">The unique identifier for the image to update metadata.</param>
        ///// <param name="metadataFilePath">The filepath to the metadata json file.</param>
        ///// <returns>MetadataResponse</returns>
        //MetadataResponse UpdateMetadata(string collectionId, string imageId, string metadataFilePath);
        /// <summary>
        /// Adds metadata to an image.
        /// </summary>
        /// <param name="collectionId">The unique identifier for the collection who's image to update metadata.</param>
        /// <param name="imageId">The unique identifier for the image to update metadata.</param>
        /// <param name="metadataFileData">A byte[] of metadata data.</param>
        /// <param name="metadataFileName">The filename of the metadata file.</param>
        /// <returns>MetadataResponse</returns>
        MetadataResponse UpdateMetadata(string collectionId, string imageId, byte[] metadataFileData, string metadataFileName = "metadata.json");
        ///// <summary>
        ///// Finds similar images.
        ///// </summary>
        ///// <param name="collectionId">The unique identifier for the collection who's image to add metadata to.</param>
        ///// <param name="imageFilePath">The file path of the image used to search for similar images in a collection.</param>
        ///// <param name="limit">The number of similar results you want returned. Default limit is 10 results, you can specify a maximum limit of 100 results.</param>
        ///// <returns>SimilarImagesConfig</returns>
        //SimilarImagesConfig FindSimilar(string collectionId, string imageFilePath, int limit = 10);
        /// <summary>
        /// Finds similar images.
        /// </summary>
        /// <param name="collectionId">The unique identifier for the collection who's image to add metadata to.</param>
        /// <param name="imageFileData">A byte[] of image data used to search for similar images in a collection.</param>
        /// <param name="limit">The number of similar results you want returned. Default limit is 10 results, you can specify a maximum limit of 100 results.</param>
        /// <returns></returns>
        SimilarImagesConfig FindSimilar(string collectionId, byte[] imageFileData, int limit = 10);
    }
}
