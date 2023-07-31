using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Service.Dtos.Teams;

public class TeamGetAllDto
{
    [MaxLength(25)]
    public string Name { get; set; } = string.Empty;


    public string Image { get; set; } = string.Empty;

    public string Leader { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
