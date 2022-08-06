using System.Threading.Tasks;
using AutomationPractice.Pages;
using Microsoft.Playwright;
using NUnit.Framework;

namespace AutomationPractice.Tests;

[TestFixture]
public abstract class BaseTest
{
    protected IPage _driver;
    Browsers _browser;
    protected HomePage _homePage;
    
    protected BaseTest(Browsers browser)
    {
        _browser = browser;
    }
    
    [OneTimeSetUp]
    public async Task InitializeTest()
    {
        _driver = await DriverFactory.Build(_browser);
        _homePage = new HomePage(_driver);
    }
    
    [SetUp]
    public async Task Init()
    {
        await _homePage.GoHome();
    }

    [OneTimeTearDown]
    public async Task CleanUpTest()
    {
        await _driver.CloseAsync();
    }

}