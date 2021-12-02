using System.Net;

namespace PhlegmaticOne.GaussMethod.Clients;

public abstract class ClientBase
{
    public int Port { get; init; }
    public IPAddress IpAddress { get; init; }
    public string Hostname { get; init; }
    protected ClientBase(string ip, int port)
    {
        Port = port > 0 ? port : throw new ArgumentException();
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
    public abstract Task ConnectAsync();
    protected abstract Task<string> SendRequestGetRespond(string request);
    protected abstract string ToRequest(string request);
}