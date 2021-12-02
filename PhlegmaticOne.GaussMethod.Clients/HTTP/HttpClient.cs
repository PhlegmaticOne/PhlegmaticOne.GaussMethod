using System.Text;
using Newtonsoft.Json;
using PhlegmaticOne.GaussMethod.Clients.TCP;

namespace PhlegmaticOne.GaussMethod.Clients.HTTP;

public class HttpClient : TcpClient
{
    public HttpClient(string ip, int port) : base(ip, port) { }

    public async Task<string> SendGetRequest()
    {
        var request = new StringBuilder().AppendLine("GET / HTTP/1.1").AppendLine("Accept: text/html, charset=utf-8")
                                         .AppendLine("Accept-Language: en-US").AppendLine("User-Agent: C# program")
                                         .AppendLine("Connection: close").AppendLine(base.ToRequest(string.Empty))
                                         .ToString();
        return await SendRequestGetRespond(request);
    }
    public async Task<string> SendPostRequest(string controller, string action, object data)
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