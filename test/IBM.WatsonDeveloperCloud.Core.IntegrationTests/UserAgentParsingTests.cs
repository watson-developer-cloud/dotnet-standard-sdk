/**
* Copyright 2019 IBM Corp. All Rights Reserved.
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

using IBM.WatsonDeveloperCloud.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace IBM.WatsonDeveloperCloud.Core.IntegrationTests
{
    [TestClass]
    public class UserAgentParsingTests
    {
        [TestMethod]
        public void TestGetDefaultHeaders()
        {
            Dictionary<string, string> sdkHeaders = new Dictionary<string, string>();
            string osInfo = RuntimeInformation.OSDescription;
            string os = Utility.GetOs(osInfo);
            string osVersion = Utility.GetVersion(osInfo);
            string frameworkDescription = RuntimeInformation.FrameworkDescription.Trim();

            sdkHeaders.Add("User-Agent", string.Format(
                    "{0} {1} {2} {3}",
                    Constants.SDK_VERSION,
                    Utility.CleanupUserAgentString(os),
                    Utility.CleanupUserAgentString(osVersion),
                    Utility.CleanupUserAgentString(frameworkDescription)
                ));
            Assert.IsTrue(sdkHeaders.Count == 1);
            Assert.IsTrue(sdkHeaders.ContainsKey("User-Agent"));
            Assert.IsTrue(sdkHeaders["User-Agent"].Contains(Constants.SDK_VERSION));
            Assert.IsFalse(sdkHeaders["User-Agent"].Contains("("));
            Assert.IsFalse(sdkHeaders["User-Agent"].Contains(")"));
            Assert.IsFalse(sdkHeaders["User-Agent"].Contains(":"));
            Assert.IsFalse(sdkHeaders["User-Agent"].Contains(";"));
            Assert.IsFalse(sdkHeaders["User-Agent"].Contains("#"));
            Assert.IsFalse(sdkHeaders["User-Agent"].Contains("~"));
            Assert.IsTrue(sdkHeaders["User-Agent"].Split().Length == 4);
        }

        [TestMethod]
        public void GetVersionWindows()
        {
            string osDescription = "Microsoft Windows 10.0.17134 ";
            string osVersion = Utility.GetVersion(osDescription);
            Assert.IsTrue(osVersion == "10.0.17134");
        }

        [TestMethod]
        public void GetVersionOSX()
        {
            string osDescription = "Darwin 17.7.0 Darwin Kernel Version 17.7.0: Fri Nov  2 20:43:16 PDT 2018; root:xnu-4570.71.17~1/RELEASE_X86_64";
            string osVersion = Utility.GetVersion(osDescription);
            Assert.IsTrue(osVersion == "17.7.0");
        }

        [TestMethod]
        public void GetVersionLinux()
        {
            string osDescription = "Linux 4.19.28-1-MANJARO #1 SMP PREEMPT Sun Mar 10 08:32:42 UTC 2019";
            string osVersion = Utility.GetVersion(osDescription);
            Assert.IsTrue(osVersion == "4.19.28");
        }
    }
}
