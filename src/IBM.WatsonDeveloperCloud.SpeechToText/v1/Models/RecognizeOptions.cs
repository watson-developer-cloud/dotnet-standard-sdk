using System.Collections.Generic;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Models
{
    public class RecognizeOptions
    {
        public bool Continuous { get; private set; }
        public int InactivityTimeout { get; private set; }
        public string[] Keywords { get; private set; }
        public double KeywordsThreshold { get; private set; }
        public int MaxAlternatives { get; private set; }
        public string Model { get; private set; }
        public string SessionId { get; private set; }
        public bool Timestamps { get; private set; }
        public double WordAlternativesThreshold { get; private set; }
        public bool WordConfidence { get; private set; }
        public bool ProfanityFilter { get; private set; }
        public bool SmartFormatting { get; private set; }

        public RecognizeOptions()
        {
            this.KeywordsThreshold = 0;
            this.MaxAlternatives = 1;
            this.ProfanityFilter = true;
        }

        public RecognizeOptions IsSmartFormatting(bool smartFormatting)
        {
            this.SmartFormatting = smartFormatting;
            return this;
        }

        public RecognizeOptions IsProfanityFilter(bool profanityFilter)
        {
            this.ProfanityFilter = profanityFilter;
            return this;
        }

        public RecognizeOptions IsContinuous(bool continuous)
        {
            this.Continuous = continuous;
            return this;
        }

        public RecognizeOptions WithInactivityTimeout(int inactivityTimeout)
        {
            this.InactivityTimeout = inactivityTimeout;
            return this;
        }

        public RecognizeOptions WithKeywords(string[] keywords)
        {
            this.Keywords = (keywords == null) ? null : (string[])keywords.Clone();
            return this;
        }

        public RecognizeOptions WithKeywordsThreshold(double keywordsThreshold)
        {
            this.KeywordsThreshold = keywordsThreshold;
            return this;
        }

        public RecognizeOptions WithMaxAlternatives(int maxAlternatives)
        {
            this.MaxAlternatives = maxAlternatives;
            return this;
        }

        public RecognizeOptions SetModel(string model)
        {
            this.Model = model;
            return this;
        }

        public RecognizeOptions WithSession(Session session)
        {
            this.SessionId = session.SessionId;
            return this;
        }

        public RecognizeOptions WithSession(string sessionId)
        {
            this.SessionId = sessionId;
            return this;
        }

        public RecognizeOptions IsTimestamps(bool timestamps)
        {
            this.Timestamps = timestamps;
            return this;
        }

        public RecognizeOptions WithWordAlternativesThreshold(double wordAlternativesThreshold)
        {
            this.WordAlternativesThreshold = wordAlternativesThreshold;
            return this;
        }

        public RecognizeOptions WithWordConfidence(bool wordConfidence)
        {
            this.WordConfidence = wordConfidence;
            return this;
        }

        public IDictionary<string, object> GetArguments()
        {
            IDictionary<string, object> arguments = new Dictionary<string, object>();

            arguments.Add("model", string.IsNullOrEmpty(this.Model) ? "en-US_NarrowbandModel" : this.Model);

            if (this.Continuous)
                arguments.Add("continuous", this.Continuous);

            if (this.InactivityTimeout > 0)
                arguments.Add("inactivity_timeout", this.InactivityTimeout);

            if (this.Keywords != null && this.Keywords.Length > 0)
                arguments.Add("keywords", this.Keywords);

            if (this.Keywords != null && this.Keywords.Length > 0 && (this.KeywordsThreshold >= 0 && this.KeywordsThreshold <= 1))
                arguments.Add("keywords_threshold", this.KeywordsThreshold);

            arguments.Add("max_alternatives", this.MaxAlternatives <= 0 ? 1 : this.MaxAlternatives);

            if (this.WordAlternativesThreshold >= 0 && this.WordAlternativesThreshold <= 1)
                arguments.Add("word_alternatives_threshold 	", this.WordAlternativesThreshold);

            if (this.WordConfidence)
                arguments.Add("word_confidence", this.WordConfidence);

            if (this.Timestamps)
                arguments.Add("timestamps", this.Timestamps);

            if (this.ProfanityFilter)
                arguments.Add("profanity_filter", this.ProfanityFilter);

            if (this.SmartFormatting)
                arguments.Add("smart_formatting", this.SmartFormatting);

            return arguments;
        }
    }
}