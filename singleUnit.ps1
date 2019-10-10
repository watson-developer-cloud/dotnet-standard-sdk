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

$targetArgs = '-targetargs: test test\VisualRecognition.v4.UnitTests -c Release -f netcoreapp2.0'
$filter = '-filter:+[IBM.Watson*]*-[*Tests*]*-[*Example*]*'
& $openCover '-target:C:\\Program Files\\dotnet\\dotnet.exe' $targetArgs '-register:user' $filter '-oldStyle' '-mergeoutput' '-hideskipped:File' '-searchdirs:$testdir\\bin\\release\\netcoreapp2.0' '-output:coverage\\coverage.xml'

& $reportGenerator -reports:coverage\coverage.xml -targetdir:coverage -verbosity:Error