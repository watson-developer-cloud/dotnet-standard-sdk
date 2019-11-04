#! /bin/sh

if [ -d "coverage" ]; then
    rm -rf coverage
fi

dotnet test test/Assistant.v1.UnitTests --logger:trx --results-directory ../../coverage \
    "/p:CollectCoverage=true" \
    "/p:CoverletOutputFormat=\"json,cobertura\"" \
    "/p:MergeWith=../../coverage/coverage.json" \
    "/p:CoverletOutput=../../coverage/"

dotnet test test/Assistant.v2.UnitTests --logger:trx --results-directory ../../coverage \
    "/p:CollectCoverage=true" \
    "/p:CoverletOutputFormat=\"json,cobertura\"" \
    "/p:MergeWith=../../coverage/coverage.json" \
    "/p:CoverletOutput=../../coverage/"

dotnet test test/CompareComply.v1.UnitTests --logger:trx --results-directory ../../coverage \
    "/p:CollectCoverage=true" \
    "/p:CoverletOutputFormat=\"json,cobertura\"" \
    "/p:MergeWith=../../coverage/coverage.json" \
    "/p:CoverletOutput=../../coverage/"

dotnet test test/Discovery.v1.UnitTests --logger:trx --results-directory ../../coverage \
    "/p:CollectCoverage=true" \
    "/p:CoverletOutputFormat=\"json,cobertura\"" \
    "/p:MergeWith=../../coverage/coverage.json" \
    "/p:CoverletOutput=../../coverage/"

dotnet test test/LanguageTranslator.v3.UnitTests --logger:trx --results-directory ../../coverage \
    "/p:CollectCoverage=true" \
    "/p:CoverletOutputFormat=\"json,cobertura\"" \
    "/p:MergeWith=../../coverage/coverage.json" \
    "/p:CoverletOutput=../../coverage/"

dotnet test test/NaturalLanguageClassifier.v1.UnitTests --logger:trx --results-directory ../../coverage \
    "/p:CollectCoverage=true" \
    "/p:CoverletOutputFormat=\"json,cobertura\"" \
    "/p:MergeWith=../../coverage/coverage.json" \
    "/p:CoverletOutput=../../coverage/"

dotnet test test/NaturalLanguageUnderstanding.v1.UnitTests --logger:trx --results-directory ../../coverage \
    "/p:CollectCoverage=true" \
    "/p:CoverletOutputFormat=\"json,cobertura\"" \
    "/p:MergeWith=../../coverage/coverage.json" \
    "/p:CoverletOutput=../../coverage/"

dotnet test test/PersonalityInsights.v3.UnitTests --logger:trx --results-directory ../../coverage \
    "/p:CollectCoverage=true" \
    "/p:CoverletOutputFormat=\"json,cobertura\"" \
    "/p:MergeWith=../../coverage/coverage.json" \
    "/p:CoverletOutput=../../coverage/"

dotnet test test/SpeechToText.v1.UnitTests --logger:trx --results-directory ../../coverage \
    "/p:CollectCoverage=true" \
    "/p:CoverletOutputFormat=\"json,cobertura\"" \
    "/p:MergeWith=../../coverage/coverage.json" \
    "/p:CoverletOutput=../../coverage/"

dotnet test test/TextToSpeech.v1.UnitTests --logger:trx --results-directory ../../coverage \
    "/p:CollectCoverage=true" \
    "/p:CoverletOutputFormat=\"json,cobertura\"" \
    "/p:MergeWith=../../coverage/coverage.json" \
    "/p:CoverletOutput=../../coverage/"

dotnet test test/ToneAnalyzer.v3.UnitTests --logger:trx --results-directory ../../coverage \
    "/p:CollectCoverage=true" \
    "/p:CoverletOutputFormat=\"json,cobertura\"" \
    "/p:MergeWith=../../coverage/coverage.json" \
    "/p:CoverletOutput=../../coverage/"

dotnet test test/VisualRecognition.v3.UnitTests --logger:trx --results-directory ../../coverage \
    "/p:CollectCoverage=true" \
    "/p:CoverletOutputFormat=\"json,cobertura\"" \
    "/p:MergeWith=../../coverage/coverage.json" \
    "/p:CoverletOutput=../../coverage/"

dotnet test test/VisualRecognition.v4.UnitTests --logger:trx --results-directory ../../coverage \
    "/p:CollectCoverage=true" \
    "/p:CoverletOutputFormat=\"json,cobertura\"" \
    "/p:MergeWith=../../coverage/coverage.json" \
    "/p:CoverletOutput=../../coverage/"

dotnet ~/.nuget/packages/reportgenerator/4.1.5/tools/netcoreapp2.0/ReportGenerator.dll "-reports:coverage/coverage.cobertura.xml" "-targetdir:coverage/report"