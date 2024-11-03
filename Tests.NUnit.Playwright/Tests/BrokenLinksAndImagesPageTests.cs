using Microsoft.Playwright;
using NUnit.Framework.Interfaces;
using Test.Utils.Fixtures;
using Test.Utils.PageObjects;
using BrowserType = Test.Utils.Fixtures.BrowserType;

namespace Tests.NUnit.Playwright.Tests;

//TODO: cover with tests
[TestFixture]
public class BrokenLinksAndImagesPageTests
{
    private readonly BrowserSetUpBuilder _browserSetUp = new();
    private BrokenPage? Page { get; set; }

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        Page = await _browserSetUp
            .WithBrowser(BrowserType.Chromium)
            .WithChannel("chrome")
            .InHeadlessMode(false)
            .WithSlowMo(100)
            .WithTimeout(10000)
            .WithVideoSize(1900, 1080)
            .SaveVideo("videos/")
            .WithArgs("--start-maximized")
            .OpenNewPage<BrokenPage>();
        _browserSetUp.AddRequestResponseLogger();
        await Page!.Open();
    }

    [SetUp]
    public async Task SetUp()
    {
        var traceName = TestContext.CurrentContext.Test.ClassName + "/" + TestContext.CurrentContext.Test.Name;
        await _browserSetUp.StartTracing(traceName);
    }

    [Test]
    public async Task OpenBrokenPage()
    {
        var title = await Page!.Title.TextContentAsync();

        Assert.That(title, Is.EqualTo(Page!.ExpectedTitle));
    }

    [Test]
    public async Task VisibleElementsOnPage()
    {
        Assert.Multiple(async () =>
       {
           Assert.That(Page.ImgsAndLinksVisible, Is.True);
           Assert.That(Page.TextForElementsVisible, Is.True);
       });
    }

    [Test]
    public async Task ClickOnValidLink()
    {
        await Page.ValidLink.ClickAsync();
        Assert.That(_browserSetUp.Page.Url, Is.EqualTo("https://demoqa.com/"));
    }

    [Test]
    public async Task ClickBrokenLink()
    {
        await Page.ValidLink.ClickAsync();
        Assert.That(Page.Page.Url, Is.EqualTo("http://the-internet.herokuapp.com/status_codes/500"));
    }

    [TearDown]
    public async Task TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            await _browserSetUp.Screenshot(
                TestContext.CurrentContext.Test.ClassName,
                TestContext.CurrentContext.Test.Name);
        }

        var tracePAth = Path.Combine(
            "playwright-traces",
            $"{TestContext.CurrentContext.Test.ClassName}",
            $"{TestContext.CurrentContext.Test.Name}.zip");
        await _browserSetUp.StopTracing(tracePAth);
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _browserSetUp.Page!.CloseAsync();
        await _browserSetUp.Context!.CloseAsync();
    }
}