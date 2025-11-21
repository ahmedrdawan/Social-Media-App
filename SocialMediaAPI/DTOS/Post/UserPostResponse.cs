


public class UserPostResponse
{
    public string Id { get; set; }
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; } = null!;
}