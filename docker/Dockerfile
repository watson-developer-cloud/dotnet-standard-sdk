# Import .NET SDK base image (change version as needed)
FROM mcr.microsoft.com/dotnet/core/sdk:2.2

#Set working directory to src
WORKDIR /src

# Create new .net project (console project, change as needed)
RUN dotnet new console

# Install SDK packages
RUN dotnet add package IBM.Watson.SpeechToText.v1 --version 3.3.0 && \
    dotnet add package IBM.Cloud.SDK.Core --version 0.8.2 && \
    dotnet add package IBM.Watson.TextToSpeech.v1 --version 3.3.0 && \
    dotnet add package IBM.Watson.NaturalLanguageUnderstanding.v1 --version 3.3.0 && \
    dotnet add package IBM.Watson.VisualRecognition.v3 --version 3.3.0 && \
    dotnet add package IBM.Watson.NaturalLanguageClassifier.v1 --version 3.3.0 && \
    dotnet add package IBM.Watson.Discovery.v1 --version 3.3.0 && \
    dotnet add package IBM.Watson.LanguageTranslator.v3 --version 3.3.0 && \
    dotnet add package IBM.Watson.PersonalityInsights.v3 --version 3.3.0 && \
    dotnet add package IBM.Watson.ToneAnalyzer.v3 --version 3.3.0 && \
    dotnet add package IBM.Watson.CompareComply.v1 --version 3.3.0 && \
    dotnet add package IBM.Watson.Common --version 3.0.1 && \
    dotnet add package IBM.Watson.Assistant.v1 --version 3.3.0 && \
    dotnet add package IBM.Watson.Assistant.v2 --version 3.3.0 
