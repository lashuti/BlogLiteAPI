using Amazon.S3.Model;

namespace BlogLiteAPI.Infrastructure.Services
{
    public interface IS3ImageService
    {
        Task<PutObjectResponse> UploadImageAsync(string imageKey, IFormFile file, CancellationToken cancellationToken);
        Task<GetObjectResponse> GetImageAsync(string imageKey, CancellationToken cancellationToken);
        Task<DeleteObjectResponse> DeleteImageAsync(string imageKey, CancellationToken cancellationToken);
    }
}
