using System.Text.Json.Serialization;

namespace Soenneker.Blazor.Videojs.Configuration;

/// <summary>
/// Skip button options for the control bar.
/// </summary>
public sealed class VideoJsSkipButtonsOptions
{
    /// <summary>
    /// Jump forward seconds (5, 10, 30).
    /// </summary>
    [JsonPropertyName("forward")]
    public int? Forward { get; set; }

    /// <summary>
    /// Jump backward seconds (5, 10, 30).
    /// </summary>
    [JsonPropertyName("backward")]
    public int? Backward { get; set; }
}
