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
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddVideoJsInteropAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped();
        services.TryAddScoped<IVideoJsInterop, VideoJsInterop>();

        return services;
    }
}
