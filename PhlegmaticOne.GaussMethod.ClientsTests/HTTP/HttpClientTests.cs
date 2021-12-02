using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PhlegmaticOne.GaussMethod.Servers.Extensions;

namespace PhlegmaticOne.GaussMethod.Clients.HTTP.Tests;

[TestClass()]
public class HttpClientTests
{
    private const string TESTED_IP = "localhost";
    private const int TESTED_PORT = 8080;
    private static readonly double[,] TESTED_MATRIX =
    {
        {2, 1, -1, 8},
        {-3, -1, 2, -11},
        {-2, 1, 2, -3}
    };

    [TestMethod()]
    public async Task HttpClientTest()
    {
        var httpClient = new HttpClient(TESTED_IP, TESTED_PORT)
        {
            Hostname = TESTED_IP
        };
        await httpClient.ConnectAsync();
        var respond = await httpClient.SendPostRequest("Gauss", "Solve", TESTED_MATRIX);
        var s = respond.Length;
        var firstIndex = respond.IndexOf('[', respond.IndexOf('[') + 1);
        var secondIndex = respond.LastIndexOf(']');
        var str = respond.Substring(firstIndex, secondIndex - firstIndex);
        var gaussSolving = JsonConvert.DeserializeObject<double[]>(str);
        Assert.AreEqual(2, gaussSolving[0]);
        Assert.AreEqual(3, gaussSolving[1]);
        Assert.AreEqual(-1, gaussSolving[2]);
    }
}