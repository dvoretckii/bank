using System.Runtime.Serialization;

namespace Shops.Exceptions;

public class ProductAlreadyExists : Exception
{
    public ProductAlreadyExists()
    {
    }

    public ProductAlreadyExists(string message)
        : base(message) { }

    public ProductAlreadyExists(string message, Exception inner)
        : base(message, inner) { }

    protected ProductAlreadyExists(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}