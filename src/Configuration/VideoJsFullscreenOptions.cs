using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Soenneker.Blazor.Videojs.Configuration;

/// <summary>
/// Fullscreen options for Video.js.
/// </summary>
public sealed class VideoJsFullscreenOptions
{
    /// <summary>
    /// Options passed to the Fullscreen API.
    /// </summary>
    [JsonPropertyName("options")]
    public Dictionary<string, object>? Options { get; set; }
}
