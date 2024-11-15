using Test.Utils.Fixtures;

namespace Tests.NUnit.Playwright.Tests.LinksPageTests;

//TODO: this set of test is better to run each test in separate browser or divide this test into 2 groups 
[TestFixture]
public class LinksThatLeadsToAnotherPage
{
    private readonly BrowserSetUpBuilder _browserSetUpBuilder = new();
    private Test.Utils.PageObjects.LinksPage Page { get; set; }

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        Page = await _browserSetUpBuilder
            .WithBrowser(BrowserType.Chromium)
            .WithChannel("chrome")
            .InHeadlessMode(true)
            .WithTimeout(10000)
            .WithSlowMo(100)
            .WithArgs("--start-maximized")
            .OpenNewPage<Test.Utils.PageObjects.LinksPage>();
        _browserSetUpBuilder.AddRequestResponseLogger();
        await Page.Open();
    }

    [Test]
    public async Task GoToLinksPage_TitleIsCorrect()
    {
        var title = await Page.Title.TextContentAsync();

        Assert.That(title, Is.EqualTo("Links"));
    }

    [Test]
    public async Task ClickOnHomeLink_ReturnsCorrectTab()
    {
        await Page.HomeLink.ClickAsync();
        var expectedPageUrl = Page.Url;

        Assert.That(expectedPageUrl, Is.EqualTo(Page.Url));
    }


    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _browserSetUpBuilder.Context!.CloseAsync();
        await Page.ClosePage();
    }
}