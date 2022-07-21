
namespace DeMozzWeb.ImageUploadService
{
    public class LocalImageUploadService : IImageUploadService
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;

        public LocalImageUploadService(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            Environment = environment;
        }

        
        public async Task<string> UploadImageAsync(IFormFile Im)
        {
            var ImagePath = Path.Combine(Environment.ContentRootPath, @"wwwroot\ResumeImages", Im.FileName);
            using var fileStream = new FileStream(ImagePath, FileMode.Create);
            await Im.CopyToAsync(fileStream);

            return "/ResumeImages/"+Im.FileName;
        }
    }
}
