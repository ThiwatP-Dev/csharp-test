using Microsoft.AspNetCore.Mvc;
using Web.Service.Dto;
using Web.Service.Interfaces;

namespace Web.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(IUserSerivce userSerivce) : ControllerBase
{
    private readonly IUserSerivce _userSerivce = userSerivce;

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUserDto request)
    {
        if (await _userSerivce.IsUsernameExists(request.UserName))
        {
            return BadRequest("Username already exists.");
        }

        await _userSerivce.CreateAsync(request);

        return Created();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await _userSerivce.Get();
        if (users.Count != 0)
        {
            return Ok(users);
        }

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] long id)
    {
        var user = await _userSerivce.GetByIdAsync(id);
        if (user is null)
        {
            return NotFound("User not found.");
        }

        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] CreateUserDto request)
    {
        var user = await _userSerivce.GetByIdAsync(id);
        if (user is null)
        {
            return NotFound("User not found.");
        }

        if (await _userSerivce.IsUsernameExists(request.UserName, id))
        {
            return BadRequest("Username already exists.");
        }

        await _userSerivce.UpdateAsync(id, request);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] long id)
    {
        var user = await _userSerivce.GetByIdAsync(id);
        if (user is null)
        {
            return NotFound("User not found.");
        }

        await _userSerivce.DeleteAsync(id);

        return Ok();
    }
}