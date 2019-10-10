if(Test-Path -Path coverage)
{
  Remove-Item .\coverage -recurse
}

dotnet restore

if(-Not (Test-Path -Path '\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe'))
{
    nuget install -Verbosity quiet -OutputDirectory packages -Version 4.6.519 OpenCover
}
$openCover = '.\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe'

if(-Not (Test-Path -Path '\packages\ReportGenerator.2.4.5.0\tools\ReportGenerator.exe'))
{
    nuget install -Verbosity quiet -OutputDirectory packages -Version 2.4.5.0 ReportGenerator
}
$reportGenerator = '.\packages\ReportGenerator.2.4.5.0\tools\ReportGenerator.exe'

New-Item -path . -name coverage -itemtype directory

Copy-Item .\test\VisualRecognition.v4.IntegrationTests\VisualRecognitionTestData .\VisualRecognitionTestData -recurse
Copy-Item .\test\SpeechToText.v1.IntegrationTests\SpeechToTextTestData .\SpeechToTextTestData -recurse
Copy-Item .\test\Discovery.v1.IntegrationTests\DiscoveryTestData .\DiscoveryTestData -recurse
Copy-Item .\test\CompareComply.v1.IntegrationTests\CompareComplyTestData .\CompareComplyTestData -recurse
Copy-Item .\test\NaturalLanguageClassifier.v1.IntegrationTests\NaturalLanguageClassifierTestData .\NaturalLanguageClassifierTestData - recurse


ForEach ($folder in (Get-ChildItem -Path .\test -Directory)) 
{
    if(-Not ($folder.fullName -contains 'unit'))
    {
        $targetArgs = '-targetargs: test ' + $folder.FullName + ' -c Release -f netcoreapp2.0'
        $filter = '-filter:+[IBM.Watson*]*-[*Tests*]*-[*Example*]*'
        & $openCover '-target:C:\\Program Files\\dotnet\\dotnet.exe' $targetArgs '-register:user' $filter '-oldStyle' '-mergeoutput' '-hideskipped:File' '-searchdirs:$testdir\\bin\\release\\netcoreapp2.0' '-output:coverage\\coverage.xml'
    }
}

& $reportGenerator -reports:coverage\coverage.xml -targetdir:coverage -verbosity:Error

Remove-Item .\VisualRecognitionTestData -recurse
Remove-Item .\SpeechToTextTestData -recurse
Remove-Item .\DiscoveryTestData -recurse
Remove-Item .\CompareComplyTestData -recurse
Remove-Item .\NaturalLanguageClassifierTestData -recurse
Remove-Item .\packages -recurse