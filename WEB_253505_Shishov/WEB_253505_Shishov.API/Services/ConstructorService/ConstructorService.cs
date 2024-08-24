using Microsoft.EntityFrameworkCore;
using WEB_253505_Shishov.API.Data;
using WEB_253505_Shishov.Domain.Entities;
using WEB_253505_Shishov.Domain.Models;

namespace WEB_253505_Shishov.API.Services.ConstructorService;

public class ConstructorService : IConstructorService
{
	private readonly int _maxPageSize = 20;
	private readonly AppDbContext _context;
	public ConstructorService(AppDbContext context)
	{
		_context = context;
	}
	public Task<ResponseData<Constructor>> CreateProductAsync(Constructor constructor, IFormFile? formFile)
	{
		throw new NotImplementedException();
	}

	public Task DeleteProductAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<ResponseData<Constructor>> GetProductByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public async Task<ResponseData<ProductListModel<Constructor>>> GetProductListAsync(
								string? categoryNormalizedName, 
								int pageNo = 1, 
								int pageSize = 3)
	{
		var query = _context.Constructors.AsQueryable();
		var dataList = new ProductListModel<Constructor>();

		if (pageSize > _maxPageSize)
			pageSize = _maxPageSize;

		query = query
			.Where(d => categoryNormalizedName == null || d.Category.NormalizedName.Equals(categoryNormalizedName));

		var count = await query.CountAsync();
		if (count == 0)
		{
			return ResponseData<ProductListModel<Constructor>>.Success(dataList);
		}

		int totalPages = (int)Math.Ceiling((double)count / pageSize);

		if (pageNo > totalPages)
		{
			return ResponseData<ProductListModel<Constructor>>.Error("No such page");
		}

		dataList.Items = await query
								.OrderBy(c => c.Id)
								.Skip((pageNo - 1) * pageSize)
								.Take(pageSize)
								.ToListAsync();

		dataList.CurrentPage = pageNo;
		dataList.TotalPages = totalPages;

		return ResponseData<ProductListModel<Constructor>>.Success(dataList);
	}

	public Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
	{
		throw new NotImplementedException();
	}

	public Task UpdateProductAsync(int id, Constructor constructor, IFormFile? formFile)
	{
		throw new NotImplementedException();
	}
}
