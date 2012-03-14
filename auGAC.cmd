@echo off
if defined ProgramFiles set pf=%ProgramFiles%
if defined ProgramFiles(x86) set pf=%ProgramFiles(x86)%
if exist "%pf%\Microsoft SDKs\Windows\v6.0" set sdk=%pf%\Microsoft SDKs\Windows\v6.0\Bin
if exist "%pf%\Microsoft SDKs\Windows\v6.0A" set sdk=%pf%\Microsoft SDKs\Windows\v6.0A\Bin
if exist "%pf%\Microsoft SDKs\Windows\v7.0A" set sdk=%pf%\Microsoft SDKs\Windows\v7.0A\Bin
if exist "%sdk%\NETFX 4.0 Tools" set sdk=%sdk%\NETFX 4.0 Tools
if exist "%sdk%\x64" set sdk=%sdk%\x64
if defined sdk "%sdk%\gacutil.exe" /nologo /f /il %~dp0gac-list.txt
pause