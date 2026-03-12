using System.Text.Json.Serialization;

namespace Soenneker.Blazor.Videojs.Dtos;

/// <summary>
/// Represents a media source for Video.js.
/// </summary>
public sealed class VideoJsSource
{
    /// <summary>
    /// Source URL.
    /// </summary>
    [JsonPropertyName("src")]
    public string Src { get; set; } = string.Empty;

    /// <summary>
    /// MIME type (e.g., "video/mp4").
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Display label for the source.
    /// </summary>
    [JsonPropertyName("label")]
    public string? Label { get; set; }

    /// <summary>
    /// Resolution descriptor (e.g., "1080").
    /// </summary>
    [JsonPropertyName("res")]
    public string? Res { get; set; }

    /// <summary>
    /// Marks the source as selected.
    /// </summary>
    [JsonPropertyName("selected")]
    public bool? Selected { get; set; }
}
