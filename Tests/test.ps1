Import-Module .\bin\Debug\netstandard2.0\OnlyHuman.Text.dll
Get-Content -Encoding utf8 .\Tests\testdata.txt | Join-Lines