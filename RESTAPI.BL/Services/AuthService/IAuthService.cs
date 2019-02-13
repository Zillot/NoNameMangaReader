using RESTAPI.Model.DTOModels;

namespace RESTAPI.BL.Services
{
	public interface IAuthService
	{
        string Login(UserCredentialsDTO credentials);
        string Login(AppCredentialsDTO credentials);
	}
}
