using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class LikeController : ControllerBase
{
    private readonly ILikeServices _likeServices;

    public LikeController(ILikeServices likeServices)
    {
        _likeServices = likeServices;
    }

    [HttpPost("{postId}")]
    public async Task<IActionResult> LikePost(Guid postId)
    {
        var response = await _likeServices.LikePostAsync(postId);
        return !response.Succes ? BadRequest(response) : Ok(response);
    }
    [HttpDelete("{postId}")]
    public async Task<IActionResult> UnLikePost(Guid postId)
    {
        var response = await _likeServices.UnLikePostAsync(postId);
        return !response.Succes ? BadRequest(response) : Ok(response);
    }

    [HttpGet("{postId}")]
    public async Task<IActionResult> GetLikesCount(Guid postId)
    {
        // also Can Return The AllUser Likes 
        var result = await _likeServices.GetLikesCountAsync(postId);
        return Ok(new {Likes = result});
    }
}