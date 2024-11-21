using NUnit.Framework.Interfaces;
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
            .WithSlowMo(100)
            .WithTimeout(10000)
            .WithVideoSize(1900, 1080)
            .SaveVideo("videos/")
            .WithArgs("--start-maximized")
            .OpenNewPage<WebTablePage>();
        _browserSetUpBuilder.AddRequestResponseLogger();
        await Page.Open();
    }

    [SetUp]
    public async Task SetUp()
    {
        var traceName = TestContext.CurrentContext.Test.ClassName + "/" + TestContext.CurrentContext.Test.Name;
        await _browserSetUpBuilder.StartTracing(traceName);
    }

    [Test]
    public async Task OpenWebTablePage()
    {
        var title = await Page.Title.TextContentAsync();

        Assert.That(title, Is.EqualTo(Page.ExpectedTitle));
    }

    [Test]
    public async Task WebTableSmoke()
    {
        var rowCount = await Page.GetRowCountAsync(Page.Page!, Page.TableXPath);
        var columnCount = await Page.GetColumnCountAsync(Page.Page!, Page.TableXPath);
        var cellZeroZero = await Page.GetCellTextAsync(Page.Page!, Page.TableXPath, 0, 0);
        var cellOneOne = await Page.GetCellTextAsync(Page.Page!, Page.TableXPath, 1, 1);
        var cellTwoTwo = await Page.GetCellTextAsync(Page.Page!, Page.TableXPath, 2, 2);

        Assert.Multiple(() =>
        {
            Assert.That(rowCount, Is.EqualTo(3));
            Assert.That(columnCount, Is.EqualTo(7));
            Assert.That(cellZeroZero, Is.EqualTo("Cierra"));
            Assert.That(cellOneOne, Is.EqualTo("Cantrell"));
            Assert.That(cellTwoTwo, Is.EqualTo("29"));
        });
    }

    [TearDown]
    public async Task TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            await _browserSetUpBuilder.Screenshot(
                TestContext.CurrentContext.Test.ClassName!,
                TestContext.CurrentContext.Test.Name);
        }

        var tracePath = Path.Combine(
            "playwright-traces",
            $"{TestContext.CurrentContext.Test.ClassName}",
            $"{TestContext.CurrentContext.Test.Name}.zip");
        await _browserSetUpBuilder.StopTracing(tracePath);
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _browserSetUpBuilder.Page!.CloseAsync();
        await _browserSetUpBuilder.Context!.CloseAsync();
    }
}