using System.Text.Json.Serialization;

namespace Soenneker.Blazor.Videojs.Configuration;

/// <summary>
/// Spatial navigation options for Video.js.
/// </summary>
public sealed class VideoJsSpatialNavigationOptions
{
    /// <summary>
    /// Enables spatial navigation.
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; set; }

    /// <summary>
    /// Enables horizontal seek with arrow keys.
    /// </summary>
    [JsonPropertyName("horizontalSeek")]
    public bool? HorizontalSeek { get; set; }
}
