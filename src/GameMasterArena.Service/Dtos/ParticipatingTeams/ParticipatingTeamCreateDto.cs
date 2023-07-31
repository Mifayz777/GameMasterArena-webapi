using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Service.Dtos.ParticipatingTeams;

public class ParticipatingTeamCreateDto
{
    public long Tournament_id { get; set; }

    public long Team_id { get; set; }

    public string Description { get; set; } = string.Empty;
}
