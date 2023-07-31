using GameMasterArena.DataAccess.Interfaces.ParticipatingTeams;
using GameMasterArena.DataAccess.Interfaces.TeamMembers;
using GameMasterArena.DataAccess.Utils;
using GameMasterArena.DataAccess.ViewModels.ParticipatingTeams;
using GameMasterArena.DataAccess.ViewModels.TeamMembers;
using GameMasterArena.Domain.Entities.ParticipatingTeams;
using GameMasterArena.Domain.Entities.TeamMembers;
using GameMasterArena.Domain.Exceptions.ParticipatingTeams;
using GameMasterArena.Service.Dtos.ParticipatingTeams;
using GameMasterArena.Service.Dtos.TeamMembers;
using GameMasterArena.Service.Interfaces.Auth;
using GameMasterArena.Service.Interfaces.Common;
using GameMasterArena.Service.Interfaces.ParticipatingTeamsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Service.Services.ParticipatingTeamsSevice;

public class ParticipatingTeamService: Interfaces.ParticipatingTeamsService.IParticipatingTeamService
{
    private readonly IParticipatingTeam _repository;
    private readonly IFileService _fileService;
    private readonly IIdentityService _identity;

    public ParticipatingTeamService(IParticipatingTeam participatingTeam, IFileService fileService, IIdentityService identity)
    {
        this._repository = participatingTeam;
        this._fileService = fileService;
        this._identity = identity;
    }

    public async Task<bool> CreateAsync(ParticipatingTeamCreateDto dto)
    {
        ParticipatingTeam team = new ParticipatingTeam()
        {
            Tournament_id = dto.Tournament_id,
            Team_id = dto.Team_id,
            Description = dto.Description
        };

        var result = await _repository.CreateAsync(team, _identity.Id);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long teamId)
    {
        var team = await _repository.GetByTeamIdAsync(teamId);
        if (team is null) throw new ParticipatingTeamNotFoundException();

        var dbResult = await _repository.DeleteAsync(teamId);
        return dbResult > 0;
    }

    public async Task<IList<ParticipatingTeamsViewModel>> GetAllAsync(PaginationParams @params)
    {
        var teams = await _repository.GetAllAsync(@params);
        return (IList<ParticipatingTeamsViewModel>)teams;
    }

    public async Task<IList<ParticipatingTeamsViewModel>> GetByTeamIdAsync(long teamId)
    {
        var teams = await _repository.GetByTeamIdAsync(teamId);
        return (IList<ParticipatingTeamsViewModel>)teams;
    }

    public async Task<IList<ParticipatingTeamsViewModel>> GetByTournamentIdAsync(long teamId)
    {
        var teams = await _repository.GetByTournamentIdAsync(teamId);
        return (IList<ParticipatingTeamsViewModel>)teams;
    }
}
