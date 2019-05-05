if(Test-Path -Path coverage)
{
  Remove-Item .\coverage -recurse
}

dotnet restore

if((Test-Path -Path packages))
{
    Remove-Item .\packages -recurse
}

New-Item -path . -name packages -itemtype directory
nuget install -Verbosity quiet -OutputDirectory packages -Version 4.7.922 OpenCover
nuget install -Verbosity quiet -OutputDirectory packages -Version 4.1.4 ReportGenerator

New-Item -path . -name coverage -itemtype directory
Copy-Item .\test\VisualRecognition.v3.IntegrationTests\VisualRecognitionTestData .\VisualRecognitionTestData -recurse
Copy-Item .\test\SpeechToText.v1.IntegrationTests\SpeechToTextTestData .\SpeechToTextTestData -recurse
Copy-Item .\test\Discovery.v1.IntegrationTests\DiscoveryTestData .\DiscoveryTestData -recurse
Copy-Item .\test\CompareComply.v1.IntegrationTests\CompareComplyTestData .\CompareComplyTestData -recurse

$openCover = '.\packages\OpenCover.4.7.922\tools\OpenCover.Console.exe'

ForEach ($folder in (Get-ChildItem -Path .\test -Directory)) 
{
    $targetArgs = '-targetargs: test ' + $folder.FullName + ' -c Release -f netcoreapp2.0'
    $filter = '-filter:+[*.IntegrationTests]*+[*.UnitTests]*-[*Tests*]*-[*Example*]*'
    & $openCover '-target:C:\\Program Files\\dotnet\\dotnet.exe' $targetArgs '-register:user' $filter '-oldStyle' '-mergeoutput' '-hideskipped:File' '-searchdirs:$testdir\\bin\\release\\netcoreapp2.0' '-output:coverage\\coverage.xml'
}

$reportGenerator = '.\packages\ReportGenerator.4.1.4\tools\ReportGenerator.exe'
& $reportGenerator -reports:coverage\coverage.xml -targetdir:coverage -verbosity:Error

Remove-Item .\VisualRecognitionTestData -recurse
Remove-Item .\SpeechToTextTestData -recurse
Remove-Item .\DiscoveryTestData -recurse
Remove-Item .\CompareComplyTestData -recurse
Remove-Item .\packages -recurse