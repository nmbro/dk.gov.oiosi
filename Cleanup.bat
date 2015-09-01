@echo off
:: This script will not pause if run from the command console, but will if double-clicked in Explorer:
setlocal enableextensions

set SCRIPT=%0
set DQUOTE="

REM Call with argument 'NoPause' or 'ForcePause to never pause or always pause regardless of how the script was launched
IF [%1]==[NoPause] (
  SET PAUSE_ON_CLOSE=0
) ELSE IF [%1]==[ForcePause] (
  SET PAUSE_ON_CLOSE=1
) ELSE (
  REM Detect how script was launched
  @echo %SCRIPT:~0,1% | findstr /l %DQUOTE% > NUL
  if %ERRORLEVEL% EQU 0 set PAUSE_ON_CLOSE=1
)

:: Run your app


CLS
ECHO Cleaning the solution for build files

:: Framework solutions

ECHO.
ECHO Cleaning Target folder
RMDIR /S /Q target

ECHO Cleaning Packages folder
RMDIR /S /Q packages

ECHO Cleaning samples
FOR /d /r samples %%d IN (bin,obj) DO @if EXIST "%%d" RD /s/q "%%d"

ECHO Cleaning src
FOR /d /r src %%d IN (bin,obj) DO @if EXIST "%%d" RD /s/q "%%d"

ECHO Cleaning test
FOR /d /r test %%d IN (bin,obj) DO @if EXIST "%%d" RD /s/q "%%d"

:: Various files

ECHO Cleaning Microsoft.VisualStudio.Diagnostics.ServiceModelSink.pdb
FOR /d /r . %%d IN (Microsoft.VisualStudio.Diagnostics.ServiceModelSink.pdb) DO @if EXIST "%%d" RD /s/q "%%d"

ECHO Cleaning Microsoft.VisualStudio.HostingProcess.Utilities.pdb
FOR /d /r . %%d IN (Microsoft.VisualStudio.HostingProcess.Utilities.pdb) DO @if EXIST "%%d" RD /s/q "%%d"

ECHO Cleaning Microsoft.VisualStudio.HostingProcess.Utilities.Sync.pdb
FOR /d /r . %%d IN (Microsoft.VisualStudio.HostingProcess.Utilities.Sync.pdb) DO @if EXIST "%%d" RD /s/q "%%d"

ECHO Cleaning Microsoft.VisualStudio.TestPlatform.Extensions.TmiAdapter.pdb
FOR /d /r . %%d IN (Microsoft.VisualStudio.TestPlatform.Extensions.TmiAdapter.pdb) DO @if EXIST "%%d" RD /s/q "%%d"

ECHO Cleaning Microsoft.VisualStudio.TestPlatform.Extensions.TrxLogger.pdb
FOR /d /r . %%d IN (Microsoft.VisualStudio.TestPlatform.Extensions.TrxLogger.pdb) DO @if EXIST "%%d" RD /s/q "%%d"

ECHO Cleaning Microsoft.VisualStudio.TestPlatform.Extensions.VSTestIntegration.pdb
FOR /d /r . %%d IN (Microsoft.VisualStudio.TestPlatform.Extensions.VSTestIntegration.pdb) DO @if EXIST "%%d" RD /s/q "%%d"

ECHO Cleaning Microsoft.VisualStudio.TestPlatform.ObjectModel.pdb
FOR /d /r . %%d IN (Microsoft.VisualStudio.TestPlatform.ObjectModel.pdb) DO @if EXIST "%%d" RD /s/q "%%d"

ECHO Cleaning Microsoft.VisualStudio.TestPlatform.TestExecutor.Core.pdb
FOR /d /r . %%d IN (Microsoft.VisualStudio.TestPlatform.TestExecutor.Core.pdb) DO @if EXIST "%%d" RD /s/q "%%d"

ECHO Cleaning Microsoft.VisualStudio.QualityTools.UnitTestFramework.pdb
FOR /d /r . %%d IN (Microsoft.VisualStudio.QualityTools.UnitTestFramework.pdb) DO @if EXIST "%%d" RD /s/q "%%d"

ECHO Cleaning Microsoft.VisualStudio.Tools.Applications.Adapter.v9.0.pdb
FOR /d /r . %%d IN (Microsoft.VisualStudio.Tools.Applications.Adapter.v9.0.pdb) DO @if EXIST "%%d" RD /s/q "%%d"

::ECHO Cleaning .bak
::DEL /S /F /Q *.bak > NUL


ECHO Cleaning pingme.txt
FOR /r . %%F IN (pingme.txt) DO @if %%~zF==0 del "%%F"

ECHO Finish cleaning
ECHO.

:EXIT
@ECHO OFF
IF [%PAUSE_ON_CLOSE%]==[1] PAUSE


@ECHO OFF
ENDLOCAL
@ECHO ON

