using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Domain.Entities.Tournaments;

public class Tournament: Auditable
{
    public string Name { get; set; } = string.Empty;

    public int Reward { get; set; }

    public DateTime Start_at { get; set; }

    public string Image { get; set; } = string.Empty;

}
