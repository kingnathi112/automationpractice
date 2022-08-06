using System.Threading.Tasks;
using AutomationPractice.Tests.Helpers;
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
    public AutomationPracticeTests(Browsers browser) : base(browser)
    {
    }
}