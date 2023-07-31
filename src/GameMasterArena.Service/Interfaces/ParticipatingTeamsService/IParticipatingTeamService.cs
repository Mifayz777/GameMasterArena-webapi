using GameMasterArena.DataAccess.Utils;
using GameMasterArena.DataAccess.ViewModels.ParticipatingTeams;
using GameMasterArena.Service.Dtos.ParticipatingTeams;
using GameMasterArena.Service.Dtos.TeamMembers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Service.Interfaces.ParticipatingTeamsService;

public interface IParticipatingTeamService
{
    public Task<bool> CreateAsync(ParticipatingTeamCreateDto dto);
    
    public Task<bool> DeleteAsync(long teamId);

    public Task<IList<ParticipatingTeamsViewModel>> GetAllAsync(PaginationParams @params);
    public Task<IList<ParticipatingTeamsViewModel>> GetByTeamIdAsync(long teamId);
    public Task<IList<ParticipatingTeamsViewModel>> GetByTournamentIdAsync(long teamId);

}
