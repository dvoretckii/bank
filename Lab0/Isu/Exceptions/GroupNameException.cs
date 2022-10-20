using System.Runtime.Serialization;

namespace Isu.Exceptions;

public class GroupNameException : Exception
{
    public GroupNameException()
    {
    }

    public GroupNameException(string message)
        : base(message) { }

    public GroupNameException(string message, Exception inner)
        : base(message, inner) { }

    protected GroupNameException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}