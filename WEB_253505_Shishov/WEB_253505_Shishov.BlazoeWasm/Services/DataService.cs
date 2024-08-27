using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Http;
using WEB_253505_Shishov.Domain.Entities;
using WEB_253505_Shishov.Domain.Models;

namespace WEB_253505_Shishov.BlazoeWasm.Services;

public class DataService : IDataService
{
	private readonly HttpClient _httpClient;
	private readonly JsonSerializerOptions _jsonSerializerOptions;
	private readonly IAccessTokenProvider _accessTokenProvider;
	private readonly string _pageSize;

	public DataService(HttpClient httpClient, IConfiguration configuration, IAccessTokenProvider accessTokenProvider)
	{
		_httpClient = httpClient;
		_pageSize = configuration.GetSection("ItemsPerPage").Value;
		_jsonSerializerOptions = new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};
		_accessTokenProvider = accessTokenProvider;
	}

	public List<Category> Categories { get; set; }
	public List<Constructor> Constructors { get; set; }
	public bool Success { get; set; }
	public string ErrorMessage { get; set; }
	public int TotalPages { get; set; }
	public int CurrentPage { get; set; }
	public Category SelectedCategory { get; set; } = null;

	public event Action DataLoaded;

	public async Task GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
	{
		var tokenRequest = await _accessTokenProvider.RequestAccessToken();
		if (tokenRequest.TryGetToken(out var accessToken))
		{
			_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken.Value);
		}

		try
		{
			var route = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}api/Constructors/");

			if (categoryNormalizedName is not null)
			{
				route.Append($"{categoryNormalizedName}/");
			}

			List<KeyValuePair<string, string>> queryData = new();

			if (pageNo > 1)
			{
				queryData.Add(KeyValuePair.Create("pageNo", pageNo.ToString()));
			}

			if (!_pageSize.Equals("3"))
			{
				queryData.Add(KeyValuePair.Create("pageSize", _pageSize));

			}

			if (queryData.Count > 0)
			{
				route.Append(QueryString.Create(queryData));
			}

			var response = await _httpClient.GetAsync(new Uri(route.ToString()));
			if (!response.IsSuccessStatusCode)
			{
				Success = false;
				ErrorMessage = $"Error occured in fetching data: {response.StatusCode.ToString()}";
			}

			var data = await response.Content.ReadFromJsonAsync<ResponseData<ProductListModel<Constructor>>>
																			(_jsonSerializerOptions);
			if (!data.Successfull)
			{
				Success = false;
				ErrorMessage = data.ErrorMessage;
			}

			Success = true;
			Constructors = data.Data.Items;
			CurrentPage = data.Data.CurrentPage;
			TotalPages = data.Data.TotalPages;
			DataLoaded.Invoke();

		}
		catch(Exception ex)
		{
			Success = false;
			ErrorMessage = $"Error occured in http client: {ex.Message}";
		}
	}

	public async Task GetCategoryListAsync(int pageNo = 1)
	{
		var tokenRequest = await _accessTokenProvider.RequestAccessToken();
		if(tokenRequest.TryGetToken(out var accessToken))
		{
			_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken.Value);
		}

		var urlString = $"{_httpClient.BaseAddress.AbsoluteUri}api/Categories";
		try
		{
			var response = await _httpClient.GetAsync(new Uri(urlString));
			if (!response.IsSuccessStatusCode)
			{
				Success = false;
				ErrorMessage = $"Error occured in fetching data: {response.StatusCode.ToString()}";
			}

			var data = await response.Content.ReadFromJsonAsync<ResponseData<List<Category>>>
																	(_jsonSerializerOptions);

			if (!data.Successfull)
			{
				Success = false;
				ErrorMessage = data.ErrorMessage;
			}

			Success = true;
			Categories = data.Data;
			DataLoaded.Invoke();

		}
		catch (Exception ex)
		{
			Success = false;
			ErrorMessage = $"Error occured in http client: {ex.Message}";
		}
	}
}
