using Microsoft.Playwright;

namespace Test.Utils.PageObjects;

public class DynamicPropertiesPage : IBasePage
    {
    public IPage? Page { get; set; }
    public string Url { get; } = "https://demoqa.com/text-box";
    public string ExpectedTitle { get; } = "Text Box";
    
    public ILocator Title => Page!.Locator("xpath=//h1[text()='Text Box']");
    
    public async Task<DynamicPropertiesPage> Open()
    {
        await Page!.GotoAsync(Url);
        return this;
    }
    
    public async Task<DynamicPropertiesPage> ClosePage()
    {
        await Page!.CloseAsync();
        return this;
    }
}