using Microsoft.AspNetCore.Mvc;
using WEB_253505_Shishov.Domain.Entities;
using WEB_253505_Shishov.Domain.Models;
using WEB_253505_Shishov.Services.CategoryService;

namespace WEB_253505_Shishov.Services.ConstructorService;

public class MemoryConstructorService : IConstructorService
{
	private readonly ICategoryService _categoryService;
	private readonly IConfiguration _config;
	private List<Constructor> _constructors;
	private List<Category> _categories;
	public MemoryConstructorService([FromServices] IConfiguration config,
									ICategoryService categoryService)
	{
		_categoryService = categoryService;
		_categories = _categoryService.GetCategoryListAsync().Result.Data;
		_config = config;

		SetupData();
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

	public Task<ResponseData<ProductListModel<Constructor>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
	{
		/*		if (string.IsNullOrEmpty(categoryNormalizedName))
				{
					return Task.FromResult(ResponseData<ProductListModel<Constructor>>.Error("Empty or null categoryName"));
				}
		*/
		var itemsPerPage = _config.GetValue<int>("ItemsPerPage");
		var items = _constructors
			.Where(p => categoryNormalizedName == null || p.Category.NormalizedName.Equals(categoryNormalizedName))
			.ToList();
		int totalPages = (int)Math.Ceiling((double)items.Count() / itemsPerPage);

		var pagedItems = new ProductListModel<Constructor>
		{
			Items = items.Skip((pageNo - 1) * itemsPerPage).Take(itemsPerPage).ToList(),
			CurrentPage = pageNo,
			TotalPages = totalPages
		};

		var result = ResponseData<ProductListModel<Constructor>>.Success(pagedItems);

		return Task.FromResult(result);
	}

	public Task UpdateProductAsync(int id, Constructor constructor, IFormFile? formFile)
	{
		throw new NotImplementedException();
	}

	private void SetupData()
	{
		_constructors = new List<Constructor>
		{
			new Constructor
			{
				Id = 1,
				Picies = 100,
				Category = _categories[0],
				CategoryId = 1,
				Description = "City Police Patrol Car",
				Image = "images/lego1.jpg",
				Price = 9.99m
			},
			new Constructor
			{
				Id = 2,
				Picies = 230,
				Category = _categories[1],
				CategoryId = 2,
				Description = "Star Wars X-Wing Starfighter",
				Image = "images/lego2.jpg",
				Price = 29.99m
			},
			new Constructor
			{
				Id = 3,
				Picies = 1500,
				Category = _categories[6],
				CategoryId = 3,
				Description = "Technic Bugatti Chiron",
				Image = "images/lego3.jpg",
				Price = 349.99m
			},
			new Constructor
			{	
				Id = 4,
				Picies = 450,
				Category = _categories[6],
				CategoryId = 4,
				Description = "Ninjago Spinjitzu Masters",
				Image = "images/lego4.jpg",
				Price = 49.99m
			},
			new Constructor
			{
				Id = 5,
				Picies = 500,
				Category = _categories[6],
				CategoryId = 5,
				Description = "Friends Heartlake City Resort",
				Image = "images/lego5.jpg",
				Price = 59.99m
			},
			new Constructor
			{	
				Id = 6,
				Picies = 750,
				Category = _categories[6],
				CategoryId = 7,
				Description = "Harry Potter Hogwarts Castle",
				Image = "images/lego6.jpg",
				Price = 399.99m
			},
			new Constructor
			{
				Id = 7,
				Picies = 100,
				Category = _categories[6],
				CategoryId = 7,
				Description = "Creator 3-in-1 Rocket Truck",
				Image = "images/lego7.jpeg",
				Price = 14.99m
			},
			new Constructor
			{
				Id = 8,
				Picies = 1200,
				Category = _categories[7],
				CategoryId = 8,
				Description = "Marvel Super Heroes Avengers Tower",
				Image = "images/lego8.jpg",
				Price = 99.99m
			},
			new Constructor
			{
				Id = 9,
				Picies = 850,
				Category = _categories[8],
				CategoryId = 9,
				Description = "Architecture Empire State Building",
				Image = "images/lego9_.jpg",
				Price = 129.99m
			},
			new Constructor
			{
				Id = 10,
				Picies = 400,
				Category = _categories[9],
				CategoryId = 10,
				Description = "Disney Frozen Ice Castle",
				Image = "images/lego10.jpg",
				Price = 59.99m
			}
		};
	}
}
