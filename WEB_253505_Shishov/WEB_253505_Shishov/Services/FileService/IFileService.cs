namespace WEB_253505_Shishov.Services.FileService;

public interface IFileService
{
	Task<string> SaveFileAsync(IFormFile formFile);
	Task DeleteFileAsync(string fileName);
}
