using Microsoft.Playwright;

namespace Test.Utils.PageObjects;

public class BrokenLinksAndImagesPage : IBasePage
    {
    public IPage? Page { get; set; }
    public string Url { get; } = "https://demoqa.com/broken";
    public string ExpectedTitle { get; } = "Broken Links - Images";
    
    public ILocator Title => Page!.Locator("xpath=//h1[text()='Broken Links - Images']");
    
    public async Task<BrokenLinksAndImagesPage> Open()
    {
        await Page!.GotoAsync(Url);
        return this;
    }
    
    public async Task<BrokenLinksAndImagesPage> ClosePage()
    {
        await Page.CloseAsync();
        return this;
    }
}