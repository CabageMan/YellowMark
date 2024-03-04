using Microsoft.AspNetCore.Mvc;
using YellowMark.Application.Contexts.User;
using YellowMark.Domain.Users;

namespace YellowMark.Hosts.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(
    ILogger<UserController> logger,
    IUserService userService
        ) : ControllerBase
{
    private readonly IUserService _userService = userService;

    private readonly ILogger<UserController> _logger = logger;


    [HttpGet("get-by-id")]
    public async Task<IActionResult> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var result = await _userService.GetById(userId, cancellationToken);
        return Ok(result);
    }

    [HttpPost("get-by-id")]
    public async Task<IActionResult> CreateAsync(CreateUserDto userModel, CancellationToken cancellationToken)
    {
        var userId = await _userService.CreateAsync(userModel, cancellationToken);
        return Created(nameof(CreateAsync), userId);
    }
}
