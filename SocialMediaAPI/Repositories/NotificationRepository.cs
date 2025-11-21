




using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class NotificationRepository(Appdbcontext appdbcontext) : INotificationServices
{
    public async Task<NotificationResponse> NewCommentAsync(string UserId)
    {
        return await NewNotification(UserId, "Comment");
    }

    public async Task<NotificationResponse> NewLikeAsync(string UserId)
    {
        return await NewNotification(UserId, "Like");
    }

    private async Task<NotificationResponse> NewNotification(string UserId, string message)
    {
        var Notification = new Notification(
                    Id: Guid.NewGuid().ToString(),
                    UserId: UserId.ToString(),
                    Message: $"New {message} Recived",
                    CreatedAt: DateTime.UtcNow
                );
        await appdbcontext.Notifications.AddAsync(Notification);
        var IsSaved = await appdbcontext.SaveChangesAsync();

        return IsSaved > 0 ? new NotificationResponse { 
            NotificationId = Notification.Id,
            Message = Notification.Message,
            CreatedAt = Notification.CreatedAt
        } :
            new NotificationResponse {
             Message = "Notification Falid" };
    }

    public async Task<NotificationResponse> NewFollowAsync(string UserId)
    {
        return await NewNotification(UserId, "Follow");
    }

    public async Task<IEnumerable<NotificationResponse>> GetNotificationsAsync()
    {
        return await appdbcontext.Notifications.Select(n => new NotificationResponse
        {
            NotificationId = n.Id,
            CreatedAt = n.CreatedAt,
            IsRead = n.IsRead,
            Message = n.Message
        }).ToListAsync();
    }

    public async Task<NotificationResponse> GetNotificationByIdAsync(string NotificationId)
    {
        var result = await appdbcontext.Notifications.FindAsync(NotificationId);
        return new NotificationResponse
        {
            NotificationId = result.Id,
            CreatedAt = result.CreatedAt,
            IsRead = result.IsRead,
            Message = result.Message
        };
    }

    public async Task<bool> MarkAsReadAsync(string notificationId)
    {
        var notification = await appdbcontext.Notifications.FindAsync(notificationId);
        if (notification == null)
            return false;

        if (!notification.IsRead)
        {
            notification.IsRead = true;
            await appdbcontext.SaveChangesAsync();
        }
        return true;
    }
}