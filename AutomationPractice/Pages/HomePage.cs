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

    public async Task HoverWomenCategory()
    {
        await Map.WomenCategory.WaitForAsync(_waitForVisible);
        await Map.WomenCategory.HoverAsync();
    }
    public async Task<bool?> IsWomenCategoryHovered()
    {
        await Map.CategoryHovered.First.WaitForAsync(_waitForVisible);
        return await Map.CategoryHovered.First.IsVisibleAsync();
    }
    
    public async Task HoverDressesCategory()
    {
        await Map.DressesCategory.WaitForAsync(_waitForVisible);
        await Map.DressesCategory.HoverAsync();
    }
    public async Task<bool?> IsDressesCategoryHovered()
    {
        await Map.CategoryHovered.Last.WaitForAsync(_waitForVisible);
        return await Map.CategoryHovered.Last.IsVisibleAsync();
    }

    public async Task ClickOnBlousesUnderWomenCategory()
    {
        await HoverWomenCategory();
        await Map.BlousesSubCategory.WaitForAsync(_waitForVisible);
        await Map.BlousesSubCategory.ClickAsync();
    }

    public async Task<string?> NavigatedHeader()
    {
        await Map.NavigatedPageHeader.WaitForAsync(_waitForVisible);
        var header = await Map.NavigatedPageHeader.TextContentAsync();
        return header?.Trim();
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
    public ILocator WomenCategory => _page.Locator("//a[@title='Women']");
    public ILocator DressesCategory => _page.Locator("//*[@id=\"block_top_menu\"]/ul/li[2]/a");
    public ILocator TShirtsCategory => _page.Locator("//a[@title='T-shirts']");
    public ILocator CategoryHovered => _page.Locator(".submenu-container.clearfix.first-in-line-xs");
    public ILocator BlousesSubCategory => _page.Locator("//a[@title='Blouses']");
    public ILocator NavigatedPageHeader => _page.Locator(".cat-name");
}