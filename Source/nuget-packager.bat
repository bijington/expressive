echo Building Expressive Solution
"c:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.exe" /build release "Expressive.sln"

echo Creating nuget-package directory
mkdir nuget-package

echo Creating nuget package
Tools\nuget pack Expressive.nuspec -symbols -OutputDirectory .\nuget-package\

pause