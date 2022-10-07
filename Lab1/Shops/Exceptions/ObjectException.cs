using System.Runtime.Serialization;

namespace Shops.Exceptions;

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