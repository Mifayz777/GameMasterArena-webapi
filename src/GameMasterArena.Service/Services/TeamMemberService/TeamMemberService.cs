using GameMasterArena.DataAccess.Interfaces.TeamMembers;
using GameMasterArena.DataAccess.Interfaces.Teams;
using GameMasterArena.DataAccess.Utils;
using GameMasterArena.DataAccess.ViewModels.TeamMembers;
using GameMasterArena.DataAccess.ViewModels.Teams;
using GameMasterArena.Domain.Entities.TeamMembers;
using GameMasterArena.Domain.Entities.Teams;
using GameMasterArena.Domain.Exceptions.TeamMembers;
using GameMasterArena.Service.Dtos.TeamMembers;
using GameMasterArena.Service.Interfaces.Auth;
using GameMasterArena.Service.Interfaces.Common;
using GameMasterArena.Service.Interfaces.TeamMemberServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Service.Services.TeamMemberService;

public class TeamMemberService : ITeamMemberService
{

    private readonly ITeamMember _repository;
    private readonly IFileService _fileService;
    private readonly IIdentityService _identityService;

    public TeamMemberService (ITeamMember teamMemberService, IFileService fileService, IIdentityService identityService)
    {
        this._repository = teamMemberService;
        this._fileService = fileService;
        this._identityService = identityService;
    }

    public async Task<long> CountAsync(long teamId)
        => await _repository.CountAsync(teamId);

    public async Task<bool> CreateAsync(ParticipatingTeamDto dto)
    {
        TeamMember team = new TeamMember()
        {
            Person_id = _identityService.Id,
            Team_id = dto.Team_id,
            Description = dto.Description
        };
        var result = await _repository.CreateAsync(team);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long personId, long teamId)
    {
        var team = await _repository.GetByTeamIdAsync(teamId);
        if (team is null) throw new TeamMemberNotFoundExcaption();

        var dbResult = await _repository.DeleteAsync(personId, teamId, _identityService.Id);
        return dbResult > 0;
    }

    public async Task<IList<TeamMemberViewModel>> GetAllAsync(PaginationParams @params)
    {
        var teams = await _repository.GetAllAsync(@params);
        return (IList<TeamMemberViewModel>)teams;
    }

    public async Task<IList<TeamMemberViewModel>> GetByTeamIdAsync(long teamId)
    {
        var teams = await _repository.GetByTeamIdAsync(teamId);
        return (IList<TeamMemberViewModel>)teams;
    }

    public async Task<TeamMemberViewModel> GetByPersonIdAsync(long personId)
    {
        var teams = await _repository.GetByPersonIdAsync(personId);
        return teams;
    }
}
