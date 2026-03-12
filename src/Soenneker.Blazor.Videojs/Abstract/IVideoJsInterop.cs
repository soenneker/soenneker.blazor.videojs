using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Videojs.Configuration;
using Soenneker.Blazor.Videojs.Dtos;
using Soenneker.Blazor.Videojs;

namespace Soenneker.Blazor.Videojs.Abstract;

/// <summary>
/// A Blazor interop library for Video.js
/// </summary>
public interface IVideoJsInterop
{
    ValueTask Initialize(bool useCdn = true, CancellationToken cancellationToken = default);

    ValueTask Create(ElementReference elementReference, string elementId, VideoJsConfiguration? configuration = null,
        CancellationToken cancellationToken = default);

    ValueTask UpdateSources(string elementId, List<VideoJsSource> sources, CancellationToken cancellationToken = default);

    ValueTask SetPoster(string elementId, string? poster, CancellationToken cancellationToken = default);

    ValueTask RegisterEvent(string elementId, string eventName, DotNetObjectReference<VideoJsEventBridge> dotNetReference,
        string callbackMethod, CancellationToken cancellationToken = default);

    ValueTask Dispose(string elementId, CancellationToken cancellationToken = default);
}
