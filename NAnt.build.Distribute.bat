@echo off
:: This script will not pause if run from the command console, but will if double-clicked in Explorer:
setlocal enableextensions

set SCRIPT=%0
set DQUOTE="

:: Detect how script was launched
@echo %SCRIPT:~0,1% | findstr /l %DQUOTE% > NUL
if %ERRORLEVEL% EQU 0 set PAUSE_ON_CLOSE=1

:: Run your app

@echo off
set EnableNuGetPackageRestore = true
set msbuildPath01=C:\Windows\Microsoft.NET\Framework\v4.0
set msbuildPath02=C:\Windows\Microsoft.NET\Framework\v4.0.30319
set nantPath01=%cd%\lib\NAnt\nant\bin
set PATH=%PATH%;%msbuildPath01%;%msbuildPath02%;%nantPath01%
@echo on


@echo on
REM NAnt log fil can not be savet to folder target/temp.
REM NAnt start by deleting the directory (including it own log) which is not good
REM And on a clean checkout, the directory does not even exist.
nant -D:build.number=65534 -D:buildType=Dev -f:build.xml Distribute -logfile:NAnt.build.log


:EXIT
@echo off
if defined PAUSE_ON_CLOSE pause


@echo off
endlocal
@echo on
