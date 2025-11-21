



using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FollowController(IFollowerServices followerServices) : ControllerBase
{

    [HttpPost("{userId}")]
    public async Task<IActionResult> Follow(Guid userId)
    {
        var result = await followerServices.FollowAsync(userId);
        return result.Succes ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> UnFollow(Guid userId)
    {
        var result = await followerServices.UnFollowAsync(userId);
        return result.Succes ? NoContent() : BadRequest(result);
    }

    [HttpGet("followers/{userId}")]
    public async Task<IActionResult> GetFollower(Guid userId)
    {
        var result = await followerServices.GetFollowersAsync(userId);
        return Ok(result);
    }

    [HttpGet("followeing/{userId}")]
    public async Task<IActionResult> GetFollowing(Guid userId)
    {
        var result = await followerServices.GetFollowingAsync(userId);
        return Ok(result);
    }

    [HttpGet("followers/count/{userId}")]
    public async Task<IActionResult> GetFollowersCount(Guid userId)
    {
        var result = await followerServices.GetFollowerCountAsync(userId);
        return Ok(new { FollowersCount = result});
    }
    [HttpGet("following/count/{userId}")]
    public async Task<IActionResult> GetFollowingCount(Guid userId)
    {
        var result = await followerServices.GetFollowingCountAsync(userId);
        return Ok(new { FollowersCount = result});
    }
}
