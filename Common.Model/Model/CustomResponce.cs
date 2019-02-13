using System;

namespace Custom.Model.Models
{
    public class CustomResponce
    {
        public string Message { get; set; }
        public Guid ResponseId { get; set; }
        public object Response { get; set; }

        public CustomResponce(CustomResponseType type)
        {
            this.ResponseId = Guid.NewGuid();
            this.Message = type.ToString();
        }
    }
}
