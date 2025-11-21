


using System.Net;

public class AuthModel
{
    public HttpStatusCode StatusCode { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }

    public string Token { get; set; }
    public AuthModel(HttpStatusCode StatusCode, string Email = "", string message = "", bool success = false, string token = "")
    {
        this.StatusCode = StatusCode;
        this.Email = Email;
        Message = message;
        Success = success;
        Token = token;
    }
}