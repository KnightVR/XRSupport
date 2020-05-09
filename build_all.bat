@echo off

rem TODO get this from terminal input
set Version="0.1"
set Company="KnightVR"

setlocal
rem Get app name from name of parent folder
set ParentDir=%~dp0
set ParentDir=%ParentDir: =:%
set ParentDir=%ParentDir:\= %
call :getparentdir %ParentDir%
set AppName=%ParentDir::= %

CALL :dequote AppName
CALL :dequote Version
CALL :dequote Company

echo %AppName%
echo %Version%
echo %Company%

echo "Generating Windows 64 installer script..."
cd XRBuild\XRPC\Installer
call gen_installer.bat "%AppName%" "%Version%" "%Company%"
cd ..\..\..\

echo "Building Android App..."
"C:/Program Files/Unity/Hub/Editor/2019.3.13f1/Editor/Unity.exe" -quit -projectPath -build_target Android -executeMethod XRAutobuilder.AndroidBuild -version "%Version%"
echo "Building Windows 64 App..."
"C:/Program Files/Unity/Hub/Editor/2019.3.13f1/Editor/Unity.exe" -quit -projectPath -build_target Win64 -executeMethod XRAutobuilder.Win64Build -version "%Version%"
echo "Compiling Windows 64 installer..."
cd XRBuild\XRPC\Installer
"..\..\Inno\Inno Setup 6\Compil32.exe" /cc %AppName%_installer.iss
cd ..\..\..\
echo Build Complete.

goto :EOF

:getparentdir
if "%~1" EQU "" goto :EOF
Set ParentDir=%~1
shift
goto :getparentdir

:DeQuote
for /f "delims=" %%A in ('echo %%%1%%') do set %1=%%~A
EXIT /B 0