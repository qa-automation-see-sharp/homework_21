using Test.Utils.Fixtures;
using Test.Utils.PageObjects;

namespace Tests.NUnit.Playwright.Tests;

[TestFixture]
public class BrokenLinksAndImagesPageTests
{
    private readonly BrowserSetUpBuilder _browserSetUpBuilder = new();
    private BrokenLinksAndImagesPage Page { get; set; }

    [SetUp]
    public async Task OneTimeSetUp()
    {
        Page = await _browserSetUpBuilder
            .WithBrowser(BrowserType.Chromium)
            .WithChannel("chrome")
            .InHeadlessMode(true)
            .WithTimeout(10000)
            .WithArgs("--start-maximized")
            .OpenNewPage<BrokenLinksAndImagesPage>();
        
        await Page.Open();
    }
    
    [Test]
    public async Task GoToButtonsPage_TitleIsCorrect()
    {
        var title = await Page.Title.TextContentAsync();

        Assert.That(title, Is.EqualTo("Broken Links - Images"));
    }
    
    [Test]
    public async Task ClickOnValidLink_ReturnsCorrectTab()
    {
        await Page.ValidLink.ClickAsync();
        var expectedPageUrl = Page.Page!.Url;

        Assert.That(expectedPageUrl, Is.EqualTo(Page.Url));
    }
    
    [Test]
    public async Task ClickOnBrokenLink_ReturnsBrokenTab()
    {
        await Page.BrokenLink.ClickAsync();
        await Task.Delay(3000);

        Assert.That(Page.Page!.Url, Is.EqualTo(("http://the-internet.herokuapp.com/status_codes/500")));
    }
    
    [TearDown]
    public async Task OneTimeTearDown()
    {
        await _browserSetUpBuilder.Context!.CloseAsync();
        await Page.ClosePage();
    }
}