using GameMasterArena.DataAccess.Interfaces.Persons;
using GameMasterArena.DataAccess.Interfaces.Teams;
using GameMasterArena.DataAccess.Utils;
using GameMasterArena.DataAccess.ViewModels.Persons;
using GameMasterArena.DataAccess.ViewModels.Teams;
using GameMasterArena.Domain.Exceptions.Files;
using GameMasterArena.Domain.Exceptions.Teams;
using GameMasterArena.Service.Common.Helpers;
using GameMasterArena.Service.Dtos.Persons;
using GameMasterArena.Service.Interfaces.Auth;
using GameMasterArena.Service.Interfaces.Common;
using GameMasterArena.Service.Interfaces.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Service.Services.Persons;

public class PersonService : IPersonService
{

    private readonly IPerson _repository;
    private readonly IFileService _fileService;
    private readonly IIdentityService _identity;

    public PersonService(IPerson team, IFileService fileService, IIdentityService identityService)
    {
        this._repository = team;
        this._fileService = fileService;
        this._identity = identityService;
    }

    public async Task<long> CountAsync()
        => await _repository.CountAsync();


    public async Task<IList<PersonViewModel>> GetAllAsync(PaginationParams @params)
    {
        var teams = await _repository.GetAllAsync(@params);
        return (IList<PersonViewModel>)teams;
    }

    public async Task<PersonViewModel> GetByIdAsync(long teamId)
    {
        var teams = await _repository.GetByIdAsync(teamId);
        return teams;
    }

    public async Task<bool> UpdateAsync(PersonUpdateDto dto)
    {
        var team = await _repository.GetByIdAsync(_identity.Id);
        if (team is null) throw new TeamNotFoundException();

        
        team.FirstName = dto.FirstName;
        team.LastName = dto.LastName;
        team.Description = dto.Description;

        if (dto.Image is not null)
        {
            // delete old image
            //var deleteResult = await _fileService.DeleteImageAsync(team.Image);
            //if (deleteResult is false) throw new ImageNotFoundException();

            
            string newImagePath = await _fileService.UploadImageAsync(dto.Image);

            
            team.Image = newImagePath;
        }
        

        team.UpdateAt = TimeHelper.GetDateTime();

        var dbResult = await _repository.UpdateAsync(_identity.Id, team);
        return dbResult > 0;
    }
}