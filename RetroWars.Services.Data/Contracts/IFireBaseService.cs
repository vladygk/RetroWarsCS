namespace Bluebean_Backend.Utils.Interfaces
{
    public interface IFireBaseService
    {
        public Task<string> UploadFile(string photo64,string fileFolder, string fileName);
    }
}
