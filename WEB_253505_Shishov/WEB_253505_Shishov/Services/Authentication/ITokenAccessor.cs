namespace WEB_253505_Shishov.Services.Authentication;

public interface ITokenAccessor
{
	Task<string> GetAccessTokenAsync();
	Task SetAuthorizationHeaderAsync(HttpClient httpClient);
}
