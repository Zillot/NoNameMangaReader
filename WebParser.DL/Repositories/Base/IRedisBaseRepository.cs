namespace WebParser.DL.Repositories.Base
{
    public interface IRedisBaseRepository
    {
        void ClearAllData();
        void SetIntegrationTestMode();
    }
}
