using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Service.Dtos.Teams;

public class TeamCreateDto
{
    public string Name { get; set; } = string.Empty;

    public IFormFile Image { get; set; } = default!;

    public string Description { get; set; } = string.Empty;
}
