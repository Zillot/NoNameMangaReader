using Auth.Model.DTOModels;
using CommonLib.Models.DTOModels;

namespace Auth.BL.Services
{
	public interface IAuthService
	{
        string Login(UserCredentialsDTO credentials);
        string Login(AppCredentialsDTO credentials);
	}
}
