using System.Text.Json.Serialization;

namespace Soenneker.Blazor.Videojs.Configuration;

/// <summary>
/// HTML5 tech options for Video.js.
/// </summary>
public sealed class VideoJsHtml5Options
{
    /// <summary>
    /// Forces native controls for touch devices.
    /// </summary>
    [JsonPropertyName("nativeControlsForTouch")]
    public bool? NativeControlsForTouch { get; set; }

    /// <summary>
    /// Enables native audio track support.
    /// </summary>
    [JsonPropertyName("nativeAudioTracks")]
    public bool? NativeAudioTracks { get; set; }

    /// <summary>
    /// Enables native text track support.
    /// </summary>
    [JsonPropertyName("nativeTextTracks")]
    public bool? NativeTextTracks { get; set; }

    /// <summary>
    /// Enables native video track support.
    /// </summary>
    [JsonPropertyName("nativeVideoTracks")]
    public bool? NativeVideoTracks { get; set; }

    /// <summary>
    /// Preload non-active text tracks.
    /// </summary>
    [JsonPropertyName("preloadTextTracks")]
    public bool? PreloadTextTracks { get; set; }
}
