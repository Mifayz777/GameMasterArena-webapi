using GameMasterArena.DataAccess.Utils;
using GameMasterArena.Service.Dtos.TeamMembers;
using GameMasterArena.Service.Dtos.Tournaments;
using GameMasterArena.Service.Interfaces.TeamMemberServices;
using GameMasterArena.Service.Interfaces.TeamServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameMasterArena.WebApi.Controllers;

[Route("api/TeamMember")]
[ApiController]
public class TeamMemberController : ControllerBase
{
    private readonly ITeamMemberService _service;
    private readonly int maxPageSize = 30;

    public TeamMemberController(ITeamMemberService service)
    {
        this._service = service;
    }

    [HttpPost("Create")]
    public async void CreateAsync([FromForm] ParticipatingTeamDto dto)
     => Ok(await _service.CreateAsync(dto));


    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));


    [HttpDelete("TeamId")]
    public async Task<IActionResult> DeleteAsync(long tournamentId, long teamId)
        => Ok(await _service.DeleteAsync(tournamentId, teamId));



    [HttpGet("GetByTeamId")]
    public async Task<IActionResult> GetByIdAsync([FromQuery] long teamId)
        => Ok(await _service.GetByTeamIdAsync(teamId));


    [HttpGet("GetByPeronId")]
    public async Task<IActionResult> GetByPersonIdAsync([FromQuery] long personId)
    => Ok(await _service.GetByPersonIdAsync(personId));


    [HttpGet("count")]
    public async Task<IActionResult> CountAsync(long teamId)
        => Ok(await _service.CountAsync(teamId));






}
