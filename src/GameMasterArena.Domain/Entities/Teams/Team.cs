using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Domain.Entities.Teams;

public class Team: Auditable
{
    public long Person_id { get; set; }

    [MaxLength(25)]
    public string Name { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;

}
