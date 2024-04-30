using AssetManagement.Server.Infrastructure.Extensions;
using System.Runtime.Serialization;

namespace AssetManagement.Server.Infrastructure.Exceptions
{
    [Serializable]
    public class ConflictHelperException : Exception
    {
        public ConflictHelperException(ExceptionEnum exceptionEnumValue, ExceptionErrorCodes errorCode, string propertyValue)
        : base(MethodsExt.ConstructExceptionMessage(exceptionEnumValue, errorCode, propertyValue))
        { }

        public ConflictHelperException(ExceptionEnum exceptionEnumValue, ExceptionErrorCodes errorCode, string data, object messages)
        : base(MethodsExt.ConstructExceptionMessage(exceptionEnumValue, errorCode, data, messages)) { }

        protected ConflictHelperException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public ConflictHelperException() { }
        public ConflictHelperException(string message) : base(message) { }
        public ConflictHelperException(string message, Exception innerException) : base(message, innerException) { }
    }
}
