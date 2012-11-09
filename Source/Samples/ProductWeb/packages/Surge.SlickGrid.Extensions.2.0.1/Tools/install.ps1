param($installPath, $toolsPath, $package, $project)

$readme = (Join-Path (Join-Path $toolsPath ..) ReadMe.md)
$DTE.ItemOperations.OpenFile($readme)