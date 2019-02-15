namespace CommonLib.Models.Exeptions
{
    public class OnlyPrivateUsageException : NNMRException
    {
        private static readonly string ERROR_CODE = "1";
        private static readonly string MESSAGE = "Forbiden";
        private static readonly string DETAIL = "For inner use only";
        private static readonly int HTTP_CODE = 403;

        public OnlyPrivateUsageException() : base(ERROR_CODE, MESSAGE, DETAIL, HTTP_CODE)
        {

        }
    }
}
