using System.Runtime.Serialization;

namespace TTP.Domain.Exceptions;
[Serializable]
public abstract class AppException : Exception
{
    protected AppException() { }
    protected AppException(string message) : base(message) { }
    protected AppException(string message, Exception? innerException) : base(message, innerException) { }
    protected AppException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    public abstract int StatusCode { get; }
}