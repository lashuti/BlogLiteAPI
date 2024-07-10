using System.Runtime.CompilerServices;

namespace BlogLiteAPI.DataAccess
{
    public class BlogCreateModel
    {
        public required string Title { get; set; }
        public IFormFile? HeaderImage { get; set; }
        public required string Content { get; set; }

        public Blog AsBlogObject(string headerImageUrl)
        {
            if (string.IsNullOrEmpty(headerImageUrl))
            {
                throw new ArgumentException("Header image URL cannot be null or empty.", nameof(headerImageUrl));
            }

            return new Blog { Title = Title, Content = Content, HeaderImageUrl = headerImageUrl };
        }
    }
}
