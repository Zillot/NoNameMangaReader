using CommonLib.Models.Exeptions;

namespace Auth.Model.Exceptions
{
    public class MangaQueuedException : NNMRException
    {
        private static readonly string ERROR_CODE = "MQ.1";
        private static readonly string MESSAGE = "Manga Queued";
        private static readonly string DETAIL = "Manga parsing proccess was inseted to queue";

        public MangaQueuedException() : base(ERROR_CODE, MESSAGE, DETAIL)
        {

        }
    }
}
