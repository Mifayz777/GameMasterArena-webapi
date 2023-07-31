using GameMasterArena.DataAccess.Utils;
using GameMasterArena.DataAccess.ViewModels.Teams;
using GameMasterArena.Domain.Entities.Teams;
using GameMasterArena.Service.Dtos.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Service.Interfaces.TeamServices;

public interface ITeamService
{
    public Task<bool> CreateAsync(TeamCreateDto dto);

    public Task<bool> DeleteAsync(long teamId);

    public Task<IList<TeamViewModel>> GetAllAsync(PaginationParams @params);

    public Task<TeamViewModel> GetByIdAsync(long teamId);

    public Task<bool> UpdateAsync(long teamId, TeamUpdateDto dto);

    public Task<long> CountAsync();
}
