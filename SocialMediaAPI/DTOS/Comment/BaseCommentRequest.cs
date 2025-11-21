

using System.ComponentModel.DataAnnotations;

public abstract class BaseCommentRequest
{   
    [Required]
    public string Content { get; set; }
}