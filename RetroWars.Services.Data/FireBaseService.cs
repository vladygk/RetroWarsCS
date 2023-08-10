
namespace RetroWars.Services.Data;

using Firebase.Storage;
using RetroWars.Services.Data.Contracts;

public class FireBaseService :IFireBaseService
    {
        //private static string ApiKey = "YOUR_API_KEY";
        private static string Bucket = "retrowars-asp.appspot.com";

        public async Task<string> UploadFile(string photo64,string fileFolder,string fileName)
        {
            var stream = new MemoryStream(Convert.FromBase64String(photo64));

            var uploadTask = new FirebaseStorage(Bucket)
                .Child(fileFolder)
                .Child(fileName)
                .PutAsync(stream);

           
            var downloadUrl = await uploadTask;

            return downloadUrl;
        }
    }

