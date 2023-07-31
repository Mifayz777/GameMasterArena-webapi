using GameMasterArena.DataAccess.Common.Interfaces;
using GameMasterArena.DataAccess.ViewModels.Teams;
using GameMasterArena.Domain.Entities.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using TeamViewModel = GameMasterArena.DataAccess.ViewModels.Teams.TeamViewModel;

namespace GameMasterArena.DataAccess.Interfaces.Teams;

public interface ITeam:  IGetAll<TeamViewModel>, ISearchable<TeamViewModel>
{
    public Task<long> CountAsync();
    public Task<int> CreateAsync(Team entity);

    public Task<int> UpdateAsync(long id, TeamViewModel entity, long leaderId);

    public Task<int> DeleteAsync(long id);

    public Task<TeamViewModel?> GetByIdAsync(long id);
    public Task<bool> CheckAsync(long teamId, long person_id);
    public Task<bool> CheckLeader(long personId);
    public Task<bool> CheckName(string name);


}
