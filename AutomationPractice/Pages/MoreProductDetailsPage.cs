using AutomationPractice.Helpers;
using Microsoft.Playwright;

namespace AutomationPractice.Pages;

public class MoreProductDetailsPage
{
    private IPage _page;
    private MoreProductDetailsPageMap Map;

    private LocatorWaitForOptions _waitForVisible;
    private LocatorWaitForOptions _waitForHidden;
    
    public MoreProductDetailsPage(IPage page)
    {
        _page = page;
        Map = new MoreProductDetailsPageMap(page);
        _waitForVisible = new LocatorWaitForOptions { State = WaitForSelectorState.Visible };
        _waitForHidden = new LocatorWaitForOptions { State = WaitForSelectorState.Hidden };
    }
    
    public async Task<string?> ProductTitle()
    {
        await Map.ProductTitle.WaitForAsync(_waitForVisible);
        return await Map.ProductTitle.TextContentAsync();
    }
    
    public async Task<string?> ProductPriceString()
    {
        await Map.ProductPrice.WaitForAsync(_waitForVisible);
        return await Map.ProductPrice.TextContentAsync();
    }
    
    public async Task<double?> ProductPriceDouble()
    {
        await Map.ProductPrice.WaitForAsync(_waitForVisible);
        var unitPrice =  await Map.ProductPrice.TextContentAsync();
        if (unitPrice != null) return unitPrice.ToPriceDouble();
        return 0.00;
    }
    public async Task<string?> Quantity()
    {
        await Map.RecentlyAddedProductQuantity.WaitForAsync(_waitForVisible);
        return await Map.RecentlyAddedProductQuantity.TextContentAsync();
    }
    
    public async Task SetQuantityWithInput(int quantity)
    {
        await Map.ProductQuantityTxt.WaitForAsync(_waitForVisible);
        await Map.ProductQuantityTxt.FillAsync(quantity.ToString());
    }
    public async Task SetQuantityByIncreaseOrDecrease(int quantity)
    {
        await Map.ProductQuantityTxt.WaitForAsync(_waitForVisible);
        var getQuantity = await Map.ProductQuantityTxt.TextContentAsync();

        var currentQuantity = 0;
        int.TryParse(getQuantity, out currentQuantity);

        if (currentQuantity > quantity)
            await Map.DecreaseQuantityBtn.ClickAsync( new LocatorClickOptions{ClickCount = (currentQuantity - quantity - 1)});
        else if (currentQuantity < quantity)
            await Map.IncreaseQuantityBtn.ClickAsync(new LocatorClickOptions
                { ClickCount = (quantity - 1 - currentQuantity) });
        else return;
    }

    public async Task ClickAddToCart()
    {
        await Map.AddToCartBtn.WaitForAsync(_waitForVisible);
        await Map.AddToCartBtn.ClickAsync();
    }

    public async Task AddItemWithDesiredQuantityToCart(int quantity)
    {
        await SetQuantityByIncreaseOrDecrease(quantity);
        await ClickAddToCart();
    }

    public async Task<bool> IsAddedProductVisible()
    {
        await Map.ProductTitle.WaitForAsync(_waitForVisible);
        return await Map.ProductTitle.IsVisibleAsync();
    }
    
    public async Task<bool> IsRecentlyAddedProductVisible()
    {
        await Map.RecentlyAddedProductTitle.WaitForAsync(_waitForVisible);
        return await Map.RecentlyAddedProductTitle.IsVisibleAsync();
    }    
    public async Task<double?> GetSubtotalPrice()
    {
        await Map.RecentlyAddedSubtotal.WaitForAsync(_waitForVisible);
        var subtotal =  await Map.RecentlyAddedSubtotal.TextContentAsync();
        if (subtotal != null) return subtotal.ToPriceDouble();
        return 0.00;
    }
}

public class MoreProductDetailsPageMap
{
    private IPage _page;
    public MoreProductDetailsPageMap(IPage page)
    {
        _page = page;
    }
    public ILocator ProductTitle => _page.Locator("//h1[@itemprop='name']");
    public ILocator ProductPrice => _page.Locator("#our_price_display");
    public ILocator ProductQuantityTxt => _page.Locator("#quantity_wanted");
    public ILocator DecreaseQuantityBtn => _page.Locator(".btn.btn-default.button-minus.product_quantity_down");
    public ILocator IncreaseQuantityBtn => _page.Locator(".btn.btn-default.button-plus.product_quantity_up");
    public ILocator AddToCartBtn => _page.Locator("//button[@name='Submit']");
    public ILocator RecentlyAddedProductTitle => _page.Locator("#layer_cart_product_title");
    public ILocator RecentlyAddedProductQuantity => _page.Locator("#layer_cart_product_quantity");
    public ILocator RecentlyAddedSubtotal => _page.Locator("#layer_cart_product_price");
}