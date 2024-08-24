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
	public async Task<ResponseData<Constructor>> CreateProductAsync(Constructor constructor, IFormFile? formFile)
	{
		var newProduct = await _context.Constructors.AddAsync(constructor);

		return ResponseData<Constructor>.Success(newProduct.Entity);
	}

	public async Task DeleteProductAsync(int id)
	{
		var product = await _context.Constructors.FirstOrDefaultAsync(c => c.Id == id);
		if (product is null)
		{
			return;
		}

		_context.Entry(product).State = EntityState.Deleted;
	}

	public async Task<ResponseData<Constructor>> GetProductByIdAsync(int id)
	{
		var product = await _context.Constructors.FirstOrDefaultAsync(c => c.Id == id);
		if (product is null)
		{
			return ResponseData<Constructor>.Error($"No such object with id : {id}");
		}
		return ResponseData<Constructor>.Success(product);
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

		int totalPages = (int)Math.Ceiling(count / (double)pageSize);

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

	public async Task UpdateProductAsync(int id, Constructor constructor, IFormFile? formFile)
	{
		var product = await _context.Constructors.FirstOrDefaultAsync(c => c.Id == id);
		if (product is null)
			return;

		product.Price = constructor.Price;
		product.Description = constructor.Description;
		product.Category = constructor.Category;
		product.CategoryId = constructor.CategoryId;
		product.Picies = constructor.Picies;
		product.Image = constructor.Image;

		_context.Entry(product).State = EntityState.Modified;
	}
}
