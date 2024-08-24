using WEB_253505_Shishov.Domain.Entities;
using WEB_253505_Shishov.Domain.Models;

namespace WEB_253505_Shishov.Services.ConstructorService;

public interface IConstructorService
{
	public Task<ResponseData<ProductListModel<Constructor>>> GetProductListAsync(string?
		categoryNormalizedName, int pageNo = 1);
	public Task<ResponseData<Constructor>> GetProductByIdAsync(int id);
	public Task UpdateProductAsync(int id, Constructor constructor, IFormFile? formFile);
	public Task DeleteProductAsync(int id);
	public Task<ResponseData<Constructor>> CreateProductAsync(Constructor constructor, IFormFile? formFile);
}
