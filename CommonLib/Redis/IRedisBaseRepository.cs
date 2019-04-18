namespace CommonLib.Redis
{
    public interface IRedisBaseRepository
    {
        void ClearAllData();
        void SetIntegrationTestMode();
    }
}
