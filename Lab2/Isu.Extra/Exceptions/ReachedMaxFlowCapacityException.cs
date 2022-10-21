using System.Runtime.Serialization;

namespace Isu.Extra.Exceptions;

public class ReachedMaxFlowCapacityException : Exception
{
    public ReachedMaxFlowCapacityException()
    {
    }

    public ReachedMaxFlowCapacityException(string message)
        : base(message) { }

    public ReachedMaxFlowCapacityException(string message, Exception inner)
        : base(message, inner) { }

    protected ReachedMaxFlowCapacityException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}