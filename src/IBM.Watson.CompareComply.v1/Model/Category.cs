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

using IBM.Cloud.SDK.Core;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IBM.Watson.CompareComply.v1.Model
{
    /// <summary>
    /// Information defining an element's subject matter.
    /// </summary>
    public class Category : BaseModel
    {
        /// <summary>
        /// The category of the associated element.
        /// </summary>
        /// <value>
        /// The category of the associated element.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum LabelEnum
        {
            
            /// <summary>
            /// Enum AMENDMENTS for Amendments
            /// </summary>
            [EnumMember(Value = "Amendments")]
            AMENDMENTS,
            
            /// <summary>
            /// Enum ASSET_USE for Asset Use
            /// </summary>
            [EnumMember(Value = "Asset Use")]
            ASSET_USE,
            
            /// <summary>
            /// Enum ASSIGNMENTS for Assignments
            /// </summary>
            [EnumMember(Value = "Assignments")]
            ASSIGNMENTS,
            
            /// <summary>
            /// Enum AUDITS for Audits
            /// </summary>
            [EnumMember(Value = "Audits")]
            AUDITS,
            
            /// <summary>
            /// Enum BUSINESS_CONTINUITY for Business Continuity
            /// </summary>
            [EnumMember(Value = "Business Continuity")]
            BUSINESS_CONTINUITY,
            
            /// <summary>
            /// Enum COMMUNICATION for Communication
            /// </summary>
            [EnumMember(Value = "Communication")]
            COMMUNICATION,
            
            /// <summary>
            /// Enum CONFIDENTIALITY for Confidentiality
            /// </summary>
            [EnumMember(Value = "Confidentiality")]
            CONFIDENTIALITY,
            
            /// <summary>
            /// Enum DELIVERABLES for Deliverables
            /// </summary>
            [EnumMember(Value = "Deliverables")]
            DELIVERABLES,
            
            /// <summary>
            /// Enum DELIVERY for Delivery
            /// </summary>
            [EnumMember(Value = "Delivery")]
            DELIVERY,
            
            /// <summary>
            /// Enum DISPUTE_RESOLUTION for Dispute Resolution
            /// </summary>
            [EnumMember(Value = "Dispute Resolution")]
            DISPUTE_RESOLUTION,
            
            /// <summary>
            /// Enum FORCE_MAJEURE for Force Majeure
            /// </summary>
            [EnumMember(Value = "Force Majeure")]
            FORCE_MAJEURE,
            
            /// <summary>
            /// Enum INDEMNIFICATION for Indemnification
            /// </summary>
            [EnumMember(Value = "Indemnification")]
            INDEMNIFICATION,
            
            /// <summary>
            /// Enum INSURANCE for Insurance
            /// </summary>
            [EnumMember(Value = "Insurance")]
            INSURANCE,
            
            /// <summary>
            /// Enum INTELLECTUAL_PROPERTY for Intellectual Property
            /// </summary>
            [EnumMember(Value = "Intellectual Property")]
            INTELLECTUAL_PROPERTY,
            
            /// <summary>
            /// Enum LIABILITY for Liability
            /// </summary>
            [EnumMember(Value = "Liability")]
            LIABILITY,
            
            /// <summary>
            /// Enum PAYMENT_TERMS_BILLING for Payment Terms & Billing
            /// </summary>
            [EnumMember(Value = "Payment Terms & Billing")]
            PAYMENT_TERMS_BILLING,
            
            /// <summary>
            /// Enum PRICING_TAXES for Pricing & Taxes
            /// </summary>
            [EnumMember(Value = "Pricing & Taxes")]
            PRICING_TAXES,
            
            /// <summary>
            /// Enum PRIVACY for Privacy
            /// </summary>
            [EnumMember(Value = "Privacy")]
            PRIVACY,
            
            /// <summary>
            /// Enum RESPONSIBILITIES for Responsibilities
            /// </summary>
            [EnumMember(Value = "Responsibilities")]
            RESPONSIBILITIES,
            
            /// <summary>
            /// Enum SAFETY_AND_SECURITY for Safety and Security
            /// </summary>
            [EnumMember(Value = "Safety and Security")]
            SAFETY_AND_SECURITY,
            
            /// <summary>
            /// Enum SCOPE_OF_WORK for Scope of Work
            /// </summary>
            [EnumMember(Value = "Scope of Work")]
            SCOPE_OF_WORK,
            
            /// <summary>
            /// Enum SUBCONTRACTS for Subcontracts
            /// </summary>
            [EnumMember(Value = "Subcontracts")]
            SUBCONTRACTS,
            
            /// <summary>
            /// Enum TERM_TERMINATION for Term & Termination
            /// </summary>
            [EnumMember(Value = "Term & Termination")]
            TERM_TERMINATION,
            
            /// <summary>
            /// Enum WARRANTIES for Warranties
            /// </summary>
            [EnumMember(Value = "Warranties")]
            WARRANTIES
        }

        /// <summary>
        /// The category of the associated element.
        /// </summary>
        [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
        public LabelEnum? Label { get; set; }
        /// <summary>
        /// One or more hashed values that you can send to IBM to provide feedback or receive support.
        /// </summary>
        [JsonProperty("provenance_ids", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ProvenanceIds { get; set; }
    }

}
