using Microsoft.Playwright;

namespace Test.Utils.PageObjects;

public partial class WebTablePage: IBasePage
{
    public IPage? Page { get; set; }
    
    public string Url { get; } = "https://demoqa.com/webtables";
    public string ExpectedTitle { get; } = "Web Tables";
}