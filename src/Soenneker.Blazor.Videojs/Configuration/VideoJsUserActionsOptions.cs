using System.Text.Json.Serialization;

namespace Soenneker.Blazor.Videojs.Configuration;

/// <summary>
/// User action options for Video.js.
/// </summary>
public sealed class VideoJsUserActionsOptions
{
    /// <summary>
    /// Enables or disables click handling.
    /// </summary>
    [JsonPropertyName("click")]
    public bool? Click { get; set; }

    /// <summary>
    /// Enables or disables double-click handling.
    /// </summary>
    [JsonPropertyName("doubleClick")]
    public bool? DoubleClick { get; set; }

    /// <summary>
    /// Hotkey configuration (true/false or object).
    /// </summary>
    [JsonPropertyName("hotkeys")]
    public object? Hotkeys { get; set; }
}
