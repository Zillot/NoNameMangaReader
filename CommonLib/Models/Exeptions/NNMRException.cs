using System;

namespace CommonLib.Models.Exeptions
{
    public class NNMRException: Exception
    {
        public int? StatusCode { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Detail { get; set; }

        public NNMRException()
        {

        }

        public NNMRException(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public NNMRException(string errorCode, string errorMessage, string detail)
            : this(errorCode, errorMessage)
        {
            Detail = detail;
        }

        public NNMRException(string errorCode, string errorMessage, int statusCode) 
            : this(errorCode, errorMessage)
        {
            StatusCode = statusCode;
        }

        public NNMRException(string errorCode, string errorMessage, string detail, int statusCode)
            : this(errorCode, errorMessage)
        {
            Detail = detail;
            StatusCode = statusCode;
        }
    }
}
