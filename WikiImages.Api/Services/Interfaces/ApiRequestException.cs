using System;
using System.Runtime.Serialization;

namespace WikiImages.Api.Services.Interfaces
{
    public sealed class ApiRequestException : Exception
    {
        public ApiRequestException()
        {
        }

        public ApiRequestException(string message) : base(message)
        {
        }

        public ApiRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        private ApiRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
