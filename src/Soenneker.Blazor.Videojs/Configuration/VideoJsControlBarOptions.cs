using System.Text.Json.Serialization;

namespace Soenneker.Blazor.Videojs.Configuration;

/// <summary>
/// Control bar options for Video.js.
/// </summary>
public sealed class VideoJsControlBarOptions
{
    /// <summary>
    /// Remaining time display options.
    /// </summary>
    [JsonPropertyName("remainingTimeDisplay")]
    public VideoJsRemainingTimeDisplayOptions? RemainingTimeDisplay { get; set; }

    /// <summary>
    /// Skip buttons options.
    /// </summary>
    [JsonPropertyName("skipButtons")]
    public VideoJsSkipButtonsOptions? SkipButtons { get; set; }
}
