[![](https://img.shields.io/nuget/v/soenneker.blazor.videojs.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.videojs/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.videojs/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.videojs/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.videojs.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.videojs/)
[![](https://img.shields.io/badge/Demo-Live-blueviolet?style=for-the-badge&logo=github)](https://soenneker.github.io/soenneker.blazor.videojs)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Blazor.Videojs
### A Blazor interop library for Video.js

This library wraps Video.js with a strongly-typed Blazor component and configuration model. It auto-loads Video.js assets (CDN or local) and exposes common player events as `EventCallback`s.

The options closely mirror the Video.js options surface. Refer to the [Video.js options guide](https://videojs.com/guides/options/) for configuration details.

## Installation

```
dotnet add package Soenneker.Blazor.Videojs
```

### Register services

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddVideoJsInteropAsScoped();
}
```

## Usage

```razor
@using Soenneker.Blazor.Videojs
@using Soenneker.Blazor.Videojs.Configuration
@using Soenneker.Blazor.Videojs.Dtos

<VideoJs Configuration="@_config" OnPlay="HandlePlay" />

@code {
    private readonly VideoJsConfiguration _config = new()
    {
        Controls = true,
        Autoplay = "muted",
        Muted = true,
        Fluid = true,
        Responsive = true,
        AspectRatio = "16:9",
        Poster = "https://vjs.zencdn.net/v/oceans.png",
        PlaybackRates = [0.5, 1, 1.5, 2],
        ControlBar = new VideoJsControlBarOptions
        {
            SkipButtons = new VideoJsSkipButtonsOptions { Backward = 10, Forward = 10 }
        },
        Sources =
        [
            new VideoJsSource { Src = "https://vjs.zencdn.net/v/oceans.mp4", Type = "video/mp4" },
            new VideoJsSource { Src = "https://vjs.zencdn.net/v/oceans.webm", Type = "video/webm" }
        ]
    };

    private void HandlePlay()
    {
        // Your event logic here
    }
}
```

## Sources

You can provide sources through the configuration object (shown above) or via the component parameter:

```razor
<VideoJs Configuration="@_config" Sources="@_sources" />
```

`Sources` overrides `Configuration.Sources` when provided.

## CDN vs local assets

The interop will load Video.js from the CDN by default. To use the packaged static files instead, set:

```csharp
var config = new VideoJsConfiguration
{
    UseCdn = false
};
```

## Events

The component exposes common Video.js events as `EventCallback`s, including `OnReady`, `OnPlay`, `OnPause`, `OnEnded`, `OnTimeUpdate`, `OnLoadedMetadata`, `OnSeeking`, `OnSeeked`, `OnWaiting`, `OnPlaying`, `OnRateChange`, and more.
