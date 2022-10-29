using System.Runtime.Serialization;

namespace Isu.Extra.Exceptions;

public class AdditionalSubjectException : Exception
{
    public AdditionalSubjectException()
    {
    }

    public AdditionalSubjectException(string message)
        : base(message) { }

    public AdditionalSubjectException(string message, Exception inner)
        : base(message, inner) { }

    protected AdditionalSubjectException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}