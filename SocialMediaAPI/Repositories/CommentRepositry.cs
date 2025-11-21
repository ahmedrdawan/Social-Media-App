




using Microsoft.EntityFrameworkCore;

public class CommentRepository(IAuthService authService,Appdbcontext appdbcontext, INotificationServices notificationServices) : ICommentServices
{
    public async Task<ResponseModel> AddCommentAsync(string id, Comment comment)
    {
        var currentUser = await authService.GetCurrentUserAsync();
        comment.UserId = currentUser.Id;
        comment.CreatedAt = DateTime.UtcNow;
        comment.PostId = id.ToString();

        await appdbcontext.Comments.AddAsync(comment);

        var Issaved = await appdbcontext.SaveChangesAsync();
        if (Issaved <= 0)
            return new ResponseModel { message = "Failed to add comment" };
        
        var post = await appdbcontext.Posts.FindAsync(id);
        if(post != null)
            await notificationServices.NewCommentAsync(post.UserId);
        
        return new ResponseModel { Succes = true, message = "Comment added successfully" };
    }

    public async Task<ResponseModel> DeleteCommentAsync(Guid commentId)
    {
        var comment = await appdbcontext.Comments.FindAsync(commentId);
        if (comment == null)
            return new ResponseModel { message = "Comment not found" };

        var currentUser = await authService.GetCurrentUserAsync();
        if (comment.UserId != currentUser.Id)
            return new ResponseModel { message = "You are not authorized to delete this comment" };

        appdbcontext.Comments.Remove(comment);
        var Issaved = await appdbcontext.SaveChangesAsync();
        return Issaved > 0 ? new ResponseModel { Succes = true, message = "Comment deleted successfully" } :
            new ResponseModel { message = "Failed to delete comment" };
    }


    public async Task<IEnumerable<commentResponse>> GetCommentsForPostAsync(string postId)
    {
        var CommentResponse = await appdbcontext.Comments.Where(e=>e.PostId == postId)
            .Select(e=> new commentResponse
            {
                Id = e.Id,
                Content = e.Content,
                CreatedAt = e.CreatedAt,
                UserId = e.UserId,
                UserName = e.User!.UserName
            })
            .ToListAsync();
        return CommentResponse;
    }

    public async Task<ResponseModel> UpdateCommentAsync(Guid commentId, Comment comment)
    {
        var oldComment = await appdbcontext.Comments.FindAsync(commentId);
        if (comment == null)
            return new ResponseModel { message = "Comment not found" };

        var currentUser = await authService.GetCurrentUserAsync();
        if (comment.UserId != currentUser.Id)
            return new ResponseModel { message = "You are not authorized to delete this comment" };

        oldComment!.Content = comment.Content;
        oldComment.UpdatedAt = DateTime.UtcNow;


        appdbcontext.Comments.Update(comment);
        var Issaved = await appdbcontext.SaveChangesAsync();
        return Issaved > 0 ? new ResponseModel { Succes = true, message = "Comment Updated successfully" } :
            new ResponseModel { message = "Failed to Updated comment" };
    }


}