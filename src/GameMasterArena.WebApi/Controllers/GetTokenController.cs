using GameMasterArena.Service.Interfaces.Auth;
using GameMasterArena.Service.Interfaces.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameMasterArena.WebApi.Controllers;

[Route("api/GetToken")]
[ApiController]
public class GetTokenController : ControllerBase
{
    private readonly IMailSender _smsSender;
    private readonly IIdentityService _identity;

    public GetTokenController(IMailSender smsSender, IIdentityService identityService)
    {
        this._smsSender = smsSender;
        this._identity = identityService;
    }

    [HttpGet("Token")]
    public Task<IActionResult> GetAsync()
    {
        return Task.FromResult<IActionResult>(
        Ok(new
        {
            _identity.Id,
            _identity.FirstName,
            _identity.LastName,
            _identity.Email
        }
        ));
    }



}
