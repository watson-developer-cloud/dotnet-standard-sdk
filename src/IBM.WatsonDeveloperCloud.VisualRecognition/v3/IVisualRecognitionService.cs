using IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model;

namespace IBM.WatsonDeveloperCloud.VisualRecognition.v3
{
    public interface IVisualRecognitionService
    {
        DetectedFaces DetectFaces(VisualRecognitionOptions options);
    }
}
