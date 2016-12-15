using System;

namespace IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model
{
    public class ImageProcessed
    {
        public String SourceUrl { get; set; }
        public String ResolvedUrl { get; set; }
        public String Image { get; set; }
        public ImageProcessingError Error { get; set; }
    }
}
