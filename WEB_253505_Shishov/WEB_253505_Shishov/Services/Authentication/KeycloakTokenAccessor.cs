using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Json.Nodes;
using WEB_253505_Shishov.HelperClasses;

namespace WEB_253505_Shishov.Services.Authentication;

public class KeycloakTokenAccessor : ITokenAccessor
{
	private readonly IHttpContextAccessor _contextAccessor;
	private readonly HttpClient _httpclient;
	private readonly KeycloakData _keycloakData;
	private readonly HttpContext _context;
	public KeycloakTokenAccessor(IHttpContextAccessor contextAccessor, HttpClient httpClient, IOptions<KeycloakData> options)
	{
		_contextAccessor = contextAccessor;
		_httpclient = httpClient;
		_context = _contextAccessor.HttpContext;
		_keycloakData = options.Value;
	}
	public async Task<string> GetAccessTokenAsync()
	{
		if (_context.User.Identity.IsAuthenticated)
		{
			return await _context.GetTokenAsync("access_token");
		}

		var uri = $"{_keycloakData.Host}/realms/{_keycloakData.Realm}/protocol/openid-connect/token";

		var content = new FormUrlEncodedContent([
			new KeyValuePair<string,string>("client_id", _keycloakData.ClientId),
			new KeyValuePair<string,string>("grant_type", "client_credentials"),
			new KeyValuePair<string,string>("client_secret", _keycloakData.ClientSecret)
			]);

		var response = await _httpclient.PostAsync(uri, content);
		if (!response.IsSuccessStatusCode)
		{
			throw new HttpRequestException(response.StatusCode.ToString());
		}

		var jsonString = await response.Content.ReadAsStringAsync();
		return JsonObject.Parse(jsonString)["access_token"].ToString();
	}

	public async Task SetAuthorizationHeaderAsync(HttpClient httpClient)
	{
		var token = await GetAccessTokenAsync();

		httpClient.DefaultRequestHeaders.Authorization = 
			new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
	}
}
