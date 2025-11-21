

// ✔ Add comment
// ✔ Get comments for post
// ✔ Delete comment

public interface ICommentServices
{
    Task<ResponseModel> AddCommentAsync(string id, Comment comment);
    Task<IEnumerable<commentResponse>> GetCommentsForPostAsync(string postId);
    Task<ResponseModel> DeleteCommentAsync(Guid commentId);

    Task<ResponseModel> UpdateCommentAsync(Guid commentId, Comment comment);
    
}