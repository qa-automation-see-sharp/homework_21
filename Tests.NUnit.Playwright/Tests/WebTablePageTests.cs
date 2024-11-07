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
    
    [Test]
    public async Task GoToWebTablePage_TitleIsCorrect()
    {
        var title = await Page.Title.TextContentAsync();

        Assert.That(title, Is.EqualTo("Web Tables"));
    }

    [Test]
    public async Task ClickAddButton_ReturnRegistrationForm()
    {
        await Page.ClickAddButton();
        var registrationFormIsDisplayed = Page.RegistrationForm.IsVisibleAsync();
        
        Assert.That(registrationFormIsDisplayed, Is.True);
    }

    [Test]
    public async Task FillInRegistrationForm_ReturnSavedData()
    {
        await Page.ClickAddButton();

        await Page.EnterFirstName("Liuda");
        await Page.EnterLastName("Test");
        await Page.EnterEmail("test@test.com");
        await Page.EnterAge("25");
        await Page.EnterSalary("12345");
        await Page.EnterDepartment("QA");

        await Page.ClickSubmitButton();

        var rows = Page.FindRows();
        
    }
}