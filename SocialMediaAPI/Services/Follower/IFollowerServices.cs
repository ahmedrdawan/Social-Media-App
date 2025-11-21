


public interface IFollowerServices
{
    Task<ResponseModel> FollowAsync(Guid userId);
    Task<ResponseModel> UnFollowAsync(Guid userId);
    Task<IEnumerable<FollowResponse>> GetFollowersAsync(Guid userId);
    Task<IEnumerable<FollowResponse>> GetFollowingAsync(Guid userId);
    Task<int> GetFollowerCountAsync(Guid userId);
    Task<int> GetFollowingCountAsync(Guid userId);
}