using System.Text;

namespace PhlegmaticOne.GaussMethod.Clients.TCP;
/// <summary>
/// Represents  tcp client
/// </summary>
public class TcpClient : ClientBase
{
    private readonly System.Net.Sockets.TcpClient _tcpClient;
    /// <summary>
    /// Initializes new TcpClient instance
    /// </summary>
    /// <param name="ip">Specified ip</param>
    /// <param name="port">Specified port</param>
    public TcpClient(string ip, int port) : base(ip, port) => _tcpClient = new System.Net.Sockets.TcpClient();
    public override async Task ConnectAsync() => await _tcpClient.ConnectAsync(IpAddress, Port);
    protected override async Task<object> SendRequestGetRespond(string request)
    {
        await using var networkStream = _tcpClient.GetStream();
        await networkStream.WriteAsync(Encoding.Default.GetBytes(request));
        await using var respondStream = new MemoryStream();
        await networkStream.CopyToAsync(respondStream);
        respondStream.Position = 0;
        var respond = Encoding.UTF8.GetString(respondStream.ToArray());
        return RespondReceived(respond) ?? respond;
    }
    /// <summary>
    /// Sends message to server
    /// </summary>
    /// <param name="message"></param>
    /// <returns>Server respond</returns>
    public async Task<object> SendMessageAsync(string message) => await SendRequestGetRespond(ToRequest(message));
    /// <summary>
    /// Sends empty request tot server
    /// </summary>
    /// <returns>Server respond</returns>
    public async Task<object> SendEmptyRequestAsync() => await SendRequestGetRespond(string.Empty);
    protected override string ToRequest(string request) => new StringBuilder()
                                                           .AppendLine($"Destination IP: {IpAddress}")
                                                           .AppendLine($"Destination port: {Port}")
                                                           .AppendLine($"Host name: {Hostname}")
                                                           .AppendLine($"Content length: {request.Length}")
                                                           .AppendLine($"Body: [ {request} ]")
                                                           .ToString();
}