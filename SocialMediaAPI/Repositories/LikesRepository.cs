




using Microsoft.EntityFrameworkCore;

public class LikeRepository(IAuthService authService,Appdbcontext appdbcontext) : ILikeServices
{
    public async Task<ResponseModel> UnLikePostAsync(Guid postId)
    {
        var result = await appdbcontext.Posts.FindAsync(postId);
        if (result == null)
            return new ResponseModel{message = "Post not found"}; 

        var currentUser = await authService.GetCurrentUserAsync();
        var exist = await appdbcontext.Likes.FindAsync(postId, currentUser.Id);
        if (exist == null)
            return new ResponseModel { message = "Already Unliked this post" };
        
        appdbcontext.Likes.Remove(exist);
        var isSucces = await appdbcontext.SaveChangesAsync();
        return isSucces > 0 ? new ResponseModel(Succes:true, message: "unLikePost Sucess"):
            new ResponseModel(message: "ynLikePost Falid");
    }
    public async Task<ResponseModel> LikePostAsync(Guid postId)
    {
        var result = await appdbcontext.Posts.FindAsync(postId.ToString());
        if (result == null)
            return new ResponseModel{message = "Post not found"}; 


        var currentUser = await authService.GetCurrentUserAsync();
        var exist = await appdbcontext.Likes.FindAsync(postId.ToString(), currentUser.Id);
        if (exist != null)
            return new ResponseModel { message = "Already liked this post" };

        Like like = new Like
        {
            UserId = currentUser.Id,
            PostId = postId.ToString(),
            LikedAt = DateTime.UtcNow
        };

        await appdbcontext.Likes.AddAsync(like);
        var isSucces = await appdbcontext.SaveChangesAsync();
        return isSucces > 0 ? new ResponseModel(Succes:true, message: "LikePost Sucess"):
            new ResponseModel(message: "LikePost Falid");
    }
    public async Task<int> GetLikesCountAsync(Guid postId)
    {
        return await appdbcontext.Likes.CountAsync(l=> l.PostId == postId.ToString());
    }
}