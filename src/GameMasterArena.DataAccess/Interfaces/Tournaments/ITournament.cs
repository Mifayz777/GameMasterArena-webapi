using GameMasterArena.DataAccess.Common.Interfaces;
using GameMasterArena.DataAccess.ViewModels.Teams;
using GameMasterArena.Domain.Entities.ParticipatingTeams;
using GameMasterArena.Domain.Entities.Teams;
using GameMasterArena.Domain.Entities.Tournaments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.DataAccess.Interfaces.Tournaments;

public interface ITournament: IRepository<Tournament, Tournament>, IGetAll<Tournament>, ISearchable<Tournament>
{
    //public Task<int> ParticipationAsync(ParticipatingTeam entity);

    public Task<long> CountAsync();
}
