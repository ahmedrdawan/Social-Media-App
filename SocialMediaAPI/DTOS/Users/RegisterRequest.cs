

using System.ComponentModel.DataAnnotations;

public class RegisterRequest
{
    [Required]
    [MaxLength(50)]
    public string Username { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}