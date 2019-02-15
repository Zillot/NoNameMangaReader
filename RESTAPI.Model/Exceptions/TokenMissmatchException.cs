using CommonLib.Models.Exeptions;

namespace RESTAPI.Model.Exceptions
{
    public class TokenMissmatchException: NNMRException
    {
        private static readonly string ERROR_CODE = "TM.1";
        private static readonly string MESSAGE = "Token missmatch";
        private static readonly string DETAIL = "You token dont have nessary claim, get new token to resolve problem";
        private static readonly int HTTP_CODE = 405;

        public TokenMissmatchException() : base(ERROR_CODE, MESSAGE, DETAIL, HTTP_CODE)
        {

        }
    }
}
