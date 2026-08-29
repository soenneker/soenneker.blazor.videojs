using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Extensions.EventCallback;

namespace Soenneker.Blazor.Videojs;

/// <summary>
/// Bridges Video.js events back into Blazor callbacks.
/// </summary>
public sealed class VideoJsEventBridge
{
    private readonly IReadOnlyDictionary<string, EventCallback> _eventCallbacks;

    public VideoJsEventBridge(IReadOnlyDictionary<string, EventCallback> eventCallbacks)
    {
        _eventCallbacks = eventCallbacks;
    }

    /// <summary>
    /// Responds when event occurs.
    /// </summary>
    /// <param name="eventName">Name of the event to publish or subscribe to.</param>
    /// <returns>A task that completes when the on event operation is complete.</returns>
    [JSInvokable]
    public async Task OnEvent(string eventName)
    {
        if (_eventCallbacks.TryGetValue(eventName, out EventCallback callback))
        {
            await callback.InvokeIfHasDelegate();
        }
    }
}
