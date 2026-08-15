@echo off
echo ============================================
echo  a2z Tech - Publish + Setup
echo ============================================
echo.
powershell -ExecutionPolicy Bypass -File "%~dp0installer\build.ps1"
echo.
pause
