[CmdLetBinding()]
param (
	[Parameter()][string] $ProjectName,
	[Parameter()][string] $TargetFileName,
	[Parameter()][System.IO.DirectoryInfo] $TargetDir,
	[Parameter()][Uri] $ProjectUri,
	[Parameter()][string] $Author,
	[Parameter()][string] $Company,
	[Parameter()][string] $Copyright,
	[Parameter()][Version] $Version,
	[Parameter()][string] $Tags,
	[Parameter()][Uri] $IconUri,
	[Parameter()][string] $ReleaseNotes,
	[Parameter()][string] $Description
)
Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

[uri]$HelpUri = "https://github.com/emptyother/OnlyHuman.Text"
[uri]$LicenseUri = "https://choosealicense.com/licenses/isc/"

$Psd1Filename = "$ProjectName.psd1"
$Psd1Path = Join-Path $TargetDir $Psd1Filename

$moduleSettings = @{
	AliasesToExport   = @()
	CmdletsToExport   = '*'
	FunctionsToExport = @()
	Guid              = '8707fce4-44b8-4642-8437-b4d935731bc7'
	PowerShellVersion = '6.0.0'
	RootModule        = "$TargetFileName"
	VariablesToExport = @()
	ClrVersion        = '4.0'
	Path              = "$Psd1Path"
}
if ($ProjectUri.ToString()) {
	if (!$ProjectUri.IsAbsoluteUri) { Throw "ProjectUri should not be a relative URI." }
	$moduleSettings.Add("ProjectUri", $ProjectUri)
}
if ($HelpUri.ToString()) {
	if (!$HelpUri.IsAbsoluteUri) { Throw "HelpUri should not be a relative URI." }
	$moduleSettings.Add("HelpInfoUri", $HelpUri)
}
if ($Description) { $moduleSettings.Add("Description", $Description) }
if ($Version) { $moduleSettings.Add("ModuleVersion", $Version) }
if ($Copyright) { $moduleSettings.Add("Copyright", $Copyright) }
if ($Company) { $moduleSettings.Add("CompanyName", $Company) }
if ($Author) { $moduleSettings.Add("Author", $Author) }
if ($IconUri.ToString()) {
	if (!$IconUri.IsAbsoluteUri) { Throw "IconUri should not be a relative URI." }
	$moduleSettings.Add("IconUri", $IconUri)
}
if ($ReleaseNotes) { $moduleSettings.Add("ReleaseNotes", $ReleaseNotes) }
if ($LicenseUri.ToString()) {
	if ($LicenseUri.IsAbsoluteUri) { 
		$moduleSettings.Add("LicenseUri", $LicenseUri)
	}
}
if ($Tags) {
	$tagcollection = $Tags.Split(",") | ForEach-Object {
		$_.Trim()
	}
	$moduleSettings.Add("Tags", $tagcollection)
}
New-ModuleManifest @moduleSettings
$filenamelist = Get-ChildItem $TargetDir | ForEach-Object { $_.Name }
Update-ModuleManifest -Path $Psd1Path -FileList $filenamelist