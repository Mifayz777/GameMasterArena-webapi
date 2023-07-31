using GameMasterArena.DataAccess.Common.Interfaces;
using GameMasterArena.DataAccess.ViewModels.ParticipatingTeams;
using GameMasterArena.DataAccess.ViewModels.Teams;
using GameMasterArena.Domain.Entities.ParticipatingTeams;
using GameMasterArena.Domain.Entities.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.DataAccess.Interfaces.ParticipatingTeams;

public interface IParticipatingTeam: IGetAll<ParticipatingTeamsViewModel>, ISearchable<ParticipatingTeamsViewModel>
{
    public Task<IList<ParticipatingTeamsViewModel>> GetByTeamIdAsync(long id);
    public Task<IList<ParticipatingTeamsViewModel>> GetByTournamentIdAsync(long id);


    public Task<int> DeleteAsync(long id);

    public Task<int> CreateAsync(ParticipatingTeam entity, long leaderId);

    public Task<Team> CheckLeader (long leaderId, long teamId);

}
