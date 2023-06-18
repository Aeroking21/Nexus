@echo off

REM Change directory to WebBackend
cd /d "%~dp0WebBackend"

REM Run dotnet run --launch-profile https for the backend
start "" dotnet run --launch-profile https


REM Change directory to GraphSample
cd /d "%~dp0GraphSample"

REM Run dotnet watch for the frontend
start "" dotnet watch
