@echo off
setlocal
cd /d "%~dp0"

echo ============================================
echo  a2z Tech - Publish + Setup
echo ============================================
echo.
echo Publishes win-x64 Release to dist\app
echo builds dist\A2ZTech-Portable.exe
echo and dist\A2ZTechSetup.exe (Inno Setup).
echo.

powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0installer\build.ps1"
if errorlevel 1 (
  echo.
  echo ERROR: Publish / setup failed.
  pause
  exit /b 1
)

echo.
echo --------------------------------------------
echo  Done
echo --------------------------------------------
echo  App:   %~dp0dist\A2ZTech-Portable.exe
echo  Folder:%~dp0dist\app\A2ZTech.exe
if exist "%~dp0dist\A2ZTechSetup.exe" (
  echo  Setup: %~dp0dist\A2ZTechSetup.exe
) else (
  echo  Setup: not created - install Inno Setup, then re-run.
  echo         https://jrsoftware.org/isdl.php
)
echo.
pause
endlocal
