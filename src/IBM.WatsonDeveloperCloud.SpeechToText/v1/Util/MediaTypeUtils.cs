using System.IO;
using IBM.WatsonDeveloperCloud.Http;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Util
{
    public static class MediaTypeUtils
    {
        public static string GetMediaTypeFromFile(this FileStream audio)
        {
            string contentType = string.Empty;
            string fileExtension = Path.GetExtension(audio.Name);

            if (fileExtension.Equals(".wav"))
                contentType = HttpMediaType.AUDIO_WAV;
            else if (fileExtension.Equals(".ogg"))
                contentType = HttpMediaType.AUDIO_OGG;
            else if (fileExtension.Equals(".oga"))
                contentType = HttpMediaType.AUDIO_OGG;
            else if (fileExtension.Equals(".flac"))
                contentType = HttpMediaType.AUDIO_FLAC;
            else if (fileExtension.Equals(".raw"))
                contentType = HttpMediaType.AUDIO_RAW;

            return contentType;
        }
    }
}