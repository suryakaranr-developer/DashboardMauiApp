# DashboardMauiApp

A .NET MAUI (net8.0) app with a **Dashboard** page, targeting **Android** and **iOS**,
with **4 build flavours**: `Dev`, `UAT`, `Stage`, `Prod`.

## What's inside

```
DashboardMauiApp.sln
DashboardMauiApp/
  DashboardMauiApp.csproj      <-- flavour configs defined here
  MauiProgram.cs
  App.xaml / App.xaml.cs
  AppShell.xaml / AppShell.xaml.cs
  Configuration/
    AppConfig.cs                <-- per-flavour constants (API URL, env name, colors)
  Models/
    DashboardCard.cs
  ViewModels/
    BaseViewModel.cs
    DashboardViewModel.cs
  Views/
    DashboardPage.xaml / .cs    <-- the Dashboard screen
  Platforms/
    Android/                    <-- MainActivity, MainApplication, AndroidManifest
    iOS/                        <-- AppDelegate, Program.cs, Info.plist
  Resources/
    Styles, AppIcon, Splash, Images, Fonts, Raw
```

## Prerequisites (on your own machine)

This project was hand-assembled as a text scaffold (the sandbox that built it has
no internet access to NuGet/dotnet installers), so before opening it you need:

1. **.NET 8 SDK** — https://dotnet.microsoft.com/download/dotnet/8.0
2. **MAUI workload**:
   ```bash
   dotnet workload install maui
   ```
3. For Android: **Android SDK** (via Visual Studio, or `dotnet workload install android` +
   Android command-line tools / Android Studio).
4. For iOS: a **Mac with Xcode** installed (iOS builds/deploys require macOS + Xcode,
   or a paired Mac if you're on Windows with "Pair to Mac").
5. Restore packages:
   ```bash
   dotnet restore
   ```

## How the flavours work

The 4 flavours are plain **MSBuild Configurations**, defined in `DashboardMauiApp.csproj`:

| Configuration | Compiler constant | App Id (Android/iOS bundle id) | Notes |
|---|---|---|---|
| `Dev` (or `Debug`) | `DEV` | `com.company.dashboardapp.dev` | green banner, verbose logging |
| `UAT` | `UAT` | `com.company.dashboardapp.uat` | amber banner |
| `Stage` | `STAGE` | `com.company.dashboardapp.stage` | blue banner, `.aab` package |
| `Prod` (or `Release`) | `PROD` | `com.company.dashboardapp` | red demo banner, AOT + trimming, `.aab` |

`Configuration/AppConfig.cs` uses `#if DEV / UAT / STAGE / PROD` to expose:
- `AppConfig.EnvironmentName`
- `AppConfig.ApiBaseUrl` (point this at your real per-environment API)
- `AppConfig.BannerColorHex`

The Dashboard page binds to these so you can visually confirm which flavour is running
(a colored banner with the environment name + API URL at the top of the screen).

Each flavour also gets its **own application id / bundle id and assembly name**, so
Dev/UAT/Stage/Prod builds can be installed **side-by-side** on the same device.

> To rename the package (`com.company.dashboardapp`) to your own, edit the
> `ApplicationId` values in `DashboardMauiApp.csproj`.

## Build & run commands

### Android

```bash
# Debug/Dev build & install to a connected device/emulator
dotnet build -t:Run -f net8.0-android -c Dev

# UAT
dotnet build -t:Run -f net8.0-android -c UAT

# Stage
dotnet build -t:Run -f net8.0-android -c Stage

# Prod (release, signed builds should add keystore properties)
dotnet publish -f net8.0-android -c Prod
```

### iOS (must run on a Mac)

```bash
# Dev, on a paired simulator
dotnet build -t:Run -f net8.0-ios -c Dev

# Prod, for App Store submission (adjust signing/provisioning first)
dotnet publish -f net8.0-ios -c Prod -p:ArchiveOnBuild=true
```

### From Visual Studio / Visual Studio for Mac

Just pick the target **Configuration** (Dev / UAT / Stage / Prod) from the toolbar
dropdown next to the Android/iOS target framework selector, then hit Run/Debug.

## Notes & next steps

- `AppConfig.ApiBaseUrl` values are placeholders — point them at your real
  dev/uat/stage/prod API endpoints.
- The Dashboard currently shows **sample data** (`DashboardViewModel.LoadDataAsync`);
  swap the `Task.Delay` block for a real `HttpClient` call using the injected
  `HttpClient` (already registered in `MauiProgram.cs` with `AppConfig.ApiBaseUrl`
  as its `BaseAddress`).
- `Resources/Fonts` is empty — add `OpenSans-Regular.ttf` / `OpenSans-Semibold.ttf`
  (or your own fonts) there, matching the names referenced in `MauiProgram.cs`,
  or remove the `ConfigureFonts` block if you don't need custom fonts.
- App icon/splash are minimal placeholder SVGs — replace
  `Resources/AppIcon/appicon.svg`, `appiconfg.svg`, and `Resources/Splash/splash.svg`
  with your real branding.
- For production Android signing, add a keystore + `AndroidSigningKeyStore`,
  `AndroidSigningKeyAlias`, etc. to the `Prod` property group in the `.csproj`
  (or pass them via `-p:` on the CLI/CI so secrets aren't committed).
- For iOS signing, configure your Team ID / provisioning profile in the `.csproj`
  or in Xcode-managed signing before publishing.
