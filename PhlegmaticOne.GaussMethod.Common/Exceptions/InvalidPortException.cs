namespace PhlegmaticOne.GaussMethod.Common.Exceptions;

// <summary>
/// Represents custom exception when port has wrong format
/// </summary>
public class InvalidPortException : Exception
{
    private readonly int _port;
    private readonly string _message;

    public InvalidPortException(string message, int port)
    {
        _port = port;
        _message = message;
    }

    public override string Message => $"{_message}. Invalid port was: {_port}";
}

