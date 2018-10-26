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

using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Http.Extensions;
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.Util;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IBM.WatsonDeveloperCloud.VisualRecognition.v3
{
    public partial class VisualRecognitionService : WatsonService, IVisualRecognitionService
    {
        /// <summary>
        /// Sets the apikey of the service. 
        /// Also sets the endpoint if the user has not set the endpoint.
        /// </summary>
        /// <param name="apikey"></param>
        public void SetCredential(string apikey)
        {
            this.ApiKey = apikey;
            if (!_userSetEndpoint)
                this.Endpoint = "https://gateway-a.watsonplatform.net/visual-recognition/api";
        }

        /// <summary>
        /// Gets the stream of a Core ML model file (.mlmodel) of a custom classifier that returns "core_ml_enabled": true in the classifier details.
        /// </summary>
        /// <param name="classifierId"></param>
        /// <returns>The Core ML model of the requested classifier.</returns>
        public Task<Stream> GetCoreMlModel(string classifierId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(classifierId))
                throw new ArgumentNullException(nameof(classifierId));

            System.Threading.Tasks.Task<Stream> result = null;
            
            try
            {
                IClient client;
                if (_tokenManager == null)
                {
                    client = this.Client;
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v3/classifiers/{classifierId}/core_ml_model");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(ApiKey))
                    restRequest.WithArgument("api_key", ApiKey);
                restRequest.WithArgument("classifier_id", classifierId);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                restRequest.WithFormatter(new MediaTypeHeaderValue("application/octet-stream"));
                result = restRequest.AsStream();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Downloads a Core ML model file (.mlmodel) of a custom classifier that returns "core_ml_enabled": true in the classifier details.
        /// The name of the retreived Core ML model will be [classifierId].mlmodel.
        /// </summary>
        /// <param name="classifierId">The requested Core ML model's classifier ID</param>
        /// <param name="filePath">The file path (without the filename) to download the Core ML model.</param>
        public void DownloadCoreMlModel(string classifierId, string filePath, Dictionary<string, object> customData = null)
        {
            var data = GetCoreMlModel(classifierId);

            using (Stream file = File.Create(string.Format("{0}\\{1}.mlmodel", filePath, classifierId)))
            {
                Utility.CopyStream(data.Result, file);
            }
        }
    }
}