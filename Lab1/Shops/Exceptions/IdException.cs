using System.Runtime.Serialization;

namespace Shops.Exceptions;

public class IdException : Exception
{
    public IdException()
    {
    }

    public IdException(string message)
        : base(message) { }

    public IdException(string message, Exception inner)
        : base(message, inner) { }

    protected IdException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}