using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Blazor.Videojs.Abstract;
using Soenneker.Blazor.Videojs.Configuration;
using Soenneker.Blazor.Videojs.Dtos;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using Soenneker.Utils.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Blazor.Videojs;

/// <inheritdoc cref="IVideoJsInterop"/>
public sealed class VideoJsInterop : IVideoJsInterop
{
    private const string _modulePath = "/_content/Soenneker.Blazor.Videojs/js/videojsinterop.js";
    private const string _cdnCssUrl = "https://cdn.jsdelivr.net/npm/video.js@8.23.4/dist/video-js.min.css";
    private const string _cdnCssIntegrity = "sha256-aZM6nunxxsAVKRkQNSLoYiISSWTiGV0o6y0MBDwm9Zw=";
    private const string _cdnScriptUrl = "https://cdn.jsdelivr.net/npm/video.js@8.23.4/dist/video.min.js";
    private const string _cdnScriptIntegrity = "sha256-rNCoGkzHiSjxUwWYdi7Xg+6RZnS/OgvypsJqbAsW7Us=";
    private const string _localCssUrl = "/_content/Soenneker.Blazor.Videojs/css/video-js.min.css";
    private const string _localScriptUrl = "/_content/Soenneker.Blazor.Videojs/js/video.min.js";

    private readonly ILogger<VideoJsInterop> _logger;
    private readonly IResourceLoader _resourceLoader;
    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly AsyncInitializer<bool> _scriptInitializer;

    private readonly CancellationScope _cancellationScope = new();

    public VideoJsInterop(ILogger<VideoJsInterop> logger, IResourceLoader resourceLoader, IModuleImportUtil moduleImportUtil)
    {
        _logger = logger;
        _resourceLoader = resourceLoader;
        _moduleImportUtil = moduleImportUtil;

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

            _ = await _moduleImportUtil.GetContentModuleReference(_modulePath, cancellationToken);
        }
        catch (JSException ex)
        {
            _logger.LogError(ex, "Failed to initialize Video.js resources");
            throw;
        }
    }

    public async ValueTask Initialize(bool useCdn = true, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
            await _scriptInitializer.Init(useCdn, linked);
    }

    public async ValueTask Create(ElementReference elementReference, string elementId, VideoJsConfiguration? configuration = null,
        CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            bool useCdn = configuration?.UseCdn ?? true;
            await _scriptInitializer.Init(useCdn, linked);

            string? json = configuration == null ? null : JsonUtil.Serialize(configuration);

            IJSObjectReference jsRef = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await jsRef.InvokeVoidAsync("create", linked, elementReference, elementId, json);
        }
    }

    public async ValueTask UpdateSources(string elementId, List<VideoJsSource> sources, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference jsRef = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await jsRef.InvokeVoidAsync("updateSources", linked, elementId, sources);
        }
    }

    public async ValueTask SetPoster(string elementId, string? poster, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference jsRef = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await jsRef.InvokeVoidAsync("setPoster", linked, elementId, poster);
        }
    }

    public async ValueTask RegisterEvent(string elementId, string eventName, DotNetObjectReference<VideoJsEventBridge> dotNetReference, string callbackMethod,
        CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference jsRef = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await jsRef.InvokeVoidAsync("registerEvent", linked, elementId, eventName, dotNetReference, callbackMethod);
        }
    }

    public async ValueTask Dispose(string elementId, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference jsRef = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await jsRef.InvokeVoidAsync("dispose", linked, elementId);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _moduleImportUtil.DisposeContentModule(_modulePath);
        await _cancellationScope.DisposeAsync();
    }
}