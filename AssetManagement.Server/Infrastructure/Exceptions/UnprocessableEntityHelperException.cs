using System.Runtime.Serialization;
using AssetManagement.Server.Infrastructure.Extensions;

namespace AssetManagement.Server.Infrastructure.Exceptions
{
    [Serializable]
    public class UnprocessableEntityHelperException : Exception
    {
        public UnprocessableEntityHelperException(ExceptionEnum exceptionEnumValue, string propertyValue, object messages)
        : base(MethodsExt.ConstructExceptionMessage(exceptionEnumValue, ExceptionErrorCodes.UNPROCESSABLE_ENTITY, propertyValue, messages: messages))
        { }

        public UnprocessableEntityHelperException(ExceptionEnum entity, ExceptionErrorCodes errorCode, string entityName, object messages)
        : base(MethodsExt.ConstructExceptionMessage(entity, errorCode, entityName, messages))
        { }

        protected UnprocessableEntityHelperException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }

        public UnprocessableEntityHelperException()
        {
        }

        public UnprocessableEntityHelperException(string message) : base(message)
        {
        }

        public UnprocessableEntityHelperException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
