

using System.ComponentModel.DataAnnotations.Schema;

public class Post
{
    public string Id { get; set; }
    public string Content { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    [ForeignKey(nameof(User))]
    public string? UserId { get; set; }
    public User? User { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<Like>? Likes { get; set; }
    public Post()
    {
        Id = Guid.NewGuid().ToString();
    }
    public Post(string content, DateTime createdAt, string imageUrl)
    {
        Content = content;
        CreatedAt = createdAt;
        ImageUrl = imageUrl;
    }
}

