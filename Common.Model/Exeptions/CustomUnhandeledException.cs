namespace Common.Model.Exeptions
{
    public class UnhandeledException: CustomException
    {
        private static readonly string ERROR_CODE = "0";
        private static readonly string MESSAGE = "unhandled exception";
        private static readonly string DETAIL = "";
        private static readonly int HTTP_CODE = 500;

        public UnhandeledException() : base(ERROR_CODE, MESSAGE, DETAIL, HTTP_CODE)
        {

        }
    }
}
