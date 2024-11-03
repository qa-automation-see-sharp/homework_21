using Microsoft.Playwright;
using NUnit.Framework.Interfaces;
using Test.Utils.Fixtures;
using Test.Utils.PageObjects;
using Microsoft.Playwright.NUnit;

namespace Tests.NUnit.Playwright.Tests;

//TODO: cover with tests
[TestFixture]
public class LinksPageTests : PageTest
{
    private readonly BrowserSetUpBuilder _browserSetUp = new();
    private LinksPage? Page { get; set; }

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        Page = await _browserSetUp
            //.WithBrowser(BrowserType.Chromium)
            .WithChannel("chrome")
            .InHeadlessMode(false)
            .WithSlowMo(100)
            .WithTimeout(10000)
            .WithVideoSize(1900, 1080)
            .SaveVideo("videos/")
            .WithArgs("--start-maximized")
            .OpenNewPage<LinksPage>();
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
    public async Task OpenLinksPage()
    {
        var title = await Page!.Title.TextContentAsync();

        Assert.That(title, Is.EqualTo(Page!.ExpectedTitle));
    }

    [Test]
    public async Task LinksNotBeEmpty()
    {
        var linksList = Page!.Links;
        for (int i = 0; i < await linksList.CountAsync(); i++)
        {
            Assert.That(linksList.Nth(i).GetAttributeAsync("href"), Is.Not.Empty);
        }
    }
    
    [Test]
    public async Task ClickOnHomeLink(){

        var page = await _browserSetUp.Context.NewPageAsync();

        await Page!.HomeLink.ClickAsync();
        await page.WaitForLoadStateAsync();

        Assert.That(page.Url.Equals("https://demoqa.com/"));
    } 

    [Test]
    public async Task CheckResponseLink()
    {
        await Page.CreatedLink.ClickAsync();
        await Expect(Page.Response).ToHaveTextAsync("Link has responded with staus 201 and status text Created");
        await Page.NoContentLink.ClickAsync();
        await Expect(Page.Response).ToHaveTextAsync("Link has responded with staus 204 and status text No Content");
        await Page.NotFoundLink.ClickAsync();
        await Expect(Page.Response).ToHaveTextAsync("Link has responded with staus 404 and status text Not Found");
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