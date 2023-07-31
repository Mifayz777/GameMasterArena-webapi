using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Domain.Exceptions.Tournaments;

public class TournamentNotFoundException: NotFoundException
{
    public TournamentNotFoundException()
    {
        this.TitleMessage = "Company not found";
    }
}
