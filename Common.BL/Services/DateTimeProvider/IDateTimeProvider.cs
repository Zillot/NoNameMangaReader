using System;

namespace Common.BL.Services
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow();
    }
}
