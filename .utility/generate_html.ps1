function New-TableOfContents($path)
{
    # Verify the path exists and retrieve the official full path
    $root = (Get-Item $path).FullName

    Write-Output "<!DOCTYPE html>
<html>
<head>
    <meta charset=`"utf-8`">
    <meta http-equiv=`"X-UA-Compatible`" content=`"IE=edge`">
    <meta name=`"viewport`" content=`"width=device-width, initial-scale=1`">
    <title>IBM Watson Developer Cloud</title>
    <link rel=`"stylesheet`" href=`"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css`" integrity=`"sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u`" crossorigin=`"anonymous`">
</head>
<body>
<div class=`"container`">
    <div class=`"page-header`">
        <h1>IBM Watson Developer Cloud .NET Standard SDK</h1>
    </div>
    <p><a href=`"https://www.ibm.com/watson/developer/`">Info</a>
        | <a href=`"https://cloud.ibm.com/developer/watson/documentation`">Documentation</a>
    </p>
    <p>Documentation by branch/tag:</p>
    <ul>
"
    # For each child item that is a directory, create a link containing the relative path
    Get-ChildItem $root | where { $_.PSIsContainer -eq $true } |% {
        # Strip the root out of the path name (including the trailing slash)
        # in order to create a hyperlink that can be used from any location
        $relPath = $_.FullName.Remove(0, $root.Length + 1)
        $htmlPath = "docs/" + $_.Name +"/html/index.html"
        # Write the link out - this is where you could get fancier with what you output
        # For example, you could include the last modified date/time, etc.
        Write-Output "<li><a href=`"$htmlPath`">$relPath</a><br /></li>"
    }
    Write-Output "        </ul>
</div>
</body>
</html>"
}
