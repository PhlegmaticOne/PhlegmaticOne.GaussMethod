using PhlegmaticOne.GaussMethod.Common.Exceptions;
using System.Net;

namespace PhlegmaticOne.GaussMethod.Clients;

/// <summary>
/// Delegate for custom event when respond from server is returned
/// </summary>
/// <param name="sender">Keeper of event</param>
/// <param name="response">Response in string format</param>
/// <returns>Any object that can be done from response data</returns>
public delegate object? ConfigureRespondedData(object sender, string response);
/// <summary>
/// Represents base class for clients
/// </summary>
public abstract class ClientBase
{
    /// <summary>
    /// Port to connect
    /// </summary>
    public int Port { get; init; }
    /// <summary>
    /// Ip address to connect
    /// </summary>
    public IPAddress IpAddress { get; init; }
    /// <summary>
    /// Host name of server
    /// </summary>
    public string Hostname { get; init; }
    /// <summary>
    /// Event when respond is returned
    /// </summary>
    public event ConfigureRespondedData OnRespondReceived;

    protected ClientBase(string ip, int port)
    {
        Port = port > 0 ? port : throw new InvalidPortException("Port cannot be less or equal to zero", port);
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
    /// Connects to server asynchronously
    /// </summary>
    public abstract Task ConnectAsync();
    /// <summary>
    /// Sends request and gets response
    /// </summary>
    protected abstract Task<object> SendRequestGetRespond(string request);
    /// <summary>
    /// Parsing user data to request into a specified request type
    /// </summary>
    protected abstract string ToRequest(string request);
    /// <summary>
    /// Invokes event when respond is returned
    /// </summary>
    /// <param name="respond"></param>
    /// <returns></returns>
    protected virtual object? RespondReceived(string respond) => OnRespondReceived?.Invoke(this, respond);
}