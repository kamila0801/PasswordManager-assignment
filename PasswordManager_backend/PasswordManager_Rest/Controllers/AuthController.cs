using System.Security.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordManager_Rest.Dto;
using PasswordManager_Security.IService;

namespace PasswordManager_Rest.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private ISecurityService _service;
        
    public AuthController(ISecurityService service)
    {
        _service = service;
    }
         
    [AllowAnonymous ]
    [HttpPost(nameof(Login))]
    public ActionResult<TokenDto> Login(LoginDto dto)
    {
        try
        {
            var token =
                _service.GenerateJwtToken(dto.Username, dto.Password);
            return Ok(new TokenDto
            {
                Jwt = token.Jwt,
                Message = token.Message
            });
        }
        catch (AuthenticationException authException)
        {
            return Unauthorized(authException.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Please contact admin, aka horribly wrong, checking actions");
        }
    }

    [HttpPost(nameof(CreateUser))]
    public ActionResult<AuthUserDto> CreateUser([FromBody] CreateAuthUserDto createAuthUserDto)
    {
        try
        {
            var authUser =
                _service.GenerateNewAuthUser(createAuthUserDto.Username, createAuthUserDto.Password);
            return new AuthUserDto
            {
                Id = authUser.Id, 
                Username = authUser.Username
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500, "contact administration");
        }
    }

}  