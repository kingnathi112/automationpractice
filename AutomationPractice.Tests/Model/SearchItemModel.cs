using System.Collections.Generic;
using Newtonsoft.Json;

namespace AutomationPractice.Tests.Model;

public class SearchItemsModel
{
    [JsonProperty("SearchCriteria")]
    public List<SearchItemModel> SearchCriteria { get; set; }
}
public class SearchItemModel
{
    [JsonProperty("itemName")]
    public string ItemName { get; set; }
}