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

using Newtonsoft.Json;
using System.Collections.Generic;

namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model
{
    public class Voice
    {
        private const string MALE = "male";
        private const string FEMALE = "female";

        public static readonly Voice DE_DIETER = new Voice("de-DE_DieterVoice", MALE, "de-DE");
        public static readonly Voice DE_BIRGIT = new Voice("de-DE_BirgitVoice", FEMALE, "de-DE");
        public static readonly Voice EN_ALLISON = new Voice("en-US_AllisonVoice", FEMALE, "en-US");
        public static readonly Voice EN_LISA = new Voice("en-US_LisaVoice", FEMALE, "en-US");
        public static readonly Voice EN_MICHAEL = new Voice("en-US_MichaelVoice", MALE, "en-US");
        public static readonly Voice ES_ENRIQUE = new Voice("es-ES_EnriqueVoice", MALE, "es-ES");
        public static readonly Voice ES_LAURA = new Voice("es-ES_LauraVoice", FEMALE, "es-US");
        public static readonly Voice ES_SOFIA = new Voice("es-US_SofiaVoice", FEMALE, "es-US");
        public static readonly Voice FR_RENEE = new Voice("fr-FR_ReneeVoice", FEMALE, "fr-FR");
        public static readonly Voice GB_KATE = new Voice("en-GB_KateVoice", FEMALE, "en-GB");
        public static readonly Voice IT_FRANCESCA = new Voice("it-IT_FrancescaVoice", FEMALE, "it-IT");
        public static readonly Voice JA_EMI = new Voice("ja-JP_EmiVoice", FEMALE, "ja-JP");
        public static readonly Voice PT_ISABELA = new Voice("pt-BR_IsabelaVoice", FEMALE, "pt-BR");


        [JsonProperty("customizable")]
        public bool Customizable { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }

        public Voice() { }

        public Voice(string name, string gender, string language)
        {
            this.Name = name;
            this.Gender = gender;
            this.Language = language;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}