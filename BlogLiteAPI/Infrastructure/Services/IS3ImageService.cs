using Amazon.S3.Model;

namespace BlogLiteAPI.Infrastructure.Services
{
    public interface IS3ImageService
    {
        Task<PutObjectResponse> UploadImageAsync(string imageKey, IFormFile file);
        Task<GetObjectResponse> GetImageAsync(string imageKey);
        Task<DeleteObjectResponse> DeleteImageAsync(string imageKey);
    }
}
