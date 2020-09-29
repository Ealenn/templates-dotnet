using Opw.HttpExceptions;
using System;
using System.Net;

namespace Api.Dotnet.Template.Exceptions
{
    public abstract class ApiException : HttpExceptionBase
    {
        public sealed override HttpStatusCode StatusCode { get; protected set; }
        public string ErrorCode { get; }

        protected ApiException(HttpStatusCode statusCode, string errorCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }

        protected ApiException(HttpStatusCode statusCode, string errorCode, string message, Exception exception)
            : base(message, exception)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }
    }
}
