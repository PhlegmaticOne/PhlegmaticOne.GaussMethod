using Newtonsoft.Json;
using PhlegmaticOne.GaussMethod.Servers.Extensions;
using PhlegmaticOne.GaussMethod.Servers.TCP;
using System.Reflection;

namespace PhlegmaticOne.GaussMethod.Servers.HTTP;
/// <summary>
/// Http server implementation
/// </summary>
public class HttpServer : TcpServer
{
    /// <summary>
    /// Initializes new HttpServer instance
    /// </summary>
    /// <param name="ip">Specified ip</param>
    /// <param name="port">Specified port</param>
    public HttpServer(string ip, int port) : base(ip, port) { }
    protected override string ToResponse(string request)
    {
        string body, responseType;
        try
        {
            body = ConfigureResponseBody(request);
            responseType = "HTTP/1.1 200 OK";
        }
        catch (Exception ex)
        {
            body = ex.Message;
            responseType = "HTTP/1.1 400 Error";
        }
        return $"{responseType}\nServer: local / 1.6.2\nDate: {DateTime.Now}\nContent-Type: text/html\nContent-Length: {body.Length}\nConnection: close\n[ {body} ]";
    }
    protected override string ConfigureResponseBody(string request)
    {
        if (request.Contains("GET"))
        {
            return "Request was processed";
        }

        if (request.Contains("POST"))
        {
            var controller = request.Between("Controller: ", "\n");
            var action = request.Between("Action: ", "\n");
            var data = request.Between("Data: ", "\n");
            var typeInfo = Assembly.GetExecutingAssembly().DefinedTypes.First(type => type.Name.Contains(controller));
            var typeInstance = Activator.CreateInstance(typeInfo);
            var methodInfo = typeInfo.DeclaredMethods.First(method => method.Name.Contains(action));
            var respondData = methodInfo.Invoke(typeInstance, new[] { data });
            return JsonConvert.SerializeObject(respondData);
        }

        return "Unknown request";
    }
}