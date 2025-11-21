

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class Appdbcontext(DbContextOptions<Appdbcontext> options) : IdentityDbContext<User>(options)
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<UserFollower> UserFollowers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<Like>()
            .HasKey(l=>new {l.PostId, l.UserId});

        builder.Entity<UserFollower>()
            .HasKey(uf => new { uf.FollowerId, uf.FollowingId });

        builder.Entity<UserFollower>()
            .HasOne(uf => uf.Follower)
            .WithMany(u => u.Following)
            .HasForeignKey(uf => uf.FollowerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<UserFollower>()
            .HasOne(uf => uf.Following)
            .WithMany(u => u.Followers)
            .HasForeignKey(uf => uf.FollowingId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}
