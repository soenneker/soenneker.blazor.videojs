[![](https://img.shields.io/nuget/v/soenneker.blazor.videojs.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.videojs/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.videojs/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.videojs/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.videojs.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.videojs/)
[![](https://img.shields.io/badge/Demo-Live-blueviolet?style=for-the-badge&logo=github)](https://soenneker.github.io/soenneker.blazor.videojs)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.videojs/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.videojs/actions/workflows/codeql.yml)

# Soenneker.Blazor.Videojs

Wraps Video.js in a Blazor component with typed configuration, dynamic source and poster updates, and common player events exposed as `EventCallback`s.

## Installation

```bash
dotnet add package Soenneker.Blazor.Videojs
```

Register the scoped interop in `Program.cs`:

```csharp
using Soenneker.Blazor.Videojs.Registrars;

builder.Services.AddVideoJsInteropAsScoped();
```

## Basic player

```razor
@using Soenneker.Blazor.Videojs
@using Soenneker.Blazor.Videojs.Configuration
@using Soenneker.Blazor.Videojs.Dtos

<VideoJs Configuration="@_configuration"
         OnPlay="HandlePlay"
         OnEnded="HandleEnded" />

@code {
    private readonly VideoJsConfiguration _configuration = new()
    {
        Controls = true,
        Autoplay = "muted",
        Muted = true,
        Fluid = true,
        Responsive = true,
        AspectRatio = "16:9",
        Poster = "https://vjs.zencdn.net/v/oceans.png",
        PlaybackRates = [0.5, 1, 1.5, 2],
        Sources =
        [
            new VideoJsSource { Src = "https://vjs.zencdn.net/v/oceans.mp4", Type = "video/mp4" },
            new VideoJsSource { Src = "https://vjs.zencdn.net/v/oceans.webm", Type = "video/webm" }
        ]
    };

    private void HandlePlay() { }
    private void HandleEnded() { }
}
```

The component creates and disposes the Video.js player with the component lifecycle. Its generated default classes are `video-js vjs-default-skin`; pass `Class` to append your own classes.

## Change media after initialization

Pass `Sources` when the source list changes at runtime:

```razor
<VideoJs Configuration="@_configuration" Sources="@_sources" />
```

`Sources` overrides `Configuration.Sources` when provided.

```csharp
_sources =
[
    new VideoJsSource
    {
        Src = "https://media.example.com/next-video.mp4",
        Type = "video/mp4"
    }
];
```

After the player is created, the component watches `Sources` and `Configuration.Poster` and applies changes through Video.js. Other configuration properties are creation options: changing them later does not recreate or reconfigure the player. Give each component a unique `Id` if you set one explicitly.

## Packaged or CDN assets

Video.js CSS and JavaScript load from a pinned jsDelivr URL by default, with Subresource Integrity validation. To avoid the external dependency, use the assets included in the NuGet package:

```csharp
private readonly VideoJsConfiguration _configuration = new()
{
    UseCdn = false
};
```

Keep `UseCdn` consistent for all players sharing the same scoped interop. Video.js installs a page-global `videojs` object, so this package cannot isolate two asset builds on the same page.

## Events

Available callbacks are:

- `OnReady`, `OnPlay`, `OnPause`, and `OnEnded`
- `OnTimeUpdate`, `OnDurationChange`, and `OnRateChange`
- `OnLoadedMetadata`, `OnLoadedData`, `OnCanPlay`, and `OnCanPlayThrough`
- `OnSeeking`, `OnSeeked`, `OnWaiting`, and `OnPlaying`
- `OnVolumeChange`, `OnProgress`, `OnStalled`, and `OnSuspend`
- `OnAbort`, `OnError`, and `OnEmptied`

Callbacks can be added, replaced, or removed on later renders. They are notifications without event payloads; `OnError` does not include Video.js error details. `OnTimeUpdate` can fire frequently, so keep its handler inexpensive.

## Configuration notes

`VideoJsConfiguration` mirrors commonly used Video.js creation options, including controls, autoplay, muted playback, responsive sizing, playback rates, control-bar options, HTML5 tech options, languages, and plugins. See the [Video.js options guide](https://videojs.com/guides/options/) for the browser library's option semantics.

- Browser autoplay rules still apply. `Autoplay = "muted"` with `Muted = true` is the most broadly permitted mode, but playback is never guaranteed.
- Media hosts must serve the correct MIME type and allow any cross-origin access required by the browser and selected streaming format.
- Source and poster URLs are sent to the browser as provided. Do not build them from untrusted input without applying your application's URL policy.
- The component exposes lifecycle and event integration, not imperative controls such as play, pause, seek, or volume setters.

## Child content

Use `ChildContent` when the underlying `<video>` element needs custom children such as tracks or fallback markup:

```razor
<VideoJs Configuration="@_configuration">
    <track kind="captions"
           src="/captions/en.vtt"
           srclang="en"
           label="English"
           default />
</VideoJs>
```

When child content is supplied, the component does not render `<source>` elements itself; sources in the Video.js configuration are still passed to the player options.

## Low-level interop

`IVideoJsInterop` exposes initialization, creation, source/poster updates, event registration, and player disposal for integrations that manage their own `<video>` element. Most applications should use the `VideoJs` component so player and .NET callback references are cleaned up together.
