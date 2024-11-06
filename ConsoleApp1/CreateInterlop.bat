@echo off
setlocal enabledelayedexpansion

REM Set output directory
set "OUTPUT_DIR=interop_output"

REM Create output directory if it doesn't exist
if not exist "%OUTPUT_DIR%" (
    mkdir "%OUTPUT_DIR%"
    if errorlevel 1 (
        echo ERROR: Failed to create output directory
        exit /b 1
    )
)

REM Generate strong name key if it doesn't exist
if not exist "%OUTPUT_DIR%\ControlBuilder.snk" (
    echo Generating strong name key...
    sn -k "%OUTPUT_DIR%\ControlBuilder.snk"
    if errorlevel 1 (
        echo ERROR: Failed to generate strong name key
        exit /b 1
    )
)

echo.
echo Step 1: Exporting type library...
tlbexp.exe "C:\Users\DM\Desktop\CBOpenIf1\Control Builder M Professional\Bin\ControlBuilderPro.exe" ^
    /out:"%OUTPUT_DIR%\ControlBuilderPro.tlb"
if errorlevel 1 (
    echo ERROR: Failed to export type library
    exit /b 1
)

echo.
echo Step 2: Generating interop assembly...
tlbimp.exe "%OUTPUT_DIR%\ControlBuilderPro.tlb" ^
    /out:"%OUTPUT_DIR%\Interop.ControlBuilderPro.dll" ^
    /namespace:ModernApp.ControlBuilder ^
    /machine:x64 ^
    /primary ^
    /keyfile:"%OUTPUT_DIR%\ControlBuilder.snk" ^
    /asmversion:1.0.0.0 ^
    /reference:"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\mscorlib.dll"
if errorlevel 1 (
    echo ERROR: Failed to generate interop assembly
    exit /b 1
)

REM Clean up temporary files
del "%OUTPUT_DIR%\ControlBuilderPro.tlb"

echo.
if exist "%OUTPUT_DIR%\Interop.ControlBuilderPro.dll" (
    echo SUCCESS: Interop assembly generated successfully
    echo Location: %CD%\%OUTPUT_DIR%\Interop.ControlBuilderPro.dll
) else (
    echo ERROR: Interop assembly not created
    exit /b 1
)