namespace RetroWars.Services.Data.Contracts;

public interface IFireBaseService
{
    public Task<string> UploadFile(string photo64, string fileFolder, string fileName);
}

