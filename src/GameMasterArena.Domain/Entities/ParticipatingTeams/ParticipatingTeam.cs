using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Domain.Entities.ParticipatingTeams;

public class ParticipatingTeam:Auditable
{
    public long Tournament_id { get; set; }

    public long Team_id { get; set; }
}
