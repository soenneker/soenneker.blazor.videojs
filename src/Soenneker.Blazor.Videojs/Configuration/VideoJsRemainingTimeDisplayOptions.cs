using System.Text.Json.Serialization;

namespace Soenneker.Blazor.Videojs.Configuration;

/// <summary>
/// Remaining time display options for Video.js.
/// </summary>
public sealed class VideoJsRemainingTimeDisplayOptions
{
    /// <summary>
    /// Shows remaining time as negative when true.
    /// </summary>
    [JsonPropertyName("displayNegative")]
    public bool? DisplayNegative { get; set; }
}
