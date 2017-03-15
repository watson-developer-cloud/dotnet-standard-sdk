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

using System;
using System.Collections.Generic;
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model;

namespace IBM.WatsonDeveloperCloud.VisualRecognition.v3
{
    public class VisualRecognitionService : WatsonService, IVisualRecognitionService
    {
        const string PATH_MESSAGE = "/v1/workspaces/{0}/message";
        const string VERSION_DATE_2016_05_20 = "2016-05-20";
        const string SERVICE_NAME = "visual_recognition";
        const string URL = "https://gateway-a.watsonplatform.net/visual-recognition/api";

        public VisualRecognitionService()
            : base(SERVICE_NAME)
        {

        }

        public ClassifyTopLevelMultiple Classify(string url, string[] classifierIDs = null, string[] owners = null, float threshold = 0, string acceptLanguage = "en")
        {
            throw new NotImplementedException();
        }

        public ClassifyPost Classify(byte[] imageData, string imageName, string imageMimeType, string[] classifierIDs = null, string[] owners = null, float threshold = 0, string acceptLanguage = "en")
        {
            throw new NotImplementedException();
        }

        public Faces DetectFaces(string url)
        {
            throw new NotImplementedException();
        }

        public Faces DetectFaces(byte[] imageData = null, string imageName = null, string imageMimeType = null, string[] urls = null)
        {
            throw new NotImplementedException();
        }

        public GetClassifiersTopLevelBrief GetClassifiers(bool verbose = false)
        {
            throw new NotImplementedException();
        }

        public GetClassifiersTopLevelBrief CreateClassifier(string classifierName, Dictionary<string, string> positiveExamples, string negativeExamplesPath = null)
        {
            throw new NotImplementedException();
        }

        public GetClassifiersTopLevelBrief CreateClassifier(string classifierName, Dictionary<string, byte[]> positiveExamplesData, byte[] negativeExamplesData = null)
        {
            throw new NotImplementedException();
        }

        public void DeleteClassifier(string classifierId)
        {
            throw new NotImplementedException();
        }

        public GetClassifiersPerClassifierVerbose GetClassifier(string classifierId)
        {
            throw new NotImplementedException();
        }

        public GetClassifiersPerClassifierVerbose UpdateClassifier(string classifierId, Dictionary<string, string> positiveExamples, string negativeExamplesPath = null)
        {
            throw new NotImplementedException();
        }

        public GetClassifiersPerClassifierVerbose UpdateClassifier(string classifierId, Dictionary<string, byte[]> positiveExamplesData, byte[] negativeExamplesData = null)
        {
            throw new NotImplementedException();
        }

        public GetCollections GetCollections()
        {
            throw new NotImplementedException();
        }

        public CreateCollection CreateCollection(string name)
        {
            throw new NotImplementedException();
        }

        public void DeleteCollection(string collectionId)
        {
            throw new NotImplementedException();
        }

        public CreateCollection GetCollection(string collectionId)
        {
            throw new NotImplementedException();
        }

        public GetCollectionImages GetCollectionImages(string collectionId)
        {
            throw new NotImplementedException();
        }

        public CollectionsConfig AddImage(string collectionId, byte[] imageData, string imageName, string imageMetadataFilepath)
        {
            throw new NotImplementedException();
        }

        public CollectionsConfig AddImage(string collectionId, byte[] imageData, string imageName, byte[] imageMetadataData, string imageMetadataFilename = "metadata.json")
        {
            throw new NotImplementedException();
        }

        public void DeleteImage(string collectionId, string imageId)
        {
            throw new NotImplementedException();
        }

        public GetCollectionsBrief GetImage(string collectionId, string imageId)
        {
            throw new NotImplementedException();
        }

        public void DeleteImageMetadata(string collectionId, string imageId)
        {
            throw new NotImplementedException();
        }

        public MetadataResponse GetMetadata(string collectionId, string imageId)
        {
            throw new NotImplementedException();
        }

        public MetadataResponse UpdateMetadata(string collectionId, string imageId, string metadataFilePath)
        {
            throw new NotImplementedException();
        }

        public MetadataResponse UpdateMetadata(string collectionId, string imageId, byte[] metadataFileData, string metadataFileName = "metadata.json")
        {
            throw new NotImplementedException();
        }

        public SimilarImagesConfig FindSimilar(string collectionId, string imageFilePath, int limit = 10)
        {
            throw new NotImplementedException();
        }

        public SimilarImagesConfig FindSimilar(string collectionId, byte[] imageFileData, int limit = 10)
        {
            throw new NotImplementedException();
        }
    }
}
