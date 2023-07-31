using GameMasterArena.DataAccess.Utils;
using GameMasterArena.Service.Dtos.ParticipatingTeams;
using GameMasterArena.Service.Interfaces.Auth;
using GameMasterArena.Service.Interfaces.ParticipatingTeamsService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameMasterArena.WebApi.Controllers;

[Route("api/ParticipatingTeam")]
[ApiController]
public class ParticipatingTeamController : ControllerBase
{

    private readonly IParticipatingTeamService _service;
    private readonly IIdentityService _identity;

    private readonly int maxPageSize = 30;

    public ParticipatingTeamController(IParticipatingTeamService service, IIdentityService identityService)
    {
        this._service = service;
        this._identity = identityService;
    }


    [HttpPost("Create")]
    public async void CreateAsync([FromForm] ParticipatingTeamCreateDto dto)
     => Ok(await _service.CreateAsync(dto));


    [HttpGet("All")]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));


    [HttpDelete("TeamId")]
    public async Task<IActionResult> DeleteAsync(long id)
    => Ok(await _service.DeleteAsync(id));


    [HttpGet("GetByTeamId")]
    public async Task<IActionResult> GetByIdAsync([FromQuery] long teamId)
        => Ok(await _service.GetByTeamIdAsync(teamId));


    [HttpGet("GetByTournamentId")]
    public async Task<IActionResult> GetByTournamentIdAsync([FromQuery] long teamId)
    => Ok(await _service.GetByTournamentIdAsync(teamId));

}
