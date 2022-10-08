namespace Shops.Exceptions;
using System.Runtime.Serialization;

public class ProductDoesNotExist : Exception
{
    public ProductDoesNotExist()
    {
    }

    public ProductDoesNotExist(string message)
        : base(message) { }

    public ProductDoesNotExist(string message, Exception inner)
        : base(message, inner) { }

    protected ProductDoesNotExist(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}