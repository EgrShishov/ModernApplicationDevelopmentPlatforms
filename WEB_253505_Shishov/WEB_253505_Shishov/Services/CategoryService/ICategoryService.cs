using WEB_253505_Shishov.Domain.Entities;
using WEB_253505_Shishov.Domain.Models;

namespace WEB_253505_Shishov.Services.CategoryService;

public interface ICategoryService
{
	public Task<ResponseData<List<Category>>> GetCategoryListAsync();
}
