using Microsoft.Playwright;

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
        await Map.SearchQueryInput.WaitForAsync(_waitForVisible);
        await Map.SearchQueryInput.FillAsync("");
        await Map.SearchQueryInput.FillAsync(item);

        await Map.SearchBtn.WaitForAsync(_waitForVisible);
        await Map.SearchBtn.ClickAsync();
    }

    public async Task<bool> IsSearchedItemShown(string item)
    {
        await Map.SearchResultTxt(item).WaitForAsync(_waitForVisible);
        return await Map.SearchResultTxt(item).IsVisibleAsync();
    }

    public async Task ClickSignIn()
    {
        await Map.LoginLink.WaitForAsync(_waitForVisible);
        await Map.LoginLink.ClickAsync();
    }
    
    public async Task ClickMoreDetails()
    {
        await Map.MoreDetailsBtn.WaitForAsync(_waitForVisible);
        await Map.MoreDetailsBtn.ClickAsync();
    }
    
}
public class HomePageMap
{
    private IPage _page;
    public HomePageMap(IPage page)
    {
        _page = page;
    }

    public ILocator SearchQueryInput => _page.Locator("#search_query_top");
    public ILocator SearchResultTxt(string searchItem) => _page.Locator($"//img[@title='{searchItem}']");
    public ILocator SearchBtn => _page.Locator("//button[@name='submit_search']");
    public ILocator LoginLink => _page.Locator(".login");
    public ILocator MoreDetailsBtn => _page.Locator("//a[@title='View']");
}