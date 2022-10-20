using System.Runtime.Serialization;

namespace Isu.Exceptions;

public class CourseNumberException : Exception
{
    public CourseNumberException()
    {
    }

    public CourseNumberException(string message)
        : base(message) { }

    public CourseNumberException(string message, Exception inner)
        : base(message, inner) { }

    protected CourseNumberException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}