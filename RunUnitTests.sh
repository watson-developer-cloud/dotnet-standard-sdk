#! /bin/sh

if [ -d "coverage" ]; then
    rm -rf coverage
fi

dotnet test test/VisualRecognition.v4.UnitTests /p:CollectCoverage=true /p:CoverletOutputFormat=\"opencover,lcov\" /p:CoverletOutput=../../coverage/lcov
dotnet ~/.nuget/packages/reportgenerator/4.1.5/tools/netcoreapp2.0/ReportGenerator.dll "-reports:coverage/lcov.opencover.xml" "-targetdir:coverage/report"