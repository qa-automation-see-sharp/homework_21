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
            .WithSlowMo(100)
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

    [Test]
    public async Task ClickOnCreatedLink_ReturnsCorrectResponse()
    {
        await Page.CreatedLink.ClickAsync();
        var responseMessage = await Page.ResponseMessage.TextContentAsync();
        
        Assert.That(responseMessage, 
            Is.EqualTo("Link has responded with staus 201 and status text Created"));
    }
    
    [Test]
    public async Task ClickOnNoContentLink_ReturnsCorrectResponse()
    {
        await Page.NoContentLink.ClickAsync();
        var responseMessage = await Page.ResponseMessage.TextContentAsync();
        
        Assert.That(responseMessage, 
            Is.EqualTo("Link has responded with staus 204 and status text No Content"));
    }
    
    [Test]
    public async Task MovedLink_ReturnsCorrectResponse()
    {
        await Page.MovedLink.ClickAsync();
        var responseMessage = await Page.ResponseMessage.TextContentAsync();
        
        Assert.That(responseMessage, 
            Is.EqualTo("Link has responded with staus 301 and status text Moved Permanently"));
    }
    
    [Test]
    public async Task BadRequestLink_ReturnsCorrectResponse()
    {
        await Page.BadRequestLink.ClickAsync();
        var responseMessage = await Page.ResponseMessage.TextContentAsync();
        
        Assert.That(responseMessage, 
            Is.EqualTo("Link has responded with staus 400 and status text Bad Request"));
    }
    
    [Test]
    public async Task UnauthorizedLink_ReturnsCorrectResponse()
    {
        await Page.UnauthorizedLink.ClickAsync();
        var responseMessage = await Page.ResponseMessage.TextContentAsync();
        
        Assert.That(responseMessage, 
            Is.EqualTo("Link has responded with staus 401 and status text Unauthorized"));
    }
    
    [Test]
    public async Task ForbiddenLink_ReturnsCorrectResponse()
    {
        await Page.ForbiddenLink.ClickAsync();
        var responseMessage = await Page.ResponseMessage.TextContentAsync();
        
        Assert.That(responseMessage, 
            Is.EqualTo("Link has responded with staus 403 and status text Forbidden"));
    }
    
    [Test]
    public async Task NotFoundLink_ReturnsCorrectResponse()
    {
        await Page.NotFoundLink.ClickAsync();
        var responseMessage = await Page.ResponseMessage.TextContentAsync();
        
        Assert.That(responseMessage, 
            Is.EqualTo("Link has responded with staus 404 and status text Not Found"));
    }
    
    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _browserSetUpBuilder.Context!.CloseAsync();
        await Page.ClosePage();
    }
}