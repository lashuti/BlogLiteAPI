using Amazon.S3;
using Amazon.S3.Model;

namespace BlogLiteAPI.Infrastructure.Services
{
    public class S3ImageService(IAmazonS3 s3) : IS3ImageService
    {
        private readonly IAmazonS3 _s3 = s3;
        private readonly string directory = "blogs";
        private readonly string _bucketName = "bloglitebucket";

        public async Task<PutObjectResponse> UploadImageAsync(string imageKey, IFormFile file, CancellationToken cancellationToken)
        {
            var putObjectRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = $"{directory}/{imageKey}",
                ContentType = file.ContentType,
                InputStream = file.OpenReadStream(),
                Metadata =
                {
                    ["x-amz-meta-originalname"] = file.FileName,
                    ["x-amz-meta-extension"] = Path.GetExtension(file.FileName)
                }
            };

            return await _s3.PutObjectAsync(putObjectRequest, cancellationToken);
        }

        public Task<GetObjectResponse> GetImageAsync(string imageKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteObjectResponse> DeleteImageAsync(string imageKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
