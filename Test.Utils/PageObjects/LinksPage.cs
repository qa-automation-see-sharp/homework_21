﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Test.Utils.PageObjects;

public class LinksPage : IBasePage
{
    public IPage? Page { get; set; }
    public string Url { get; } = "https://demoqa.com/links";
    public string ExpectedTitle { get; } = "Links";

    public ILocator Title => Page!.Locator("xpath=//h1[text()='Links']");
    public ILocator HomeLink => Page!.Locator("xpath=/html//a[@id='simpleLink']");
    public ILocator HomeZDs2pLink => Page!.Locator("xpath=/html//a[@id='dynamicLink']");
    public ILocator CreatedLink => Page!.Locator("xpath=/html//a[@id='created']");
    public ILocator NoContentLink => Page!.Locator("xpath=/html//a[@id='no-content']");
    public ILocator MovedLink => Page!.Locator("xpath=/html//a[@id='moved']");
    public ILocator BadRequestLink => Page!.Locator("xpath=/html//a[@id='bad-request']");
    public ILocator UnauthorizedLink => Page!.Locator("xpath=/html//a[@id='unauthorized']");
    public ILocator ForbiddenLink => Page!.Locator("xpath=/html//a[@id='forbidden']");
    public ILocator NotFoundLink => Page!.Locator("xpath=/html//a[@id='invalid-url']");
    public ILocator ResponseMessage => Page!.Locator("#linkResponse");

    public async Task<LinksPage> Open()
    {
        await Page!.GotoAsync(Url);
        return this;
    }

    public async Task<LinksPage> ClosePage()
    {
        await Page!.CloseAsync();
        return this;
    }

    public async Task<LinksPage> CLickAndWaitForRequest(Func<Task> action, Func<IResponse, bool> urlOrPredicate)
    {
        await Page!.RunAndWaitForResponseAsync(action, urlOrPredicate);
        return this;
    }
}
