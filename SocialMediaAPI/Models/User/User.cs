using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public string? Bio { get; set; }
    public string? ProfilePhotoUrl { get; set; }
    public DateTime DateCreated { get; set; }
    public ICollection<Post>? Posts { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<Like>? Likes { get; set; }
    public ICollection<UserFollower>? Followers { get; set; }
    public ICollection<UserFollower>? Following { get; set; }
    public ICollection<Notification>? Notifications { get; set; }

    public User()
    {
        DateCreated = DateTime.UtcNow;
    }
}
