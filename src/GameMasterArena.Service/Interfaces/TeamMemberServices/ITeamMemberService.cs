using GameMasterArena.DataAccess.Utils;
using GameMasterArena.DataAccess.ViewModels.TeamMembers;
using GameMasterArena.DataAccess.ViewModels.Teams;
using GameMasterArena.Service.Dtos.TeamMembers;
using GameMasterArena.Service.Dtos.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Service.Interfaces.TeamMemberServices;

public interface ITeamMemberService
{
    public Task<bool> CreateAsync(ParticipatingTeamDto dto);

    public Task<bool> DeleteAsync(long personId, long teamId);

    public Task<IList<TeamMemberViewModel>> GetAllAsync(PaginationParams @params);

    public Task<IList<TeamMemberViewModel>> GetByTeamIdAsync(long teamId);

    public Task<TeamMemberViewModel> GetByPersonIdAsync(long personId);
    
    public Task<long> CountAsync(long teamId);
}
