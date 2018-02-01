

/**
* Copyright 2017 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 the "License";
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

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    using System.Linq;

    /// <summary>
    /// options which are specific to a particular enrichment
    /// </summary>
    public partial class EnrichmentOptions : LanguageEnrichmentOptions
    {
        /// <summary>
        /// Initializes a new instance of the EnrichmentOptions class.
        /// </summary>
        public EnrichmentOptions() { }

        /// <summary>
        /// Initializes a new instance of the EnrichmentOptions class.
        /// </summary>
        /// <param name="extract">A comma sepeated list of analyses that
        /// should be applied when using the `alchemy_language` enrichment.
        /// See the the service documentation for details on each extract
        /// option.
        /// 
        /// Possible values include:
        /// 
        /// * entity
        /// * keyword
        /// * taxonomy
        /// * concept
        /// * relation
        /// * doc-sentiment
        /// * doc-emotion
        /// * typed-rels
        /// 
        /// </param>
        /// <param name="model">Required when using the `typed-rel` extract
        /// option. Should be set to the ID of a previously published custom
        /// Watson Knowledge Studio model.
        /// </param>
        /// <param name="language">If provided, then do not attempt to detect
        /// the language of the input document. Instead, assume the language
        /// is the one specified in this field.
        /// 
        /// You can set this property to work around
        /// `unsupported-text-language` errors.
        /// 
        /// Supported languages include English, German, French, Italian,
        /// Portuguese, Russian, Spanish and Swedish. Supported language
        /// codes are the ISO-639-1, ISO-639-2, ISO-639-3, and the plain
        /// english name of the language (e.g. "russian").
        /// . Possible values include: 'english', 'german', 'french',
        /// 'italian', 'portuguese', 'russian', 'spanish', 'swedish', 'en',
        /// 'eng', 'de', 'ger', 'deu', 'fr', 'fre', 'fra', 'it', 'ita', 'pt',
        /// 'por', 'ru', 'rus', 'es', 'spa', 'sv', 'swe'</param>
        public EnrichmentOptions(string extract = default(string), bool? sentiment = default(bool?), bool? quotations = default(bool?), bool? showSourceText = default(bool?), bool? hierarchicalTypedRelations = default(bool?), string model = default(string), string language = default(string))
            : base(extract, sentiment, quotations, showSourceText, hierarchicalTypedRelations, model, language)
        {
        }

    }
}
