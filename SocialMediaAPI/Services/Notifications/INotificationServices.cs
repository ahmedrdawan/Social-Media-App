


public interface INotificationServices
{
    Task<NotificationResponse> NewLikeAsync(string UserId);
    Task<NotificationResponse> NewCommentAsync(string UserId);
    Task<NotificationResponse> NewFollowAsync(string UserId);

    Task<IEnumerable<NotificationResponse>> GetNotificationsAsync();
    Task<NotificationResponse> GetNotificationByIdAsync(string NotificationId);

    Task<bool> MarkAsReadAsync(string NotificationId);
}