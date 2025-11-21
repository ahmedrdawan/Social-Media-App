

using System.ComponentModel.DataAnnotations.Schema;

public class Like
{
    [ForeignKey("User")]
    public string UserId { get; set; }
    [ForeignKey("Post")]
    public string PostId { get; set; }
    public DateTime LikedAt { get; set; }
    public User? User { get; set; }
    public Post? Post { get; set; }

}


// ✅ STEP 6 — Likes Module
// ✔ Like a post
// ✔ Unlike a post
// ✔ Count likes
// Endpoints:
// POST /api/likes/{postId}
// DELETE /api/likes/{postId}
// GET /api/likes/count/{postId}
