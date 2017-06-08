if((Test-Path -Path coverage))
{
  rm coverage -r -force
}

dotnet restore

New-Item -path . -name coverage -itemtype directory
Copy-Item .\test\IBM.WatsonDeveloperCloud.VisualRecognition.v3.IntegrationTests\VisualRecognitionTestData .\VisualRecognitionTestData -recurse
Copy-Item .\test\IBM.WatsonDeveloperCloud.SpeechToText.v1.IntegrationTests\SpeechToTextTestData .\SpeechToTextTestData -recurse
$openCover = '.\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe'

ForEach($projectFolder in (Get-ChildItem -Path .\src -Directory))
{
    $projectJson = $projectFolder.FullName + '\project.json'
    echo 'building ' $projectFolder.Name
    dotnet build $projectJson
}

ForEach ($folder in (Get-ChildItem -Path .\test -Directory)) 
{ 
    if(!$folder.Name.Contains("Discovery") -and !$folder.Name.Contains("TextToSpeech"))
    {  
        $targetArgs = '-targetargs: test \"' + $folder.FullName + '\" -c Debug -f netcoreapp1.0'
        $filter = '-filter:+[IBM.WatsonDeveloperCloud*]*-[*Tests*]*-[*Example*]*'
        & $openCover '-target:C:\Program Files\dotnet\dotnet.exe' $targetArgs '-register:user' $filter '-oldStyle' '-mergeoutput' '-hideskipped:File' '-searchdirs:$testdir\bin\Debug\netstandard1.6' '-output:coverage\coverage.xml'
    }
}

Remove-Item .\VisualRecognitionTestData -recurse
Remove-Item .\SpeechToTextTestData -recurse

$reportGenerator = '.\packages\ReportGenerator.2.4.5.0\tools\ReportGenerator.exe'
& $reportGenerator -reports:coverage\coverage.xml -targetdir:coverage -verbosity:Error