using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Videojs.Configuration;
using Soenneker.Blazor.Videojs.Dtos;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Blazor.Videojs.Abstract;

/// <summary>
/// A Blazor interop library for Video.js
/// </summary>
public interface IVideoJsInterop : IAsyncDisposable
{
    /// <summary>
    /// Executes the initialize operation.
    /// </summary>
    /// <param name="useCdn">The use cdn.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Initialize(bool useCdn = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the create operation.
    /// </summary>
    /// <param name="elementReference">The element reference.</param>
    /// <param name="elementId">The element id.</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Create(ElementReference elementReference, string elementId, VideoJsConfiguration? configuration = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates sources.
    /// </summary>
    /// <param name="elementId">The element id.</param>
    /// <param name="sources">The sources.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask UpdateSources(string elementId, List<VideoJsSource> sources, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets poster.
    /// </summary>
    /// <param name="elementId">The element id.</param>
    /// <param name="poster">The poster.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask SetPoster(string elementId, string? poster, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the register event operation.
    /// </summary>
    /// <param name="elementId">The element id.</param>
    /// <param name="eventName">The event name.</param>
    /// <param name="dotNetReference">The dot net reference.</param>
    /// <param name="callbackMethod">The callback method.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask RegisterEvent(string elementId, string eventName, DotNetObjectReference<VideoJsEventBridge> dotNetReference,
        string callbackMethod, CancellationToken cancellationToken = default);

    /// <summary>
    /// Releases resources used by the current instance.
    /// </summary>
    /// <param name="elementId">The element id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Dispose(string elementId, CancellationToken cancellationToken = default);
}
