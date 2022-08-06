using Microsoft.Playwright;

namespace AutomationPractice;

    public static class DriverFactory
    {
        public async static Task<IPage> Build(Browsers browser, bool isHeadless = false)
        {
            switch (browser)
            {
                case Browsers.Chrome:
                {
                    var _playwright = await Playwright.CreateAsync();
                    var browserOption = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                    {
                        Headless = isHeadless,
                        Channel = "chrome"
                    });
                    var context = await browserOption.NewContextAsync( new BrowserNewContextOptions
                    {
                        ViewportSize = new ViewportSize
                        {
                            Width = 1080,
                            Height = 720
                        }
                    });
                    return await context.NewPageAsync();
                    break;
                }
                case Browsers.Firefox:
                {
                    var _playwright = await Playwright.CreateAsync();
                    var browserOption = await _playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
                    {
                        Headless = isHeadless
                    });
                    var context = await browserOption.NewContextAsync( new BrowserNewContextOptions
                    {
                        ViewportSize = new ViewportSize
                        {
                            Width = 1080,
                            Height = 720
                        }
                    });
                    return await context.NewPageAsync();
                    break;
                }
                case Browsers.Webkit:
                {
                    var _playwright = await Playwright.CreateAsync();
                    var browserOption = await _playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions
                    {
                        Headless = isHeadless
                    });
                    var context = await browserOption.NewContextAsync( new BrowserNewContextOptions
                    {
                        ViewportSize = new ViewportSize
                        {
                            Width = 1080,
                            Height = 720
                        }
                    });
                    return await context.NewPageAsync();
                    break;
                }
                case Browsers.Edge:
                {
                    var _playwright = await Playwright.CreateAsync();
                    var browserOption = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                    {
                        Headless = isHeadless,
                        Channel = "msedge"
                    });
                    var context = await browserOption.NewContextAsync( new BrowserNewContextOptions
                    {
                        ViewportSize = new ViewportSize
                        {
                            Width = 1080,
                            Height = 720
                        }
                    });
                    return await context.NewPageAsync();
                    break;
                }
                default:
                    return null;
            }

        }

    }