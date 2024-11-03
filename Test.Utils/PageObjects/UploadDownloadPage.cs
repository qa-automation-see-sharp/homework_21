using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Test.Utils.PageObjects
{
    public class UploadDownloadPage : IBasePage
    {
        public IPage? Page { get; set; }
        public string Url { get; } = "https://demoqa.com/upload-download";
        public string ExpectedTitle { get; } = "Upload and Download";

        public ILocator Title => Page!.Locator("xpath=//h1[text()='Upload and Download']");
        public ILocator DownloadButton => Page!.Locator("#downloadButton");
        public ILocator UploadButton => Page!.Locator("#uploadFile");
        public ILocator FilePath => Page!.Locator("#uploadedFilePath");
        
        public async Task<UploadDownloadPage> Open()
        {
            await Page!.GotoAsync(Url);
            return this;
        }
    }
}