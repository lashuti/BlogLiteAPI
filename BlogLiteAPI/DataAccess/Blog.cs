namespace BlogLiteAPI.DataAccess
{
    public class Blog
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string ImageName { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
