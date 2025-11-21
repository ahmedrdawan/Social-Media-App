
using Microsoft.EntityFrameworkCore;

public class PostRepositry: IPostServices
{
    private readonly IAuthService authService;
    private readonly Appdbcontext appdbcontext;

    public PostRepositry(IAuthService authService,Appdbcontext appdbcontext)
    {
        this.authService = authService;
        this.appdbcontext = appdbcontext;
    }
    public async Task<ResponseModel> CreatePostAsync(Post post)
    {
        Post? result = await SearchPost(post.Id);
        if (result != null)
            return new ResponseModel("Post already exists");
        
        var currentUser = await authService.GetCurrentUserAsync();
        post.UserId = currentUser.Id;
        await appdbcontext.Posts.AddAsync(post);


        var IsSaved = await appdbcontext.SaveChangesAsync();
        return (IsSaved > 0)? new ResponseModel("Post created successfully", true):
                            new ResponseModel("Failed to create post");

    }

    public async Task<ResponseModel> DeletePostAsync(Guid id)
    {
        Post? result = await SearchPost(id.ToString());
        if (result == null)
            return new ResponseModel("Post not found");
        if (result.UserId != (await authService.GetCurrentUserAsync()).Id)
            return new ResponseModel("You are not authorized to delete this post");
        appdbcontext.Posts.Remove(result);
        var IsSaved = await appdbcontext.SaveChangesAsync();
        return (IsSaved > 0)? new ResponseModel("Post deleted successfully", true):
                            new ResponseModel("Failed to delete post");
    }

    public async Task<ResponseModel> UpdatePostAsync(Guid id, Post post)
    {
        var currentUser = await authService.GetCurrentUserAsync();
        var existing = await SearchPost(id.ToString());

        if (existing == null)
            return new ResponseModel("Post not found");

        if (existing.UserId != currentUser.Id)
            return new ResponseModel("Not allowed to update this post");

        existing.Content = post.Content;
        existing.ImageUrl = post.ImageUrl;

        appdbcontext.Posts.Update(existing);
        var saved = await appdbcontext.SaveChangesAsync();

        return saved > 0 ? new ResponseModel("Post updated successfully", true)
                        : new ResponseModel("Failed to update post");
    }


    public async Task<IEnumerable<UserPostResponse?>> GetAllPostsAsync()
    {
       
        var result = await appdbcontext.Posts.ToListAsync();
        var userPosts = new List<UserPostResponse?>();
        foreach (var post in result)
        {
            var user = await appdbcontext.Users.FindAsync(post.UserId);
            if (user != null)
            {
                userPosts.Add(new UserPostResponse
                {
                    Id = post.Id,
                    Content = post.Content,
                    CreatedAt = post.CreatedAt,
                    UserId = user.Id,
                    UserName = user.UserName
                });
            }
        }
        return userPosts;
    }

    public async Task<UserPostResponse?> GetPostByIdAsync(Guid id)
    {
        Post? result = await SearchPost(id.ToString());
        return result != null ? new UserPostResponse
        {
            Id = result.Id,
            Content = result.Content,
            CreatedAt = result.CreatedAt,
            UserId = result.UserId,
            UserName = (await appdbcontext.Users.FindAsync(result.UserId))!.UserName
        } : null;
    }

    private async Task<Post?> SearchPost(string id)
    {
        var result = await appdbcontext.Posts.FindAsync(id);
        return result;
    }
}