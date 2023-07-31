using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Domain.Exceptions.TeamMembers;

public class TeamMemberNotFoundExcaption: NotFoundException
{
    public TeamMemberNotFoundExcaption()
    {
        this.TitleMessage = "Company not found";
    }
}
