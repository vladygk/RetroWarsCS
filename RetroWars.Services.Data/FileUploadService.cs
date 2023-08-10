
namespace RetroWars.Services.Data;

using Microsoft.AspNetCore.Http;
using RetroWars.Services.Data.Contracts;


public class FileUploadService : IFileUploadService
{
    public string ConvertToBase64(string path)
    {
        byte[] bytes = File.ReadAllBytes(path);
        string fileBase64 = Convert.ToBase64String(bytes);

        return fileBase64;
    }

    public async Task<string> UploadFile(IFormFile? file)
    {
        if (file is  null) { 
        return String.Empty;
        }
        string path = String.Empty;

        string pathAndFileName = String.Empty;
        try
        {
            if (file.Length > 0)
            {
                string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Temp"));
                pathAndFileName = Path.Combine(path, filename);
                using (var filestream = new FileStream(pathAndFileName, FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                }

                return pathAndFileName;
            }

        }
        catch (Exception)
        {
            throw new Exception("Can't upload file");
        }
        return pathAndFileName;
    }
}
