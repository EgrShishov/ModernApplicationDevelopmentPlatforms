using Microsoft.Extensions.Options;
using System.Text.Json;
using WEB_253505_Shishov.HelperClasses;
using WEB_253505_Shishov.Services.FileService;

namespace WEB_253505_Shishov.Services.Authentication;

public class KeycloakAuthService : IAuthService
{
    private readonly KeycloakData _keycloakData;
    private readonly HttpClient _httpClient;
    private readonly IFileService _fileService;
    private readonly ITokenAccessor _tokenAccessor;
    public KeycloakAuthService(
        IOptions<KeycloakData> options, 
        HttpClient httpClient, 
        IFileService fileService, 
        ITokenAccessor tokenAccessor)
    {
        _httpClient = httpClient;
        _keycloakData = options.Value;
        _fileService = fileService;
        _tokenAccessor = tokenAccessor;
    }
    public async Task<(bool Result, string ErrorMessage)> RegisterUserAsync(string email, string password, IFormFile? avatar)
	{
        try
        {
            await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }

        var avatarUrl = "images/default-profile-picture.png";
        if (avatar != null)
        {
            var result = await _fileService.SaveFileAsync(avatar);
            if (result != null)
            {
                avatarUrl = result;
            }
        }

        var newUser = new CreateUserModel()
        {
            Email = email,
            Username = email,
        };

        newUser.Credentials.Add(new UserCredentials
        {
            Temporary = false,
            Value = password,
        });
        newUser.Attributes.Add("avatar", avatarUrl);

        var uri = $"{_keycloakData.Host}/admin/realms/{_keycloakData.Realm}/users";

        var userData = JsonSerializer.Serialize(newUser, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        var content = new StringContent(userData, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(uri, content);
        if (!response.IsSuccessStatusCode)
        {
            return (false, response.StatusCode.ToString());
        }

        return (true, response.Content.ToString());
	}

}
class CreateUserModel
{
    public Dictionary<string, string> Attributes { get; set; } = new();
    public string Username { get; set; }
    public string Email { get; set; }
    public bool Enabled { get; set; } = true;
    public bool EmailVerified { get; set; } = true;
    public List<UserCredentials> Credentials { get; set; } = new();
}
class UserCredentials
{
    public string Type { get; set; } = "password";
    public bool Temporary { get; set; } = false;
    public string Value { get; set; }
}