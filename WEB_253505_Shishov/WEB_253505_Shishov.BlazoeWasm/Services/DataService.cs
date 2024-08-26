using WEB_253505_Shishov.Domain.Entities;

namespace WEB_253505_Shishov.BlazoeWasm.Services;

public class DataService : IDataService
{
	public List<Category> Categories { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public List<Constructor> Constructors { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public bool Success { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public string ErrorMessage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public int TotalPages { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public int CurrentPage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public Category SelectedCategory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

	public event Action DataLoaded;

	public Task GetCategoryListAsync()
	{
		throw new NotImplementedException();
	}

	public Task GetProductListAsync(int pageNo = 1)
	{
		throw new NotImplementedException();
	}
}
