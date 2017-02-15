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

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Model
{
    public class ObserveResultOptions
    {
        /// <summary>
        /// The identifier of the session whose results you want to observe.
        /// </summary>
        public string SessionId { get; private set; }

        /// <summary>
        /// The sequence ID of the recognition task whose results you want to observe. Omit the parameter to obtain results either for an ongoing recognition, if any, or for the next recognition task regardless of whether it specifies a sequence ID.
        /// </summary>
        public int SequenceId { get; private set; }

        /// <summary>
        /// Indicates whether the service is to return interim results. If true, interim results are returned as a stream of JSON objects; each object represents a single <see cref="SpeechRecognitionEvent" />. If false, the response is a single <see cref="SpeechRecognitionEvent" /> with final results only.
        /// </summary>
        public bool InterimResults { get; private set; }

        private ObserveResultOptions() { }

        public interface IObserveResultOptionsBuilder
        {
            IOptional WithSession(string _sessionId);
        }

        public interface IOptional
        {
            IOptional WithSequenceId(int _sequenceId);
            IOptional WithInterimResults();
            ObserveResultOptions Build();
        }

        class ObserveResultOptionsBuilder: IObserveResultOptionsBuilder, IOptional
        {
            string _sessionId;
            int _sequenceId;
            bool _interimResults;

            public IOptional WithSession(string _sessionId)
            {
                this._sessionId = _sessionId;
                return this;
            }

            public IOptional WithSequenceId(int _sequenceId)
            {
                this._sequenceId = _sequenceId;
                return this;
            }

            public IOptional WithInterimResults()
            {
                this._interimResults = true;
                return this;
            }

            public ObserveResultOptions Build()
            {
                return this;
            }

            public static implicit operator ObserveResultOptions(ObserveResultOptionsBuilder builder)
            {
                return new ObserveResultOptions()
                {
                    SessionId = builder._sessionId,
                    SequenceId = builder._sequenceId,
                    InterimResults = builder._interimResults
                };
            }
        }

        public static IObserveResultOptionsBuilder Builder => new ObserveResultOptionsBuilder();
    }
}