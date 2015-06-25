REM Created by: Peter Freiberg, mySupply ApS

@ECHO OFF
SETLOCAL ENABLEDELAYEDEXPANSION

:: This script will not pause if run from the command console, but will if double-clicked in Explorer:
set SCRIPT=%0
set DQUOTE="

REM Call with argument 'NoPause' or 'ForcePause to never pause or always pause regardless of how the script was launched
IF [%2]==[NoPause] (
  SET PAUSE_ON_CLOSE=0
) ELSE IF [%2]==[ForcePause] (
  SET PAUSE_ON_CLOSE=1
) ELSE (
  REM Detect how script was launched
  @echo %SCRIPT:~0,1% | findstr /l %DQUOTE% > NUL
  if %ERRORLEVEL% EQU 0 set PAUSE_ON_CLOSE=1
)

REM Set output directory for restored packages
SET NuGetPath=lib\NuGet\NuGet.exe
IF [%1]==[] (
  SET OUTDIR=packages
) ELSE (
  SET OUTDIR=%1
)

if NOT EXIST "%NuGetPath%" (
    ECHO ##teamcity[message text='NuGet.exe not found' errorDetails='Path=%NuGetPath%' status='ERROR']
  ) ELSE (
    REM Find all files named 'packages.config' except for the ones in mySupply.External
    FOR /F "tokens=*" %%A IN ('DIR /S /B packages.config ^| FINDSTR /V mySupply.External') DO (
      ECHO Restoring "%%A" to "%OUTDIR%"
      %NuGetPath% install "%%A" -OutputDirectory "%OUTDIR%"
    )
)
:EXIT
@ECHO OFF
IF [%PAUSE_ON_CLOSE%]==[1] PAUSE


@ECHO OFF
ENDLOCAL
@ECHO ON

