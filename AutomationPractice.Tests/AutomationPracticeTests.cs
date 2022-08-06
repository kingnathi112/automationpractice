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


    public AutomationPracticeTests(Browsers browser) : base(browser)
    {
    }
}