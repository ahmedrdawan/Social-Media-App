



using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentServices CommentServices;
    private readonly IMapper mapper;

    public CommentsController(ICommentServices CommentServices,IMapper mapper)
    {
        this.CommentServices = CommentServices;
        this.mapper = mapper;
    }

    [HttpPost("{postId}")]
    public async Task<IActionResult> CreaetePost(string postId, [FromBody] CreateCommentRequest request)
    {
        Comment mappComment = mapper.Map<Comment>(request);
        ResponseModel CommentModel = await CommentServices.AddCommentAsync(postId, mappComment);
        return CommentModel.Succes ? Created("GetComment", CommentModel) : BadRequest(CommentModel);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetComments(Guid id)
    {
        var Comment = await CommentServices.GetCommentsForPostAsync(id.ToString());
        
        return Comment != null ? Ok(Comment) : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComment(Guid id,[FromBody] UpdateCommentRequest request)
    {
        Comment mappComment = mapper.Map<Comment>(request);
        var CommentModel = await CommentServices.UpdateCommentAsync(id, mappComment);
        return CommentModel.Succes ? NoContent() : BadRequest(CommentModel);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(Guid id)
    {
        var CommentModel = await CommentServices.DeleteCommentAsync(id);
        return CommentModel.Succes ? NoContent() : BadRequest(CommentModel);
    }
    

}