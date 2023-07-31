using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Service.Dtos.Tournaments;

public class TournamentUpdateDto
{
    public string Name { get; set; } = string.Empty;

    public int Reward { get; set; }

    public DateTime Start_at { get; set; }

    public IFormFile Image { get; set; } = default!;

    public string Description { get; set; } = string.Empty;
}
