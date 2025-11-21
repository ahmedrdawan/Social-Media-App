


using Azure;
using Microsoft.EntityFrameworkCore;

public class FollowerRespositry(IAuthService authService, Appdbcontext appdbcontext, INotificationServices notificationServices) : IFollowerServices
{
    public async Task<ResponseModel> FollowAsync(Guid userId)
    {
        var currentUser = await authService.GetCurrentUserAsync();

        var result = await appdbcontext.UserFollowers.FindAsync(currentUser.Id,userId.ToString());

        if (result != null)
            return new ResponseModel(message: "The User Is Already Follow Him");
        
        if (userId.ToString() == currentUser.Id)
            return new ResponseModel { message = "You cannot follow yourself!" };

        var followerUser = new UserFollower
        {
            FollowerId = currentUser.Id,
            FollowingId = userId.ToString(),
            CreatedAt = DateTime.UtcNow
        };
        await appdbcontext.UserFollowers.AddAsync(followerUser);
        int IsSave = await appdbcontext.SaveChangesAsync();
        if (IsSave <= 0)
            return new ResponseModel(message: "Created follower is Faild");

        await notificationServices.NewFollowAsync(userId.ToString());
        return new ResponseModel(Succes:true, message: "Created follower is Sucess");
    }

    public async Task<IEnumerable<FollowResponse>> GetFollowersAsync(Guid userId)
    {
        var result = await authService.GetCurrentUserAsync();
        if (result.Id != userId.ToString())
            return null;
        
        return await appdbcontext.UserFollowers.Where(uf=>uf.FollowingId == result.Id).Select(uf=>new FollowResponse
        {
            UserName = uf.Follower.UserName
        }).ToListAsync();
    }

    public async Task<IEnumerable<FollowResponse>> GetFollowingAsync(Guid userId)
    {
        var result = await authService.GetCurrentUserAsync();
        if (result.Id != userId.ToString())
            return null;
        
        return await appdbcontext.UserFollowers.Where(uf=>uf.FollowerId == result.Id).Select(uf=>new FollowResponse
        {
            UserName = uf.Follower.UserName
        }).ToListAsync();
    }

    public async Task<ResponseModel> UnFollowAsync(Guid userId)
    {
        var currentUser = await authService.GetCurrentUserAsync();

        var result = await appdbcontext.UserFollowers.FindAsync(currentUser.Id,userId.ToString());

        if (result == null)
            return new ResponseModel(message: "The User Is Already UnFollow Him");
            
        if (userId.ToString() == currentUser.Id)
            return new ResponseModel { message = "You cannot follow yourself!" };
        
        appdbcontext.UserFollowers.Remove(result);
        int IsSave = await appdbcontext.SaveChangesAsync();
        return IsSave > 0 ? new ResponseModel(Succes:true, message: "Deleted follower is Sucess"):
            new ResponseModel(message: "Deleted follower is Faild");
    }


    public async Task<int> GetFollowingCountAsync(Guid userId) =>
         await appdbcontext.UserFollowers.CountAsync(uf=>uf.FollowerId == userId.ToString());

    public async Task<int> GetFollowerCountAsync(Guid userId) =>
         await appdbcontext.UserFollowers.CountAsync(uf=>uf.FollowingId == userId.ToString());


}