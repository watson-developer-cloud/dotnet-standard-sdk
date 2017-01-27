#!/bin/bash

set -e

# Install OpenCover and ReportGenerator, and save the path to their executables.
nuget install -Verbosity quiet -OutputDirectory packages -Version 4.6.519 OpenCover
nuget install -Verbosity quiet -OutputDirectory packages -Version 2.4.5.0 ReportGenerator

OPENCOVER=$PWD/packages/OpenCover.4.6.519/tools/OpenCover.Console.exe
REPORTGENERATOR=$PWD/packages/ReportGenerator.2.4.5.0/tools/ReportGenerator.exe

CONFIG=Release
# Arguments to use for the build
DOTNET_BUILD_ARGS="-c $CONFIG"
# Arguments to use for the test
DOTNET_TEST_ARGS="$DOTNET_BUILD_ARGS"

echo CLI args: $DOTNET_BUILD_ARGS

echo Restoring

dotnet restore -v Warning

echo Building

dotnet build $DOTNET_BUILD_ARGS **/project.json

echo Testing

coverage=./coverage
rm -rf $coverage
mkdir $coverage

mono dotnet test -f netcoreapp1.0 $DOTNET_TEST_ARGS test/IBM.WatsonDeveloperCloud.LanguageTranslator.UnitTests/

echo "Calculating coverage with OpenCover"
mono $OPENCOVER \
  -target:"c:\Program Files\dotnet\dotnet.exe" \
  -targetargs:"test -f netcoreapp1.0 $DOTNET_TEST_ARGS test/IBM.WatsonDeveloperCloud.LanguageTranslator.UnitTests" \
  -mergeoutput \
  -hideskipped:File \
  -output:$coverage/coverage.xml \
  -oldStyle \
  -filter:"+[IBM.WatsonDeveloperCloud.LanguageTranslator*]* -[IBM.WatsonDeveloperCloud.LanguageTranslator.*Tests*]*" \
  -searchdirs:$testdir/bin/$CONFIG/netcoreapp1.0 \
  -register:user

echo "Generating HTML report"
mono $REPORTGENERATOR \
  -reports:$coverage/coverage.xml \
  -targetdir:$coverage \
  -verbosity:Error
