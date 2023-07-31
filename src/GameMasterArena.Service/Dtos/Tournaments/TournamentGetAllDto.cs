using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Service.Dtos.Tournaments;

public class TournamentGetAllDto
{
    public string Name { get; set; } = string.Empty;

    public int Reward { get; set; }

    public DateTime Start_at { get; set; }

    public string Image { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime Create_at { get; set; }
    
    public DateTime Upddate_at { get; set; }

}
