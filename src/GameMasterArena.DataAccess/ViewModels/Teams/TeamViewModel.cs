using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.DataAccess.ViewModels.Teams;

public class TeamViewModel
{

    public int TeamId { get; set; }

    [MaxLength(25)]
    public string Name { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;

    public string Leader { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreateAt { get; set; }

    public DateTime UpdateAt { get; set; }
}
