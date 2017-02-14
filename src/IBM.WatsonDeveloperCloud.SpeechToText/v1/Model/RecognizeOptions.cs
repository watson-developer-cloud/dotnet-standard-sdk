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
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Util;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Model
{
    public class RecognizeOptions
    {
        public string ContentType { get; private set; }
        public string TransferEncoding { get; private set; }
        public string Model { get; private set; }
        public string CustomizationId { get; private set; }

        public bool Continuous { get; private set; }
        public int InactivityTimeout { get; private set; }
        public string[] Keywords { get; private set; }
        public double KeywordsThreshold { get; private set; }
        public int MaxAlternatives { get; private set; }
        public double WordAlternativesThreshold { get; private set; }
        public bool WordConfidence { get; private set; }
        public bool Timestamps { get; private set; }
        public bool ProfanityFilter { get; private set; }
        public bool SmartFormatting { get; private set; }
        public bool SpeakerLabels { get; private set; }

        public StreamContent BodyContent { get; private set; }
        public MultipartFormDataContent FormData { get; private set; }

        public string SessionId { get; private set; }

        private RecognizeOptions() { }

        public interface IRecognizeOptions
        {
            IRecognizeOptions WithContentType(string _contentType);
            IRecognizeOptions WithTransferEnconding();
            IRecognizeOptions WithModel(string _model);
            IRecognizeOptions WithCustomization(string _customizationId);
            INonMultipart WithBody(FileStream _audio);
            IUpload WithFormData(Metadata _metaData);
        }

        public interface IUpload
        {
            IUpload Upload(FileStream _audio);
            RecognizeOptions Build();
        }

        public interface INonMultipart
        {
            INonMultipart IsContinuous();
            INonMultipart SetInactivityTimeout(int _inactivityTimeout);
            INonMultipart WithKeywords(string[] _keywords);
            INonMultipart WithKeywordsThreshold(double _keywordsThreshold);
            INonMultipart MaxAlternatives(int _maxAlternatives);
            INonMultipart WithWordAlternativeThreshold(double _wordAlternativesThreshould);
            INonMultipart WithWordConfidence();
            INonMultipart WithTimestamps();
            INonMultipart WithoutProfanityFilter();
            INonMultipart WithSmartFormatting();
            INonMultipart WithSpeakerLabels();
            RecognizeOptions Build();
        }

        public interface IRecognizeOptionsBuilder : IRecognizeOptions, IUpload, INonMultipart { }

        class RecognizeOptionsBuilder : IRecognizeOptionsBuilder
        {
            string _contentType;
            string _transferEncoding;
            string _model = "en-US_BroadbandModel";
            string _customizationId;

            bool _continuous;
            int _inactivityTimeout = 30;
            string[] _keywords;
            double _keywordsThreshold;
            int _maxAlternatives = 1;
            double _wordAlternativesThreshold;
            bool _wordConfidence;
            bool _timestamps;
            bool _profanityFilter = true;
            bool _smartFormatting;
            bool _speakerLabels;

            StreamContent _bodyContent;
            MultipartFormDataContent _formData;

            public IRecognizeOptions WithContentType(string _contentType)
            {
                this._contentType = _contentType;
                return this;
            }

            public IRecognizeOptions WithTransferEnconding()
            {
                this._transferEncoding = "chuncked";
                return this;
            }

            public IRecognizeOptions WithModel(string _model)
            {
                this._model = _model;
                return this;
            }

            public IRecognizeOptions WithCustomization(string _customizationId)
            {
                this._customizationId = _customizationId;
                return this;
            }

            public INonMultipart WithBody(FileStream _audio)
            {
                _bodyContent = new StreamContent(_audio);

                if(string.IsNullOrEmpty(this._contentType))
                    this._contentType = _audio.GetMediaTypeFromFile();

                _bodyContent.Headers.Add("Content-Type", this._contentType);
                return this;
            }

            public IUpload WithFormData(Metadata _metaData)
            {
                var json = JsonConvert.SerializeObject(_metaData);

                StringContent metadata = new StringContent(json);
                metadata.Headers.ContentType = MediaTypeHeaderValue.Parse(HttpMediaType.APPLICATION_JSON);

                _formData = new MultipartFormDataContent();
                _formData.Add(metadata, "metadata");

                return this;
            }

            public IUpload Upload(FileStream _audio)
            {
                var audioContent = new ByteArrayContent((_audio as Stream).ReadAllBytes());

                if (string.IsNullOrEmpty(this._contentType))
                    this._contentType = _audio.GetMediaTypeFromFile();

                audioContent.Headers.ContentType = MediaTypeHeaderValue.Parse(this._contentType);

                _formData.Add(audioContent, "upload", _audio.Name);

                return this;
            }

            public RecognizeOptions Build()
            {
                return this;
            }

            public INonMultipart IsContinuous()
            {
                this._continuous = true;
                return this;
            }

            public INonMultipart SetInactivityTimeout(int _inactivityTimeout)
            {
                this._inactivityTimeout = _inactivityTimeout;
                return this;
            }

            public INonMultipart WithKeywords(string[] _keywords)
            {
                this._keywords = _keywords;
                return this;
            }

            public INonMultipart WithKeywordsThreshold(double _keywordsThreshold)
            {
                this._keywordsThreshold = _keywordsThreshold;
                return this;
            }

            public INonMultipart MaxAlternatives(int _maxAlternatives)
            {
                this._maxAlternatives = _maxAlternatives;
                return this;
            }

            public INonMultipart WithWordAlternativeThreshold(double _wordAlternativesThreshould)
            {
                this._wordAlternativesThreshold = _wordAlternativesThreshould;
                return this;
            }

            public INonMultipart WithWordConfidence()
            {
                this._wordConfidence = true;
                return this;
            }

            public INonMultipart WithTimestamps()
            {
                this._timestamps = true;
                return this;
            }

            public INonMultipart WithoutProfanityFilter()
            {
                this._profanityFilter = false;
                return this;
            }

            public INonMultipart WithSmartFormatting()
            {
                this._smartFormatting = true;
                return this;
            }

            public INonMultipart WithSpeakerLabels()
            {
                this._speakerLabels = true;
                return this;
            }

            public static implicit operator RecognizeOptions(RecognizeOptionsBuilder builder)
            {
                return new RecognizeOptions()
                {
                    ContentType = builder._contentType,
                    TransferEncoding = builder._transferEncoding,
                    Model = builder._model,
                    CustomizationId = builder._customizationId,
                    Continuous = builder._continuous,
                    InactivityTimeout = builder._inactivityTimeout,
                    Keywords = builder._keywords,
                    KeywordsThreshold = builder._keywordsThreshold,
                    MaxAlternatives = builder._maxAlternatives,
                    WordAlternativesThreshold = builder._wordAlternativesThreshold,
                    WordConfidence = builder._wordConfidence,
                    Timestamps = builder._timestamps,
                    ProfanityFilter = builder._profanityFilter,
                    SmartFormatting = builder._smartFormatting,
                    SpeakerLabels = builder._speakerLabels,
                    BodyContent = builder._bodyContent,
                    FormData = builder._formData
                };
            }
        }

        public static IRecognizeOptions Builder => new RecognizeOptionsBuilder();
    }
}