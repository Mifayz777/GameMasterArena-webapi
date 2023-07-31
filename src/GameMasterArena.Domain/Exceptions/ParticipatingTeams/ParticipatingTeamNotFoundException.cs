using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Domain.Exceptions.ParticipatingTeams;

public class ParticipatingTeamNotFoundException: NotFoundException
{
    public ParticipatingTeamNotFoundException()
    {
        this.TitleMessage = "Company not found";
    }
}
