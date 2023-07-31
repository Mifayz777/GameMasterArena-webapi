using GameMasterArena.DataAccess.Utils;
using GameMasterArena.DataAccess.ViewModels.Teams;
using GameMasterArena.Domain.Entities.ParticipatingTeams;
using GameMasterArena.Domain.Entities.Tournaments;
using GameMasterArena.Service.Dtos.Teams;
using GameMasterArena.Service.Dtos.Tournaments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Service.Interfaces.TournamentServices;

public interface ITournamentService
{
    public Task<bool> CreateAsync(TournamentCreateDto dto);

    public Task<bool> DeleteAsync(long tournamentId);

    public Task<IList<Tournament>> GetAllAsync(PaginationParams @params);

    public Task<Tournament> GetByIdAsync(long teamId);

    public Task<bool> UpdateAsync(long teamId, TournamentUpdateDto dto);

    public Task<long> CountAsync();

    public  Task<int> ParticipationAsync(TournamentParticipationDto dto);
}
