using System.Runtime.Serialization;

namespace Isu.Exceptions;

public class StudentException : Exception
{
    public StudentException()
    {
    }

    public StudentException(string message)
        : base(message) { }

    public StudentException(string message, Exception inner)
        : base(message, inner) { }

    protected StudentException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}