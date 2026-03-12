using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;
using Soenneker.Blazor.Videojs.Abstract;

namespace Soenneker.Blazor.Videojs.Registrars;

/// <summary>
/// A Blazor interop library for Video.js
/// </summary>
public static class VideoJsInteropRegistrar
{
    /// <summary>
    /// Adds <see cref="IVideoJsInterop"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddVideoJsInteropAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped();
        services.TryAddScoped<IVideoJsInterop, VideoJsInterop>();

        return services;
    }
}
