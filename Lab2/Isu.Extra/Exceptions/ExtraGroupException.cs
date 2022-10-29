using System.Runtime.Serialization;

namespace Isu.Extra.Exceptions;

public class ExtraGroupException : Exception
{
    public ExtraGroupException()
    {
    }

    public ExtraGroupException(string message)
        : base(message) { }

    public ExtraGroupException(string message, Exception inner)
        : base(message, inner) { }

    protected ExtraGroupException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}