pushd %~dp0
pushd ..\src\WinRTUtils.WindowsPhone\
msbuild WinRTUtils.WindowsPhone.csproj /p:Configuration=Release
popd

pushd ..\src\WinRTUtils.Windows\
msbuild WinRTUtils.Windows.csproj /p:Configuration=Release
popd

nuget pack WinRTUtils.nuspec