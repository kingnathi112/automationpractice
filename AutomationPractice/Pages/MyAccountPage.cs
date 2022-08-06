using Microsoft.Playwright;

namespace AutomationPractice.Pages;

public class MyAccountPage
{
    private MyAccountPageMap Map;
    private IPage _page;

    private LocatorWaitForOptions _waitForVisible;
    private LocatorWaitForOptions _waitForHidden;
    
    public MyAccountPage(IPage page)
    {
        _page = page;
        Map = new MyAccountPageMap(page);
        _waitForVisible = new LocatorWaitForOptions { State = WaitForSelectorState.Visible };
        _waitForHidden = new LocatorWaitForOptions { State = WaitForSelectorState.Hidden };
    }

    public async Task<bool?> IsAuthenticationHeaderVisible()
    {
        await Map.MyAccountHeader.WaitForAsync(_waitForVisible);
        return await Map.MyAccountHeader.IsVisibleAsync();
    }

    public async Task<string?> IsCustomerNameVisible()
    {
        await Map.CustomerNameLink.WaitForAsync(_waitForVisible);
        return await Map.CustomerNameLink.TextContentAsync();
    }
    public async Task SignInUsingEmailAndPassword(string email, string password)
    {
        await Map.EmailInput.WaitForAsync(_waitForVisible);
        await Map.EmailInput.FillAsync(email);
        
        await Map.PasswordInput.WaitForAsync(_waitForVisible);
        await Map.PasswordInput.FillAsync(password);

        await Map.SignInBtn.WaitForAsync(_waitForVisible);
        await Map.SignInBtn.ClickAsync();
    }
    
}
public class MyAccountPageMap
{
    private IPage _page;

    public MyAccountPageMap(IPage page)
    {
        _page = page;
    }

    public ILocator MyAccountHeader => _page.Locator(".page-heading");
    public ILocator EmailInput => _page.Locator("#email");
    public ILocator PasswordInput => _page.Locator("#passwd");
    public ILocator SignInBtn => _page.Locator("#SubmitLogin");
    public ILocator CustomerNameLink => _page.Locator(".account");
}