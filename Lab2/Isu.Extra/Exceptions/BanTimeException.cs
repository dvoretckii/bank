using System.Runtime.Serialization;

namespace Isu.Extra.Exceptions;

public class BanTimeException : Exception
{
    public BanTimeException()
    {
    }

    public BanTimeException(string message)
        : base(message) { }

    public BanTimeException(string message, Exception inner)
        : base(message, inner) { }

    protected BanTimeException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}