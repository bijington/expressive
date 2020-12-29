param($WorkingDirectory, [Switch]$DontShowReport)

Set-StrictMode -Version Latest

if ($null -eq $WorkingDirectory) {
    $WorkingDirectory = $PSScriptRoot
}

# Currently requires manually installing the item below
dotnet tool install --global dotnet-reportgenerator-globaltool --version 4.8.2

# Compile the source
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# Generate the coverage report
reportgenerator -reports:"$WorkingDirectory\**\coverage.netcoreapp3.1.opencover.xml" -targetdir:"$WorkingDirectory\coverage" -reporttypes:html

if ($DontShowReport) {
    return
}

Start-Process "$WorkingDirectory\coverage\index.htm"