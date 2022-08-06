using System.Threading.Tasks;
using NUnit.Framework;

namespace AutomationPractice.Tests;

[TestFixture(Browsers.Chrome)]
public class AutomationPracticeTests : BaseTest
{
    [Test]
    public async Task Search_SingleSearch_VerifySearchedItemIsInTheSearchResults()
    {
        await _homePage.Search("Blouse");
        Assert.IsTrue(await _homePage.IsSearchedItemShown("Blouse"));
    }

    [Test]
    public async Task Search_MultipleSearch_VerifySearchedItemIsInTheSearchResults()
    {
        string searchItem = $"Faded Short Sleeve T-shirts,Blouse,Printed Chiffon Dress";
        var searchProducts= searchItem.Split(',');

        foreach (var searchProduct in searchProducts)
        {
            await _homePage.Search(searchProduct);
            Assert.IsTrue(await _homePage.IsSearchedItemShown(searchProduct));
        }
    }
    public AutomationPracticeTests(Browsers browser) : base(browser)
    {
    }
}