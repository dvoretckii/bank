using System.Runtime.Serialization;

namespace Shops.Exceptions;

public class CountException : Exception
{
    public CountException()
    {
    }

    public CountException(string message)
        : base(message) { }

    public CountException(string message, Exception inner)
        : base(message, inner) { }

    protected CountException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}