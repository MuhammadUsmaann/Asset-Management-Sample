using AssetManagement.Server.Infrastructure.Extensions;
using System.Runtime.Serialization;

namespace AssetManagement.Server.Infrastructure.Exceptions
{
    [Serializable]
    public class AlreadyExistHelperException : Exception
    {
        public AlreadyExistHelperException(ExceptionEnum texCatalogExceptionEnumType, string dataName)
        : base(MethodsExt.ConstructExceptionMessage(texCatalogExceptionEnumType, ExceptionErrorCodes.ALREADY_EXISTS, dataName))
        { }

        public AlreadyExistHelperException()
        {
        }

        public AlreadyExistHelperException(string message) : base(message)
        {
        }

        public AlreadyExistHelperException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AlreadyExistHelperException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
        }
    }
}
