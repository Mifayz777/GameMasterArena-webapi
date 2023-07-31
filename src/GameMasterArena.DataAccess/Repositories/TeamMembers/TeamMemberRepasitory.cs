
using Dapper;
using GameMasterArena.DataAccess.Interfaces.TeamMembers;
using GameMasterArena.DataAccess.Interfaces.Teams;
using GameMasterArena.DataAccess.Repositories.Teams;
using GameMasterArena.DataAccess.Utils;
using GameMasterArena.DataAccess.ViewModels.Persons;
using GameMasterArena.DataAccess.ViewModels.TeamMembers;
using GameMasterArena.Domain.Entities.Persons;
using GameMasterArena.Domain.Entities.TeamMembers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.DataAccess.Repositories.TeamMembers;

public class TeamMemberRepasitory : BaseRepasitory, ITeamMember
{
    public async Task<bool> CheckPerson(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "select count(*) from team_members " +
                $"where person_id = {id}";
            var result = await _connection.QuerySingleAsync<long>(query);
            if (result > 0) return true;
            else return false;
        }
        catch
        {
            return false;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<long> CountAsync(long teamId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select count(*) from team_members where team_id = {teamId}";
            var result = await _connection.QuerySingleAsync<long>(query);
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

    public async Task<int> CreateAsync(TeamMember entity)
    {
        try
        {

            bool check = await CheckPerson(entity.Person_id);
            if(check)
            {
                return 0;
            }
            else
            {
                await _connection.OpenAsync();
                string query = $"select count(*) from team_members where team_id = {entity.Team_id}";
                var count = await _connection.QuerySingleAsync<long>(query);
                if (count < 5)
                {
                    query = "INSERT INTO public.team_members(team_id, person_id, description) " +
                    $"VALUES (@Team_id, @Person_id, @Description);";
                    var result = await _connection.ExecuteAsync(query, entity);
                    return result;
                }
                else
                {
                    return 0;
                }
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

    public async Task<int> DeleteAsync(long personId, long teamId, long leaderId)
    {
        try
        {
            await _connection.OpenAsync();
            ITeam team = new TeamRepasitory();
            var check = await team.CheckAsync(teamId, leaderId);
            if (check)
            {
                string query = "DELETE FROM public.team_members WHERE person_id = @Id;";
                var result = await _connection.ExecuteAsync(query, new { Id = personId });
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

    public async Task<int> DeleteTeamAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "DELETE FROM public.team_members WHERE team_id = @Id;";
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

    public async Task<IList<TeamMemberViewModel>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM public.teammemberviewmodel " +
                $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<TeamMemberViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<TeamMemberViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<TeamMemberViewModel> GetByPersonIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM teammemberviewmodel where person_id = {id}";
            var result = await _connection.QuerySingleAsync<TeamMemberViewModel>(query);
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

    public async Task<IList<TeamMemberViewModel>> GetByTeamIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM teammemberviewmodel where team_id=@Id";
            var result = (await _connection.QueryAsync<TeamMemberViewModel>(query, new {Id = id})).ToList();
            return result;
        }
        catch
        {
            return new List<TeamMemberViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public Task<(int ItemsCount, IList<TeamMemberViewModel>)> SearchAsync(string search, PaginationParams @params)
    {
        throw new NotImplementedException();
    }
}
