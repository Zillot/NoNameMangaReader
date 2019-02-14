using Auth.Model.DTOModels;

namespace Auth.BL.Services
{
	public interface IAuthService
	{
        string Login(UserCredentialsDTO credentials);
        string Login(AppCredentialsDTO credentials);
	}
}
