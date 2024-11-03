using Microsoft.Playwright;

namespace Test.Utils.PageObjects
{
    public class BrokenPage : IBasePage
    {
        public IPage? Page { get; set; }
        public string Url { get; } = "https://demoqa.com/broken";
        public string ExpectedTitle { get; } = "Broken Links - Images";

        public ILocator Title => Page!.Locator($"xpath=//h1[text()='{ExpectedTitle}']");
        public ILocator ValidImageText => Page!.GetByText("text='Valid image'");
        public ILocator BrokenImageText => Page!.GetByText("text='Broken image'");
        public ILocator ValidLinkText => Page!.GetByText("text='Valid Link'");
        public ILocator BrokenLinkText => Page!.GetByText("text='Broken Link'");
        public ILocator ValidImage => Page!.Locator("p~img[src='/images/Toolsqa.jpg']");
        public ILocator BrokenImage => Page!.Locator("p~img[src='/images/Toolsqa_1.jpg");
        public ILocator ValidLink => Page!.GetByRole(AriaRole.Link, new() { Name = "Click Here for Valid Link" });
        public ILocator BrokenLink => Page!.GetByRole(AriaRole.Link, new() { Name = "Click Here for Broken Link" });

        public async Task<BrokenPage> Open()
        {
            await Page!.GotoAsync(Url);
            return this;
        }

        public async Task<bool> TextForElementsVisible()
        {
            return
                await ValidImageText.IsVisibleAsync() &&
                await BrokenImageText.IsVisibleAsync() &&
                await ValidImageText.IsVisibleAsync() &&
                await BrokenLinkText.IsVisibleAsync();
        }

        public async Task<bool> ImgsAndLinksVisible()
        {
            return
                await ValidImage.IsVisibleAsync() &&
                await BrokenImage.IsVisibleAsync() &&
                await ValidLink.IsVisibleAsync() &&
                await BrokenLink.IsVisibleAsync();
        }
    }
}