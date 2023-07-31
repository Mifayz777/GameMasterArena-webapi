using FluentValidation;
using GameMasterArena.DataAccess.Interfaces.Teams;
using GameMasterArena.DataAccess.Repositories.Teams;
using GameMasterArena.DataAccess.Utils;
using GameMasterArena.Domain.Entities.Teams;
using GameMasterArena.Service.Common.Helpers;
using GameMasterArena.Service.Dtos.Teams;
using GameMasterArena.Service.Interfaces.TeamServices;
using GameMasterArena.Service.Validators.Dtos.Teams;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameMasterArena.WebApi.Controllers;

[Route("api/Team")]
[ApiController]
public class TeamController : ControllerBase
{
    private readonly ITeamService _service;
    private readonly int maxPageSize = 30;

    public TeamController(ITeamService service)
    {
        this._service = service;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateAsync([FromForm] TeamCreateDto dto)
    {
        var createValidator = new TeamCreateValidator();
        var result = createValidator.Validate(dto);
        if (result.IsValid) return Ok(await _service.CreateAsync(dto)); 
        else return BadRequest(result.Errors);
    }


    [HttpGet("All")]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));


    [HttpDelete("TeamId")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await _service.DeleteAsync(id));



    [HttpGet("GetById")]
    public async Task<IActionResult> GetByIdAsync([FromQuery] long id)
        => Ok(await _service.GetByIdAsync(id));


    [HttpPut("TeamUpdateId")]
    public async Task<IActionResult> UpdateAsync(long Id, [FromForm] TeamUpdateDto dto)
    {
        var updateValidator = new TeamUpdateValidator();
        var result = updateValidator.Validate(dto);
        if (result.IsValid) return Ok(await _service.UpdateAsync(Id, dto));
        else return BadRequest(result.Errors);
    }
       


    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _service.CountAsync());
}
