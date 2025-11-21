

public interface ILikeServices
{
    Task<ResponseModel> LikePostAsync(Guid postId);
    Task<ResponseModel> UnLikePostAsync(Guid postId);
    Task<int> GetLikesCountAsync(Guid postId);
}