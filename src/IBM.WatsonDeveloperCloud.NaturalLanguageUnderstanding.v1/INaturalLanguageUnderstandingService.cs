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

using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model;
using System.Collections.Generic;

namespace IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1
{
    public partial interface INaturalLanguageUnderstandingService
    {
        /// <summary>
        /// Analyze text, HTML, or a public webpage. Analyzes text, HTML, or a public webpage with one or more text analysis features.  ### Concepts Identify general concepts that are referenced or alluded to in your content. Concepts that are detected typically have an associated link to a DBpedia resource.  ### Emotion Detect anger, disgust, fear, joy, or sadness that is conveyed by your content. Emotion information can be returned for detected entities, keywords, or user-specified target phrases found in the text.  ### Entities Detect important people, places, geopolitical entities and other types of entities in your content. Entity detection recognizes consecutive coreferences of each entity. For example, analysis of the following text would count "Barack Obama" and "He" as the same entity:  "Barack Obama was the 44th President of the United States. He took office in January 2009."  ### Keywords Determine the most important keywords in your content. Keyword phrases are organized by relevance in the results.  ### Metadata Get author information, publication date, and the title of your text/HTML content.  ### Relations Recognize when two entities are related, and identify the type of relation.  For example, you can identify an "awardedTo" relation between an award and its recipient.  ### Semantic Roles Parse sentences into subject-action-object form, and identify entities and keywords that are subjects or objects of an action.  ### Sentiment Determine whether your content conveys postive or negative sentiment. Sentiment information can be returned for detected entities, keywords, or user-specified target phrases found in the text.   ### Categories Categorize your content into a hierarchical 5-level taxonomy. For example, "Leonardo DiCaprio won an Oscar" returns "/art and entertainment/movies and tv/movies" as the most confident classification.
        /// </summary>
        /// <param name="parameters">An object containing request parameters. The `features` object and one of the `text`, `html`, or `url` attributes are required.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="AnalysisResults" />AnalysisResults</returns>
        AnalysisResults Analyze(Parameters parameters, Dictionary<string, object> customData = null);
        /// <summary>
        /// Delete model. Deletes a custom model.
        /// </summary>
        /// <param name="modelId">model_id of the model to delete.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="InlineResponse200" />InlineResponse200</returns>
        InlineResponse200 DeleteModel(string modelId, Dictionary<string, object> customData = null);
        /// <summary>
        /// List models. Lists available models for Relations and Entities features, including Watson Knowledge Studio custom models that you have created and linked to your Natural Language Understanding service.
        /// </summary>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="ListModelsResults" />ListModelsResults</returns>
        ListModelsResults ListModels(Dictionary<string, object> customData = null);
    }
}
