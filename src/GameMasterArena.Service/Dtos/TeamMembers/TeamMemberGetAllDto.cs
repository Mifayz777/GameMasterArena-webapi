using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Service.Dtos.TeamMembers;

public class TeamMemberGetAllDto
{
    public string TeamName { get; set; } = string.Empty;

    public string TournamentName { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdateAt { get; set; }
}
