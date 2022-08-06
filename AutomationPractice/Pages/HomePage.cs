﻿using Microsoft.Playwright;

namespace AutomationPractice.Pages;

public class HomePage
{
    private HomePageMap Map;
    private IPage _page;
    private const string HomeUrl = "http://automationpractice.com";

    private LocatorWaitForOptions _waitForVisible;
    private LocatorWaitForOptions _waitForHidden;
    
    public HomePage(IPage page)
    {
        _page = page;
        Map = new HomePageMap(page);
        
        _waitForVisible = new LocatorWaitForOptions { State = WaitForSelectorState.Visible };
        _waitForHidden = new LocatorWaitForOptions { State = WaitForSelectorState.Hidden };
    }
    
    public async Task GoHome()
    {
        await _page.GotoAsync(HomeUrl);
    }

    public async Task Search(string item)
    {
        await Map.SearchQueryTxt.WaitForAsync(_waitForVisible);
        await Map.SearchQueryTxt.FillAsync("");
        await Map.SearchQueryTxt.FillAsync(item);

        await Map.SearchBtn.WaitForAsync(_waitForVisible);
        await Map.SearchBtn.ClickAsync();
    }

    public async Task<bool> IsSearchedItemShown(string item)
    {
        await Map.SearchResultTxt(item).WaitForAsync(_waitForVisible);
        return await Map.SearchResultTxt(item).IsVisibleAsync();
    }
    
}
public class HomePageMap
{
    private IPage _page;

    public HomePageMap(IPage page)
    {
        _page = page;
    }

    public ILocator SearchQueryTxt => _page.Locator("#search_query_top");
    public ILocator SearchResultTxt(string searchItem) => _page.Locator($"//img[@title='{searchItem}']");
    public ILocator SearchBtn => _page.Locator("//button[@name='submit_search']");
}