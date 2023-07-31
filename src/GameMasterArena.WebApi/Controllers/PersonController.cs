using GameMasterArena.DataAccess.Utils;
using GameMasterArena.Service.Dtos.Persons;
using GameMasterArena.Service.Dtos.Teams;
using GameMasterArena.Service.Interfaces.Persons;
using GameMasterArena.Service.Interfaces.TeamServices;
using GameMasterArena.Service.Validators.Dtos.Students;
using GameMasterArena.Service.Validators.Dtos.Teams;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameMasterArena.WebApi.Controllers;

[Route("api/Person")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IPersonService _service;
    private readonly int maxPageSize = 30;

    public PersonController(IPersonService service)
    {
        this._service = service;
    }


    [HttpGet("All")]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("GetById")]
    public async Task<IActionResult> GetByIdAsync([FromQuery] long id)
     => Ok(await _service.GetByIdAsync(id));


    [HttpPut("PersonUpdate")]
    public async Task<IActionResult> UpdateAsync([FromForm] PersonUpdateDto dto)
    {
        var updateValidator = new PersonUpdateValidator();
        var result = updateValidator.Validate(dto);
        if (result.IsValid) return Ok(await _service.UpdateAsync(dto));
        else return BadRequest(result.Errors);
    }

}
