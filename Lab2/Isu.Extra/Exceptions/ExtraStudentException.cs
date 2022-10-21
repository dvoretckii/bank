using System.Runtime.Serialization;

namespace Isu.Extra.Exceptions;

public class ExtraStudentException : Exception
{
    public ExtraStudentException()
    {
    }

    public ExtraStudentException(string message)
        : base(message) { }

    public ExtraStudentException(string message, Exception inner)
        : base(message, inner) { }

    protected ExtraStudentException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}