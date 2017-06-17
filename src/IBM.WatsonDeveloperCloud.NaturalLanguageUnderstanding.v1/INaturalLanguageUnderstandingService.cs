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

using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model;

namespace IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1
{
    public interface INaturalLanguageUnderstandingService
    {
        /// <summary>
        /// Analyze text, HTML, or a public webpage. Analyzes text, HTML, or a public webpage with one or more text analysis features.
        /// </summary>
        /// <param name="parameters">An object containing request parameters. The `features` object and one of the `text`, `html`, or `url` attributes are required. (optional)</param>
        /// <returns><see cref="AnalysisResults" />AnalysisResults</returns>
        AnalysisResults Analyze(Parameters parameters = null);
        /// <summary>
        /// Delete model. Deletes a custom model.
        /// </summary>
        /// <param name="modelId">model_id of the model to delete</param>
        /// <returns><see cref="object" />object</returns>
        object DeleteModel(string modelId);

        /// <summary>
        /// List models. Lists available models for Relations and Entities features, including Watson Knowledge Studio custom models that you have created and linked to your Natural Language Understanding service.
        /// </summary>
        /// <returns><see cref="ListModelsResults" />ListModelsResults</returns>
        ListModelsResults GetModels();
    }
}
