using Microsoft.Playwright;
using Test.Utils.Fixtures;
using Test.Utils.PageObjects;
using BrowserType = Test.Utils.Fixtures.BrowserType;

namespace Tests.NUnit.Playwright.Tests;

//TODO: cover with tests
[TestFixture]
public class TextBoxPageTests
{
    private readonly BrowserSetUpBuilder _browserSetUpBuilder = new();
    private TextBoxPage Page { get; set; }

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        Page = await _browserSetUpBuilder
            .WithBrowser(BrowserType.Chromium)
            .WithChannel("chrome")
            .InHeadlessMode(false)
            .WithTimeout(10000)
            .WithArgs("--start-maximized")
            .OpenNewPage<TextBoxPage>();
    }
    
    [Test]
    public async Task GoToTextBoxPage_TitleIsCorrect()
    {
        await Page.Open();
        var title = await Page.Title.TextContentAsync();

        Assert.That(title, Is.EqualTo("Text Box"));
    }
}