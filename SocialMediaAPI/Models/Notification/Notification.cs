


using System.ComponentModel.DataAnnotations.Schema;

public class Notification
{
    public string Id {get;set;}
    
    [ForeignKey(nameof(user))]
    public string? UserId {get;set;}
    public string Message {get;set;}
    public bool IsRead {get;set;}
    public DateTime CreatedAt {get;set;}

    public User? user {get;set;}


    public Notification(string Id, string UserId, string Message, DateTime CreatedAt, bool IsRead = false)
    {
        this.Id = Id;
        this.Message = Message;
        this.IsRead = IsRead;
        this.CreatedAt = CreatedAt;
        this.UserId = UserId;
    }
}
