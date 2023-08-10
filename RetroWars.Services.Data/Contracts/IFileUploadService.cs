namespace RetroWars.Services.Data.Contracts;

using Microsoft.AspNetCore.Http;


public interface IFileUploadService
{
    public  Task<string> UploadFile(IFormFile? file);

    public string ConvertToBase64(string path);
}
