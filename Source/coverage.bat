@echo off
if not exist ".\GeneratedReports" mkdir ".\GeneratedReports"

IF EXIST ".\GeneratedReports\results.trx" del ".\GeneratedReports\results.trx"
 
REM Remove any previously created test output directories
CD %~dp0
FOR /D /R %%X IN (%USERNAME%*) DO RD /S /Q "%%X"
 
REM Run the tests against the targeted output
call :RunOpenCoverUnitTestMetrics
 
REM Generate the report output based on the test results
if %errorlevel% equ 0 (
 call :RunReportGeneratorOutput
)
 
REM Launch the report
if %errorlevel% equ 0 (
 call :RunLaunchReport
)
exit /b %errorlevel%
 
:RunOpenCoverUnitTestMetrics
.\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -target:"C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\Common7\IDE\mstest.exe" -register:user -targetargs:"/testcontainer:\"Expressive.Tests\bin\Debug\net45\Expressive.Tests.dll\" /resultsfile:\".\GeneratedReports\results.trx\"" -filter:"+[Expressive*]* -[Expressive.Tests]*" -mergebyhash -skipautoprops -output:".\GeneratedReports\Expressive.xml"

exit /b %errorlevel%

C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\IDE
 
:RunReportGeneratorOutput
.\packages\ReportGenerator.3.1.2\tools\ReportGenerator.exe -reports:".\GeneratedReports\Expressive.xml" -targetdir:".\GeneratedReports\Output"

exit /b %errorlevel%
 
:RunLaunchReport
start "report" "%~dp0\GeneratedReports\Output\index.htm"
exit /b %errorlevel%