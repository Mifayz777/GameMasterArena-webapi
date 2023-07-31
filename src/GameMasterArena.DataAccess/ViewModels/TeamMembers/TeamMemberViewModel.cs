using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.DataAccess.ViewModels.TeamMembers;

public class TeamMemberViewModel
{

    public string TeamName { get; set; } = string.Empty;

    public string PersonName { get; set; } = string.Empty;
    
    public DateTime CreateAt { get; set; }

    public DateTime UpdateAt { get; set; }
}
