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

using IBM.Cloud.SDK.Core.Authentication.Cp4d;
using IBM.Watson.NaturalLanguageUnderstanding.v1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IBM.Watson.IntegrationTests
{
    //[TestClass]
    public class Cp4dIntegrationTests
    {
        //[TestMethod]
        public void TestIcp4d_Success()
        {
            var url = "";
            var username = "";
            var password = "";
            var versionDate = "";
            CloudPakForDataAuthenticator cloudPakForDataAuthenticator = new CloudPakForDataAuthenticator(url: url, username: username, password: password, disableSslVerification: true);
            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(version: versionDate, authenticator: cloudPakForDataAuthenticator);
            service.SetServiceUrl("");
            var listWorkspaceResult = service.ListModels();

            Assert.IsNotNull(listWorkspaceResult);
            Assert.IsNotNull(listWorkspaceResult.Result);
            Assert.IsNotNull(listWorkspaceResult.Result.Models);
        }
    }
}
