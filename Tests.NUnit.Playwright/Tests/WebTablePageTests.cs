using Test.Utils.Fixtures;
using Test.Utils.PageObjects;

namespace Tests.NUnit.Playwright.Tests;

//TODO: cover with tests
[TestFixture]
public class WebTablePageTests
{
    private readonly BrowserSetUpBuilder _browserSetUpBuilder = new();
    private WebTablePage Page { get; set; }

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        Page = await _browserSetUpBuilder
            .WithBrowser(BrowserType.Chromium)
            .WithChannel("chrome")
            .InHeadlessMode(false)
            .WithTimeout(10000)
            .WithArgs("--start-maximized")
            .OpenNewPage<WebTablePage>();
        
        await Page.Open();
    }
}