using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NotificationsController(INotificationServices notificationServices) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetNotifications()
    {
        var result = await notificationServices.GetNotificationsAsync();
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("read/{UserId}")]
    public async Task<IActionResult> GetNotificationById(Guid NotificationId)
    {
        var result = await notificationServices.GetNotificationByIdAsync(NotificationId.ToString());
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpPut("read/{notificationId}")]
    public async Task<IActionResult> MarkAsRead(string notificationId)
    {
        var success = await notificationServices.MarkAsReadAsync(notificationId);
        if (!success)
            return NotFound();

        return NoContent();
    }
}