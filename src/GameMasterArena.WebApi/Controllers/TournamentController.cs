using FluentValidation;
using GameMasterArena.DataAccess.Interfaces.Tournaments;
using GameMasterArena.DataAccess.Utils;
using GameMasterArena.Service.Dtos.Teams;
using GameMasterArena.Service.Dtos.Tournaments;
using GameMasterArena.Service.Interfaces.TeamServices;
using GameMasterArena.Service.Interfaces.TournamentServices;
using GameMasterArena.Service.Validators.Dtos.Teams;
using GameMasterArena.Service.Validators.Dtos.Tournaments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameMasterArena.WebApi.Controllers;

[Route("api/Tournament")]
[ApiController]
public class TournamentController : ControllerBase
{

    private readonly ITournamentService _service;
    private readonly int maxPageSize = 30;

    public TournamentController(ITournamentService service)
    {
        this._service = service;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateAsync([FromForm] TournamentCreateDto dto)
    {
        var createValidator = new TournamentCreateValidator();
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
    public async Task<IActionResult> UpdateAsync(long Id, [FromForm] TournamentUpdateDto dto)
    {
        var updateValidator = new TournamentUpdateValidator();
        var result = updateValidator.Validate(dto);
        if (result.IsValid) return  Ok(await _service.UpdateAsync(Id, dto));
        else return BadRequest(result.Errors);
    }
       


    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _service.CountAsync());

}
