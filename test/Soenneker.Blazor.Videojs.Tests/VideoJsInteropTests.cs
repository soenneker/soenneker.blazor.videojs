using Soenneker.Blazor.Videojs.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Blazor.Videojs.Tests;

[Collection("Collection")]
public sealed class VideoJsInteropTests : FixturedUnitTest
{
    private readonly IVideoJsInterop _blazorlibrary;

    public VideoJsInteropTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _blazorlibrary = Resolve<IVideoJsInterop>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
