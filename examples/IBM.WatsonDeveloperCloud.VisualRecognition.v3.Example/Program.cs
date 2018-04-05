using System;
using System.IO;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3;

namespace GetCoreMLModelExample
{
    class Program
    {
        static void Main(string[] args)
        {
            GetCoreMLModel getCoreMLModel = new GetCoreMLModel();
            getCoreMLModel.Init();
        }
    }

    class GetCoreMLModel
    {
        private string _apikey = "your-apikey";
        private string _classifierId = "your-classifier-id";
        private string _downloadFilePath = "your-path";

        public void Init()
        {
            VisualRecognitionService visualRec = new VisualRecognitionService(_apikey, "2016-05-20");
            Console.WriteLine("getting Core ML model...");
            var data = visualRec.GetCoreMlModel(_classifierId);

            using (Stream file = File.Create(string.Format("{0}{1}.mlmodel", _downloadFilePath, _classifierId)))
            {
                CopyStream(data.Result, file);
            }

            Console.WriteLine("Model downloaded to {0}!", _downloadFilePath);
            Console.ReadKey();
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }
    }
}
