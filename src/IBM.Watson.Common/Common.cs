/**
* (C) Copyright IBM Corp. 2019, 2021.
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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace IBM.Watson
{
    public partial class Common
    {
        /// <summary>
        /// The SDK version.
        /// </summary>
        public const string Version = "watson-apis-dotnet-standard-sdk-5.2.0";
        private static string os;
        private static string osVersion;
        private static string frameworkDescription;

        /// <summary>
        /// Returns a set of default headers to use with each request.
        /// </summary>
        /// <param name="serviceName">The service name to be used in X-IBMCloud-SDK-Analytics header.</param>
        /// <param name="serviceVersion">The service version to be used in X-IBMCloud-SDK-Analytics header.</param>
        /// <param name="operation">The operation name to be used in X-IBMCloud-SDK-Analytics header.</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetSdkHeaders(string serviceName, string serviceVersion, string operationId)
        {
            Dictionary<string, string> sdkHeaders = new Dictionary<string, string>();
            sdkHeaders.Add("X-IBMCloud-SDK-Analytics", 
                string.Format("service_name={0};service_version={1};operation_id={2}", 
                serviceName, 
                serviceVersion, 
                operationId));

            if (string.IsNullOrEmpty(os) || string.IsNullOrEmpty(osVersion))
            {
                string osInfo = RuntimeInformation.OSDescription;
                os = GetOs(osInfo);
                osVersion = GetVersion(osInfo);
            }
            if (string.IsNullOrEmpty(frameworkDescription))
            {
                frameworkDescription = RuntimeInformation.FrameworkDescription.Trim();
            }

            sdkHeaders.Add("User-Agent",
                string.Format(
                    "{0} {1} {2} {3}",
                    Version,
                    CleanupString(os),
                    CleanupString(osVersion),
                    CleanupString(frameworkDescription)
                ));
            return sdkHeaders;
        }

        public static string GetVersion(string input)
        {
            Regex pattern = new Regex("\\d+(\\.\\d+)+");
            Match m = pattern.Match(input);
            return m.Value;
        }

        public static string GetOs(string input)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return "MacOS";
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return "Windows";
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return "Linux";
            }

            return input;
        }

        private static string CleanupString(string input)
        {
            Regex pattern = new Regex("[;:#()~/ ]");
            return pattern.Replace(input, "-");
        }
    }
}
