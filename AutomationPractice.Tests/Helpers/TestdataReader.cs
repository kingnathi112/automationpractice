using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutomationPractice.Tests.Model;
using Newtonsoft.Json;

namespace AutomationPractice.Tests.Helpers;

public class TestdataReader
{
    public static async Task<string> ReadConfig()
    {
        string[] currentPath = Assembly.GetExecutingAssembly().GetManifestResourceNames();
        string config = "testdata.json";
        using (var resourceStream = Assembly.GetExecutingAssembly()
                                        .GetManifestResourceStream(currentPath.FirstOrDefault(x => x.Contains($"{config}"))) ??
                                    throw new ArgumentNullException($"Assembly.GetExecutingAssembly()\n" +
                                                                    $".GetManifestResourceStream(currentPath.FirstOrDefault(x => x.Contains(\"{config}\")))"))
        {
            using (var reader = new StreamReader(resourceStream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
    public static async Task<List<SearchItemModel>> GetSearchItems()
    {
        var config = await ReadConfig();
        var searchItems = JsonConvert.DeserializeObject<SearchItemsModel>(config);
        return searchItems.SearchCriteria;
    }
}