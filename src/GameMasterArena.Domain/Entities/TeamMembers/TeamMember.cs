using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Domain.Entities.TeamMembers;

public class TeamMember: Auditable
{
    public long Person_id { get; set; }
    public long Team_id { get; set; }

}
