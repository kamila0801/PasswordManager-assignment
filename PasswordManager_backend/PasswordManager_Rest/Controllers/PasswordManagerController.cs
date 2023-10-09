using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordManager_Main.IService;
using PasswordManager_Main.Models;
using PasswordManager_Rest.Dto;
using PasswordManager_Security.IService;

namespace PasswordManager_Rest.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize]
public class PasswordManagerController : ControllerBase
{
    private readonly IPasswordUnitService _passwordUnitService;
    private readonly IAuthUserService _authUserService;

    public PasswordManagerController(IPasswordUnitService passwordUnitService)
    {
        _passwordUnitService = passwordUnitService;
        
    }

    [HttpPost]
    public IActionResult AddPasswordUnit([FromBody] PasswordUnit passwordUnit)
    {
        _passwordUnitService.AddPasswordUnit(passwordUnit);
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetPasswordUnitById(int id, [FromQuery] string masterPassword)
    {
        PasswordUnit passwordUnit = _passwordUnitService.GetPasswordUnitById(id, masterPassword);
        if (passwordUnit == null)
        {
            return NotFound();
        }
        return Ok(passwordUnit);
    }

    [HttpPost(nameof(GetAllPasswordUnits))]
    public async Task<IActionResult> GetAllPasswordUnits(LoginDto dto)
    {
        var test = dto;
        IEnumerable<PasswordUnit> passwordUnits = _passwordUnitService.GetAllPasswordUnits(dto.Username, dto.Password);
        return Ok(passwordUnits);
    }

    [HttpPut]
    public IActionResult UpdatePasswordUnit([FromBody] PasswordUnit passwordUnit, [FromQuery] string masterPassword)
    {
        _passwordUnitService.UpdatePasswordUnit(passwordUnit, masterPassword);
        return Ok();
    }

    [HttpPost(nameof(DeletePasswordUnit))]
    public IActionResult DeletePasswordUnit([FromBody] PasswordUnit passwordUnit)
    {
        _passwordUnitService.DeletePasswordUnit(passwordUnit);
        return Ok();
    }
}