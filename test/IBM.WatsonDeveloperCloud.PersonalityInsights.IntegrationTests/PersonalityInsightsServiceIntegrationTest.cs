using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Models;

namespace IBM.WatsonDeveloperCloud.PersonalityInsights.IntegrationTests
{
    public class PersonalityInsightsServiceIntegrationTest
    {
        public void GetProfile_Success()
        {
            ProfileOptions.CreateOptions()
                          .WithTextPlain()
                          .AsEnglish()
                          .AcceptJson()
                          .AcceptEnglishLanguage()
                          .WithBody("");

        }
    }
}
