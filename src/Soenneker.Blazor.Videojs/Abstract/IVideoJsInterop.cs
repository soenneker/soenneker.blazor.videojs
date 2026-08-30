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
    /// Initializes the video javascript so it is ready for use.
    /// </summary>
    /// <param name="useCdn">Whether cdn.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the video javascript is ready for use.</returns>
    ValueTask Initialize(bool useCdn = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a video javascript instance from the supplied inputs.
    /// </summary>
    /// <param name="elementReference">Element Reference for the create operation.</param>
    /// <param name="elementId">ID of the DOM element to target.</param>
    /// <param name="configuration">configuration that supplies runtime settings.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the create operation is complete.</returns>
    ValueTask Create(ElementReference elementReference, string elementId, VideoJsConfiguration? configuration = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates sources.
    /// </summary>
    /// <param name="elementId">ID of the DOM element to target.</param>
    /// <param name="sources">sources to read or transform.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the sources update is complete.</returns>
    ValueTask UpdateSources(string elementId, List<VideoJsSource> sources, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets poster.
    /// </summary>
    /// <param name="elementId">ID of the DOM element to target.</param>
    /// <param name="poster">Poster for the set poster operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the poster has been stored.</returns>
    ValueTask SetPoster(string elementId, string? poster, CancellationToken cancellationToken = default);

    /// <summary>
    /// Registers event for the video javascript.
    /// </summary>
    /// <param name="elementId">ID of the DOM element to target.</param>
    /// <param name="eventName">Name of the event to publish or subscribe to.</param>
    /// <param name="dotNetReference">JavaScript-invokable reference to the .NET component instance.</param>
    /// <param name="callbackMethod">callback Method to invoke when the operation runs.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the event registration is complete.</returns>
    ValueTask RegisterEvent(string elementId, string eventName, DotNetObjectReference<VideoJsEventBridge> dotNetReference,
        string callbackMethod, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes a previously registered Video.js event callback.
    /// </summary>
    /// <param name="elementId">ID of the player element.</param>
    /// <param name="eventName">Video.js event name.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the event callback has been removed.</returns>
    ValueTask UnregisterEvent(string elementId, string eventName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Releases resources used by the current instance.
    /// </summary>
    /// <param name="elementId">ID of the DOM element to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the dispose operation is complete.</returns>
    ValueTask Dispose(string elementId, CancellationToken cancellationToken = default);
}
