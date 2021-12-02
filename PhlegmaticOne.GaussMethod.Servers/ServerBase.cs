using System.Net;

namespace PhlegmaticOne.GaussMethod.Servers;

public delegate string? ConfigureResponseHandler(object sender, string request);
public abstract class ServerBase
{
    public int Port { get; }
    public IPAddress IpAddress { get; }
    public bool IsListening { get; set; }
    public event ConfigureResponseHandler OnRequestReceived;
    protected ServerBase(string ip, int port)
    {
        Port = port > 0 ? port : throw new ArgumentException();
        IsListening = true;
        if (IPAddress.TryParse(ip, out var ipAddress))
        {
            IpAddress = ipAddress;
        }
        else if (ip == "localhost")
        {
            IpAddress = Dns.GetHostAddresses(ip).First();
        }
        else
        {
            throw new ArgumentException();
        }
    }
    public abstract Task StartListeningAsync();
    public abstract void Stop();
    protected abstract string ConfigureResponseBody(string request);
    protected abstract string ToResponse(string request);
    protected virtual string? RequestReceived(string request) => OnRequestReceived?.Invoke(this, request);

}