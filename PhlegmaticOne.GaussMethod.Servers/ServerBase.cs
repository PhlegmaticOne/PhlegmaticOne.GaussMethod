using System.Net;
using PhlegmaticOne.GaussMethod.Common.Exceptions;

namespace PhlegmaticOne.GaussMethod.Servers;
/// <summary>
/// 
/// </summary>
/// <param name="sender"></param>
/// <param name="request"></param>
/// <returns></returns>
public delegate string? ConfigureResponseHandler(object sender, string request);
/// <summary>
/// Represents base server type
/// </summary>
public abstract class ServerBase
{
    /// <summary>
    /// Port to send response
    /// </summary>
    public int Port { get; }
    /// <summary>
    /// Ip address to send response 
    /// </summary>
    public IPAddress IpAddress { get; }
    /// <summary>
    /// Is server listens to any connections
    /// </summary>
    public bool IsListening { get; set; }
    /// <summary>
    /// Event when request is received
    /// </summary>
    public event ConfigureResponseHandler OnRequestReceived;
    /// <summary>
    /// Initializes new ServerBase instance
    /// </summary>
    /// <param name="ip">specified ip</param>
    /// <param name="port">Specified port</param>
    /// <exception cref="InvalidPortException">Port is incorrect</exception>
    /// <exception cref="InvalidIpAddressException">Ip is incorrect</exception>
    protected ServerBase(string ip, int port)
    {
        Port = port > 0 ? port : throw new InvalidPortException("Port cannot be less or equal to zero", port);
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
            throw new InvalidIpAddressException("Cannot parse input ip.", ip);
        }
    }
    /// <summary>
    /// Starts to listen to connections
    /// </summary>
    /// <returns></returns>
    public abstract Task StartListeningAsync();
    /// <summary>
    /// Stops to listen to connections
    /// </summary>
    public abstract void Stop();
    /// <summary>
    /// Configures responses body specified by protocol
    /// </summary>
    protected abstract string ConfigureResponseBody(string request);
    /// <summary>
    /// Configures responses specified by protocol
    /// </summary>
    protected abstract string ToResponse(string request);
    /// <summary>
    /// Invokes event when request is received
    /// </summary>
    protected virtual string? RequestReceived(string request) => OnRequestReceived?.Invoke(this, request);
    /// <summary>
    /// Final object destroy
    /// </summary>
    ~ServerBase() => Stop();

}