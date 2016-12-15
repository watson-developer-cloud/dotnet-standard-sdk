using IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model;
using System.Collections.Generic;
using System.IO;

namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1
{
    interface ITextToSpeechService
    {
        List<Voice> GetVoices();
        Voice GetVoice(string voiceName);
        Pronunciation GetPronunciation(string text);
        Pronunciation GetPronunciation(string text, Voice voice, Phoneme phoneme);
        Stream Synthesize(string text, Voice voice, AudioType audioType);
        List<CustomVoiceModel> GetCustomVoiceModels(string language);
        CustomVoiceModel GetCustomVoiceModel(string modelId);
        CustomVoiceModel SaveCustomVoiceModel(CustomVoiceModel model);
        void DeleteCustomVoiceModel(CustomVoiceModel model);
        List<CustomWordTranslation> GetWords(CustomVoiceModel model);
        void SaveWords(CustomVoiceModel model, params CustomWordTranslation[] translations);
        void DeleteWord(CustomVoiceModel model, CustomWordTranslation translation);
    }

}
