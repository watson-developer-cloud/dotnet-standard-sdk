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

using System.IO;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Models
{
    public class CreateModelOptions
    {
        private CreateModelOptions() { }

        public string BaseModelId { get; private set; }
        public string Name { get; private set; }
        public FileStream ForcedGlossary { get; private set; }
        public FileStream ParallelCorpus { get; private set; }
        public FileStream MonolingualCorpus { get; private set; }

        public interface ICreateModelOptions
        {
            ICreateModelOptions WithBaseModelId(string _baseModelId);
            ICreateModelOptions WithName(string _name);
            CreateModelOptions SetForcedGlossary(FileStream _forcedGlossary);
            CreateModelOptions SetParallelCorpus(FileStream _parallelCorpus);
            CreateModelOptions SetMonolingualCorpus(FileStream _monolingualCorpus);
        }

        class CreateModelOptionBuilder : ICreateModelOptions
        {
            string _baseModelId;
            string _name;
            FileStream _forcedGlossary;
            FileStream _parallelCorpus;
            FileStream _monolingualCorpus;

            public CreateModelOptions SetForcedGlossary(FileStream _forcedGlossary)
            {
                this._forcedGlossary = _forcedGlossary;
                return this;
            }

            public CreateModelOptions SetMonolingualCorpus(FileStream _monolingualCorpus)
            {
                this._monolingualCorpus = _monolingualCorpus;
                return this;
            }

            public CreateModelOptions SetParallelCorpus(FileStream _parallelCorpus)
            {
                this._parallelCorpus = _parallelCorpus;
                return this;
            }

            public ICreateModelOptions WithBaseModelId(string _baseModelId)
            {
                this._baseModelId = _baseModelId;
                return this;
            }

            public ICreateModelOptions WithName(string _name)
            {
                this._name = _name;
                return this;
            }

            public static implicit operator CreateModelOptions(CreateModelOptionBuilder builder)
            {
                return new CreateModelOptions()
                {
                    BaseModelId = builder._baseModelId,
                    Name = builder._name,
                    ForcedGlossary = builder._forcedGlossary,
                    ParallelCorpus = builder._parallelCorpus,
                    MonolingualCorpus = builder._monolingualCorpus
                };
            }
        }

        public static ICreateModelOptions CreateOptions()
        {
            return new CreateModelOptionBuilder();
        }
    }
}