using Soenneker.Blazor.Videojs.Configuration;
using Soenneker.Blazor.Videojs.Dtos;
using System.Collections.Generic;
using System.Text.Json.Serialization.Metadata;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;

namespace Soenneker.Blazor.Videojs;

[JsonSourceGenerationOptions(JsonSerializerDefaults.Web, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, ReadCommentHandling = JsonCommentHandling.Skip, UseStringEnumConverter = true)]
[JsonSerializable(typeof(VideoJsConfiguration))]
[JsonSerializable(typeof(object))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(bool))]
[JsonSerializable(typeof(int))]
[JsonSerializable(typeof(long))]
[JsonSerializable(typeof(double))]
[JsonSerializable(typeof(decimal))]
[JsonSerializable(typeof(float))]
[JsonSerializable(typeof(System.Text.Json.JsonElement))]
[JsonSerializable(typeof(System.Collections.Generic.Dictionary<string, object?>))]
[JsonSerializable(typeof(System.Collections.Generic.List<object?>))]
[JsonSerializable(typeof(string[]))]
[JsonSerializable(typeof(object[]))]
internal partial class LibraryJsonContext : JsonSerializerContext
{
    internal static JsonTypeInfo<T> Get<T>() =>
        (JsonTypeInfo<T>)(Default.GetTypeInfo(typeof(T)) ?? throw new NotSupportedException($"No generated JSON metadata for {typeof(T)}."));

    internal static JsonSerializerOptions WithContext(JsonSerializerContext? additionalContext)
    {
        JsonSerializerOptions defaults = Get<object>().Options;
        if (additionalContext is null)
            return defaults;
        var options = new JsonSerializerOptions(defaults)
        {
            TypeInfoResolver = JsonTypeInfoResolver.Combine(defaults.TypeInfoResolver!, additionalContext)
        };
        options.MakeReadOnly();
        return options;
    }
}
