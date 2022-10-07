using System.Runtime.Serialization;

namespace Shops.Exceptions;

public class BudgetException : Exception
{
    public BudgetException()
    {
    }

    public BudgetException(string message)
        : base(message) { }

    public BudgetException(string message, Exception inner)
        : base(message, inner) { }

    protected BudgetException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}