namespace Shops.Exceptions;
using System.Runtime.Serialization;

public class ProductPackDoesNotExist : Exception
{
    public ProductPackDoesNotExist()
    {
    }

    public ProductPackDoesNotExist(string message)
        : base(message) { }

    public ProductPackDoesNotExist(string message, Exception inner)
        : base(message, inner) { }

    protected ProductPackDoesNotExist(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}