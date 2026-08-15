# a2z Tech - Release + Installer build
# Usage: powershell -ExecutionPolicy Bypass -File .\installer\build.ps1

$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$publishDir = Join-Path $root "dist\app"
$distDir = Join-Path $root "dist"
$iss = Join-Path $PSScriptRoot "A2ZTech.iss"
$csproj = Join-Path $root "A2ZTech.csproj"

Write-Host "==> Publishing self-contained Release (win-x64)..." -ForegroundColor Cyan
# If a running app locks dist\app\Data\inventory.db, move Data aside so publish can clean.
$dataDir = Join-Path $publishDir "Data"
$dataBackup = Join-Path $distDir "_Data_build_backup"
if (Test-Path $dataDir) {
    if (Test-Path $dataBackup) { Remove-Item $dataBackup -Recurse -Force -ErrorAction SilentlyContinue }
    try {
        Move-Item -Path $dataDir -Destination $dataBackup -Force
        Write-Host "Moved locked Data aside for rebuild." -ForegroundColor Yellow
    } catch {
        Write-Host "Could not move Data (may be locked): $_" -ForegroundColor Yellow
    }
}
if (Test-Path $publishDir) { Remove-Item $publishDir -Recurse -Force }
New-Item -ItemType Directory -Force -Path $publishDir | Out-Null

dotnet publish $csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=false -o $publishDir
if ($LASTEXITCODE -ne 0) { throw "dotnet publish failed" }

Write-Host "==> Looking for Inno Setup (ISCC)..." -ForegroundColor Cyan
$pf86 = ${env:ProgramFiles(x86)}
$pf = $env:ProgramFiles
$lad = $env:LocalAppData
$isccCandidates = @(
    (Join-Path $pf86 "Inno Setup 6\ISCC.exe"),
    (Join-Path $pf "Inno Setup 6\ISCC.exe"),
    (Join-Path $lad "Programs\Inno Setup 6\ISCC.exe"),
    (Join-Path $pf86 "Inno Setup 7\ISCC.exe"),
    (Join-Path $pf "Inno Setup 7\ISCC.exe")
)
$iscc = $isccCandidates | Where-Object { $_ -and (Test-Path $_) } | Select-Object -First 1

New-Item -ItemType Directory -Force -Path $distDir | Out-Null

# Remove obsolete installer/portable names if present
@(
    (Join-Path $distDir "PanacheSetup.exe"),
    (Join-Path $distDir "PanacheInventory-Portable.zip")
) | ForEach-Object { if (Test-Path $_) { Remove-Item $_ -Force } }

if ($iscc) {
    Write-Host "==> Building Setup.exe with $iscc" -ForegroundColor Cyan
    & $iscc $iss
    if ($LASTEXITCODE -ne 0) { throw "ISCC failed" }
    $setup = Get-ChildItem $distDir -Filter "A2ZTechSetup.exe" | Select-Object -First 1
    if ($setup) {
        Write-Host ""
        Write-Host "SUCCESS - give buyers this file:" -ForegroundColor Green
        Write-Host $setup.FullName -ForegroundColor Green
    }
}
else {
    $zip = Join-Path $distDir "A2ZTech-Portable.zip"
    if (Test-Path $zip) { Remove-Item $zip -Force }
    Compress-Archive -Path (Join-Path $publishDir "*") -DestinationPath $zip
    Write-Host ""
    Write-Host "Inno Setup not available - created portable zip instead:" -ForegroundColor Yellow
    Write-Host $zip
}

Write-Host ""
Write-Host "Published app folder: $publishDir"
Write-Host "Run locally: $(Join-Path $publishDir 'A2ZTech.exe')"

# Restore local Data after publish so developer DB is kept
if (Test-Path $dataBackup) {
    $restoredData = Join-Path $publishDir "Data"
    if (-not (Test-Path $restoredData)) {
        Move-Item -Path $dataBackup -Destination $restoredData -Force
        Write-Host "Restored local Data folder." -ForegroundColor Yellow
    } else {
        Remove-Item $dataBackup -Recurse -Force -ErrorAction SilentlyContinue
    }
}