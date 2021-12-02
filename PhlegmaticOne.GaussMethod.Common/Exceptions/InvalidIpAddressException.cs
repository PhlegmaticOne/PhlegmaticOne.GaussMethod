namespace PhlegmaticOne.GaussMethod.Common.Exceptions;

/// <summary>
/// Represents custom exception when ip has wrong format
/// </summary>
public class InvalidIpAddressException : Exception
{
    private readonly string _ipAddress;
    private readonly string _message;
    public InvalidIpAddressException(string message, string ipAddress)
    {
        _ipAddress = ipAddress;
        _message = message;
    }
    public override string Message => $"{_message}. Invalid ip was: {_ipAddress}";
}
