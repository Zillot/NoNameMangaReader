using System;
namespace CommonLib.Models
{
    public interface IBaseDBModel
    {
        DateTime Created { get; set; }
        DateTime Updated { get; set; }
    }
}
