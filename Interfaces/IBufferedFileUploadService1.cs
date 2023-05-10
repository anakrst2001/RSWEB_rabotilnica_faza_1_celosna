namespace MVCBook.Interfaces
{
    public interface IBufferedFileUploadService1
    {
        Task<string> UploadFile1(IFormFile file1, IWebHostEnvironment webHostEnvironment1);
    }
}
