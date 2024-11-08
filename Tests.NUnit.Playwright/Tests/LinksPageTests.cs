using Test.Utils.Fixtures;
using Test.Utils.PageObjects;

namespace Tests.NUnit.Playwright.Tests;

//TODO: cover with tests
[TestFixture]
public class LinksPageTests
{
    private readonly BrowserSetUpBuilder _browserSetUpBuilder = new();
    private LinksPage Page { get; set; }

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        Page = await _browserSetUpBuilder
            .WithBrowser(BrowserType.Chromium)
            .WithChannel("chrome")
            .InHeadlessMode(false)
            .WithTimeout(10000)
            .WithArgs("--start-maximized")
            .OpenNewPage<LinksPage>();
        
        await Page.Open();
    }
    
    [Test]
    public async Task GoToLinksPage_TitleIsCorrect()
    {
        var title = await Page.Title.TextContentAsync();

        Assert.That(title, Is.EqualTo("Links"));
    }
}