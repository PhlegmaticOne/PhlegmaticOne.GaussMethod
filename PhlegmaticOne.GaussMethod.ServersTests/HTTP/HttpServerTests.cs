using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using PhlegmaticOne.GaussMethod.Servers.Responds;

namespace PhlegmaticOne.GaussMethod.Servers.HTTP.Tests;

[TestClass()]
public class HttpServerTests
{
    private const string TESTED_IP = "localhost";
    private const int TESTED_PORT = 8080;
    [TestMethod()]
    public async Task HttpServerTest()
    {
        var server = new HttpServer(TESTED_IP, TESTED_PORT);
        await server.StartListeningAsync();
    }
    [TestMethod()]
    public async Task HttpServerTestWithEvent()
    {
        var server = new HttpServer(TESTED_IP, TESTED_PORT);
        server.OnRequestReceived += (object sender, string request) => DefaultHttpResponses.OK;
        await server.StartListeningAsync();
    }
}