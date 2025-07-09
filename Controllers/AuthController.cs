using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly ITokenRepository tokenRepository;

    public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
    {
        this.userManager = userManager;
        this.tokenRepository = tokenRepository;
    }
    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var identityUser = new IdentityUser
        {
            UserName = registerDto.UserName,
            Email = registerDto.UserName,

        };
        var res = await userManager.CreateAsync(identityUser, registerDto.Password);
        if (res.Succeeded)
        {
            if (registerDto.Roles != null && registerDto.Roles.Any())
            {
                res = await userManager.AddToRolesAsync(identityUser, registerDto.Roles);
                if (res.Succeeded)
                {
                    return Ok(new { message = "User created successfully" });
                }
                else
                {
                    return BadRequest(new { message = "User created but roles not assigned", errors = res.Errors });
                }
            }
        }
        return BadRequest(new { message = "User not created", errors = res.Errors });
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await userManager.FindByEmailAsync(loginDto.UserName);
        if (user != null)
        {
            var isCorrect = await userManager.CheckPasswordAsync(user, loginDto.Password);

            if (isCorrect)
            {
                // Get roles
                var roles = await userManager.GetRolesAsync(user);
                if (roles == null || !roles.Any()) return BadRequest(new { message = "User has no roles assigned" });

                // Create JWT token
                var jwt = tokenRepository.CreateJWTToken(user, roles.ToList());
                var response = new LoginResponseDto
                {
                    token = jwt
                };
                return Ok(response);


                // return Ok(new { message = "Login successful" , token = token });
            }
        }
        return BadRequest(new { message = "Incorrect email or password!" });

    }
}