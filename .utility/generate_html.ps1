function New-TableOfContents($path)
{
    # Verify the path exists and retrieve the official full path
    $root = (Get-Item $path).FullName

    Write-Output "<!DOCTYPE html>
    <html>
    <head>
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title>IBM Watson Developer Cloud</title>
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" integrity="sha384-1q8mTJOASx8j1Au+a5WDVnPi2lkFfwwEAa8hDDdjZlpLegxhjVME1fgjWPGmkzs7" crossorigin="anonymous">
    </head>
    <body>
    <div class="container">
        <div class="page-header">
            <h1>IBM Watson Developer Cloud .NET Standard SDK</h1>
        </div>
        <p><a href="http://www.ibm.com/watson/developercloud/">Info</a>
            | <a href="http://www.ibm.com/watson/developercloud/doc/index.html">Documentation</a>
        </p>
        <p>Documentation by branch/tag:</p>
        <ul>
"
    Get-ChildItem $root | where { $_.PSIsContainer -eq $true } |% {
        $relPath = $_.FullName.Remove(0, $root.Length + 1)
        $htmlPath = $_.FullName +"/html/index.html"
        Write-Output "<li><a href=`"$htmlPath`">$relPath</a><br /></li>"
    }
    Write-Output "        </ul>
    </div>
    </body>
    </html>"
}
