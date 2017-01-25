/**
* Copyright 2015 IBM Corp. All Rights Reserved.
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
using IBM.WatsonDeveloperCloud.Util.Extensions;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Models
{
    public class ProfileOptions
    {
        private ProfileOptions() { }

        public string ContentType { get; private set; }
        public string Language { get; private set; }
        public string Accept { get; private set; }
        public string AcceptLanguage { get; private set; }
        public bool IncludeRaw { get; private set; }
        public bool ConsumptionPreferences { get; private set; }
        public bool CsvHeaders { get; private set; }
        public string Text { get; private set; }

        [JsonProperty("input", NullValueHandling = NullValueHandling.Ignore)]
        public ContentListContainer ContentItems { get; private set; }

        public interface IHeader
        {
            IContentType WithTextPlain();
            IContentType WithTextHtml();
            IContentType WithApplicationJson();
        }

        public interface IContentType
        {
            IContentLanguage AsArabic();
            IContentLanguage AsEnglish();
            IContentLanguage AsSpanish();
            IContentLanguage AsJapanese();
        }

        public interface IContentLanguage
        {
            IAccept AcceptJson();
            IAccept AcceptCsv();
        }

        public interface IAccept
        {
            IAcceptLanguage AcceptArabicLanguage();
            IAcceptLanguage AcceptGermanLanguage();
            IAcceptLanguage AcceptEnglishLanguage();
            IAcceptLanguage AcceptSpanishLanguage();
            IAcceptLanguage AcceptFrenchLanguage();
            IAcceptLanguage AcceptItalianLanguage();
            IAcceptLanguage AcceptJapaneseLanguage();
            IAcceptLanguage AcceptKoreanLanguage();
            IAcceptLanguage AcceptBrazilianPortugueseLanguage();
            IAcceptLanguage AcceptSimplifiedChineseLanguage();
            IAcceptLanguage AcceptTraditionalChineseLanguage();
        }

        public interface IAcceptLanguage
        {
            IAcceptLanguage AddRawScore();
            IAcceptLanguage AddConsumptionPreferences();
            IAcceptLanguage AddCsvHeaders();
            IProfileOptionsBuilder WithBody(string text);
            IProfileOptionsBuilder WithBody(ContentListContainer content);
        }

        public interface IProfileOptionsBuilder : IHeader, IContentType, IContentLanguage, IAccept, IAcceptLanguage
        {
            IHeader PorfileOptions();
        }

        class ProfileOptionsBuilder : IProfileOptionsBuilder
        {
            string _contentType;
            string _contentLanguage;
            string _accept;
            string _acceptLanguage;
            bool _includeRaw;
            bool _headers;
            bool _consumptionPreferences;
            bool _csvHeaders;
            string _text;
            ContentListContainer _contentItems;

            ContentListContainer _body = new ContentListContainer();

            public IContentType WithTextPlain()
            {
                this._contentType = "text/plain";
                return this;
            }

            public IContentType WithTextHtml()
            {
                this._contentType = "text/html";
                return this;
            }

            public IContentType WithApplicationJson()
            {
                this._contentType = "application/json";
                return this;
            }

            public IContentLanguage AsArabic()
            {
                this._contentLanguage = "ar";
                return this;
            }

            public IContentLanguage AsEnglish()
            {
                this._contentLanguage = "en";
                return this;
            }

            public IContentLanguage AsSpanish()
            {
                this._contentLanguage = "es";
                return this;
            }

            public IContentLanguage AsJapanese()
            {
                this._contentLanguage = "ja";
                return this;
            }

            public IAccept AcceptJson()
            {
                this._accept = "application/json";
                return this;
            }

            public IAccept AcceptCsv()
            {
                this._accept = "text/csv";
                return this;
            }

            public IAcceptLanguage AcceptArabicLanguage()
            {
                this._acceptLanguage = "ar";
                return this;
            }

            public IAcceptLanguage AcceptGermanLanguage()
            {
                this._acceptLanguage = "de";
                return this;
            }

            public IAcceptLanguage AcceptEnglishLanguage()
            {
                this._acceptLanguage = "en";
                return this;
            }

            public IAcceptLanguage AcceptSpanishLanguage()
            {
                this._acceptLanguage = "es";
                return this;
            }

            public IAcceptLanguage AcceptFrenchLanguage()
            {
                this._acceptLanguage = "fr";
                return this;
            }

            public IAcceptLanguage AcceptItalianLanguage()
            {
                this._acceptLanguage = "it";
                return this;
            }

            public IAcceptLanguage AcceptJapaneseLanguage()
            {
                this._acceptLanguage = "ja";
                return this;
            }

            public IAcceptLanguage AcceptKoreanLanguage()
            {
                this._acceptLanguage = "ko";
                return this;
            }

            public IAcceptLanguage AcceptBrazilianPortugueseLanguage()
            {
                this._acceptLanguage = "pt-br";
                return this;
            }

            public IAcceptLanguage AcceptSimplifiedChineseLanguage()
            {
                this._acceptLanguage = "zh-cn";
                return this;
            }

            public IAcceptLanguage AcceptTraditionalChineseLanguage()
            {
                this._acceptLanguage = "zh-tw";
                return this;
            }

            public IAcceptLanguage AddRawScore()
            {
                this._includeRaw = true;
                return this;
            }

            public IAcceptLanguage AddConsumptionPreferences()
            {
                this._consumptionPreferences = true;
                return this;
            }

            public IAcceptLanguage AddCsvHeaders()
            {
                this._csvHeaders = true;
                return this;
            }
            
            public IProfileOptionsBuilder WithBody(string text)
            {
                this._text = text;
                return this;
            }

            public IProfileOptionsBuilder WithBody(ContentListContainer content)
            {
                this._contentItems = content;
                return this;
            }

            public IHeader PorfileOptions()
            {
                return this;
            }

            public static implicit operator ProfileOptions(ProfileOptionsBuilder builder)
            {
                return new ProfileOptions()
                {
                    ContentType = builder._contentType,
                    Language = builder._acceptLanguage,
                    Accept = builder._accept,
                    AcceptLanguage = builder._acceptLanguage,
                    IncludeRaw = builder._includeRaw,
                    ConsumptionPreferences = builder._consumptionPreferences,
                    CsvHeaders = builder._csvHeaders,
                    Text = builder._text,
                    ContentItems = builder._contentItems
                };
            }
        }

        public static IHeader CreateOptions()
        {
            return new ProfileOptionsBuilder();
        }
    }
}