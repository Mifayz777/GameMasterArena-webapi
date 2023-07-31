using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Domain.Exceptions.Teams;

public class TeamNotFoundException: NotFoundException
{
    public TeamNotFoundException()
    {
        this.TitleMessage = "Company not found";
    }
}
