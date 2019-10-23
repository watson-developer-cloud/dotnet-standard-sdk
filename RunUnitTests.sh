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