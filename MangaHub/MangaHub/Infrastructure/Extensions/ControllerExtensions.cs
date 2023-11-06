using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Infrastructure.Extensions;

public static class ControllerExtensions
{
    public static int GetCurrentUserId(this ControllerBase controller)
    {
        var userIdString = controller.HttpContext.User.FindFirst("id")
            ?.Value ?? "-1";

        var userId = int.Parse(userIdString);

        return userId;
    }
}
