using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
using AssetManagement.Server.Infrastructure.Extensions;

namespace AssetManagement.Server.Infrastructure.Exceptions
{
    [Serializable]
    public class NotModifiedHelperException : Exception
    {
        public NotModifiedHelperException(ExceptionEnum texCatalogExceptionEnumValue, string propertyValue)
        : base(MethodsExt.ConstructExceptionMessage(texCatalogExceptionEnumValue, ExceptionErrorCodes.NOT_MODIFIED, propertyValue))
        { }

        protected NotModifiedHelperException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }

        public NotModifiedHelperException()
        {
        }

        public NotModifiedHelperException(string message) : base(message)
        {
        }

        public NotModifiedHelperException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
