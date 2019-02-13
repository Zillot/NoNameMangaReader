namespace Common.Model.Exeptions
{
    public class CustomUnhandeledException: CustomException
    {
        public CustomUnhandeledException() : base("0", "unhandled exception", 500)
        {

        }
    }
}
