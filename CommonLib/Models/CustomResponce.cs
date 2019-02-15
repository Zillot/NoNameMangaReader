using System;

namespace CommonLib.Models
{
    public class CustomResponse
    {
        public string Message { get; set; }
        public Guid ResponseId { get; set; }
        public object Response { get; set; }

        public CustomResponse(CustomResponseType type)
        {
            this.ResponseId = Guid.NewGuid();
            this.Message = type.ToString();
        }
    }
}
