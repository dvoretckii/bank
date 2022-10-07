using System.Runtime.Serialization;

namespace Shops.Exceptions;

public class PriceException : Exception
{
    public PriceException()
    {
    }

    public PriceException(string message)
        : base(message) { }

    public PriceException(string message, Exception inner)
        : base(message, inner) { }

    protected PriceException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}