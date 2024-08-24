using System.Text;
using System.Text.Json;
using WEB_253505_Shishov.Domain.Entities;
using WEB_253505_Shishov.Domain.Models;
using WEB_253505_Shishov.Services.FileService;

namespace WEB_253505_Shishov.Services.ConstructorService;

public class ApiConstructorService : IConstructorService
{
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly IFileService _fileService;
	private readonly JsonSerializerOptions _serializerOptions;
	private readonly ILogger<ApiConstructorService> _logger;
	private readonly string _pageSize;
	public ApiConstructorService(IConfiguration configuration, 
								IHttpClientFactory httpClientFactory, 
								IFileService fileService,
								ILogger<ApiConstructorService> logger)
	{
		_httpClientFactory = httpClientFactory;
		_fileService = fileService;
		_serializerOptions = new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};
		_logger = logger;
		_pageSize = configuration.GetSection("ItemsPerPage").Value;
	}

	public async Task<ResponseData<Constructor>> CreateProductAsync(Constructor constructor, IFormFile? formFile)
	{
		constructor.Image = "images/noimage.jpg";

		if (formFile != null)
		{
			var imageUrl = await _fileService.SaveFileAsync(formFile);

			if (!string.IsNullOrEmpty(imageUrl))
				constructor.Image = imageUrl;
		}

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

	public async Task DeleteProductAsync(int id)
	{
		var client = _httpClientFactory.CreateClient("api");

		var response = await client.DeleteAsync($"{client.BaseAddress}Constructors/{id}");
		if (!response.IsSuccessStatusCode)
		{
			throw new Exception("Delete operation failed");
		}
		return;
	}

	public async Task<ResponseData<Constructor>> GetProductByIdAsync(int id)
	{
		var client = _httpClientFactory.CreateClient("api");

		var response = await client.GetAsync($"{client.BaseAddress}Constructors/{id}");
		if (!response.IsSuccessStatusCode)
		{
			return ResponseData<Constructor>.Error($"Error: {response.StatusCode.ToString()}");
		}

		var product = await response.Content.ReadFromJsonAsync<Constructor>();

		return ResponseData<Constructor>.Success(product);
	}

	public async Task<ResponseData<ProductListModel<Constructor>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
	{
		var client = _httpClientFactory.CreateClient("api");

		var urlString = new StringBuilder($"{client.BaseAddress.AbsoluteUri}Constructors");

		if (categoryNormalizedName != null)
		{
			urlString.Append($"/{categoryNormalizedName}/");
		}
		if (pageNo >= 1) 
		{
			urlString.Append($"?pageNo={pageNo}");
		}
		if (!_pageSize.Equals("3"))
		{
			urlString.Append(QueryString.Create("&pageSize", _pageSize));
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

	public async Task UpdateProductAsync(int id, Constructor constructor, IFormFile? formFile)
	{
		var client = _httpClientFactory.CreateClient("api");

		if (formFile != null)
		{
			try
			{
				await _fileService.DeleteFileAsync(constructor.Image);
			}
			catch (Exception ex)
			{
				throw;
			}

			var imageUrl = await _fileService.SaveFileAsync(formFile);

			if (!string.IsNullOrEmpty(imageUrl))
				constructor.Image = imageUrl;
		}

		// TODO var response = await client.PutAsJsonAsync<
	}
}
