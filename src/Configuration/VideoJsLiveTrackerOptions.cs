using System.Text.Json.Serialization;

namespace Soenneker.Blazor.Videojs.Configuration;

/// <summary>
/// Live tracker options for Video.js.
/// </summary>
public sealed class VideoJsLiveTrackerOptions
{
    /// <summary>
    /// Seconds of seekable window required to show live UI.
    /// </summary>
    [JsonPropertyName("trackingThreshold")]
    public int? TrackingThreshold { get; set; }

    /// <summary>
    /// Seconds away from live edge to consider "live".
    /// </summary>
    [JsonPropertyName("liveTolerance")]
    public int? LiveTolerance { get; set; }
}
