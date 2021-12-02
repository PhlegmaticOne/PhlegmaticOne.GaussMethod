using System.Text;

namespace PhlegmaticOne.GaussMethod.Clients.TCP;

public class TcpClient : ClientBase
{
    private readonly System.Net.Sockets.TcpClient _tcpClient;
    public TcpClient(string ip, int port) : base(ip, port) => _tcpClient = new System.Net.Sockets.TcpClient();
    public override async Task ConnectAsync() => await _tcpClient.ConnectAsync(IpAddress, Port);
    protected override async Task<string> SendRequestGetRespond(string request)
    {
        await using var networkStream = _tcpClient.GetStream();
        await networkStream.WriteAsync(Encoding.Default.GetBytes(request));
        await using var respondStream = new MemoryStream();
        await networkStream.CopyToAsync(respondStream);
        respondStream.Position = 0;
        return Encoding.UTF8.GetString(respondStream.ToArray()); ;
    }

    public async Task<string> SendMessageAsync(string message) => await SendRequestGetRespond(ToRequest(message));
    public async Task<string> SendEmptyRequestAsync() => await SendRequestGetRespond(string.Empty);
    protected override string ToRequest(string request) => new StringBuilder()
                                                           .AppendLine($"Destination IP: {IpAddress}")
                                                           .AppendLine($"Destination port: {Port}")
                                                           .AppendLine($"Host name: {Hostname}")
                                                           .AppendLine($"Content length: {request.Length}")
                                                           .AppendLine($"Body: [ {request} ]")
                                                           .ToString();
}