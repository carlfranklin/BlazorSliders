using System.Threading.Tasks;
using PlaywrightSharp;
using VerifyXunit;
using Xunit;

[UsesVerify]
public class IntegrationTest :
    IClassFixture<PlaywrightFixture>
{
    IPage page;

    public IntegrationTest(PlaywrightFixture fixture)
    {
        page = fixture.Page;
    }

    [Fact]
    public async Task Vertical()
    {
        await LoadPage("/");
        await Verifier.Verify(page);
    }

    [Fact]
    public async Task Crazy()
    {
        await LoadPage("crazy");
        await Verifier.Verify(page);
    }

    [Fact]
    public async Task Horizontals()
    {
        await LoadPage("horizontals");
        await Verifier.Verify(page);
    }

    [Fact]
    public async Task FourPanels()
    {
        await LoadPage("fourpanels");
        await Verifier.Verify(page);
    }

    [Fact]
    public async Task WindowResize()
    {
        await LoadPage("windowresize");
        await Verifier.Verify(page);
    }

    [Fact]
    public async Task DoubleVertical()
    {
        await LoadPage("doublevertical");
        await Verifier.Verify(page);
    }

    [Fact]
    public async Task DoubleHorizontal()
    {
        await LoadPage("doublehorizontal");
        await Verifier.Verify(page);
    }

    Task LoadPage(string url)
    {
        return page.GoToAsync($"https://localhost:5001/{url}");
    }
}