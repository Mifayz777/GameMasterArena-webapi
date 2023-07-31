using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Service.Dtos.Tournaments;

public class TournamentParticipationDto
{
    public long Team_id { get; set; }

    public long Tournament_id { get; set; }
}
