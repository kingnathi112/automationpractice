using System.Threading.Tasks;
using AutomationPractice.Tests.Helpers;
using AutomationPractice.Tests.Testdata;
using NUnit.Framework;

namespace AutomationPractice.Tests;

[TestFixture(Browsers.Chrome)]
public class AutomationPracticeTests : BaseTest
{
    [Test]
    public async Task Search_SingleSearch_VerifyIfResultsHasProduct()
    {
        await _homePage.Search("Blouse");
        Assert.IsTrue(await _homePage.IsSearchedItemShown("Blouse"));
    }

    [Test]
    public async Task Search_MultipleSearch_VerifyIfResultsHasProduct()
    {
        string searchItem = $"Faded Short Sleeve T-shirts,Blouse,Printed Chiffon Dress";
        var searchProducts= searchItem.Split(',');

        foreach (var searchProduct in searchProducts)
        {
            await _homePage.Search(searchProduct);
            Assert.IsTrue(await _homePage.IsSearchedItemShown(searchProduct));
        }
    }
    
    [Test]
    public async Task Search_Multiple_UsingTestdata_VerifyIfResultsHasProduct()
    {
        var searchItems = await TestdataReader.GetSearchItems();

        foreach (var search in searchItems)
        {
            await _homePage.Search(search.ItemName);
            Assert.IsTrue(await _homePage.IsSearchedItemShown(search.ItemName));
        }
    }
    
    [Test]
    public async Task SignIn_LoginWithValidCredentials()
    {
        await _homePage.ClickSignIn();
        Assert.IsTrue(await _accountPage.IsAuthenticationHeaderVisible());
        await _accountPage.SignInUsingEmailAndPassword(UserDetails.Username, UserDetails.Password);
        Assert.AreEqual(UserDetails.FullName,await _accountPage.IsCustomerNameVisible());
    }

    public AutomationPracticeTests(Browsers browser) : base(browser)
    {
    }
}