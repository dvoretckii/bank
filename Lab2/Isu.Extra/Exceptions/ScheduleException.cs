using System.Runtime.Serialization;

namespace Isu.Extra.Exceptions;

public class ScheduleException : Exception
{
    public ScheduleException()
    {
    }

    public ScheduleException(string message)
        : base(message) { }

    public ScheduleException(string message, Exception inner)
        : base(message, inner) { }

    protected ScheduleException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}