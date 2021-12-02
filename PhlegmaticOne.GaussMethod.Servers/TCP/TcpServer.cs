using System.Net.Sockets;
using System.Text;

namespace PhlegmaticOne.GaussMethod.Servers.TCP;

public class TcpServer : ServerBase
{
    private readonly TcpListener _listener;
    private readonly CancellationTokenSource _cancellationTokenSource;

    public TcpServer(string ip, int port) : base(ip, port) =>
        (_listener, _cancellationTokenSource) = (new TcpListener(IpAddress, port), new());

    public override async Task StartListeningAsync()
    {
        _listener.Start();
        try
        {
            while (IsListening)
            {
                var client = await Task.Run(() => _listener.AcceptTcpClientAsync(), _cancellationTokenSource.Token);
                await using var stream = client.GetStream();
                var requestBuilder = new StringBuilder();
                var buffer = new byte[1024];
                do
                {
                    await stream.ReadAsync(buffer, 0, buffer.Length);
                    requestBuilder.Append(Encoding.UTF8.GetString(buffer));
                } while (stream.DataAvailable);

                var request = requestBuilder.ToString().Trim();
                var response = RequestReceived(request) ?? ToResponse(request);
                await stream.WriteAsync(Encoding.UTF8.GetBytes(response));
            }
        }
        catch (Exception serverStopRequest)
        {
            IsListening = false;
            _listener.Stop();
        }
    }

    public override void Stop() => _cancellationTokenSource.Cancel();

    protected override string ConfigureResponseBody(string request)
    {
        if (request.Contains("HTTP"))
        {
            return "Cannot process http request with TCP server";
        }

        var body = request.Substring(request.IndexOf('['), request.LastIndexOf(']'));
        var responseBody = $"Received message: {body}. Answer: Request accepted";
        return $"Received message: {body}. Answer: Request accepted";
    }

    protected override string ToResponse(string request)
    {
        string body = ConfigureResponseBody(request);
        return new StringBuilder().AppendLine($"Destination IP: {IpAddress}").AppendLine($"Destination port: {Port}")
                                  .AppendLine($"Content-Length: {body.Length}").AppendLine($"[ {body} ]").ToString();
    }

    ~TcpServer() => Stop();
}