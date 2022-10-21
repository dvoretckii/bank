using System.Runtime.Serialization;

namespace Isu.Extra.Exceptions;

public class LessonTimeException : Exception
{
    public LessonTimeException()
    {
    }

    public LessonTimeException(string message)
        : base(message) { }

    public LessonTimeException(string message, Exception inner)
        : base(message, inner) { }

    protected LessonTimeException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}