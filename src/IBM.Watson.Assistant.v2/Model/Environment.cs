/**
* (C) Copyright IBM Corp. 2022.
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

using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace IBM.Watson.Assistant.v2.Model
{
    /// <summary>
    /// Environment.
    /// </summary>
    public class Environment
    {
        /// <summary>
        /// The name of the environment.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// The description of the environment.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// The language of the environment. An environment is always created with the same language as the assistant it
        /// is associated with.
        /// </summary>
        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }
        /// <summary>
        /// The assistant ID of the assistant the environment is associated with.
        /// </summary>
        [JsonProperty("assistant_id", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string AssistantId { get; private set; }
        /// <summary>
        /// The environment ID of the environment.
        /// </summary>
        [JsonProperty("environment_id", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string EnvironmentId { get; private set; }
        /// <summary>
        /// The type of the environment. All environments other than the `draft` and `live` environments have the type
        /// `staging`.
        /// </summary>
        [JsonProperty("environment", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string _Environment { get; private set; }
        /// <summary>
        /// An object describing the release that is currently deployed in the environment.
        /// </summary>
        [JsonProperty("release_reference", NullValueHandling = NullValueHandling.Ignore)]
        public EnvironmentReleaseReference ReleaseReference { get; set; }
        /// <summary>
        /// The search skill orchestration settings for the environment.
        /// </summary>
        [JsonProperty("orchestration", NullValueHandling = NullValueHandling.Ignore)]
        public EnvironmentOrchestration Orchestration { get; set; }
        /// <summary>
        /// The session inactivity timeout setting for the environment.
        /// </summary>
        [JsonProperty("session_timeout", NullValueHandling = NullValueHandling.Ignore)]
        public long? SessionTimeout { get; set; }
        /// <summary>
        /// An array of objects describing the integrations that exist in the environment.
        /// </summary>
        [JsonProperty("integration_references", NullValueHandling = NullValueHandling.Ignore)]
        public List<IntegrationReference> IntegrationReferences { get; set; }
        /// <summary>
        /// An array of objects describing the skills (such as actions and dialog) that exist in the environment.
        /// </summary>
        [JsonProperty("skill_references", NullValueHandling = NullValueHandling.Ignore)]
        public List<SkillReference> SkillReferences { get; set; }
        /// <summary>
        /// The timestamp for creation of the object.
        /// </summary>
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTime? Created { get; private set; }
        /// <summary>
        /// The timestamp for the most recent update to the object.
        /// </summary>
        [JsonProperty("updated", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTime? Updated { get; private set; }
    }

}
