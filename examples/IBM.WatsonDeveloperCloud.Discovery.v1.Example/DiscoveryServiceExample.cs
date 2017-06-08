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

using System;
using IBM.WatsonDeveloperCloud.Discovery.v1.Model;

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Example
{
    public class DiscoveryServiceExample
    {
        private DiscoveryService _discovery;
        private string _createdEnvironmentId;

        #region Constructor
        public DiscoveryServiceExample(string username, string password)
        {
            _discovery = new DiscoveryService(username, password, DiscoveryService.DISCOVERY_VERSION_DATE_2016_12_01);
            //_discovery.Endpoint = "http://localhost:1234";

            GetEnvironments();
            CreateEnvironment();
            GetEnvironment();
            UpdateEnvironment();
            DeleteEnvironment();

            Console.WriteLine("\n~ Discovery examples complete.");
        }
        #endregion

        #region Environments
        public void GetEnvironments()
        {
            Console.WriteLine(string.Format("\nCalling GetEnvironments()..."));
            var result = _discovery.ListEnvironments();

            if (result != null)
            {
                if(result.Environments != null && result.Environments.Count > 0)
                {
                    foreach(ModelEnvironment environment in result.Environments)
                    {
                        Console.WriteLine(string.Format("Environment name: {0} | id: {1} | status: {2} | description: {3}", environment.Name, environment.EnvironmentId, environment.Status, environment.Description));
                    }
                }
                else
                {
                    Console.WriteLine("There are no environments.");
                }
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }

        public void CreateEnvironment()
        {
            CreateEnvironmentRequest createEnvironmentRequest = new CreateEnvironmentRequest()
            {
                Name = "dotnet-test-environment",
                Description = "Environment created in the .NET SDK Examples"
            };

            Console.WriteLine(string.Format("\nCalling CreateEnvironment()..."));
            var result = _discovery.CreateEnvironment(createEnvironmentRequest);

            if(result != null)
            {
                Console.WriteLine(string.Format("Environment name: {0} | id: {1} | status: {2} | description: {3}", result.Name, result.EnvironmentId, result.Status, result.Description));
                _createdEnvironmentId = result.EnvironmentId;
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }

        public void GetEnvironment()
        {
            Console.WriteLine(string.Format("\nCalling GetEnvironment()..."));
            var result = _discovery.GetEnvironment(_createdEnvironmentId);

            if (result != null)
            {
                Console.WriteLine(string.Format("Environment name: {0} | id: {1} | status: {2} | description: {3}", result.Name, result.EnvironmentId, result.Status, result.Description));
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }

        public void UpdateEnvironment()
        {
            Console.WriteLine(string.Format("\nCalling UpdateEnvironment()..."));

            UpdateEnvironmentRequest updateEnvironmentRequest = new UpdateEnvironmentRequest()
            {
                Name = "dotnet-test-environment-updated",
                Description = "Environment created in the .NET SDK Examples-updated"
            };

            var result = _discovery.UpdateEnvironment(_createdEnvironmentId, updateEnvironmentRequest);

            if (result != null)
            {
                Console.WriteLine(string.Format("Environment name: {0} | id: {1} | status: {2} | description: {3}", result.Name, result.EnvironmentId, result.Status, result.Description));
                _createdEnvironmentId = result.EnvironmentId;
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }

        public void DeleteEnvironment()
        {
            Console.WriteLine(string.Format("\nCalling DeleteEnvironment()..."));
            var result = _discovery.DeleteEnvironment(_createdEnvironmentId);

            if(result != null)
            {
                Console.WriteLine(string.Format("{0} deleted.", _createdEnvironmentId));
                _createdEnvironmentId = null;
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }
        #endregion
    }
}
