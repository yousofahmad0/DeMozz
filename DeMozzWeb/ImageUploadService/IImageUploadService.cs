namespace DeMozzWeb.ImageUploadService
{
    public interface IImageUploadService
    {
        Task<string> UploadImageAsync(IFormFile Im);
    }
}
