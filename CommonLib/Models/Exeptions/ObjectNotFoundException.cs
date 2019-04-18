namespace CommonLib.Models.Exeptions
{
    public class ObjectNotFoundException: NNMRException
    {
        private static readonly string ERROR_CODE = "ONF:1";
        private static readonly string MESSAGE = "Object not found";
        private static readonly string DETAIL = "";
        private static readonly int HTTP_CODE = 500;

        public ObjectNotFoundException() : base(ERROR_CODE, MESSAGE, DETAIL, HTTP_CODE)
        {

        }
    }
}
