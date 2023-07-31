using GameMasterArena.Service.Dtos.Persons;
using GameMasterArena.Service.Dtos.PersonsAuth;
using GameMasterArena.Service.Interfaces.Persons;
using GameMasterArena.Service.Validators.Dtos.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameMasterArena.Api.Controller;

[Route("api/StudentAuth")]
[ApiController]
public class PersonAuthController : ControllerBase
{
    private readonly IAuthPersonService _authService;

    public PersonAuthController(IAuthPersonService auth)
    {
        this._authService  = auth;
    }


    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterAsync([FromForm] PersonRegisterDto registerDto)
    {
        var validator = new PersonRegisterValidator();
        var result = validator.Validate(registerDto);
        if (result.IsValid)
        {
            var serviceResult = await _authService.RegisterAsync(registerDto);
            return Ok(new { serviceResult.Result, serviceResult.CashedMinutes });
        }
        else return BadRequest(result.Errors);
    }


    [HttpPost("register/send-code")]
    [AllowAnonymous]
    public async Task<IActionResult> SendCodeRegisterAsync(string mail)
    {
       var serviceResult = await _authService.SendCodeForRegisterAsync(mail);
        return Ok(new { serviceResult.Result, serviceResult.CashedVerificationMinutes });
    }
    

    [HttpPost("register/verify")]
    [AllowAnonymous]
    public async Task<IActionResult> VerifyRegisterAsync([FromBody] PersonVerifyDto verifyDto )
    {
        var servisResult = await _authService.VerifyRegisterAsync(verifyDto.Email, verifyDto.Code);
        return Ok(new { servisResult.Result, servisResult.Token });
    }


    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync([FromBody] PersonLoginDto logindto)
    {
        var validator = new PersonLoginValidator();
        var valResult = validator.Validate(logindto);
        if (valResult.IsValid == false) return BadRequest(valResult.Errors);

        var serviceResult = await _authService.LoginAsync(logindto);
        return Ok(new { serviceResult.Result, serviceResult.Token });
    }
}
