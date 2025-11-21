



using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public class UserFollower
{
    [ForeignKey(nameof(Follower))]
    public string FollowerId { get; set; }

    [ForeignKey(nameof(Following))]
    public string FollowingId { get; set; }
    public DateTime CreatedAt { get; set; }
    public User? Follower {get;set;}
    public User? Following {get;set;}
}