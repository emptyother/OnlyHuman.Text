Import-Module ..\bin\Debug\netstandard2.0\OnlyHuman.Text.dll
Get-Content -Encoding utf8 .\testdata.txt | Join-Lines