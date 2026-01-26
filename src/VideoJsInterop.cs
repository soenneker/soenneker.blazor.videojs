using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Soenneker.Blazor.Videojs.Abstract;
using Soenneker.Blazor.Videojs.Configuration;
using Soenneker.Blazor.Videojs.Dtos;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Asyncs.Initializers;
using Soenneker.Utils.Json;

namespace Soenneker.Blazor.Videojs;

/// <inheritdoc cref="IVideoJsInterop"/>
public sealed class VideoJsInterop: IVideoJsInterop
{
    private readonly IJSRuntime _jsRuntime;
    private readonly ILogger<VideoJsInterop> _logger;
    private readonly IResourceLoader _resourceLoader;
    private readonly AsyncInitializer<bool> _scriptInitializer;

    private const string _module = "Soenneker.Blazor.Videojs/js/videojsinterop.js";
    private const string _moduleName = "VideoJsInterop";
    private const string _cdnCssUrl = "https://cdn.jsdelivr.net/npm/video.js@8.23.4/dist/video-js.min.css";
    private const string _cdnCssIntegrity = "sha256-aZM6nunxxsAVKRkQNSLoYiISSWTiGV0o6y0MBDwm9Zw=";
    private const string _cdnScriptUrl = "https://cdn.jsdelivr.net/npm/video.js@8.23.4/dist/video.min.js";
    private const string _cdnScriptIntegrity = "sha256-rNCoGkzHiSjxUwWYdi7Xg+6RZnS/OgvypsJqbAsW7Us=";
    private const string _localCssUrl = "_content/Soenneker.Blazor.Videojs/css/video-js.min.css";
    private const string _localScriptUrl = "_content/Soenneker.Blazor.Videojs/js/video.min.js";

    public VideoJsInterop(IJSRuntime jSRuntime, ILogger<VideoJsInterop> logger, IResourceLoader resourceLoader)
    {
        _jsRuntime = jSRuntime;
        _logger = logger;
        _resourceLoader = resourceLoader;

        _scriptInitializer = new AsyncInitializer<bool>(InitializeScript);
    }

    private async ValueTask InitializeScript(bool useCdn, CancellationToken cancellationToken)
    {
        try
        {
            if (useCdn)
            {
                await _resourceLoader.LoadStyle(_cdnCssUrl, _cdnCssIntegrity, cancellationToken: cancellationToken);
                await _resourceLoader.LoadScriptAndWaitForVariable(_cdnScriptUrl, "videojs", _cdnScriptIntegrity, cancellationToken: cancellationToken);
            }
            else
            {
                await _resourceLoader.LoadStyle(_localCssUrl, cancellationToken: cancellationToken);
                await _resourceLoader.LoadScriptAndWaitForVariable(_localScriptUrl, "videojs", cancellationToken: cancellationToken);
            }
            await _resourceLoader.ImportModuleAndWaitUntilAvailable(_module, _moduleName, 100, cancellationToken);
        }
        catch (JSException ex)
        {
            _logger.LogError(ex, "Failed to initialize Video.js resources");
            throw;
        }
    }

    public ValueTask Initialize(bool useCdn = true, CancellationToken cancellationToken = default)
    {
        return _scriptInitializer.Init(useCdn, cancellationToken);
    }

    public async ValueTask Create(ElementReference elementReference, string elementId, VideoJsConfiguration? configuration = null,
        CancellationToken cancellationToken = default)
    {
        bool useCdn = configuration?.UseCdn ?? true;
        await _scriptInitializer.Init(useCdn, cancellationToken);

        string? json = configuration == null ? null : JsonUtil.Serialize(configuration);

        await _jsRuntime.InvokeVoidAsync("VideoJsInterop.create", cancellationToken, elementReference, elementId, json);
    }

    public ValueTask UpdateSources(string elementId, List<VideoJsSource> sources, CancellationToken cancellationToken = default)
    {
        return _jsRuntime.InvokeVoidAsync("VideoJsInterop.updateSources", cancellationToken, elementId, sources);
    }

    public ValueTask SetPoster(string elementId, string? poster, CancellationToken cancellationToken = default)
    {
        return _jsRuntime.InvokeVoidAsync("VideoJsInterop.setPoster", cancellationToken, elementId, poster);
    }

    public ValueTask RegisterEvent(string elementId, string eventName, DotNetObjectReference<VideoJsEventBridge> dotNetReference,
        string callbackMethod, CancellationToken cancellationToken = default)
    {
        return _jsRuntime.InvokeVoidAsync("VideoJsInterop.registerEvent", cancellationToken, elementId, eventName, dotNetReference, callbackMethod);
    }

    public ValueTask Dispose(string elementId, CancellationToken cancellationToken = default)
    {
        return _jsRuntime.InvokeVoidAsync("VideoJsInterop.dispose", cancellationToken, elementId);
    }
}
