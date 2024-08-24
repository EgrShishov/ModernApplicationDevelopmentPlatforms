using System.Text;
using System.Text.Json;
using WEB_253505_Shishov.Domain.Entities;
using WEB_253505_Shishov.Domain.Models;

namespace WEB_253505_Shishov.Services.ConstructorService;

public class ApiConstructorService : IConstructorService
{
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly JsonSerializerOptions _serializerOptions;
	private readonly ILogger<ApiConstructorService> _logger;
	private readonly string _pageSize;
	public ApiConstructorService(IConfiguration configuration, 
								IHttpClientFactory httpClientFactory, 
								ILogger<ApiConstructorService> logger)
	{
		_httpClientFactory = httpClientFactory;
		_serializerOptions = new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};
		_logger = logger;
		_pageSize = configuration.GetSection("ItemsPerPage").Value;
	}

	public async Task<ResponseData<Constructor>> CreateProductAsync(Constructor constructor, IFormFile? formFile)
	{
		var client = _httpClientFactory.CreateClient("api");
		var uri = new Uri(client.BaseAddress.AbsoluteUri + "Constructors");
		
		var response = await client.PostAsJsonAsync(uri, constructor, _serializerOptions);
		if (!response.IsSuccessStatusCode)
		{
			_logger.LogError($"-----> object not created. Error:{ response.StatusCode.ToString()}");
			return ResponseData<Constructor>.Error($"Object not added. Error:{response.StatusCode.ToString()}");
		}

		return await response.Content.ReadFromJsonAsync<ResponseData<Constructor>>(_serializerOptions);
	}

	public Task DeleteProductAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<ResponseData<Constructor>> GetProductByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public async Task<ResponseData<ProductListModel<Constructor>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
	{
		var client = _httpClientFactory.CreateClient("api");

		var urlString = new StringBuilder($"{client.BaseAddress.AbsoluteUri}Constructors/");

		if (categoryNormalizedName != null)
		{
			urlString.Append($"{categoryNormalizedName}/");
		}
		if (pageNo >= 1) 
		{
			urlString.Append($"pageNo={pageNo}");
		}
		if (!_pageSize.Equals("3"))
		{
			urlString.Append(QueryString.Create("pageSize", _pageSize));
		}

		var response = await client.GetAsync(new Uri(urlString.ToString()));
		
		if (response.IsSuccessStatusCode)
		{
			try
			{
				return await response
				.Content
				.ReadFromJsonAsync<ResponseData<ProductListModel<Constructor>>>
															(_serializerOptions);
			}
			catch (JsonException ex)
			{
				_logger.LogError($"-----> Error: {ex.Message}");
				return ResponseData<ProductListModel<Constructor>>.Error($"Error: {ex.Message}");
			}
		}

		_logger.LogError($"-----> Error in fetching data from server. Error:{ response.StatusCode.ToString()}");

		return ResponseData<ProductListModel<Constructor>>.Error($"Data not fetched. Error:{response.StatusCode.ToString()}");
	}

	public Task UpdateProductAsync(int id, Constructor constructor, IFormFile? formFile)
	{
		throw new NotImplementedException();
	}
}
