namespace PhlegmaticOne.GaussMethod.Servers.Responds;

/// <summary>
/// Default http responses
/// </summary>
public static class DefaultHttpResponses
{
    /// <summary>
    /// Default ok http response
    /// </summary>
    public static string OK => $"HTTP/1.1 200 OK\nServer: local / 1.6.2\nDate: {DateTime.Now}\nContent-Type: text/html\nContent-Length: 0\nConnection: close\n[ ]";
    /// <summary>
    /// Default http response with error
    /// </summary>
    public static string Error => $"HTTP/1.1 400 Error\nServer: local / 1.6.2\nDate: {DateTime.Now}\nContent-Type: text/html\nContent-Length: 0\nConnection: close\n[ ]";
}