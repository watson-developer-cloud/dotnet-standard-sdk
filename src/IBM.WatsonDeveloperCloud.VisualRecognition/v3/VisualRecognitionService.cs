using System;
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model;

namespace IBM.WatsonDeveloperCloud.VisualRecognition.v3
{
    public class VisualRecognitionService : WatsonService, IVisualRecognitionService
    {
        const string PATH_MESSAGE = "/v1/workspaces/{0}/message";
        const string VERSION_DATE_2016_07_11 = "2016-07-11";
        const string SERVICE_NAME = "conversation";
        const string URL = "https://gateway.watsonplatform.net/conversation/api";

        public VisualRecognitionService()
            : base(SERVICE_NAME)
        {

        }

        public DetectedFaces DetectFaces(VisualRecognitionOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
