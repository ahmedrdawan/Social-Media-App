

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostServices postServices;
    private readonly IMapper mapper;

    public PostsController(IPostServices postServices,IMapper mapper)
    {
        this.postServices = postServices;
        this.mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreaetePost([FromBody] CreatePostRequest request)
    {
        Post mappPost = mapper.Map<Post>(request);
        ResponseModel postModel = await postServices.CreatePostAsync(mappPost);
        return postModel.Succes ? Created("GetPost", postModel) : BadRequest(postModel);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPost(Guid id)
    {
        var post = await postServices.GetPostByIdAsync(id);
        
        return post != null ? Ok(post) : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(Guid id,[FromBody] UpdatePostRequest request)
    {
        Post mappPost = mapper.Map<Post>(request);
        var postModel = await postServices.UpdatePostAsync(id, mappPost);
        return postModel.Succes ? NoContent() : BadRequest(postModel);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(Guid id)
    {
        var postModel = await postServices.DeletePostAsync(id);
        return postModel.Succes ? NoContent() : BadRequest(postModel);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllPosts()
    {
        var result = await postServices.GetAllPostsAsync();
        // Implementation for creating a post
        return Ok(result);
    }
}