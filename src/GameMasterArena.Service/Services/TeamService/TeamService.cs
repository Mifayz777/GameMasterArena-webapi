using GameMasterArena.DataAccess.Interfaces.Teams;
using GameMasterArena.DataAccess.Repositories.Teams;
using GameMasterArena.DataAccess.Utils;
using GameMasterArena.DataAccess.ViewModels.Teams;
using GameMasterArena.Domain.Entities.Teams;
using GameMasterArena.Domain.Exceptions.Files;
using GameMasterArena.Domain.Exceptions.Teams;
using GameMasterArena.Service.Common.Helpers;
using GameMasterArena.Service.Dtos.Teams;
using GameMasterArena.Service.Interfaces.Auth;
using GameMasterArena.Service.Interfaces.Common;
using GameMasterArena.Service.Interfaces.TeamServices;
using GameMasterArena.Service.Services.Auth;
using GameMasterArena.Service.Services.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamViewModel = GameMasterArena.DataAccess.ViewModels.Teams.TeamViewModel;

namespace GameMasterArena.Service.Services.TeamService;

public class TeamService : ITeamService
{
    private readonly ITeam _repository;
    private readonly IFileService _fileService;
    private readonly IIdentityService _identity;

    public TeamService(ITeam team, IFileService fileService, IIdentityService identityService) 
    {
        this._repository = team;
        this._fileService = fileService;
        this._identity = identityService;
    }

    public async Task<long> CountAsync()
        => await _repository.CountAsync();

    public async Task<bool> CreateAsync(TeamCreateDto dto)
    {
        string imagepath = await _fileService.UploadImageAsync(dto.Image);
        Team team = new Team()
        {
            Person_id = _identity.Id,
            Name = dto.Name,
            Image = imagepath,
            Description = dto.Description,
        };
        var result = await _repository.CreateAsync(team);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var team = await _repository.GetByIdAsync(id);
        if (team is null) throw new TeamNotFoundException();

        var result = await _fileService.DeleteImageAsync(team.Image);
        if (result == false) throw new ImageNotFoundException();

        var dbResult = await _repository.DeleteAsync(id);
        return dbResult > 0;
    }

    public async Task<IList<TeamViewModel>> GetAllAsync(PaginationParams @params)
    {
        var teams = await _repository.GetAllAsync(@params);
        return (IList<TeamViewModel>)teams;
    }

    public async Task<TeamViewModel> GetByIdAsync(long teamId)
    {
        var teams = await _repository.GetByIdAsync(teamId);
        return teams;
    }

    public async Task<bool> UpdateAsync(long teamId, TeamUpdateDto dto)
    {
        var team = await _repository.GetByIdAsync(teamId);
        if (team is null) throw new TeamNotFoundException();

        
        team.Name = dto.Name;
        team.Description = dto.Description;

        if (dto.Image is not null)
        {
            
            var deleteResult = await _fileService.DeleteImageAsync(team.Image);
            if (deleteResult is false) throw new ImageNotFoundException();

            
            string newImagePath = await _fileService.UploadImageAsync(dto.Image);

            team.Image = newImagePath;
        }


        team.UpdateAt = TimeHelper.GetDateTime();

        var dbResult = await _repository.UpdateAsync(teamId, team, _identity.Id);
        return dbResult > 0;
    }

    
}
