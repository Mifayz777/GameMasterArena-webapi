using Dapper;
using GameMasterArena.DataAccess.Interfaces.ParticipatingTeams;
using GameMasterArena.DataAccess.Utils;
using GameMasterArena.DataAccess.ViewModels.ParticipatingTeams;
using GameMasterArena.DataAccess.ViewModels.TeamMembers;
using GameMasterArena.Domain.Entities.ParticipatingTeams;
using GameMasterArena.Domain.Entities.Persons;
using GameMasterArena.Domain.Entities.Teams;
using GameMasterArena.Domain.Entities.Tournaments;
using GameMasterArena.Domain.Exceptions.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.DataAccess.Repositories.ParticipatingTeams;

public class ParticipatingTeamsRepasitory : BaseRepasitory, IParticipatingTeam

{
    public async Task<Team> CheckLeader(long leaderId, long teamId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "select * from team " +
                $"where id = {teamId} and person_id = {leaderId}";
            var result = await _connection.QuerySingleAsync<Team>(query);
            return result;
        }
        catch
        {
            return null;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> CreateAsync(ParticipatingTeam entity, long leaderId)
    {
        try
        {
            var check = await CheckLeader(leaderId, entity.Team_id);
            if (check != null)
            {
                await _connection.OpenAsync();
                string query = "INSERT INTO public.participating_teams(tournament_id, team_id, description) " +
                    "VALUES (@Tournament_id, @Team_id, @Description);";
                var result = await _connection.ExecuteAsync(query, entity);
                return result;
            }
            else
            {
                return 0;
            }
            
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }


    public async Task<int> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "DELETE FROM public.participating_teams WHERE team_id = @Id;";
            var result = await _connection.ExecuteAsync(query, new { Id = id });
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<ParticipatingTeamsViewModel>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM public.participatingteamsviewmodel " +
                $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<ParticipatingTeamsViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<ParticipatingTeamsViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<ParticipatingTeamsViewModel>> GetByTeamIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM participatingteamsviewmodel where team_id = {id}";
            var result = await _connection.QueryAsync<ParticipatingTeamsViewModel>(query);
            return (IList<ParticipatingTeamsViewModel>)result;
        }
        catch
        {
            return null;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<ParticipatingTeamsViewModel>> GetByTournamentIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM participatingteamsviewmodel where tournament_id = {id}";
            var result = await _connection.QueryAsync<ParticipatingTeamsViewModel>(query);
            return (IList<ParticipatingTeamsViewModel>)result;
        }
        catch
        {
            return null;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public Task<(int ItemsCount, IList<ParticipatingTeamsViewModel>)> SearchAsync(string search, PaginationParams @params)
    {
        throw new NotImplementedException();
    }
}
