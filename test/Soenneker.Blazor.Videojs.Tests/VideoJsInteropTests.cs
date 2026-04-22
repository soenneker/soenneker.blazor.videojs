using Soenneker.Blazor.Videojs.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Blazor.Videojs.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class VideoJsInteropTests : HostedUnitTest
{
    private readonly IVideoJsInterop _blazorlibrary;

    public VideoJsInteropTests(Host host) : base(host)
    {
        _blazorlibrary = Resolve<IVideoJsInterop>(true);
    }

    [Test]
    public void Default()
    {

    }
}
