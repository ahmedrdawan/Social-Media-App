

// Endpoints:
// POST /api/comments
// GET /api/comments/post/{postId}
// DELETE /api/comments/{id}

using System.ComponentModel.DataAnnotations.Schema;

public class Comment
{
    public string Id { get; set; }
    [ForeignKey("Post")]
    public string PostId { get; set; }
    [ForeignKey("User")]
    public string UserId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Post? Post { get; set; }
    public User? User { get; set; }
    public Comment()
    {
        Id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;   
    }
}