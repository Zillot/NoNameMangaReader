using CommonLib.Models.Exeptions;

namespace Auth.Model.Exceptions
{
    public class CredentialsException : NNMRException
    {
        private static readonly string ERROR_CODE = "C.1";
        private static readonly string MESSAGE = "Credentials error";
        private static readonly string DETAIL = "Wrong password of login or both";

        public CredentialsException() : base(ERROR_CODE, MESSAGE, DETAIL)
        {

        }
    }
}
