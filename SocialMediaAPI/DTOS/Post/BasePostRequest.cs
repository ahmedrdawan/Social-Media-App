using System.ComponentModel.DataAnnotations;

public abstract class BasePostRequest
{
    [Required]
    public string Content { get; set; }
    [Required]
    public string? ImageUrl { get; set; }
}