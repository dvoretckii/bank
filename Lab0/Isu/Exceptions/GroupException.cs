using System.Runtime.Serialization;

namespace Isu.Exceptions;

public class GroupException : Exception
{
    public GroupException()
    {
    }

    public GroupException(string message)
        : base(message) { }

    public GroupException(string message, Exception inner)
        : base(message, inner) { }

    protected GroupException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}