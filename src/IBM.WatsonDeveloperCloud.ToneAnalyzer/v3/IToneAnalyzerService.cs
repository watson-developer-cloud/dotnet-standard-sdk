using System.Collections.Generic;
using IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Models;

namespace IBM.WatsonDeveloperCloud.ToneAnalyzer.v3
{
    public interface IToneAnalyzerService
    {
        /// <summary>
        /// Analyzes the tone of a piece of text. The message is analyzed for several tones - social, emotional, and language. For each tone, various traits are derived. For example, conscientiousness, agreeableness, and openness.
        /// </summary>
        /// <param name="text">Text that contains the content to be analyzed.</param>
        /// <returns></returns>
        ToneAnalysis AnalyzeTone(string text);

        /// <summary>
        /// Analyzes the tone of a piece of text. The message is analyzed for several tones - social, emotional, and language. For each tone, various traits are derived. For example, conscientiousness, agreeableness, and openness.
        /// </summary>
        /// <param name="text">Text that contains the content to be analyzed.</param>
        /// <param name="filterTones">Filter the results by a specific tone. Valid values for tones are emotion, language, and social.</param>
        /// <returns></returns>
        ToneAnalysis AnalyzeTone(string text, List<Tone> filterTones);

        /// <summary>
        /// Analyzes the tone of a piece of text. The message is analyzed for several tones - social, emotional, and language. For each tone, various traits are derived. For example, conscientiousness, agreeableness, and openness.
        /// </summary>
        /// <param name="text">Text that contains the content to be analyzed.</param>
        /// <param name="filterTones">Filter the results by a specific tone. Valid values for tones are emotion, language, and social.</param>
        /// <param name="sentences">Filter your response to remove the sentence level analysis. Valid values for sentences are true and false. This parameter defaults to true when it's not set, which means that a sentence level analysis is automatically provided. Change sentences=false to filter out the sentence level analysis.</param>
        /// <returns></returns>
        ToneAnalysis AnalyzeTone(string text, List<Tone> filterTones, bool sentences = true);
    }
}