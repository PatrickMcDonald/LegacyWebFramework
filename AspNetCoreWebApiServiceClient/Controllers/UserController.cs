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

    [HttpGet(template: "Async/{method}")]
    public async Task<IActionResult> Async(string method)
    {
        await _userService.AsyncMethodAsync(method);

        return Ok();
    }

    [HttpGet(template: "Parallel/{count:int}")]
    public async Task<IActionResult> Parallel(int count)
    {
        await _userService.ParallelMethodAsync(count);

        return Ok();
    }
}
