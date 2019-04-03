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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace IBM.Watson.IntegrationTests
{
    [TestClass]
    public class CommonIntegrationTests
    {
        [TestMethod]
        public void TestGetDefaultHeaders()
        {
            Dictionary<string, string> sdkHeaders = Common.GetSdkHeaders("bogusServiceName", "bogusServiceVersion", "bogusOperationid");
            Assert.IsTrue(sdkHeaders.Count == 2);
            Assert.IsTrue(sdkHeaders.ContainsKey("X-IBMCloud-SDK-Analytics"));
            Assert.IsTrue(sdkHeaders.ContainsKey("User-Agent"));
            Assert.IsTrue(sdkHeaders["X-IBMCloud-SDK-Analytics"] == "service_name=bogusServiceName;service_version=bogusServiceVersion;operation_id=bogusOperationid");
            Assert.IsTrue(sdkHeaders["User-Agent"].Contains(Common.Version));
            Assert.IsFalse(sdkHeaders["User-Agent"].Contains("("));
            Assert.IsFalse(sdkHeaders["User-Agent"].Contains(")"));
            Assert.IsFalse(sdkHeaders["User-Agent"].Contains(":"));
            Assert.IsFalse(sdkHeaders["User-Agent"].Contains(";"));
            Assert.IsFalse(sdkHeaders["User-Agent"].Contains("#"));
            Assert.IsFalse(sdkHeaders["User-Agent"].Contains("~"));
            Assert.IsTrue(sdkHeaders["User-Agent"].Split(" ").Length == 4);
        }

        [TestMethod]
        public void GetVersionWindows()
        {
            string osDescription = "Microsoft Windows 10.0.17134 ";
            string osVersion = Common.GetVersion(osDescription);
            Assert.IsTrue(osVersion == "10.0.17134");
        }

        [TestMethod]
        public void GetVersionOSX()
        {
            string osDescription = "Darwin 17.7.0 Darwin Kernel Version 17.7.0: Fri Nov  2 20:43:16 PDT 2018; root:xnu-4570.71.17~1/RELEASE_X86_64";
            string osVersion = Common.GetVersion(osDescription);
            Assert.IsTrue(osVersion == "17.7.0");
        }

        [TestMethod]
        public void GetVersionLinux()
        {
            string osDescription = "4.19.28-1-MANJARO#1SMPPREEMPTSunMar1008:32:42UTC2019";
            string osVersion = Common.GetVersion(osDescription);
            Assert.IsTrue(osVersion == "4.19.28");
        }
    }
}
