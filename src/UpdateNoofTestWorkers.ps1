Param(
[string]$browser
)
if ($Browser -eq "cloud" -Or $Browser -eq "browserstack")
{
Write-Host "##vso[task.setvariable variable=NumberOfTestWorkers]1"
Write-Host "NumberOfTestWorkers is set to $NumberOfTestWorkers"
}