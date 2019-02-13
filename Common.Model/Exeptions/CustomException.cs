using System;

namespace Common.Model.Exeptions
{
    public class CustomException: Exception
    {
        public int? StatusCode { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Detail { get; set; }

        public CustomException()
        {

        }

        public CustomException(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Detail = $"{ErrorCode} {ErrorMessage}";
        }

        public CustomException(string errorCode, string errorMessage, string detail)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Detail = detail;
        }

        public CustomException(string errorCode, string errorMessage, int statusCode) 
            : this(errorCode, errorMessage)
        {
            StatusCode = statusCode;
        }
    }
}
