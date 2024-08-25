namespace WEB_253505_Shishov.Services.Authentication;

public interface IAuthService
{
	Task<(bool Result, string ErrorMessage)> RegisterUserAsync(string email, string password, IFormFile? avatar);
}
