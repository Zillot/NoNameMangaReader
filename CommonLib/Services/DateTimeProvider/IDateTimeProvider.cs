using System;

namespace CommonLib.Services
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow();
    }
}
