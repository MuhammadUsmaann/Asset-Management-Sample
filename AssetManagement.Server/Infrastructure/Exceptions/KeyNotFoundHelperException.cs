using AssetManagement.Server.Infrastructure.Extensions;
using System.Runtime.Serialization;

namespace AssetManagement.Server.Infrastructure.Exceptions
{
    [Serializable]
    public class KeyNotFoundHelperException : Exception
    {
        public KeyNotFoundHelperException(ExceptionEnum texCatalogExceptionEnumValue, string propertyValue)
       : base(MethodsExt.ConstructExceptionMessage(texCatalogExceptionEnumValue, ExceptionErrorCodes.NOT_FOUND, propertyValue))
        { }

        public KeyNotFoundHelperException()
        {
        }

        public KeyNotFoundHelperException(string message) : base(message)
        {
        }

        public KeyNotFoundHelperException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected KeyNotFoundHelperException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
        }
    }
}
