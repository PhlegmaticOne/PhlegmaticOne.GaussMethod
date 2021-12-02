using Newtonsoft.Json;
using PhlegmaticOne.GaussMethod.Clients.TCP;
using System.Text;

namespace PhlegmaticOne.GaussMethod.Clients.HTTP;
/// <summary>
/// Represents http client
/// </summary>
public class HttpClient : TcpClient
{
    /// <summary>
    /// Initializes new HttpClient instance
    /// </summary>
    /// <param name="ip">Specified ip</param>
    /// <param name="port">Specified port</param>
    public HttpClient(string ip, int port) : base(ip, port) { }
    /// <summary>
    /// Sends get http request to server asynchronously
    /// </summary>
    /// <returns>Server get response</returns>
    public async Task<object> SendGetRequest()
    {
        var request = new StringBuilder().AppendLine("GET / HTTP/1.1").AppendLine("Accept: text/html, charset=utf-8")
                                         .AppendLine("Accept-Language: en-US").AppendLine("User-Agent: C# program")
                                         .AppendLine("Connection: close").AppendLine(base.ToRequest(string.Empty))
                                         .ToString();
        return await SendRequestGetRespond(request);
    }
    /// <summary>
    /// Sends post http request to server asynchronously
    /// </summary>
    /// <param name="controller">Controller name</param>
    /// <param name="action">Action name</param>
    /// <param name="data">Data for action</param>
    /// <returns>Server post respond</returns>
    public async Task<object> SendPostRequest(string controller, string action, object data)
    {
        var dataJson = JsonConvert.SerializeObject(data);
        var body = $"Controller: {controller}\nAction: {action}\nData: {dataJson}\n";
        var request = new StringBuilder().AppendLine("POST / HTTP/1.1").AppendLine("Accept: text/html, charset=utf-8")
                                         .AppendLine("Accept-Language: en-US").AppendLine("User-Agent: C# program")
                                         .AppendLine("Connection: close").AppendLine(base.ToRequest(body))
                                         .ToString();
        return await SendRequestGetRespond(request);
    }
}