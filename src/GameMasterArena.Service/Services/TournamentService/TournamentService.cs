using GameMasterArena.DataAccess.Interfaces.Teams;
using GameMasterArena.DataAccess.Interfaces.Tournaments;
using GameMasterArena.DataAccess.Utils;
using GameMasterArena.DataAccess.ViewModels.Teams;
using GameMasterArena.Domain.Entities.Teams;
using GameMasterArena.Domain.Entities.Tournaments;
using GameMasterArena.Domain.Exceptions.Files;
using GameMasterArena.Domain.Exceptions.Tournaments;
using GameMasterArena.Service.Common.Helpers;
using GameMasterArena.Service.Dtos.Tournaments;
using GameMasterArena.Service.Interfaces.Common;
using GameMasterArena.Service.Interfaces.TournamentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Service.Services.TournamentService;

public class TournamentService: ITournamentService
{
    private readonly ITournament _repository;
    private readonly IFileService _fileService;

    public TournamentService(ITournament tournament, IFileService fileService)
    {
        this._repository = tournament;
        this._fileService = fileService;
    }

    public async Task<long> CountAsync()
       => await _repository.CountAsync();

    public async Task<bool> CreateAsync(TournamentCreateDto dto)
    {
        string imagepath = await _fileService.UploadImageAsync(dto.Image);
        Tournament tournament = new Tournament()
        {
            Name = dto.Name,
            Image = imagepath,
            Start_at = dto.Start_at,
            Reward = dto.Reward,
            Description = dto.Description,
        };
        var result = await _repository.CreateAsync(tournament);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long tournamentId)
    {
        var tournament = await _repository.GetByIdAsync(tournamentId);
        if (tournament is null) throw new TournamentNotFoundException();

        var result = await _fileService.DeleteImageAsync(tournament.Image);
        if (result == false) throw new ImageNotFoundException();

        var dbResult = await _repository.DeleteAsync(tournamentId);
        return dbResult > 0;
    }

    public async Task<IList<Tournament>> GetAllAsync(PaginationParams @params)
    {
        var tournaments = await _repository.GetAllAsync(@params);
        return (IList<Tournament>)tournaments;
    }

    public async Task<Tournament> GetByIdAsync(long tournamentId)
    {
        var tournament = await _repository.GetByIdAsync(tournamentId);
        return tournament;
    }

    public async Task<bool> UpdateAsync(long tournamentId, TournamentUpdateDto dto)
    {
        var tournament = await _repository.GetByIdAsync(tournamentId);
        if (tournament is null) throw new TournamentNotFoundException();

        
        tournament.Name = dto.Name;
        tournament.Description = dto.Description;
        tournament.Reward = dto.Reward;
        tournament.Start_at = dto.Start_at;

        if (dto.Image is not null)
        {
           
            var deleteResult = await _fileService.DeleteImageAsync(tournament.Image);
            if (deleteResult is false) throw new ImageNotFoundException();

           
            string newImagePath = await _fileService.UploadImageAsync(dto.Image);

           
            tournament.Image = newImagePath;
        }

        var dbResult = await _repository.UpdateAsync(tournamentId, tournament);
        return dbResult > 0;
    }

    public Task<int> ParticipationAsync(TournamentParticipationDto dto)
    {
        throw new NotImplementedException();
    }

}
