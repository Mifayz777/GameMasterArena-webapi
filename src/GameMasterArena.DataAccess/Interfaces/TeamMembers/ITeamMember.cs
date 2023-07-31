using GameMasterArena.DataAccess.Common.Interfaces;
using GameMasterArena.DataAccess.ViewModels.ParticipatingTeams;
using GameMasterArena.DataAccess.ViewModels.TeamMembers;
using GameMasterArena.Domain.Entities.TeamMembers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.DataAccess.Interfaces.TeamMembers;

public interface ITeamMember: IGetAll<TeamMemberViewModel>, ISearchable<TeamMemberViewModel>
{
    public Task<int> DeleteAsync(long personId, long teamId, long leaderId);

    public Task<int> DeleteTeamAsync(long id);

    public Task<int> CreateAsync(TeamMember entity);

    public Task<IList<TeamMemberViewModel>> GetByTeamIdAsync(long id);

    public Task<TeamMemberViewModel> GetByPersonIdAsync(long id);

    public Task<bool> CheckPerson(long id);

    public Task<long> CountAsync(long teamId);
}
