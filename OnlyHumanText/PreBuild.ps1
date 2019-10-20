using namespace System.IO

[CmdLetBinding()]
param (
	[Parameter()][string] $ProjectName,
	[Parameter()][string] $TargetFileName,
	[Parameter()][System.IO.DirectoryInfo] $ProjectDir,
	[Parameter()][System.IO.DirectoryInfo] $TargetDir,
	[Parameter()][Uri] $ProjectUri,
	[Parameter()][string] $Author,
	[Parameter()][string] $Company,
	[Parameter()][string] $Copyright,
	[Parameter()][Version] $Version,
	[Parameter()][string] $Tags,
	[Parameter()][string] $ReleaseNotes,
	[Parameter()][string] $Description
)
Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

[uri]$HelpUri = "https://github.com/emptyother/OnlyHuman.Text"
[uri]$LicenseUri = "https://choosealicense.com/licenses/isc/"

$Psd1Path = Join-Path $ProjectDir "$ProjectName.psd1"
$targetDll = [DirectoryInfo](Join-Path $TargetDir $TargetFileName)
$RelativePath = [Path]::GetRelativePath($targetDll.Parent.Parent, $targetDll)
$RelativePath = Join-Path "lib" $RelativePath

$moduleSettings = @{
	AliasesToExport   = @()
	CmdletsToExport   = '*'
	FunctionsToExport = @()
	Guid              = '8707fce4-44b8-4642-8437-b4d935731bc7'
	PowerShellVersion = '6.0.0'
	RootModule        = "$RelativePath"
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
