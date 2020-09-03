dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

.\packages\ReportGenerator.3.1.2\tools\ReportGenerator.exe -reports:".\Expressive.Tests\coverage.netcoreapp3.0.opencover.xml" -targetdir:".\GeneratedReports\Output"

Start-Process .\GeneratedReports\Output\index.htm