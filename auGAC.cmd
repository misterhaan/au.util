@echo off
if defined ProgramFiles set pf=%ProgramFiles%
if defined ProgramFiles(x86) set pf=%ProgramFiles(x86)%
if exist "%pf%\Microsoft SDKs\Windows\v6.0" set sdk=%pf%\Microsoft SDKs\Windows\v6.0\Bin
if exist "%pf%\Microsoft SDKs\Windows\v6.0A" set sdk=%pf%\Microsoft SDKs\Windows\v6.0A\Bin
if exist "%pf%\Microsoft SDKs\Windows\v7.0A" set sdk=%pf%\Microsoft SDKs\Windows\v7.0A\Bin
if exist "%pf%\Microsoft SDKs\Windows\v7.1A" set sdk=%pf%\Microsoft SDKs\Windows\v7.1A\Bin
if exist "%pf%\Microsoft SDKs\Windows\v8.0" set sdk=%pf%\Microsoft SDKs\Windows\v8.0\Bin
if exist "%pf%\Microsoft SDKs\Windows\v8.0A" set sdk=%pf%\Microsoft SDKs\Windows\v8.0A\Bin
if exist "%pf%\Microsoft SDKs\Windows\v8.1" set sdk=%pf%\Microsoft SDKs\Windows\v8.1\Bin
if exist "%pf%\Microsoft SDKs\Windows\v8.1A" set sdk=%pf%\Microsoft SDKs\Windows\v8.1A\Bin
if exist "%pf%\Microsoft SDKs\Windows\v10.0A" set sdk=%pf%\Microsoft SDKs\Windows\v10.0A\Bin

if exist "%sdk%\NETFX 4.6.1 Tools" set sdk=%sdk%\NETFX 4.6.1 Tools
if exist "%sdk%\NETFX 4.6 Tools" set sdk=%sdk%\NETFX 4.6 Tools
if exist "%sdk%\NETFX 4.5.1 Tools" set sdk=%sdk%\NETFX 4.5.1 Tools
if exist "%sdk%\NETFX 4.0 Tools" set sdk=%sdk%\NETFX 4.0 Tools
if exist "%sdk%\x64" set sdk=%sdk%\x64
if defined sdk "%sdk%\gacutil.exe" /nologo /f /il %~dp0gac-list.txt
pause
