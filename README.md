# a2z Tech Inventory Management System

## Quick Start

### Run published app

```text
dist\app\A2ZTech.exe
```

Refresh `dist` after code changes:

```powershell
powershell -ExecutionPolicy Bypass -File .\installer\build.ps1
```

Installer output: `dist\A2ZTechSetup.exe`

### Prerequisites
- Windows 10+ (win-x64) and WebView2 Runtime
- .NET 8 SDK only needed to build from source (`dist\app` is self-contained)

### Running from source

```powershell
dotnet run -c Release --project A2ZTech.csproj
```

Or Debug:

```text
bin\Debug\net8.0-windows\win-x64\A2ZTech.exe
```

### First-Time Setup

1. **License Activation** (if prompted)
2. **Default Login** — Softio.Admin / Softio@2026!
3. **Database** — `Data\inventory.db` next to the exe (created on first run)

## Features

- Dashboard, Inventory, POS, Sales, Reports, History
- Quotations, Customers, Suppliers, Expenses, Currencies
- Barcodes, Users, Settings
- Import/Export, licensing, offline-friendly UI fonts

## Build

```powershell
dotnet build A2ZTech.csproj
powershell -ExecutionPolicy Bypass -File .\installer\build.ps1
```

## Support

Check logs next to the app executable and contact Softio Services if needed.
