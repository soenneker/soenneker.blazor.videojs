using System.Collections.Generic;
using System.Text.Json.Serialization;
using Soenneker.Blazor.Videojs.Dtos;

namespace Soenneker.Blazor.Videojs.Configuration;

/// <summary>
/// Configuration settings for the Video.js player.
/// </summary>
public sealed class VideoJsConfiguration
{
    /// <summary>
    /// When true, loads Video.js assets from the CDN.
    /// </summary>
    [JsonIgnore]
    public bool UseCdn { get; set; } = true;

    /// <summary>
    /// Shows the player controls.
    /// </summary>
    [JsonPropertyName("controls")]
    public bool Controls { get; set; } = true;

    /// <summary>
    /// Starts playback automatically when possible.
    /// </summary>
    [JsonPropertyName("autoplay")]
    public object? Autoplay { get; set; }

    /// <summary>
    /// Source URL for a single media source.
    /// </summary>
    [JsonPropertyName("src")]
    public string? Src { get; set; }

    /// <summary>
    /// Mutes audio output.
    /// </summary>
    [JsonPropertyName("muted")]
    public bool Muted { get; set; }

    /// <summary>
    /// Loops playback when the media ends.
    /// </summary>
    [JsonPropertyName("loop")]
    public bool Loop { get; set; }

    /// <summary>
    /// Specifies preload behavior ("auto", "metadata", "none").
    /// </summary>
    [JsonPropertyName("preload")]
    public string? Preload { get; set; } = "auto";

    /// <summary>
    /// Poster image URL.
    /// </summary>
    [JsonPropertyName("poster")]
    public string? Poster { get; set; }

    /// <summary>
    /// Player display width (number or string).
    /// </summary>
    [JsonPropertyName("width")]
    public object? Width { get; set; }

    /// <summary>
    /// Player display height (number or string).
    /// </summary>
    [JsonPropertyName("height")]
    public object? Height { get; set; }

    /// <summary>
    /// Makes the player fluid (responsive) in width.
    /// </summary>
    [JsonPropertyName("fluid")]
    public bool Fluid { get; set; } = true;

    /// <summary>
    /// Enables responsive mode.
    /// </summary>
    [JsonPropertyName("responsive")]
    public bool Responsive { get; set; } = true;

    /// <summary>
    /// Responsive control bar breakpoints.
    /// </summary>
    [JsonPropertyName("breakpoints")]
    public Dictionary<string, int>? Breakpoints { get; set; }

    /// <summary>
    /// Aspect ratio (e.g., "16:9").
    /// </summary>
    [JsonPropertyName("aspectRatio")]
    public string? AspectRatio { get; set; }

    /// <summary>
    /// Puts the player into audio-only mode.
    /// </summary>
    [JsonPropertyName("audioOnlyMode")]
    public bool? AudioOnlyMode { get; set; }

    /// <summary>
    /// Enables the poster viewer experience for audio-only content.
    /// </summary>
    [JsonPropertyName("audioPosterMode")]
    public bool? AudioPosterMode { get; set; }

    /// <summary>
    /// Prevents automatic setup for elements with data-setup.
    /// </summary>
    [JsonPropertyName("autoSetup")]
    public bool? AutoSetup { get; set; }

    /// <summary>
    /// Disables Picture-in-Picture on the video element.
    /// </summary>
    [JsonPropertyName("disablePictureInPicture")]
    public bool? DisablePictureInPicture { get; set; }

    /// <summary>
    /// Enables document Picture-in-Picture when supported.
    /// </summary>
    [JsonPropertyName("enableDocumentPictureInPicture")]
    public bool? EnableDocumentPictureInPicture { get; set; }

    /// <summary>
    /// Enables smooth seeking on supported devices.
    /// </summary>
    [JsonPropertyName("enableSmoothSeeking")]
    public bool? EnableSmoothSeeking { get; set; }

    /// <summary>
    /// Uses experimental SVG icons instead of font icons.
    /// </summary>
    [JsonPropertyName("experimentalSvgIcons")]
    public bool? ExperimentalSvgIcons { get; set; }

    /// <summary>
    /// Fullscreen options.
    /// </summary>
    [JsonPropertyName("fullscreen")]
    public VideoJsFullscreenOptions? Fullscreen { get; set; }

    /// <summary>
    /// Sets the element id if not already provided.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Time in milliseconds to consider the user inactive.
    /// </summary>
    [JsonPropertyName("inactivityTimeout")]
    public int? InactivityTimeout { get; set; }

    /// <summary>
    /// List of playback rates.
    /// </summary>
    [JsonPropertyName("playbackRates")]
    public List<double>? PlaybackRates { get; set; }

    /// <summary>
    /// Hints the browser to allow inline playback on iOS.
    /// </summary>
    [JsonPropertyName("playsinline")]
    public bool? PlaysInline { get; set; }

    /// <summary>
    /// Component control bar options.
    /// </summary>
    [JsonPropertyName("controlBar")]
    public VideoJsControlBarOptions? ControlBar { get; set; }

    /// <summary>
    /// Media sources for the player.
    /// </summary>
    [JsonPropertyName("sources")]
    public List<VideoJsSource>? Sources { get; set; }

    /// <summary>
    /// Optional language code for player UI.
    /// </summary>
    [JsonPropertyName("language")]
    public string? Language { get; set; }

    /// <summary>
    /// Custom language translations.
    /// </summary>
    [JsonPropertyName("languages")]
    public Dictionary<string, Dictionary<string, string>>? Languages { get; set; }

    /// <summary>
    /// Enables the new live UI.
    /// </summary>
    [JsonPropertyName("liveui")]
    public bool? LiveUi { get; set; }

    /// <summary>
    /// Live tracker configuration.
    /// </summary>
    [JsonPropertyName("liveTracker")]
    public VideoJsLiveTrackerOptions? LiveTracker { get; set; }

    /// <summary>
    /// Treats autoplay:true the same as autoplay:'play'.
    /// </summary>
    [JsonPropertyName("normalizeAutoplay")]
    public bool? NormalizeAutoplay { get; set; }

    /// <summary>
    /// Custom message shown when playback is not supported.
    /// </summary>
    [JsonPropertyName("notSupportedMessage")]
    public string? NotSupportedMessage { get; set; }

    /// <summary>
    /// Disables title attributes on UI elements.
    /// </summary>
    [JsonPropertyName("noUITitleAttributes")]
    public bool? NoUiTitleAttributes { get; set; }

    /// <summary>
    /// Plugin initialization options.
    /// </summary>
    [JsonPropertyName("plugins")]
    public Dictionary<string, object>? Plugins { get; set; }

    /// <summary>
    /// Enables or disables the poster image component.
    /// </summary>
    [JsonPropertyName("posterImage")]
    public bool? PosterImage { get; set; }

    /// <summary>
    /// Prefer full window mode on platforms without fullscreen API.
    /// </summary>
    [JsonPropertyName("preferFullWindow")]
    public bool? PreferFullWindow { get; set; }

    /// <summary>
    /// Restore element when disposing the player.
    /// </summary>
    [JsonPropertyName("restoreEl")]
    public object? RestoreEl { get; set; }

    /// <summary>
    /// Suppresses not supported errors until user interaction.
    /// </summary>
    [JsonPropertyName("suppressNotSupportedError")]
    public bool? SuppressNotSupportedError { get; set; }

    /// <summary>
    /// Allows techs to override the poster life-cycle.
    /// </summary>
    [JsonPropertyName("techCanOverridePoster")]
    public bool? TechCanOverridePoster { get; set; }

    /// <summary>
    /// Preferred tech order.
    /// </summary>
    [JsonPropertyName("techOrder")]
    public List<string>? TechOrder { get; set; }

    /// <summary>
    /// User action configuration.
    /// </summary>
    [JsonPropertyName("userActions")]
    public VideoJsUserActionsOptions? UserActions { get; set; }

    /// <summary>
    /// VTT.js URL override.
    /// </summary>
    [JsonPropertyName("vtt.js")]
    public string? VttJs { get; set; }

    /// <summary>
    /// Spatial navigation options.
    /// </summary>
    [JsonPropertyName("spatialNavigation")]
    public VideoJsSpatialNavigationOptions? SpatialNavigation { get; set; }

    /// <summary>
    /// Child component configuration.
    /// </summary>
    [JsonPropertyName("children")]
    public object? Children { get; set; }

    /// <summary>
    /// HTML5 tech-specific options.
    /// </summary>
    [JsonPropertyName("html5")]
    public VideoJsHtml5Options? Html5 { get; set; }
}
