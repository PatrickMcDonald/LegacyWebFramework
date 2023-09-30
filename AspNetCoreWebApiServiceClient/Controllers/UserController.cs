using LegacyWcfServices;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebApiServiceClient.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet(template: "{userName}")]
    public async Task<ActionResult<UserInfo>> GetAsync(string userName)
    {
        return await _userService.GetUserInfoAsync(userName);
    }
}
