using AssetManagement.Server.Infrastructure.Extensions;
using System.Runtime.Serialization;

namespace AssetManagement.Server.Infrastructure.Exceptions
{
    [Serializable]
    public class BadRequestHelperException : Exception
    {
        public BadRequestHelperException(ExceptionEnum texCatalogExceptionEnumValue, string propertyValue)
        : base(MethodsExt.ConstructExceptionMessage(texCatalogExceptionEnumValue, ExceptionErrorCodes.BAD_REQUEST, propertyValue))
        { }

        public BadRequestHelperException(ExceptionEnum entity, ExceptionErrorCodes errorCode, object messages)
        : base(MethodsExt.ConstructExceptionMessage(entity, errorCode, messages: messages))
        { }

        protected BadRequestHelperException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        public BadRequestHelperException()
        {
        }

        public BadRequestHelperException(string message) : base(message)
        {
        }

        public BadRequestHelperException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
