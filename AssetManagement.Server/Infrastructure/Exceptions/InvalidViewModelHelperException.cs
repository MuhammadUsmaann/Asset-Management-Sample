using System.Runtime.Serialization;

namespace AssetManagement.Server.Infrastructure.Exceptions
{
    [Serializable]
    public class InvalidViewModelHelperException : Exception
    {
        public InvalidViewModelHelperException(string message)
        : base(message)
        {
        }

        public InvalidViewModelHelperException()
        {
        }

        public InvalidViewModelHelperException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidViewModelHelperException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
        }
    }
}
