using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Test.Utils.PageObjects
{
    public class DynamicPropertiesPage : IBasePage
    {
        public IPage? Page { get; set; }
        public string Url { get; } = "https://demoqa.com/dynamic-properties";
        public string ExpectedTitle { get; } = "Dynamic Properties";
        
        public ILocator Title => Page!.Locator("xpath=//h1[text()='Dynamic Properties']");
        public ILocator EnableButton => Page!.Locator("button#enableAfter");
        public ILocator ColorButton => Page!.Locator("#colorChange");
        public ILocator VisibleAfterButton => Page!.Locator("#visibleAfter");


        public async Task Open()
        {
            await Page!.GotoAsync(Url);
        }
    }
}