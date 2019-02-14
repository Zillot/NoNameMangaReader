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
        }

        public CustomException(string errorCode, string errorMessage, string detail)
            : this(errorCode, errorMessage)
        {
            Detail = detail;
        }

        public CustomException(string errorCode, string errorMessage, int statusCode) 
            : this(errorCode, errorMessage)
        {
            StatusCode = statusCode;
        }

        public CustomException(string errorCode, string errorMessage, string detail, int statusCode)
            : this(errorCode, errorMessage)
        {
            Detail = detail;
            StatusCode = statusCode;
        }
    }
}
