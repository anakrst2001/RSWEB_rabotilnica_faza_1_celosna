using MVCBook.Interfaces;

namespace MVCBook.Services
{
    public class BufferedFileUploadLocalService1: IBufferedFileUploadService1
    {
        public async Task<string> UploadFile1(IFormFile file1, IWebHostEnvironment webHostEnvironment1)
        {
            string path = "";
            try
            {
                if (file1.Length > 0)
                {
                    path = Path.GetFullPath(Path.Combine(webHostEnvironment1.WebRootPath, "url"));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, file1.FileName), FileMode.Create))
                    {
                        await file1.CopyToAsync(fileStream);
                    }
                    return file1.FileName;
                }
                else
                {
                    return "none";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }
    }
}
